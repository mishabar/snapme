var BetaRegistrationService = angular.module('BetaRegistrationService', [])
    .service('betaRegistrationService', function ($http) {
        this.register = function (email) {
            return $http.post('/api/v1/beta/register', { email: email });
        }
    });


var ShippingService = angular.module('ShippingService', [])
    .service('shippingService', function ($http) {
        this.calculate = function (details) {
            return $http.post('api/v1/shipping/rates', details);
        }
    });