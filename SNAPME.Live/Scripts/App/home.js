app.controller('HomeController', function ($scope, $timeout) {
    $scope.provider = 'Facebook';
    $scope.images = ['tech.jpg', 'wshopping.jpg'];

    $scope.body = angular.element(document.body);
    $scope.body.addClass('ii-home');
    $scope.body.css('background', 'url(/content/images/home/' + $scope.images[Math.floor(Math.random() * 10 % $scope.images.length)] + ') no-repeat center center fixed');

    $scope.login = function (provider) {
        $scope.provider = provider;
        $timeout(function () {
            frmFbLogin.submit();
        }, 100);
    }
});