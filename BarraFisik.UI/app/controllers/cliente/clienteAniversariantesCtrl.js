(function () {
    app.controller('clienteAniversariantesCtrl', clienteAniversariantesCtrl);

    function clienteAniversariantesCtrl(clienteData, $stateParams, $scope, SweetAlert, $filter, $state) {

        $scope.aniversariantes = [];

        $scope.months = [{
            value: 1,
            text: 'Janeiro'
        }, {
            value: 2,
            text: 'Fevereiro'
        }, {
            value: 3,
            text: 'Março'
        }, {
            value: 4,
            text: 'Abril'
        }, {
            value: 5,
            text: 'Maio'
        }, {
            value: 6,
            text: 'Junho'
        }, {
            value: 7,
            text: 'Julho'
        }, {
            value: 8,
            text: 'Agosto'
        }, {
            value: 9,
            text: 'Setembro'
        }, {
            value: 10,
            text: 'Outubro'
        }, {
            value: 11,
            text: 'Novembro'
        }, {
            value: 12,
            text: 'Dezembro'
        }];

        $scope.referencia = {
            mes: $stateParams.mes
        };

        $scope.showStatus = function () {
            var selected = $filter('filter')($scope.months, {
                value: $scope.referencia.mes
            });
            return ($scope.referencia.mes && selected.length) ? selected[0].text : 'Not set';
        };

        $scope.alterames = function(data) {
            $state.go('app.clientes.aniversariantes', {mes:data});
        }
        
        clienteData.getAniversariantes($stateParams.mes).then(function (result) {
            if (result.data.length === 0)
                $scope.vazio = "Nenhum cliente faz aniversário neste mês";
            else $scope.aniversariantes = result.data;
        }), function (error) {
            console.log = error;
        };
    }
})();