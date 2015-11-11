(function () {
    "use strict";

    app.controller('mensalidadesCtrl', mensalidadesCtrl);

    function mensalidadesCtrl($scope, ClienteId, modalService, $modalInstance, mensalidadesData, ngTableParams, $filter, $timeout, SweetAlert, toaster) {
        var vm = this;
        vm.mensalidades = [];
        $scope.teste = "teste";

        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        mensalidadesData.getMensalidades(ClienteId).then(function (result) {
            vm.mensalidades = result.data;
            loadTable(vm.mensalidades);
        });

        function loadTable(data) {
            $scope.tableParams = new ngTableParams({
                page: 1, // show first page
                count: 12, // count per page
                sorting: {
                    DataPagamento: 'desc' // initial sorting
                }
            }, {
                counts: [],
                total: data.length, // length of data
                getData: function ($defer, params) {
                    // use build-in angular filter
                    var orderedData = params.sorting() ? $filter('orderBy')(data, params.orderBy()) : data;
                    $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                }
            });

            $scope.$watch('vm.mensalidades', function () {
                $scope.tableParams.reload();
            });
        }

        vm.delete = function (id) {            
            var modalOptions = {
                closeButtonText: 'Cancelar',
                actionButtonText: 'Excluir',
                headerText: 'Excluir ?',
                bodyText: 'Tem certeza que deseja exlcuir esta mensalidade?'
            };

            modalService.showModal({}, modalOptions).then(function () {
                mensalidadesData.deleteMensalidade(id).then(function () {
                    toaster.pop('success', '', 'Mensalidade Excluída com Sucesso!');
                    $.each(vm.mensalidades, function (i) {
                        if (vm.mensalidades[i].MensalidadesId === id) {
                            vm.mensalidades.splice(i, 1);
                            return false;
                        }
                    });
                    $scope.tableParams.reload();
                }), function (data) {
                    
                };
            });
        }

        $scope.formEdit = {
            submit: function (form, mensalidade) {
                var firstError = null;
                if (form.$invalid) {

                    var field = null, firstError = null;
                    for (field in form) {
                        if (field[0] != '$') {
                            if (firstError === null && !form[field].$valid) {
                                firstError = form[field].$name;
                            }

                            if (form[field].$pristine) {
                                form[field].$dirty = true;
                            }
                        }
                    }
                    angular.element('.ng-invalid[name=' + firstError + ']').focus();                    
                    return;

                } else {
                    // Cadastra/Atualiza mensalidade
                    mensalidade.ClienteId = ClienteId;
                    mensalidade.ValorPago = mensalidade.ValorPago.toString().replace(",", ".");
                    mensalidadesData.editMensalidade(mensalidade).success(function () {
                        toaster.pop('success', '', 'Mensalidade Atualizada com Sucesso!');
                        $scope.editId = -1;
                    }).error(function (error) {
                        var errors = [];
                        for (var key in error.ModelState) {
                            for (var i = 0; i < error.ModelState[key].length; i++) {
                                errors.push(error.ModelState[key][i]);
                                toaster.pop('error', '', error.ModelState[key][i]);
                            }
                        }
                        vm.errors = errors;
                    });
                }

            }
        };

        $scope.editId = -1;

        $scope.setEditId = function (pid) {
            $scope.editId = pid;
        };

        /**
         * /
         * @returns {DatePicker} 
         */
        $scope.today = function () {
            $scope.DataPagamento = new Date();
        };
        $scope.today();


        $scope.clear = function () {
            $scope.DataPagamento = null;
        };

        $scope.toggleMin = function () {
            $scope.minDate = ($scope.minDate) ? null : new Date();
        };
        $scope.toggleMin();

        $scope.opened = [];
        $scope.open = function ($event, index) {
            $event.preventDefault();
            $event.stopPropagation();
            $timeout(function () {
                $scope.opened[index] = true;
            });
        };

        $scope.dateOptions = {
            'year-format': "'yyyy'",
            'starting-day': 1
        };
    }

}());