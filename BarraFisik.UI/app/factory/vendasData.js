(function () {
    'use strict';

    app.factory('vendasData', vendasData);

    vendasData.$inject = ['$http', 'apiUrl'];


    function vendasData($http, apiUrl) {


        function getAll() {
            return $http.get(apiUrl + 'api/vendas');
        }

        function getVendasProdutos() {
            return $http.get(apiUrl + 'api/vendasprodutos');
        }

        function add(venda) {
            return $http.post(apiUrl + 'api/vendas', venda);
        }

        function addVendasProdutos(vendasProdutosList, idVenda) {
            return $http.post(apiUrl + 'api/vendasprodutos/'+idVenda,  vendasProdutosList);
        }

        function getPendentes(mes, ano) {
            return $http.get(apiUrl + 'api/vendas/pendentes/'+mes+'/'+ano);
        }

        function getVendasAnual(ano) {
            return $http.get(apiUrl + 'api/vendas/anual/'+ano);
        }

        function edit(venda) {
            return $http.put(apiUrl + 'api/vendas', venda);
        }

        function searchVendas(search) {
            return $http.post(apiUrl + 'api/vendas/search', search);
        }

        function remove(id) {
            return $http.delete(apiUrl + 'api/vendas/' + id);
        }

        function getVendasByCliente(idCliente) {
            return $http.get(apiUrl + 'api/vendas/cliente/' + idCliente);
        }

        var service = {
            getAll: getAll,
            add: add,
            edit: edit,
            searchVendas: searchVendas,
            getPendentes: getPendentes,
            getVendasAnual: getVendasAnual,
            remove: remove,
            addVendasProdutos: addVendasProdutos,
            getVendasProdutos: getVendasProdutos,
            getVendasByCliente: getVendasByCliente,
        };

        return service;

    };



})();