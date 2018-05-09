using System;
using System.Windows;
using MailSenderWPF.ViewModel;
using MailSenderDll;

namespace MailSenderWPF
{
    /// <summary>
    /// Логика взаимодействия для WpfMailSender.xaml
    /// </summary>
    public partial class MainWindow : Window, IViev
    {

        #region  реализация интерфейса IViev
        public string MsgText { get => rtbRun.Text; set => rtbRun.Text = value; }
        public string Sender { get => cbSenderSelect.Text; set => cbSenderSelect.Text = value; }
        public string MsgHead { get => tbxHeadMsg.Text; set => tbxHeadMsg.Text = value; }
        public string SmptServer { get => cbSmtpSelect.Text; set => cbSmtpSelect.Text = value; }

        #endregion
        private readonly ViewModelLocator _locator;

        public MainWindow()
        {
           
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            Application.Current.MainWindow = this;
            _locator = (ViewModelLocator)FindResource("Locator");
            Scheduler sc = new Scheduler(this);

            cbSenderSelect.ItemsSource = VariablesClass.Senders;

            cbSenderSelect.DisplayMemberPath = "Key";
            cbSenderSelect.SelectedValuePath = "Value";

            cbSmtpSelect.ItemsSource = VariablesClass.SmptServer;
            cbSmtpSelect.DisplayMemberPath = "Key";
            cbSmtpSelect.SelectedValuePath = "Value";

            btnClock.Click += delegate { tabControl.SelectedItem = tabPlanner; };
            
            btnSendAtOnce.Click += delegate
            {
                string strLogin = cbSenderSelect.Text;
                string strPassword = cbSenderSelect.SelectedValue.ToString(); 

                MailSender mailSender = new MailSender(strLogin, strPassword);
                    sc.SendMails(_locator.Main.Emails);

                if (MsgText == "" || MsgHead == "")
                {
                    MessageBox.Show("Письмо не заполнено");
                    tabEdit.Focus();
                }
            };
            btnSend.Click += delegate
            {
                var tsSendTime = sc.GetSendTime(tpSetTime.Text);
                var locator = (ViewModelLocator)FindResource("Locator");
              
                    tsSendTime = sc.GetSendTime(tpSetTime.Text);

                DateTime dtSendDateTime = (cldSchedulDateTimes.SelectedDate ?? DateTime.Today).Add(tsSendTime);
                if (dtSendDateTime < DateTime.Now)
                {
                    MessageBox.Show(@"Дата и время отправки писем не могут быть раньше, чем настоящее
                    время");
                    return;
                }
                MailSender mailSender = new MailSender(cbSenderSelect.Text, cbSenderSelect.SelectedValue.ToString());

                sc.SendEmails(dtSendDateTime, _locator.Main.Emails); 
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
                if (tabControl.SelectedIndex > 0)
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

