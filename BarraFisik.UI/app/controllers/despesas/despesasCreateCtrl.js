(function () {
    "use strict";

    app.controller('despesasCreateCtrl', despesasCreateCtrl);

    function despesasCreateCtrl($scope, $modalInstance, despesasData, funcionariosData, fornecedoresData, categoriaFinanceiraData, tipoPagamentoData, subCategoriaData) {
        var vm = this;

        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        }

        //List Tipos de Pagamento
        tipoPagamentoData.getTipos().then(function (tipos) {
            $scope.tiposPagamento = tipos.data;
        });

        //List SubCategorias
        $scope.carregarSubcategorias = function () {            
            if ($scope.despesa.CategoriaFinanceiraId == undefined) {
                $scope.despesa.SubCategoriaFinanceiraId = null;
                $scope.subcategorias = [];
            } else {                
                subCategoriaData.getByCategoria($scope.despesa.CategoriaFinanceiraId).then(function (subcat) {
                    $scope.subcategorias = subcat.data;
                });
            }
        }

        //List Funcionarios
        funcionariosData.getAllAtivos().then(function (funcionariosList) {
            $scope.funcionarios = funcionariosList.data;
        });

        //List Fornecedores
        fornecedoresData.getAllAtivos().then(function (fornecedoresList) {
            $scope.fornecedores = fornecedoresList.data;
        });

        //List CategoriaFinanceira
        categoriaFinanceiraData.getCategoriaByTipo('Despesas').then(function (categoriasList) {
            $scope.categorias = categoriasList.data;
        });

        function getSituacao(vencimento, atual) {
            if (vencimento.getTime() >= atual.getTime()) {
                return "Pendente";
            }
            return "Vencido";
        };

        $scope.soma = function () {
            $scope.despesa.ValorTotal = $scope.despesa.Valor;
        }

        $scope.form = {
            submit: function (form, despesa) {
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
                    //Situação
                    if (despesa.DataPagamento != null) {
                        despesa.Situacao = "Quitado";
                    } else despesa.Situacao = getSituacao(despesa.DataVencimento, new Date());

                    despesasData.addDespesa(despesa).success(function () {
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