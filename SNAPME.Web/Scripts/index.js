// create angular controller
iisnapApp.controller('salesController', function ($scope, $http, $timeout, $cookies) {

    $scope.sales = [];
    $scope.featured_sales = [];
    $scope.authenticated = false;
    $scope.Math = window.Math;

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
        $http.post('/api/v1/like/product', { id: product.id })
            .then(function (response) {
                product.likes = response.data.liked;
            }, function (response) {
            })
    };

    $scope.favorProduct = function ($event, id) {
        $event.preventDefault();
        $event.stopPropagation();
        $http.post('/api/v1/favorite/product', { id: product.id })
            .then(function (response) {
                product.favors = response.data.favored;
            }, function (response) {
            })
    };

    $scope.refresh = function () {
        $http.get('/api/v1/sales/active')
            .then(function(response) {
                $scope.sales = response.data.sales;
                $scope.featured_sales = $.grep($scope.sales, function (el, i) { return el.is_featured; });
                $scope.authenticated = response.data.a;
                $timeout(function () {
                    if (($('.slider').data('i') || false) == false){
                        $('.slider').slider({ full_width: true, indicators: false, height: 592 });
                        $('.slider').data('i', true);
                    }
                }, 100);
            }, function(response) {
                console.log(response.statusText);
            });
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