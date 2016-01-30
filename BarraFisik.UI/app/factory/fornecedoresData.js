(function () {
    'use strict';

    app.factory('fornecedoresData', fornecedoresData);

    fornecedoresData.$inject = ['$http', 'apiUrl'];


    function fornecedoresData($http, apiUrl) {


        function getAll() {
            return $http.get(apiUrl + 'api/fornecedores');
        }

        function getAllAtivos() {
            return $http.get(apiUrl + 'api/fornecedoresAtivos');
        }

        function getById(id) {
            return $http.get(apiUrl + 'api/fornecedores/'+id);
        }

        function add(fornecedor) {
            return $http.post(apiUrl + 'api/fornecedores', fornecedor);
        }

        function edit(fornecedor) {
            return $http.put(apiUrl + 'api/fornecedores', fornecedor);
        }

        function remove(id) {
            return $http.delete(apiUrl + 'api/fornecedores/' + id);
        }

        var service = {
            getAll: getAll,
            getAllAtivos: getAllAtivos,
            getById: getById,
            add: add,
            edit: edit,
            remove: remove
        };

        return service;

    };



})();