(function () {
    'use strict';

    angular.module('app').service('listCategorias', listCategorias);

    listCategorias.$inject = ['$http', 'apiUrl', 'categoriaFinanceiraData'];

    //this.categorias = [
    //    { id: 'c60d01a4-8215-4955-8d5e-0447907317ba', title: 'Despesas 01' },
    //    { id: '58e35cbd-d2a2-43e1-b194-8ab72fe54df8', title: 'Despesas 02' },
    //    { id: 'be05ab32-ae04-4ddf-a396-97c92f84b070', title: 'Despesas 03' }
    //];

    function listCategorias() {
        return [
        { id: 'c60d01a4-8215-4955-8d5e-0447907317ba', title: 'Despesas 01' },
        { id: '58e35cbd-d2a2-43e1-b194-8ab72fe54df8', title: 'Despesas 02' },
        { id: 'be05ab32-ae04-4ddf-a396-97c92f84b070', title: 'Despesas 03' }
    ];
        //this.getCategoriasDespesas = function() {
        //    //categoriaFinanceiraData.getCategoriaByTipo("Despesas").then(function (cat) {

        //    //    $scope.categorias = [{ id: 0, title: "" }];
        //    //    angular.forEach(cat.data, function (value, key) {
        //    //        $scope.categorias.push({ id: value.CategoriaFinanceiraId, title: value.Categoria });
        //    //    });

        //    //});
        //    return [
        //    { id: 'c60d01a4-8215-4955-8d5e-0447907317ba', title: 'Despesas 01' },
        //    { id: '58e35cbd-d2a2-43e1-b194-8ab72fe54df8', title: 'Despesas 02' },
        //    { id: 'be05ab32-ae04-4ddf-a396-97c92f84b070', title: 'Despesas 03' }
        //    ];
        //}
        
    };


});