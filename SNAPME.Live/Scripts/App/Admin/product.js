app.controller('AdminProductController', function ($scope, $timeout, $interval, $filter, $mdDialog, $sce, productService, communityService) {
    $scope.loading = true;
    $scope.loadingSales = true;
    $scope.images_modes = ["Contain", "Cover"];
    $scope.communities = [];
    $scope.product = {};
    $scope.sales = [];
    $scope.$parent.bodyColor = '#fff';

    $scope.init = function (productId) {
        communityService.getCommunities()
            .then(function (response) {
                $scope.communities = response.data;
            });
        productService.getProduct(productId)
            .then(function (response) {
                $scope.product = response.data;
            },
            function (response) { });
    }

    $scope.loadSales = function () {

    }

    $scope.updateProduct = function (valid) {
        if (valid) {
            productService.updateProduct(this.product).then(
                function (response) {
                    
                },
                function (response) { });
        }
    }

    $scope.updateHtml = function () {
        $scope.product.full_details = $sce.trustAsHtml(tinymce);
    };

    $scope.range = function (i) {
        return new Array(i);
    }
});