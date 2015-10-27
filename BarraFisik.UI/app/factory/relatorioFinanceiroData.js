(function () {
    'use strict';

    app.factory('relatorioFinanceiroData', relatorioFinanceiroData);

    relatorioFinanceiroData.$inject = ['$http', 'apiUrl'];


    function relatorioFinanceiroData($http, apiUrl) {


        function getRelatorio(filters) {
            return $http.post(apiUrl+'api/relatoriofinanceiro', filters);
        }


        var service = {
            getRelatorio: getRelatorio,
        };

        return service;

    };



})();