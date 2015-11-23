// create angular controller
iisnapApp.controller('accountController', function ($scope, $http, $cookies) {

    $scope.logoff = function ($event) {
        $event.preventDefault();
        logoutForm.submit();
    };

    $scope.inviteFriends = function (id) {
        window.FB.ui({
            method: 'send',
            link: 'http://www.iisnap.com/welcome?ref=' + id
        });
    }
});