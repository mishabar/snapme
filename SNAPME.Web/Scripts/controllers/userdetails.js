// create angular controller
iisnapApp.controller('userDetailsController', function ($scope, $timeout, accountService) {

    $scope.details = {};
    $scope.menu = 'details';
    $scope.address = { idx: -1 };
    $scope.profile_image = '/Content/Images/iisnap.ico';
    $scope.states = { "ACT": "Australian Capital Territory", "NSW": "New South Wales", "VIC": "Victoria", "QLD": "Queensland", "SA": "South Australia", "WA": "Western Australia", "TAS": "Tasmania", "NT": "Northern Territory" };

    $scope.init = function (id, name) {
        $scope.profile_image = '/Account/Image/' + id + '?width=' + 300;
        $scope.details.name = name;
        accountService.getDetails()
            .then(function (response) {
                $scope.details = response.data;
                $scope.details.name = response.data.first_name + ' ' + response.data.last_name;
                $timeout(function () { $('.collapsible').collapsible(); $('select').material_select(); }, 100);
            },
            function (response) {
                Materialize.toast('Something wen wrong :( Please try refreshing the page.', 2000);
            });
    }

    $scope.saveDetails = function () {
        if ($scope.detailsForm.$valid) {
            accountService.saveDetails($scope.details)
                .then(function (response) {
                    Materialize.toast('Basic Info was saved.', 2000);
                }, function (response) {
                    Materialize.toast('Something wen wrong :( Please try again.', 2000);
                })
        }
    }

    $scope.addAddress = function (valid) {
        if (valid) {
            accountService.saveAddress($scope.address)
                .then(function (response) {
                    $scope.details.addresses.push($scope.address);
                    $scope.address = {};
                    $timeout(function () { $('.collapsible').collapsible(); $('select').material_select(); }, 100);
                    $scope.addressForm.$setPristine();
                    $scope.addressForm.$setUntouched();
                    Materialize.toast('Address was saved.', 2000);
                }, function (response) {
                    Materialize.toast('Something wen wrong :( Please try again.', 2000);
                });
        }
    }

    $scope.updateAddress = function (address, idx) {
        var _scope = angular.element('#addressForm' + idx).scope();
        address.idx = idx;
        if (_scope['addressForm' + idx].$valid) {
            accountService.saveAddress(_scope.address)
                .then(function (response) {
                    Materialize.toast('Address was saved.', 2000);
                }, function (response) {
                    Materialize.toast('Something wen wrong :( Please try again.', 2000);
                });
        }
    }

    $scope.removeAddress = function (idx) {
        accountService.deleteAddress(idx)
            .then(function (response) {
                $scope.details.addresses.splice(idx, 1);
                Materialize.toast('Address was removed.', 2000);
            }, function (response) {
                Materialize.toast('Something wen wrong :( Please try again.', 2000);
            });
    }
});