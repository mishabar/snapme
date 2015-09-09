var iisnapApp = angular.module('iisnapApp', ['ngCookies']);

// create angular controller
iisnapApp.controller('welcomeController', function ($scope, $http, $cookies) {

    $scope.submitted = false;

    $(document).ready(function () {
        $('.modal-trigger').leanModal({
            complete: function () {
                var $iframe = $('#about iframe');
                $iframe.attr('src', $iframe.attr('src'));
            }
        });
    });

    // function to submit the form after all validation has occurred            
    $scope.submitForm = function ($event) {

        $scope.submitted = true;

        if ($scope[$event.target.name].$valid) {
            var response = $http.post('/api/v1/prelaunch/register', { email: $scope[$event.target.name].email.$modelValue, referrer: $cookies.get('iisref') || '' });
            response.success(function (data, status, errors, config) {
                var $form = $('<form id="pl" action="/Welcome/PreLaunch" method="post"><input type="hidden" name="key" /></form>');
                $form.find('input').val(data);
                $('body').append($form);
                $form.submit();
            });
            response.error(function (data, status, errors, config) {
                console.log(data);
            });
        }
        $event.preventDefault();
    };
});