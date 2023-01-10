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
    public class PurchaseController : Controller
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
        public ActionResult index(PurchaseModel purchaseLst)
        {
            var userID = Request.Cookies["user"].Value;
            var allPurchaseLst = PurchaseDac.GetAll();

            purchaseLst.Code = allPurchaseLst.Count() > 0 ?
                (int.Parse(allPurchaseLst[allPurchaseLst.Count() - 1].Code) + 1).ToString() : "0";
            Debug.WriteLine($"{purchaseLst.Product}, {purchaseLst.Account}, {purchaseLst.Count}, {userID}, {purchaseLst.Code}");

            PurchaseDac.Create(purchaseLst.Product, purchaseLst.Count, userID, purchaseLst.Code, purchaseLst.Account);
            return Json(new { message = "등록 완료" }, JsonRequestBehavior.AllowGet);
        }


        // 구매 조회페이지
        public ActionResult PurchaseList()
        {
            if (Request.Cookies.Count == 0 || Request.Cookies["user"] == null)
            {
                return RedirectToAction("index", "home");
            }
            var userID = Request.Cookies["user"].Value;
            ViewBag.Purchases = PurchaseDac.Get(userID);
            ViewBag.Products = ProductDac.Get(userID);
            ViewBag.Customers = AccountDac.Get(userID);
            return View();
        }

        [HttpPost]              // 삭제
        public ActionResult DeletePurchase(PurchaseModel purchaseItem)
        {
            PurchaseDac.Delete(purchaseItem.Code);
            return Json(new { message = "삭제 완료" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]              // 업데이트
        public ActionResult updatePurchase(PurchaseModel purchaseItem)   // 조회
        {
            var userID = Request.Cookies["user"].Value;
            purchaseItem.UserID = userID;

            PurchaseDac.Update(purchaseItem);
            return Json(new { message = "업데이트 완료" }, JsonRequestBehavior.AllowGet);
        }


    }
}