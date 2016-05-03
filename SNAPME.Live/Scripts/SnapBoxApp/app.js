var app = angular.module('IISNAPBOXAPP', ['ngMessages', 'ngMaterial', 'ngSanitize', 'ngCookies', 'ui.validate']);
app.config(function ($mdThemingProvider, $compileProvider) {
    $mdThemingProvider.theme('default')
        .primaryPalette('grey')
        .accentPalette('amber');
    $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|chrome-extension|intent):/);
});