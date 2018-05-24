using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lesson7_HomeWork_Task3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Order> orders = new List<Order>();
        public MainWindow()
        {
            InitializeComponent();
            List<string> movies = new List<string>()
            {
               "boolWekend",
               "charX",
               "string3"
            };
            cbxSelectMovie.ItemsSource = movies;


            dtgOrders.ItemsSource = orders;
            // Временно вся логика тут, просто сбор макета программы, потом все разделено будет
            // вывод в дата грид ради теста, потом будет в базу данных передавать, дата грид останется, чтоб был
            //
            btnCOnfirm.Click += delegate
            {

                orders.Add(new Order
                {
                    Movie = cbxSelectMovie.Text,
                    BiletsCount = txbBiletsCount.Text,
                    Time = DateTime.Now.ToString()
                });

             
                dtgOrders.Items.Refresh();
            };

        }

    }
}
