using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class clsJointPoints  //骨骼点的集合
    {
        //骨骼点和骨骼类型的字典
        public List<clsJointPoint> JointPoints = new List<clsJointPoint>();

        public static string[] JointGroup0 = new string[] { "Head", "Neck" };
        public static string[] JointGroup1 = new string[] { "HandTipRight","ThumbRight","HandRight","WristRight","ElbowRight","ShoulderRight" };
        public static string[] JointGroup2 = new string[] { "HandTipLeft", "ThumbLeft", "HandLeft", "WristLeft", "ElbowLeft", "ShoulderLeft" };
        public static string[] JointGroup3 = new string[] { "SpineShoulder", "SpineMid", "SpineBase"};
        public static string[] JointGroup4 = new string[] { "HipRight", "KneeRight", "AnkleRight", "FootRight" };
        public static string[] JointGroup5 = new string[] { "HipLeft", "KneeLeft", "AnkleLeft", "FootLeft" };

        public static string[][] Groups = new string[][] { JointGroup0, JointGroup1, JointGroup2, JointGroup3, JointGroup4, JointGroup5 };

        public clsJointPoints()  //默认构造
        {
        }

        public clsJointPoints(clsKinect ck)  //默认构造
        {
            foreach (var item in ck.bodies[0].Joints)
            {
                JointPoints.Add(new clsJointPoint(item.Value.Position.X,item.Value.Position.Y,item.Value.Position.Z, item.Key.ToString()));
            }
            
        }

        public clsJointPoints(IReadOnlyDictionary<JointType, Joint> Joints)  //根据Kinect读取到的字典有参构造骨骼点集合
        {
            foreach(var item in Joints)
               this.JointPoints.Add(new clsJointPoint(item.Value));
        }

        public clsJointPoint FindPoint(string type)
        {
            foreach(var item in JointPoints)
            {
                if (item.type==type)
                {
                    return item;
                }
            }
            return new clsJointPoint(-999, -999, -999, type.ToString());
        }

        public int FindIndex(string type)
        {
            int i = 0;
            foreach(var item in JointPoints)
            {
                if (item.type == type) return i;
                i++;
            }
            return -1;
        }

        public void ChangeFocusGroup(bool[] group)
        {
            for (int i = 0; i < 6; i++)
                foreach (var item in Groups[i])
                    this.JointPoints[FindIndex(item)].focus = group[i];
        }
    }
}
