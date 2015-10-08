// create angular controller
iisnapApp.controller('saleController', function ($scope, $http, $timeout, $cookies, salesService, productsService) {

    $scope.product = null;
    $scope.sale = null;
    $scope.authenticated = false;
    $scope.Math = window.Math;

    $(document).ready(function () {
        $('.modal-trigger').leanModal();
    });

    $scope.init = function (productId) {
        $scope.productId = productId;
        productsService.getProduct($scope.productId)
            .success(function (data) {
                $scope.product = data;
                $timeout(function () {
                    if (($('.slider').data('i') || false) == false) {
                        $('.slider').slider({ full_width: true, indicators: true, height: 400 });
                        $('.slider').data('i', true);
                    }
                }, 100);
            })
            .error(function (error) { console.log(error.message) });
    };

    $scope.refresh = function () {
        salesService.getSale($scope.productId)
            .success(function (data) {
                $scope.product.sale = data.sale;
            })
            .error(function (error) { console.log(error.message) });
    }

    $scope.intervalFunction = function () {
        $timeout(function () {
            $scope.refresh();
            $scope.intervalFunction();
        }, 5000);
    };

    //$scope.refresh();

    // Kick off the interval
    $scope.intervalFunction();
});