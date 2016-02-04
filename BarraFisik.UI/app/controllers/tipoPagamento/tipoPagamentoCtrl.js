(function () {
    "use strict";

    app.controller('tipoPagamentoCtrl', tipoPagamentoCtrl);

    function tipoPagamentoCtrl($scope, ngTableParams, tipoPagamentoData, SweetAlert, $filter, $timeout, $state) {
        var vm = this;
        vm.tipos = [];
        $scope.createTipo = false;

        activate();

        function activate() {
            $scope.$emit('LOAD');

            tipoPagamentoData.getTipos().then(function (result) {
                vm.tipos = result.data;
            });

            $scope.$emit('UNLOAD');
        }

        $scope.tableParams = new ngTableParams({
            page: 1, // show first page
            count: 40, // count per page
            sorting: {
                Descricao: 'asc' // initial sorting
            }
        }, {
            counts: [],
            total: vm.tipos.length, // length of data
            getData: function ($defer, params) {
                // use build-in angular filter
                var orderedData = params.sorting() ? $filter('orderBy')(vm.tipos, params.orderBy()) : vm.tipos;
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });

        $scope.$watch('vm.tipos', function () {
            $scope.tableParams.reload();
        });


        $scope.save = function (tipo) {
            tipoPagamentoData.editTipo(tipo).then(function () {
                SweetAlert.swal("Atualizado!", "Dados salvos com sucesso!", "success");
                $scope.editId = -1;
            });
        }

        $scope.delete = function (id) {
            SweetAlert.swal({
                title: "Confirmar Exclusão?",
                text: "Tem certeza que deseja excluir esse registro?",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Confirmar",
                cancelButtonText: "Cancelar",
                closeOnConfirm: false,
                closeOnCancel: false
            }, function (isConfirm) {
                if (isConfirm) {
                    SweetAlert.swal({
                        title: "Excluído!",
                        text: "Registro excluído com sucesso.",
                        type: "success",
                        confirmButtonColor: "#007AFF"
                    });

                    tipoPagamentoData.deleteTipo(id).then(function () {
                        SweetAlert.swal("Excluído!", "Dados apgados com sucesso!", "success");
                        $.each(vm.tipos, function (i) {
                            if (vm.tipos[i].TipoPagamentoId === id) {
                                vm.tipos.splice(i, 1);
                                return false;
                            }
                        });
                        $scope.tableParams.reload();
                    });
                } else {
                    SweetAlert.swal({
                        title: "Cancelado",
                        text: "Operação de exclusão cancelada",
                        type: "error",
                        confirmButtonColor: "#007AFF"
                    });
                }
            });
        }


        $scope.tipo = {
            Sigla: null,
            Descricao: null,
        };

        $scope.cancelCreate = function (form) {
            $scope.createTipo = false;
            $scope.tipo = {
                Sigla: null,
                Descricao: null
            };
            form.$setPristine(true);
        }

        $scope.form = {

            submit: function (form, tipo) {
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
                    tipoPagamentoData.addTipo(tipo).success(function (tipo) {
                        //Limpa o formulario
                        form.$setPristine(true);
                        $scope.tipo = {
                            Sigla: null,
                            Descricao: null,
                        };
                        SweetAlert.swal("Cadastrado!", "Dados cadastrado com sucesso!", "success");
                        $scope.createTipo = false;
                        tipoPagamentoData.getTipos().then(function (result) {
                            vm.tipos = result.data;
                        });
                    }).error(function (error) {
                        var errors = [];
                        for (var key in error.ModelState) {
                            for (var i = 0; i < error.ModelState[key].length; i++) {
                                errors.push(error.ModelState[key][i]);
                                SweetAlert.swal("Erro ao cadastrar!", error.ModelState[key][i], "error");
                            }
                        }
                        vm.errors = errors;
                    });
                }

            }
        };

        $scope.formEdit = {

            submit: function (form, tipo) {
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
                    // Cadastra o tipo
                    tipoPagamentoData.editTipo(tipo).success(function () {
                        SweetAlert.swal("Atualizado!", "Dados salvos com sucesso!", "success");
                        $scope.editId = -1;
                    }).error(function (error) {
                        var errors = [];
                        for (var key in error.ModelState) {
                            for (var i = 0; i < error.ModelState[key].length; i++) {
                                errors.push(error.ModelState[key][i]);
                                SweetAlert.swal("Erro ao salvar!", error.ModelState[key][i], "error");
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
    };
})();