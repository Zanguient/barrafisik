(function () {
    'use strict';

    app.factory('categoriaFinanceiraData', categoriaFinanceiraData);

    categoriaFinanceiraData.$inject = ['$http', 'apiUrl'];


    function categoriaFinanceiraData($http, apiUrl) {


        function getCategorias() {
            return $http.get(apiUrl+'api/categoriaFinanceira');
        }

        function getCategoriaByTipo(tipo) {
            return $http.get(apiUrl + "api/categoriaFinanceira/" + tipo);
        }

        function addCategoria(categoria) {
            return $http.post(apiUrl + 'api/categoriaFinanceira', categoria);
        }

        function editCategoria(categoria) {
            return $http.put(apiUrl + 'api/categoriaFinanceira', categoria);
        }

        function deleteCategoria(id) {
            return $http.delete(apiUrl + 'api/categoriaFinanceira/' + id);
        }

        var service = {
            getCategorias: getCategorias,
            getCategoriaByTipo: getCategoriaByTipo,
            addCategoria: addCategoria,
            editCategoria: editCategoria,
            deleteCategoria: deleteCategoria
        };

        return service;

    };



})();