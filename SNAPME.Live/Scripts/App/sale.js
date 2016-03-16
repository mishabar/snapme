function JoinSaleDialogController($scope, $timeout, $filter, $mdDialog, product) {

    $scope.product = product;
    $scope.selectedIndex = 0;
    $scope.nextButtonTitle = "Next";
    $scope.quantity = 1;
    $scope.states = ["Australian Capital Territory", "New South Wales", "Northern Territory", "Queensland", "South Australia", "Tasmania", "Victoria", "Western Australia"];

    $scope.nextStep = function () {
        $mdDialog.hide($scope.quantity);
        //$scope.selectedIndex++;
        //$scope.nextButtonTitle = $scope.selectedIndex < 2 ? "Next" : "Join";
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
        return false;
    }
}