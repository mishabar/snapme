var iisnapApp = angular.module('iisnapApp', []);

// create angular controller
iisnapApp.controller('welcomeController', function ($scope, $http) {

    $scope.submitted = false;

    $(document).ready(function () {
        $('.modal-trigger').leanModal({
            complete: function () {
                var $iframe = $('#modal1 iframe');
                $iframe.attr('src', $iframe.attr('src'));
            }
        });
    });

    // function to submit the form after all validation has occurred            
    $scope.submitForm = function ($event) {

        $scope.submitted = true;

        if ($scope[$event.target.name].$valid) {
            var response = $http.post('/api/v1/prelaunch/register', { email: $scope[$event.target.name].email });
            response.success(function (data, status, errors, config) {
                console.log(data);
            });
            response.error(function (data, status, errors, config) {
                console.log(data);
            });
        }
        $event.preventDefault();
    };
});