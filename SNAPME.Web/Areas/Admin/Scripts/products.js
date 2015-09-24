iisnapApp.controller('adminProductsController', function ($scope, $http, $timeout, $cookies, salesService, productsService) {

    $scope.products = null;
    $scope.Math = window.Math;

    productsService.getProducts(null, 1)
        .success(function (response) {
            if (response.hasData) {
                $scope.products = response.data;
                $timeout(function () {
                    $('.materialboxed').removeClass('initialized').materialbox();
                }, 1000);
            } else {
                Materialize.toast('No products found!', 2000);
            }
        })
        .error(function (error) { console.log(error.message) });
}); 