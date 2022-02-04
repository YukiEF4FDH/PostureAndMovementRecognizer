using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Kinect;


namespace Test
{
    public partial class MainWindow : Window
    {
        static public int AngleCount = 19;
        static public int JointCount = 25;

        public static bool []FocusGroups = new bool[] { false, false, false, false, false, false };
        public static bool check = false;

        public static clsKinect ck = null;

        public static DrawingGroup drawingGroup;
        public DrawingImage imageSource;

        public static int index = 0; // 6个骨骼里被Tracking到的那个
        public clsAction Actions = new clsAction();

        public static clsSkeletons skeletons = new clsSkeletons();
        public static clsMySkeletons MySkeletons = new clsMySkeletons();

        public static bool ImmidiatelyTestFlag = false;

        public static clsComparison Comparison = new clsComparison();

        public static string cb1_string = null;
        public static string cb2_string = null;

        public static Grid GRID;
        public static SkeletonPanel Mysp;

        public ImageSource ImageSource
        {
            get
            {
                return this.imageSource;
            }
        }
        public MainWindow()
        {
            ck = new clsKinect();
            drawingGroup = new DrawingGroup();
            this.imageSource = new DrawingImage( drawingGroup);

            if ( ck.bodyFrameReader != null)
                 ck.bodyFrameReader.FrameArrived += clsDataUpdate.Reader_FrameArrived;
            this.DataContext = this;

            InitializeComponent();

            this.CB.ItemsSource = Actions.Action;
            this.CB2.ItemsSource = Actions.MatchAction;

            GRID = grid;
        }

        public void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (ck.bodyFrameReader != null)
               ck.bodyFrameReader.FrameArrived += clsDataUpdate.Reader_FrameArrived;
        }
        public void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (ck.bodyFrameReader != null)
            {
                ck.bodyFrameReader.Dispose(); ck.bodyFrameReader = null;
            }
            if (ck.kinectSensor != null)
            {
                ck.kinectSensor.Close();ck.kinectSensor = null;
            }
        }
        private void StartRecord_Click(object sender, RoutedEventArgs e)
        {
           if (clsDataUpdate.BodyExistFlag)
                clsFileOperation.StartRecord(Actions, skeletons/*, index*/, CB, CB2, true); // Model
        }
        private void StartRecord2_Click(object sender, RoutedEventArgs e)
        {
            if (clsDataUpdate.BodyExistFlag)
                clsFileOperation.StartRecord(Actions, skeletons/*, index*/, CB, CB2, false);  //Match
        }

        private void Button_Click(object sender, RoutedEventArgs e) //Test按钮
        {
            clsComparison comparison = new clsComparison();
            clsMySkeletons mySkeletons_1 = new clsMySkeletons(); clsMySkeletons mySkeletons_2 = new clsMySkeletons();
            clsFileOperation.GetTwoSkeletonsToMatch(CB.SelectedItem.ToString(), CB2.SelectedItem.ToString(), mySkeletons_1, mySkeletons_2);
            comparison.Comparation(mySkeletons_1, mySkeletons_2);
            Match.Text = comparison.Match.ToString();
        }

        private void DeleteBotton_Click(object sender, RoutedEventArgs e)
        {
            clsAction.LinkFlag = true;
            DeleteWindow deleteWindow = new DeleteWindow();
            deleteWindow.ShowDialog();
            if (deleteWindow.flag) clsFileOperation.StartDelete(CB, Actions, deleteWindow.flag);
            else clsFileOperation.StartDelete(CB2, Actions, deleteWindow.flag);
            CB.SelectedIndex = 0;CB2.SelectedIndex = 0;
        }

        private void Test2_Click(object sender, RoutedEventArgs e)
        {
            if (clsDataUpdate.BodyExistFlag)
            {
                clsDataUpdate.TB(Match);
                ImmidiatelyTestFlag = true;
            }
        }
        private void CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (clsAction.LinkFlag) { clsAction.LinkFlag = false; return; }cb1_string = CB.SelectedItem.ToString();
        }

        private void CB2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (clsAction.LinkFlag) { clsAction.LinkFlag = false; return; } cb2_string = CB2.SelectedItem.ToString();
        }

        private void Head_Neck_Checked(object sender, RoutedEventArgs e)
        {
            check = FocusGroups[0] = (bool)Head_Neck.IsChecked;
        }

        private void Right_Arm_Checked(object sender, RoutedEventArgs e)
        {
            check = FocusGroups[1] = (bool)Right_Arm.IsChecked;
        }

        private void Left_Arm_Checked(object sender, RoutedEventArgs e)
        {
            check = FocusGroups[2] = (bool)Left_Arm.IsChecked;
        }

        private void Torso_Checked(object sender, RoutedEventArgs e)
        {
            check = FocusGroups[3] = (bool)Torso.IsChecked;
        }

        private void Right_Leg_Checked(object sender, RoutedEventArgs e)
        {
            check = FocusGroups[4] = (bool)Right_Leg.IsChecked;
        }

        private void Left_Leg_Checked(object sender, RoutedEventArgs e)
        {
            check = FocusGroups[5] = (bool)Left_Leg.IsChecked;
        }
    }
}
