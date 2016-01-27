(function () {
    'use strict';

    app.factory('armazemData', armazemData);

    armazemData.$inject = ['$http', 'apiUrl'];


    function armazemData($http, apiUrl) {


        function getArmazem() {
            return $http.get(apiUrl + 'api/armazem');
        }

        function addArmazem(armazem) {
            return $http.post(apiUrl + 'api/armazem', armazem);
        }

        function editArmazem(armazem) {
            return $http.put(apiUrl + 'api/armazem', armazem);
        }

        function deleteArmazem(id) {
            return $http.delete(apiUrl + 'api/armazem/' + id);
        }

        var service = {
            getArmazem: getArmazem,
            addArmazem: addArmazem,
            editArmazem: editArmazem,
            deleteArmazem: deleteArmazem
        };

        return service;

    };



})();