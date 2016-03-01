(function () {
    "use strict";

    app.controller('estoqueEntradaCtrl', estoqueEntradaCtrl);

    function estoqueEntradaCtrl($scope, $modalInstance, estoqueData, movimentacaoData, fornecedoresData, estoque, toaster) {
        var vm = this;
        $scope.estoque = estoque;
        $scope.entrada = {
            EstoqueId: estoque.EstoqueId,
            ProdutoId: estoque.Produtos.ProdutoId,
            ArmazemId: estoque.Armazem.ArmazemId,
            DataMovimento: new Date()
        };


    

        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        }

        //List Fornecedores
        fornecedoresData.getAllAtivos().then(function (fornecedoresList) {
            $scope.fornecedores = fornecedoresList.data;
        });
        

        $scope.form = {
            submit: function (form, entrada) {
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
                    //Upate Valores
                    var quantidadeOriginal = entrada.Quantidade;
                    entrada.Quantidade = entrada.Quantidade + estoque.Quantidade;
                    entrada.ValorTotal = entrada.Quantidade * entrada.ValorUnitario;
                    entrada.TotalVendido = estoque.TotalVendido;
                    entrada.SaldoVenda = estoque.SaldoVenda;
                    entrada.DataCadastro = new Date();

                    estoqueData.edit(entrada).success(function () {
                        entrada.Quantidade = quantidadeOriginal;
                        entrada.ValorTotalCusto = entrada.Quantidade * entrada.ValorUnCusto;
                        movimentacaoData.add(entrada).success(function() {
                            toaster.pop('info', '', 'Movimentação Estoque criada!');
                        });
                        $modalInstance.close();
                    }).error(function (error) {
                        var errors = [];
                        for (var key in error.ModelState) {
                            for (var i = 0; i < error.ModelState[key].length; i++) {
                                errors.push(error.ModelState[key][i]);
                            }
                        }
                        vm.errors = errors;
                        toaster.pop("error", error.Message, "");
                    });
                }
            }
        };
    }

    
}());