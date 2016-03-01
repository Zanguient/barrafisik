(function () {
    "use strict";

    app.controller('receitasCreateCtrl', receitasCreateCtrl);

    function receitasCreateCtrl($scope, $modalInstance, receitasData, funcionariosData, clienteData, categoriaFinanceiraData, tipoPagamentoData, subCategoriaData, toaster) {
        var vm = this;
        $scope.isCliente = true;

        //Data atual 
        var today = new Date();

        $scope.receita = {
            MesReferencia: today.getMonth()+1,
            AnoReferencia: today.getFullYear()
        };

        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        }

        //List Tipos de Pagamento
        tipoPagamentoData.getTipos().then(function (tipos) {
            $scope.tiposPagamento = tipos.data;
        });

        //List SubCategorias
        $scope.carregarSubcategorias = function () {
            if ($scope.receita.CategoriaFinanceiraId == undefined) {
                $scope.receita.SubCategoriaFinanceiraId = null;
                $scope.subcategorias = [];
            } else {
                subCategoriaData.getByCategoria($scope.receita.CategoriaFinanceiraId).then(function (subcat) {
                    $scope.subcategorias = subcat.data;
                });
            }
        }

        //List Funcionarios
        funcionariosData.getAllAtivos().then(function (funcionariosList) {
            $scope.funcionarios = funcionariosList.data;
        });

        //List Clientes
        clienteData.getClientes().then(function (clientesList) {
            $scope.clientes = clientesList.data;
        });

        //List CategoriaFinanceira
        categoriaFinanceiraData.getCategoriaByTipo('Receitas').then(function (categoriasList) {
            $scope.categorias = categoriasList.data;
        });

        function getSituacao(vencimento, atual) {
            if (vencimento.getTime() >= atual.getTime()) {
                return "Pendente";
            }
            return "Vencido";
        };

        $scope.form = {
            submit: function (form, receita) {                
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
                    //if (receita.DataPagamento != null) {
                    //    receita.Situacao = "Quitado";
                    //} else receita.Situacao = getSituacao(receita.DataVencimento, new Date());

                    if (receita.DataPagamento != null) {
                        receita.Situacao = "Quitado";
                    } else receita.Situacao = "Pendente";

                    if (receita.Cliente != null) {
                        receita.ClienteId = receita.Cliente.ClienteId;
                        receita.Cliente = null;
                    } 

                    //Subcategoria Mensalidade
                    if (receita.SubCategoriaFinanceiraId == '0d57c87d-3bd9-420b-ab98-123fdb75a269') {
                        //Cliente não selecionado ou inválido
                        if (receita.ClienteId == null) {
                            toaster.pop('error', '', 'Cliente Inválido ou não selecionado!');
                            return false;
                        }
                        receitasData.addMensalidade(receita).success(function () {
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
                    } else {
                        receitasData.addReceita(receita).success(function () {
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
            }
        };
    }

    
}());