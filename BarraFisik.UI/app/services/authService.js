'use strict';
app.factory('authService', [
    '$http', '$q', '$rootScope', 'apiUrl', '$window', function ($http, $q, $rootScope, apiUrl, $window) {

        var authServiceFactory = {};

        var _authentication = {
            id: null,
            isAuth: false,
            nome: "",
            userName: "",
            isAdmin: false,
            token: ""
        };

        var _user = {};

        var _saveRegistration = function (registration) {

            _logOut();

            return $http.post(apiUrl + 'api/account/register', registration).then(function (response) {
                return response;
            });

        };

        var _login = function (loginData) {

            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

            var deferred = $q.defer();

            $http.post(apiUrl + "security/token", data, { headers: { 'Content-Type': "application/x-www-form-urlencoded" } }).success(function (response) {

                var token = response.access_token;
                _authentication.token = token;
                //localStorage.setItem('authorizationData', JSON.stringify(response));
                //localStorage.setItem('authorizationData', response.access_token);

                $http.get(apiUrl + "api/accounts/user/" + loginData.userName, { headers: { Authorization: "Bearer " + token } }).then(function (user) {
                    //localStorage.setItem("user", JSON.stringify(user));
                    var usr = user.data;
                    if (usr.Roles.indexOf("Admin") !== -1) {
                        _authentication.isAdmin = true;
                    }
                    _authentication.nome = usr.FullName;
                    _authentication.isAuth = true;
                    _authentication.userName = loginData.userName;
                    _authentication.id = usr.Id;

                    localStorage.setItem('authorizationData', JSON.stringify(_authentication));

                    deferred.resolve(_authentication);
                });

            }).error(function (err, status) {
                _logOut();
                deferred.reject(err);
            });

            return deferred.promise;

        };

        var _logOut = function () {
            localStorage.removeItem("authorizationData");

            _authentication.isAuth = false;
            _authentication.userName = "";
            _authentication.isAdmin = false;
            _authentication.token = "";
        };

        var _fillAuthData = function () {

            var authData = localStorage.getItem('authorizationData');
            if (authData) {
                _authentication.isAuth = true;
                _authentication.userName = authData.userName;
            }

        };

        authServiceFactory.saveRegistration = _saveRegistration;
        authServiceFactory.login = _login;
        authServiceFactory.logOut = _logOut;
        authServiceFactory.fillAuthData = _fillAuthData;
        authServiceFactory.authentication = _authentication;
        authServiceFactory.user = _user;

        return authServiceFactory;
    }]);