(function () {
    'use strict';

    app.factory('receitasData', receitasData);

    receitasData.$inject = ['$http', "apiUrl"];

    function receitasData($http, apiUrl) {
        function getReceitas() {
            return $http.get(apiUrl + "api/receitas");
        }

        function addReceita(receita) {
            return $http.post(apiUrl + "api/receitas", receita);
        }

        function editReceita(receita) {
            return $http.put(apiUrl + "api/receitas", receita);
        }

        function deleteReceita(id) {
            return $http.delete(apiUrl + "api/receitas/"+id);
        }

        var service = {
            getReceitas: getReceitas,
            addReceita: addReceita,
            editReceita: editReceita,
            deleteReceita: deleteReceita
        };

        return service;
    }
})();