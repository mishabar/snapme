var AccountService = angular.module('AccountService', [])
    .service('accountService', function ($http) {
        this.register = function (email) {
            return $http.post('/api/v1/account', { email: email });
        }
    });

var ProductsService = angular.module('ProductsService', [])
    .service('productsService', function ($http) {
        this.getCategories = function () {
            return $http.get('/api/v1/categories');
        }
        this.getProducts = function (category) {
            return $http.get('/api/v1/products/' + category.id);
        }
        this.getProduct = function (id) {
            return $http.get('/api/v1/product/' + id);
        }
        this.saveProduct = function (product) {
            return $http.post('/api/v1/product', product);
        }
    });

var SalesService = angular.module('SalesService', [])
    .service('salesService', function ($http) {
        this.getLiveSales = function (category) {
            return $http.get('/api/v1/sales/live/' + category.id);
        }
    });