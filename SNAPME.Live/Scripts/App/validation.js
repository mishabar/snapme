app.directive('validCreditCard', function () {
    return {
        require: 'ngModel',
        link: function (scope, elm, attrs, ctrl) {
            ctrl.$validators.validCreditCard = function (modelValue, viewValue) {
                if (ctrl.$isEmpty(modelValue)) {
                    // consider empty models to be valid
                    return false;
                }

                return Stripe.card.validateCardNumber(viewValue);
            };
        }
    };
});