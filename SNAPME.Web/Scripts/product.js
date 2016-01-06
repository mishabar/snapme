// create angular controller
iisnapApp.controller('saleController', function ($scope, $http, $timeout, $cookies, salesService, productsService, sharedDataService) {
    $scope.loading = true;
    $scope.joining = false;
    $scope.product = { sale: { joined: false } };
    $scope.user = {};
    $scope.authenticated = false;
    $scope.Math = window.Math;

    $(document).ready(function () {
        $('.modal-trigger').leanModal();
        $('#productTabs').tabs();
    });

    $scope.init = function (productId) {
        $scope.productId = productId;
        $scope.user = sharedDataService.get('user');
        productsService.getProduct($scope.productId)
            .success(function (data) {
                $scope.product = data;
                $scope.loading = false;
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
        $scope.joining = true;
        salesService.joinSale($scope.product.id)
            .then(function (response) {
                $scope.joining = false;
                $scope.product.sale = response.data;
                $('#mdlAfterJoined').openModal();
            },
            function (response) { });
    };

    $scope.showComments = function () {
        $('#productTabs').tabs('select_tab', 'tabComments');
    }

    $scope.showDescription = function () {
        $('#productTabs').tabs('select_tab', 'tabDescription');
    }

    $scope.submitComment = function () {
        if (!$scope.commentForm.$invalid) {
            productsService.saveComment($scope.product.id, $scope.productComment)
                .then(function (response) {
                    $scope.product.comments.unshift(response.data);
                    $scope.productComment = null;
                }, function (response) { });
        }
    }

    $scope.inviteFriends = function (sn) {
        if (sn == 'facebook') {
            window.open('https://www.facebook.com/sharer/sharer.php?u=' + encodeURIComponent(document.location.href));
        } else if (sn == 'google') {
            window.open('https://plus.google.com/share?url=' + encodeURIComponent(document.location.href));
        } else if (sn == 'twitter') {
            window.open('https://twitter.com/home?status=' + encodeURIComponent(document.location.href));
        } else if (sn == 'pinterest') {
            window.open('https://pinterest.com/pin/create/button/?url=' + encodeURIComponent(document.location.href) + '&media=' + encodeURIComponent(document.location.origin + '/image/' + $scope.product.id + '/' + $('.slides li:eq(0) img').data('idx')));
        }
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