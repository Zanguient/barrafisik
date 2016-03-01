(function () {
    'use strict';

    app.factory('vendasProdutosData', vendasProdutosData);

    vendasProdutosData.$inject = ['$http', 'apiUrl'];


    function vendasProdutosData($http, apiUrl) {


        function getAll() {
            return $http.get(apiUrl + 'api/vendasprodutos');
        }

        function getByVenda(vendaId) {
            return $http.get(apiUrl + 'api/vendasprodutos/venda/' + vendaId);
        }

        function addProduto(produto) {
            return $http.post(apiUrl + 'api/vendasprodutos/addproduto', produto);
        }

        function edit(vendasProdutos) {
            return $http.post(apiUrl + 'api/vendasprodutos', vendasProdutos);
        }

        function addVendasProdutos(vendasProdutosList, idVenda) {
            return $http.post(apiUrl + 'api/vendasprodutos/'+idVenda,  vendasProdutosList);
        }        

        function remove(id) {
            return $http.delete(apiUrl + 'api/vendasprodutos/' + id);
        }

        var service = {
            getAll: getAll,
            addProduto: addProduto,
            remove: remove,
            edit: edit,
            getByVenda: getByVenda,
            addVendasProdutos: addVendasProdutos,
        };

        return service;

    };


})();