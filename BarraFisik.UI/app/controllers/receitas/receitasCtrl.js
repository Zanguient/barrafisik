(function () {
    "use strict";

    app.controller('receitasCtrl', receitasCtrl);

    function receitasCtrl($scope, ngTableParams, receitasData, tipoPagamentoData, funcionariosData, clienteData, categoriaFinanceiraData, SweetAlert, $filter, $state, $modal) {
        var vm = this;
        vm.receitas = [];
        vm.categorias = [];
        $scope.categorias = [{id: "", title: ""}];
        $scope.createDespesa = false;        

        var _selected;

        $scope.selected = undefined;
        $scope.states = ['Alabama', 'Alaska', 'Arizona', 'Arkansas', 'California', 'Colorado', 'Connecticut', 'Delaware', 'Florida', 'Georgia', 'Hawaii', 'Idaho', 'Illinois', 'Indiana', 'Iowa', 'Kansas', 'Kentucky', 'Louisiana', 'Maine', 'Maryland', 'Massachusetts', 'Michigan', 'Minnesota', 'Mississippi', 'Missouri', 'Montana', 'Nebraska', 'Nevada', 'New Hampshire', 'New Jersey', 'New Mexico', 'New York', 'North Dakota', 'North Carolina', 'Ohio', 'Oklahoma', 'Oregon', 'Pennsylvania', 'Rhode Island', 'South Carolina', 'South Dakota', 'Tennessee', 'Texas', 'Utah', 'Vermont', 'Virginia', 'Washington', 'West Virginia', 'Wisconsin', 'Wyoming'];

        $scope.ngModelOptionsSelected = function (value) {
            if (arguments.length) {
                _selected = value;
            } else {
                return _selected;
            }
        };

        $scope.modelOptions = {
            debounce: {
                default: 500,
                blur: 250
            },
            getterSetter: true
        };


        activate();

        function activate() {
            $scope.$emit('LOAD');
            receitasData.getReceitas().then(function (result) {
                vm.receitas = result.data;
            });

            categoriaFinanceiraData.getCategoriaByTipo("Receitas").then(function (cat) {
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
            atualizaValores();
        });

        //Search
        $scope.pesquisar = function (search) {
            receitasData.searchReceitas(search).then(function (result) {
                vm.receitas = result.data;
            });
        };

        //Cadastrar
        $scope.openCadastrar = function () {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/receitas/create.html',
                size: 'lg',
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/receitas/receitasCreateCtrl.js', 'ui.mask', 'app/factory/subCategoriaData.js']);
                        }
                    ]
                },
                controller: 'receitasCreateCtrl as vm'
            });
            vm.modalInstance.result.then(function (data) {
                SweetAlert.swal("Sucesso!", "Receita cadastrada com sucesso!", "success");
                receitasData.getReceitas().then(function(result) {
                    vm.receitas = result.data;
                });
            }, function () {
                console.log('Cancelled');
            });
        }

        //Editar
        $scope.openEditar = function (receita) {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/receitas/edit.html',
                size: 'lg',
                resolve: {
                    receita: function () {
                        return receita;
                    },
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/receitas/receitasEditCtrl.js', 'ui.mask', 'app/factory/subCategoriaData.js']);
                        }
                    ]
                },
                controller: 'receitasEditCtrl as vm'
            });
            vm.modalInstance.result.then(function (data) {
                SweetAlert.swal("Sucesso!", "Receita salva com sucesso!", "success");
                receitasData.getReceitas().then(function (result) {
                    vm.receitas = result.data;
                });
                atualizaValores();
            }, function () {
                console.log('Cancelled');
            });
        }

        //Ficha
        $scope.openFicha = function (receita) {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/receitas/ficha.html',
                size: 'lg',
                resolve: {
                    receita: function () {
                        return receita;
                    },
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/receitas/receitasFichaCtrl.js', 'app/factory/funcionariosData.js', 'app/factory/clienteData.js', 'app/factory/subCategoriaData.js']);
                        }
                    ]
                },
                controller: 'receitasFichaCtrl as vm'
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
                    receitasData.deleteReceita(id).then(function () {
                        SweetAlert.swal("Excluído!", "Registro excluído com sucesso!", "success");
                        $.each(vm.receitas, function (i) {
                            if (vm.receitas[i].ReceitasId === id) {
                                vm.receitas.splice(i, 1);
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

        function atualizaValores() {
            $scope.totalQuitado = 0;
            $scope.totalPendente = 0;
            $scope.totalVencido = 0;
            angular.forEach(vm.receitas, function (value, key) {
                if (value.Situacao === "Quitado") {
                    $scope.totalQuitado = $scope.totalQuitado + value.ValorTotal;
                } else if (value.Situacao === "Pendente") {
                    $scope.totalPendente = $scope.totalPendente + value.ValorTotal;
                } else $scope.totalVencido = $scope.totalVencido + value.ValorTotal;
            });
            $scope.tableParams.reload();
        }

    }
})();