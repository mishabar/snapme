app.controller('SettingsController', function ($scope, $timeout, $filter, shippingService, communityService, accountService) {
    $scope.loading = true;
    $scope.communities = [];
    
    accountService.getCommunities().then(
        function (response) {
            $scope.communities = response.data;
            $scope.loading = false;
        },
        function (response) {
            $scope.$parent.communicationError();
            $scope.loading = false;
        });
});