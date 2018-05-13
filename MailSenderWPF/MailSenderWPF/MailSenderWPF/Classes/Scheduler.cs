using System;
using System.Windows.Threading;
using MailSenderDll;
using System.Linq;
using CodePasswordDLL;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MailSenderWPF
{
    /// <summary>
    /// Класс планировщик, который создает расписание, следит за его выполнением и напоминает событиях
    /// Также помогает автоматизировать рассылку писем в соответствии с расписанием
    /// </summary>
    public class Scheduler
    {
        IViev viev;
        public Scheduler(IViev Viev)
        {
            this.viev = Viev;
        }
        /// <summary>
        /// Окно успешной отправки сообщения
        /// </summary>
        Success sd = new Success();
        /// <summary>
        /// Окно ошибки при отправке
        /// </summary>
        Error ew = new Error();
        private string body;
        private string head;
        private readonly DispatcherTimer timer = new DispatcherTimer();
        private MailSender mSender; 
        private ObservableCollection<Email> emails;
        public Dictionary<DateTime, string> dicDates = new Dictionary<DateTime, string>();
        public Dictionary<DateTime, string> DatesEmailTexts
        {
            get { return dicDates; }
            set
            {
                dicDates = value;
                dicDates = dicDates.OrderBy(pair => pair.Key).
                ToDictionary(pair => pair.Key, pair => pair.Value);
            }
        }

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
        public void SendEmails(Dictionary<DateTime, string> DicDates, ObservableCollection<Email> emails)
        {
            this.dicDates = DicDates;
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
            if (dicDates.Count == 0)
            {
                timer.Stop();
            }
            else if (dicDates.Keys.First<DateTime>().ToShortTimeString() == DateTime.Now.ToShortTimeString())
            {
                body = dicDates[dicDates.Keys.First<DateTime>()];
                head = $"Рассылка от {dicDates.Keys.First<DateTime>().ToShortTimeString()} ";
                this.SendMails(emails);
                dicDates.Remove(dicDates.Keys.First<DateTime>());
            }
        }
        /// <summary>
        /// Отправка писем по всем имейлам сразу
        /// </summary>
        /// <param name="emails">Список адресов</param>
        public void SendMails(ObservableCollection<Email> emails)
        {
            if (viev.FlagNow == true)
            {
                body = viev.MsgText;
                head = viev.MsgHead;
                viev.FlagNow = false;
            }
            mSender = new MailSender(viev.Sender, CodePassword.GetPassword(VariablesClass.Senders[viev.Sender]))
            {
                StrBody = body,
                StrSubject =head,
                SmptServer = viev.SmptServer,
                SmtpPort = VariablesClass.SmptServer[viev.SmptServer]

            };
            bool err = false;
                try
                {
                    foreach (Email email in emails)
                    {
                        mSender.SendMail(email.Value, email.Name);
                    }
                }
                catch (Exception)
                {
                    err = true;
                    System.Media.SystemSounds.Hand.Play();
                    ew.Show();
                }
                if (err == false)
                {
                    System.Media.SystemSounds.Asterisk.Play();
                    sd.Show();
                }
        }
    }
}

