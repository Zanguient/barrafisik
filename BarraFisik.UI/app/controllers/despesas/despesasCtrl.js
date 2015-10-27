(function () {
    "use strict";

    app.controller('despesasCtrl', despesasCtrl);

    function despesasCtrl($scope, ngTableParams, despesasData, SweetAlert, $filter, categoriaFinanceiraData, $state) {
        var vm = this;
        vm.despesas = [];
        vm.categorias = [];
        $scope.categorias = [{id: "", title: ""}];
        $scope.createDespesa = false;

        activate();

        function activate() {
            $scope.$emit('LOAD');
            despesasData.getDespesas().then(function (result) {
                vm.despesas = result.data;
            });

            categoriaFinanceiraData.getCategoriaByTipo("Despesas").then(function (cat) {
                vm.categorias = cat.data;
                
                angular.forEach(vm.categorias, function (value, key) {
                    $scope.categorias.push({ id: value.CategoriaFinanceiraId, title: value.Categoria });
                });
            });

            $scope.$emit('UNLOAD');
        }

        $scope.tableParams = new ngTableParams({
            page: 1, // show first page
            count: 50, // count per page
            sorting: {
                Data: 'desc'
            }            
        }, {
            counts: [],
            total: vm.despesas.length, // length of data
            getData: function ($defer, params) {
                // use build-in angular filter                   
                var orderedData = params.sorting() ? $filter('orderBy')(vm.despesas, params.orderBy()) : vm.despesas;
                orderedData = $filter('filter')(orderedData, params.filter());

                params.total(orderedData.length);
                $scope.total = orderedData.length;

                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });

        $scope.$watch('vm.despesas', function () {
            $scope.tableParams.reload();
        });

        $scope.editId = -1;

        $scope.setEditId = function (pid) {
            $scope.editId = pid;
        };

        $scope.despesa = {
            Nome: null,
            Valor: null,
            Observacao: null,
        };

        $scope.cancelCreate = function (form) {
            $scope.createDespesa = false;
            $scope.despesa = {
                Nome: null,
                Valor: null,
                Observacao: null,
            };
            form.$setPristine(true);
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
                    // Cadastra a receita
                    despesa.Valor = despesa.Valor.toString().replace(",", ".");
                    despesasData.addDespesa(despesa).success(function (despesa) {
                        //Limpa o formulario
                        form.$setPristine(true);
                        $scope.despesa = {
                            Nome: null,
                            Valor: null,
                            Observacao: null,
                        };
                        SweetAlert.swal("Cadastrado!", "Despesa cadastrada com sucesso!", "success");
                        $scope.createDespesa = false;
                        $state.go($state.current, {}, { reload: true });
                    }).error(function (error) {
                        var errors = [];
                        for (var key in error.ModelState) {
                            for (var i = 0; i < error.ModelState[key].length; i++) {
                                errors.push(error.ModelState[key][i]);
                                SweetAlert.swal("Erro ao cadastrar!", error.ModelState[key][i], "error");
                            }
                        }
                        vm.errors = errors;
                    });
                }

            }
        };

        $scope.formEdit = {
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
                    // Cadastra a receita
                    despesa.Valor = despesa.Valor.toString().replace(",", ".");
                    despesasData.editDespesa(despesa).success(function (despesa) {
                        SweetAlert.swal("Atualizado!", "Despesa salva com sucesso!", "success");
                        $scope.editId = -1;
                        $state.go($state.current, {}, { reload: true });
                    }).error(function (error) {
                        var errors = [];
                        for (var key in error.ModelState) {
                            for (var i = 0; i < error.ModelState[key].length; i++) {
                                errors.push(error.ModelState[key][i]);
                                SweetAlert.swal("Erro ao cadastrar!", error.ModelState[key][i], "error");
                            }
                        }
                        vm.errors = errors;
                    });
                }

            }
        };

        $scope.delete = function (id) {
            SweetAlert.swal({
                title: "Confirmar Exclusão?",
                text: "Tem certeza que deseja excluir esse registro?",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Confirmar",
                cancelButtonText: "Cancelar",
                closeOnConfirm: false,
                closeOnCancel: false
            }, function (isConfirm) {
                if (isConfirm) {
                    SweetAlert.swal({
                        title: "Excluído!",
                        text: "Registro excluído com sucesso.",
                        type: "success",
                        confirmButtonColor: "#007AFF"
                    });

                    despesasData.deleteDespesa(id).then(function () {
                        SweetAlert.swal("Excluído!", "Dados apgados com sucesso!", "success");
                        $.each(vm.despesas, function (i) {
                            if (vm.despesas[i].DespesasId === id) {
                                vm.despesas.splice(i, 1);
                                return false;
                            }
                        });
                        $scope.tableParams.reload();
                    });
                } else {
                    SweetAlert.swal({
                        title: "Cancelado",
                        text: "Operação de exclusão cancelada",
                        type: "error",
                        confirmButtonColor: "#007AFF"
                    });
                }
            });
        }
    }
})();