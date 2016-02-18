(function () {
    "use strict";

    app.controller('produtosCategoriaCtrl', produtosCategoriaCtrl);

    function produtosCategoriaCtrl($scope, ngTableParams, produtosCategoriaData, SweetAlert, $filter) {
        var vm = this;
        vm.categorias = [];
        $scope.createCategoria = false;

        activate();

        function activate() {
            $scope.$emit('LOAD');

            produtosCategoriaData.getAll().then(function (result) {
                vm.categorias = result.data;                
            });

            $scope.$emit('UNLOAD');
        }

        $scope.tableParams = new ngTableParams({
            page: 1, // show first page
            count: 40, // count per page
            sorting: {
                Nome: 'asc' // initial sorting
            }
        }, {
            counts: [],
            total: vm.categorias.length, // length of data
            getData: function ($defer, params) {
                // use build-in angular filter
                var orderedData = params.sorting() ? $filter('orderBy')(vm.categorias, params.orderBy()) : vm.categorias;
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });

        $scope.$watch('vm.categorias', function () {
            $scope.tableParams.reload();
        });


        $scope.save = function (categoria) {
            produtosCategoriaData.edit(categoria).then(function () {
                SweetAlert.swal("Atualizado!", "Categoria do Produto salva com sucesso!", "success");
                $scope.editId = -1;
            });
        }

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
                closeOnCancel: true
            }, function (isConfirm) {
                if (isConfirm) {
                    produtosCategoriaData.remove(id).then(function () {
                        SweetAlert.swal("Excluído!", "Dados apgados com sucesso!", "success");
                        $.each(vm.categorias, function (i) {
                            if (vm.categorias[i].ProdutoCategoriaId === id) {
                                vm.categorias.splice(i, 1);
                                return false;
                            }
                        });
                        $scope.tableParams.reload();
                    });
                }                 
            });
        }

        var categoriaDefault = {
            Nome: null,
            Descricao: null,
        };

        $scope.categoria = angular.copy(categoriaDefault);

        $scope.cancelCreate = function (form) {
            $scope.createCategoria = false;
            $scope.categoria = angular.copy(categoriaDefault);
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
                    produtosCategoriaData.add(categoria).success(function (result) {
                        SweetAlert.swal("Cadastrado!", "Categoria do Produto cadastrada com sucesso!", "success");

                        //Limpa o formulario
                        form.$setPristine(true);
                        $scope.categoria = angular.copy(categoriaDefault);                        
                        $scope.createCategoria = false;
                        produtosCategoriaData.getAll().then(function (result) {
                            vm.categorias = result.data;
                        });
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
                    produtosCategoriaData.edit(categoria).success(function () {
                        SweetAlert.swal("Atualizado!", "Categoria do Produto salva com sucesso!", "success");
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

        $scope.editId = -1;

        $scope.setEditId = function (pid) {
            $scope.editId = pid;
        };
    };
})();