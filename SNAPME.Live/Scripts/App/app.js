var app = angular.module('IISNAP', ['ngMaterial', 'ngSanitize', 'ngCookies']);
app.config(function ($mdThemingProvider) {
    $mdThemingProvider.theme('default')
        .primaryPalette('blue')
        .accentPalette('amber')
});

app.controller('MainController', function ($scope, $mdDialog, $cookies, $timeout) {
    $scope.communities = [
        {
            id: 1, name: 'Beauty', icon: null, banner: { nameClass: "rightMiddle", url: "http://www.lumene.com/sites/all/themes/lumene/images/stellar_look_campaign/model_banner.jpg", position: "50% 88%" },
            products: [
            { name: 'ghd AIR® HAIRDRYER', description: 'A SALON FINISH 2X FASTER', snaps: 13, current_price: 87, price: 99, target: 69, progress: 40, image_mode: 'cover', image: 'http://www.ghdhair.com/medias/sys_master/products/8811542118430/Hercules_above.jpg' },
            { name: 'Brow Powder Duo', description: 'By ANASTASIA', snaps: 3, current_price: 95.1, price: 99, target: 69, progress: 13, image_mode: 'contain', image: 'http://dy6g3i6a1660s.cloudfront.net/Oms82f5Ssy9smF0Ued1uTwR-BH4/zb_p.jpg' },
            { name: 'Filmstar Killer Cheekbones', description: 'By CHARLOTTE TILBURY', snaps: 19, current_price: 82.5, price: 99, target: 69, progress: 55, image_mode: 'contain', image: 'http://dy6g3i6a1660s.cloudfront.net/TA7gGCNO3yXeRyduGhIxtwR-BH4/zb_p.jpg' },
            { name: 'Signature Kabuki Set', description: 'By RAE MORRIS', snaps: 12, current_price: 87.5, price: 99, target: 69, progress: 38.5, image_mode: 'contain', image: 'http://dy6g3i6a1660s.cloudfront.net/-fu6I8qh2AhSjquMZJcr8AR-BH4/zb_p-66/rae-morris-signature-kabuki-set.jpg' }
            ],
            featured_product: {
                name: 'OROGOLD Cosmetics 24K Deep Peeling Gel', video_url: 'https://www.youtube.com/embed/OQba3dS17xw?rel=0',
                review: '<p>Traveling really wreaks havoc on your skin, but it\'s often hard to know how to combat the situation. Sometimes, it\'s a matter of not wanting to tear up your skin while vacationing . . . you don\'t want to ravage your skin in the name of treatment before all those selfies and friends pics. Other issues include a difficulty in finding luxury skincare suitable for 3-1-1 carry-on travel bags. What will renew and replenish and still add to sumptuousness of your relaxation? Orogold! I was happy to be hosted to experience it.</p><p>This line will make you feel like royalty. It\'s made with 24K pure gold from Italy! A certificate is included in the gorgeous product presentation. Why gold? This is what they say:</p><p class="ii-quote">Hints of gold being added to food and skin care routines can be seen all through history. Ancient civilizations added gold leaves to their food to show hospitality while entertaining their guests. A more direct use of gold in skin care can be traced to the Egyptian civilization. Cleopatra, the famous Egyptian queen, known for goddess like beauty, was rumored to use gold face masks to preserve the her surreal beauty and flawless complexion. Mentions of gold in skin care can also be found in ancient Chinese civilizations, many empresses used crushed gold in their skin care routines to enhance their beauty.Today, gold is used in a variety of skin care applications to deal with a number of skin conditions. Gold is also known to be used in several different types of medical applications including being used in a process known as chrysotherapy. It comes as no secret that spas and major cosmetic brands across the globe have begun to include and concentrate on gold as a skin care treatment once again, following in the steps of the ancients. According to the World Gold Council, market development organization for the gold industry, gold leaf facial treatments have become extremely popular in Asian countries such as Japan. Advocates state that these treatments can help to rejuvenate your skin and reduce fine lines and wrinkles. They also explain that the major reason behind gold’s success as a skin care ingredient is that it manages to lock the moisture in your skin and help to keep your skin firm.</p><p>Normally, I\'m a little terrified of peels and brighteners: many make my skin blister. The 24K Deep Peel is velvety and gentle . . .it feels like honey upon application on dry skin. You gently rub off the dry, dulling skin on your top layer, making you glow -- but not in a burning, irritated way. Your skin will feel satiny and makeup applies beautifully.</p><p>This product is sized perfectly for 3-1-1 carry-on travel bags!</p><p class="ii-quote">24K Deep Peeling is designed to provide a powerful facial cleanse by removing a thin layer of dry cells to reveal ultra-smooth and youthful skin. It is specially formulated to gently polish, renew, and revitalize the skin without stripping away vital oils or causing irritation. 24K Deep Peeling is formulated with 24K Gold along with ingredients such as, Ascorbic Acid (Vitamin C), Tocopheryl Acetate (Vitamin E) and Green Tea.</p>'
            }
        },
        {
            id: 1, name: 'Tech', icon: null, banner: { nameClass: "leftMiddle", url: "https://www.webnms.com/iot/images/smart-home-banner.jpg", position: "50% 88%" },
            products: [
                { name: 'Autel Robotics X-Star Premium Drone', description: '4K Camera, 1.2-Mile HD Live View & Hard Case', snaps: 0, current_price: 1299, price: 1299, target: 799, progress: 0, image_mode: 'cover', image: 'http://ecx.images-amazon.com/images/I/71KbetVv5IL._SY500_.jpg' },
                //{ name: 'August Smart Lock', description: 'Keyless Home Entry with Your Smartphone', snaps: 4, current_price: 176.44, price: 199, target: 129, progress: 32, image_mode: 'contain', image: 'http://ecx.images-amazon.com/images/I/61PETdlWajL._SL1000_.jpg' },
                { name: 'ICEORB', description: 'Portable Wireless Floating Bluetooth Speaker', snaps: 10, current_price: 135.67, price: 149.99, target: 79, progress: 21, image_mode: 'contain', image: 'http://ecx.images-amazon.com/images/I/51dOi4Mr6wL.jpg' },
                { name: 'GoPro HERO4 BLACK', description: 'By GOPRO', snaps: 2, current_price: 420.54, price: 429, target: 320, progress: 7, image_mode: 'cover', image: 'http://ecx.images-amazon.com/images/I/71X7ZijN3yL._SL1500_.jpg' },
                { name: 'Silicon Power 64GB Jewel J80', description: 'USB 3.0 Flash Drive, Titanium', snaps: 22, current_price: 15.43, price: 17.99, target: 9.99, progress: 33, image_mode: 'contain', image: 'http://ecx.images-amazon.com/images/I/61G4WcPob-L._SL1500_.jpg' },
                { name: 'ASUS VM42-S075V Desktop', description: 'By ASUS', snaps: 12, current_price: 199.77, price: 249, target: 170, progress: 63, image_mode: 'contain', image: 'http://ecx.images-amazon.com/images/I/41QmEpAHnLL.jpg' },
                { name: 'Dell Gaming S2716DG 27" Screen LED-Lit Monitor', description: 'By DELL', snaps: 27, current_price: 531.42, price: 579.99, target: 450, progress: 38, image_mode: 'contain', image: 'http://ecx.images-amazon.com/images/I/81hLfv-lewL._SL1500_.jpg' }
            ],
            featured_product: {
                name: 'August Smart Lock', video_url: 'https://www.youtube.com/embed/rSgXyMJT8C8?rel=0',
                review: '<p>The August Smart Lock nails most of the things I\'d want in a connected door lock. It\'s easy to install. It looks good (yes, despite its size) and the August app (in iOS, at least) gives you the right balance between flexibility and keeping things safe and simple. The fact that it works with your existing deadbolt is also a plus.</p><p>The $250 asking price (international availability pending) puts August on the more expensive end of the smart-lock spectrum. The August Smart Lock also suffers from the same limitations as other connected locks. Because it\'s a Bluetooth-only device, controlling the lock with your phone when you\'re out of range requires either a compatible third-party hub or August\'s new $50 Connect accessory that you\'ll need to buy separately. Bluetooth also means you\'ll suffer from minor but still annoying lag when you first open the app to interact with the Smart Lock.</p>'
            }
        }
    ];

    $scope.mysnapbox = [
        { name: 'FUGOO Style - Portable Bluetooth Speaker', url: 'http://www.amazon.com/dp/B00IBJ3MWU', wants: 34, image_mode: 'contain', image: 'http://ecx.images-amazon.com/images/I/81NdkRIsfGL._SL1500_.jpg' },
        { name: 'Samsung Gear VR - Virtual Reality Headset', url: 'http://www.amazon.com/dp/B016OFYGXQ', wants: 12, image_mode: 'contain', image: 'http://ecx.images-amazon.com/images/I/916%2Bqqjdl7L._SL1500_.jpg' },
        { name: 'RUIMIO Contour Kit and Highlighting Palette', url: 'http://www.amazon.com/dp/B019MNL73Y', wants: 55, image_mode: 'contain', image: 'http://ecx.images-amazon.com/images/I/61wGiyjF%2BTL._SL1200_.jpg' }
    ];

    $scope.fbLogin = function () {
        $('#frmFbLogin').submit();
    }

    $scope.logout = function () {
        $('#frmLogout').submit();
    }

    $scope.showProduct = function (product) {
        document.location.href = '/product/' + product.name;
    }

    $scope.showExternalProduct = function (product) {
        window.open(product.url);
    }

    $scope.showHowToDialog = function (ev) {
        $mdDialog.show({
            controller: HowToDialogController,
            templateUrl: '/Modals/Howto',
            parent: angular.element(document.body),
            //targetEvent: ev,
            clickOutsideToClose: false
        });
    };

    if (!$cookies.get('ht') && window._au) {
        angular.element(document).ready(function () {
            angular.element(btnHowTo).triggerHandler('click');
        });
    }
});

app.filter('trustAsResourceUrl', ['$sce', function ($sce) {
    return function (val) {
        return $sce.trustAsResourceUrl(val);
    };
}])


function HowToDialogController($scope, $mdDialog, $cookies) {

    $cookies.put('ht', 't');

    $scope.tabs = [
        { title: "What?", btn: "Why?", content: "iiSnap is a community shop that lets you buy with like minded and save on everything you buy." },
        { title: "Why?", btn: "When?", content: "Shopping as a community means you can share your passion with others and shop with confidence." },
        { title: "When?", btn: "How?", content: "Discover new snap sales weekly, upcoming products can be view on the coming soon section on the site." },
        { title: "How?", btn: "Got Ya!", content: "Join a sale you fancy and invite others to drive the price down.<br/><br/>When time is up you only pay the closing price." }
    ];
    $scope.selectedIndex = 0;

    $scope.nextSlide = function () {
        $scope.selectedIndex++;
        if ($scope.selectedIndex == $scope.tabs.length) {
            $mdDialog.hide();
        }
    }
}