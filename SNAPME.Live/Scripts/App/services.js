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

        this.getCommunity = function (name) {
            return $http.get('/api/v1/community/' + encodeURI(name));
        }
    });


var ProductService = angular.module('ProductService', [])
    .service('productService', function ($http) {
        this.getProduct = function (name) {
            return $http.get('/api/v1/product/' + encodeURI(name));
        }

        this.joinSale = function (id, shipping, quantity, price, stripeToken, sourceId) {
            var data = {};
            data.id = id;
            data.quantity = quantity;
            data.price = price;
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