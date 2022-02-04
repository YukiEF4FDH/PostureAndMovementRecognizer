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
using System.Windows.Threading;

namespace Test
{
    /// <summary>
    /// Counter.xaml 的交互逻辑
    /// </summary>
    public partial class Counter : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        public bool first_time_flag = false;
        DateTime time_0, time_1;
        static public bool StandbyEnd = false;

        public Counter()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            if (!first_time_flag)
            {
                time_0 = DateTime.Now;
                first_time_flag = !first_time_flag;
            }
            else
            {
                time_1 = DateTime.Now;
                TimeSpan sec = time_1 - time_0;
                int seconds = sec.Seconds;
                int x = 2, y = 1, z = 0;
                switch (seconds)
                {
                    case 1:
                        StandbyTime.Content = x.ToString();
                        break;
                    case 2:
                        StandbyTime.Content = y.ToString();
                        break;
                    case 3:
                        StandbyTime.Content = z.ToString();
                        StandbyTime.Visibility = Visibility.Hidden;
                        timer.Stop();
                        StandbyEnd = true;
                        this.Close();
                        break;
                }
            }
        }
    }
}
