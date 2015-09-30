(function () {
    "use sctrict";
    app.controller('accountCtrl', accountCtrl);

    function accountCtrl($scope, ngTableParams, accountsData, $filter, SweetAlert, $state) {
        var vm = this;
        vm.users = [];
        $scope.createUser = false;

        activate();

        function activate() {
            accountsData.getUsers().then(function (result) {
                vm.users = result.data;
            });
        }

        $scope.editId = -1;

        $scope.setEditId = function (pid) {
            $scope.editId = pid;
        };

        $scope.tableParams = new ngTableParams({
            count: 50,
            filter: $scope.filter,
            sorting: {
                FullName: "asc"
            }
        }, {
            counts: [],
            total: vm.users.length, // length of data
            getData: function ($defer, params) {
                // use build-in angular filter                   
                var orderedData = params.sorting() ? $filter('orderBy')(vm.users, params.orderBy()) : vm.users;
                orderedData = $filter('filter')(orderedData, params.filter());

                params.total(orderedData.length);
                $scope.total = orderedData.length;

                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });

        $scope.$watch("vm.users", function () {
            $scope.tableParams.reload();
        });

        $scope.user = {
            UserName: null,
            Nome: null,
            Email: null,
            Password: null,
            ConfirmPassword: null
        };

        vm.addRole = function (Id, Role) {
            SweetAlert.swal({
                title: "Adicionar Permissão",
                text: "Tem certeza que deseja que este usuário tenha privilégios de  " + Role + "?",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Confirmar",
                cancelButtonText: "Cancelar",
                closeOnConfirm: false,
                closeOnCancel: false
            }, function (isConfirm) {
                if (isConfirm) {
                    accountsData.addRole(Id, Role).then(function () {
                        SweetAlert.swal({
                            title: "Permissão!",
                            text: "Permissão adicionada com sucesso.",
                            type: "success",
                            confirmButtonColor: "#007AFF"
                        });
                        $state.go($state.current, {}, { reload: true });
                    }), function (data) {
                        SweetAlert.swal({
                            title: "Erro!",
                            text: "Erro ao adicionar permissão.",
                            type: "success",
                            confirmButtonColor: "#007AFF"
                        });
                        console.log(data.message, "Erro ao adicionar permissão!");
                    };
                } else {
                    SweetAlert.swal({
                        title: "Cancelado",
                        text: "Operação de permissão cancelada",
                        type: "error",
                        confirmButtonColor: "#007AFF"
                    });
                }
            });
        };

        vm.removeRole = function (Id, Role) {
            SweetAlert.swal({
                title: "Remover Permissão",
                text: "Tem certeza que deseja remover privilégios de  " + Role + "?",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Confirmar",
                cancelButtonText: "Cancelar",
                closeOnConfirm: false,
                closeOnCancel: false
            }, function (isConfirm) {
                if (isConfirm) {                   
                    accountsData.removeRole(Id, Role).then(function () {
                        SweetAlert.swal({
                            title: "Permissão!",
                            text: "Permissão removida com sucesso.",
                            type: "success",
                            confirmButtonColor: "#007AFF"
                        });
                        $state.go($state.current, {}, { reload: true });
                    }), function (data) {
                        SweetAlert.swal({
                            title: "Erro!",
                            text: "Erro ao remover permissão.",
                            type: "error",
                            confirmButtonColor: "#007AFF"
                        });
                        console.log(data.message, "Erro ao remover permissão!");
                    };
                } else {
                    SweetAlert.swal({
                        title: "Cancelado",
                        text: "Operação de permissão cancelada",
                        type: "error",
                        confirmButtonColor: "#007AFF"
                    });
                }
            });
        };

        $scope.cancelCreate = function (form) {
            $scope.createUser = false;
            $scope.user = {
                UserName: null,
                Nome: null,
                Email: null,
                Password: null,
                ConfirmPassword: null
            };
            form.$setPristine(true);
        }

        $scope.delete = function(id) {
            SweetAlert.swal({
                title: "Confirmar Exclusão?",
                text: "Tem certeza que deseja excluir esse usuário?",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Confirmar",
                cancelButtonText: "Cancelar",
                closeOnConfirm: false,
                closeOnCancel: false
            }, function(isConfirm) {
                if (isConfirm) {
                    SweetAlert.swal({
                        title: "Excluído!",
                        text: "Registro excluído com sucesso.",
                        type: "success",
                        confirmButtonColor: "#007AFF"
                    });

                    accountsData.deleteUser(id).then(function() {
                        SweetAlert.swal("Excluído!", "Dados apgados com sucesso!", "success");
                        $.each(vm.users, function(i) {
                            if (vm.users[i].Id === id) {
                                vm.users.splice(i, 1);
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
        };

        $scope.resetPassword = function (user) {
            SweetAlert.swal({
                title: "Redefinir Senha?",
                text: "A nova senha para este usuário será: 123456, Confirmar Execução?",
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
                        title: "Alterado!",
                        text: "Senha redefinida com sucesso.",
                        type: "success",
                        confirmButtonColor: "#007AFF"
                    });

                    accountsData.resetPassword(user.Id).then(function () {
                    }), function (data) {
                        console.log(data.message, "Erro ao resetar senha!");
                    };                   
                } else {
                    SweetAlert.swal({
                        title: "Cancelado",
                        text: "Operação de redefinição de senha cancelada",
                        type: "error",
                        confirmButtonColor: "#007AFF"
                    });
                }
            });           
        };

        $scope.form = {

            submit: function (form, user) {
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
                    accountsData.addUser(user).success(function (user) {
                        //Limpa o formulario
                        $scope.createUser = false;
                        $scope.user = {
                            UserName: null,
                            Nome: null,
                            Email: null,
                            Password: null,
                            ConfirmPassword: null
                        };
                        form.$setPristine(true);
                        vm.users.push(user);
                        SweetAlert.swal("Cadastrado!", "Usuário cadastrado com sucesso!", "success");
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

            submit: function (form, user) {
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
                    // Salva o usuario
                    user.Name = user.FullName;
                    accountsData.editUser(user).success(function (user) {             
                        SweetAlert.swal("Atualizado!", "Usuário atualizado com sucesso!", "success");
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
    }
})();
