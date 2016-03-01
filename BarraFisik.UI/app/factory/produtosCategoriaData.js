(function () {
    'use strict';

    app.factory('produtosCategoriaData', produtosCategoriaData);

    produtosCategoriaData.$inject = ['$http', 'apiUrl'];


    function produtosCategoriaData($http, apiUrl) {


        function getAll() {
            return $http.get(apiUrl + 'api/produtosCategoria');
        }

        function add(categoria) {
            return $http.post(apiUrl + 'api/produtosCategoria', categoria);
        }

        function edit(categoria) {
            return $http.put(apiUrl + 'api/produtosCategoria', categoria);
        }

        function remove(id) {
            return $http.delete(apiUrl + 'api/produtosCategoria/' + id);
        }

        var service = {
            getAll: getAll,
            add: add,
            edit: edit,
            remove: remove
        };

        return service;

    };



})();