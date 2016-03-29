app.controller('MyHomeController', function ($scope, $timeout, shippingService, communityService) {
    $scope.loading = true;
    $scope.communities = [];
    $scope.featured_product = {};//$scope.$parent.communities[Math.floor(Math.random() * 10 % 2)].featured_product;

    communityService.getCommunities().then(
        function (response) {
            $scope.communities = response.data;
            $scope.loading = false;
        },
        function (response) {
            $scope.$parent.communicationError();
            $scope.loading = false;
        });
});