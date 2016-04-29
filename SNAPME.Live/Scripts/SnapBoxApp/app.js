var app = angular.module('IISNAPBOXAPP', ['ngMessages', 'ngMaterial', 'ngSanitize', 'ngCookies', 'ui.validate']);
app.config(function ($mdThemingProvider) {
    $mdThemingProvider.theme('default')
        .primaryPalette('grey')
        .accentPalette('amber')
});