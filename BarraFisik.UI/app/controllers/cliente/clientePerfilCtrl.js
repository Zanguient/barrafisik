(function () {
    'use strict';

    app.controller('clientePerfilCtrl', clientePerfilCtrl);

    function clientePerfilCtrl($scope, clienteData, horarioData, SweetAlert, $stateParams, $state, receitasData, vendasData, tipoPagamentoData, toaster) {
        var vm = this;
        vm.TotalPago = 0;
        vm.avaliacoesFisicas = [];
        vm.vendas = [];
        $scope.baixa = {};


        $scope.parseFloat = parseFloat;

        $scope.editId = -1;

        $scope.setEditId = function (pid) {
            $scope.editId = pid;
        };

        activate();

        function activate() {
            clienteData.getById($stateParams.id).then(function (cliente) {
                vm.cliente = cliente.data;
            });

            receitasData.getMensalidades($stateParams.id).then(function (mensalidades) {
                vm.mensalidades = mensalidades.data;
            });

            receitasData.getAvaliacaoCliente($stateParams.id).then(function(avaliacoes) {
                vm.avaliacoesFisicas = avaliacoes.data;
            });

            vendasData.getVendasByCliente($stateParams.id).then(function(vendas) {
                vm.vendas = vendas.data;
            });

            //List Tipos de Pagamento
            tipoPagamentoData.getTipos().then(function (tipos) {
                $scope.tiposPagamento = tipos.data;
            });
            

            //clienteData.getClientePerfil($stateParams.id).then(function (cliente) {
            //    vm.cliente = cliente.data;

            //    //Mensalidades
            //    if (vm.cliente.Mensalidades.lenght > 0) {
            //        angular.forEach(vm.cliente.Mensalidades, function (value, key) {
            //            vm.TotalPago = vm.TotalPago + value.ValorPago;
            //        });
            //    }

            //    receitasAvaliacaoFisicaData.getByCliente($stateParams.id).then(function (avaliacoes) {
            //        vm.avaliacoesFisicas = avaliacoes.data;
            //    })
            //});
        }

        $scope.clearBaixa = function() {
            $scope.baixa = {};
        }

        $scope.baixaVenda = function (venda) {
            venda.DataPagamento = $scope.baixa.DataPagamento;
            venda.TipoPagamentoId = $scope.baixa.TipoPagamentoId;
            vendasData.edit(venda).then(function() {
                toaster.pop('success', 'Venda baixada com sucesso!', '');
                $scope.baixa = {};
                $scope.editId = -1;
                activate();
            });            
        }


    }
}());