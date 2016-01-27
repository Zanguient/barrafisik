(function() {
    "use strict";

    app.controller('funcionariosCreateCtrl', funcionariosCreateCtrl);

    function funcionariosCreateCtrl($scope, $modalInstance, funcionariosData) {
        var vm = this;

        vm.cancel = function() {
            $modalInstance.dismiss('cancel');
        }

        $scope.funcionario = {
            isAtivo: true
        }

        $scope.form = {

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
                    funcionariosData.add(funcionario).success(function () {
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