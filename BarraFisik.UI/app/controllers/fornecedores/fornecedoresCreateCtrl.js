(function() {
    "use strict";

    app.controller('fornecedoresCreateCtrl', fornecedoresCreateCtrl);

    function fornecedoresCreateCtrl($scope, $modalInstance, fornecedoresData) {
        var vm = this;
        $scope.isCpf = true;
       
        vm.cancel = function() {
            $modalInstance.dismiss('cancel');
        }

        $scope.cpfcnpj = '999.999.999-99';

        //alterna mask CPF e CNPJ
        $scope.cpfmask = function() {
            $scope.cpfcnpj = '999.999.999-99';
            $scope.isCpf = true;
        }
        $scope.cnpjmask = function () {
            $scope.cpfcnpj = '99.999.999/9999-99';
            $scope.isCpf = false;
        }


        $scope.fornecedor = {
            isAtivo: true
        }

        $scope.form = {

            submit: function (form, fornecedor) {
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
                    fornecedoresData.add(fornecedor).success(function () {
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