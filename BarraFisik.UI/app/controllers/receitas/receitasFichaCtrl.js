(function() {
    "use strict";

    app.controller('receitasFichaCtrl', receitasFichaCtrl);

    function receitasFichaCtrl($scope, $modalInstance, receita, funcionariosData, clienteData, subCategoriaData) {
        var vm = this;
        
        if (receita.FuncionarioId != null)
        {
            funcionariosData.getById(receita.FuncionarioId).then(function (funcionario) {                
                receita.Funcionarios = funcionario.data;
            })
        }

        if (receita.ClienteId != null) {
            clienteData.getById(receita.ClienteId).then(function (cliente) {
                receita.Cliente = cliente.data;
            })
        }

        if (receita.SubCategoriaFinanceiraId != null) {
            subCategoriaData.getById(receita.SubCategoriaFinanceiraId).then(function (subcategoria) {
                receita.SubCategoriaFinanceira = subcategoria.data;
            })
        }

        $scope.receita = receita;

        vm.cancel = function() {
            $modalInstance.dismiss('cancel');
        }         
        
    }

}());