(function() {
    "use strict";

    app.controller('createMensalidadesCtrl', createMensalidadesCtrl);

    function createMensalidadesCtrl($scope, ClienteId, $modalInstance, mensalidadesData, $timeout, SweetAlert) {
        var vm = this;
        vm.mensalidades = [];

        var today = new Date();
        
        $scope.mensalidade = {
            DataPagamento: today,
            AnoReferencia: today.getFullYear(),
            MesReferencia: today.getMonth() + 1,
            ValorPago: '0',
        }

        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        /**
         * /
         * @returns {DatePicker} 
         */
        $scope.today = function () {
            $scope.DataPagamento = new Date();
        };
        $scope.today();


        $scope.clear = function () {
            $scope.DataPagamento = null;
        };

        $scope.toggleMin = function () {
            $scope.minDate = ($scope.minDate) ? null : new Date();
        };
        $scope.toggleMin();

        $scope.opened = [];
        $scope.open = function (index) {
            $timeout(function () {
                $scope.opened[index] = true;
            });
        };

        $scope.dateOptions = {
            'year-format': "'yyyy'",
            'starting-day': 1
        };

        $scope.form = {

            submit: function (form, mensalidade) {
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
                    return;

                } else {
                    // Cadastra mensalidade
                    mensalidade.ClienteId = ClienteId;
                    mensalidade.ValorPago = mensalidade.ValorPago.replace(',', '.');
                    mensalidadesData.addMensalidade(mensalidade).success(function () {
                        $modalInstance.close();
                    }).error(function (error) {
                        var errors = [];
                        for (var key in error.ModelState) {
                            for (var i = 0; i < error.ModelState[key].length; i++) {
                                errors.push(error.ModelState[key][i]);
                                //SweetAlert.swal("Erro ao cadastrar!", error.ModelState[key][i], "error");
                            }
                        }
                        vm.errors = errors;
                    });
                }

            }
        };
    }

}());