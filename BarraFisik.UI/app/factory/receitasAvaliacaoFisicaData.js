(function() {
    "use strict";

    app.factory("receitasAvaliacaoFisicaData", receitasAvaliacaoFisicaData);

    receitasAvaliacaoFisicaData.$inject = ['$http', 'apiUrl'];

    function receitasAvaliacaoFisicaData($http, apiUrl) {
        
        function getAll() {
            return $http.get(apiUrl + 'api/receitasAvaliacaoFisica');
        }

        function addReceitaAvaliacaoFisica(avaliacaoFisica) {
            return $http.post(apiUrl + 'api/receitasAvaliacaoFisica', avaliacaoFisica);
        }

        function getByCliente(id) {
            return $http.get(apiUrl + 'api/receitasAvaliacaoPorCliente/' + id);
        }

        function editReceitaAvaliacao(avaliacao) {
            return $http.put(apiUrl + 'api/receitasAvaliacaoFisica', avaliacao);
        }

        function deleteReceitaAvaliacao(id) {
            return $http.delete(apiUrl + 'api/receitasAvaliacaoFisica/' + id);
        }

        var service = {
            getAll: getAll,
            addReceitaAvaliacaoFisica: addReceitaAvaliacaoFisica,
            getByCliente: getByCliente,
            editReceitaAvaliacao: editReceitaAvaliacao,
            deleteReceitaAvaliacao: deleteReceitaAvaliacao,
        }

        return service;
    }

}());