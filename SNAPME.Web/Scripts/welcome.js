var iisnapApp = angular.module('iisnapApp', []);

// create angular controller
iisnapApp.controller('welcomeController', function ($scope) {

    $(document).ready(function () {
        $('.modal-trigger').leanModal({
            complete: function () {
                var $iframe = $('#modal1 iframe');
                $iframe.attr('src', $iframe.attr('src'));
            }
        });
    });

    // function to submit the form after all validation has occurred            
    $scope.submitForm = function (isValid) {

        // check to make sure the form is completely valid
        if (isValid) {
            alert('our form is amazing');
        }

    };

});