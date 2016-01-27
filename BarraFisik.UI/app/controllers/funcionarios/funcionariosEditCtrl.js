(function() {
    "use strict";

    app.controller('funcionariosEditCtrl', funcionariosEditCtrl);

    function funcionariosEditCtrl($scope, $modalInstance, funcionariosData, funcionario) {
        var vm = this;

        if (funcionario.DataNascimento !== null)
        {
            //Convert Data Nascimento
            var dtNascimento = funcionario.DataNascimento.toString().substring(0, 10);
            dtNascimento = new Date(dtNascimento);
            funcionario.DataNascimento = new Date(dtNascimento.getTime() + dtNascimento.getTimezoneOffset() * 60000);
        }

        $scope.funcionario = funcionario;

        vm.cancel = function() {
            $modalInstance.dismiss('cancel');
        }

        $scope.formEdit = {
            submit: function (form, funcionario) {
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
                    funcionariosData.edit(funcionario).success(function () {
                        $modalInstance.close();
                    }).error(function (error) {
                        var errors = [];
                        for (var key in error.ModelState) {
                            for (var i = 0; i < error.ModelState[key].length; i++) {
                                errors.push(error.ModelState[key][i]);
                            }
                        }
                        vm.errors = errors;
                    });
                }

            }
        };
    }

}());