(function() {
    "use strict";

    app.controller('createReceitasAvaliacaoFisicaCtrl', createReceitasAvaliacaoFisicaCtrl);

    function createReceitasAvaliacaoFisicaCtrl($scope, Cliente, $modalInstance, receitasAvaliacaoFisicaData) {
        var vm = this;
        vm.mensalidades = [];

        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        };


        $scope.form = {

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
                    avaliacaoFisica.ClienteId = Cliente.ClienteId;
                    avaliacaoFisica.Valor = avaliacaoFisica.Valor.toString().replace(",", ".");
                    receitasAvaliacaoFisicaData.addReceitaAvaliacaoFisica(avaliacaoFisica).success(function () {
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