(function () {
    "use strict";

    app.controller('mensalidadesCtrl', mensalidadesCtrl);

    function mensalidadesCtrl($scope, Cliente, $modal, modalService, $modalInstance, tipoPagamentoData, mensalidadesData, receitasData, ngTableParams, $filter, $timeout, SweetAlert, toaster) {
        var vm = this;
        vm.mensalidades = [];

        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        receitasData.getMensalidades(Cliente.ClienteId).then(function (result) {
            vm.mensalidades = result.data;
        });

        //List Tipos de Pagamento
        tipoPagamentoData.getTipos().then(function (tipos) {
            $scope.tipos = tipos.data;
        });

        $scope.tableParams = new ngTableParams({
            page: 1, // show first page
            count: 8, // count per page
            sorting: {
                DataPagamento: 'asc' // initial sorting
            }
        }, {
            counts: [],
            total: vm.mensalidades.length, // length of data
            getData: function ($defer, params) {
                // use build-in angular filter
                var orderedData = params.sorting() ? $filter('orderBy')(vm.mensalidades, params.orderBy()) : vm.mensalidades;
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });

        $scope.$watch('vm.mensalidades', function () {
            $scope.tableParams.reload();
        });

        //Comprovante
        $scope.openComprovante = function (m) {
            if (m.DataPagamento == null) {
                toaster.pop('error', '', 'Mensalidade Pendente!');
            } else {
                window.open("http://localhost:49000/app/views/mensalidades/comprovante.html?mes=" + m.MesReferencia +
                "&ano=" + m.AnoReferencia + "&valor=" + m.ValorTotal +
                "&cliente=" + Cliente.Nome,
                "minhaJanela", "height=250,width=370");
            }
        }

        vm.delete = function (id) {
            var modalOptions = {
                closeButtonText: 'Cancelar',
                actionButtonText: 'Excluir',
                headerText: 'Excluir ?',
                bodyText: 'Tem certeza que deseja exlcuir esta mensalidade?'
            };

            modalService.showModal({}, modalOptions).then(function () {
                receitasData.deleteMensalidade(id).then(function () {
                    toaster.pop('success', '', 'Mensalidade Excluída com Sucesso!');
                    $.each(vm.mensalidades, function (i) {
                        if (vm.mensalidades[i].ReceitasId === id) {
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
                    mensalidade.ClienteId = Cliente.ClienteId;
                    receitasData.editMensalidade(mensalidade).success(function () {
                        toaster.pop('success', '', 'Mensalidade Atualizada com Sucesso!');
                        receitasData.getMensalidades(Cliente.ClienteId).then(function (result) {
                            vm.mensalidades = result.data;
                        });
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