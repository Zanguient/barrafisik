(function () {
    "use strict";

    app.controller('vendasCreateCtrl', vendasCreateCtrl);

    function vendasCreateCtrl($scope, $modalInstance, estoqueData, vendasData, clienteData, tipoPagamentoData, vendasProdutosData, armazemData, receitasData, funcionariosData, toaster) {
        var vm = this;
        $scope.isCliente = true;
        $scope.totalProdutos = 0;
        $scope.produtos = [];
        $scope.produto = {};


        $scope.add = function (produto) {
            //produto.Valor = produto.Valor * produto.Quantidade;
            $scope.totalProdutos = $scope.totalProdutos + (produto.Valor * produto.Quantidade);
            $scope.produtos.push(produto);
            $scope.produto = {}
        }

        $scope.remove = function ($index, produto) {
            $scope.totalProdutos = $scope.totalProdutos - (produto.Valor * produto.Quantidade);
            $scope.produtos.splice($index, 1);
        }

        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        }

        $scope.venda = {
            DataVencimento: new Date(),
            DataPagamento: new Date()
        };    

        //List Clientes
        clienteData.getClientes().then(function (clientesList) {
            $scope.clientes = clientesList.data;
        });

        //List Funcionarios
        funcionariosData.getAll().then(function (funcionariosList) {
            $scope.funcionarios = funcionariosList.data;
        });

        //List Armazem
        armazemData.getArmazem().then(function (armazens) {
            $scope.armazem = armazens.data;
        });

        //List Estoques by armazem
        $scope.carregarEstoques = function () {
            $scope.estoques = [];
            estoqueData.getByArmazem($scope.venda.ArmazemId).then(function (estoques) {
                if (estoques.data.length === 0)
                    toaster.pop('error', '', 'Não existem produtos neste armazém');
                $scope.estoques = estoques.data;
            });
        }

        //List Tipos de Pagamento
        tipoPagamentoData.getTipos().then(function (tipos) {
            $scope.tiposPagamento = tipos.data;
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
        

        $scope.form = {
            submit: function (form, venda) {
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
                    if (venda.Cliente != null) {
                        venda.ClienteId = venda.Cliente.ClienteId;
                        venda.Cliente = null;
                    }

                    var receita = venda;
                    receita.Valor = $scope.totalProdutos;
                    receita.ValorTotal = receita.Valor;
                    receita.Observacao = "Venda de produtos";

                    if (venda.DataPagamento != null)
                        receita.Situacao = 'Quitado';
                    else receita.Situacao = 'Pendente';
                    venda.CategoriaFinanceiraId = '1c1278df-f5a5-4407-a0c4-bdbb71c362b1';
                    venda.SubCategoriaFinanceiraId = '090b2553-c505-44a5-8a95-552aec30eee2';
                    //Cria a Receita
                    receitasData.addReceita(receita).success(function (receita) {
                        venda.ReceitasId = receita.ReceitasId;
                        vendasData.add(venda).success(function (venda) {
                            //Adicionar Vendas Produtos
                            vendasProdutosData.addVendasProdutos($scope.produtos, venda.VendaId).success(function () { });
                            $modalInstance.close();                                                     
                        }).error(function (error) {
                            var errors = [];
                            for (var key in error.ModelState) {
                                if (error.ModelState.hasOwnProperty(key)) {
                                    for (var i = 0; i < error.ModelState[key].length; i++) {
                                        errors.push(error.ModelState[key][i]);
                                    }
                                }
                            }
                            vm.errors = errors;
                            toaster.pop("error", error.Message, "");
                        });
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