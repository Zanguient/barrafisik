(function () {
    'use strict';

    app.controller('receitasCtrl', receitasCtrl);

    function receitasCtrl($scope, ngTableParams, receitasData, SweetAlert, $filter, categoriaFinanceiraData, $state) {
        var vm = this;
        vm.title = 'receitasCtrl';
        vm.receitas = [];
        vm.categorias = [];
        $scope.categorias = [{ id: "", title: "" }];
        $scope.createReceita = false;

        activate();

        function activate() {
            $scope.$emit('LOAD');
            receitasData.getReceitas().then(function (result) {
                vm.receitas = result.data;
            });

            categoriaFinanceiraData.getCategoriaByTipo("Receitas").then(function (cat) {
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
            total: vm.receitas.length, // length of data
            getData: function ($defer, params) {
                // use build-in angular filter
                var orderedData = params.sorting() ? $filter('orderBy')(vm.receitas, params.orderBy()) : vm.receitas;
                orderedData = $filter('filter')(orderedData, params.filter());

                params.total(orderedData.length);
                $scope.total = orderedData.length;

                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });

        $scope.$watch('vm.receitas', function () {
            $scope.tableParams.reload();
        });

        $scope.editId = -1;

        $scope.setEditId = function (pid) {
            $scope.editId = pid;
        };

        $scope.receita = {
            Nome: null,
            Valor: null,
            Observacao: null,
        };

        $scope.cancelCreate = function (form) {
            $scope.createReceita = false;
            $scope.receita = {
                Nome: null,
                Valor: null,
                Observacao: null,
            };
            form.$setPristine(true);
        }

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
                    // Cadastra a receita
                    receita.Valor = receita.Valor.toString().replace(",", ".");
                    receitasData.addReceita(receita).success(function (receita) {
                        //Limpa o formulario
                        form.$setPristine(true);
                        $scope.receita = {
                            Nome: null,
                            Valor: null,
                            Observacao: null,
                        };
                        SweetAlert.swal("Cadastrado!", "Receita cadastrada com sucesso!", "success");
                        $scope.createReceita = false;
                        $state.go($state.current, {}, { reload: true });
                        //vm.receitas.push(receita);
                        //$scope.tableParams.reload();
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
                    // Cadastra a receita
                    receita.Valor = receita.Valor.toString().replace(",", ".");
                    receitasData.editReceita(receita).success(function (receita) {
                        SweetAlert.swal("Atualizado!", "Receita salva com sucesso!", "success");
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

                    receitasData.deleteReceita(id).then(function () {
                        SweetAlert.swal("Excluído!", "Dados apgados com sucesso!", "success");
                        $.each(vm.receitas, function (i) {
                            if (vm.receitas[i].ReceitasId === id) {
                                vm.receitas.splice(i, 1);
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
