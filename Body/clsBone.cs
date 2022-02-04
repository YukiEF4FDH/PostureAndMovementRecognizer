using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Microsoft.Kinect;

namespace Test
{
    public class clsBone  //骨头类
    {
        public clsJointPoint sp;
        public clsJointPoint ep;

        public clsBone()
        {
        }
        public clsBone(clsJointPoint s, clsJointPoint e) //构造
        {
            sp = s;
            ep = e;
        }
        public void Draw(DrawingContext drawingContext, clsSkeleton skeleton)  //绘制函数
        {
            Pen drawPen = new Pen(Brushes.White, 5); //画笔的创建
            double start_x = skeleton.Points2D.getPoint_2D_Dic()[(JointType)Enum.Parse(typeof(JointType),sp.type)].X;  //获得两个端点的投影到二维上的坐标
            double start_y = skeleton.Points2D.getPoint_2D_Dic()[(JointType)Enum.Parse(typeof(JointType), sp.type)].Y;
            double end_x = skeleton.Points2D.getPoint_2D_Dic()[(JointType)Enum.Parse(typeof(JointType),ep.type)].X;
            double end_y = skeleton.Points2D.getPoint_2D_Dic()[(JointType)Enum.Parse(typeof(JointType), ep.type)].Y;
            Point start = new Point(start_x, start_y);  //将获得的坐标新建为Point点
            Point end = new Point(end_x, end_y);  
            drawingContext.DrawLine(drawPen, start,end);  //绘制一条线
        }

        public double getAngle(clsBone bone)
        {
            clsJointPoint center = new clsJointPoint(); clsJointPoint A = new clsJointPoint(); clsJointPoint B = new clsJointPoint();
            if (this.ep.type==bone.sp.type)
            {
                center = ep; A = sp; B = bone.ep;
            }
            else if (this.ep.type==bone.ep.type)
            {
                center = ep; A = sp; B = bone.sp;
            }
            else if(this.sp.type==bone.sp.type)
            {
                center = sp; A = ep; B = bone.ep;
            }
            else if(this.sp.type==bone.ep.type)
            {
                center = sp; A = ep; B = bone.sp;
            }
            double x1, y1, z1, x2, y2, z2;

            x1 = center.getX() - A.getX(); y1 = center.getY() - A.getY(); z1 = center.getZ() - A.getZ();
            x2 = center.getX() - B.getX(); y2 = center.getY() - B.getY(); z2 = center.getZ() - B.getZ();

            Vector3 vec1 = new Vector3(x1, y1, z1); Vector3 vec2 = new Vector3(x2, y2, z2);
            return vec1.getAngle(vec2);
        }
    }
}