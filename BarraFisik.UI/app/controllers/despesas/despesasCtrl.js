(function () {
    "use strict";

    app.controller('despesasCtrl', despesasCtrl);

    function despesasCtrl($scope, ngTableParams, despesasData, tipoPagamentoData, funcionariosData, fornecedoresData, categoriaFinanceiraData, SweetAlert, $filter, $state, $modal) {
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
            });

            $scope.$emit('UNLOAD');
        }

        $scope.tableParams = new ngTableParams({
            page: 1, // show first page
            count: 50, // count per page
            sorting: {
                DataVencimento: 'asc'
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

        $scope.$watch('vm.despesas', function() {
            $scope.totalQuitado = 0;
            $scope.totalPendente = 0;
            $scope.totalVencido = 0;
            angular.forEach(vm.despesas, function (value, key) {               
                if (value.Situacao === "Quitado") {
                    $scope.totalQuitado = $scope.totalQuitado + value.ValorTotal;
                } else if (value.Situacao === "Pendente") {
                    $scope.totalPendente = $scope.totalPendente + value.ValorTotal;
                } else $scope.totalVencido = $scope.totalVencido + value.ValorTotal;
            });
            $scope.tableParams.reload();
        });

        //Search
        $scope.pesquisar = function (search) {
            despesasData.searchDespesas(search).then(function (result) {
                vm.despesas = result.data;
            });
        };

        //Cadastrar
        $scope.openCadastrar = function () {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/despesas/create.html',
                size: 'lg',
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/despesas/despesasCreateCtrl.js', 'ui.mask']);
                        }
                    ]
                },
                controller: 'despesasCreateCtrl as vm'
            });
            vm.modalInstance.result.then(function (data) {
                SweetAlert.swal("Sucesso!", "Fornecedor cadastrado com sucesso!", "success");
                despesasData.getDespesas().then(function(result) {
                    vm.despesas = result.data;
                });
            }, function () {
                console.log('Cancelled');
            });
        }

        //Editar
        $scope.openEditar = function (despesa) {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/despesas/edit.html',
                size: 'lg',
                resolve: {
                    despesa: function () {
                        return despesa;
                    },
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/despesas/despesasEditCtrl.js', 'ui.mask']);
                        }
                    ]
                },
                controller: 'despesasEditCtrl as vm'
            });
            vm.modalInstance.result.then(function (data) {
                SweetAlert.swal("Sucesso!", "Despesa salva com sucesso!", "success");
                despesasData.getDespesas().then(function (result) {
                    vm.despesas = result.data;
                });
            }, function () {
                console.log('Cancelled');
            });
        }

        //Ficha
        $scope.openFicha = function (despesa) {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/despesas/ficha.html',
                size: 'lg',
                resolve: {
                    despesa: function () {
                        return despesa;
                    },
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/despesas/despesasFichaCtrl.js', 'app/factory/funcionariosData.js', 'app/factory/fornecedoresData.js']);
                        }
                    ]
                },
                controller: 'despesasFichaCtrl as vm'
            });
            vm.modalInstance.result.then(function () {

            }, function () {
                console.log('Cancelled');
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