// create angular controller
iisnapApp.controller('userFavoritesController', function ($scope, $timeout, accountService) {

    $scope.details = {};
    $scope.favorites = [];
    $scope.menu = 'favorites';
    $scope.profile_image = '/Content/Images/iisnap.ico';
    
    $scope.init = function (id, name) {
        $scope.profile_image = '/Account/Image/' + id + '?width=' + 300;
        $scope.details.name = name;
        accountService.getFavorites()
            .then(function (response) {
                $scope.favorites = response.data;
            },
            function (response) {
                Materialize.toast('Something wen wrong :( Please try refreshing the page.', 2000);
            });
    }

    $scope.openProduct = function (name, id) {
        var parts = name.split(' ');
        parts.push(id);
        document.location.href = '/product/' + parts.join('-');
    };
});