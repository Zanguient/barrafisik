(function () {
    "use strict";

    app.controller('estoqueCtrl', estoqueCtrl);

    function estoqueCtrl($scope, ngTableParams, estoqueData, SweetAlert, $filter, $state, $modal) {
        var vm = this;
        vm.estoques = [];
        $scope.grupo = 'Armazem.Descricao'

        activate();

        function activate() {
            $scope.$emit('LOAD');
            estoqueData.getAll().then(function (result) {
                vm.estoques = result.data;
            });

            $scope.$emit('UNLOAD');
        }

        $scope.tableParams = new ngTableParams({
            page: 1,            // show first page
            count: 30,          // count per page
            sorting: {
                Produto: 'asc'     // initial sorting
            },
        }, {
            counts: [],
            total: vm.estoques.length, // length of data
            getData: function ($defer, params) {
                // use build-in angular filter
                var orderedData = params.sorting() ? $filter('orderBy')(vm.estoques, params.orderBy()) : vm.estoques;
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });

        $scope.$watch('vm.estoques', function () {
            $scope.tableParams.reload();
        });

        //Movimentações Estoque
        $scope.openMovimentacoes = function (id) {    
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/estoque/movimentacoes.html',
                size: 'lg',                
                resolve: {
                    estoqueId: function () { return id; },
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/estoque/estoqueMovimentacaoCtrl.js']);
                        }
                    ]
                },
                controller: 'estoqueMovimentacaoCtrl as vm'
            });
            vm.modalInstance.result.then(function () {
            }, function () {
                
            });
        }


        //Cadastrar
        $scope.openCadastrar = function () {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/estoque/create.html',
                size: 'lg',
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/estoque/estoqueCreateCtrl.js', 'ui.mask', 'app/factory/fornecedoresData.js', 'app/factory/produtosCategoriaData.js', 'app/factory/produtosData.js', 'app/factory/armazemData.js']);
                        }
                    ]
                },
                controller: 'estoqueCreateCtrl as vm'
            });
            vm.modalInstance.result.then(function (data) {
                SweetAlert.swal("Sucesso!", "Estoque cadastrado com sucesso!", "success");
                estoqueData.getAll().then(function (result) {
                    vm.estoques = result.data;
                });
            }, function () {
                console.log('Cancelled');
            });
        }

        //Entrada Estoque
        $scope.openEntradaEstoque = function (estoque) {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/estoque/entrada.html',
                size: 'lg',
                resolve: {
                    estoque: function () { return estoque; },
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/estoque/estoqueEntradaCtrl.js', 'ui.mask', 'app/factory/fornecedoresData.js']);
                        }
                    ]
                },
                controller: 'estoqueEntradaCtrl as vm'
            });
            vm.modalInstance.result.then(function (data) {
                SweetAlert.swal("Sucesso!", "Estoque cadastrado com sucesso!", "success");
                estoqueData.getAll().then(function (result) {
                    vm.estoques = result.data;
                });
            }, function () {
                console.log('Cancelled');
            });
        }

        $scope.formEdit = {

            submit: function (form, estoque) {
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
                    estoqueData.edit(estoque).success(function () {
                        SweetAlert.swal("Atualizado!", "Dados salvos com sucesso!", "success");
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
                    estoqueData.remove(id).then(function () {
                        SweetAlert.swal("Excluído!", "Dados apgados com sucesso!", "success");
                        $.each(vm.estoques, function (i) {
                            if (vm.estoques[i].EstoqueId === id) {
                                vm.estoques.splice(i, 1);
                                return false;
                            }
                        });
                        $scope.tableParams.reload();
                    });
                }
            });
        }

        $scope.editId = -1;

        $scope.setEditId = function (pid) {
            $scope.editId = pid;
        };

    }
})();