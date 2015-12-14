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

        function editCliente(clienteHorario) {
            return $http.put(apiUrl + 'api/clienteUpdate', clienteHorario);
        }

        function getClientePerfil(id) {
            return $http.get(apiUrl + 'api/cliente/perfil/' + id);
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

        function updateClientesPendentes(mes, ano) {
            return $http.get(apiUrl + 'api/clientes/updateClientesPendentes/'+mes+"/"+ano);
        }

        function getClientesSituacao(situacao) {
            return $http.get(apiUrl+'api/clientes/'+situacao);
        }

        function inativarClientes(clientes) {
            return $http.post(apiUrl + 'api/clientes/inativarClientes', clientes);
        }

        function getClientesAll() {
            return $http.get(apiUrl + 'api/clientes/all');
        }

        function getInscritos(ano) {
            return $http.get(apiUrl + 'api/clientes/inscritos/'+ano);
        }

        var service = {
            getClientes: getClientes,
            getCliente: getCliente,
            addCliente: addCliente,
            editCliente: editCliente,
            desativarCliente: desativarCliente,
            ativarCliente: ativarCliente,
            getAniversariantes: getAniversariantes,
            updateClientesPendentes: updateClientesPendentes,
            getClientesSituacao: getClientesSituacao,
            inativarClientes: inativarClientes,
            getClientesAll: getClientesAll,
            getInscritos: getInscritos,
            getClientePerfil: getClientePerfil
        };

        return service;

    };



})();