(function () {
    "use strict";

    app.controller('valoresCtrl', valoresCtrl);

    function valoresCtrl($scope, ngTableParams, valoresData, SweetAlert, $filter, $timeout, $state) {
        var vm = this;
        vm.valores = [];
        $scope.createValor = false;

        activate();

        function activate() {
            valoresData.getValores().then(function (result) {
                vm.valores = result.data;
            });
        }

        $scope.tableParams = new ngTableParams({
            page: 1, // show first page
            count: 20, // count per page
            sorting: {
                HorarioInicio: 'asc', // initial sorting
                QtdDias: 'asc'
            }
        }, {
            counts: [],
            total: vm.valores.length, // length of data
            getData: function ($defer, params) {
                // use build-in angular filter
                var orderedData = params.sorting() ? $filter('orderBy')(vm.valores, params.orderBy()) : vm.valores;
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });

        $scope.$watch('vm.valores', function () {
            $scope.tableParams.reload();
        });


        $scope.editId = -1;

        $scope.setEditId = function (pid) {
            $scope.editId = pid;
        };

        $scope.delete = function (id) {
            SweetAlert.swal({
                title: "Confirmar Exclusão",
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

                    valoresData.deleteValor(id).then(function () {
                        SweetAlert.swal("Excluído!", "Dados apgados com sucesso!", "success");
                        $.each(vm.valores, function (i) {
                            if (vm.valores[i].ValoresId === id) {
                                vm.valores.splice(i, 1);
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

        $scope.valor = {
            QtdDias: null,
            Valor: null,
            HorarioInicio: null,
            HorarioFim: null
        };

        $scope.cancelCreate = function (form) {
            $scope.createValor = false;
            $scope.valor = {
                QtdDias: null,
                Valor: null,
                HorarioInicio: null,
                HorarioFim: null
            };
            form.$setPristine(true);
        }

        $scope.form = {

            submit: function (form, valor) {
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
                    // Cadastra o valor
                    valoresData.addValor(valor).success(function (valor) {
                        //Limpa o formulario
                        form.$setPristine(true);
                        $scope.valor = {
                            QtdDias: null,
                            Valor: null,
                            HorarioInicio: null,
                            HorarioFim: null
                        };
                        SweetAlert.swal("Cadastrado!", "Valor cadastrado com sucesso!", "success");
                        $scope.createValor = false;
                        vm.valores.push(valor);
                        $scope.tableParams.reload();
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

            submit: function (form, valor) {
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
                    // Cadastra o fila
                    valoresData.editValor(valor).success(function () {
                        SweetAlert.swal("Atualizado!", "Valor atualizado com sucesso!", "success");
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
    };
})();