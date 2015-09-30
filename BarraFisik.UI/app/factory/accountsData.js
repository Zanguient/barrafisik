(function () {
    'use strict';

    app.factory('accountsData', accountsData);

    accountsData.$inject = ['$http', 'apiUrl'];


    function accountsData($http, apiUrl) {


        function getUsers() {
            return $http.get(apiUrl+'api/accounts/users');
        }

        function addUser(user) {
            return $http.post(apiUrl + 'api/accounts/create', user);
        }

        function editUser(user) {
            return $http.put(apiUrl + 'api/accounts/edit', user);
        }

        function deleteUser(id) {
            return $http.delete(apiUrl + 'api/accounts/user/' + id);
        }

        function resetPassword(id) {
            return $http.post(apiUrl + 'api/accounts/resetPassword/' + id);
        }

        function alterarSenha(user) {
            return $http.post(apiUrl + 'api/accounts/ChangePassword', user);
        }

        function addRole(id, role) {
            return $http.put(apiUrl + 'api/accounts/user/addRole/' + id + '/' + role);
        }

        function removeRole(id, role) {
            return $http.put(apiUrl + 'api/accounts/user/removeRole/' + id + '/' + role);
        }


        var service = {
            getUsers: getUsers,
            addUser: addUser,
            editUser: editUser,
            resetPassword: resetPassword,
            alterarSenha: alterarSenha,
            deleteUser: deleteUser,
            addRole: addRole,
            removeRole: removeRole
        };

        return service;

    };



})();