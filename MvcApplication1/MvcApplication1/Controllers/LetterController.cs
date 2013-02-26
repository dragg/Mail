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
        // POST api/letter
        public string Post([FromUri] int value)
        {
            using (var mail = new MailEntities())
            {
                string letter = mail.Letters.Single(u => u.Id == value).TextLetter;
                return letter;
            }
        }

    }
}
