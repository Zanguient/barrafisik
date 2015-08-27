(function () {
    'use strict';

    app.factory('clienteData', clienteData);

    clienteData.$inject = ['$http', 'apiUrl'];


    function clienteData($http, apiUrl) {


        function getClientes() {
            return $http.get(apiUrl+'api/clientes');
        }

        function addCliente(cliente) {
            return $http.post(apiUrl+'api/clientes', cliente);
        }

        function desativarCliente(id) {
            return $http.put(apiUrl + 'api/cliente/desativar/' + id);
        }

        function ativarCliente(id) {
            return $http.put(apiUrl + 'api/cliente/ativar/' + id);
        }

        var service = {
            getClientes: getClientes,
            addCliente: addCliente,
            desativarCliente: desativarCliente,
            ativarCliente: ativarCliente
        };

        return service;

    };



})();