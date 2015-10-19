(function () {
    "use strict";

    app.controller('filaEsperaCtrl', filaEsperaCtrl);

    function filaEsperaCtrl($scope, ngTableParams, filaEsperaData, SweetAlert, $filter, $timeout, $state) {
        var vm = this;
        vm.fila = [];
        $scope.createFila = false;
       
        

        activate();

        function activate() {
            $scope.$emit('LOAD');

            //if existe sessioStorage fila
            if (window.sessionStorage.getItem('fila') != null)
                window.sessionStorage.removeItem('fila');

            filaEsperaData.getFila().then(function (result) {
                vm.fila = result.data;
                loadTable(vm.fila);
                $scope.$emit('UNLOAD');
            });
        }

        function loadTable(data) {
            $scope.tableParams = new ngTableParams({
                page: 1, // show first page
                count: 20, // count per page
                sorting: {
                    DataReserva: 'asc' // initial sorting
                }
            }, {
                total: data.length, // length of data
                getData: function ($defer, params) {
                    // use build-in angular filter
                    var orderedData = params.sorting() ? $filter('orderBy')(data, params.orderBy()) : data;
                    $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                }
            });

            $scope.$watch('vm.fila', function () {
                $scope.tableParams.reload();
            });
        }

        $scope.save = function(fila) {
            filaEsperaData.editFila(fila).then(function() {
                SweetAlert.swal("Atualizado!", "Dados salvos com sucesso!", "success");
                $scope.editId = -1;
            });
        }

        $scope.cadastrar = function (p) {
            window.sessionStorage.setItem('fila', JSON.stringify(p));
            $state.go('app.clientes.cadastrar');
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

                    filaEsperaData.deleteFila(id).then(function () {
                        SweetAlert.swal("Excluído!", "Dados apgados com sucesso!", "success");
                        $.each(vm.fila, function (i) {
                            if (vm.fila[i].FilaEsperaId === id) {
                                vm.fila.splice(i, 1);
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

        //Format Data Atual
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1;
        var yyyy = today.getFullYear();

        if (dd < 10) {
            dd = '0' + dd
        }

        if (mm < 10) {
            mm = '0' + mm
        }

        today =  dd + '/' + mm + '/' + yyyy;


        $scope.fila = {
            DataReserva: today,
            Nome: null,
            Telefone: "",
            Celular: "",
            Email: null
        };

        $scope.cancelCreate = function (form) {
            $scope.createFila = false;
            $scope.fila = {
                DataReserva: new Date(),
                Nome: null,
                Telefone: "",
                Celular: "",
                Email: null
            };
            form.$setPristine(true);
        }

        $scope.form = {

            submit: function (form, fila) {
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
                    if (fila.DataReserva === today)
                        fila.DataReserva = new Date();
                    filaEsperaData.addFila(fila).success(function (fila) {
                        //Limpa o formulario
                        form.$setPristine(true);
                        $scope.fila = {
                            DataReserva: today,
                            Nome: null,
                            Telefone: "",
                            Celular: "",
                            Email: null,
                            Hora: ""
                        };
                        SweetAlert.swal("Cadastrado!", "Dados cadastrado com sucesso!", "success");
                        $scope.createFila = false;
                        vm.fila.push(fila);
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

            submit: function (form, fila) {
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
                    filaEsperaData.editFila(fila).success(function () {
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

        $scope.today = function () {
            $scope.DataReserva = new Date();
        };
        $scope.today();


        $scope.clear = function () {
            $scope.DataReserva = null;
        };

        // Disable weekend selection
        //$scope.disabled = function (date, mode) {
        //    return (mode === 'day' && (date.getDay() === 0 || date.getDay() === 7));
        //};

        $scope.toggleMin = function () {
            $scope.minDate = ($scope.minDate) ? null : new Date();
        };
        $scope.toggleMin();

        $scope.opened = [];
        $scope.open = function (index) {
            $timeout(function () {
                $scope.opened[index] = true;
            });
        };

        $scope.dateOptions = {
            'year-format': "'yyyy'",
            'starting-day': 1
        };
    };
})();