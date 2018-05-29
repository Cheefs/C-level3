using System;
using System.Windows;
using MailSenderWPF.ViewModel;
using MailSenderDll;
using System.Collections.Generic;
using System.Linq;
using MailSenderWPF.Services;
using Common;
using System.Data.Entity;


namespace MailSenderWPF
{
    /// <summary>
    /// Логика взаимодействия для WpfMailSender.xaml
    /// </summary>
    public partial class MainWindow : Window, IViev
    {
        private EmailxmlContainer _container;
       // public List<Email> ListInfo;
        private  Email email;
        private  DataAccessService accessService = new DataAccessService();
        private Reporting report;



        #region  реализация интерфейса IViev
        public string MsgText { get => rtbText.Text; set => rtbText.Text = value; }
        public string Sender { get => cbSenderSelect.Text; set => cbSenderSelect.Text = value; }
        public string MsgHead { get => tbxHeadMsg.Text; set => tbxHeadMsg.Text = value; }
        public string SmptServer { get => cbSmtpSelect.Text; set => cbSmtpSelect.Text = value; }
        public bool FlagNow { get; set; }

        #endregion

        private readonly ViewModelLocator _locator;
        Dictionary<DateTime, string> dicDates = new Dictionary<DateTime, string>();
        public MainWindow()
        {
           

            InitializeComponent();
           
          
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            Application.Current.MainWindow = this;

        
            _locator = (ViewModelLocator)FindResource("Locator");
            Scheduler sc = new Scheduler(this);

            lvwSheduler.Items.Clear();
            lvwSheduler.ItemsSource = dicDates;

            lvwSheduler.DisplayMemberPath = "Key";
            lvwSheduler.SelectedValuePath = "Value";

            cbSenderSelect.ItemsSource = VariablesClass.Senders;
            cbSenderSelect.DisplayMemberPath = "Key";
            cbSenderSelect.SelectedValuePath = "Value";
            cbSenderSelect.SelectedIndex = 0;


            cbSmtpSelect.ItemsSource = VariablesClass.SmptServer;
            cbSmtpSelect.DisplayMemberPath = "Key";
            cbSmtpSelect.SelectedValuePath = "Value";
            cbSmtpSelect.SelectedIndex = 0;
            //Загрузка главной формы
            Main.Loaded += delegate{ Reload(); };
            //Удаление записи с планировшика
            ListVievItemsScheduler.BtnDeleteClick += delegate
            {
                if (dicDates.Keys != null)
                {
                    dicDates.Remove(dicDates.Keys.First<DateTime>());
                    lvwSheduler.Items.Refresh();
                }
            };
            //Перейти в планировшик
            btnClock.Click += delegate { tabControl.SelectedItem = tabPlanner; };
            //Отправить письмо сейчас
            btnSendAtOnce.Click += delegate
            {
                FlagNow = true;
                MailSender mailSender = new MailSender(cbSenderSelect.Text, cbSenderSelect.SelectedValue.ToString());
                if (MsgText == "" || MsgHead == "")
                {
                    MessageBox.Show("Заполните текст письма");
                    rtbxBody.Visibility = Visibility.Visible;
                }
                else
                    sc.SendMails(_locator.Main.Emails);
            };
            //Запланировать отправку писем через планировшик
            btnSend.Click += delegate
            {
                sc.SendEmails(dicDates, _locator.Main.Emails);
                lvwSheduler.Items.Refresh();
                rtbxBody.Visibility = Visibility.Hidden;
                rtbText.Text = null;

            };
            //добавить в планировшик
            BtnAddToPlanner.Click += delegate
            {
                var locator = (ViewModelLocator)FindResource("Locator");

                var tsSendTime = sc.GetSendTime(ListVievItemsScheduler.Text);

                DateTime dtSendDateTime = (cldSchedulDateTimes.SelectedDate ?? DateTime.Today).Add(tsSendTime);
                if (dtSendDateTime < DateTime.Now)
                {
                    MessageBox.Show(@"Дата и время отправки писем не могут быть раньше, чем настоящее
                    время");
                    return;
                }
                dicDates.Add(cldSchedulDateTimes.SelectedDate ??
                DateTime.Today.Add(tsSendTime), MsgText);
                lvwSheduler.Items.Refresh();
            };
            //Скрыть или отобразить форму ввода нового адресата
            edcMails.BtnAddClick += delegate
            {
                if (ctrlCaveEmail.Visibility == Visibility.Hidden)
                    ctrlCaveEmail.Visibility = Visibility.Visible;
                else ctrlCaveEmail.Visibility = Visibility.Hidden;
            };

            //Удаление из списка адресатов
            edcMails.BtnDeleteClick += delegate
            {

                this.Reload();

                using (_container = new EmailxmlContainer())
                {
                    email = _container.Emails.Find(accessService.GetEmails().ElementAt(emailInfo.dgEmails.SelectedIndex).Id);
                    //accessService.GetEmails().ListInfo[emailInfo.dgEmails.SelectedIndex].Id,
                    if (_container.Entry(email).State == EntityState.Detached)
                    {
                        _container.Emails.Attach(email);
                    }
                    _container.Emails.Remove(email);
                    _container.SaveChanges();
                };
                //email = new Email()
                //{
                //    Id = ListInfo[emailInfo.dgEmails.SelectedIndex].Id,
                //    Name = ListInfo[emailInfo.dgEmails.SelectedIndex].Name,
                //    Value = ListInfo[emailInfo.dgEmails.SelectedIndex].Value
                //};

                //accessService.DeleteEmail(email);

            };

            //Отображения формы редактирования адресатов
            edcMails.BtnEditClick += delegate 
            {
                if (tbxEditEmailName.Visibility == Visibility.Hidden)
                {
                    tbxEditEmailName.Visibility = Visibility.Visible;
                    tbxEditEmailsValue.Visibility = Visibility.Visible;
                    btnSaveChenges.Visibility = Visibility.Visible;
                }
                else
                {
                    tbxEditEmailName.Visibility = Visibility.Hidden;
                    tbxEditEmailsValue.Visibility = Visibility.Hidden;
                    btnSaveChenges.Visibility = Visibility.Hidden;
                }

            };
            //Сохранение редактированых данных
            btnSaveChenges.Click += delegate
              {
                  this.Reload();

                  using (_container = new EmailxmlContainer())
                  {
                      email = _container.Emails.Find(accessService.GetEmails().ElementAt(emailInfo.dgEmails.SelectedIndex).Id);
                      //accessService.GetEmails().ListInfo[emailInfo.dgEmails.SelectedIndex].Id,
                      email.Name = tbxEditEmailName.Text;
                      email.Value = tbxEditEmailsValue.Text;

                      _container.Emails.Attach(email);
                      _container.Entry(email).State = EntityState.Modified;
                      _container.SaveChanges();
                  };
                     
                 // DataAccessService accessService = new DataAccessService();
                 // accessService.UpdateEmail(email);
            };
            //Записать список адресатов в файл
            btnReport.Click += delegate { report = new Reporting(); ; };

            #region ComingSoon



            edcSender.BtnAddClick += delegate { MessageBox.Show("ComingSoon"); };
            edcSender.BtnDeleteClick += delegate { MessageBox.Show("ComingSoon"); };
            edcSender.BtnEditClick += delegate { MessageBox.Show("ComingSoon"); };

            edcSmpt.BtnAddClick += delegate { MessageBox.Show("ComingSoon"); };
            edcSmpt.BtnDeleteClick += delegate { MessageBox.Show("ComingSoon"); };
            edcSmpt.BtnEditClick += delegate { MessageBox.Show("ComingSoon"); };
            #endregion
            
            //Отображение формы для ввода текста письма
            ListVievItemsScheduler.BtnAddClick += delegate
            {
                if (rtbxBody.Visibility == Visibility.Visible)
                    rtbxBody.Visibility = Visibility.Hidden;
                else rtbxBody.Visibility = Visibility.Visible;
            };
            //Перейти на следующую вкладку
            tabSwtcher.btnNextClick += delegate
            {
                if (tabControl.SelectedIndex > 0)
                {
                    tabSwtcher.IsHideBtnNext = true;
                }
                tabSwtcher.IsHideBtnPrevios = false;
                tabControl.SelectedIndex++;
            };
            //Перейти на предыдущую вкладку
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
        //Перезагрузить даннные
        private void Reload()
        {
            using (_container = new EmailxmlContainer())
            {
                emailInfo.dgEmails.ItemsSource = _container.Emails.ToList();
            };
                // ListInfo = _container.Emails.ToList();
               
        }  
    }
}


