app.controller('CommunityController', function ($scope, $filter) {

    $scope.init = function (communityName) {
        $scope.community = $filter('filter')($scope.$parent.communities, { name: communityName }, true)[0];
    }
});