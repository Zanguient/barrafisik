(function () {
    "use strict";

    app.controller('receitasEditCtrl', receitasEditCtrl);

    function receitasEditCtrl($scope, $modalInstance, receitasData, funcionariosData, clienteData, categoriaFinanceiraData, tipoPagamentoData, receita, subCategoriaData) {
        var vm = this;

        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        }        

        if (receita.DataPagamento !== null) {
            //Convert Data Pagamento
            var dtPagamento = receita.DataPagamento.toString().substring(0, 10);
            dtPagamento = new Date(dtPagamento);
            receita.DataPagamento = new Date(dtPagamento.getTime() + dtPagamento.getTimezoneOffset() * 60000);
        }

        //Convert Data Vencimento
        var dtVencimento = receita.DataVencimento.toString().substring(0, 10);
        dtVencimento = new Date(dtVencimento);
        receita.DataVencimento = new Date(dtVencimento.getTime() + dtVencimento.getTimezoneOffset() * 60000);

        if (receita.SubCategoriaFinanceiraId != null) {
            subCategoriaData.getByCategoria(receita.CategoriaFinanceiraId).then(function (subcat) {
                $scope.subcategorias = subcat.data;
            });
        }

        $scope.receita = receita;

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

        //List Tipos de Pagamento
        tipoPagamentoData.getTipos().then(function (tipos) {
            $scope.tiposPagamento = tipos.data;
        });

        //List Funcionarios
        funcionariosData.getAllAtivos().then(function (funcionariosList) {
            $scope.funcionarios = funcionariosList.data;
        });

        //List Clientes
        clienteData.getClientes().then(function (clientesList) {
            $scope.clientes = clientesList.data;
        });

        if (receita.ClienteId != null) {
            clienteData.getById(receita.ClienteId).then(function (result) {
                receita.Cliente = result.data;
            });
        }

        //List CategoriaFinanceira
        categoriaFinanceiraData.getCategoriaByTipo('Receitas').then(function (categoriasList) {
            $scope.categorias = categoriasList.data;
        });

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
                        receitasData.editMensalidade(receita).success(function () {
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
                        receitasData.editReceita(receita).success(function () {
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