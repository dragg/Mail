using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login(string Login, string Password)
        {
            try
            {
                var user = Helper.Helper.Login(Login, Password);
                Session["User"] = user;
                DateTime expire = DateTime.Now.AddYears(1);
                Response.Cookies.Set(new HttpCookie("Login", Login) { Expires = expire });
                Response.Cookies.Set(new HttpCookie("Password", Password) { Expires = expire });
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Register(string Nick, string Password, string ConfinmPassword)
        {
            if (Password == ConfinmPassword)
            {
                try
                {
                    Helper.Helper.Register(Nick, Password);
                    return Login(Nick, Password);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
            }
            else
            {
                ViewBag.Message = "Your password and confinm password don't identical!";
            }

            return View();
        }

        public ActionResult Log_out()
        {

            Session.Abandon();

            Response.SetCookie(new HttpCookie("Email") { Expires = new DateTime() });
            Response.SetCookie(new HttpCookie("Password") { Expires = new DateTime() });

            return RedirectToAction("Index", "Home");
        }

    }
}
