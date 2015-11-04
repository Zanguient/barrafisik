(function () {
    'use strict';

    app.controller('clientePerfilCtrl', clientePerfilCtrl);

    function clientePerfilCtrl($scope, clienteData, horarioData, SweetAlert, $stateParams, $state, receitasAvaliacaoFisicaData) {
        var vm = this;
        vm.TotalPago = 0;
        vm.avaliacoesFisicas = [];

        $scope.parseFloat = parseFloat;

        activate();

        function activate() {
            clienteData.getClientePerfil($stateParams.id).then(function (cliente) {
                vm.cliente = cliente.data;

                if (vm.cliente.Mensalidades.lenght > 0) {
                    angular.forEach(vm.cliente.Mensalidades, function (value, key) {
                        vm.TotalPago = vm.TotalPago + value.ValorPago;
                    });
                }
                


                receitasAvaliacaoFisicaData.getByCliente($stateParams.id).then(function (avaliacoes) {
                    vm.avaliacoesFisicas = avaliacoes.data;
                })
            });
        }


    }
}());