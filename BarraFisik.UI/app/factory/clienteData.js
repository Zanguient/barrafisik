(function () {
    'use strict';

    app.factory('clienteData', clienteData);

    clienteData.$inject = ['$http', 'apiUrl'];


    function clienteData($http, apiUrl) {


        function getClientes() {
            return $http.get(apiUrl+'api/clientes');
        }

        function getCliente(idCliente) {
            return $http.get(apiUrl + 'api/cliente/' + idCliente);
        }

        function editCliente(cliente) {
            return $http.put(apiUrl + 'api/clientes', cliente);
        }

        function addCliente(clienteHorario) {
            return $http.post(apiUrl+'api/clientes', clienteHorario);
        }

        function desativarCliente(id) {
            return $http.put(apiUrl + 'api/cliente/desativar/' + id);
        }

        function ativarCliente(id) {
            return $http.put(apiUrl + 'api/cliente/ativar/' + id);
        }

        function getAniversariantes(mes) {
            return $http.get(apiUrl + "api/aniversariantes/" + mes);
        }

        var service = {
            getClientes: getClientes,
            getCliente: getCliente,
            addCliente: addCliente,
            editCliente: editCliente,
            desativarCliente: desativarCliente,
            ativarCliente: ativarCliente,
            getAniversariantes: getAniversariantes
        };

        return service;

    };



})();