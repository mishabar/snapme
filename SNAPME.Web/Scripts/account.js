// create angular controller
iisnapApp.controller('accountController', function ($scope, $http, $cookies) {

    $scope.logoff = function ($event) {
        $event.preventDefault();
        logoutForm.submit();
    };
});