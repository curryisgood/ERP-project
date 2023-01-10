using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1111.Dac;
using WebApplication1111.Models;

namespace WebApplication1111.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult index()
        {
            if (Request.Cookies["user"]==null)
            {
                return RedirectToAction("Index","Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult index(AccountModel accnt)
        {
            var userID = Request.Cookies["user"].Value;
            var account = AccountDac.Get(userID);

            accnt.Code = account.Count() > 0 ?
               (int.Parse(account[account.Count() - 1].Code) + 1).ToString() : "0";

            var boolisExist = false;
            foreach(var aclst in account)
            {
                if (aclst.Name == accnt.Name)
                {
                    boolisExist = true;
                    break;
                }
            }
            if (boolisExist)
            {
                return Json(new { message = "이미 존재하는 거래처입니다." }, JsonRequestBehavior.AllowGet);
            }
            AccountDac.Create(accnt.Code, accnt.Name, userID);
            return Json(new { message = "거래처 등록이 완료되었습니다." }, JsonRequestBehavior.AllowGet);
        }



        
        // 조회 페이지 ======================
        public ActionResult acntLst()
        {
            if (Request.Cookies.Count == 0 || Request.Cookies["user"] == null)
            {
                return RedirectToAction("index", "home");
            }

            var userID = Request.Cookies["user"].Value;
            var customers = AccountDac.Get(userID);

            ViewBag.Customers = customers;
            return View();
        }



        [HttpPost]              // 삭제
        public ActionResult deleteCustomer(AccountModel customer)
        {
            var userID = Request.Cookies["user"].Value;

            try
            {
                Debug.WriteLine($"{customer.Code}, {customer.Name}");
                AccountDac.Delete(customer.Code, customer.Name, userID);
            }
            catch (Exception err)
            {
                return Json(new { message = err.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { message = "거래처 삭제가 완료되었습니다." }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]              // 업데이트
        public ActionResult updateCustomer(AccountModel customer)
        {
            var userID = Request.Cookies["user"].Value;
            customer.UserID = userID;

            try
            {
                AccountDac.Update(customer);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return Json(new { message = "거래처가 갱신되었습니다." }, JsonRequestBehavior.AllowGet);
        }
    }
}