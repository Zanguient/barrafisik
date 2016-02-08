(function() {
    "use strict";

    app.controller('createReceitasAvaliacaoFisicaCtrl', createReceitasAvaliacaoFisicaCtrl);

    function createReceitasAvaliacaoFisicaCtrl($scope, Cliente, $modalInstance, tipoPagamentoData, receitasAvaliacaoFisicaData, $timeout, receitasData) {
        var vm = this;              

        //List Tipos de Pagamento
        tipoPagamentoData.getTipos().then(function (tipos) {
            $scope.tipos = tipos.data;
        });

        //Format Data Atual
        var today = new Date();

        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        $scope.avaliacaofisica = {
            DataPagamento: today,
            DataVencimento: new Date(today.getFullYear(), today.getMonth(), 10)
        }

        $scope.form = {
            submit: function (form, avaliacaoFisica) {
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
                    // Cadastra
                    if (avaliacaoFisica.DataPagamento === today)
                        avaliacaoFisica.DataPagamento = new Date();

                    if (avaliacaoFisica.DataPagamento != null) {
                        avaliacaoFisica.Situacao = "Quitado";
                    } else avaliacaoFisica.Situacao = "Pendente";

                    if (receita.Cliente != null) {
                        receita.ClienteId = receita.Cliente.ClienteId;
                        receita.Cliente = null;
                    }

                    avaliacaoFisica.ClienteId = Cliente.ClienteId;
                    avaliacaoFisica.ValorTotal = avaliacaoFisica.Valor;
                    receitasData.addAvaliacaoFisica(avaliacaoFisica).success(function () {
                        $modalInstance.close();
                    }).error(function (error) {
                        var errors = [];
                        for (var key in error.ModelState) {
                            for (var i = 0; i < error.ModelState[key].length; i++) {
                                errors.push(error.ModelState[key][i]);
                            }
                        }
                        vm.errors = errors;
                    });
                }
            }
        };
    }

}());