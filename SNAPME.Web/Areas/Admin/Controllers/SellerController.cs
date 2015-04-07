using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SNAPME.Services.Interfaces;
using SNAPME.Tokens;
using SNAPME.Tokens.Admin;
using SNAPME.Web.Areas.Admin.Models;

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
            return View(new SellerToken { id = null });
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
                    if (Request.IsAjaxRequest())
                    {
                        return Json(new { url = "/Admin/Seller" });
                    }
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

        // GET: Admin/Seller/Product/{id}
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

        // POST: Admin/Seller/Product
        [HttpPost]
        public JsonResult Product(ProductToken product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _sellerService.SaveProduct(product);
                    return Json(product);
                }
                catch (Exception ex)
                {
                    return Json(new { error = ex.Message });
                }
            }

            return Json(new { error = "Bad Request" });
        }

        // POST: Admin/Seller/ProductImage
        [HttpPost]
        public JsonResult ProductImage(ProductImageModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _sellerService.SaveProductImage(model.id, model.image, model.idx);
                    return Json(new { });
                }
                catch (Exception ex)
                {
                    return Json(new { error = ex.Message });
                }
            }

            return Json(new { error = "Bad Request" });
        }
    }
}