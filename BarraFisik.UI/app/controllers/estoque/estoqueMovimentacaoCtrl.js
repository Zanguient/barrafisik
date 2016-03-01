(function () {
    "use strict";

    app.controller('estoqueMovimentacaoCtrl', estoqueMovimentacaoCtrl);

    function estoqueMovimentacaoCtrl($scope, estoqueId, $modalInstance, ngTableParams, movimentacaoData, $filter) {
        var vm = this;
        vm.movimentacao = [];

        activate();

        function activate() {
            movimentacaoData.getByEstoque(estoqueId).then(function (result) {
                vm.movimentacao = result.data;
            });
        }

        $scope.tableParams = new ngTableParams({
            page: 1,
            count: 20,
            sorting: { DataMovimento: 'asc' }
        }, {
            counts: [10,20,30],
            total: vm.movimentacao.length, // length of data
            getData: function ($defer, params) {
                var orderedData = params.sorting() ? $filter('orderBy')(vm.movimentacao, params.orderBy()) : vm.movimentacao;
                orderedData = $filter('filter')(orderedData, params.filter());

                params.total(orderedData.length);
                $scope.total = orderedData.length;

                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });

        $scope.$watch('vm.movimentacao', function () {
            $scope.tableParams.reload();
        });

        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        }
    }
}());