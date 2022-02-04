using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Test
{
    class clsFileOperation   //文件操作函数的集合，其实就是窗口上三个按钮的触发函数
    {
        public static bool FrameFlag = false;
        public static string path = "";
        public static string filename = "";

        public static void StartRecord(clsAction Actions, clsSkeletons skeletons, ComboBox CB, ComboBox CB2, bool flag)
        {//开始录制新的动作的函数
            InputWindow inputwindow = new InputWindow();
            inputwindow.ShowDialog();
            if ((!inputwindow.retFlag)||inputwindow.flag)
            {
                path = inputwindow.getpath(); filename = inputwindow.getfilename();
                Actions.AddAction(inputwindow.getpath(), inputwindow.getfilename(), CB, CB2, flag); //动作的添加

                if (inputwindow.flag)   //表示在inputwindow窗口上点击了确定按钮
                {
                    Counter counter = new Counter(); //弹出321倒计时的窗口
                    counter.ShowDialog();
                    inputwindow.flag = false;
                }
                if (Counter.StandbyEnd) //表示倒计时已经结束，倒计时窗口已经关闭
                {
                    FrameFlag = true;
                }
                CB.SelectedIndex = 0; //默认ComBoBox选中Index为0的项
                CB2.SelectedIndex = 0; //默认ComBoBox选中Index为0的项
            }
        }

        public static void StartDelete(ComboBox CB, clsAction Actions, bool flag)
        {//删除一个动作
            string filename = CB.SelectedItem.ToString();
            string path = "";
            string list = "";

            if (flag)  list = "ModelActionList";
            else  list = "MatchActionList";

            List<string> context = new List<string>(File.ReadAllLines("D:\\"+list+".txt", Encoding.Default));//先读取到内存变
            for (int i = context.Count - 1; i >= 0; i--)
            {
                string[] str = new string[2];
                int zero = context[i].IndexOf(' ');
                if (context[i].Substring(0, zero) == filename)
                {
                   path = context[i].Substring(zero); break;
                }
            }//从ActionLIst文件中得到路径

            CB.ItemsSource = null;  //解除数据绑定
            if (flag)
            { 
                Actions.Action.Remove(filename);
                Actions.DeleteAction(path, filename, flag); //从List中删除该项

                CB.ItemsSource = Actions.Action; //重新绑定数据源
            }
            else
            {
                Actions.MatchAction.Remove(filename);
                Actions.DeleteAction(path, filename, flag); //从List中删除该项

                CB.ItemsSource = Actions.MatchAction; //重新绑定数据源
            }
            CB.SelectedIndex = 0; //默认ComBoBox选中的项
        }

        public  static void GetTwoSkeletonsToMatch(string Filename1, string Filename2, clsMySkeletons mySkeletons_1, clsMySkeletons mySkeletons_2)
        {
            List<string> context1 = new List<string>(File.ReadAllLines("D:\\ModelActionList.txt", Encoding.Default));//先读取到内存变
            List<string> context2 = new List<string>(File.ReadAllLines("D:\\MatchActionList.txt", Encoding.Default));//先读取到内存变

            GetMySkeletons(context1, mySkeletons_1, Filename1);
            GetMySkeletons(context2, mySkeletons_2, Filename2);
        }

        public static void GetSkeletonToMatch(string Filename, clsMySkeletons mySkeletons) //读取要拿来比对的模板骨骼数据
        {
            List<string> context = new List<string>(File.ReadAllLines("D:\\ModelActionList.txt", Encoding.Default));//先读取到内存变
            GetMySkeletons(context, mySkeletons, Filename);
        }

        public static void getPositionfromFile(StreamReader sr, clsSkeleton skeleton, string type, int flag)
        {
            string ans = sr.ReadLine();
            string[] JointItem = ans.Split(' ');
            double x = 0; double y = 0; double z = 0;
            x = double.Parse(JointItem[0]); y = double.Parse(JointItem[1]);
            if (flag != 3) z = double.Parse(JointItem[2]);
            if (flag == 0)
            {
                int index = skeleton.FindInRelatedSkeletonPoints(type);
                skeleton.RelatedSkeletonPoints[index] = new clsJointPoint(x, y, z, type);
            }
            else if (flag == 1)
            {
                int index = skeleton.JointPoints.FindIndex(type);
                skeleton.JointPoints.JointPoints[index] = new clsJointPoint(x, y, z, type);
            }
            else
                skeleton.FileCreatePoints2D[type] = new System.Windows.Point(x, y);
        }

        public static void GetMySkeletons(List<string> context, clsMySkeletons mySkeletons, string Filename)
        {
            string path = ""; //string path2 = "";

            for (int i = context.Count - 1; i >= 0; i--)
            {
                int zero = context[i].IndexOf(' ');
                if (context[i].Substring(0, zero) == Filename)
                {
                    path = context[i].Substring(zero);
                }
            }
            string filename1 = Filename + "-Angle"; //文件名是ComBoBOx 总选中的那一项
            string filename2 = Filename + "-JointPoint";

            FileStream Anglefs = new FileStream(path + filename1 + ".txt", FileMode.Open);
            StreamReader Anglesr = new StreamReader(Anglefs);

            FileStream JointPointfs = new FileStream(path + filename2 + ".txt", FileMode.Open);
            StreamReader JointPointsr = new StreamReader(JointPointfs);

            for (int i = 0; i < clsMySkeletons.NUM_OF_FRAME; i += 2)
            {
                clsSkeleton skeleton = new clsSkeleton(); clsSkeleton temp = new clsSkeleton();
                foreach (var item in temp.angles)
                {
                    string str = Anglesr.ReadLine();
                    skeleton.angles[item.Key] = double.Parse(str);
                }

                foreach (var item in temp.JointPoints.JointPoints)
                    getPositionfromFile(JointPointsr, skeleton, item.type, 0);
                getPositionfromFile(JointPointsr, skeleton, "SpineMid", 1);

                //读2d放到skeleton
                JointPointsr.ReadLine();
                foreach (var item in skeleton.JointPoints.JointPoints)
                    getPositionfromFile(JointPointsr, skeleton,item.type , 3);
                mySkeletons.ReadToMySkeletons(skeleton);

                string garbage = Anglesr.ReadLine();
                garbage = JointPointsr.ReadLine();
            }

            Anglesr.Close(); JointPointsr.Close();
            Anglefs.Close(); JointPointfs.Close();
        }
    }   
}
