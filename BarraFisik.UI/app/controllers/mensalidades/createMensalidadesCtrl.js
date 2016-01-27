(function() {
    "use strict";

    app.controller('createMensalidadesCtrl', createMensalidadesCtrl);

    function createMensalidadesCtrl($scope, Cliente, $modalInstance, tipoPagamentoData, mensalidadesData, $timeout, SweetAlert) {
        var vm = this;
        vm.mensalidades = [];

        //List Tipos de Pagamento
        tipoPagamentoData.getTipos().then(function (tipos) {
            $scope.tipos = tipos.data;
        });

        //Format Data Atual
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1;
        var yyyy = today.getFullYear();

        if (dd < 10) {
            dd = '0' + dd
        }

        if (mm < 10) {
            mm = '0' + mm
        }

        today = dd + '/' + mm + '/' + yyyy;


        $scope.mensalidade = {
            DataPagamento: today,
            AnoReferencia: yyyy,
            MesReferencia: parseInt(mm),
            ValorPago: Cliente.Valor
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
                    if (mensalidade.DataPagamento === today)
                        mensalidade.DataPagamento = new Date();

                    mensalidade.ClienteId = Cliente.ClienteId;
                    mensalidade.ValorPago = mensalidade.ValorPago.toString().replace(",",".");
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