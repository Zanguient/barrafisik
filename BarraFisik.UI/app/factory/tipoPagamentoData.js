(function () {
    'use strict';

    app.factory('tipoPagamentoData', tipoPagamentoData);

    tipoPagamentoData.$inject = ['$http', 'apiUrl'];


    function tipoPagamentoData($http, apiUrl) {


        function getTipos() {
            return $http.get(apiUrl+'api/tipo');
        }

        function addTipo(tipo) {
            return $http.post(apiUrl + 'api/tipo', tipo);
        }

        function editTipo(tipo) {
            return $http.put(apiUrl + 'api/tipo', tipo);
        }

        function deleteTipo(id) {
            return $http.delete(apiUrl + 'api/tipo/' + id);
        }

        var service = {
            getTipos: getTipos,
            addTipo: addTipo,
            editTipo: editTipo,
            deleteTipo: deleteTipo
        };

        return service;

    };



})();