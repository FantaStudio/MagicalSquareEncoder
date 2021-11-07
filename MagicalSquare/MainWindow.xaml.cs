using System;
using System.Collections.Generic;
using System.Data;
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

namespace MagicalSquare
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EncodeMessage_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Message_box.Text) || Message_box.Text.Length < 2)
            {
                MessageBox.Show("Введите сообщение не менее 2 символов");
                return;
            }
            try
            {
                Result_box.Text = MagicalSquareScrambler.Encode(Message_box.Text);
                MessageBox.Show("Сообщение успешно зашифровано");
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось зашифровать сообщение");
            }
        }

        private void DecodeMessage_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Message_box.Text) || Message_box.Text.Length < 2)
            {
                MessageBox.Show("Введите сообщение не менее 2 символов");
                return;
            }
            try
            {
                Result_box.Text = MagicalSquareScrambler.Decode(Message_box.Text);
                MessageBox.Show("Сообщение успешно расшифровано");
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось расшифровать сообщение");
            }
        }

        private void ShowSquare_Click(object sender, RoutedEventArgs e) => new SquarePage(Message_box.Text).ShowDialog();
    }
}
