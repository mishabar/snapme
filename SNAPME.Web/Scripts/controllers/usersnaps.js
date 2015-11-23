// create angular controller
iisnapApp.controller('userSnapsController', function ($scope, $timeout, accountService) {

    $scope.details = {};
    $scope.favorites = [];
    $scope.menu = 'snaps';
    $scope.profile_image = '/Content/Images/iisnap.ico';
    $scope.snaps = [];
    $scope.current_product = {};
    $scope.checkout = { address: null, billing_address: null, shipping_method: null };
    $scope.checkout_step = 1;
    $scope.addresses = [];

    $scope.init = function (id, name) {
        $scope.profile_image = '/Account/Image/' + id + '?width=' + 300;
        $scope.details.name = name;
        accountService.getAddresses()
            .then(function (data) {
                $scope.addresses = data.data;
                //$scope.addresses.push({ address_name: 'Create new...' });
                $timeout(function () { $('select').material_select() }, 50);
            });
        accountService.getSnaps()
            .then(function (response) {
                $scope.snaps = response.data;
            },
            function (response) {
                Materialize.toast('Something wen wrong :( Please try refreshing the page.', 2000);
            });
    }

    $scope.openProduct = function ($event, name, id) {
        $event.stopPropagation();
        var parts = name.split(' ');
        parts.push(id);
        document.location.href = '/product/' + parts.join('-');
    };

    $scope.checkout = function (product) {
        $scope.current_product = product;
        $scope.checkout_step = 1;
        $('#checkoutModal').openModal({ dismissible: false });
    }

    $scope.checkoutMoveNext = function () {
        $scope.checkout_step++;
        $('#checkoutModal .determinate.iisnap-orange').css('width', ($scope.checkout_step - 1) * 50 + '%');
        if ($scope.checkout_step == 3) {
            $scope.checkout.shipping_method = { carrier: 'Australian Post', price: 10 };
        }
    }

    $scope.checkoutMoveBack = function () {
        $scope.checkout_step--;
        $('#checkoutModal .determinate.iisnap-orange').css('width', ($scope.checkout_step - 1) * 50 + '%');
    }

    $scope.checkoutClose = function () {
        $('#checkoutModal').closeModal();
        $scope.checkout_step = 1;
        $('#checkoutModal .determinate.iisnap-orange').css('width', '0');
    }

    $scope.canAdvance = function () {
        return ($scope.checkout_step == 1 && $scope.checkout.address != null) ||
            ($scope.checkout_step == 2 && $scope.checkout.billing_address != null);
    }
});