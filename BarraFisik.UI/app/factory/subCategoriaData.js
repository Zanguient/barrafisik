(function () {
    'use strict';

    app.factory('subCategoriaData', subCategoriaData);

    subCategoriaData.$inject = ['$http', 'apiUrl'];

    function subCategoriaData($http, apiUrl) {
        function getAll() {
            return $http.get(apiUrl + "api/subCategoriaFinanceira");
        }

        function getByCategoria(idCategoria) {
            return $http.get(apiUrl + "api/subCategoriaFinanceira/Categoria/"+ idCategoria);
        }

        function getById(id) {
            return $http.get(apiUrl + "api/subCategoriaFinanceira/" + id);
        }

        function add(subcategoria) {
            return $http.post(apiUrl + "api/subCategoriaFinanceira", subcategoria);
        }

        function edit(subcategoria) {
            return $http.put(apiUrl + "api/subCategoriaFinanceira", subcategoria);
        }

        function remove(id) {
            return $http.delete(apiUrl + "api/subCategoriaFinanceira/" + id);
        }


        var service = {
            getAll: getAll,
            getByCategoria: getByCategoria,
            getById: getById,
            add: add,
            edit: edit,
            remove: remove,
        };

        return service;
    }
})();