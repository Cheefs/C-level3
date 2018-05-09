using System;
using System.Linq;
using System.Windows.Threading;
using MailSenderDll;
using CodePasswordDLL;
using System.Collections.ObjectModel;

namespace MailSenderWPF
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
        Success sd = new Success();
        /// <summary>
        /// Окно ошибки при отправке
        /// </summary>
        Error ew = new Error();
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
        ObservableCollection<Email> emails;

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
        public void SendEmails(DateTime dtSend, ObservableCollection<Email> emails)
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
        public void SendMails(ObservableCollection<Email> emails)
        {
            bool err = false;
            try
            {
                MailSender mSender = new MailSender ( viev.Sender, CodePassword.GetPassword(VariablesClass.Senders[viev.Sender]) )
                {
                    StrSubject = viev.MsgHead,
                    StrBody = viev.MsgText,
                    SmptServer = viev.SmptServer,
                    SmtpPort = VariablesClass.SmptServer[viev.SmptServer]
                };
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
