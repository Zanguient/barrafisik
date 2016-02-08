(function () {
    'use strict';

    app.factory('mensalidadesData', mensalidadesData);

    mensalidadesData.$inject = ['$http', 'apiUrl'];


    function mensalidadesData($http, apiUrl) {


        function getMensalidades(id) {
            return $http.get(apiUrl+'api/mensalidades/'+id);
        }

        //function addMensalidade(mensalidade) {
        //    return $http.post(apiUrl+'api/mensalidades', mensalidade);
        //}

        function addMensalidade(mensalidade) {
            return $http.post(apiUrl + 'api/receitas/mensalidade', mensalidade);
        }

        function editMensalidade(mensalidade) {
            return $http.put(apiUrl+'api/mensalidades', mensalidade);
        }

        function deleteMensalidade(id) {
            return $http.delete(apiUrl + 'api/mensalidades/' + id);
        }

        var service = {
            getMensalidades: getMensalidades,
            addMensalidade: addMensalidade,
            editMensalidade: editMensalidade,
            deleteMensalidade: deleteMensalidade
        };

        return service;

    };



})();