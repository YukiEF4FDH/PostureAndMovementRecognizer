using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace Test
{
    // 三维向量
    public class Vector3
    {
        private double X = 0;
        private double Y = 0;
        private double Z = 0;

        public Vector3()
        {
            X = 0; Y = 0; Z = 0;
        }

        public Vector3(double x, double y, double z)
        {
            X = x; Y = y; Z = z;
        }

        public Vector3(clsJointPoint A, clsJointPoint B)
        {
            X = A.getX() - B.getX();
            Y = A.getY() - B.getY();
            Z = A.getZ() - B.getZ();
        }

        public Vector3(Joint A, Joint B)
        {
            X = A.Position.X - B.Position.X;
            Y = A.Position.Y - B.Position.Y;
            Z = A.Position.Z - B.Position.Z;
        }

        public double Multiple(Vector3 vector)
        {
            double ans = X * vector.X + Y * vector.Y + Z * vector.Z;
            return ans;
        }

        public double Distance()
        {
            double ans = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));
            return ans;
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
        public double getAngle(Vector3 vec)
        {
            double cos_ans = this.Multiple(vec) / (this.Distance() * vec.Distance());
            double angle = Math.Acos(cos_ans) * 180 / 3.14159; //计算获得的是弧度制，将其转化为角度值
            return angle;
        }
    }
}
