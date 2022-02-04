using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using System.Windows.Media;
using System.Windows;
using System.IO;

namespace Test
{
    public class clsSkeleton
    {
        public static string []AngleList = new string[]{ "Head_Neck_SpineShoulder" , "Neck_SpineShoulder_ShoulderLeft" , "Neck_SpineShoulder_ShoulderRight","SpineShoulder_ShoulderLeft_ElbowLeft","SpineShoulder_ShoulderRight_ElbowRight",
        "ShoulderLeft_ElbowLeft_WristLeft", "ShoulderRight_ElbowRight_WristRight", "ElbowLeft_WirstLeft_HandLeft","ElbowRight_WirstRight_HandRight", "Neck_SpineShoulder_SpineMid", "SpineShoulder_SpineMid_SpineBase",
        "SpineMid_SpineBase_HipLeft", "SpineMid_SpineBase_HipRight","SpineBase_HipRight_KneeRight", "SpineBase_HipLeft_KneeLeft","HipRight_KneeRight_AnkleRight", "HipLeft_KneeLeft_AnkleLeft", "KneeRight_AnkleRight_FootRight","KneeLeft_AnkleLeft_FootLeft" };

        public static string[] JointList = new string[] { "SpineBase", "SpineMid" , "Neck" , "Head" , "ShoulderLeft" , "ElbowLeft" , "WristLeft", "HandLeft", "ShoulderRight" ,
       "ElbowRight", "WristRight", "HandRight","HipLeft", "KneeLeft", "AnkleLeft","FootLeft","HipRight","KneeRight","AnkleRight","FootRight","SpineShoulder","HandTipLeft","ThumbLeft","HandTipRight","ThumbRight"};

        public clsJointPoints JointPoints = new clsJointPoints();

        public clsJointPoint2D Points2D = new clsJointPoint2D();

        public clsBones Bones = new clsBones();

        public Dictionary<string, double> angles = new Dictionary<string, double>();
        private List<string> Angles = new List<string>();

        public List<clsJointPoint> RelatedSkeletonPoints = new List<clsJointPoint>();

        public Dictionary<string,Point> FileCreatePoints2D = new Dictionary<string, Point>();

        public clsSkeleton()
        {
            foreach (var item in JointPoints.JointPoints)
            {
                RelatedSkeletonPoints.Add(new clsJointPoint(0, 0, 0,item.type));
                FileCreatePoints2D.Add(item.type, new System.Windows.Point(0, 0));
            }

            for (int i = 0; i < 19; i++)
                angles.Add(AngleList[i], 0);

            for (int i = 0; i < JointList.Length; i++)
                JointPoints.JointPoints.Add(new clsJointPoint(0, 0, 0, JointList[i]));

            foreach(var item in JointPoints.JointPoints)
                RelatedSkeletonPoints.Add(new clsJointPoint(0, 0, 0, item.type));
        }

        public clsSkeleton(Body body, clsKinect ck)
        {
            JointPoints = new clsJointPoints(body.Joints);
            Points2D = new clsJointPoint2D(JointPoints, ck, body);
            foreach(var item in Points2D.Point_2D)
            {
                double x = item.Value.X;
                double y = item.Value.Y;
                this.FileCreatePoints2D[item.Key.ToString()] =  new Point(x, y);
            }
            GetAngles();

            clsJointPoint BasePoint = JointPoints.FindPoint("SpineMid");

            foreach (var item in JointPoints.JointPoints)
            {
                double x = item.getX() - BasePoint.getX();
                double y = item.getY() - BasePoint.getY();
                double z = item.getZ() - BasePoint.getZ();
                clsJointPoint point = new clsJointPoint(x, y, z,item.type);
                this.RelatedSkeletonPoints.Add(point);
            }
        }

        public void Draw(Body b, clsKinect ck, DrawingContext drawingContext, clsJointPoints JointPoints)
        {
            Bones.Draw(drawingContext, this);  Points2D.Draw(b, drawingContext,JointPoints);
        }

        public void GetAngles()
        {
            Bones.getAngles(JointPoints,angles);
        }

        public void WriteRelatedPosition(string path ,string filename)
        {
            FileStream fs = new FileStream(path + filename + ".txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            clsJointPoint BasePoint = JointPoints.FindPoint("SpineMid");
            foreach(var item in JointPoints.JointPoints)
            {
                double x = item.getX() - BasePoint.getX();
                double y = item.getY() - BasePoint.getY();
                double z = item.getZ() - BasePoint.getZ();
                clsJointPoint point = new clsJointPoint(x, y, z,item.type);
                sw.WriteLine(point);
            }
            sw.WriteLine();sw.Flush();sw.Close();fs.Close();
        }

        public void WriteRelatedPositionWithValue(string path, string filename)
        {
            FileStream fs = new FileStream(path + filename + ".txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            foreach(var item in RelatedSkeletonPoints)
            {
                sw.WriteLine(item.getX().ToString()+ " "+ item.getY().ToString() +" "+ item.getZ().ToString());
            }
            sw.WriteLine(JointPoints.FindPoint("SpineMid").ToString());
            sw.WriteLine();

            foreach(var item in FileCreatePoints2D)
            {
                sw.WriteLine(item.Value.X.ToString()+" "+ item.Value.Y.ToString());
            }
            sw.WriteLine();sw.Flush(); sw.Close();fs.Close();
        }

        public void WriteAngles(string path, string filename)
        {
            FileStream fs = new FileStream(path + filename + ".txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            //打开刚才新创建的动作的数据文件
            foreach (var item in angles)
                sw.WriteLine(item.Value.ToString());
            sw.WriteLine();sw.Flush();sw.Close(); fs.Close();
        }

        public int FindInRelatedSkeletonPoints(string type)
        {
            int i = 0;
            foreach(var item in RelatedSkeletonPoints)
            {
                if (item.type == type) return i;  i++;
            }
            return -1;
        }
    } 
}
