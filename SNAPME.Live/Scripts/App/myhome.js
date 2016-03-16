app.controller('MyHomeController', function ($scope, $timeout) {
    $scope.communities = $scope.$parent.communities;
    $scope.featured_product = $scope.$parent.communities[Math.floor(Math.random() * 10 % 2)].featured_product;
});