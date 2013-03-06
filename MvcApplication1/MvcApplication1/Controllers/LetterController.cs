using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcApplication1.DataBase;
using MvcApplication1.Helper;

namespace MvcApplication1.Controllers
{
    public class LetterController : ApiController
    {
        // POST api/letter
        public string Post([FromUri] int value)
        {
            using (var mail = new MailEntities())
            {
                string letter = mail.Letters.Single(u => u.Id == value).TextLetter;
                return letter;
            }
        }

        public void Post([FromUri] string val)
        {
        }

        public IEnumerable<MvcApplication1.Models.LetterModal> Get([FromUri] string valu, [FromUri] string cookie)
        {
            var cookieMas = cookie.Split(';', ' ');
            string login = null;
            for (int i = 0; i < cookieMas.Length; i++)
            {
                var data = cookieMas[i].Split('=');
                if (data[0] == "Login")
                {
                    login = data[1];
                }
            }

            return Helper.Helper.GetLetter( valu == "inbox" ? Let.Inbox : Let.Outbox, login).ToArray();
        }
    }
}
