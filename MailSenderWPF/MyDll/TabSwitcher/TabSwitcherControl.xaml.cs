using System.Windows;
using System.Windows.Controls;

namespace TabSwitcher
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class TabSwitcherControl : UserControl
    {
        public event RoutedEventHandler btnNextClick;
        public event RoutedEventHandler btnPreviosClick;


        private void btnPrevios_Click(object sender, RoutedEventArgs e)
        {
            btnPreviosClick?.Invoke(sender, e);
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            btnNextClick?.Invoke(sender, e);
        }
 

        public TabSwitcherControl()
        {
            InitializeComponent();
        }

            #region properties
        private bool bHideBtnPrevios = false; // поле соответствующее будет ли скрыта кнопка Предыдущий
        private bool bHideBtnNext = false; // поле соответствующее будет ли скрыта кнопка Следующий
                                   /// <summary>
                                   /// Свойство соответствующее будет ли скрыта кнопка Предыдущий.
                                   /// Чтобы Свойство отразилось на PropertiesGrid у нашего контрола, его нужно представить именно свойством, а не полем
            /// </summary>
        public bool IsHideBtnPrevios
        {
            get { return bHideBtnPrevios; }
            set
            {
                bHideBtnPrevios = value;
                SetButtons(); // метод, который отвечает на отрисовку кнопок
            }
        }// IsHideBtnPrevios
        public bool IsHideBtnNext
        {
            get { return bHideBtnNext; }
            set
            {
                bHideBtnNext = value;
                SetButtons(); // метод который отвечает за отрисовку кнопок
            }
        }// IsHideBtnPrevios
        private void BtnNextTrueBtnPreviosFalse()
        {
            btnNext.Visibility = Visibility.Hidden;
            btnPrevios.Visibility = Visibility.Visible;
            btnPrevios.Width = 229;
            btnNext.Width = 0;
            btnPrevios.HorizontalAlignment = HorizontalAlignment.Stretch;
        }
        private void BtnPreviosTrueBtnNextFalse()
        {
            btnPrevios.Visibility = Visibility.Hidden;
            btnNext.Visibility = Visibility.Visible;
            btnNext.Width = 229;
            btnPrevios.Width = 0;
            btnNext.HorizontalAlignment = HorizontalAlignment.Stretch;
        }
        private void BtnPreviosFalseBtnNextFalse()
        {
            btnNext.Visibility = Visibility.Visible;
            btnPrevios.Visibility = Visibility.Visible;
            btnNext.Width = 115;
            btnPrevios.Width = 114;
        }
        private void BtnPreviosTrueBtnNextTrue()
        {
            btnPrevios.Visibility = Visibility.Hidden;
            btnNext.Visibility = Visibility.Hidden;
        }
/// <summary>
/// Метод который отвечает за отрисовку кнопок.
/// Есть четыре варианта, когда обе кнопки доступны. Доступна одна и не доступна вторая. И обекнопки недоступны
/// </summary>
        private void SetButtons()
        {
            if (bHideBtnPrevios && bHideBtnNext) BtnPreviosTrueBtnNextTrue();
            else if (!bHideBtnNext && !bHideBtnPrevios) BtnPreviosFalseBtnNextFalse();
            else if (bHideBtnNext && !bHideBtnPrevios) BtnNextTrueBtnPreviosFalse();
            else if (!bHideBtnNext && bHideBtnPrevios) BtnPreviosTrueBtnNextFalse();
        }
        #endregion

   
    }
}

