(function () {
    'use strict';

    app.factory('filaEsperaData', filaEsperaData);

    filaEsperaData.$inject = ['$http', 'apiUrl'];


    function filaEsperaData($http, apiUrl) {


        function getFila() {
            return $http.get(apiUrl+'api/filaespera');
        }

        function addFila(fila) {
            return $http.post(apiUrl + 'api/filaespera', fila);
        }

        function editFila(fila) {
            return $http.put(apiUrl + 'api/filaespera', fila);
        }

        function deleteFila(id) {
            return $http.delete(apiUrl + 'api/filaespera/'+id);
        }

        var service = {
            getFila: getFila,
            addFila: addFila,
            editFila: editFila,
            deleteFila: deleteFila
        };

        return service;

    };



})();