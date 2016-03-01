(function() {
    "use strict";

    app.controller('fornecedoresEditCtrl', fornecedoresEditCtrl);

    function fornecedoresEditCtrl($scope, $modalInstance, fornecedoresData, fornecedor) {
        var vm = this;
        $scope.fornecedor = fornecedor;
       
        if (fornecedor.CpfCnpj != null) {
            if (fornecedor.CpfCnpj.length > 11) {
                $scope.cpfcnpj = "99.999.999/9999-99";
                $scope.cpfcheked = "CPF";
            } else {
                $scope.cpfcnpj = '999.999.999-99';
                $scope.cnpjcheked = "CNPJ";
            }
        }

        //alterna mask CPF e CNPJ
        $scope.cpfmask = function () {
            $scope.cpfcnpj = '999.999.999-99';
        }
        $scope.cnpjmask = function () {
            $scope.cpfcnpj = '99.999.999/9999-99';
        }

        vm.cancel = function() {
            $modalInstance.dismiss('cancel');
        }

        $scope.formEdit = {
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
                    fornecedoresData.edit(fornecedor).success(function () {
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