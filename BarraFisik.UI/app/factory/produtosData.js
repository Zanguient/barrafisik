(function () {
    'use strict';

    app.factory('produtosData', produtosData);

    produtosData.$inject = ['$http', 'apiUrl'];


    function produtosData($http, apiUrl) {


        function getAll() {
            return $http.get(apiUrl + 'api/produtos');
        }

        function getByCategoria(categoriaId) {
            return $http.get(apiUrl + 'api/produtos/categoria/'+categoriaId);
        }

        function add(produto) {
            return $http.post(apiUrl + 'api/produtos', produto);
        }

        function edit(produto) {
            return $http.put(apiUrl + 'api/produtos', produto);
        }

        function remove(id) {
            return $http.delete(apiUrl + 'api/produtos/' + id);
        }

        var service = {
            getAll: getAll,
            getByCategoria: getByCategoria,
            add: add,
            edit: edit,
            remove: remove
        };

        return service;

    };



})();