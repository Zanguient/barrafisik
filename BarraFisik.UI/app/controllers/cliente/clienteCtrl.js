(function () {
    "use strict";

    app.controller('clienteCtrl', clienteCtrl);

    function clienteCtrl($scope, ngTableParams, clienteData, SweetAlert, $filter, $state, $stateParams, $window, $modal) {
        var vm = this;
        vm.clientes = [];
       
        activate();

        function activate() {
            clienteData.getClientes().then(function (result) {               
                vm.clientes = result.data;
                loadTable(vm.clientes);
            });
        }

        function loadTable(data) {
            $scope.tableParams = new ngTableParams({
                page: 1, // show first page
                count: 5, // count per page
                sorting: {
                    Nome: 'asc' // initial sorting
                }
            }, {
                total: data.length, // length of data
                getData: function ($defer, params) {
                    // use build-in angular filter
                    var orderedData = params.sorting() ? $filter('orderBy')(data, params.orderBy()) : data;
                    $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                }
            });

            $scope.$watch('vm.clientes', function () {
                $scope.tableParams.reload();
            });
        }

        $scope.Ativar = function (id) {
            clienteData.ativarCliente(id).then(function () {
                SweetAlert.swal("Ativado!", "Cliente foi ativado com sucesso!", "success");
                $state.go($state.current, {}, { reload: true }); //second parameter is for $stateParams
            });
        }

        $scope.Desativar = function (id) {
            clienteData.desativarCliente(id).then(function () {
                SweetAlert.swal("Desativado!", "Cliente Desativado com Sucesso!", "success");
                $state.go($state.current, {}, { reload: true });
            });
        }

        $scope.refresh = function() {
            $window.location.reload();
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
                $state.go($state.current, {}, { reload: true });
            });
        }

        //Mensalidades
        $scope.createMensalidade = function (id) {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/mensalidades/create.html',
                size: 'lg',
                resolve: {
                    ClienteId: function () {
                        return id;
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
                $state.go($state.current, {}, { reload: true });
            }, function () {
                console.log('Cancelled');
            });
        }

    };
})();