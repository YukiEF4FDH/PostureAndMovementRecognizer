using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Test
{
    public class clsBones  //骨头的集合
    {
        public List<clsBone> Bones = new List<clsBone>();  //骨头的LIst
        public clsBones() //默认构造函数，将两个点构成骨头的骨头添加进去
        {
            Bones.Add(new clsBone(new clsJointPoint(0,0,0,"Head"), new clsJointPoint(0,0,0,"Neck")));
            Bones.Add(new clsBone(new clsJointPoint(0,0,0,"Neck"), new clsJointPoint(0,0,0,"SpineShoulder")));
            Bones.Add(new clsBone(new clsJointPoint(0,0,0,"SpineShoulder"), new clsJointPoint(0,0,0,"SpineMid")));
            Bones.Add(new clsBone(new clsJointPoint(0,0,0,"SpineMid"), new clsJointPoint(0,0,0,"SpineBase")));
            Bones.Add(new clsBone(new clsJointPoint(0,0,0,"SpineShoulder"), new clsJointPoint(0,0,0,"ShoulderRight")));
            Bones.Add(new clsBone(new clsJointPoint(0,0,0,"SpineShoulder"), new clsJointPoint(0,0,0,"ShoulderLeft")));
            Bones.Add(new clsBone(new clsJointPoint(0,0,0,"SpineBase"), new clsJointPoint(0,0,0,"HipRight")));
            Bones.Add(new clsBone(new clsJointPoint(0,0,0,"SpineBase"), new clsJointPoint(0,0,0,"HipLeft")));
            Bones.Add(new clsBone(new clsJointPoint(0,0,0,"ShoulderRight"), new clsJointPoint(0,0,0,"ElbowRight")));
            Bones.Add(new clsBone(new clsJointPoint(0,0,0,"ElbowRight"), new clsJointPoint(0,0,0,"WristRight")));
            Bones.Add(new clsBone(new clsJointPoint(0,0,0, "WristRight"), new clsJointPoint(0,0,0,"HandRight")));
            Bones.Add(new clsBone(new clsJointPoint(0,0,0, "HandRight"), new clsJointPoint(0,0,0,"HandTipRight")));
            Bones.Add(new clsBone(new clsJointPoint(0,0,0, "WristRight"), new clsJointPoint(0,0,0,"ThumbRight")));
            Bones.Add(new clsBone(new clsJointPoint(0,0,0, "ShoulderLeft"), new clsJointPoint(0,0,0,"ElbowLeft")));
            Bones.Add(new clsBone(new clsJointPoint(0,0,0, "ElbowLeft"), new clsJointPoint(0,0,0,"WristLeft")));
            Bones.Add(new clsBone(new clsJointPoint(0,0,0, "WristLeft"), new clsJointPoint(0,0,0,"HandLeft")));
            Bones.Add(new clsBone(new clsJointPoint(0,0,0, "HandLeft"), new clsJointPoint(0,0,0,"HandTipLeft")));
            Bones.Add(new clsBone(new clsJointPoint(0,0,0, "WristLeft"), new clsJointPoint(0,0,0,"ThumbLeft")));
            Bones.Add(new clsBone(new clsJointPoint(0,0,0, "HipRight"), new clsJointPoint(0,0,0,"KneeRight")));
            Bones.Add(new clsBone(new clsJointPoint(0,0,0, "KneeRight"), new clsJointPoint(0,0,0,"AnkleRight")));
            Bones.Add(new clsBone(new clsJointPoint(0,0,0, "AnkleRight"), new clsJointPoint(0,0,0,"FootRight")));
            Bones.Add(new clsBone(new clsJointPoint(0,0,0, "HipLeft"), new clsJointPoint(0,0,0,"KneeLeft")));
            Bones.Add(new clsBone(new clsJointPoint(0,0,0, "KneeLeft"), new clsJointPoint(0,0,0,"AnkleLeft")));
            Bones.Add(new clsBone(new clsJointPoint(0,0,0, "AnkleLeft"), new clsJointPoint(0,0,0,"FootLeft")));
        }

        public void getAngles(clsJointPoints JointPoints, Dictionary<string, double> Angle_Type_Value_Dic)
        {
            clsBone b1 = new clsBone(); clsBone b2 = new clsBone();

            b1 = new clsBone(JointPoints.FindPoint("Head"), JointPoints.FindPoint("Neck"));
            b2 = new clsBone(JointPoints.FindPoint("Neck"), JointPoints.FindPoint("SpineShoulder"));
            Angle_Type_Value_Dic.Add("Head_Neck_SpineShoulder", b1.getAngle(b2));

            b1 = new clsBone(JointPoints.FindPoint("Neck"), JointPoints.FindPoint("SpineShoulder"));
            b2 = new clsBone(JointPoints.FindPoint("SpineShoulder"), JointPoints.FindPoint("ShoulderLeft"));
            Angle_Type_Value_Dic.Add("Neck_SpineShoulder_ShoulderLeft", b1.getAngle(b2));

            b1 = new clsBone(JointPoints.FindPoint("Neck"), JointPoints.FindPoint("SpineShoulder"));
            b2 = new clsBone(JointPoints.FindPoint("SpineShoulder"), JointPoints.FindPoint("ShoulderRight"));
            Angle_Type_Value_Dic.Add("Neck_SpineShoulder_ShoulderRight", b1.getAngle(b2));

            b1 = new clsBone(JointPoints.FindPoint("SpineShoulder"), JointPoints.FindPoint("ShoulderLeft"));
            b2 = new clsBone(JointPoints.FindPoint("ShoulderLeft"), JointPoints.FindPoint("ElbowLeft"));
            Angle_Type_Value_Dic.Add("SpineShoulder_ShoulderLeft_ElbowLeft", b1.getAngle(b2));

            b1 = new clsBone(JointPoints.FindPoint("SpineShoulder"), JointPoints.FindPoint("ShoulderRight"));
            b2 = new clsBone(JointPoints.FindPoint("ShoulderRight"), JointPoints.FindPoint("ElbowRight"));
            Angle_Type_Value_Dic.Add("SpineShoulder_ShoulderRight_ElbowRight", b1.getAngle(b2));

            b1 = new clsBone(JointPoints.FindPoint("ShoulderLeft"), JointPoints.FindPoint("ElbowLeft"));
            b2 = new clsBone(JointPoints.FindPoint("ElbowLeft"), JointPoints.FindPoint("WristLeft"));
            Angle_Type_Value_Dic.Add("ShoulderLeft_ElbowLeft_WristLeft", b1.getAngle(b2));

            b1 = new clsBone(JointPoints.FindPoint("ShoulderRight"), JointPoints.FindPoint("ElbowRight"));
            b2 = new clsBone(JointPoints.FindPoint("ElbowRight"), JointPoints.FindPoint("WristRight"));
            Angle_Type_Value_Dic.Add("ShoulderRight_ElbowRight_WristRight", b1.getAngle(b2));

            b1 = new clsBone(JointPoints.FindPoint("ElbowLeft"), JointPoints.FindPoint("WirstLeft"));
            b2 = new clsBone(JointPoints.FindPoint("WirstLeft"), JointPoints.FindPoint("HandLeft"));
            Angle_Type_Value_Dic.Add("ElbowLeft_WirstLeft_HandLeft", b1.getAngle(b2));

            b1 = new clsBone(JointPoints.FindPoint("ElbowRight"), JointPoints.FindPoint("WirstRight"));
            b2 = new clsBone(JointPoints.FindPoint("WirstRight"), JointPoints.FindPoint("HandRight"));
            Angle_Type_Value_Dic.Add("ElbowRight_WirstRight_HandRight", b1.getAngle(b2));

            b1 = new clsBone(JointPoints.FindPoint("Neck"), JointPoints.FindPoint("SpineShoulder"));
            b2 = new clsBone(JointPoints.FindPoint("SpineShoulder"), JointPoints.FindPoint("SpineMid"));
            Angle_Type_Value_Dic.Add("Neck_SpineShoulder_SpineMid", b1.getAngle(b2));

            b1 = new clsBone(JointPoints.FindPoint("SpineShoulder"), JointPoints.FindPoint("SpineMid"));
            b2 = new clsBone(JointPoints.FindPoint("SpineMid"), JointPoints.FindPoint("SpineBase"));
            Angle_Type_Value_Dic.Add("SpineShoulder_SpineMid_SpineBase", b1.getAngle(b2));

            b1 = new clsBone(JointPoints.FindPoint("SpineMid"), JointPoints.FindPoint("SpineBase"));
            b2 = new clsBone(JointPoints.FindPoint("SpineBase"), JointPoints.FindPoint("HipLeft"));
            Angle_Type_Value_Dic.Add("SpineMid_SpineBase_HipLeft", b1.getAngle(b2));

            b1 = new clsBone(JointPoints.FindPoint("SpineMid"), JointPoints.FindPoint("SpineBase"));
            b2 = new clsBone(JointPoints.FindPoint("SpineBase"), JointPoints.FindPoint("HipRight"));
            Angle_Type_Value_Dic.Add("SpineMid_SpineBase_HipRight", b1.getAngle(b2));

            b1 = new clsBone(JointPoints.FindPoint("SpineBase"), JointPoints.FindPoint("HipRight"));
            b2 = new clsBone(JointPoints.FindPoint("HipRight"), JointPoints.FindPoint("KneeRight"));
            Angle_Type_Value_Dic.Add("SpineBase_HipRight_KneeRight", b1.getAngle(b2));

            b1 = new clsBone(JointPoints.FindPoint("SpineBase"), JointPoints.FindPoint("HipLeft"));
            b2 = new clsBone(JointPoints.FindPoint("HipLeft"), JointPoints.FindPoint("KneeLeft"));
            Angle_Type_Value_Dic.Add("SpineBase_HipLeft_KneeLeft", b1.getAngle(b2));

            b1 = new clsBone(JointPoints.FindPoint("HipRight"), JointPoints.FindPoint("KneeRight"));
            b2 = new clsBone(JointPoints.FindPoint("KneeRight"), JointPoints.FindPoint("AnkleRight"));
            Angle_Type_Value_Dic.Add("HipRight_KneeRight_AnkleRight", b1.getAngle(b2));

            b1 = new clsBone(JointPoints.FindPoint("HipLeft"), JointPoints.FindPoint("KneeLeft"));
            b2 = new clsBone(JointPoints.FindPoint("KneeLeft"), JointPoints.FindPoint("AnkleLeft"));
            Angle_Type_Value_Dic.Add("HipLeft_KneeLeft_AnkleLeft", b1.getAngle(b2));

            b1 = new clsBone(JointPoints.FindPoint("KneeRight"), JointPoints.FindPoint("AnkleRight"));
            b2 = new clsBone(JointPoints.FindPoint("AnkleRight"), JointPoints.FindPoint("FootRight"));
            Angle_Type_Value_Dic.Add("KneeRight_AnkleRight_FootRight", b1.getAngle(b2));

            b1 = new clsBone(JointPoints.FindPoint("KneeLeft"), JointPoints.FindPoint("AnkleLeft"));
            b2 = new clsBone(JointPoints.FindPoint("AnkleLeft"), JointPoints.FindPoint("FootLeft"));
            Angle_Type_Value_Dic.Add("KneeLeft_AnkleLeft_FootLeft", b1.getAngle(b2));

            return; //Angle_Type_Value_Dic;
        }

        public void Draw(DrawingContext drawingContext,clsSkeleton skeleton)
        {//绘制函数，遍历List绘制骨头即可
            foreach(clsBone item in Bones)
            {
                item.Draw(drawingContext, skeleton);
            }
        }
    }
}
