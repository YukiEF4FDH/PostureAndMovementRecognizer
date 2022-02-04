using Microsoft.Kinect;
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


namespace Test
{
    /// <summary>
    /// SkeletonPanel.xaml 的交互逻辑
    /// </summary>
    public partial class SkeletonPanel : UserControl
    {
        Image theImage1 = new Image();

        public SkeletonPanel()
        {
            InitializeComponent();
        }
        /*public void Show( clsSkeleton skeleton)
        {
            DrawingGroup dg = new DrawingGroup();
            using (DrawingContext dc = dg.Open())
            {
                skeleton.Draw(MainWindow.ck.bodies[MainWindow.index], MainWindow.ck, dc, skeleton.JointPoints);
            }
            DrawingImage dImageSource1 = new DrawingImage(dg);
            theImage1.Source = dImageSource1;
            SkeletonCanvas.Children.Add(theImage1);
        }*/

        public void DrawFileSkeleton( clsSkeleton skeleton)
        {
            Brush drawBrush = new SolidColorBrush(Color.FromArgb(128, 0, 255, 0));
            DrawingGroup dg = new DrawingGroup();
            using (DrawingContext dc = dg.Open())
            {
                foreach(var item in skeleton.Bones.Bones)
                {
                    Pen drawPen = new Pen(Brushes.Gray, 5); //画笔的创建
                    double start_x = skeleton.FileCreatePoints2D[item.sp.type].X;  //获得两个端点的投影到二维上的坐标
                    double start_y = skeleton.FileCreatePoints2D[item.sp.type].Y;
                    double end_x = skeleton.FileCreatePoints2D[item.ep.type].X;
                    double end_y = skeleton.FileCreatePoints2D[item.ep.type].Y;
                    Point start = new Point(start_x, start_y);  //将获得的坐标新建为Point点
                    Point end = new Point(end_x, end_y);
                    dc.DrawLine(drawPen, start, end);  //绘制一条线
                }
                foreach(var item in skeleton.FileCreatePoints2D)
                {
                    Brush B = new SolidColorBrush(Color.FromArgb(128, 0, 255, 0));
                    dc.DrawEllipse(drawBrush, null, item.Value, 3, 3);
                }
            }
            DrawingImage dImageSource1 = new DrawingImage(dg);
            theImage1.Source = dImageSource1;
            SkeletonCanvas.Children.Add(theImage1);
        }
    }
}
