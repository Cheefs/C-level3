using System.Windows;

namespace Lesson7_HomeWork_Task3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,IViev
    {
        #region IViev
       public string VievMovie { get=>cbxSelectMovie.Text; set=>cbxSelectMovie.Text=value; }
       public string VievBiletsCount { get=>txbBiletsCount.Text; set=>txbBiletsCount.Text=value; }
        #endregion
     
        public MainWindow()
        {
            InitializeComponent();

            Model model = new Model(this);
            //Ресурсы
            cbxSelectMovie.ItemsSource =model.movies;
            dtgOrders.ItemsSource = model.orders;



            // Временно вся логика тут, просто сбор макета программы, потом все разделено будет
            // вывод в дата грид ради теста, потом будет в базу данных передавать, дата грид останется
    
            btnCOnfirm.Click += delegate
            {

                model.AddOrder();
             
                dtgOrders.Items.Refresh();
            };

        }

    }
}
