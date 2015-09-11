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