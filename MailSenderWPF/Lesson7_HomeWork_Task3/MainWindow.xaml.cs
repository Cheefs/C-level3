using System.Windows;
using System;

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
            DataAccessService dt = new DataAccessService();
            Model model = new Model(this);

            //Ресурсы
            cbxSelectMovie.ItemsSource =model.movies;
            dtgOrders.ItemsSource = dt.GetOrder();

            // вывод в дата грид ради теста, потом будет в базу данных передавать, дата грид останется
    
            btnCOnfirm.Click += delegate
            {
                Orders ord = new Orders()
                {
                    DateTime = DateTime.Now.ToString(),
                    Movie = VievMovie,
                    SoldBilets = VievBiletsCount
                };
                dt.NewOrder(ord);
                
               // model.AddOrder();    
                dtgOrders.Items.Refresh();
            };

        }

    }
}
