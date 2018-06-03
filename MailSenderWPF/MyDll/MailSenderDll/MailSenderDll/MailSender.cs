using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MailSenderDll
{
    /// <summary>
    /// Рассылка писем
    /// </summary>
    public class MailSender
    {
        #region vars
        /// <summary>
        /// Почта отправителя
        /// </summary>
        public string StrLogin { get; set; }
        /// <summary>
        /// Пароль почты отправителя
        /// </summary>
        public string StrPassword { get; set; }
        /// <summary>
        /// Сервер
        /// </summary>
        public string SmptServer { get; set; }
        /// <summary>
        /// Порт
        /// </summary>
        public int SmtpPort { get; set; }
        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string StrBody { get; set; }
        /// <summary>
        /// Заголовок
        /// </summary>
        public string StrSubject { get; set; }
        #endregion

        public MailSender(string sLogin, string sPassword)
        {
            StrLogin = sLogin;
            StrPassword = sPassword;
        }
        /// <summary>
        /// Метод отправки писем
        /// </summary>
        /// <param name="mail">email адресс</param>
        /// <param name="name">пароль почты</param>
        public Task SendMail(string mail, string name)
        {
            return Task.Run(() =>
            {
                using (MailMessage mm = new MailMessage(StrLogin, mail))
                {
                    mm.Subject = StrSubject;
                    mm.Body = StrBody;
                    mm.IsBodyHtml = false;
                    SmtpClient sc = new SmtpClient(SmptServer, SmtpPort);
                    sc.EnableSsl = true;
                    sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                    sc.UseDefaultCredentials = false;
                    sc.Credentials = new NetworkCredential(StrLogin, StrPassword);

                    sc.Send(mm);
                }
            });
           
        }
    }
}
