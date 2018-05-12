using System.Windows;

namespace MailSenderWPF
{
    /// <summary>
    /// Логика взаимодействия для Success.xaml
    /// </summary>
    public partial class Success : Window
    {
        public Success()
        {
            InitializeComponent();

            btnConfirm.Click += delegate {Hide(); };
        }
    }
}
