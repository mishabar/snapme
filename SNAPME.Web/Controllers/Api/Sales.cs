using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SNAPME.Tokens;

namespace SNAPME.Web.Controllers.Api
{
    public static class Sales
    {
        public static SaleToken[] sales = new SaleToken[] {
                    new SaleToken{ id = "55b817638e659b2438f1df4e", name = "Bose SoundLink Mini", banner_image_url = "http://www.rgbdirect.co.uk/ProductImages/BOSE%C2%AE/big/SoundLink_Mini_Carbon.png", banner_url = "https://media.timeout.com/images/101659821/image.jpg", image_url = "https://40.media.tumblr.com/0930af123f313f757bb6a6c1f94ec1d1/tumblr_nrbvuyzpKU1svko7io1_500.jpg", msrp= 175.00, target = 129.00, price = 175.00, drops = 0, progress = 0, summary = "Take the sound wherever you go!", likes = true, favors = false, has_activity = true, is_featured = true, points = 12 },
                    new SaleToken{ id =  "1234567891", name =  "Intel NUC", banner_image_url = "http://www.techzone.lk/wp-content/uploads/2014/11/up18.png", banner_url = "http://www.gadgetreview.com/wp-content/uploads/2013/09/TV-Watching.jpg", image_url =  "http://www.techzone.lk/wp-content/uploads/2014/11/up18.png", msrp =  499.99, target =  129.99, price = 499.99, drops =  0, progress =  0, summary = "Stunning visuals and edge-of-your-seat performance in an ultra-small package.", likes = true, favors = true, has_activity = false, is_featured = true, points = 15 },
                    new SaleToken{ id =  "1234567892", name =  "KitchenAid Stand Mixer", image_url =  "http://www.theproductpedia.com/wp-content/uploads/2013/11/kitchenaid-stand-mixer.jpg", msrp =  125.00, target =  79.00, price =  125.00, drops =  0, progress =  0, summary = "Versatility to help you with any recipe!", likes = false, favors = false, has_activity = false, is_featured = false, points = 10 },
                    new SaleToken{ id =  "1234567893", name =  "KitchenAid Ceramic 4.2 - Quart Casserole Dish with Lid", image_url =  "http://www.kitchenaid.com/digitalassets/KBLR42CRER/Standalone_1175X1290.jpg", msrp =  99.00, target =  69.00, price = 99.00, drops =  0, progress =  0, summary = "Trap in moisture to keep your dishes piping hot and tender, all the way to the table.", likes = false, favors = false, is_featured = false, points = 5 },
                    new SaleToken{ id = "1234567894", name = "2015 Maxi Cosi Pria 70 Convertible Car Seat", image_url = "http://ecx.images-amazon.com/images/I/91%2Bl0jWoV8L._SX522_.jpg", summary = "For adventurous little nomads", msrp = 274.99, target = 165.00, price = 274.99, drops = 0, progress = 0, likes = false, favors = false, has_activity = false, is_featured = false, points = 14 },
                    new SaleToken{ id = "1234567895", name = "Canon EOS 70D Digital SLR Camera", image_url = "http://ecx.images-amazon.com/images/I/81mOhBdCE4L._SX522_.jpg", msrp = 999.00, target = 550.00, price = 999.00, drops = 0, progress = 0, has_activity = true, likes = true, favors = false, points = 25, is_featured = false, summary = "The 70D is an excellent blend of control and quality"}
            };

        
    }
}