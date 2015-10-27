iisnapApp.controller('adminProductController', function ($scope, $http, $timeout, $cookies, salesService, productsService, sellerService) {

    $scope.product = { "id": null };// "id": null, "name": "Bose Soudlink Mini II", "sku": "BOSESLMII", "category": "Electronics", "retail_price": 199, "weight": 900, "width": 25, "length": 16, "height": 16 };
    $scope.seller = null;
    $scope.Math = window.Math;

    $scope.init = function (productId) {
        if (productId === 'null' || productId === '') {
            $scope.product = { "id": null, "name": "", "sku": "", "category": "", "retail_price": 0, "weight": 0, "width": 0, "length": 0, "height": 0 };
        } else {
            productsService.getProduct(productId, false)
            .success(function (data) {
                $scope.product = data;
                sellerService.getSeller(data.seller_id)
                    .success(function (sdata) { $scope.seller = sdata })
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
            productsService.saveProduct($scope.product)
                .success(function (data) {
                    $scope.product = data;
                    Materialize.toast('Product Saved', 3000, 'green darken-1');
                })
                .error(function (data) { Materialize.toast('Something went wrong :(', 3000, ' red darken-2') });
        }
    };

    $(function () {
        $('select').material_select();
    });
}); 