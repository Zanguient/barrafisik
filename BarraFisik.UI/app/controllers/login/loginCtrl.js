(function() {
    "use strict";

    app.controller('loginCtrl', loginCtrl);

    function loginCtrl($scope, $rootScope, authService, $location) {
        
        $scope.login = function () {
            $scope.loadingLogin = true;
            authService.login($scope.loginData).then(function (response) {
                $location.path('/app/dashboard');
            },
             function (err) {
                 $scope.authError = err.error_description;
                 $scope.loginData = {
                     userName: "",
                     password: ""
                 };
                 //toastr.error(err.error_description);
             })['finally'](function () {
                 $scope.loadingLogin = false;
             });
        };
    }
}());