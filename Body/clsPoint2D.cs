using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Test
{
    // 3D点的投影 用于画面绘制
    public class clsJointPoint2D
    {
        public Dictionary<JointType, DepthSpacePoint> Point_2D = new Dictionary<JointType, DepthSpacePoint>();

        public clsJointPoint2D()
        {
        }
        public clsJointPoint2D(clsJointPoints jps,clsKinect ck,Body body)
        {
            foreach (var item in jps.JointPoints)
            {
                CameraSpacePoint position = body.Joints[(JointType)Enum.Parse(typeof(JointType), item.type)].Position;
                if (position.Z < 0) position.Z = 0.1f;
                Point_2D.Add((JointType)Enum.Parse(typeof(JointType), item.type), ck.getcoordinateMapper().MapCameraPointToDepthSpace(position));
            }
        }
        public void Draw(Body b, DrawingContext drawingContext, clsJointPoints JointPoints)
        {
            foreach (JointType jointType in b.Joints.Keys)
            {
                DepthSpacePoint p = Point_2D[jointType]; Brush drawBrush;
                if (!JointPoints.JointPoints[JointPoints.FindIndex(jointType.ToString())].focus)
                    drawBrush = new SolidColorBrush(Color.FromArgb(128, 0, 255, 0));
                else
                    drawBrush = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));
                drawingContext.DrawEllipse(drawBrush, null, new System.Windows.Point(p.X, p.Y), 3, 3);
            }
        }
        public Dictionary<JointType, DepthSpacePoint> getPoint_2D_Dic()
        {
            return this.Point_2D;
        }
    }
}
