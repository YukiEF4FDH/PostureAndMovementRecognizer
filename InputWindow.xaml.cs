using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.ComponentModel;

namespace Test
{
    /// <summary>
    /// InputWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InputWindow : Window
    {
        private string filename = "";
        public bool flag = false;
        //public static bool FrameFlag = false;
        public static int i = 0;
        private string path = "D:\\\\";
        public bool retFlag = false;
        public InputWindow()
        {
            retFlag = false;
            InitializeComponent();
            this.Closing += InputWindow_Closing;
        }

        private void InputCheckBotton_Click(object sender, RoutedEventArgs e)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd");
            string username = System.Environment.UserName;

            filename = InputBox.Text + "-" + time + "-" + username;

            string path = System.IO.File.ReadAllText("D:\\ActionList.txt", Encoding.Default);
            Regex reg = new Regex(filename);
            Match mat = reg.Match(path);
            while (mat.Success)
            {
                System.Windows.MessageBox.Show("文件名不合法！请重输！");//位置
                return;
            }

            flag = true; //FrameFlag = true;
            this.Close();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog m_Dialog = new FolderBrowserDialog();
            DialogResult result = m_Dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            string m_Dir = m_Dialog.SelectedPath.Trim();
            m_Dir = m_Dir.Replace("\\", "\\\\");
            path = m_Dir+"\\\\";
        }
        public string getpath()
        {
            return this.path;
        }

        public string getfilename()
        {
            return this.filename;
        }
        public void InputWindow_Closing(object sender, CancelEventArgs e)
        {
            retFlag = true;
            return;
        }
    }
}
