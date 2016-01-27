(function () {
    'use strict';

    app.factory('funcionariosData', funcionariosData);

    funcionariosData.$inject = ['$http', 'apiUrl'];


    function funcionariosData($http, apiUrl) {


        function getAll() {
            return $http.get(apiUrl + 'api/funcionarios');
        }

        function getAllAtivos() {
            return $http.get(apiUrl + 'api/funcionariosAtivos');
        }

        function add(funcionario) {
            return $http.post(apiUrl + 'api/funcionarios', funcionario);
        }

        function edit(funcionario) {
            return $http.put(apiUrl + 'api/funcionarios', funcionario);
        }

        function remove(id) {
            return $http.delete(apiUrl + 'api/funcionarios/' + id);
        }

        var service = {
            getAll: getAll,
            getAllAtivos: getAllAtivos,
            add: add,
            edit: edit,
            remove: remove
        };

        return service;

    };



})();