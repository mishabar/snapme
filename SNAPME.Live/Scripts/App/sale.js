function JoinSaleDialogController($scope, $timeout, $filter, $mdDialog, shippingService, productService, product) {

    $scope.product = product;
    $scope.joining = false;
    $scope.selectedIndex = 0;
    $scope.nextButtonTitle = "Next";
    $scope.quantity = 1;
    $scope.shipping = {};
    $scope.billing = {};
    $scope.states = ["Australian Capital Territory", "New South Wales", "Northern Territory", "Queensland", "South Australia", "Tasmania", "Victoria", "Western Australia"];

    $scope.nextStep = function () {
        $scope.selectedIndex++;
        $scope.nextButtonTitle = $scope.selectedIndex < 2 ? "Next" : "Join";
        if ($scope.selectedIndex == 2) {
            // calclulate shipping
            var shippingDetails = $scope.product.shipping_info;
            shippingDetails.to_zip = $scope.shipping.postalCode;
            shippingService.calculate(shippingDetails)
                .then(function (response) {
                    $scope.shipping.price = response.data.postage_result.total_cost;
                });
        } else if ($scope.selectedIndex == 3) {
            $scope.joining = true;
            Stripe.card.createToken({
                number: $scope.billing.cardNumber,
                cvc: $scope.billing.cvc,
                exp_month: $scope.billing.expMonth,
                exp_year: $scope.billing.expYear,
                name: $scope.billing.firstName + ' ' + $scope.billing.lastName,
                address_line1: $scope.billing.address,
                address_line2: $scope.billing.address2,
                address_city: $scope.billing.city,
                address_state: $scope.billing.state,
                address_zip: $scope.billing.postalCode,
                address_country: "AU"
            }, function (status, stripeResponse) {
                if (stripeResponse.error) {
                    $scope.joining = false;
                    $scope.error = stripeResponse.error.message;
                } else {
                    productService.joinSale($scope.product.id, $scope.shipping, $scope.quantity, product.current_price, stripeResponse.id)
                        .then(function (response) {
                            $scope.joining = false;
                        }, function (response) {
                            $scope.joining = false;
                        })
                }
            });
        }
    }

    $scope.prevStep = function () {
        $scope.selectedIndex--;
        $scope.nextButtonTitle = $scope.selectedIndex == 2 ? "Join" : "Next";
    }

    $scope.cancel = function () {
        $mdDialog.cancel();
    }

    $scope.validForm = function () {
        if ($scope.selectedIndex == 0 && $scope.detailsForm) { return $scope.detailsForm.$valid }
        if ($scope.selectedIndex == 1 && $scope.shippingForm) { return $scope.shippingForm.$valid }
        if ($scope.selectedIndex == 2 && $scope.billingForm) { return $scope.billingForm.$valid }
        return false;
    }

    $scope.validCardNumber = function (value) {
        return Stripe.card.validateCardNumber(value);
    }

    $scope.validExpiration = function (t, v) {
        mm = (t === 'M' ? v : this.billingForm.expMonth.$viewValue);
        yyyy = (t === 'Y' ? v : this.billingForm.expYear.$viewValue);

        if (mm && yyyy) {
            result = Stripe.card.validateExpiry(mm, yyyy);
            if (result) {
                if (t === 'M' && this.billingForm.expYear.$invalid) this.billingForm.expYear.$validate();
                if (t === 'Y' && this.billingForm.expMonth.$invalid) this.billingForm.expMonth.$validate();
            }
            return result;
        }

        return true;
    }

    $scope.copyShippingAddress = function () {
        if ($scope.billing.sameAsShipping) {
            $scope.billing.firstName = $scope.shipping.firstName;
            $scope.billing.lastName = $scope.shipping.lastName;
            $scope.billing.address = $scope.shipping.address;
            $scope.billing.address2 = $scope.shipping.address2;
            $scope.billing.city = $scope.shipping.city;
            $scope.billing.state = $scope.shipping.state;
            $scope.billing.postalCode = $scope.shipping.postalCode;
        }
    }
}