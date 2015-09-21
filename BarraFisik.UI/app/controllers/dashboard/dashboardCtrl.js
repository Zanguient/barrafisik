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
                    }), function(error) {
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
