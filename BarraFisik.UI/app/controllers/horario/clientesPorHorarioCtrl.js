(function() {
    "use strict";

    app.controller('clientesPorHorarioCtrl', clientesPorHorarioCtrl);

    function clientesPorHorarioCtrl($scope, cliente, $modalInstance) {
        var vm = this;

        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }

}());