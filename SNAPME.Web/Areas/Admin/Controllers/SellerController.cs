using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SNAPME.Services.Interfaces;
using SNAPME.Tokens;
using SNAPME.Tokens.Admin;

namespace SNAPME.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SellerController : Controller
    {
        private ISellerService _sellerService;
        private IIDService _idService;

        public SellerController(ISellerService sellerService, IIDService idService)
        {
            _sellerService = sellerService;
            _idService = idService;
        }

        // GET: Admin/Seller
        public ActionResult Index()
        {
            return View(_sellerService.GetAll());
        }

        // GET: Admin/Seller
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Seller
        [HttpPost]
        public ActionResult Create(SellerToken token)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _sellerService.Save(token);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.ToString();
                    return View(token);
                }
            }
            return View(token);
        }

        // GET: Admin/Seller/Edit/{id}
        public ActionResult Edit(string id)
        {
            return View(_sellerService.GetById(id));
        }

        // GET: Admin/Seller/Details/{id}
        public ActionResult Details(string id)
        {
            return PartialView("_Details", _sellerService.GetById(id));
        }

        // GET: Admin/Seller/Products/{id}
        public ActionResult Products(string id)
        {
            return PartialView("_Products", _sellerService.GetAllProducts(id));
        }

        // GET: Admin/Seller/Sales/{id}
        public ActionResult Sales(string id)
        {
            return PartialView("_Sales");
        }

        // GET: Admin/Seller/Products/{id}
        public ActionResult Product(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return PartialView("_Product", new ProductToken { is_draft = true, id =  _idService.GenerateId() });  
            }
            else
            {
                return PartialView("_Product", _sellerService.GetProduct(id));
            }
        }
    }
}