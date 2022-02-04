using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class clsComparison
    {
        private clsSkeleton AvgSkeleton1 = new clsSkeleton(); // Sample
        private clsSkeleton AvgSkeleton2 = new clsSkeleton(); // 循环放Match

        public static double []angle_gap = new double[]{ 2, 2, 3, 4, 6, 7, 7, 10, 15, 1, 1, 8, 3, 12, 1, 15, 13, 28, 23 };
        public static double[] position_gap = new double[] { 0.0389,0,0.0385,0.0529,0.0497,0.0921,0.1027,0.1052,0.0484,0.1158,0.1193,0.0797,0.0416,0.1111,0.1988,0.2680,0.0422,0.0944,0.1589,0.1744,0.0289,0.1117,0.1046,0.0949,0.0977 };

        public double AngleMatch = 0; public double PositionMatch = 0; public double Match = 0;

        public void GetAvgSkeleton(clsMySkeletons skeletons, clsSkeleton skeleton)
        {
            foreach (var item in skeletons.getMySkeletons()[0].angles)
            {
                double AvgAngle = 0;
                for (int i = 0; i < skeletons.getMySkeletons().Count; i++)
                    AvgAngle += skeletons.getMySkeletons()[i].angles[item.Key];
                AvgAngle /= 25;
                skeleton.angles[item.Key] = AvgAngle;
            }
            foreach (var item in skeletons.getMySkeletons()[0].RelatedSkeletonPoints)
            {
                double x = 0; double y = 0; double z = 0;
                
                for (int i = 0; i < skeletons.getMySkeletons().Count; i++)
                {
                    int index = skeletons.getMySkeletons()[i].FindInRelatedSkeletonPoints(item.type);
                    x += skeletons.getMySkeletons()[i].RelatedSkeletonPoints[index].getX();
                    y += skeletons.getMySkeletons()[i].RelatedSkeletonPoints[index].getY();
                    z += skeletons.getMySkeletons()[i].RelatedSkeletonPoints[index].getZ();
                }
                x /= 25; y /= 25; z /= 25;
                int index2 = skeleton.FindInRelatedSkeletonPoints(item.type);
                skeleton.RelatedSkeletonPoints[index2] = new clsJointPoint(x, y, z,item.type);
            }
            double xx = 0; double yy = 0; double zz = 0;
            for (int i = 0; i < skeletons.getMySkeletons().Count; i++)
            {
                int index = skeletons.getMySkeletons()[i].JointPoints.FindIndex("SpineMid");
                xx += skeletons.getMySkeletons()[i].JointPoints.JointPoints[index].getX();
                yy += skeletons.getMySkeletons()[i].JointPoints.JointPoints[index].getY();
                zz += skeletons.getMySkeletons()[i].JointPoints.JointPoints[index].getZ();
            }
            xx /= 25; yy /= 25; zz /= 25;
            int index1 = skeleton.JointPoints.FindIndex("SpineMid");
            skeleton.JointPoints.JointPoints[index1] = new clsJointPoint(xx, yy, zz,"SpineMid");
            foreach (var item in skeletons.getMySkeletons()[0].RelatedSkeletonPoints)
            {
                int index = skeleton.FindInRelatedSkeletonPoints(item.type);
                xx += skeleton.RelatedSkeletonPoints[index].getX();
                yy += skeleton.RelatedSkeletonPoints[index].getY();
                zz += skeleton.RelatedSkeletonPoints[index].getZ();
                skeleton.JointPoints.JointPoints[index] = new clsJointPoint(xx, yy, zz,item.type);
            }
 
            foreach(var item in skeletons.getMySkeletons()[0].FileCreatePoints2D)
            {
                double xxx = 0; double yyy = 0;
                for (int i = 0; i < skeletons.getMySkeletons().Count; i++)
                {
                    xxx+=skeletons.getMySkeletons()[i].FileCreatePoints2D[item.Key]. X;
                    //xxx += skeleton.FileCreatePoints2D[item.Key].X;
                    yyy += skeletons.getMySkeletons()[i].FileCreatePoints2D[item.Key].Y;
                    //yyy += skeleton.FileCreatePoints2D[item.Key].Y;
                }
                xxx /= 25; yyy /= 25;
                skeleton.FileCreatePoints2D[item.Key] = new System.Windows.Point(xxx, yyy);
            }
        }

        public void Comparation(clsMySkeletons mySkeletons1, clsMySkeletons mySkeletons2)
        {
            if (mySkeletons1.getMySkeletons().Count != mySkeletons2.getMySkeletons().Count)
                return; // 帧数不同
            GetAvgSkeleton(mySkeletons1, AvgSkeleton1);
            GetAvgSkeleton(mySkeletons2, AvgSkeleton2);
            ImmediatelyComparision(AvgSkeleton1, AvgSkeleton2);
        }

        public void ImmediatelyComparision(clsSkeleton skeleton1, clsSkeleton skeleton2)
        {
            for (int i = 0; i < angle_gap.Length; i++)
                if (Math.Abs(skeleton1.angles[clsSkeleton.AngleList[i]] - skeleton2.angles[clsSkeleton.AngleList[i]]) <= angle_gap[i])
                    AngleMatch++;

            for (int i = 0; i < position_gap.Length; i++)
                if (Math.Abs(AvgSkeleton1.RelatedSkeletonPoints[AvgSkeleton1.FindInRelatedSkeletonPoints(clsSkeleton.JointList[i])] - 
                    AvgSkeleton2.RelatedSkeletonPoints[AvgSkeleton2.FindInRelatedSkeletonPoints(clsSkeleton.JointList[i])]) <= position_gap[i]) 
                    PositionMatch++;

            Match = AngleMatch + PositionMatch;
        }
    }
}
