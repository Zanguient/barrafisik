"use strict";

app.controller('ClientesPendentesCtrl', ["$scope", "clienteData", "$rootScope", "SweetAlert", "$state", function ($scope, clienteData, $rootScope, SweetAlert, $state) {

    //Atualiza clientes para pendente caso dia do mes for maior que 10 e não tenha mensalidade paga para o mês atual
    var today = new Date();
    if (today.getDate() > 10 && $rootScope.updateClientes === false) {
        clienteData.updateClientesPendentes(today.getMonth(), today.getFullYear()).then(function (data) { });
        $rootScope.updateClientes = true;
    }

    $scope.clientesPendentes = [];

    clienteData.getClientesSituacao('Pendente').then(function (result) {
        $scope.clientesPendentes = result.data;
    }), function (error) {
        console.log(error);
    }

    $scope.selection = [];
    $scope.selectedAll = false;

    $scope.checkAll = function () {
        $scope.selectedAll = !$scope.selectedAll;
        if ($scope.selectedAll) {
            $scope.selectedAll = true;
            angular.forEach($scope.clientesPendentes, function (item) {
                if (item.Selected === false) {
                    $scope.selection.push(item);
                    item.Selected = $scope.selectedAll;
                }
            });
        } else {
            $scope.selectedAll = false;
            angular.forEach($scope.clientesPendentes, function (item) {
                var idx = $scope.selection.indexOf(item);
                if (idx > -1) {
                    $scope.selection.splice(idx, 1);
                }
                item.Selected = $scope.selectedAll;
            });
        }
    };

    $scope.toggleSelection = function toggleSelection(cliente) {
        var idx = $scope.selection.indexOf(cliente);

        // is currently selected
        if (idx > -1) {
            $scope.selection.splice(idx, 1);
        }

            // is newly selected
        else {
            $scope.selection.push(cliente);
        }
    };

    $scope.inativarSelecionados = function () {
        if ($scope.selection.length === 0) {
            sweetAlert('Seleção vazia', 'Nenhum cliente foi selecionado para inativação!', 'error');
        } else {
            SweetAlert.swal({
                title: "Confirmar Inativação?",
                text: "Tem certeza que deseja inativar esses clientes?",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Confirmar",
                cancelButtonText: "Cancelar",
                closeOnConfirm: false,
                closeOnCancel: false
            }, function (isConfirm) {
                if (isConfirm) {
                    clienteData.inativarClientes($scope.selection).then(function (data) {
                        SweetAlert.swal("Inativado!", "Cliente Inativado com sucesso!", "success");
                        $state.go($state.current, {}, { reload: true });
                    }), function (error) {
                        console.log(error);
                    };

                } else {
                    SweetAlert.swal({
                        title: "Cancelado",
                        text: "Operação de inativação cancelada",
                        type: "error",
                        confirmButtonColor: "#007AFF"
                    });
                }
            });
        }
    }

}
]);
app.controller('InscritosCtrl', ["$scope", "clienteData", "$rootScope", function ($scope, clienteData, $rootScope) {

    $scope.ano1 = new Date().getFullYear();
    $scope.ano2 = new Date().getFullYear()-1;
    $scope.selectAno = [];

    $scope.getAno = function () {
        $scope.selectAno = [];
        activate();
    }

    $rootScope.userInfo = JSON.parse(localStorage.getItem('authorizationData'));

    activate();

    function activate() {
        clienteData.getInscritos($scope.ano1).then(function(result) {
            var i = result.data;

            $scope.inscritos = [i.Janeiro, i.Fevereiro, i.Marco, i.Abril, i.Maio, i.Junho, i.Julho, i.Agosto, i.Setembro, i.Outubro, i.Novembro, i.Dezembro];

            clienteData.getInscritos($scope.ano2).then(function(result2) {
                var i2 = result2.data;

                $scope.UltimoAno = i2.UltimoAno;
                $scope.PrimeiroAno = i2.PrimeiroAno;                
                for (var x = i2.UltimoAno; x >= i2.PrimeiroAno; x--) {
                    $scope.selectAno.push({ ano: x });
                }

                $scope.inscritos2 = [i2.Janeiro, i2.Fevereiro, i2.Marco, i2.Abril, i2.Maio, i2.Junho, i2.Julho, i2.Agosto, i2.Setembro, i2.Outubro, i2.Novembro, i2.Dezembro];

                $scope.data = {
                    labels: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
                    datasets: [
                        {
                            label: $scope.ano2,
                            fillColor: 'rgba(220,220,220,0.2)',
                            strokeColor: 'rgba(220,220,220,1)',
                            pointColor: 'rgba(220,220,220,1)',
                            pointStrokeColor: '#fff',
                            pointHighlightFill: '#fff',
                            pointHighlightStroke: 'rgba(220,220,220,1)',
                            data: $scope.inscritos2
                        },
                        {
                            label: $scope.ano1,
                            fillColor: 'rgba(151,187,205,0.2)',
                            strokeColor: 'rgba(151,187,205,1)',
                            pointColor: 'rgba(151,187,205,1)',
                            pointStrokeColor: '#fff',
                            pointHighlightFill: '#fff',
                            pointHighlightStroke: 'rgba(151,187,205,1)',
                            data: $scope.inscritos
                        }
                    ]
                };
            });
        }).catch(function(error) {
            console.log(error);
        });
    }
   

    $scope.options = {

        maintainAspectRatio: false,

        // Sets the chart to be responsive
        responsive: true,

        ///Boolean - Whether grid lines are shown across the chart
        scaleShowGridLines: true,

        //String - Colour of the grid lines
        scaleGridLineColor: 'rgba(0,0,0,.05)',

        //Number - Width of the grid lines
        scaleGridLineWidth: 1,

        //Boolean - Whether the line is curved between points
        bezierCurve: false,

        //Number - Tension of the bezier curve between points
        bezierCurveTension: 0.4,

        //Boolean - Whether to show a dot for each point
        pointDot: true,

        //Number - Radius of each point dot in pixels
        pointDotRadius: 4,

        //Number - Pixel width of point dot stroke
        pointDotStrokeWidth: 1,

        //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
        pointHitDetectionRadius: 20,

        //Boolean - Whether to show a stroke for datasets
        datasetStroke: true,

        //Number - Pixel width of dataset stroke
        datasetStrokeWidth: 2,

        //Boolean - Whether to fill the dataset with a colour
        datasetFill: true,

        // Function - on animation progress
        onAnimationProgress: function () { },

        // Function - on animation complete
        onAnimationComplete: function () { },

        //String - A legend template
        legendTemplate: '<ul class="tc-chart-js-legend"><% for (var i=0; i<datasets.length; i++){%><li><span style="background-color:<%=datasets[i].strokeColor%>"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>'
    };

}]);