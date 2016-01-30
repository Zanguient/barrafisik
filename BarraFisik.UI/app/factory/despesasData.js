(function () {
    'use strict';

    app.factory('despesasData', despesasData);

    despesasData.$inject = ['$http', 'apiUrl'];

    function despesasData($http, apiUrl) {
        function getDespesas() {
            return $http.get(apiUrl + "api/despesas");
        }

        function addDespesa(despesa) {
            return $http.post(apiUrl + "api/despesas", despesa);
        }

        function editDespesa(despesa) {
            return $http.put(apiUrl + "api/despesas", despesa);
        }

        function deleteDespesa(id) {
            return $http.delete(apiUrl + "api/despesas/" + id);
        }

        function searchDespesas(search) {
            return $http.post(apiUrl + "api/despesas/search", search);
        }

        var service = {
            getDespesas: getDespesas,
            addDespesa: addDespesa,
            editDespesa: editDespesa,
            deleteDespesa: deleteDespesa,
            searchDespesas: searchDespesas
        };

        return service;
    }
})();