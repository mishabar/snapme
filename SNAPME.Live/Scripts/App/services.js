var BetaRegistrationService = angular.module('BetaRegistrationService', [])
    .service('betaRegistrationService', function ($http) {
        this.register = function (email) {
            return $http.post('/api/v1/beta/register', { email: email });
        }
    });