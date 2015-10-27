(function () {
    'use strict';

    app.controller('relatorioFinanceiroCtrl', relatorioFinanceiroCtrl);

    function relatorioFinanceiroCtrl($scope, ngTableParams, relatorioFinanceiroData, categoriaFinanceiraData, $filter) {
        var vm = this;
        $scope.search = false;

        $scope.filter = {
            Tipo: null,
            Categoria: null,
            DataInicio: null,
            DataFim: null
        }

        getCategorias('Todos');

        $scope.filterCategorias = function (tipo) {
            getCategorias(tipo);
        }

        function getCategorias(tipo) {
            if (tipo === 'Todos') {
                categoriaFinanceiraData.getCategorias().then(function(categorias) {
                    vm.categorias = categorias.data;
                });
            } else {               
                categoriaFinanceiraData.getCategoriaByTipo(tipo).then(function(categorias) {
                    vm.categorias = categorias.data;
                });
            }            
        }

        vm.pesquisar = function (filter) {
            $scope.totalValor = 0;
            $scope.search = true;
            $scope.$emit('LOAD');
            relatorioFinanceiroData.getRelatorio(filter).then(function(result) {
                vm.relatorioFinanceiro = result.data;

                angular.forEach(result.data, function (value, key) {
                    $scope.totalValor = $scope.totalValor + value.Valor;
                });

                $scope.total = result.data.length;

                //$scope.tableParams = new ngTableParams({
                //    page: 1, // show first page
                //    count: result.data.length, // count per page
                //    sorting: {
                //        Data: 'asc'
                //    }
                //}, {
                //    counts: [],
                //    total: result.data.length, // length of data
                //    getData: function ($defer, params) {
                //        // use build-in angular filter
                //        var orderedData = params.sorting() ? $filter('orderBy')(result.data, params.orderBy()) : result.data;
                //        $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                        
                //        $scope.total = orderedData.length;
                //    }
                //});

                //$scope.$watch('result.data', function () {
                //    $scope.tableParams.reload();
                //});
            });
            $scope.$emit('UNLOAD');
        };

        $scope.sortField = undefined;
        $scope.reverse = false;

        $scope.isSortUp = function (fieldName) {
            return $scope.sortField === fieldName && !$scope.reverse; 
        };
        
        $scope.isSortDown = function (fieldName) {
            return $scope.sortField === fieldName && $scope.reverse;
        };
        
        //order data
        $scope.sort = function (fieldName) {
            if ($scope.sortField === fieldName) {
                $scope.reverse = !$scope.reverse;  
            } else {  
                $scope.sortField = fieldName;
                $scope.reverse = false;
            };
        }


        
        vm.clear = function() {
            $scope.filter = {
                Tipo: null,
                Categoria: null,
                DataInicio: null,
                DataFim: null
            }
        };

    }
})();
