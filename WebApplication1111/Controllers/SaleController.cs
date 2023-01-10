using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1111.Dac;
using WebApplication1111.Models;

namespace WebApplication1111.Controllers
{
    public class SaleController : Controller
    {
        public ActionResult index()
        {
            if (Request.Cookies["user"] == null)
            {
                return RedirectToAction("index", "home");
            }
            var userID = Request.Cookies["user"].Value;
            ViewBag.prodList = ProductDac.Get(userID);
            ViewBag.Customers = AccountDac.Get(userID);
            return View();
        }

        [HttpPost]
        public ActionResult index(SaleModel saleLst)
        {
            var userID = Request.Cookies["user"].Value;
            var allPurchaseLst = SaleDac.GetAll();

            saleLst.Code = allPurchaseLst.Count() > 0 ?
                (int.Parse(allPurchaseLst[allPurchaseLst.Count() - 1].Code) + 1).ToString() : "0";
            // Debug.WriteLine($"{saleLst.Product}, {purchaseLst.Account}, {purchaseLst.Count}, {userID}, {purchaseLst.Code}");

            SaleDac.Create(saleLst.Product, saleLst.Count, userID, saleLst.Code, saleLst.Account);
            return Json(new { message = "등록 완료" }, JsonRequestBehavior.AllowGet);
        }


        // 조회 페이지
        public ActionResult SaleList()
        {
            if (Request.Cookies.Count == 0 || Request.Cookies["user"] == null)
            {
                return RedirectToAction("index", "home");
            }

            var userID = Request.Cookies["user"].Value;
            ViewBag.Sale = SaleDac.Get(userID);
            ViewBag.Products = ProductDac.Get(userID);
            ViewBag.Customers = AccountDac.Get(userID);
            return View();
        }

        [HttpPost]
        public ActionResult deleteSale(SaleModel saleItem)
        {
            SaleDac.Delete(saleItem.Code);
            return Json(new { message = "삭제 완료" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult updateSale(SaleModel saleItem)
        {
            var userID = Request.Cookies["user"].Value;
            saleItem.UserID = userID;

            SaleDac.Update(saleItem);
            return Json(new { message = "업데이트 완료" }, JsonRequestBehavior.AllowGet);
        }
    }
}