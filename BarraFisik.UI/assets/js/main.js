var app = angular.module('app', ['clip-two']);
app.run(['$rootScope', '$state', '$stateParams',
function ($rootScope, $state, $stateParams) {

    // Attach Fastclick for eliminating the 300ms delay between a physical tap and the firing of a click event on mobile browsers
    FastClick.attach(document.body);

    // UpdateClientes 1 vez a cada entrada no sistema
    $rootScope.updateClientes = false;

    // Set some reference to access them from any scope
    $rootScope.$state = $state;
    $rootScope.$stateParams = $stateParams;

    //Prevent Backspace
    $(document).on("keydown", function (e) {
        if (e.which === 8 && !$(e.target).is("input:not([readonly]):not([type=radio]):not([type=checkbox]), textarea, [contentEditable], [contentEditable=true]")) {
            e.preventDefault();
        }
    });

    // GLOBAL APP SCOPE
    // set below basic information
    $rootScope.app = {
        name: 'Clip-Two', // name of your project
        author: 'ClipTheme', // author's name or company name
        description: 'Angular Bootstrap Admin Template', // brief description
        version: '1.0', // current version
        year: ((new Date()).getFullYear()), // automatic current year (for copyright information)
        isMobile: (function () {// true if the browser is a mobile device
            var check = false;
            if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
                check = true;
            };
            return check;
        })(),
        layout: {
            isNavbarFixed: true, //true if you want to initialize the template with fixed header
            isSidebarFixed: true, // true if you want to initialize the template with fixed sidebar
            isSidebarClosed: false, // true if you want to initialize the template with closed sidebar
            isFooterFixed: false, // true if you want to initialize the template with fixed footer
            theme: 'theme-1', // indicate the theme chosen for your project
            logo: 'assets/images/logo.png', // relative path of the project logo
        }
    };

    //+1 por que o getMonth retorn valores de 0 a 11 (jan a dez)
    $rootScope.mesAtual = new Date().getMonth()+1;
    $rootScope.dataAtual = new Date();
    $rootScope.user = {
        name: 'Jefferson R. Shibuya',
        job: 'ng-Dev',
        picture: 'app/img/user/02.jpg'
    };
}]);
app.config(['cfpLoadingBarProvider',
function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeBar = true;
    cfpLoadingBarProvider.includeSpinner = false;

}]);
