using System.Windows;
using System.Windows.Controls;

namespace TabSwitcher
{
    /// <summary>
    /// Логика взаимодействия для EditControl.xaml
    /// </summary>
    public partial class EditControl : UserControl
    {
        public event RoutedEventHandler BtnAddClick;
        public event RoutedEventHandler BtnEditClick;
        public event RoutedEventHandler BtnDeleteClick;

        public EditControl()
        {

            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            BtnAddClick?.Invoke(sender, e);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            BtnEditClick?.Invoke(sender, e);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            BtnDeleteClick?.Invoke(sender, e);
        }
    }
}
