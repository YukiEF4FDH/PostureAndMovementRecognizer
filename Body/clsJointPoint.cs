using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace Test
{
    public class clsJointPoint //骨骼点类
    {
        private double X, Y, Z;  //骨骼的坐标
        public string type;
        public bool focus = false;

        public clsJointPoint()
        {
            X = Y = Z = 0; 
        }

        public clsJointPoint(clsJointPoint jp) //拷贝构造函数
        {
            this.X = jp.X; this.Y = jp.Y; this.Z = jp.Z; this.type = jp.type;
        }

        public clsJointPoint(double x, double y, double z, string type)
        {
            this.X = x;this.Y = y;this.Z = z; this.type = type;
        }

        public clsJointPoint(Joint j)  //Joint是自带的类，也即为Kinect读取后产生的数据的存放
        {
            X = j.Position.X; Y = j.Position.Y; Z = j.Position.Z; this.type = j.JointType.ToString();
        }

        override 
            public string ToString()
        {
            return X.ToString() + " " + Y.ToString() + " " + Z.ToString();
        }

        public static double operator - (clsJointPoint lhs, clsJointPoint rhs)
        {
            double x = 0; double y = 0; double z = 0;
            x = lhs.X - rhs.X; y = lhs.Y - rhs.Y; z = lhs.Z - rhs.Z;
            double result = (Math.Abs(x) + Math.Abs(y) + Math.Abs(z));
            return result;
        }
        public double getX()
        {
            return this.X;
        }
        public double getY()
        {
            return this.Y;
        }
        public double getZ()
        {
            return this.Z;
        }
    }
}
