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
using System.Windows.Shapes;

namespace MagicalSquare
{
    /// <summary>
    /// Логика взаимодействия для SquarePage.xaml
    /// </summary>
    public partial class SquarePage : Window
    {
        public SquarePage()
        {
            InitializeComponent();
        }

        public SquarePage(string message)
        {
            InitializeComponent();

            int side = MagicalSquareScrambler.GetSideFromMessage(message);
            MagicalSquareBase magic = new MagicalSquareBase(side);
            SquareGrid.ItemsSource = magic.GetDataTable().DefaultView;
            //MessageSquareGrid.ItemsSource = MagicalSquareScrambler.GetSquareTable(message).DefaultView;
        }
    }
}
