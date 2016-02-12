(function () {
    "use strict";

    app.controller('clienteCtrl', clienteCtrl);

    function clienteCtrl($scope, ngTableParams, clienteData, SweetAlert, $filter, $state, $stateParams, $window, $modal, toaster) {
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
            clienteData.getClientes(0, 10).then(function (result) {
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
            count: 10, // count per page
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
            { show: true, title: 'Dias' },
        ];

        $scope.refresh = function () {
            $window.location.reload();
        }

        $scope.atualizaValores = function () {
            SweetAlert.swal({
                title: "Confirmar Atualização?",
                text: "Os valores das mensalidades dos clientes serão atualizadas, Deseja Continuar?",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Confirmar",
                cancelButtonText: "Cancelar",
                closeOnConfirm: false,
                closeOnCancel: false
            }, function (isConfirm) {
                if (isConfirm) {
                    SweetAlert.swal("Sucesso", "Os valores foram atualizados!", "success");
                    clienteData.atualizaValores().then(function () {                       
                        $scope.tableParams.reload();
                    });
                } else {
                    SweetAlert.swal({
                        title: "Cancelado",
                        text: "Operação de atualização cancelada",
                        type: "error",
                        confirmButtonColor: "#007AFF"
                    });
                }
            });
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
        $scope.openMensalidades = function (cliente) {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/mensalidades/mensalidades.html',
                size: 'lg',
                resolve: {
                    Cliente: function () {
                        return cliente;
                    },
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/mensalidades/mensalidadesCtrl.js', 'app/factory/mensalidadesData.js', 'app/factory/tipoPagamentoData.js', 'app/factory/receitasData.js']);
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
        
        //Cadastra Mensalidade
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
                            return $ocLazyLoad.load(['app/controllers/mensalidades/createMensalidadesCtrl.js', 'app/factory/mensalidadesData.js', 'app/factory/tipoPagamentoData.js', 'app/factory/receitasData.js']);
                        }
                    ]
                },
                controller: 'createMensalidadesCtrl as vm'
            });
            vm.modalInstance.result.then(function (mensalidade) {                
                toaster.pop('success', '', 'Mensalidade Salva com Sucesso!');
                if(mensalidade.DataPagamento != null)
                    window.open("http://localhost:49000/app/views/mensalidades/comprovante.html?mes=" + mensalidade.MesReferencia +
                        "&ano=" + mensalidade.AnoReferencia + "&valor=" + mensalidade.ValorTotal +
                        "&cliente=" + cliente.Nome, "Recibo - Mensalidade", "height=250,width=370");
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
                size: 'lg',
                resolve: {
                    Cliente: function () {
                        return cliente;
                    },
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/receitasAvaliacaoFisica/createReceitasAvaliacaoFisicaCtrl.js', 'app/factory/receitasAvaliacaoFisicaData.js', 'app/factory/tipoPagamentoData.js', 'app/factory/receitasData.js']);
                        }
                    ]
                },
                controller: 'createReceitasAvaliacaoFisicaCtrl as vm'
            });
            vm.modalInstance.result.then(function (data) {
                toaster.pop('success', '', 'Avaliação Física Salva com Sucesso!');
                if (data.DataPagamento != null)
                    window.open("http://localhost:49000/app/views/receitasAvaliacaoFisica/comprovante.html?valor=" + data.Valor + "&cliente=" + cliente.Nome, "Recibo - Avaliação Física", "height=250,width=370");
            }, function () {
                console.log('Cancelled');
            });
        }

        //Avaliacao Fisica
        $scope.openReceitasAvaliacaoFisica = function (cliente) {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/receitasAvaliacaoFisica/listaPorCliente.html',
                size: 'lg',
                resolve: {
                    Cliente: function () {
                        return cliente;
                    },
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['app/controllers/receitasAvaliacaoFisica/receitasAvaliacaoFisicaCtrl.js', 'app/factory/receitasAvaliacaoFisicaData.js', 'app/factory/tipoPagamentoData.js', 'app/factory/receitasData.js']);
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