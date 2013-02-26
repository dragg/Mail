using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using MvcApplication1.Controllers;
using MvcApplication1.DataBase;
using MvcApplication1.Models;

namespace MvcApplication1.Helper
{
    public enum Let
    {
        Inbox, Outbox
    }

    public static class Helper
    {
        public static void Register(string Nick, string Password)
        {
            using (var mail = new MailEntities())
            {
                if (!mail.Users.Any(u => u.Nick == Nick))//проверка на существующий логин
                {
                    var sha = new SHA512Managed();
                    mail.Users.Add(new User
                    {
                        Nick = Nick,
                        Password = sha.ComputeHash(Encoding.UTF8.GetBytes(Password))
                    });
                    mail.SaveChanges();
                }
                else
                {
                    throw new Exception("User with such Nick already exist!");
                }

            }
        }

        public static User Login(string Nick, string Password)
        {
            using (var mail = new MailEntities())
            {
                var user = mail.Users.SingleOrDefault(u => u.Nick == Nick);
                if (user != null)
                {
                    var sha = new SHA512Managed();
                    byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(Password));
                    for (int i = 0; i < hash.Length; i++)
                    {
                        if (hash[i] != user.Password[i])
                        {
                            throw new Exception("Password is incorrect");
                        }
                    }
                    return user;
                }
                else
                {
                    throw new Exception("User with such Nick doesn't exist!");
                }
            }
        }

        public static List<LetterModal> GetLetter(Let l, string Nick)
        {
            using (var mail = new MailEntities())
            {
                var user = mail.Users.SingleOrDefault(u => u.Nick == Nick);
                var letters = mail.Letters.Where(u => (l == Let.Inbox ? u.IdUserWhom : u.IdUserFromWhom) == user.Id).ToArray();
                var list = new List<LetterModal>();
                foreach (var item in letters)
                {
                    list.Add(new LetterModal(mail.Users.Single(u => u.Id == item.IdUserFromWhom).Nick, mail.Users.Single(u => u.Id == item.IdUserWhom).Nick, item.Subject, item.TextLetter, item.Id));
                }
                list.Reverse();
                return list;
            }
        }
    }
}