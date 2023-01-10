using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1111.Models;
using WebApplication1111.Dac;
using System.Diagnostics;
using System.Net;

namespace WebApplication1111.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult index()
        {
            if (Request.Cookies["user"] == null)
            {
                return RedirectToAction("index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult index(ProductModel prod)
        {
            var userID = Request.Cookies["user"].Value;
            var product = ProductDac.Get(userID);

            prod.Code = product.Count() > 0 ?
                (int.Parse(product[product.Count() - 1].Code) + 1).ToString() : "0";

            bool isExist = false;
            foreach(var item in product)
            {
                if(item.Name == prod.Name)
                {
                    isExist = true;
                    break;
                }
            }
            if (isExist)
            {
                return Json(new { message = "이미 존재하는 품목입니다." }, JsonRequestBehavior.AllowGet);
            }
            ProductDac.Create(prod.Code, prod.Name, prod.Type, prod.CntStock, userID);
            return Json(new { message = "제품 등록이 완료되었습니다." }, JsonRequestBehavior.AllowGet);
        }
   
        public ActionResult prodLst()
        {
            if (Request.Cookies["user"] == null)
            {
                return RedirectToAction("index", "Home");
            }
            var userID = Request.Cookies["user"].Value;
            var prodList = ProductDac.Get(userID);
           
            ViewBag.prodList = prodList;
            return View();
        }

        [HttpPost]      // 삭제
        public ActionResult deleteProduct(ProductModel prod)
        {
            var userID = Request.Cookies["user"].Value;
            
            try
            {
                ProductDac.Delete(userID, prod.Code, prod.Name);
            }
            catch (Exception err)
            {
                return Json(new { message = err.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { message = "삭제되었습니다." }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]      // 수정
        public ActionResult updateProduct(ProductModel product)
        {
            var userID = Request.Cookies["user"].Value;
            product.userID = userID;
            try
            {
                ProductDac.Update(product);

            }

            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return Json(new { message = "갱신되었습니다." }, JsonRequestBehavior.AllowGet);
        }
    }
}