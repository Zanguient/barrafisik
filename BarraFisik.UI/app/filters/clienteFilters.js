
app.filter('cpf', function () {
    return function (cpf) {
        return cpf.substr(0, 3) + '.' + cpf.substr(3, 3) + '.' + cpf.substr(6, 3) + '-' + cpf.substr(9, 2);
    };
});

app.filter('telefone', function () {
    return function (telefone) {
        if(telefone != null)
            return telefone.substr(0, 4) + '-' + telefone.substr(4, 4);
    };
});