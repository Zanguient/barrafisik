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

        //Campos para impressao
        vm.printTipo = true;
        vm.printCategoria = true;
        vm.printNome = true;
        vm.printData = true;
        vm.printObservacao = true;
        vm.printValor = true;

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
            $scope.totalReceitas = 0;
            $scope.totalDespesas = 0;
            $scope.search = true;
            $scope.$emit('LOAD');
            relatorioFinanceiroData.getRelatorio(filter).then(function(result) {
                vm.relatorioFinanceiro = result.data;

                angular.forEach(result.data, function (value, key) {
                    if (value.Tipo === "Receitas") {
                        $scope.totalReceitas = $scope.totalReceitas + value.Valor;
                    } else {
                        $scope.totalDespesas = $scope.totalDespesas + value.Valor;
                    }
                        
                });
                $scope.total = result.data.length;
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
            vm.printTipo = true;
            vm.printCategoria = true;
            vm.printNome = true;
            vm.printData = true;
            vm.printObservacao = true;
            vm.printValor = true;
        };

    }
})();
