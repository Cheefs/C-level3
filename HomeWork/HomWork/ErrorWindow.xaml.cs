using System.Windows;

namespace HomeWork
{
    /// <summary>
    /// Логика взаимодействия для ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        public ErrorWindow()
        {
            InitializeComponent();
            btnConfirm.Click += delegate { Hide();};
        }
    }
}
