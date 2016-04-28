var app = angular.module('IISNAPBOXAPP', ['ngMessages', 'ngMaterial', 'ngSanitize', 'ngCookies', 'ui.validate']);
app.config(function ($mdThemingProvider) {
    $mdThemingProvider.theme('default')
        .primaryPalette('grey')
        .accentPalette('amber')
});

window.addEventListener("load", function () {
    // Set a timeout...
    setTimeout(function () {
        // Hide the address bar!
        document.body.style.height = screen.availHeight + 'px';
        window.scrollTo(0, 1);
    }, 0);
});