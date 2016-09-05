var BetaRegistrationService = angular.module('BetaRegistrationService', [])
    .service('betaRegistrationService', function ($http) {
        this.register = function (email) {
            return $http.post('/api/v1/beta/register', { email: email });
        }
    });


var ShippingService = angular.module('ShippingService', [])
    .service('shippingService', function ($http) {
        this.calculate = function (details) {
            return $http.post('/api/v1/shipping/rates', details);
        }
    });


var CommunityService = angular.module('CommunityService', [])
    .service('communityService', function ($http) {
        this.getCommunities = function () {
            return $http.get('/api/v1/communities');
        }

        this.getCommunitiesList = function () {
            return $http.get('/api/v1/communities/list');
        }

        this.getCommunity = function (name) {
            return $http.get('/api/v1/community/' + encodeURI(name));
        }
    });

var AccountService = angular.module('AccountService', [])
    .service('accountService', function ($http) {
        
        this.getCommunities = function () {
            return $http.get('/api/v1/my/communities');
        }
    });


var PaymentService = angular.module('PaymentService', [])
    .service('paymentService', function ($http) {
        this.getUserCards = function () {
            return $http.get('/api/v1/payment/customer/cards');
        }
    });

var ProductService = angular.module('ProductService', [])
    .service('productService', function ($http) {
        this.getProduct = function (name) {
            return $http.get('/api/v1/product/' + encodeURI(name));
        }

        this.getProductSales = function (id) {
            return $http.get('/api/v1/product/' + id + '/sales');
        }

        this.getAllProducts = function () {
            return $http.get('/api/v1/products');
        }

        this.updateProduct = function (product) {
            return $http.post('/api/v1/product', product);
        }

        this.createSale = function (product_id, sale) {
            var data = sale;
            sale.product_id = product_id;
            return $http.post('/api/v1/product/' + product_id + '/sales', data);
        }

        this.updateSale = function (product_id, sale) {
            return $http.post('/api/v1/product/' + product_id + '/sales', { sale: sale });
        }

        this.joinSale = function (product, shipping, quantity, stripeToken, sourceId) {
            var data = {};
            data.product_id = product.id;
            data.product_name = product.name;
            data.price = product.target;
            data.quantity = quantity;
            data.shipping_price = shipping.price;
            data.stripe_token = stripeToken;
            data.source_id = sourceId;
            data.first_name = shipping.firstName;
            data.last_name = shipping.lastName;
            data.address = shipping.address;
            data.address2 = shipping.address2;
            data.city = shipping.city;
            data.state = shipping.state;
            data.zip_code = shipping.postalCode;
            return $http.post('/api/v1/sale/join', data);
        }
    });