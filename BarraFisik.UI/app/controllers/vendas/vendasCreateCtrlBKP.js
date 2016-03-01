(function () {
    "use strict";

    app.controller('vendasCreateCtrl', vendasCreateCtrl);

    function vendasCreateCtrl($scope, $modalInstance, estoqueData, vendasData, clienteData, tipoPagamentoData, armazemData, receitasData, toaster) {
        var vm = this;
        $scope.isCliente = true;        

        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        }

        $scope.venda = {
            DataVencimento: new Date(),
            DataPagamento: new Date()
        };    

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

        //List Clientes
        clienteData.getClientes().then(function (clientesList) {
            $scope.clientes = clientesList.data;
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
            estoqueData.getById($scope.venda.EstoqueId).then(function(estoque) {
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
                    //Valor pago >= valor Unitario*quantidade : valorpago = valorUnitario*quantidade
                    if (venda.ValorRecebido >= ($scope.estoque.ValorUnitario * venda.Quantidade))
                        venda.ValorPago = $scope.estoque.ValorUnitario * venda.Quantidade;

                    if (venda.Cliente != null) {
                        venda.ClienteId = venda.Cliente.ClienteId;
                        venda.Cliente = null;
                    }

                    //Verifica dataPagamento com Valor Recebido
                    if (venda.DataPagamento != null && (venda.ValorRecebido === 0 || venda.ValorRecebido === null)) {
                        toaster.pop("error", "", "Nenhum valor recebido!");
                        return false;
                    }

                    var receita = venda;
                    receita.Valor = $scope.estoque.ValorUnitario * venda.Quantidade;
                    receita.ValorTotal = $scope.estoque.ValorUnitario * venda.Quantidade;
                    receita.Observacao = 'Venda de ' + venda.Quantidade +' '+ $scope.estoque.Produtos.Nome;

                    if (venda.DataPagamento != null)
                        receita.Situacao = 'Quitado';
                    else receita.Situacao = 'Pendente';
                    venda.CategoriaFinanceiraId = '1c1278df-f5a5-4407-a0c4-bdbb71c362b1';
                    venda.SubCategoriaFinanceiraId = '090b2553-c505-44a5-8a95-552aec30eee2';
                    //Cria a Receita
                    receitasData.addReceita(receita).success(function (receita) {
                        venda.ReceitasId = receita.ReceitasId;
                        vendasData.add(venda).success(function () {
                            //Atualiza Estoque
                            var estoque = $scope.estoque;
                            estoque.Quantidade = estoque.Quantidade - venda.Quantidade;
                            estoque.SaldoVenda = estoque.SaldoVenda + ($scope.estoque.ValorUnitario * venda.Quantidade);
                            estoqueData.edit(estoque).then(function () {
                                $modalInstance.close();
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