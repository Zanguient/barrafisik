(function () {
    'use strict';

    app.factory('movimentacaoData', movimentacaoData);

    movimentacaoData.$inject = ['$http', 'apiUrl'];


    function movimentacaoData($http, apiUrl) {


        function getAll() {
            return $http.get(apiUrl + 'api/movimentacao');
        }

        function getByEstoque(id) {
            return $http.get(apiUrl + 'api/movimentacao/estoque/'+id);
        }

        function add(movimentacao) {
            return $http.post(apiUrl + 'api/movimentacao', movimentacao);
        }

        function edit(movimentacao) {
            return $http.put(apiUrl + 'api/movimentacao', movimentacao);
        }

        function remove(id) {
            return $http.delete(apiUrl + 'api/movimentacao/' + id);
        }

        var service = {
            getAll: getAll,
            getByEstoque: getByEstoque,
            add: add,
            edit: edit,
            remove: remove
        };

        return service;

    };



})();