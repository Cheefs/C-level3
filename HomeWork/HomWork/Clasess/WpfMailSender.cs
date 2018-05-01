using System;
using System.Net;
using System.Net.Mail;

namespace HomeWork
{
    /// <summary>
    /// Класс реализующий рассылку писем
    /// </summary>
    class EmailSendService
    {
        ErrorWindow errWdw = new ErrorWindow();
        SendEndWindow sendWdw = new SendEndWindow();
        DBClass dB = new DBClass();

        bool error;
        string SmtpServer;
        int SmptPort;
        IViev viev;
        
        public EmailSendService(IViev Viev)
        {
            this.viev = Viev;
        }
        /// <summary>
        /// Метод отправки сообщений
        /// </summary>
        public void SendMessage()
        {
            error = false;
            try
            {
                if (viev.Config == AppConfig.config[0])
                {
                    SmtpServer = AppConfig.mailRuSmpt;
                    SmptPort = AppConfig.mailRuPort;
                }
                if (viev.Config == AppConfig.config[1])
                {

                    SmtpServer = AppConfig.yandexSmtp;
                    // SmptPort = AppConfig.yandexPort;
                }
                if (viev.Config == AppConfig.config[2])
                {
                    SmtpServer = AppConfig.gmailSmpt;
                }
                SmptPort = AppConfig.Port;



                if (viev.Password != null)
                {
                    foreach (string mail in dB.listStrMails)
                    { /*"TestSMTe@yandex.ru"*/
                        using (MailMessage mm = new MailMessage(viev.Sender, mail))
                        {
                            mm.Subject = viev.MsgHead;
                            mm.Body = viev.MsgText;
                            mm.IsBodyHtml = false;

                            using (SmtpClient sc = new SmtpClient(SmtpServer, SmptPort))
                            {
                                sc.EnableSsl = true;
                                sc.Credentials = new NetworkCredential(viev.Sender, viev.Password);

                                sc.Send(mm);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                error = true;
                errWdw.Show();
            }
            if (error==false)
            sendWdw.Show();
        }
    }
}
