"use strict";

app.controller('dashCtrl', ["$rootScope", function ($rootScope) {
    // Usuário
    $rootScope.userInfo = JSON.parse(localStorage.getItem('authorizationData'));
}]);
app.controller('ClientesPendentesCtrl', ["$scope", "clienteData", "$rootScope", "SweetAlert", "$state", function ($scope, clienteData, $rootScope, SweetAlert, $state) {

    //Atualiza clientes para pendente caso dia do mes for maior que 10 e não tenha mensalidade paga para o mês atual
    var today = new Date();
    if (today.getDate() > 10 && $rootScope.updateClientes === false) {
        clienteData.updateClientesPendentes(today.getMonth() + 1, today.getFullYear()).then(function (data) {
            //Lista os pendentes quando atualiza os status dos clientes
            $scope.clientesPendentes = [];
            clienteData.getClientesSituacao('Pendente').then(function (result) {
                $scope.clientesPendentes = result.data;
            }), function (error) {
                console.log(error);
            }
        });
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
    $scope.ano2 = new Date().getFullYear() - 1;
    $scope.selectAno = [];

    $scope.getAno = function () {
        $scope.selectAno = [];
        activate();
    }

    activate();

    function activate() {
        clienteData.getInscritos($scope.ano1).then(function (result) {
            var i = result.data;

            $scope.inscritos = [i.Janeiro, i.Fevereiro, i.Marco, i.Abril, i.Maio, i.Junho, i.Julho, i.Agosto, i.Setembro, i.Outubro, i.Novembro, i.Dezembro];

            clienteData.getInscritos($scope.ano2).then(function (result2) {
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
        }).catch(function (error) {
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
app.controller('lastDespesasCtrl', ["$scope", "despesasData", "$rootScope", function ($scope, despesasData, $rootScope) {
    $scope.despesas = [];
    $scope.pendentes = 0.00;
    $scope.quitados = 0.00;


    despesasData.getDespesas().then(function (despesas) {
        $scope.despesas = despesas.data;

        angular.forEach($scope.despesas, function (item) {
            if (item.Situacao === 'Pendente') {
                $scope.pendentes = parseFloat($scope.pendentes) + item.ValorTotal;
            }

            if (item.Situacao === 'Quitado')
                $scope.quitados = parseFloat($scope.quitados) + item.ValorTotal;
        });

        $scope.data = [
        {
            value: $scope.pendentes,
            color: '#F7464A',
            highlight: '#FF5A5E',
            label: 'Pend.'
        },
        {
            value: $scope.quitados,
            color: '#46BFBD',
            highlight: '#5AD3D1',
            label: 'Quit.'
        }
        ];

        $scope.total = $scope.pendentes + $scope.quitados;
    });


    // Chart.js Options
    $scope.options = {

        // Sets the chart to be responsive
        responsive: false,

        //Boolean - Whether we should show a stroke on each segment
        segmentShowStroke: true,

        //String - The colour of each segment stroke
        segmentStrokeColor: '#fff',

        //Number - The width of each segment stroke
        segmentStrokeWidth: 2,

        //Number - The percentage of the chart that we cut out of the middle
        percentageInnerCutout: 50, // This is 0 for Pie charts

        //Number - Amount of animation steps
        animationSteps: 100,

        //String - Animation easing effect
        animationEasing: 'easeOutBounce',

        //Boolean - Whether we animate the rotation of the Doughnut
        animateRotate: true,

        //Boolean - Whether we animate scaling the Doughnut from the centre
        animateScale: false,

        //String - A legend template
        legendTemplate: '<ul class="tc-chart-js-legend"><% for (var i=0; i<segments.length; i++){%><li><span style="background-color:<%=segments[i].fillColor%>"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>'
    }
}
]);
app.controller('vendasPendentesCtrl', ["$scope", "vendasData", "$rootScope", function ($scope, vendasData, $rootScope) {
    var vm = this;
    vm.vendas = [];

    var today = new Date();
    $scope.mesAtual = today.getMonth() + 1;
    $scope.anoAtual = today.getFullYear();
    $scope.anoReferencia = today.getFullYear();


    vendasData.getPendentes(today.getMonth() + 1, today.getFullYear()).then(function (result) {
        vm.vendas = result.data;
    });

    $scope.getPendentes = function () {
        vendasData.getPendentes($scope.mesAtual, $scope.anoAtual).then(function (result) {
            vm.vendas = result.data;
        });
    }

    active();

    function active() {
        vendasData.getVendasAnual($scope.anoReferencia).then(function (result) {
            // Chart.js Data
            $scope.data = {
                labels: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
                datasets: [
                {
                    label: 'Total Vendido',
                    fillColor: 'rgba(220,220,220,0.5)',
                    strokeColor: 'rgba(220,220,220,0.8)',
                    highlightFill: 'rgba(220,220,220,0.75)',
                    highlightStroke: 'rgba(220,220,220,1)',
                    data: result.data
                }
                ]
            };
        });
    }

    $scope.getVendasAnuais = function() {
        active();
    }

    // Chart.js Options
    $scope.options = {
        maintainAspectRatio: true,

        // Sets the chart to be responsive
        responsive: true,

        //Boolean - Whether the scale should start at zero, or an order of magnitude down from the lowest value
        scaleBeginAtZero: true,

        //Boolean - Whether grid lines are shown across the chart
        scaleShowGridLines: true,

        //String - Colour of the grid lines
        scaleGridLineColor: "rgba(0,0,0,.05)",

        //Number - Width of the grid lines
        scaleGridLineWidth: 1,

        //Boolean - If there is a stroke on each bar
        barShowStroke: true,

        //Number - Pixel width of the bar stroke
        barStrokeWidth: 2,

        //Number - Spacing between each of the X value sets
        barValueSpacing: 5,

        //Number - Spacing between data sets within X values
        barDatasetSpacing: 1,

        //String - A legend template
        legendTemplate: '<ul class="tc-chart-js-legend"><% for (var i=0; i<datasets.length; i++){%><li><span style="background-color:<%=datasets[i].fillColor%>"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>'

    }
}
]);
