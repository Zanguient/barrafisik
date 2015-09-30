(function () {
    'use strict';

    app.controller('alterarSenhaCtrl', alterarSenhaCtrl);

    function alterarSenhaCtrl(accountsData, $modalInstance) {
        var vm = this;

        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        };


        vm.alterarSenha = function (usuario) {
            accountsData.alterarSenha(usuario).success(function (data) {                
                $modalInstance.close(data);
            }).error(function (error) {
                var errors = [];
                for (var key in error.ModelState) {
                    for (var i = 0; i < error.ModelState[key].length; i++) {
                        errors.push(error.ModelState[key][i]);
                    }
                }
                vm.errors = errors;
            });

        };
    }
})();
