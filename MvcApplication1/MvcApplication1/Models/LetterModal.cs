using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcApplication1.DataBase;

namespace MvcApplication1.Models
{
    public class LetterModal
    {
        int _id;
        public int Id
        {
            get { return _id; }
        }

        string _userFromWhom;
        public string UserFromWhom
        {
            get { return _userFromWhom; }
        }

        string _userWhom;
        public string UserWhom
        {
            get { return _userWhom; }
        }

        string _subject;
        public string Subject
        {
            get { return _subject; }
        }

        string _text;
        public string TextLetter
        {
            get { return _text; }
        }

        public LetterModal(string userFromWhom, string userWhom, string subject, string text, int id)
        {
            _userFromWhom = userFromWhom;
            _userWhom = userWhom;
            _subject = subject;
            _text = text;
            _id = id;
        }
    }
}