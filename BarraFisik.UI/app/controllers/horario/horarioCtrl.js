(function () {
    "use strict";

    app.controller('horarioCtrl', horarioCtrl);

    function horarioCtrl($scope, horarioData, $modal) {
        var vm = this;
        $scope.teste = "Controller Horario";

        $scope.$emit('LOAD');
        horarioData.gethorarioTotal().then(function (result) {
            $scope.horario = result.data;
            $scope.$emit('UNLOAD');
        }, function (error) {
            console.log(error);
        });

        //Horários
        $scope.clientesPorHorario = function (dia, hora) {
            vm.modalInstance = $modal.open({
                templateUrl: 'app/views/horario/clientesPorHorario.html',
                size: 'sm',
                deps: [
                    '$ocLazyLoad',
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load(['app/controllers/horario/clientesPorHorarioCtrl.js']);
                    }
                ],
                controller: 'clientesPorHorarioCtrl as vm'
            });
            vm.modalInstance.result.then(function (data) {

            }, function () {
                console.log('Cancelled');
            });
        }
    }

}());