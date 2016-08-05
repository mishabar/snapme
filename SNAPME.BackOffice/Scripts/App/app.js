var app = angular.module('IISNAP', ['ngRoute', 'ngMessages', 'ngMaterial', 'ngSanitize', 'ngCookies', 'ui.validate', 'ui.tinymce', 'lfNgMdFileInput', 'AccountService', 'ProductsService', 'SalesService']);
app.config(function ($mdThemingProvider) {
    $mdThemingProvider.theme('default')
        .primaryPalette('blue')
        .accentPalette('amber')
});

app.config(function ($routeProvider, $locationProvider) {
    $routeProvider.caseInsensitiveMatch = true;

    $routeProvider
        .when('/', { templateUrl: '/Home/Default', controller: 'HomeController' })
        .when('/GetStarted', { templateUrl: '/Home/GetStarted', controller: 'GetStartedController' })
        .when('/Products', { templateUrl: function (params) { return '/Products?_ts=' + (new Date()).getTime() }, controller: 'ProductsController' })
        .when('/Product/:productId', { templateUrl: function (params) { return '/Product/' + params.productId }, controller: 'ProductController' })
        .when('/Sales/Live', { templateUrl: '/Sales/Live', controller: 'LiveSalesController' })
        .when('/Sales/Schedule', { templateUrl: '/Sales/Schedule', controller: 'ProductsController' })
        .when('/Sales/Ended', { templateUrl: '/Sales/Ended', controller: 'EndedSalesController' })
        .when('/Snapbox', { templateUrl: function (params) { return '/Snapbox?_ts=' + (new Date()).getTime() }, controller: 'SnapboxController' })
        .when('/Settings', { templateUrl: function (params) { return '/Settings?_ts=' + (new Date()).getTime() }, controller: 'SettingsController' });

    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
});

app.controller('MainController', function ($scope, $mdDialog, $cookies, $timeout, $mdSidenav, $location, accountService, productsService) {
    $scope.tinymceOptions = {
        trusted: true,
        plugins: 'image table',
        skin: 'lightgray',
        theme: 'modern',
        height: 530
    };

    $scope.openMenu = function () {
        $mdSidenav('sidenav-left').toggle();
    }

    $scope.logout = function () {
        $('#frmLogout').submit();
    }

    $scope.scheduleSale = function (product, ev) {
        $mdDialog.show({
            controller: 'ScheduleSaleDialogController',
            templateUrl: '/Modals/ScheduleSale',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose:false,
            fullscreen: true,
            locals: { product: product}
        })
    .then(function(answer) {
        $scope.status = 'You said the information was "' + answer + '".';
    }, function() {
        $scope.status = 'You cancelled the dialog.';
    });
    }

    if ($cookies.get('firstTime')) {
        $cookies.remove('firstTime');
        $location.path('/Settings');
    }
});

app.filter('trustAsResourceUrl', ['$sce', function ($sce) {
    return function (val) {
        return $sce.trustAsResourceUrl(val);
    };
}]);