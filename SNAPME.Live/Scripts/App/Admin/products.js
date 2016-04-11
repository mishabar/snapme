app.controller('AdminProductsController', function ($scope, $timeout, $interval, $filter, $mdDialog, productService) {
    $.loading = true;
    $scope.products = [];
    $scope.$parent.bodyColor = '#fff';

    productService.getAllProducts()
        .then(function (response) {
            $scope.products = response.data;
        },
        function (response) { });
});