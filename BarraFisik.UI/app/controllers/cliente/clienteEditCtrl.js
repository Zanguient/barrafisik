(function () {
    'use strict';

    app.controller('clienteEditCtrl', clienteEditCtrl);

    function clienteEditCtrl($scope, clienteData, horarioData, SweetAlert, $stateParams, $state) {
        var vm = this;
        $scope.cliente = {};

        activate();

        function activate() {
            clienteData.getCliente($stateParams.id).then(function (cliente) {
                //Convert Data Nascimento
                var dtNascimento = cliente.data.DtNascimento.toString().substring(0, 10);
                dtNascimento = new Date(dtNascimento);
                cliente.data.DtNascimento = new Date(dtNascimento.getTime() + dtNascimento.getTimezoneOffset() * 60000);

                //Convert Data Inscrição
                var dtInscricao = cliente.data.DtInscricao.toString().substring(0, 10);
                dtInscricao = new Date(dtInscricao);
                cliente.data.DtInscricao = new Date(dtInscricao.getTime() + dtInscricao.getTimezoneOffset() * 60000);

                $scope.cliente = cliente.data;

                //Horario
                //horarioData.getHorarioCliente($stateParams.id).then(function (horario) {
                //    $scope.horario = horario.data;
                //});

            }), function (error) {
                console.log(error);
            }
        }

        var _video = null,
        patData = null;

        $scope.patOpts = { x: 0, y: 0, w: 25, h: 25 };

        $scope.toggleWebCam = false;

        $scope.channel = {
            videoHeight: 320,
            videoWidth: 240,
            video: null // Will reference the video element on success;
        }
        $scope.webcamError = false;
        $scope.onError = function (err) {
            $scope.$apply(
                function () {
                    $scope.webcamError = err;
                }
            );
        };

        $scope.onSuccess = function () {
            // The video element contains the captured camera data
            _video = $scope.channel.video;
            $scope.$apply(function () {
                $scope.patOpts.w = _video.width;
                $scope.patOpts.h = _video.height;
                $scope.showDemos = true;
            });
        };

        $scope.onStream = function (stream) {
            // You could do something manually with the stream.
        };

        //Make a snapshot of the camera data and show it in another canvas.
        $scope.makeSnapshot = function () {
            if (_video) {
                var patCanvas = document.querySelector('#snapshot');
                if (!patCanvas) return;

                patCanvas.width = _video.width;
                patCanvas.height = _video.height;
                var ctxPat = patCanvas.getContext('2d');

                var idata = getVideoData($scope.patOpts.x, $scope.patOpts.y, $scope.patOpts.w, $scope.patOpts.h);
                ctxPat.putImageData(idata, 0, 0);

                //sendSnapshotToServer(patCanvas.toDataURL());
                $scope.cliente.Foto = patCanvas.toDataURL().replace(/^data:image\/(png|jpg);base64,/, "");
                patData = idata;
            }
        };

        /**
     * Redirect the browser to the URL given.
     * Used to download the image by passing a dataURL string
     */
        $scope.downloadSnapshot = function downloadSnapshot(dataURL) {
            window.location.href = dataURL;
        };

        var getVideoData = function getVideoData(x, y, w, h) {
            var hiddenCanvas = document.createElement('canvas');
            hiddenCanvas.width = _video.width;
            hiddenCanvas.height = _video.height;
            var ctx = hiddenCanvas.getContext('2d');
            ctx.drawImage(_video, 0, 0, _video.width, _video.height);
            return ctx.getImageData(x, y, w, h);
        };

        $scope.form = {

            submit: function (form) {
                var firstError = null;
                if (form.$invalid) {

                    var field = null, firstError = null;
                    for (field in form) {
                        if (field[0] != '$') {
                            if (firstError === null && !form[field].$valid) {
                                firstError = form[field].$name;
                            }

                            if (form[field].$pristine) {
                                form[field].$dirty = true;
                            }
                        }
                    }

                    angular.element('.ng-invalid[name=' + firstError + ']').focus();
                    SweetAlert.swal("O formulário contém alguns erros!", "Por favor verifique novamente os dados informados!", "error");
                    return;

                } else {
                    // Salva dados do cliente
                    clienteData.editCliente($scope.cliente).then(function () {
                        //$scope.horario.ClienteId = $stateParams.id;
                        //horarioData.editHorario($scope.cliente).then(function () {});

                        SweetAlert.swal("Sucesso!", "Cliente foi cadastrado com sucesso!", "success");

                        //Retorna para lista de clientes
                        $state.go('app.clientes.listar', {}, {reload: true});

                    }, function (error) {
                        var errors = [];
                        for (var key in error.ModelState) {
                            for (var i = 0; i < error.ModelState[key].length; i++) {
                                errors.push(error.ModelState[key][i]);
                                SweetAlert.swal("Erro ao cadastrar!", error.ModelState[key][i], "error");
                            }
                        }
                        vm.errors = errors;
                    });

                    
                }

            }
        };
    }
}());