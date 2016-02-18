(function () {
    "use strict";

    app.controller('estoqueCreateCtrl', estoqueCreateCtrl);

    function estoqueCreateCtrl($scope, $modalInstance, estoqueData, movimentacaoData, fornecedoresData, produtosCategoriaData, produtosData, armazemData, toaster) {
        var vm = this;

        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        }

        $scope.estoque = {
            DataMovimento: new Date()
        };    

        //List Fornecedores
        fornecedoresData.getAllAtivos().then(function (fornecedoresList) {
            $scope.fornecedores = fornecedoresList.data;
        });

        //List Produtos Categoria
        produtosCategoriaData.getAll().then(function (categorias) {
            $scope.categorias = categorias.data;
        });

        //List Produtos
        $scope.carregarProdutos = function () {
            $scope.produtos = [];
            if ($scope.estoque.ProdutoCategoriaId != null && $scope.estoque.ProdutoCategoriaId !== "") {                
                produtosData.getByCategoria($scope.estoque.ProdutoCategoriaId).then(function (produtos) {
                    if (produtos.data.length === 0)
                        toaster.pop('error', '', 'Não existem produtos para esta categoria');
                    $scope.produtos = produtos.data;
                });
            }
        }
        
        //List Armazem
        armazemData.getArmazem().then(function (armazens) {
            $scope.armazem = armazens.data;
        });

        $scope.form = {
            submit: function (form, estoque) {
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
                    estoque.DataCadastro = new Date();
                    estoqueData.add(estoque).success(function (estoqueviewmodel) {
                        estoque.EstoqueId = estoqueviewmodel.EstoqueId;
                        movimentacaoData.add(estoque).success(function() {
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