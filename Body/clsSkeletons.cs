using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Test
{
    public class clsSkeletons
    {
        private clsSkeleton[] Skeletons = new clsSkeleton[6];

        public clsSkeletons()
        {

        }
        public clsSkeletons(clsKinect ck)
        {
            int i = 0;
            foreach (Body body in ck.bodies)
            {
                Skeletons[i] = new clsSkeleton(body, ck);
                i++;
            }
        }

        public void Draw(DrawingContext drawingContext, clsKinect ck, clsJointPoints JointPoints)
        {
            int i = 0, x = 0;
            foreach (Body b in ck.bodies)
            {
                if (b.IsTracked)
                {
                    Skeletons[i].Draw(b, ck, drawingContext,JointPoints);x = i;
                }
                i++;
            }
        }
        public clsSkeleton[] getSkeletons()
        {
            return this.Skeletons;
        }
    }
}
