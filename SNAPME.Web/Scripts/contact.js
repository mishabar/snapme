iisnapApp.controller('contactController', function ($scope, $http, $cookies) {

    $scope.submitted = false;
    $scope.data = { };
    $scope.sent = false;

    $scope.init = function (type) {
        $scope.data.type = type;
    };

    // function to submit the form after all validation has occurred            
    $scope.submitForm = function ($event, valid) {
        $scope.submitted = true;
        $event.preventDefault();

        if (valid) {
            $http.post('/api/v1/contact', $scope.data)
                .success(function () { $scope.sent = true; })
                .error(function (data, status, errors, config) { console.log(data) });
        }
    };
});