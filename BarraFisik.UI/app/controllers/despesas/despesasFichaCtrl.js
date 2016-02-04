(function() {
    "use strict";

    app.controller('despesasFichaCtrl', despesasFichaCtrl);

    function despesasFichaCtrl($scope, $modalInstance, despesa, funcionariosData, fornecedoresData, subCategoriaData) {
        var vm = this;
        
        if (despesa.FuncionarioId != null)
        {
            funcionariosData.getById(despesa.FuncionarioId).then(function (funcionario) {                
                despesa.Funcionarios = funcionario.data;
            })
        }

        if (despesa.FornecedorId != null) {
            fornecedoresData.getById(despesa.FornecedorId).then(function (fornecedor) {
                despesa.Fornecedores = fornecedor.data;
            })
        }

        if (despesa.SubCategoriaFinanceiraId != null) {
            subCategoriaData.getById(despesa.SubCategoriaFinanceiraId).then(function (subcategoria) {
                despesa.SubCategoriaFinanceira = subcategoria.data;
            })
        }

        $scope.despesa = despesa;

        vm.cancel = function() {
            $modalInstance.dismiss('cancel');
        }         
        
    }

}());