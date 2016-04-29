app.controller('SnapBoxAppController', function ($scope) {
    $scope.products = [
        { id: 1, name: 'ASUS VM42-S075V Desktop', image: 'http://ecx.images-amazon.com/images/I/41QmEpAHnLL.jpg', likes: 69, supplier: { name: 'Michael Bar', image: '//graph.facebook.com/10152734377022123/picture?type=square' } },
        { id: 2, name: 'ICEORB', image: 'http://ecx.images-amazon.com/images/I/51dOi4Mr6wL.jpg', likes: 123, supplier: { name: 'Ami Barda', image: '//graph.facebook.com/10152686035091417/picture?type=square' } }
    ];
    $scope.mybox = [];
});