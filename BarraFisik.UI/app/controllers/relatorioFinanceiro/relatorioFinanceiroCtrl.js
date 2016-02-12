(function () {
    'use strict';

    app.controller('relatorioFinanceiroCtrl', relatorioFinanceiroCtrl);

    function relatorioFinanceiroCtrl($scope, ngTableParams, relatorioFinanceiroData, categoriaFinanceiraData, $filter, subCategoriaData) {
        var vm = this;
        $scope.search = false;

        var filterDefault = {
            Tipo: undefined,
            Situacao: "",
            CategoriaId: null,
            SubCategoriaId: null,
            EmissaoInicio: null,
            EmissaoFim: null,
            VencimentoInicio: null,
            VencimentoFim: null,
            PagamentoInicio: null,
            PagamentoFim: null
        }

        $scope.filter = angular.copy(filterDefault);

        //Campos para impressao
        vm.printTipo = true;
        vm.printCategoria = true;
        vm.printNome = true;
        vm.printData = true;
        vm.printObservacao = true;
        vm.printValor = true;

        getCategorias('Todos');
        getSubCategorias(null);

        $scope.filterSubCategorias = function (categoriaId) {
            getSubCategorias(categoriaId);
        }

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

        function getSubCategorias(categoriaId) {
            if (categoriaId === null || categoriaId === undefined) {
                vm.subCategorias = null;
                $scope.filter.SubCategoriaId = null;
                //subCategoriaData.getAll().then(function (result) {
                    
                //    vm.subCategorias = result.data;
                //})
            } else {
                subCategoriaData.getByCategoria(categoriaId).then(function (result) {
                    vm.subCategorias = result.data;
                })
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
                        $scope.totalReceitas = $scope.totalReceitas + value.ValorTotal;
                    } else {
                        $scope.totalDespesas = $scope.totalDespesas + value.ValorTotal;
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
            $scope.filter = angular.copy(filterDefault);
            vm.printTipo = true;
            vm.printCategoria = true;
            vm.printNome = true;
            vm.printData = true;
            vm.printObservacao = true;
            vm.printValor = true;
        };

    }
})();
