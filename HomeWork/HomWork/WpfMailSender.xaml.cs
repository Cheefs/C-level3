using System;
using System.Windows;
using System.Linq;

namespace HomeWork
{
    /// <summary>
    /// Логика взаимодействия для WpfMailSender.xaml
    /// </summary>
    public partial class WpfMailSender : Window,IViev
    {

        #region  реализация интерфейса IViev
        public string MsgText { get=>rtbRun.Text; set=>rtbRun.Text=value; }
        public string Sender { get=> cbSenderSelect.Text; set=> cbSenderSelect.Text= value; }
        public string MsgHead { get=>tbxHeadMsg.Text; set=>tbxHeadMsg.Text=value; }
        public string SmptServer { get => cbSmtpSelect.Text; set => cbSmtpSelect.Text = value; }
       
        #endregion

        public WpfMailSender()
        {
           
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            Application.Current.MainWindow = this;

            Scheduler sheduler = new Scheduler(this);
            DBClass db = new DBClass();
            dgEmails.ItemsSource = db.Emails;

          
            cbSenderSelect.ItemsSource = VariablesClass.Senders;
           
            cbSenderSelect.DisplayMemberPath = "Key";
            cbSenderSelect.SelectedValuePath = "Value";
           
            cbSmtpSelect.ItemsSource = VariablesClass.SmptServer;
            cbSmtpSelect.DisplayMemberPath = "Key";
            cbSmtpSelect.SelectedValuePath = "Value";

            btnClock.Click += delegate { tabControl.SelectedItem = tabPlanner; };

            btnSendAtOnce.Click += delegate
            {
                if (MsgText =="" || MsgHead =="")
                {
                    MessageBox.Show("Письмо не заполнено");
                    tabEdit.Focus();
                }
                else sheduler.SendMails((IQueryable<Email>)dgEmails.ItemsSource); 
            };
            btnSend.Click += delegate 
            {
                TimeSpan tsSendTime = sheduler.GetSendTime(tpSetTime.Text);
                
                DateTime dtSendDateTime = (cldSchedulDateTimes.SelectedDate ??DateTime.Today).Add(tsSendTime);
                if (dtSendDateTime < DateTime.Now)
                {
                    MessageBox.Show(@"Дата и время отправки писем не могут быть раньше, чем настоящее
                    время");
                    return;
                }
                sheduler.SendEmails(dtSendDateTime, (IQueryable<Email>)dgEmails.ItemsSource);
            };

    #region ComingSoon
            edcSender.BtnAddClick += delegate { MessageBox.Show("ComingSoon"); };
            edcSender.BtnDeleteClick += delegate { MessageBox.Show("ComingSoon"); };
            edcSender.BtnEditClick += delegate { MessageBox.Show("ComingSoon"); };

            edcSmpt.BtnAddClick += delegate { MessageBox.Show("ComingSoon"); };
            edcSmpt.BtnDeleteClick += delegate { MessageBox.Show("ComingSoon"); };
            edcSmpt.BtnEditClick += delegate { MessageBox.Show("ComingSoon"); };

            edcMails.BtnAddClick += delegate { MessageBox.Show("ComingSoon"); };
            edcMails.BtnDeleteClick += delegate { MessageBox.Show("ComingSoon"); };
            edcMails.BtnEditClick += delegate { MessageBox.Show("ComingSoon"); };
    #endregion

            tabSwtcher.btnNextClick += delegate 
            { 
                if(tabControl.SelectedIndex>0)
                {
                    tabSwtcher.IsHideBtnNext = true;   
                }
                tabSwtcher.IsHideBtnPrevios = false;
                tabControl.SelectedIndex++;
            };
            tabSwtcher.btnPreviosClick += delegate 
            {
                tabSwtcher.IsHideBtnNext = false;
                if (tabControl.SelectedIndex == 1)
                {
                    tabSwtcher.IsHideBtnPrevios = true;
                }
                tabControl.SelectedIndex--;
            };
        }
    }
}

