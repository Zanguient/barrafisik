(function() {
    "use strict";

    app.controller('funcionariosFichaCtrl', funcionariosFichaCtrl);

    function funcionariosFichaCtrl($scope, $modalInstance, funcionario) {
        var vm = this;
        $scope.Idade = null;
        if (funcionario.DataNascimento != null) {
            //Convert Data Nascimento
            var dtNascimento = funcionario.DataNascimento.toString().substring(0, 10);
            dtNascimento = new Date(dtNascimento);
            $scope.Idade = calcAge(dtNascimento);           
        }
        $scope.funcionario = funcionario;

        vm.cancel = function() {
            $modalInstance.dismiss('cancel');
        }

        function calcAge(dateString) {
            var birthday = +new Date(dateString);
            return ~~((Date.now() - birthday) / (31557600000));
        }     
        
    }

}());