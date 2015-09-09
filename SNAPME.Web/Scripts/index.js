var iisnapApp = angular.module('iisnapApp', ['ngCookies']);

// create angular controller
iisnapApp.controller('indexController', function ($scope, $http, $cookies) {

    $scope.data = [
        { id: "1234567890", name: "Bose SoundLink Mini", image_url: "http://worldwide.bose.com/axa/assets/images/products/soundlink_mini/soundlink_mini_si_lg_2.png", msrp: 175.00, target: 129.00, price: 137.00, drops: 43, progress: 70 },
        { id: "1234567891", name: "Intel NUC", image_url: "http://www.pcper.com/files/imagecache/article_max_width/news/2015-01-06/nuc_01.png", msrp: 499.99, target: 129.99, price: 137.00, drops: 43, progress: 90 },
        { id: "1234567892", name: "KitchenAid Stand Mixer", image_url: "http://www.theproductpedia.com/wp-content/uploads/2013/11/kitchenaid-stand-mixer.jpg", msrp: 175.00, target: 129.00, price: 137.00, drops: 43, progress: 70 },
        { id: "1234567893", name: "KitchenAid Ceramic 4.2 - Quart Casserole Dish with Lid", image_url: "http://www.kitchenaid.com/digitalassets/KBLR42CRER/Standalone_1175X1290.jpg", msrp: 175.00, target: 129.00, price: 137.00, drops: 43, progress: 70 }
    ];
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
        console.log(name + ' ' + id);
    };
});