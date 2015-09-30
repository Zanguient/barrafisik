(function() {
    "use strict";

    app.controller('horarioClienteCtrl', horarioClienteCtrl);

    function horarioClienteCtrl($scope, cliente, $modalInstance) {
        var vm = this;

        $scope.parseInt = parseInt;

        $scope.cliente = cliente;

        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }

}());