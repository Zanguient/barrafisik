(function() {
    "use strict";

    app.controller('horarioCtrl', horarioCtrl);

    function horarioCtrl($scope, horarioData) {
        $scope.teste = "Controller Horario";

        horarioData.gethorarioTotal().then(function(result) {
            $scope.horario = result.data;
        }, function(error) {
            console.log(error);
        });
    }

}());