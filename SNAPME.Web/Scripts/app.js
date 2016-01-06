var AccountService = angular.module('AccountService', [])
    .service('accountService', function ($http) {
        this.getDetails = function () {
            return $http.get('/api/v1/user');
        }

        this.getFavorites = function () {
            return $http.get('/api/v1/user/favorites');
        }

        this.getSnaps = function () {
            return $http.get('/api/v1/user/snaps');
        }

        this.getRewards = function () {
            return $http.get('/api/v1/user/rewards');
        }

        this.getFriends = function () {
            return $http.get('/api/v1/user/friends');
        }

        this.saveDetails = function (details) {
            return $http.post('/api/v1/user/', details);
        }

        this.saveAddress = function (address) {
            return $http.post('/api/v1/user/address', address);
        }

        this.deleteAddress = function (idx) {
            return $http.delete('/api/v1/user/address/' + idx);
        }

        this.getAddresses = function () {
            return $http.get('/api/v1/user/address');
        }
    });

var SellerService = angular.module('SellerService', [])
    .service('sellerService', function ($http) {
        this.getSeller = function (id) {
            return $http.get('/api/v1/seller/' + id);
        }
    });

var SalesService = angular.module('SalesService', [])
    .service('salesService', function ($http) {
        this.getActiveSales = function (pages) {
            return $http.get('/api/v1/sales/active');
        };

        this.getSale = function (productId) {
            return $http.get('/api/v1/sale/' + productId);
        };

        this.saveSale = function (sale) {
            return $http.post('/api/v1/sale', sale);
        };

        this.joinSale = function (id) {
            return $http.post('/api/v1/sale/join', { id: id });
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

        this.saveComment = function (id, comment) {
            return $http.post('/api/v1/product/comment', { id: id, comment: comment });
        };
    });

var SharedDataService = angular.module('SharedDataService', [])
    .service('sharedDataService', function () {
        var data = {};

        this.set = function (name, value) {
            data[name] = value;
        }

        this.get = function (name) {
            return data[name];
        }
    });

var iisnapApp = angular.module('iisnapApp', ['ngCookies', 'ngSanitize', 'SalesService', 'ProductsService', 'SellerService', 'AccountService', 'SharedDataService']);

iisnapApp.filter('range', function () {
    return function (val, range) {
        range = parseInt(range);
        for (var i = 0; i < range; i++)
            val.push(i);
        return val;
    };
});