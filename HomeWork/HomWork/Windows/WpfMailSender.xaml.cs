using System.Windows;

namespace HomeWork
{
    /// <summary>
    /// Логика взаимодействия для WpfMailSender.xaml
    /// </summary>
    public partial class WpfMailSender : Window,IViev
    {
 #region  реализация интерфейса IViev
        public string MsgText { get=> tbxMessage.Text; set=> tbxMessage.Text=value; }
        public string Password { get=>passwordBox.Password; set=>passwordBox.Password=value; }
        public string Sender { get=>txbSender.Text; set=>txbSender.Text=value; }
        public string MsgHead { get=>tbxHeadMsg.Text; set=>tbxHeadMsg.Text=value; }
        public string Config { get=>cbxConfig.SelectedItem.ToString(); set=>cbxConfig.SelectedItem=value; }
     #endregion

        public WpfMailSender()
        {
            InitializeComponent();
         
            EmailSendService emailSend = new EmailSendService(this);
         
            btnSendEmail.Click += delegate{ emailSend.SendMessage();};
            cbxConfig.ItemsSource = AppConfig.config;
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            Application.Current.MainWindow=this;    
        }
    }
}
