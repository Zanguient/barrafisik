(function () {
    'use strict';

    app.factory('valoresData', valoresData);

    valoresData.$inject = ['$http', 'apiUrl'];


    function valoresData($http, apiUrl) {


        function getValores() {
            return $http.get(apiUrl+'api/valores');
        }

        function addValor(valor) {
            return $http.post(apiUrl + 'api/valores', valor);
        }

        function editValor(valor) {
            return $http.put(apiUrl + 'api/valores', valor);
        }

        function deleteValor(id) {
            return $http.delete(apiUrl + 'api/valores/'+id);
        }

        var service = {
            getValores: getValores,
            addValor: addValor,
            editValor: editValor,
            deleteValor: deleteValor
        };

        return service;

    };



})();