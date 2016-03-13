(function () {
    "use strict";

    app.controller('vendasCtrl', vendasCtrl);

    function vendasCtrl($scope, ngTableParams, vendasData, tipoPagamentoData, clienteData, SweetAlert, $filter, $state, $modal, $timeout) {
        var vm = this;
        vm.vendas = [];
        $scope.search = {};
        $scope.clientes = [];
        $scope.isCliente = true;

        //List Clientes
        clienteData.getClientes().then(function (clientesList) {
            $scope.clientes = clientesList.data;
        });

        activate();

        function activate() {
            $scope.$emit('LOAD');
            vendasData.getAll().then(function (result) {
                vm.vendas = result.data;                
            });

            $scope.$emit('UNLOAD');
        }

        $scope.tableParams = new ngTableParams({
            page: 1,            // show first page
            count: 30,          // count per page
            sorting: {
                DataVenda: 'asc'     // initial sorting
            },
        }, {
            counts: [],
            total: vm.vendas.length, // length of data
            getData: function ($defer, params) {
                var filteredData = params.filter() ?
                         $filter('filter')(vm.vendas, $scope.searchText) :
                         vm.vendas;
                var orderedData = params.sorting() ?
                        $filter('orderBy')(filteredData, params.orderBy()) :
                        vm.vendas;
                $scope.total = filteredData.length;
                $scope.filtered = filteredData;
                params.total(orderedData.length); 
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });

        $scope.$watch('vm.vendas', function () {
            atualizaValores(vm.vendas);
        });

        $scope.$watch("searchText", function () {
            atualizaValores($scope.filtered);
        });

        //List Tipos de Pagamento
        tipoPagamentoData.getTipos().then(function (tipos) {
            $scope.tiposPagamento = tipos.data;
        });

        //Search
        $scope.pesquisar = function (search) {            
            vendasData.searchVendas(search).then(function (result) {
                vm.vendas = result.data;
            });
        };

        //Cadastrar
        $scope.openCadastrar = function () {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/vendas/create.html',
                size: 'lg',
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/vendas/vendasCreateCtrl.js', 'ui.mask', 'app/factory/clienteData.js', 'app/factory/estoqueData.js', 'app/factory/armazemData.js', 'app/factory/tipoPagamentoData.js', 'app/factory/receitasData.js']);
                        }
                    ]
                },
                controller: 'vendasCreateCtrl as vm'
            });
            vm.modalInstance.result.then(function (data) {
                SweetAlert.swal("Sucesso!", "Venda cadastrada com sucesso!", "success");
                vendasData.searchVendas($scope.search).then(function (result) {
                    vm.vendas = result.data;
                });                
            }, function () {
                console.log('Cancelled');
            });
        }

        //Produtos
        $scope.openProdutos = function (vendaId) {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/vendas/produtos.html',
                size: 'lg',
                resolve: {
                    vendaId: function () { return vendaId; },
                    total: function () { return $scope.totalVendas },
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/vendas/vendasProdutosCtrl.js', 'app/factory/fornecedoresData.js', 'app/factory/armazemData.js', 'app/factory/estoqueData.js']);
                        }
                    ]
                },
                controller: 'vendasProdutosCtrl as vm'
            });
            vm.modalInstance.result.then(function (data) {                
            }, function () {
                vendasData.searchVendas($scope.search).then(function (result) {
                    vm.vendas = result.data;                   
                });                
            });
        }

        $scope.formEdit = {

            submit: function (form, venda) {
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
                    if (venda.Cliente != null) {
                        venda.ClienteId = venda.Cliente.ClienteId;
                        venda.Cliente = null;
                    } else {
                        venda.ClienteId = null;
                    }

                    vendasData.edit(venda).success(function () {
                        SweetAlert.swal("Atualizado!", "Dados salvos com sucesso!", "success");
                        $scope.editId = -1;
                        vendasData.searchVendas($scope.search).then(function (result) {
                            vm.vendas = result.data;
                        });
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
                    vendasData.remove(id).then(function () {
                        SweetAlert.swal("Excluído!", "Dados apgados com sucesso!", "success");
                        $.each(vm.vendas, function (i) {
                            if (vm.vendas[i].VendaId === id) {
                                vm.vendas.splice(i, 1);
                                return false;
                            }
                        });
                        atualizaValores(vm.vendas);
                    });
                }
            });
        }

        function atualizaValores(vendas) {
            $scope.totalVendas = 0;
            angular.forEach(vendas, function (value, key) {
                $scope.totalVendas = $scope.totalVendas + value.ValorTotal;
            });
            $scope.tableParams.reload();
        }

        $scope.editId = -1;

        $scope.setEditId = function (pid) {
            $scope.editId = pid;
        };

        $scope.opened = [];
        $scope.open = function ($event, index) {
            $event.preventDefault();
            $event.stopPropagation();
            $timeout(function () {
                $scope.opened[index] = true;
            });
        };

        $scope.dateOptions = {
            'year-format': "'yyyy'",
            'starting-day': 1
        };
    }
})();