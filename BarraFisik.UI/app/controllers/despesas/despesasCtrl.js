(function () {
    "use strict";

    app.controller('despesasCtrl', despesasCtrl);

    function despesasCtrl($scope, ngTableParams, despesasData, tipoPagamentoData, funcionariosData, fornecedoresData, categoriaFinanceiraData, SweetAlert, $filter, $state, $modal) {
        var vm = this;
        vm.despesas = [];
        vm.categorias = [];
        $scope.search = {};

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
            counts: [10,20,30,40,50],
            total: vm.despesas.length, // length of data
            getData: function ($defer, params) {
                var filteredData = params.filter() ?
                        $filter('filter')(vm.despesas, $scope.searchText) :
                        vm.despesas;
                var orderedData = params.sorting() ?
                        $filter('orderBy')(filteredData, params.orderBy()) :
                        vm.despesas;
                $scope.total = filteredData.length;
                $scope.filtered = filteredData;
                params.total(orderedData.length); // set total for recalc pagination
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });

        $scope.$watch('vm.despesas', function() {
            atualizaValores(vm.despesas);
        });

        $scope.$watch("searchText", function () {
            atualizaValores($scope.filtered);
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
                            return $ocLazyLoad.load(['app/controllers/despesas/despesasCreateCtrl.js', 'ui.mask', 'app/factory/subCategoriaData.js']);
                        }
                    ]
                },
                controller: 'despesasCreateCtrl as vm'
            });
            vm.modalInstance.result.then(function (result) {
                SweetAlert.swal("Sucesso!", "Despesa cadastrada com sucesso!", "success");
                despesasData.searchDespesas($scope.search).then(function (result) {
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
                            return $ocLazyLoad.load(['app/controllers/despesas/despesasEditCtrl.js', 'ui.mask', 'app/factory/subCategoriaData.js']);
                        }
                    ]
                },
                controller: 'despesasEditCtrl as vm'
            });
            vm.modalInstance.result.then(function (data) {
                SweetAlert.swal("Sucesso!", "Despesa salva com sucesso!", "success");
                despesasData.searchDespesas($scope.search).then(function (result) {
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
                            return $ocLazyLoad.load(['app/controllers/despesas/despesasFichaCtrl.js', 'app/factory/funcionariosData.js', 'app/factory/fornecedoresData.js', 'app/factory/subCategoriaData.js']);
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
                    despesasData.deleteDespesa(id).then(function () {
                        SweetAlert.swal("Excluído!", "Registro excluído com sucesso!", "success");
                        $.each(vm.despesas, function (i) {
                            if (vm.despesas[i].DespesasId === id) {
                                vm.despesas.splice(i, 1);
                                return false;
                            }
                        });
                        atualizaValores();
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

        function atualizaValores(despesas) {
            $scope.totalQuitado = 0;
            $scope.totalPendente = 0;
            angular.forEach(despesas, function (value, key) {
                if (value.Situacao === "Quitado") {
                    $scope.totalQuitado = $scope.totalQuitado + value.ValorTotal;
                } else if (value.Situacao === "Pendente") {
                    $scope.totalPendente = $scope.totalPendente + value.ValorTotal;
                } 
            });
            $scope.tableParams.reload();
        }
    }
})();