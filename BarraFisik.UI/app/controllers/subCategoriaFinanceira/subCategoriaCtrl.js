(function() {
    "use strict";

    app.controller('subCategoriaCtrl', subCategoriaCtrl);

    function subCategoriaCtrl($scope, $modalInstance, modalService, ngTableParams, $filter, toaster, SweetAlert, categoria, subCategoriaData) {
        var vm = this;
        vm.subCategorias = [];
        $scope.createSubCategoria = false;
        $scope.sc = {};
        
        $scope.categoria = categoria;

        subCategoriaData.getByCategoria(categoria.CategoriaFinanceiraId).then(function (result) {
            vm.subCategorias = result.data;
        })

        vm.cancel = function() {
            $modalInstance.dismiss('cancel');
        }

        $scope.editId = -1;

        $scope.setEditId = function (pid) {
            $scope.editId = pid;
        };

        $scope.cancelCreate = function (form) {
            $scope.createSubCategoria = false;
            $scope.sc = {};
            form.$setPristine(true);
        }

        $scope.tableParams = new ngTableParams({
            page: 1,
            count: 12, 
            sorting: {
                SubCategoria: 'asc' 
            }
        }, {
            counts: [],
            total: vm.subCategorias.length, // length of data
            getData: function ($defer, params) {
                // use build-in angular filter
                var orderedData = params.sorting() ? $filter('orderBy')(vm.subCategorias, params.orderBy()) : vm.subCategorias;
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });

        $scope.$watch('vm.subCategorias', function () {
            $scope.tableParams.reload();
        });

        //Cadastrar
        $scope.form = {
            submit: function (form, sc) {
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
                    // Cadastra a subcategoria
                    sc.CategoriaFinanceiraId = categoria.CategoriaFinanceiraId;                    
                    subCategoriaData.add(sc).success(function (subCategoria) {
                        //Limpa o formulario
                        form.$setPristine(true);
                        toaster.pop('success', '', 'SubCategoria Cadastrada com Sucesso!');
                        $scope.sc = {};
                        subCategoriaData.getByCategoria(categoria.CategoriaFinanceiraId).then(function (result) {
                            vm.subCategorias = result.data;
                        })
                        $scope.createSubCategoria = false;
                    }).error(function (error) {
                        var errors = [];
                        for (var key in error.ModelState) {
                            for (var i = 0; i < error.ModelState[key].length; i++) {
                                errors.push(error.ModelState[key][i]);
                                toaster.pop('error', '', error.ModelState[key][i]);
                            }
                        }
                        vm.errors = errors;
                    });
                }

            }
        };

        //Edit
        $scope.formEdit = {
            submit: function (formEdit, sc) {
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
                    // Cadastra a subcategoria
                    sc.CategoriaFinanceiraId = categoria.CategoriaFinanceiraId;
                    subCategoriaData.edit(sc).success(function (subCategoria) {
                        toaster.pop('success', '', 'SubCategoria Salva com Sucesso!');
                        $scope.editId = -1;
                    }).error(function (error) {
                        var errors = [];
                        for (var key in error.ModelState) {
                            for (var i = 0; i < error.ModelState[key].length; i++) {
                                errors.push(error.ModelState[key][i]);
                                toaster.pop('error', '', error.ModelState[key][i]);
                            }
                        }
                        vm.errors = errors;
                    });
                }

            }
        };

        $scope.delete = function (id) {
            var modalOptions = {
                closeButtonText: 'Cancelar',
                actionButtonText: 'Excluir',
                headerText: 'Excluir ?',
                bodyText: 'Tem certeza que deseja exlcuir esta SubCategoria?'
            };

            modalService.showModal({}, modalOptions).then(function () {
                subCategoriaData.remove(id).success(function () {
                    toaster.pop('success', '', 'SubCategoria Excluída com Sucesso!');
                    $.each(vm.subCategorias, function (i) {
                        if (vm.subCategorias[i].SubCategoriaFinanceiraId === id) {
                            vm.subCategorias.splice(i, 1);
                            return false;
                        }
                    });
                    $scope.tableParams.reload();
                }).error(function (error) {
                    toaster.pop("error", error.Message, "");
                });
            });
        }
        
    }

}());