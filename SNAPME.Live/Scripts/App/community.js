app.controller('CommunityController', function ($scope, $filter, communityService) {
    $scope.loading = true;
    $scope.community = {};

    $scope.init = function (communityName) {
        communityService.getCommunity(communityName).then(
            function (response) {
                $scope.community = response.data;
                $scope.loading = false;
            },
            function (response) {
                $scope.$parent.communicationError();
                $scope.loading = false;
            });
    }
});