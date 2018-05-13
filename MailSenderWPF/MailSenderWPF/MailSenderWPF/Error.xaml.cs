using System.Windows;

namespace MailSenderWPF
{
    /// <summary>
    /// Логика взаимодействия для Success.xaml
    /// </summary>
    public partial class Error : Window
    {
        public Error()
        {
            InitializeComponent();

            btnConfirm.Click += delegate { Hide(); };
        }
    }
}
