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

namespace Yes
{
    /// <summary>
    /// Логика взаимодействия для TypeAddWin.xaml
    /// </summary>
    public partial class TypeAddWin : Window
    {
        public TypeAddWin()
        {
            InitializeComponent();
        }

        private void add_type_Click(object sender, RoutedEventArgs e)
        {
            if (type_name.Text != "")
            {
                TypeKeeping.types.Add(type_name.Text);
                Close();
            }
            else
                MessageBox.Show("Не все поля заполнены.");
        }

        private void Close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow win = new MainWindow();
            win.Show();
        }
    }
}
