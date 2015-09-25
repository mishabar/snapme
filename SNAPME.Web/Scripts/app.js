var SalesService = angular.module('SalesService', [])
    .service('salesService', function ($http) {
        this.getActiveSales = function (pages) {
            return $http.get('/api/v1/sales/active');
        }
        this.getSale = function (productId) {
            return $http.get('/api/v1/sale/' + productId);
        }
    });

var ProductsService = angular.module('ProductsService', [])
    .service('productsService', function ($http) {
        this.favorProduct = function (id) {
            return $http.post('/api/v1/favor/product', { id: id });
        };

        this.likeProduct = function (id) {
            return $http.post('/api/v1/like/product', { id: id });
        };

        this.getProduct = function (id, loadSale) {
            return $http.get('/api/v1/product/' + id + '/' + (loadSale || 'true'));
        };

        this.getProducts = function (query, page) {
            return $http.post('/api/v1/products', { query: query, page: page});
        };

        this.saveProduct = function (product) {
            return $http.post('/api/v1/product', product);
        };

        this.uploadImage = function (id, idx, stream) {
            return $http.post('/api/v1/product/image', { id: id, idx: idx, stream: stream });
        };
    });

var iisnapApp = angular.module('iisnapApp', ['ngCookies', 'SalesService', 'ProductsService']);

iisnapApp.filter('range', function () {
    return function (val, range) {
        range = parseInt(range);
        for (var i = 0; i < range; i++)
            val.push(i);
        return val;
    };
});