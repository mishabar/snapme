app.controller('HomeController', function ($scope, $timeout, betaRegistrationService) {
    $scope.fullMode = false;
    $scope.images = ['tech.jpg', 'wshopping.jpg'];

    $scope.body = angular.element(document.body);
    $scope.body.addClass('ii-home');
    $scope.body.css('background', 'url(/content/images/home/' + $scope.images[Math.floor(Math.random() * 10 % $scope.images.length)] + ') no-repeat center center fixed');

    $scope.signup = { email: null, registered: -1 };

    $scope.init = function (fullMode) {
        $scope.fullMode = fullMode;
    }

    $scope.signupForBeta = function () {
        betaRegistrationService.register($scope.signup.email)
            .then(function (response) {
                $scope.signup.registered = response.data
            });
    }
});