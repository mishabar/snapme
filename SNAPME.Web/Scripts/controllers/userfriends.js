// create angular controller
iisnapApp.controller('userFriendsController', function ($scope, $timeout, accountService) {

    $scope.details = {};
    $scope.friends = [];
    $scope.menu = 'friends';
    $scope.profile_image = '/Content/Images/iisnap.ico';
    
    $scope.totalBalance = 0;
    
    $scope.init = function (id, name) {
        $scope.profile_image = '/Account/Image/' + id + '?width=' + 300;
        $scope.details.name = name;
        $scope.details.id = id;
        accountService.getFriends()
            .then(function (response) {
                $scope.friends = response.data;
            },
            function (response) {
                Materialize.toast('Something wen wrong :( Please try refreshing the page.', 2000);
            });
    }

    $scope.inviteFriends = function () {
        window.FB.ui({
                method: 'send',
                link: 'http://www.iisnap.com/welcome?ref=' + $scope.details.id
            });
    }
});