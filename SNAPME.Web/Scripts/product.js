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

    $scope.joinSale = function () {
        $('#mdlAfterJoined').openModal();
    };

    $scope.inviteFriends = function () {
        $('#mdlAfterJoined').closeModal();
        window.FB.ui({
            method: 'send',
            link: document.location.href
        });
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

iisnapApp.controller('commentController', function ($scope, $http, $timeout, productsService) {

    $scope.productId = null;
    $scope.$rating = null;

    $scope.init = function (productId) {
        $scope.productId = productId;
        $scope.$rating = $('#mdlComment .rating').addRating();
    };

    $scope.postComment = function () {
        // check rating set (1<>5) and has comments
        $('#mdlComment .invlid').removeClass('invalid');
        var rval = $('#mdlComment #rating').val();
        if (rval === '' || rval === '0') {
            $('#mdlComment .rating').addClass('invalid');
        }

        if ($('#mdlComment textarea').val().trim() === '') {
            $('#mdlComment textarea').addClass('invalid');
        }

        if ($('#mdlComment .invalid').length == 0) {
            // post and close
            $('#mdlComment').closeModal();
        }
    };

    $scope.cancelComment = function () {
        $scope.$rating.setRating(0);
        $('#mdlComment textarea').val('');
        $('#mdlComment').closeModal();
    };
});