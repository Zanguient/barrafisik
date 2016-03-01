(function () {
    "use strict";

    app.controller('vendasProdutosCtrl', vendasProdutosCtrl);

    function vendasProdutosCtrl($scope, modalService, total, $modalInstance, armazemData, estoqueData, vendasProdutosData, vendaId, toaster) {
        var vm = this;
        $scope.addProduto = false;
        vm.produtos = [];
        $scope.venda = {};
        $scope.produto = {};
        $scope.total = 0;

        //List Estoques by armazem
        $scope.carregarEstoques = function () {
            $scope.estoques = [];
            estoqueData.getByArmazem($scope.venda.ArmazemId).then(function (estoques) {
                if (estoques.data.length === 0)
                    toaster.pop('error', '', 'Não existem produtos neste armazém');
                $scope.estoques = estoques.data;
            });
        }

        //List Armazem
        armazemData.getArmazem().then(function (armazens) {
            $scope.armazem = armazens.data;
        });

        //Preço
        $scope.carregarPreco = function () {
            $scope.estoque = [];
            estoqueData.getById($scope.produto.EstoqueId).then(function (estoque) {
                $scope.produto.Nome = estoque.data.Produtos.Nome;
                $scope.produto.Valor = estoque.data.ValorUnitario;
                $scope.estoque = estoque.data;
            });
        }

        active();

        function active() {
            vendasProdutosData.getByVenda(vendaId).then(function (result) {
                vm.produtos = result.data;
            });
        }

        $scope.$watch("vm.produtos", function () {
            $scope.total = 0;
            angular.forEach(vm.produtos, function(value, key) {
                $scope.total = $scope.total + (value.Estoque.ValorUnitario * value.Quantidade);
            });
        });

        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
            //$modalInstance.close();
        }


        $scope.form = {
            submit: function (form, produto) {
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
                    produto.VendaId = vendaId;
                    vendasProdutosData.addProduto(produto).success(function () {
                        vendasProdutosData.getByVenda(vendaId).then(function (result) {
                            vm.produtos = result.data;
                        });
                        $scope.produto = {};
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

        $scope.delete = function (id) {
            var modalOptions = {
                closeButtonText: 'Cancelar',
                actionButtonText: 'Excluir',
                headerText: 'Excluir ?',
                bodyText: 'Tem certeza que deseja exlcuir este produto?'
            };

            modalService.showModal({}, modalOptions).then(function () {
                vendasProdutosData.remove(id).then(function () {
                    toaster.pop('success', '', 'Produto Excluído com Sucesso!');
                    vendasProdutosData.getByVenda(vendaId).then(function (result) {
                        vm.produtos = result.data;
                    });
                }), function (data) {

                };
            });
        }

       
    }

    
}());