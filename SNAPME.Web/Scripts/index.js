// create angular controller
iisnapApp.controller('salesController', function ($scope, $http, $timeout, $cookies, salesService, productsService) {

    $scope.sales = null;
    $scope.featured_sales = null;
    $scope.authenticated = false;
    $scope.Math = window.Math;
    $scope.pages = 1;

    $(document).ready(function () {
        $('.modal-trigger').leanModal({
            complete: function () {
                var $iframe = $('#about iframe');
                $iframe.attr('src', $iframe.attr('src'));
            }
        });
    });

    $scope.openProduct = function (name, id) {
        var parts = name.split(' ');
        parts.push(id);
        document.location.href = '/product/' + parts.join('-');
    };

    $scope.likeProduct = function ($event, product) {
        $event.preventDefault();
        $event.stopPropagation();
        productsService.likeProduct(product.product_id)
            .success(function (data) {
                product.likes = data.liked;
            })
            .error(function (error) { console.log(error.message) });
    };

    $scope.favorProduct = function ($event, product) {
        $event.preventDefault();
        $event.stopPropagation();
        productsService.favorProduct(product.product_id)
            .success(function (data) {
                product.favors = data.favored;
            })
            .error(function (error) { console.log(error.message) });
    };

    $scope.refresh = function () {
        salesService.getActiveSales($scope.pages)
            .success(function (data) {
                $scope.sales = data.sales;
                $scope.featured_sales = $.grep($scope.sales, function (el, i) { return el.is_featured; });
                $scope.authenticated = data.a;
                $timeout(function () {
                    if (($('.slider').data('i') || false) == false) {
                        $('.slider').slider({ full_width: true, indicators: false, height: 592 });
                        $('.slider').data('i', true);
                    }
                    $('.tooltipped').tooltip('remove');
                    $('.tooltipped').tooltip();
                }, 100);
            })
            .error(function (error) { console.log(error.message) });
    }

    $scope.intervalFunction = function () {
        $timeout(function () {
            $scope.refresh();
            $scope.intervalFunction();
        }, 5000);
    };

    $scope.refresh();

    // Kick off the interval
    $scope.intervalFunction();
});