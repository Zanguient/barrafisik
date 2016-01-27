
app.filter('cpf', function () {
    return function (cpf) {
        if (cpf != null && cpf !== "")
            return cpf.substr(0, 3) + "." + cpf.substr(3, 3) + "." + cpf.substr(6, 3) + "-" + cpf.substr(9, 2);
    };
});

app.filter('cpfcnpj', function () {
    return function (cpfcnpj) {
        if (cpfcnpj != null && cpfcnpj !== "") {
            if (cpfcnpj.length > 11)
                return cpfcnpj.substr(0, 2) + "." + cpfcnpj.substr(2, 3) + "." + cpfcnpj.substr(5, 3) + "/" + cpfcnpj.substr(6, 4) + '-' + cpfcnpj.substr(8, 2);
            else
                return cpfcnpj.substr(0, 3) + "." + cpfcnpj.substr(3, 3) + "." + cpfcnpj.substr(6, 3) + "-" + cpfcnpj.substr(9, 2);
        }
        
    };
});

app.filter('telefone', function () {
    return function (telefone) {
        if(telefone != null && telefone !== "")
            return '('+telefone.substr(0, 2)+') '+ telefone.substr(2, 4) + '-' + telefone.substr(6, 4);
    };
});

app.filter('celular', function () {
    return function (celular) {
        if (celular != null && celular !== "")
            if (celular.length === 10) {
                return '('+celular.substr(0, 2)+') '+ celular.substr(2, 4) + '-' + celular.substr(6, 4);
            } else {
                return '('+celular.substr(0, 2)+') ' + celular.substr(2, 5) + '-' + celular.substr(7, 4);
            }
            
    };
});

app.filter('cep', function () {
    return function (cep) {
        if (cep != null && cep !== "")
            return cep.substr(0, 5) + '-' + cep.substr(5, 3);
    };
});

