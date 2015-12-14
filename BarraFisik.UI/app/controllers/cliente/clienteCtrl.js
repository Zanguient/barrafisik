(function () {
    "use strict";

    app.controller('clienteCtrl', clienteCtrl);

    function clienteCtrl($scope, ngTableParams, clienteData, SweetAlert, $filter, $state, $stateParams, $window, $modal) {
        var vm = this;
        vm.clientes = [];
        vm.cols = [];

        $scope.status = {
            isopen: false
        };

        $scope.toggleDropdown = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.status.isopen = !$scope.status.isopen;
        };

        activate();

        function activate() {
            $scope.$emit('LOAD');
            clienteData.getClientes().then(function (result) {
                vm.clientes = result.data;
                $scope.$emit('UNLOAD');
            });
        }

        $scope.filter = {
            Nome: undefined,
            Cpf: undefined,
            Segunda: undefined,
            Terca: undefined,
            Quarta: undefined,
            Quinta: undefined,
            Sexta: undefined,
            HSegunda: undefined,
            HTerca: undefined,
            HQuarta: undefined,
            HQuinta: undefined,
            HSexta: undefined,
            Situacao: ''
        };

        $scope.limpar = function () {
            $state.go($state.current, {}, { reload: true });
        };

        $scope.inativos = false;

        $scope.exibirInativos = function () {
            $scope.inativos = !$scope.inativos;
            if ($scope.inativos) {
                clienteData.getClientesAll().then(function (result) {
                    vm.clientes = result.data;
                    $scope.tableParams.reload();
                });
            } else {
                clienteData.getClientes().then(function (result) {
                    vm.clientes = result.data;
                    $scope.tableParams.reload();
                    $scope.filter.Situacao = '';
                });
            }
        }

        $scope.tableParams = new ngTableParams({

            page: 1, // show first page
            count: 30, // count per page
            sorting: {
                Nome: 'asc' // initial sorting
            },
            filter: $scope.filter
        }, {
            total: vm.clientes.length, // length of data
            getData: function ($defer, params) {
                // use build-in angular filter                   
                var orderedData = params.sorting() ? $filter('orderBy')(vm.clientes, params.orderBy()) : vm.clientes;
                orderedData = $filter('filter')(orderedData, params.filter());

                params.total(orderedData.length);
                $scope.total = orderedData.length;

                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }

        });

        $scope.$watch("vm.clientes", function () {
            $scope.tableParams.reload();
        });

        vm.cols = [
            { show: false, title: "Foto" },
            { show: true, title: "Nome" },
            { show: true, title: "Endereço" },
            { show: true, title: "CPF" },
            { show: true, title: "E-mail" },
            { show: true, title: "Telefone" },
            { show: true, title: "Celular" },
            { show: false, title: "Dt. Nascimento" },
            { show: false, title: "Dt. Inscrição" },
            { show: true, title: "Situação" },
            { show: false, title: "Qtd. Dias" },
            { show: false, title: "Valor" },
        ];

        //$scope.Ativar = function (id) {
        //    clienteData.ativarCliente(id).then(function () {
        //        SweetAlert.swal("Ativado!", "Cliente foi ativado com sucesso!", "success");
        //        $state.go($state.current, {}, { reload: true }); //second parameter is for $stateParams
        //    });
        //}

        //$scope.Desativar = function (id) {
        //    clienteData.desativarCliente(id).then(function () {
        //        SweetAlert.swal("Desativado!", "Cliente Desativado com Sucesso!", "success");
        //        $state.go($state.current, {}, { reload: true });
        //    });
        //}

        $scope.refresh = function () {
            $window.location.reload();
        }

        //Horários
        $scope.openHorarios = function (cliente) {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/horario/horariosCliente.html',
                size: 'sm',
                resolve: {
                    cliente: function () {
                        return cliente;
                    },
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/horario/horarioClienteCtrl.js']);
                        }
                    ]
                },
                controller: 'horarioClienteCtrl as vm'
            });
            vm.modalInstance.result.then(function (data) {

            }, function () {
                console.log('Cancelled');
            });
        }

        //Mensalidades
        $scope.openMensalidades = function (id) {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/mensalidades/mensalidades.html',
                size: 'lg',
                resolve: {
                    ClienteId: function () {
                        return id;
                    },
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/mensalidades/mensalidadesCtrl.js', 'app/factory/mensalidadesData.js']);
                        }
                    ]
                },
                controller: 'mensalidadesCtrl as vm'
            });
            vm.modalInstance.result.then(function (data) {
            }, function () {
                console.log('Cancelled');
                if ($scope.inativos) {
                    clienteData.getClientesAll().then(function (result) {
                        vm.clientes = result.data;
                        $scope.tableParams.reload();
                    });
                } else {
                    clienteData.getClientes().then(function (result) {
                        vm.clientes = result.data;
                        $scope.tableParams.reload();
                    });
                }

                //clienteData.getClientes().then(function (result) {
                //    vm.clientes = result.data;
                //});
            });
        }

        //Mensalidades
        $scope.createMensalidade = function (cliente) {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/mensalidades/create.html',
                size: 'lg',
                resolve: {
                    Cliente: function () {
                        return cliente;
                    },
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/mensalidades/createMensalidadesCtrl.js', 'app/factory/mensalidadesData.js']);
                        }
                    ]
                },
                controller: 'createMensalidadesCtrl as vm'
            });
            vm.modalInstance.result.then(function (data) {
                SweetAlert.swal("Sucesso!", "Mensalidade foi cadastrada com sucesso!", "success");
                if ($scope.inativos) {
                    clienteData.getClientesAll().then(function (result) {
                        vm.clientes = result.data;
                        $scope.tableParams.reload();
                    });
                } else {
                    clienteData.getClientes().then(function (result) {
                        vm.clientes = result.data;
                        $scope.tableParams.reload();
                    });
                }
            }, function () {
                console.log('Cancelled');
            });
        }

        //Avaliacao Fisica
        $scope.createAvaliacaoFisica = function (cliente) {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/receitasAvaliacaoFisica/create.html',
                size: '',
                resolve: {
                    Cliente: function () {
                        return cliente;
                    },
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/receitasAvaliacaoFisica/createReceitasAvaliacaoFisicaCtrl.js', 'app/factory/receitasAvaliacaoFisicaData.js']);
                        }
                    ]
                },
                controller: 'createReceitasAvaliacaoFisicaCtrl as vm'
            });
            vm.modalInstance.result.then(function (data) {
                SweetAlert.swal("Sucesso!", "Avaliação Física paga com sucesso!", "success");
                //clienteData.getClientes().then(function (result) {
                //    vm.clientes = result.data;
                //});
            }, function () {
                console.log('Cancelled');
            });
        }

        //Avaliacao Fisica
        $scope.openReceitasAvaliacaoFisica = function (id) {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/receitasAvaliacaoFisica/listaPorCliente.html',
                size: '',
                resolve: {
                    ClienteId: function () {
                        return id;
                    },
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/receitasAvaliacaoFisica/receitasAvaliacaoFisicaCtrl.js', 'app/factory/receitasAvaliacaoFisicaData.js']);
                        }
                    ]
                },
                controller: 'receitasAvaliacaoFisicaCtrl as vm'
            });
            vm.modalInstance.result.then(function (data) {
            }, function () {
                console.log('Cancelled');
                //clienteData.getClientes().then(function (result) {
                //    vm.clientes = result.data;
                //});
            });
        }
    };
})();