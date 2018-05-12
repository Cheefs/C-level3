using System.Windows;
using System.Windows.Controls;


namespace TabSwitcher
{
    /// <summary>
    /// Логика взаимодействия для ListViewItemScheduler.xaml
    /// </summary>
    public partial class ListViewItemScheduler : UserControl  
    {
        public event RoutedEventHandler BtnAddClick;
        public event RoutedEventHandler BtnDeleteClick;
        public event RoutedEventHandler TxbClick;
        public ListViewItemScheduler()
        {
            InitializeComponent();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
           BtnDeleteClick?.Invoke(sender, e);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            BtnAddClick?.Invoke(sender, e);
        }

        private void txbDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            TxbClick?.Invoke(sender, e);
        }

        

       public string Text 
       {
            get => txbDate.Text;set=>txbDate.Text=value;
       }
    }
}
