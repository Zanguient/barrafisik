(function () {
    'use strict';

    app.controller('fornecedoresCtrl', fornecedoresCtrl);

    function fornecedoresCtrl($scope, ngTableParams, fornecedoresData, SweetAlert, $filter, $state, $stateParams, $window, $modal) {
        var vm = this;
        vm.fornecedores = [];
        $scope.exibirTodos = false;
        vm.isAtivos = true;

        activate();

        function activate() {
            $scope.$emit('LOAD');

            fornecedoresData.getAllAtivos().then(function (result) {
                vm.fornecedores = result.data;
                $scope.$emit('UNLOAD');
            });
        }

        $scope.tableParams = new ngTableParams({
            page: 1, // show first page
            count: 40, // count per page
            sorting: {
                Descricao: 'asc' // initial sorting
            }
        }, {
            counts: [],
            total: vm.fornecedores.length, // length of data
            getData: function ($defer, params) {
                var orderedData = params.sorting() ? $filter('orderBy')(vm.fornecedores, params.orderBy()) : vm.fornecedores;
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));

                $scope.total = orderedData.length;
            }
        });

        $scope.$watch('vm.fornecedores', function () {
            $scope.tableParams.reload();
        });

        //Exibir / Ocultar todos
        $scope.exibirTodos = function () {
            vm.isAtivos = !vm.isAtivos;
            if (vm.isAtivos) {
                fornecedoresData.getAllAtivos().then(function (result) {
                    vm.fornecedores = result.data;
                });
            } else {
                fornecedoresData.getAll().then(function (result) {
                    vm.fornecedores = result.data;
                });
            }
        }

        //Cadastrar
        $scope.openCadastrar = function () {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/fornecedores/create.html',
                size: 'lg',
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/fornecedores/fornecedoresCreateCtrl.js', 'ui.mask']);
                        }
                    ]
                },
                controller: 'fornecedoresCreateCtrl as vm'
            });
            vm.modalInstance.result.then(function (data) {
                SweetAlert.swal("Sucesso!", "Fornecedor cadastrado com sucesso!", "success");
                if (vm.isAtivos) {
                    fornecedoresData.getAllAtivos().then(function (result) {
                        vm.fornecedores = result.data;
                    });
                } else {
                    fornecedoresData.getAll().then(function (result) {
                        vm.fornecedores = result.data;
                    });
                }
            }, function () {
                console.log('Cancelled');
            });
        }

        //Editar
        $scope.openEditar = function (fornecedor) {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/fornecedores/edit.html',
                size: 'lg',
                resolve: {
                    fornecedor: function () {
                        return fornecedor;
                    },
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/fornecedores/fornecedoresEditCtrl.js', 'ui.mask']);
                        }
                    ]
                },
                controller: 'fornecedoresEditCtrl as vm'
            });
            vm.modalInstance.result.then(function (data) {
                SweetAlert.swal("Sucesso!", "Fornecedor salvo com sucesso!", "success");
                if (vm.isAtivos) {
                    fornecedoresData.getAllAtivos().then(function (result) {
                        vm.fornecedores = result.data;
                    });
                } else {
                    fornecedoresData.getAll().then(function (result) {
                        vm.fornecedores = result.data;
                    });
                }
            }, function () {
                console.log('Cancelled');
            });
        }

        //Ficha
        $scope.openFicha = function (fornecedor) {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/fornecedores/ficha.html',
                size: 'lg',
                resolve: {
                    fornecedor: function () {
                        return fornecedor;
                    },
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/fornecedores/fornecedoresFichaCtrl.js']);
                        }
                    ]
                },
                controller: 'fornecedoresFichaCtrl as vm'
            });
            vm.modalInstance.result.then(function () {

            }, function () {
                console.log('Cancelled');
            });
        }

        //Excluir
        vm.delete = function (id) {
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
                    fornecedoresData.remove(id).success(function () {
                        SweetAlert.swal("Excluído!", "Dados apgados com sucesso!", "success");
                        $.each(vm.fornecedores, function (i) {
                            if (vm.fornecedores[i].FornecedorId === id) {
                                vm.fornecedores.splice(i, 1);
                                return false;
                            }
                        });
                        $scope.tableParams.reload();
                    }).error(function (error) {
                        SweetAlert.swal("Erro!", error.Message, "error");
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
