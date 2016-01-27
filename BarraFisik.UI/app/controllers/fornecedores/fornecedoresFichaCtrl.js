(function() {
    "use strict";

    app.controller('fornecedoresFichaCtrl', fornecedoresFichaCtrl);

    function fornecedoresFichaCtrl($scope, $modalInstance, fornecedor) {
        var vm = this;
        
        $scope.fornecedor = fornecedor;

        vm.cancel = function() {
            $modalInstance.dismiss('cancel');
        }         
        
    }

}());