app.controller('SnapBoxAppController', function ($scope, $filter, $mdSidenav) {
    $scope.products = [
        { id: 1, name: 'ASUS VM42-S075V Desktop', image: 'http://ecx.images-amazon.com/images/I/41QmEpAHnLL.jpg', want: true, wants: 69, category: 'Tech', supplier: { name: 'ASUS', image: '/content/images/snapapp/asus.png', category: 'Tech' } },
        { id: 2, name: 'ICEORB', image: 'http://ecx.images-amazon.com/images/I/51dOi4Mr6wL.jpg', want: false, wants: 123, category: 'Tech', supplier: { name: 'ICE', image: '/content/images/snapapp/ice.jpg', category: 'Tech' } },
        { id: 3, name: 'Brow Powder Duo', image: 'http://dy6g3i6a1660s.cloudfront.net/Oms82f5Ssy9smF0Ued1uTwR-BH4/zb_p.jpg', want: false, wants: 77, category: 'Beauty', supplier: { name: 'SEPHORA', image: '/content/images/snapapp/sephora.gif', category: 'Beauty' } }
    ];
    $scope.alerts = [];
    $scope.mybox = $filter('filter')($scope.products, { want: true });

    $scope.categories = [{ id: 1, name: 'Beauty' }, { id: 2, name: 'Tech' }];
    $scope.selectedCategory = 0;

    $scope.toggleSidenav = function () {
        $mdSidenav('sideNav').toggle();
    }

    $scope.encodeUriComponent = function (value) {
        return encodeURIComponent(value);
    }

    $scope.filteredProducts = $filter('filter')($scope.products, { category: $scope.categories[$scope.selectedCategory].name });

    $scope.selectCategory = function (idx) {
        $scope.selectedCategory = idx;
        $scope.filteredProducts = $filter('filter')($scope.products, { category: $scope.categories[$scope.selectedCategory].name });
        $scope.toggleSidenav();
    }

    $scope.toggleBox = function (product) {
        var idx = $scope.mybox.indexOf(product);
        if (idx == -1) {
            $scope.mybox.push(product);
            product.want = true;
            product.wants++;
        } else {
            $scope.mybox.splice(idx, 1);
            product.want = false;
            product.wants--;
        }
    }
});