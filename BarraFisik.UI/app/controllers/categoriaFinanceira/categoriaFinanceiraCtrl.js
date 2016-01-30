(function () {
    'use strict';

    app.controller('categoriaFinanceiraCtrl', categoriaFinanceiraCtrl);

    function categoriaFinanceiraCtrl($scope, categoriaFinanceiraData, ngTableParams, $filter, SweetAlert) {
        var vm = this;
        vm.categorias = [];
        vm.title = 'categoriaFinanceiraCtrl';
        $scope.createCategoria = false;

        $scope.listTipo = {
            data: [{
                name: 'Receitas'
            }, {
                name: 'Despesas'
            }]
        };

        activate();

        function activate() {
            categoriaFinanceiraData.getCategorias().then(function (result) {
                vm.categorias = result.data;
            });
        }

        $scope.tableParams = new ngTableParams({
            page: 1, // show first page
            count: 40, // count per page
            sorting: {
                Tipo: 'asc', // initial sorting
                Categoria: 'asc'
            }
        }, {
            counts: [],
            total: vm.categorias.length, // length of data
            getData: function ($defer, params) {
                var orderedData = params.sorting() ? $filter('orderBy')(vm.categorias, params.orderBy()) : m.categorias;
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });

        $scope.$watch('vm.categorias', function () {
            $scope.tableParams.reload();
        });

        $scope.editId = -1;

        $scope.setEditId = function (pid) {
            $scope.editId = pid;
        };

        $scope.delete = function (id) {
            SweetAlert.swal({
                title: "Confirmar Exclusão",
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

                    categoriaFinanceiraData.deleteCategoria(id).then(function () {
                        SweetAlert.swal("Excluído!", "Dados apgados com sucesso!", "success");
                        $.each(vm.categorias, function (i) {
                            if (vm.categorias[i].CategoriaFinanceiraId === id) {
                                vm.categorias.splice(i, 1);
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

        $scope.categoria = {
            Tipo: null,
            Categoria: null,
            SubCategoria: null
        };

        $scope.cancelCreate = function (form) {
            $scope.createCategoria = false;
            $scope.categoria = {
                Tipo: null,
                Categoria: null,
                SubCategoria: null
            };
            form.$setPristine(true);
        }

        $scope.form = {

            submit: function (form, categoria) {
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
                    // Cadastra a categoria
                    categoriaFinanceiraData.addCategoria(categoria).success(function (categoria) {
                        //Limpa o formulario
                        form.$setPristine(true);
                        $scope.categoria = {
                            Tipo: null,
                            Categoria: null,
                            SubCategoria: null
                        };
                        SweetAlert.swal("Cadastrado!", "Categoria cadastrada com sucesso!", "success");
                        $scope.createCategoria = false;
                        vm.categorias.push(categoria);
                        $scope.tableParams.reload();
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

            submit: function (form, categoria) {
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
                    categoriaFinanceiraData.editCategoria(categoria).success(function () {
                        SweetAlert.swal("Atualizado!", "Categoria atualizada com sucesso!", "success");
                        $scope.editId = -1;
                    }).error(function (error) {
                        var errors = [];
                        for (var key in error.ModelState) {
                            for (var i = 0; i < error.ModelState[key].length; i++) {
                                errors.push(error.ModelState[key][i]);
                                SweetAlert.swal("Erro ao salvar!", error.ModelState[key][i], "error");
                            }
                        }
                        vm.errors = errors;
                    });
                }

            }
        };
    }
})();
