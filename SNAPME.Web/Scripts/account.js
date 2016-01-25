// create angular controller
iisnapApp.controller('accountController', function ($scope, $http, $cookies, sharedDataService) {

    $scope.friends_activities = [{ "user_id": "56735468b207ab4148aba379", "user_name": "Michael Bar", "type": 3, "text": "Michael Bar joined the sale", "created_on": "2 weeks ago", "data": { "price": 16418 } }];
    $scope.hasActivities = true;
    $scope.system_messages = [];

    $scope.init = function (name) {
        sharedDataService.set('user', { name: name });
    }

    $scope.logoff = function ($event) {
        $event.preventDefault();
        logoutForm.submit();
    };

    $scope.inviteFriends = function (id) {
        window.FB.ui({
            method: 'send',
            link: 'http://www.iisnap.com/welcome?ref=' + id
        });
    }
});