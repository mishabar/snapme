app.controller('MyHomeController', function ($scope, $timeout, $filter, shippingService, communityService) {
    $scope.loading = true;
    $scope.communities = [];
    $scope.products = [];
    $scope.featured_product = {};
    $scope.selected_community = -1;
    $scope.filtered_products = [];

    $scope.getTargetInfo = function (product) {
        return product.sale.required_snaps - product.sale.snaps_count + ' Snaps left until target price ($' + product.sale.target.toFixed(2).toLocaleString() + ')';
    }

    $scope.filterProucts = function (id) {
        $scope.selected_community = id;
        if ($scope.selected_community == -1)
            $scope.filtered_products = $scope.products;
        else
            $scope.filtered_products = $filter('filter')($scope.products, { community_id: $scope.selected_community }, true);
    }

    communityService.getCommunities().then(
        function (response) {
            $scope.communities = response.data;
            for (var i = 0; i < $scope.communities.length; i++) {
                $scope.products = $scope.products.concat($scope.communities[i].products)
            }
            $scope.filtered_products = $scope.products;
            $scope.loading = false;
        },
        function (response) {
            $scope.$parent.communicationError();
            $scope.loading = false;
        });
});