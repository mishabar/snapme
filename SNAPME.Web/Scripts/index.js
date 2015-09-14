// create angular controller
iisnapApp.controller('indexController', function ($scope, $http, $timeout, $cookies) {

    $scope.data = [];
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

    $scope.initTooltip = function ($event, product) {
        if (!product.has_activity) {
            $($event.target).tooltip({ delay: 50, position: 'left', tooltip: 'No friends\' activities yet.' });
        }
    }

    $scope.refresh = function () {
        $http.get('/api/v1/sales/active')
            .then(function(response) {
                $scope.data = response.data;
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