// create angular controller
iisnapApp.controller('userRewardsController', function ($scope, $timeout, accountService) {

    $scope.details = {};
    $scope.rewards = [];
    $scope.menu = 'rewards';
    $scope.profile_image = '/Content/Images/iisnap.ico';
    $scope.types = [
        { title: 'Points earned from purchases', description: 'Every time your complete a purchase on iiSnap you are awarded a certain amount of Snap Points (vary with each sale). You can see the amount of Snap Points you will earn on each sale on the sale page.' },
        { title: 'Points earned from friends invitations', description: 'Every time a friend joins iiSnap using your invitation link, you are awarded additional 25 Snap Points.' },
        { title: 'Points earned from firends\' purchases', description: 'Every time a friend completes a purchase after being invited by you to a sale, you get bonus Snap Points.' },
        { title: 'Points earned from special offers', description: 'Special offers will be presented by iiSnap from time to time and you will be awarded for participation.' }
    ];
    $scope.totalBalance = 0;
    
    $scope.init = function (id, name) {
        $scope.profile_image = '/Account/Image/' + id + '?width=' + 300;
        $scope.details.name = name;
        accountService.getRewards()
            .then(function (response) {
                $.each(response.data, function (i, v) { $scope.totalBalance += v.points });
                $scope.rewards = response.data;
                $timeout(function () { $('.collapsible').collapsible() }, 50);
            },
            function (response) {
                Materialize.toast('Something wen wrong :( Please try refreshing the page.', 2000);
            });
    }
});