
app.filter('cpf', function () {
    return function (cpf) {
        if(cpf != null)
            return cpf.substr(0, 3) + "." + cpf.substr(3, 3) + "." + cpf.substr(6, 3) + "-" + cpf.substr(9, 2);
    };
});

app.filter('telefone', function () {
    return function (telefone) {
        if(telefone != null)
            return telefone.substr(0, 4) + '-' + telefone.substr(4, 4);
    };
});

app.filter('celular', function () {
    return function (celular) {
        if (celular != null)
            if (celular.length === 10) {
                return '('+celular.substr(0, 2)+') '+ celular.substr(2, 4) + '-' + celular.substr(6, 4);
            } else {
                return '('+celular.substr(0, 2)+') ' + celular.substr(2, 5) + '-' + celular.substr(7, 4);
            }
            
    };
});