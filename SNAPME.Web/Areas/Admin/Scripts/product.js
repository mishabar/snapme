iisnapApp.controller('adminProductController', function ($scope, $http, $timeout, $cookies, salesService, productsService) {

    $scope.product = {"id": null};// "id": null, "name": "Bose Soudlink Mini II", "sku": "BOSESLMII", "category": "Electronics", "retail_price": 199, "weight": 900, "width": 25, "length": 16, "height": 16 };
    $scope.Math = window.Math;

    $scope.init = function (productId) {
        if (productId === 'null' || productId === '') {
            $scope.product = { "id": null, "name": "", "sku": "", "category": "", "retail_price": 0, "weight": 0, "width": 0, "length": 0, "height": 0 };
        } else {
            productsService.getProduct(productId, false)
            .success(function (data) {
                $scope.product = data;
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
                //$.post("/api/v1/product/image", { id: $('#id').val(), image: reader.result, idx: idx }, function (response) {
                //    $that.parent().css('background-image', 'url(' + reader.result + ')');
                //});
                img.attr('src', reader.result);
            };
            reader.readAsDataURL(file.files[0]);
        }
    };

    $scope.submitForm = function (valid) {
    };

    $(function () {
        $('select').material_select();
    });
}); 