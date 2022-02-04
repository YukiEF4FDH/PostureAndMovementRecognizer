using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

using Microsoft.Kinect;

namespace Test
{
    public class clsKinect   //Kinect类，即关于Kinect的操作
    {
        public KinectSensor kinectSensor = null;  //设备源
        private CoordinateMapper coordinateMapper = null;  //可以用来转化为二维点
        public BodyFrameReader bodyFrameReader = null;  //获取骨骼数据
        public Body[] bodies = null;  //设备
  
        public int displayWidth;
        public int displayHeight;

        public clsKinect()
        {
            this.kinectSensor = KinectSensor.GetDefault();
            this.coordinateMapper = this.kinectSensor.CoordinateMapper;

            FrameDescription frameDescription = this.kinectSensor.DepthFrameSource.FrameDescription;
            this.displayWidth = frameDescription.Width;
            this.displayHeight = frameDescription.Height;
            this.bodyFrameReader = this.kinectSensor.BodyFrameSource.OpenReader();

            this.kinectSensor.Open();
        }
        public CoordinateMapper getcoordinateMapper()
        {
            return this.coordinateMapper;
        }
    }
}
