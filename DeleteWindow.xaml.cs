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

namespace Test
{
    /// <summary>
    /// DeleteWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DeleteWindow : Window
    {
        public bool flag = true;

        public DeleteWindow()
        {
            InitializeComponent();
        }

        private void DeleteModel_Click(object sender, RoutedEventArgs e)
        {
            flag = true; this.Close();
        }

        private void DeleteMatch_Click(object sender, RoutedEventArgs e)
        {
            flag = false; this.Close();
        }
    }
}
