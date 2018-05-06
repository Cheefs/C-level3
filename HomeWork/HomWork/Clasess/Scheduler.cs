using System;
using System.Linq;
using System.Windows.Threading;
using MailSenderDll;
using CodePasswordDLL;

namespace HomeWork
{
    /// <summary>
    /// Класс планировщик, который создает расписание, следит за его выполнением и напоминает событиях
    /// Также помогает автоматизировать рассылку писем в соответствии с расписанием
    /// </summary>
    class Scheduler
    {
        IViev viev;
        
        public Scheduler(IViev Viev)
        {
            viev = Viev;
        }
        /// <summary>
        /// Окно успешной отправки сообщения
        /// </summary>
        SendEndWindow sd = new SendEndWindow();
        /// <summary>
        /// Окно ошибки при отправке
        /// </summary>
        ErrorWindow ew = new ErrorWindow();
        /// <summary>
        /// Таймер
        /// </summary>
        DispatcherTimer timer = new DispatcherTimer();
        /// <summary>
        /// Дата и время отправки
        /// </summary>
        DateTime dtSend;
        /// <summary>
        /// Коллекция адресов
        /// </summary>
        IQueryable<Email> emails;

        /// <summary>
        /// Методе который превращаем строку из текстбокса tbTimePicker в TimeSpan
        /// </summary>
        /// <param name="strSendTime"></param>
        /// <returns></returns>
        public TimeSpan GetSendTime(string strSendTime)
        {
            TimeSpan tsSendTime = new TimeSpan();
            try
            {
                tsSendTime = TimeSpan.Parse(strSendTime);
            }
            catch { }
            return tsSendTime;
        }
        /// <summary>
        /// Метод оправки писем по плану
        /// </summary>
        /// <param name="dtSend">дата и время отправки </param>
        /// <param name="emails">список электронных адресов </param>
        public void SendEmails(DateTime dtSend, IQueryable<Email> emails)
        {
            this.dtSend = dtSend;
            this.emails = emails;
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        /// <summary>
        /// Отсчет времени до отправки
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (dtSend.ToShortTimeString() == DateTime.Now.ToShortTimeString())
            {
                SendMails(emails);
                timer.Stop();   
            }
        }
        /// <summary>
        /// Отправка писем по всем имейлам сразу
        /// </summary>
        /// <param name="emails">Список адресов</param>
        public void SendMails(IQueryable<Email> emails)
        {
            bool err = false;
            try
            {
                MailSender mSender = new MailSender
                (
                    viev.Sender,CodePassword.GetPassword(VariablesClass.Senders[viev.Sender])
                );
                mSender.StrSubject = viev.MsgHead;
                mSender.StrBody = viev.MsgText;
                mSender.SmptServer = viev.SmptServer;
                mSender.SmtpPort = VariablesClass.SmptServer[viev.SmptServer];
                foreach (Email email in emails)
                {
                    mSender.SendMail(email.Value, email.Name);
                }
            }
           catch(Exception)
            {
                err = true;
                System.Media.SystemSounds.Hand.Play();
                ew.Show();
            }
            if(err==false)
            {
                System.Media.SystemSounds.Asterisk.Play();
                sd.Show();
            }
        }
    }
}
