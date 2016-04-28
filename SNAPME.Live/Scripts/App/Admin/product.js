app.controller('AdminProductController', function ($scope, $timeout, $interval, $filter, $mdDialog, $sce, productService, communityService) {
    $scope.loading = true;
    $scope.loadingSales = true;
    $scope.images_modes = ["Contain", "Cover"];
    $scope.communities = [];
    $scope.product = {};
    $scope.oldSales = [];
    $scope.futureSales = [];
    $scope.activeSale = null;
    $scope.$parent.bodyColor = '#fff';

    $scope.init = function (productId) {
        communityService.getCommunities()
            .then(function (response) {
                $scope.communities = response.data;
            });
        productService.getProduct(productId)
            .then(function (response) {
                $scope.product = response.data;
            },
            function (response) { });
    }

    $scope.loadSales = function () {
        productService.getProductSales($scope.product.id)
            .then(function (response) {
                active = $filter('filter')(response.data, { state: 1 }, true);
                $scope.activeSale = active.length > 0 ? active[0] : null;
                $scope.futureSales = $filter('filter')(response.data, { state: 2 }, true);
                $scope.loadingSales = false;
            });
    }

    $scope.updateProduct = function (valid) {
        if (valid) {
            productService.updateProduct(this.product).then(
                function (response) {

                },
                function (response) { });
        }
    }

    $scope.updateHtml = function () {
        $scope.product.full_details = $sce.trustAsHtml(tinymce);
    };

    $scope.scheduleNewSale = function ($event) {
        $mdDialog.show({
            locals: { product: $scope.product, sale: { sale_type: 0, id: 0 } },
            controller: ScheduleSaleDialogController,
            templateUrl: '/Admin/Product/Sale',
            parent: angular.element(document.body),
            targetEvent: $event,
            clickOutsideToClose: false
        }).then(function (reload) {
            $scope.loadingSales = true;
            $scope.loadSales();
        }, function () {
        });
    }

    $scope.range = function (i) {
        return new Array(i);
    }
});

function ScheduleSaleDialogController($scope, $timeout, $filter, $mdDialog, shippingService, productService, paymentService, product, sale) {
    $scope.product = product;
    $scope.sale = sale;
    $scope.minStartDate = new Date(+new Date() + 86400000);
    $scope.minEndDate = new Date(+new Date() + 86400000);
    $scope.types = [{ id: 0, name: "Stock" }, { id: 1, name: "Time" }];

    $scope.calculateMinDates = function () {
        $scope.minEndDate = new Date(+$scope.sale.starts_on + 2 * 86400000);
    }

    $scope.cancel = function () { $mdDialog.hide() }

    $scope.update = function () {
        productService.updateSale($scope.sale)
            .then(function (response) {
                $mdDialog.hide(true)
            });
    }

    $scope.create = function () {
        productService.createSale($scope.product.id, $scope.sale)
            .then(function (response) {
                $mdDialog.hide(true)
            });
    }
}