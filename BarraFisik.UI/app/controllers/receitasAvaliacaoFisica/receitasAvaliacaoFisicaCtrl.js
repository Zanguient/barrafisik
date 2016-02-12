(function () {
    "use strict";

    app.controller("receitasAvaliacaoFisicaCtrl", receitasAvaliacaoFisicaCtrl);

    function receitasAvaliacaoFisicaCtrl($scope, Cliente, modalService, $modalInstance, tipoPagamentoData, receitasAvaliacaoFisicaData, ngTableParams, $filter, $timeout, SweetAlert, toaster, receitasData) {
        var vm = this;
        vm.avaliacoes = [];

        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        receitasData.getAvaliacaoCliente(Cliente.ClienteId).then(function (result) {
            vm.avaliacoes = result.data;                        
        });

        $scope.tableParams = new ngTableParams({
            page: 1, // show first page
            count: 20, // count per page
            sorting: {
                DataPagamento: 'desc' // initial sorting
            }
        }, {
            counts: [],
            total: vm.avaliacoes.length, // length of data
            getData: function ($defer, params) {
                var orderedData = params.sorting() ? $filter('orderBy')(vm.avaliacoes, params.orderBy()) : vm.avaliacoes;
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });

        //List Tipos de Pagamento
        tipoPagamentoData.getTipos().then(function (tipos) {
            $scope.tipos = tipos.data;
        });

        $scope.$watch('vm.avaliacoes', function () {
            $scope.tableParams.reload();
        });


        //Comprovante
        $scope.openComprovante = function (r) {
            if (r.DataPagamento == null) {
                toaster.pop('error', '', 'Mensalidade Pendente!');
            } else {
                window.open("http://localhost:49000/app/views/receitasAvaliacaoFisica/comprovante.html?valor=" + r.Valor +
                    "&cliente=" + Cliente.Nome, "Recibo - Avaliação Física", "height=250,width=370");
            }
        }

        vm.delete = function (id) {
            var modalOptions = {
                closeButtonText: 'Cancelar',
                actionButtonText: 'Excluir',
                headerText: 'Excluir ?',
                bodyText: 'Tem certeza que deseja exlcuir este Pagamento?'
            };

            modalService.showModal({}, modalOptions).then(function () {
                receitasAvaliacaoFisicaData.deleteReceitaAvaliacao(id).then(function () {
                    toaster.pop('success', '', 'Dado Excluído com Sucesso!');
                    $.each(vm.avaliacoes, function (i) {
                        if (vm.avaliacoes[i].ReceitasAvaliacaoFisicaId === id) {
                            vm.avaliacoes.splice(i, 1);
                            return false;
                        }
                    });
                    $scope.tableParams.reload();
                }), function (data) {

                };
            });
        }

        $scope.formEdit = {
            submit: function (form, avaliacaoFisica) {
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
                    //Situação
                    if (avaliacaoFisica.DataPagamento != null) {
                        avaliacaoFisica.Situacao = "Quitado";
                    } else avaliacaoFisica.Situacao = "Pendente";

                    // Cadastra/Atualiza mensalidade
                    avaliacaoFisica.ClienteId = Cliente.ClienteId;
                    avaliacaoFisica.ValorTotal = avaliacaoFisica.Valor;
                    receitasData.editReceita(avaliacaoFisica).success(function () {
                        toaster.pop('success', '', 'Dado Atualizado com Sucesso!');
                        receitasData.getAvaliacaoCliente(Cliente.ClienteId).then(function (result) {
                            vm.avaliacoes = result.data;
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

    };

}());