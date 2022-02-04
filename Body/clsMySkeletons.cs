using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class clsMySkeletons
    {
        private List<clsSkeleton> MySkeletons = new List<clsSkeleton>();

        public static int NUM_OF_FRAME = 50;

        public clsMySkeletons() { }
        public void AddToMySkeletons(clsKinect ck)
        {
            foreach(Body body in ck.bodies)
            {
                if (body.IsTracked)
                    MySkeletons.Add(new clsSkeleton(body, ck));
            }
        }
        public void WriteMySkeletons(string path, string filename)
        {
            string AngleFilename = filename + "-Angle";
            string JointPointFilename = filename + "-JointPoint";

            foreach (clsSkeleton skeleton in MySkeletons)
            {
                skeleton.WriteAngles(path, AngleFilename);
                skeleton.WriteRelatedPositionWithValue(path, JointPointFilename);
            }
        }
        public void ReadToMySkeletons(clsSkeleton skeleton)
        {
            MySkeletons.Add(skeleton);
        }
        public List<clsSkeleton> getMySkeletons()
        {
            return this.MySkeletons;
        }
        
    }
}
