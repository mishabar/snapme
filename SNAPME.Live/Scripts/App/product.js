app.controller('ProductController', function ($scope, $timeout, $interval, $filter, $mdDialog) {
    $scope.product = {};
    $scope.potentialSnappers = [{
        "name": "Dan Amiga",
        "id": "502202799"
    },
            {
                "name": "Maoz Gelbart",
                "id": "527666654"
            },
            {
                "name": "Eran Shmueli",
                "id": "554877777"
            },
            {
                "name": "Maxim Zabuti",
                "id": "598089799"
            },
            {
                "name": "Haggai Shachar",
                "id": "618883823"
            },
            {
                "name": "Addi Horowitz",
                "id": "622911288"
            },
            {
                "name": "Lior Aharoni",
                "id": "626048288"
            },
            {
                "name": "Tal Yaniv",
                "id": "644981956"
            },
            {
                "name": "Avner Shahar Kashtan",
                "id": "654683707"
            },
            {
                "name": "Liad Hacmon",
                "id": "667513213"
            },
            {
                "name": "Yossi Melamed",
                "id": "683016984"
            },
            {
                "name": "Gabi Mor",
                "id": "683718290"
            },
            {
                "name": "Shahar Nechmad",
                "id": "684266814"
            },
            {
                "name": "Yaniv Hakim",
                "id": "694636911"
            },
            {
                "name": "Moran Katan",
                "id": "699699063"
            },
            {
                "name": "Pini Mogilevsky",
                "id": "702376921"
            },
            {
                "name": "Shai Brumer",
                "id": "707594314"
            },
            {
                "name": "Shalom Gibly",
                "id": "789424074"
            },
            {
                "name": "Kate Lia Mendelson",
                "id": "10153095591534325"
            },
            {
                "name": "Mario Renato Uriarte Amaya",
                "id": "845639484"
            },
            {
                "name": "Ilan Levy",
                "id": "10204500895902180"
            },
            {
                "name": "Shay Erlichmen",
                "id": "100001588297299"
            },
            {
                "name": "Lance Deacon",
                "id": "100002177855421"
            }];
    $scope.products = [
        {
            id: 0,
            name: 'ghd AIR® HAIRDRYER', description: 'A SALON FINISH 2X FASTER', snaps: 13, current_price: 87, price: 99, target: 69, progress: 40,
            full_details: 'Get gorgeous hair in half the time* with the ghd air® professional hairdryer. Its powerful 2,100W professional-strength motor and patented removable air filter deliver high pressure air flow for super-fast drying, while advanced ionic technology reduces frizz and flyaways to give a smooth salon-style finish in half the time*. Variable power and temperature controls, plus the choice of two nozzles, allow you to tailor your blow-dry to your hair type, while a cool shot button helps lock your finished style in place with a blast of cold air. The ghd air® is also a breeze to use thanks to an ergonomic design that makes it comfortable to hold for both left and right-handed users, and a 3m long power cable that gives total flexibility as you style. The ghd air® is compatible with the ghd air® diffuser.',
            images: ['http://www.ghdhair.com/medias/sys_master/products/8811542118430/Hercules_above.jpg',
                    'http://www.ghdhair.com/medias/sys_master/products/8811542249502/Hercules_3_4_view_logo2.jpg',
                    'http://www.ghdhair.com/medias/sys_master/products/8797240623134/248_Image_1_Hero.jpg',
                    'http://www.ghdhair.com/medias/sys_master/products/8797243211806/248_Image_3_Hero.jpg',
                    'http://www.ghdhair.com/medias/sys_master/products/8797241409566/248_Image_4_Hero.jpg'],
            images_mode: 'cover',
            snaps: [{
                "name": "Dan Amiga",
                "id": "502202799"
            },
            {
                "name": "Maoz Gelbart",
                "id": "527666654"
            },
            {
                "name": "Eran Shmueli",
                "id": "554877777"
            },
            {
                "name": "Maxim Zabuti",
                "id": "598089799"
            },
            {
                "name": "Haggai Shachar",
                "id": "618883823"
            }]
        },
        {
            id: 0,
            name: 'Brow Powder Duo', description: 'By ANASTASIA', snaps: 3, current_price: 95.1, price: 99, target: 69, progress: 13,
            full_details: 'Suggested for light to dark blonde hair. <br/>Use Brow Powder Duo to achieve beautiful natural brows. Brow Powder Duo is a lightweight, sheer to medium coverage powder that was designed to be smudge proof. Each Brow Powder Duo comes with two shades to customize the perfect brow. <br/><h4>Ingredients:</h4>Talc, Methicone, Ethylhexyl Palmitate, Zinc Stearate, Magnesium Myristate, Boron Nitride, Nylon-12, Silica, Sodium Dehydroacetate, Glycine Soja(soybean) Oil, Ascorbyl Palmitate, Tocopherol, Methylparaben, Propylparaben, Butylparaben, May Contain/peut Contenir: (+/-) Mica, Iron Oxides (ci 77499, Ci 77492, Ci 77491), Titanium Dioxide (ci 77891), Bismuth Oxychloride(ci 77163)',
            images: ['http://dy6g3i6a1660s.cloudfront.net/Oms82f5Ssy9smF0Ued1uTwR-BH4/zb_p.jpg',
               'http://dy6g3i6a1660s.cloudfront.net/cjFcyy38hXD-M5p-KSrg9QR-BH4/zb_p.jpg',
               'http://dy6g3i6a1660s.cloudfront.net/oNqyz5R9Jbd0r9MjZGb7aQR-BH4/zb_p.jpg',
               'http://dy6g3i6a1660s.cloudfront.net/h2OJiVuj9l3EwxvSBJ3SWQR-BH4/zb_p.jpg',
               'http://dy6g3i6a1660s.cloudfront.net/TT1Vs6zBFMylH7mJGCERQAR-BJA/zb_p.jpg'],
            images_mode: 'contain',
            snaps: []
        },
        {
            id: 0,
            name: 'Filmstar Killer Cheekbones', description: 'By CHARLOTTE TILBURY', snaps: 19, current_price: 82.5, price: 99, target: 69, progress: 55,
            full_details: '<p>Everyone deserves Filmstar Killer Cheekbones and this gorgeous compact and brush are two of Charlotte Tilbury\'s favorite products. </p><p>The velvet like texture of Charlotte\'s Filmstar Bronze & Glow helps to easily add contour and dimension to the face. Charlotte\'s beautifully tapered Powder & Sculpt brush is shaped to glide along cheekbones to enhance and define them. This is a luxurious kit for truly killer cheekbones.</p><h4>Ingredients</h4>Talc, Mica, Isononyl Isononanoate, Oryza Sativa (Rice) Starch, Dimethicone, Zea Mays (Corn) Starch, Zinc Stearate, Caprylyl Glycol, Phenoxyethanol, Dimethiconol, Octyldodecyl Stearoyl Stearate, Hexylene Glycol, Polymethylsilsesquioxane, Silica, Tin Oxide, Trimethylsiloxysilicate, Titanium Dioxide (CI 77891), Iron Oxides (CI 77491, CI 77492, CI 77499)',
            images: ['http://dy6g3i6a1660s.cloudfront.net/TA7gGCNO3yXeRyduGhIxtwR-BH4/zb_p.jpg',
               'http://dy6g3i6a1660s.cloudfront.net/-rfZwIDx8fYkpxKCw-lHfgR-BH4/zb_p.jpg',
               'http://dy6g3i6a1660s.cloudfront.net/0hG8Wb5HDjeMiASErygokQR-BH4/zb_p.jpg',
               'http://dy6g3i6a1660s.cloudfront.net/c2CMMXe8hqRe3j3FaeF2pgR-BH4/zb_p.jpg',
               'http://dy6g3i6a1660s.cloudfront.net/Vd4zC5rWNO5Ci42DziRUmwR-BH4/zb_p.jpg'],
            images_mode: 'contain',
            snaps: []

        },
        {
            id: 0,
            name: 'Signature Kabuki Set', description: 'By RAE MORRIS', snaps: 12, current_price: 87.5, price: 99, target: 69, progress: 38.5,
            images: ['http://dy6g3i6a1660s.cloudfront.net/-fu6I8qh2AhSjquMZJcr8AR-BH4/zb_p-66/rae-morris-signature-kabuki-set.jpg']
        },
        {
            id: 0,
            name: 'Autel Robotics X-Star Premium Drone', description: '4K Camera, 1.2-Mile HD Live View & Hard Case', snaps: 0, current_price: 1299, price: 1299, target: 799, progress: 0,
            full_details: '<p>4K (Ultra HD), 12-MP camera on a quick-release 3-axis gimbal</p><p>HD Live View (720p streaming up to 1.2 miles away) and autonomous flight modes including follow, orbit, and waypoints via the free Starlink app for iOS or Android (mobile device sold separately)</p><p>Dual GPS/GLONASS outdoor navigation, Securely magnetic interference protection, and the Star point Positioning System for precise flying where GPS signals are unavailable</p><p>Intuitive remote controller with LCD screen for flight information and one-touch buttons for starting the motors, takeoff/landing, hover/pause, and going home</p><p>Included accessories: Premium hard case, 64-GB MicroSD card that can record over 2 hours of 4K video, intelligent battery for up to 25 minutes of flying time per charge, a 1.5-hour fast charger, spare propellers and small parts</p>',
            images: ['http://ecx.images-amazon.com/images/I/71KbetVv5IL._SY500_.jpg',
                'http://ecx.images-amazon.com/images/I/71sqWbb%2BHSL._SL1500_.jpg',
                'http://ecx.images-amazon.com/images/I/71iSeVfyOGL._SL1500_.jpg',
                'http://ecx.images-amazon.com/images/I/71Y02ymRV7L._SL1500_.jpg',
                'http://ecx.images-amazon.com/images/I/71BCKr4WWxL._SL1500_.jpg'],
            images_mode: 'contain',
            snaps: []
        },
                //{ name: 'August Smart Lock', description: 'Keyless Home Entry with Your Smartphone', snaps: 4, current_price: 176.44, price: 199, target: 129, progress: 32, image_mode: 'contain', image: 'http://ecx.images-amazon.com/images/I/61PETdlWajL._SL1000_.jpg' },
        {
            id:0,
            name: 'ICEORB', description: 'Portable Wireless Floating Bluetooth Speaker', current_price: 135.67, price: 149.99, target: 79, progress: 21,
            full_details: '<p>Floating Speaker Orb spinning above a magnetic base</p><p>Special sound guide cone designed to increase 3D surround effect</p><p>Bluetooth speaker floating in the air with 10mm ground clearance</p><p>Built-in NFC function -any smartphone or tablet with NFC function can pair automatically</p><p>Levitation technology user under license from Levitation Arts, Inc.</p>',
            images_mode: 'contain',
            images:
                ['http://ecx.images-amazon.com/images/I/51dOi4Mr6wL.jpg',
                    'http://ecx.images-amazon.com/images/I/71hZJGktqZL._SL1500_.jpg',
                    'http://ecx.images-amazon.com/images/I/712MzmI61CL._SL1500_.jpg',
                    'http://ecx.images-amazon.com/images/I/71wfE28HTHL._SL1500_.jpg'
                ],
            snaps: []
        },
        {
            id: 0,
            name: 'GoPro HERO4 BLACK', description: 'By GOPRO', snaps: [], current_price: 420.54, price: 429, target: 320, progress: 7, images_mode: 'contain',
            full_details: 'HERO4 Black takes Emmy Award-winning GoPro performance to the next level with our best image quality yet, plus a 2x more powerful processor that delivers super slow motion at 240 frames per second. Incredible high-resolution 4K30 and 2.7K60 video combines with 1080p120 and 720p240 slow motion to enable stunning, immersive footage of you and your world. Protun settings for both photos and video unlock manual control of Color, ISO Limit, Exposure and more. Waterproof to 131 feet (40 meter) with 12MP photos at 30 frames per second and improved audio, HERO4 Black is the ultimate life-capture solution for those who demand the best.',
            images: [
                'http://ecx.images-amazon.com/images/I/71X7ZijN3yL._SL1500_.jpg',
                'http://ecx.images-amazon.com/images/I/61ctLTlykCL._SL1500_.jpg',
                'http://ecx.images-amazon.com/images/I/71WBNcetR1L._SL1500_.jpg',
                'http://ecx.images-amazon.com/images/I/51IZQSaxKBL._SL1500_.jpg',
                'http://ecx.images-amazon.com/images/I/51vgqsrvQSL._SL1500_.jpg'
            ]
        },
        {
            id: 0, name: 'Silicon Power 64GB Jewel J80', description: 'USB 3.0 Flash Drive, Titanium', snaps: [], current_price: 15.43, price: 17.99, target: 9.99, progress: 33,
            full_details: 'Jewel J80 is a USB 3.0 flash drive that incorporates functionality and capability in a superb design. Jewel J80 stands out by its exquisite zinc alloy metal casing and its elegant titanium color. It features an ergonomic circular shape design at the end of the drive that allows more comfortable usage and grasp, it can be perfectly fitted on a key chain or in a bag holder. Equipped with a Super Speed USB 3.0 interface, Jewel J80 enables a blazing fast data transfer that meets the demands for high speed and real efficiency. Jewel J80 utilizes the advanced Chip on Board (COB) packaging technology, making it waterproof, shockproof and dust proof. Jewel J80 comes with a life time warranty, SP Widget application software which provides seven major back up and Recuva recovery tool.',
            images_mode: 'contain', images: [
                'http://ecx.images-amazon.com/images/I/61G4WcPob-L._SL1500_.jpg',
                'http://ecx.images-amazon.com/images/I/61QvYrKO0eL._SL1024_.jpg',
                'http://ecx.images-amazon.com/images/I/51ZqsVHaHVL._SL1024_.jpg',
                'http://ecx.images-amazon.com/images/I/61r1bym7dQL._SL1024_.jpg',
                'http://ecx.images-amazon.com/images/I/61E%2BYhtmyRL._SL1024_.jpg'
                ]
        },
        {
            id: 0, name: 'ASUS VM42-S075V Desktop', description: 'By ASUS', snaps: [], current_price: 199.77, price: 249, target: 170, progress: 63, images_mode: 'contain',
            full_details: '<p>4th Generation Intel Celeron® 2957U (Haswell-R) processor</p><p>4GB RAM, 500G Storage with 100G FREE webstorage for 1 year</p><p>802.11 ac, BT4.0, and integrated speakers included (2 x 2W)</p><p>4 USB3.0 Ports and 4-in-1 card reader, HDMI and DP</p><p>Small, portable with stylish design and fully configured with Windows 8.1</p>',
            images: [
                'http://ecx.images-amazon.com/images/I/41QmEpAHnLL.jpg',
                'http://ecx.images-amazon.com/images/I/41nShFBeaVL.jpg',
                'http://ecx.images-amazon.com/images/I/41zTwRbgPsL.jpg',
                'http://ecx.images-amazon.com/images/I/31Xe0r0B1yL.jpg',
                'http://ecx.images-amazon.com/images/I/41garP9D4FL.jpg'
            ]
        },
        {
            id: 0, name: 'Dell Gaming S2716DG 27" Screen LED-Lit Monitor', description: 'By DELL', snaps: [], current_price: 531.42, price: 579.99, target: 450, progress: 38, images_mode: 'contain',
            full_details: 'Unrivaled gameplay is yours with the Dell 27 Monitor for serious gamers. Experience sharp, un-distorted moving images with NVIDIA G-SYNC and the fastest refresh rate at 144 Hz. Enjoy incredibly swift and responsive action with minimum input lag at an extremely rapid 1ms panel response time. Lose yourself in the details of stunning QHD (2560x1440 at 144 Hz) resolution - close to two times more details than Full HD.',
            images: [
                'http://ecx.images-amazon.com/images/I/81hLfv-lewL._SL1500_.jpg',
                'http://ecx.images-amazon.com/images/I/71bUf6qWkXL._SL1500_.jpg',
                'http://ecx.images-amazon.com/images/I/71JIP4mSO8L._SL1500_.jpg',
                'http://ecx.images-amazon.com/images/I/61MIvu4E8FL._SL1500_.jpg',
                'http://ecx.images-amazon.com/images/I/81UZedZWbpL._SL1500_.jpg'
                ]
        }
    ];

    $scope.init = function(productName) {
        $scope.product = $filter('filter')($scope.products, { name: productName }, true)[0];
    }

    $scope.joinSale = function (ev) {
        $mdDialog.show({
            locals: { product: $scope.product},
            controller: JoinSaleDialogController,
            templateUrl: '/Sale/' + $scope.product.id + '/Join',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: false
        }).then(function(quantity) {
            addSnapper(quantity, window._me);
        }, function() {
        });;
    }

    $scope.iJoined = function () {
        return $filter('filter')($scope.product.snaps, window._me, true).length == 1;
    }

    $scope.gtXS = function () {
        return screen.availWidth > 599;
    }

    function addSnapper(quantity, snapper) {
        $scope.product.current_price -= (quantity * ($scope.product.price - $scope.product.target) * Math.random() / 100);
        $scope.product.progress = ($scope.product.price - $scope.product.current_price) / ($scope.product.price - $scope.product.target) * 100;
        $scope.product.snaps.push(snapper);
    }

    $timeout(function () {
        $('.hero > a').lightbox();
    }, 200);

    $interval(function () {
        if (Math.random() > 0.5) {
            var idx = Math.floor(Math.random() * ($scope.potentialSnappers.length - 1));
            addSnapper(1, $scope.potentialSnappers[idx]);
            $scope.potentialSnappers.splice(idx, 1);
        }
    }, 7000);
});