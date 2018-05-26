
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
//        4. *Есть CSV-файл с таким содержанием:
//      Иванов И.И., ivanov @mail.ru, +7(111) 123-45-67
//      Петров П.П., petrov @mail.ru, +7(222) 123-45-67
//      Федоров Ф.Ф., fedorov @mail.ru, +7(333) 123-45-67

//      То есть записи представляют собой значения: ФИО, почта, телефон.
//      Необходимо написать приложение, которое:
//          a.импортирует данный файл в базу данных;
//           b.позволяет редактировать данные.
namespace Lesson7_HomeWork_Task4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataAccessService dt = new DataAccessService();
        string Phone {get => tbxPhone.Text; set =>tbxPhone.Text = value;}
        string Email{get => txbEmail.Text; set => txbEmail.Text =value;}
        string FIO { get => txbFIO.Text; set => txbFIO.Text = value; }
     
        private Task4ModelContainer _context;
        public List<Task4> ListInfo = new List<Task4>();
        private void Reload()
        {
            ListInfo.Clear();

          
            if (ListInfo != null)
            {
              // _context.Dispose();
            
                ListInfo = _context.Task4Set.ToList();
                dtgInfo.ItemsSource = ListInfo;
            }
         
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Reload();
        }
        public MainWindow()
        {
            _context = new Task4ModelContainer();
            _context.Configuration.AutoDetectChangesEnabled = true;

            InitializeComponent();
            Task4 info = new Task4();
           
            Task4ModelContainer model = new Task4ModelContainer();
            Parser p = new Parser();
            model.Configuration.AutoDetectChangesEnabled = true;

            LabelTest.Content = dtgInfo.SelectedIndex;
        
            btnParceCsv.Click += delegate{ p.Read(); Reload(); };
            btnEdit.Click += delegate
            {
                tbxPhone.Text = ListInfo[dtgInfo.SelectedIndex].Phone;
                txbEmail.Text = ListInfo[dtgInfo.SelectedIndex].Email;
                txbFIO.Text = ListInfo[dtgInfo.SelectedIndex].FIO;            
            };

            btnSave.Click += delegate
            {
                info = new Task4()
                {
                    Id = ListInfo[dtgInfo.SelectedIndex].Id,
                    FIO = FIO,
                    Email = Email,
                    Phone = Phone
                };

                dt.Update(info);
                
            };
            
       

            btnRefresh.Click += delegate
            {
                Reload();
            };

           }
  
    }
}
