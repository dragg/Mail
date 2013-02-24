using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcApplication1.DataBase;

namespace MvcApplication1.Controllers
{
    public class LetterController : ApiController
    {
        //
        // GET: /Letter/
        [HttpPost]
        public string Post(string idLetter)
        {
            using (var mail = new MailEntities())
            {
                //var letter = mail.Letters.Single(u => u.Id == id).TextLetter;
                //return letter;
            }
            return "";
        }

    }
}
