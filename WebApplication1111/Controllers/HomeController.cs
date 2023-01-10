using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using WebApplication1111.Dac;
using WebApplication1111.Models;

namespace WebApplication1111.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult index()
        {
            if (Request.Cookies["user"] != null)
            {
                return RedirectToAction("index", "Product");
            }
            return View();
        }

        [HttpPost]
        public ActionResult index(UserModel user)
        {
            if (user != null)
            {
                var userData = UserDac.Get(user.ID);
      
                    if (userData!=null && userData.ID == user.ID && userData.Password == user.Password)
                    {
                        Response.Cookies["user"].Value = user.ID;
                        Response.Cookies["user"].Expires = DateTime.Now.AddMinutes(30);
                        return Json(new { message = "로그인에 성공했습니다." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { message = "로그인에 실패했습니다." }, JsonRequestBehavior.AllowGet);
                    }
        
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }



        public ActionResult signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult signup(UserModel user)
        {
            if(user != null)
            {
                var userData = UserDac.Get(user.ID);

                if (userData == null)
                {
                    UserDac.Create(user.ID, user.Password);

                    Response.Cookies["user"].Value = user.ID;
                    Response.Cookies["user"].Expires = DateTime.Now.AddMinutes(30);

                    return Json(new { message = "회원가입에 성공했습니다." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { err = "회원가입에 실패했습니다." }, JsonRequestBehavior.AllowGet);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }


    }
}