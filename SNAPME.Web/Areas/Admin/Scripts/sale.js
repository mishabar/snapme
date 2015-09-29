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
                if (data.sale == null) { data.sale = { product_id: productId, is_featured: false } }
                else { $scope.datepicker.pickadate('picker').set('select', data.sale.starts_on, { format: 'dd/mm/yyyy' }) }
                $scope.product = data;
                $timeout(function () { $('.input-field > label[for]').addClass('active'); }, 100);
            })
            .error(function (error) { console.log(error.message) });
        }
    };


    $scope.previewImage = function (file) {
        if ($(file).val() !== '') {
            var reader = new FileReader();
            var img = $(file).closest('.row').find('img');
            var idx = $(file).data('idx');
            reader.onload = function () {
                productsService.uploadImage($scope.product.id, idx, reader.result)
                    .success(function (data) {
                        img.attr('src', reader.result);
                        Materialize.toast('Image Saved', 3000, 'green darken-1');
                    })
                    .error(function (data) { Materialize.toast('Something went wrong :(', 3000, ' red darken-2') })
            };
            reader.readAsDataURL(file.files[0]);
        }
    };

    $scope.submitForm = function (valid) {
        if (valid) {
            salesService.saveSale($scope.product.sale)
                .success(function (data) {
                    $scope.product.sale = data;
                    Materialize.toast('Sale Saved', 3000, 'green darken-1');
                })
                .error(function (data) { Materialize.toast('Something went wrong :(', 3000, ' red darken-2') });
        }
        //console.log($scope.product.sale);
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