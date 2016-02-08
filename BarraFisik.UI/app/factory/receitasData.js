(function () {
    'use strict';

    app.factory('receitasData', receitasData);

    receitasData.$inject = ['$http', "apiUrl"];

    function receitasData($http, apiUrl) {
        function getReceitas() {
            return $http.get(apiUrl + "api/receitas");
        }

        function addReceita(receita) {
            return $http.post(apiUrl + "api/receitas", receita);
        }

        function addMensalidade(mensalidade) {
            return $http.post(apiUrl + "api/receitas/mensalidade", mensalidade);
        }

        function addAvaliacaoFisica(avaliacao) {
            return $http.post(apiUrl + "api/receitas/avaliacaofisica", avaliacao);
        }

        function getMensalidades(idCliente) {
            return $http.get(apiUrl + "api/receitas/mensalidades/"+idCliente);
        }

        function getAvaliacaoCliente(idCliente) {
            return $http.get(apiUrl + "api/receitas/avaliacaofisica/" + idCliente);
        }

        function editReceita(receita) {
            return $http.put(apiUrl + "api/receitas", receita);
        }

        function editMensalidade(mensalidade) {
            return $http.put(apiUrl + "api/receitas/mensalidade", mensalidade);
        }

        function deleteReceita(id) {
            return $http.delete(apiUrl + "api/receitas/"+id);
        }

        function deleteMensalidade(id) {
            return $http.delete(apiUrl + "api/receitas/mensalidade/" + id);
        }

        function searchReceitas(search) {
            return $http.post(apiUrl + "api/receitas/search", search);
        }

        var service = {
            getReceitas: getReceitas,
            getMensalidades: getMensalidades,
            getAvaliacaoCliente: getAvaliacaoCliente,
            addReceita: addReceita,
            addMensalidade: addMensalidade,
            addAvaliacaoFisica: addAvaliacaoFisica,
            editReceita: editReceita,
            editMensalidade: editMensalidade,
            deleteReceita: deleteReceita,
            deleteMensalidade: deleteMensalidade,
            searchReceitas: searchReceitas
        };

        return service;
    }
})();