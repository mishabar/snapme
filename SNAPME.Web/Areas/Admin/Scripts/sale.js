iisnapApp.controller('adminSaleController', function ($scope, $http, $timeout, $cookies, salesService, productsService) {

    $scope.product = {"id": null};// "id": null, "name": "Bose Soudlink Mini II", "sku": "BOSESLMII", "category": "Electronics", "retail_price": 199, "weight": 900, "width": 25, "length": 16, "height": 16 };
    $scope.Math = window.Math;
    $scope.datepicker = null;

    $scope.init = function (productId) {
        if (productId === 'null' || productId === '') {
            $scope.product = { "id": null, "name": "", "sku": "", "category": "", "retail_price": 0, "weight": 0, "width": 0, "length": 0, "height": 0 };
        } else {
            productsService.getProduct(productId, true)
            .success(function (data) {
                if (data.sale == null) { data.sale = { starts_on: "" } }
                $scope.product = data;
            })
            .error(function (error) { console.log(error.message) });
        }
    };

    $scope.submitForm = function (valid) {
        //if (valid) {
        //    productsService.saveProduct($scope.product)
        //        .success(function (data) {
        //            $scope.product = data;
        //            Materialize.toast('Product Saved', 3000, 'green darken-1');
        //        })
        //        .error(function (data) { Materialize.toast('Something went wrong :(', 3000, ' red darken-2') });
        //}
        console.log($scope.product.sale.starts_on);
    };

    $scope.showDatepicker = function () {
        $scope.datepicker.pickadate('picker').open();
    };

    $(function () {
        $('select').material_select();
        $scope.datepicker = $('.datepicker').pickadate({
            format: "dd/mm/yyyy",
            onClose: function (val) {
                var val = this.get();
                var $input = $('input[name=starts_on]');
                $input.val(val);
                angular.element($input).triggerHandler('input');
            }
        });
    });
}); 