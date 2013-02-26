using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.DataBase;
using MvcApplication1.Helper;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var loginCookie = Request.Cookies["Login"];
            var passwordCookie = Request.Cookies["Password"];
            if (loginCookie != null && passwordCookie != null)
            {
                try
                {
                    var user = Helper.Helper.Login(loginCookie.Value, passwordCookie.Value);//дописать
                    Session["User"] = user;
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
            }

            return View();
        }

        public ActionResult To_Write()
        {
            return View();
        }

        public ActionResult Inbox()
        {
            ViewBag.Letters = Helper.Helper.GetLetter(Let.Inbox, Request.Cookies["Login"].Value);
            return View();
        }

        public ActionResult Outbox()
        {
            ViewBag.Letters = Helper.Helper.GetLetter(Let.Outbox, Request.Cookies["Login"].Value);
            return View();
        }

        [HttpPost]
        public ActionResult To_Write(string Whom, string Subject, string text)
        {
            using (var mail = new MailEntities())
            {
                var user = mail.Users.SingleOrDefault(u => u.Nick == Whom);
                if (user != null)
                {
                    var loginCookie = Request.Cookies["Login"]; 
                    var user1 = mail.Users.SingleOrDefault(u => u.Nick == loginCookie.Value);
                    mail.Letters.Add(new Letter
                    {
                        Subject = Subject,
                        TextLetter = text,
                        IdUserWhom = user.Id,
                        IdUserFromWhom = user1.Id
                    });
                    mail.SaveChanges();
                    ViewBag.Message = "You letter successfuly sended!";
                }
                else
                {
                    ViewBag.Message = "This Whom is missing!";
                }
            }

            return View();
        }
    }
}
