app.controller('ShippingController', function ($scope, $timeout, shippingService) {
    
    $scope.details = {};
    $scope.init = function (product) { $scope.details = product.shipping_info }

    $scope.getQuote = function () {
        $scope.loadingQuote = true;
        $scope.postalDetails = '';

        shippingService.calculate($scope.details)
            .then(function (response) {
                $scope.loadingQuote = false;
                $scope.postalDetails = '$' + response.data.postage_result.total_cost + ', ' + (response.data.postage_result.delivery_time || 'No time estimation available');
            }, function (response) {
                $scope.loadingQuote = false;
                $scope.postalDetails = response.data.Message;
            });
    }
});