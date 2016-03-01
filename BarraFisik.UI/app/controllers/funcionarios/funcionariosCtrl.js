(function () {
    'use strict';

    app.controller('funcionariosCtrl', funcionariosCtrl);

    function funcionariosCtrl($scope, ngTableParams, funcionariosData, SweetAlert, $filter, $state, $stateParams, $window, $modal) {
        var vm = this;
        vm.funcionarios = [];
        $scope.exibirTodos = false;
        vm.isAtivos = true;

        activate();

        function activate() {
            $scope.$emit('LOAD');

            funcionariosData.getAllAtivos().then(function (result) {
                vm.funcionarios = result.data;
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
            total: vm.funcionarios.length, // length of data
            getData: function ($defer, params) {
                var orderedData = params.sorting() ? $filter('orderBy')(vm.funcionarios, params.orderBy()) : vm.funcionarios;
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));

                $scope.total = orderedData.length;
            }
        });

        $scope.$watch('vm.funcionarios', function () {
            $scope.tableParams.reload();
        });

        //Exibir / Ocultar todos
        $scope.exibirTodos = function () {
            vm.isAtivos = !vm.isAtivos;
            if (vm.isAtivos) {
                funcionariosData.getAllAtivos().then(function (result) {
                    vm.funcionarios = result.data;
                });
            } else {
                funcionariosData.getAll().then(function (result) {
                    vm.funcionarios = result.data;
                });
            }
        }

        //Cadastrar
        $scope.openCadastrar = function () {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/funcionarios/create.html',
                size: 'lg',
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/funcionarios/funcionariosCreateCtrl.js', 'ui.mask']);
                        }
                    ]
                },
                controller: 'funcionariosCreateCtrl as vm'
            });
            vm.modalInstance.result.then(function (data) {
                SweetAlert.swal("Sucesso!", "Funcionário cadastrado com sucesso!", "success");
                if (vm.isAtivos) {
                    funcionariosData.getAllAtivos().then(function (result) {
                        vm.funcionarios = result.data;
                    });
                } else {
                    funcionariosData.getAll().then(function (result) {
                        vm.funcionarios = result.data;
                    });
                }
            }, function () {
                console.log('Cancelled');
            });
        }

        //Editar
        $scope.openEditar = function (funcionario) {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/funcionarios/edit.html',
                size: 'lg',
                resolve: {
                    funcionario: function () {
                        return funcionario;
                    },
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/funcionarios/funcionariosEditCtrl.js', 'ui.mask']);
                        }
                    ]
                },
                controller: 'funcionariosEditCtrl as vm'
            });
            vm.modalInstance.result.then(function (data) {
                SweetAlert.swal("Sucesso!", "Funcionário cadastrado com sucesso!", "success");
                if (vm.isAtivos) {
                    funcionariosData.getAllAtivos().then(function (result) {
                        vm.funcionarios = result.data;
                    });
                } else {
                    funcionariosData.getAll().then(function (result) {
                        vm.funcionarios = result.data;
                    });
                }
            }, function () {
                console.log('Cancelled');
            });
        }

        //Editar
        $scope.openFicha = function (funcionario) {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/funcionarios/ficha.html',
                size: 'lg',
                resolve: {
                    funcionario: function () {
                        return funcionario;
                    },
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/funcionarios/funcionariosFichaCtrl.js']);
                        }
                    ]
                },
                controller: 'funcionariosFichaCtrl as vm'
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
                    funcionariosData.remove(id).success(function () {
                        SweetAlert.swal("Excluído!", "Dados apgados com sucesso!", "success");
                        $.each(vm.funcionarios, function (i) {
                            if (vm.funcionarios[i].FuncionarioId === id) {
                                vm.funcionarios.splice(i, 1);
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
