using SecretSanta.BLL.Interfaces;
using SecretSanta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.BLL.Services
{
    public class MailService : IMailService
    {
        private IUserService _userService;

        public MailService(IUserService userService)
        {
            _userService = userService;
        }

        public string SendMails(int listID)
        {
            var users = _userService.Get(listID);

            List<string> names = users.Select(u => u.Name).ToList();
            Randomize<string>(names);

            string error = default;

            for (int i = 0; i < users.Count; i++)
            {
                bool res = SendEmailMessage(users[i].Mail, users[i].Name, names[i]);
                if (!res)
                {
                    error = "you have invalid mail data";
                }
            }

            return error;
        }

        private void Randomize<T>(List<T> items)
        {
            var random = new Random();
            int n = items.Count;

            for(int i = 0; i < n; i++)
            {
                int index = random.Next(i, n);
                T temp = items[index];
                items[index] = items[i];
                items[i] = temp;
            }
        }

        //TODO fix all
        private bool SendEmailMessage(string address, string addressorName, string name)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    string link = $"http://secretsanta.com/ListActions/Confirm?addressorName={addressorName}&name={name}";
                    //string link = "https://www.youtube.com/watch?v=MMxPbKRyh7M&list=RDMMxPbKRyh7M&start_radio=1";
                    mail.From = new MailAddress("secretsantabyneyronbite@gmail.com", "Secret Santa");
                    mail.To.Add(address);
                    mail.Subject = "You are invited to a Secret Santa event";
                    mail.Body = $"Hi. You are invited ... <a href=\"{link}\">Confirm</a>";
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("secretsantabyneyronbite@gmail.com", System.Configuration.ConfigurationSettings.AppSettings["emailPassword"]);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
