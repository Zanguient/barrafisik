(function() {
    'use strict';

    app.factory('horarioData', horarioData);

    horarioData.$inject = ['$http', 'apiUrl'];

    function horarioData($http, apiUrl) {


        function addHorario(horario) {
            return $http.post(apiUrl+'api/horarios', horario);
        }

        function getHorarioCliente(idCliente) {
            return $http.get(apiUrl + 'api/horarios/cliente/' + idCliente);
        }

        function editHorario(horario) {
            return $http.put(apiUrl + 'api/horarios', horario);
        }

        var service = {
            addHorario: addHorario,
            getHorarioCliente: getHorarioCliente,
            editHorario: editHorario
        };

        return service;

    };
}());