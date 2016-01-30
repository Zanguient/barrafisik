(function () {
    "use strict";

    app.controller('armazemCtrl', armazemCtrl);

    function armazemCtrl($scope, ngTableParams, armazemData, SweetAlert, $filter, $timeout, $state) {
        var vm = this;
        vm.armazem = [];
        $scope.createArmazem = false;

        activate();

        function activate() {
            $scope.$emit('LOAD');

            armazemData.getArmazem().then(function (result) {
                vm.armazem = result.data;
                $scope.$emit('UNLOAD');
            });
        }

        $scope.tableParams = new ngTableParams({
            page: 1, // show first page
            count: 40, // count per page
            sorting: {
                Descricao: 'asc' // initial sorting
            }
        }, {
            counts: [],
            total: vm.armazem.length, // length of data
            getData: function ($defer, params) {
                // use build-in angular filter
                var orderedData = params.sorting() ? $filter('orderBy')(vm.armazem, params.orderBy()) : vm.armazem;
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });

        $scope.$watch('vm.armazem', function () {
            $scope.tableParams.reload();
        });


        $scope.save = function (armazem) {
            armazemData.editArmazem(armazem).then(function () {
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

                    armazemData.deleteArmazem(id).then(function () {
                        SweetAlert.swal("Excluído!", "Dados apgados com sucesso!", "success");
                        $.each(vm.armazem, function (i) {
                            if (vm.armazem[i].ArmazemId === id) {
                                vm.armazem.splice(i, 1);
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

        $scope.armazem = {
            Descricao: null,
        };

        $scope.cancelCreate = function (form) {
            $scope.createArmazem = false;
            $scope.Armazem = {
                Descricao: null
            };
            form.$setPristine(true);
        }

        $scope.form = {

            submit: function (form, armazem) {
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
                    armazemData.addArmazem(armazem).success(function (armazem) {
                        //Limpa o formulario
                        form.$setPristine(true);
                        $scope.armazem = {
                            Descricao: null,
                        };
                        SweetAlert.swal("Cadastrado!", "Dados cadastrado com sucesso!", "success");
                        $scope.createArmazem = false;
                        vm.armazem.push(armazem);
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

            submit: function (form, armazem) {
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
                    armazemData.editArmazem(armazem).success(function () {
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