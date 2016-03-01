(function () {
    "use strict";

    app.controller('produtosCtrl', produtosCtrl);

    function produtosCtrl($scope, ngTableParams, produtosCategoriaData, produtosData, SweetAlert, $filter) {
        var vm = this;
        vm.produtos = [];
        $scope.createProdutos = false;

        activate();

        function activate() {
            $scope.$emit('LOAD');

            produtosData.getAll().then(function (result) {
                vm.produtos = result.data;                
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
            total: vm.produtos.length, // length of data
            getData: function ($defer, params) {
                // use build-in angular filter
                var orderedData = params.sorting() ? $filter('orderBy')(vm.produtos, params.orderBy()) : vm.produtos;
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });

        $scope.$watch('vm.produtos', function () {
            $scope.tableParams.reload();
        });

        produtosCategoriaData.getAll().then(function(result) {
            vm.categorias = result.data;
        });


        $scope.save = function (produto) {
            produtosCategoriaData.edit(produto).then(function () {
                SweetAlert.swal("Atualizado!", "Produto salvo com sucesso!", "success");
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
                    produtosData.remove(id).success(function () {
                        SweetAlert.swal("Excluído!", "Dados apgados com sucesso!", "success");
                        $.each(vm.produtos, function (i) {
                            if (vm.produtos[i].ProdutoId === id) {
                                vm.produtos.splice(i, 1);
                                return false;
                            }
                        });
                        $scope.tableParams.reload();
                    }).error(function (error) {
                        SweetAlert.swal("Erro!", error.Message, "error");
                    });
                }                 
            });
        }

        var produtoDefault = {
            CategoriaProdutoId: null,
            Nome: null,
            Descricao: null,
        };

        $scope.produto = angular.copy(produtoDefault);

        $scope.cancelCreate = function (form) {
            $scope.createProdutos = false;
            $scope.produto = angular.copy(produtoDefault);
            form.$setPristine(true);
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
                    produtosData.add(produto).success(function (result) {
                        SweetAlert.swal("Cadastrado!", "Produto cadastrado com sucesso!", "success");

                        //Limpa o formulario
                        form.$setPristine(true);
                        $scope.produto = angular.copy(produtoDefault);                        
                        $scope.createProdutos = false;
                        produtosData.getAll().then(function (result) {
                            vm.produtos = result.data;
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
                    produtosData.edit(produto).success(function () {
                        SweetAlert.swal("Atualizado!", "Produto salvo com sucesso!", "success");
                        produtosData.getAll().then(function (result) {
                            vm.produtos = result.data;
                        });
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