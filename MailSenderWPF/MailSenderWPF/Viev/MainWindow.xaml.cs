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
        private  Email email;
        private  DataAccessService accessService = new DataAccessService();
        private Reporting report;
        private int id;

        #region  реализация интерфейса IViev
        public string MsgText { get => rtbText.Text; set => rtbText.Text = value; }
        public string Sender { get => cbSenderSelect.Text; set => cbSenderSelect.Text = value; }
        //public string MsgHead { get => tbxHeadMsg.Text; set => tbxHeadMsg.Text = value; }
       // public string SmptServer { get => cbSmtpSelect.Text; set => cbSmtpSelect.Text = value; }
        public bool FlagNow { get; set; }
        public bool SaveFlag { get; set; }

        #endregion

        private readonly ViewModelLocator _locator;
        Dictionary<DateTime, string> dicDates = new Dictionary<DateTime, string>();
        public MainWindow()
        {
            
           

            InitializeComponent();
 
               
          
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            Application.Current.MainWindow = this;
Scheduler sc = new Scheduler(this);
        
            _locator = (ViewModelLocator)FindResource("Locator");
            
            
            lvwSheduler.Items.Clear();
            lvwSheduler.ItemsSource = dicDates;

            lvwSheduler.DisplayMemberPath = "Key";
            lvwSheduler.SelectedValuePath = "Value";

            cbSenderSelect.ItemsSource = VariablesClass.Senders;
            cbSenderSelect.DisplayMemberPath = "Key";
            cbSenderSelect.SelectedValuePath = "Value";
            cbSenderSelect.SelectedIndex = 0;


           // cbSmtpSelect.ItemsSource = VariablesClass.SmptServer;
            //cbSmtpSelect.DisplayMemberPath = "Key";
            //.SelectedValuePath = "Value";
           // cbSmtpSelect.SelectedIndex = 0;
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
                if (sc.CheckSmpt() == true)
                {
                    FlagNow = true;
                    MailSender mailSender = new MailSender(cbSenderSelect.Text, cbSenderSelect.SelectedValue.ToString());
                    if (MsgText == "")
                    {
                        MessageBox.Show("Заполните текст письма");
                        rtbxBody.Visibility = Visibility.Visible;
                    }
                    else
                        sc.SendMails(_locator.Main.Emails);
                    //sc.SendMails(accessService.Emails);
                }
                else MessageBox.Show("Отсутствует необходимый smtp сервер, пожалуйста выберите другого отправителя или" +
                    " добавте сервер во вкладке настройки");
            };
            //Запланировать отправку писем через планировшик
            btnSend.Click += delegate
            {
                sc.SendEmails(dicDates, _locator.Main.Emails);
                //sc.SendEmails(dicDates, accessService.Emails);
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
                using (_container = new EmailxmlContainer())
                {
                    id = emailInfo.dgEmails.SelectedIndex;
                    email = _container.Emails.Find(accessService.GetEmails().ElementAt(id).Id);
                    //accessService.GetEmails().ListInfo[emailInfo.dgEmails.SelectedIndex].Id,
                 //   if (_container.Entry(email).State == EntityState.Detached)
                    //{
                        _container.Emails.Attach(email);
                   // }
                    _container.Emails.Remove(email);
                    _container.SaveChanges();
                };
                Reload();

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
                 
                  id = emailInfo.dgEmails.SelectedIndex;
                  using (_container = new EmailxmlContainer())
                  {
                     
                      email = _container.Emails.Find(accessService.GetEmails().ElementAt(id).Id);
                      //accessService.GetEmails().ListInfo[emailInfo.dgEmails.SelectedIndex].Id,
                      email.Name = tbxEditEmailName.Text;
                      email.Value = tbxEditEmailsValue.Text;

                      _container.Emails.Attach(email);
                      _container.Entry(email).State = EntityState.Modified;
                      _container.SaveChanges();
                  };
                  Reload();
              };
            //Записать список адресатов в файл
            btnReport.Click += delegate { report = new Reporting(); ; };

            #region ComingSoon



            edcSender.BtnAddClick += delegate { MessageBox.Show("ComingSoon"); };
            edcSender.BtnDeleteClick += delegate { MessageBox.Show("ComingSoon"); };
            edcSender.BtnEditClick += delegate { MessageBox.Show("ComingSoon"); };

           // edcSmpt.BtnAddClick += delegate { MessageBox.Show("ComingSoon"); };
          //  edcSmpt.BtnDeleteClick += delegate { MessageBox.Show("ComingSoon"); };
          //  edcSmpt.BtnEditClick += delegate { MessageBox.Show("ComingSoon"); };
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
        public void Reload()
        {
            // MainWindow mw = new MainWindow();
            //emailInfo.dgEmails.Items.Refresh();
            using (_container = new EmailxmlContainer())
            {
                List<Email> em;
                em = _container.Emails.ToList();
                emailInfo.dgEmails.ItemsSource = em;
            } ;      
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            foreach (var el in VariablesClass.SmptServer.Keys)
            {
                //string[] temp = Sender.Split('@');
                //string[] ed = el.Split('.');
                //string smpt = ed[1] +"."+ ed[2];
                //if (temp[1]==smpt)
                //{
                //    el=Key;
                //    VariablesClass.SmptServer[el] = Port;
                //    //VariablesClass.SmptServer
                //    //var smptCorrect = VariablesClass.SmptServer ;
                //    MessageBox.Show($"Почта {VariablesClass.SmptServer[SmptServer]}, Сервер{el}");
                //}



                string[] temp = Sender.Split('@');
               // string[] ed = el.Split('.');
                temp[1] = "smtp." + temp[1];
                if (temp[1] == el)
                {
                    //el=Key;
                    //VariablesClass.SmptServer[el] = Port;
                    //VariablesClass.SmptServer;
                    var smptCorrect = VariablesClass.SmptServer;
                    MessageBox.Show($"Почта {temp[1]}, Сервер{el}");
                }

            }
        }
    }
}


