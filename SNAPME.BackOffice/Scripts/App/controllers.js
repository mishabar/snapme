app.controller('HomeController', function ($scope) {
});

app.controller('GetStartedController', function ($scope, $cookies, productsService) {
    $scope.selectedTab = 0;
    $scope.categories = [];
    $scope.onlineOptions = ['We have an online store we selling our goods on', 'We are selling on eBay', 'We are selling on Amazon', 'We are selling on online specialty stores', 'We are selling on deals sites like Groupon, Living Social, etc'];
    $scope.merchant = { name: null, email: null, url: null, other_online: [], categories: [], percentage: 0 };
    $scope.submitted = false;

    $scope.switchTab = function (d) {
        $scope.selectedTab += d;
    }

    $scope.canSwitch = function () {
        if ($scope.basicDetailsForm.name) {
            switch ($scope.selectedTab) {
                case 0: return $scope.basicDetailsForm.name.$valid && $scope.basicDetailsForm.email.$valid;
                case 1: return true;
            }
        }
        return false;
    }

    $scope.submitSellerRequest = function () {
        $cookies.put('authorized', true);
        $cookies.put('firstTime', true);
        $scope.submitted = true;
    }

    productsService.getCategories()
        .then(function (r) { $scope.categories = r.data });
});

app.controller('ProductsController', function ($scope, $timeout, $mdSidenav, productsService) {
    $mdSidenav('sidenav-left').close();

    $scope.categories = [];
    $scope.products = [];

    $scope.$on("$routeChangeSuccess", function (scope, next, current) {
        $timeout(function () {
            $('#contentCard md-content').height($('#contentCard').height() - 35 + 'px');
        }, 100);
    });

    $scope.changeCategory = function (category) {
        productsService.getProducts(category)
            .then(function (r) { $scope.products = r.data });
    }

    productsService.getCategories()
        .then(function (r) { $scope.categories = r.data; $scope.changeCategory(r.data[0]) });
});

app.controller('SnapboxController', function ($scope, $timeout, $mdSidenav, productsService) {
    $mdSidenav('sidenav-left').close();

    $scope.categories = [];
    $scope.products = [];
    $scope.sortDirections = [{ d: '-snapbox.likes', name: 'Most Trending', icon: 'trending_up' }, { d: 'snapbox.likes', name: 'Least Trending', icon: 'trending_down' }];
    $scope.currentSortDirection = $scope.sortDirections[0];

    $scope.$on("$routeChangeSuccess", function (scope, next, current) {
        $timeout(function () {
            $('#contentCard md-content').height($('#contentCard').height() - 90 + 'px');
        }, 200);
    });

    $scope.changeCategory = function (category) {
        productsService.getProducts(category)
            .then(function (r) { $scope.products = r.data });
    }

    $scope.sortData = function (sortDirection) {
        $scope.currentSortDirection = sortDirection;
    }

    productsService.getCategories()
        .then(function (r) { $scope.categories = r.data; $scope.changeCategory(r.data[0]) });
});

app.controller('ProductController', function ($scope, $location, $timeout, $sce, $routeParams, productsService) {
    $scope.categories = [];
    $scope.product = {};

    $scope.tinymceOptions = {
        trusted: true,
        plugins: 'image table',
        skin: 'lightgray',
        theme: 'modern',
        height: 530
    };

    $scope.$on("$routeChangeSuccess", function (scope, next, current) {
        $timeout(function () {
            $('#pageContent').height($('body').height() - 100 + 'px');
        }, 100);
    });

    productsService.getCategories()
        .then(function (r) { $scope.categories = r.data });

    if ($routeParams.productId != 0) {
        productsService.getProduct($routeParams.productId)
            .then(function (r) { $scope.product = r.data; $scope.newProduct = false; });
    } else {
        $scope.product = {};
        $scope.newProduct = true;
    }

    $scope.saveProduct = function () {
        productsService.saveProduct($scope.product)
            .then(function (r) {
                if (!r.data.error) {
                    $location.url('/product/' + r.data.id);
                }
            })
    }
});

app.controller('LiveSalesController', function ($scope, $timeout, salesService, productsService) {
    $scope.categories = [];
    $scope.products = {};

    $scope.$on("$routeChangeSuccess", function (scope, next, current) {
        $timeout(function () {
            $('#contentCard md-content').height($('#contentCard').height() - 35 + 'px');
        }, 100);
    });

    $scope.changeCategory = function (category) {
        salesService.getLiveSales(category)
            .then(function (r) { $scope.products = r.data });
    }

    productsService.getCategories()
        .then(function (r) { $scope.categories = r.data; $scope.changeCategory(r.data[0]) });
});

app.controller('EndedSalesController', function ($scope, $timeout, salesService, productsService) {
    $scope.categories = [];
    $scope.products = {};

    $scope.$on("$routeChangeSuccess", function (scope, next, current) {
        $timeout(function () {
            $('#contentCard md-content').height($('#contentCard').height() - 35 + 'px');
        }, 100);
    });

    $scope.changeCategory = function (category) {
        salesService.getLiveSales(category)
            .then(function (r) { $scope.products = r.data });
    }

    productsService.getCategories()
        .then(function (r) { $scope.categories = r.data; $scope.changeCategory(r.data[0]) });
});

app.controller('SettingsController', function ($scope) {
});

app.controller('ScheduleSaleDialogController', function ($scope, $mdDialog, product) {
    $scope.product = product;
    $scope.minDate = new Date();
    $scope.minDate.setDate($scope.minDate.getDate() + 1);

    $scope.cancel = function () {
        $mdDialog.cancel();
    };

    $scope.scheduleSale = function () {
        $scope.cancel();
    }

    $scope.canSubmit = function () {
        return $scope.scheduleForm.$invalid;
    }
});