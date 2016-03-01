(function () {
    'use strict';

    app.factory('estoqueData', estoqueData);

    estoqueData.$inject = ['$http', 'apiUrl'];


    function estoqueData($http, apiUrl) {


        function getAll() {
            return $http.get(apiUrl + 'api/estoque');
        }

        function getByArmazem(id) {
            return $http.get(apiUrl + 'api/estoque/armazem/'+id);
        }

        function add(estoque) {
            return $http.post(apiUrl + 'api/estoque', estoque);
        }

        function getById(id) {
            return $http.get(apiUrl + 'api/estoque/'+ id);
        }

        function edit(estoque) {
            return $http.put(apiUrl + 'api/estoque', estoque);
        }

        function remove(id) {
            return $http.delete(apiUrl + 'api/estoque/' + id);
        }

        var service = {
            getAll: getAll,
            getByArmazem: getByArmazem,
            getById: getById,
            add: add,
            edit: edit,
            remove: remove
        };

        return service;

    };



})();