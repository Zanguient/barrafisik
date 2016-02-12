(function () {
    'use strict';

    app.factory('relatorioFinanceiroData', relatorioFinanceiroData);

    relatorioFinanceiroData.$inject = ['$http', 'apiUrl'];


    function relatorioFinanceiroData($http, apiUrl) {


        function getRelatorio(filters) {
            return $http.post(apiUrl+'api/relatoriofinanceiro', filters);
        }

        function getRelatorioReceitas(filters) {
            return $http.post(apiUrl + 'api/relatoriofinanceiro/receitas', filters);
        }

        function getRelatorioDespesas(filters) {
            return $http.post(apiUrl + 'api/relatoriofinanceiro/despesas', filters);
        }


        var service = {
            getRelatorio: getRelatorio,
            getRelatorioReceitas: getRelatorioReceitas,
            getRelatorioDespesas: getRelatorioDespesas
        };

        return service;

    };



})();