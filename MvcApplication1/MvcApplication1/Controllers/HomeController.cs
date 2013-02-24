using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.DataBase;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var emailCookie = Request.Cookies["Login"];
            var passwordCookie = Request.Cookies["Password"];
            if (emailCookie != null && passwordCookie != null)
            {
                try
                {
                    var user = Helper.Helper.Login(emailCookie.Value, passwordCookie.Value);//дописать
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
            var emailCookie = Request.Cookies["Login"];
            using (var mail = new MailEntities())
            {
                var user = mail.Users.SingleOrDefault(u => u.Nick == emailCookie.Value);
                var letters = mail.Letters.Where(u => u.IdUserWhom == user.Id).ToArray();
                var list = new List<LetterModal>();
                foreach (var item in letters)
                {
                    list.Add(new LetterModal(mail.Users.Single(u => u.Id == item.IdUserFromWhom).Nick, mail.Users.Single(u => u.Id == item.IdUserWhom).Nick, item.Subject, item.TextLetter, item.Id));
                }
                
                ViewBag.Letters = list;
            }
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
                    var emailCookie = Request.Cookies["Login"]; 
                    var user1 = mail.Users.SingleOrDefault(u => u.Nick == emailCookie.Value);
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
