using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Test
{
    public class clsDataUpdate
    {
       public static string Match = "";
       public static clsSkeleton ModelSkeleton = new clsSkeleton();
        public static bool BodyExistFlag = false;
        private static TextBox t = new TextBox();
        public static void TB(TextBox tb)
        {
            t = tb;
        }
        public static void Reader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            bool dataReceived = false;
            
            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame != null)
                {
                    if (MainWindow.ck.bodies == null)
                    {
                        MainWindow.ck.bodies = new Body[bodyFrame.BodyCount];
                    }
                    bodyFrame.GetAndRefreshBodyData(MainWindow.ck.bodies);
                    dataReceived = true;
                }
            }
            if (dataReceived)
            {
                using (DrawingContext dc = MainWindow.drawingGroup.Open())
                {
                    dc.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, MainWindow.ck.displayWidth, MainWindow.ck.displayHeight));
                    MainWindow.skeletons = new clsSkeletons(MainWindow.ck);

                    if (InputWindow.i < clsMySkeletons.NUM_OF_FRAME && clsFileOperation.FrameFlag)
                    {
                        if (InputWindow.i == 0 || InputWindow.i % 2 == 0) MainWindow.MySkeletons.AddToMySkeletons(MainWindow.ck);
                        InputWindow.i++;
                    }
                    if (InputWindow.i >= clsMySkeletons.NUM_OF_FRAME)
                    {
                        clsFileOperation.FrameFlag = false; InputWindow.i = 0;
                        MainWindow.MySkeletons.WriteMySkeletons(clsFileOperation.path, clsFileOperation.filename);
                        MainWindow.MySkeletons.getMySkeletons().Clear();
                    }
                    for (int i = 0; i < 6; i++)
                        if (MainWindow.ck.bodies[i].IsTracked)
                        {
                            MainWindow.index = i; break;
                        }
                    MainWindow.skeletons.getSkeletons()[MainWindow.index].JointPoints.ChangeFocusGroup(MainWindow.FocusGroups);

                   /*MainWindow.index = */MainWindow.skeletons.Draw(dc, MainWindow.ck, MainWindow.skeletons.getSkeletons()[MainWindow.index].JointPoints);

                    if (MainWindow.index == -1) BodyExistFlag = false;
                    else
                    {
                        BodyExistFlag = true;
                        if (MainWindow.ImmidiatelyTestFlag)
                        {
                            MainWindow.Comparison.AngleMatch = 0;
                            MainWindow.Comparison.PositionMatch = 0;
                            clsMySkeletons myModelSkeletons = new clsMySkeletons();

                            clsSkeleton skeleton = new clsSkeleton();

                            clsFileOperation.GetSkeletonToMatch(MainWindow.cb1_string, myModelSkeletons);

                            MainWindow.Comparison.GetAvgSkeleton(myModelSkeletons, ModelSkeleton); // 画ModelSkeleton

                            if (MainWindow.ImmidiatelyTestFlag)
                            {
                                MainWindow.GRID.Children.Remove(MainWindow.Mysp);
                                MainWindow.Mysp = new SkeletonPanel();
                                MainWindow.GRID.Children.Add(MainWindow.Mysp);
                               
                                Grid.SetRow(MainWindow.Mysp, 1);
                                MainWindow.Mysp.DrawFileSkeleton(clsDataUpdate.ModelSkeleton);
                            }
                            skeleton = MainWindow.skeletons.getSkeletons()[MainWindow.index];
                            MainWindow.Comparison.ImmediatelyComparision(skeleton, ModelSkeleton);

                            t.Text = MainWindow.Comparison.Match.ToString();
                            if (MainWindow.Comparison.Match >= 50)
                            {
                                MainWindow.GRID.Children.Remove(MainWindow.Mysp);
                                MainWindow.ImmidiatelyTestFlag = false;
                            }
                        }
                    }
                }
                MainWindow.drawingGroup.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, MainWindow.ck.displayWidth, MainWindow.ck.displayHeight));
            }
        }
    }
}
