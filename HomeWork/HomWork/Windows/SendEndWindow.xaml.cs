using System.Windows;

namespace HomeWork
{
    /// <summary>
    /// Логика взаимодействия для SendEndWindow.xaml
    /// </summary>
    public partial class SendEndWindow : Window
    {
        public SendEndWindow()
        {  
            InitializeComponent();

            btnConfirm.Click += delegate { Hide();};
        }
    }
}
