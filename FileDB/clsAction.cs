using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Test
{
    public class clsAction  //录制的动作的集合类
    {
        public List<string> Action = new List<string>();  //用List来记录每一个（套）动作的名字
        public List<string> MatchAction = new List<string>();  //用List来记录每一个（套）动作的名字
        public static bool LinkFlag = false;

        public clsAction()
        {
            List<string> context = new List<string>(File.ReadAllLines("D:\\ModelActionList.txt", Encoding.Default));//先读取到内存变
            //读取存放所有动作名称的txt文件，保存在一个list中
            for (int i = context.Count - 1; i >= 0; i--)
            {
                string[] str = new string[2];
                str = context[i].Split(' ');
                Action.Add(str[0]);  //因为保存的是名称和该套动作的数据文件的路径，所以分割string字符串，其中首个是动作名称
            }
            context = null;
            context = new List<string>(File.ReadAllLines("D:\\MatchActionList.txt", Encoding.Default));//先读取到内存变
            //读取存放所有动作名称的txt文件，保存在一个list中
            for (int i = context.Count - 1; i >= 0; i--)
            {
                string[] str = new string[2];
                str = context[i].Split(' ');
                MatchAction.Add(str[0]);
            }
        }

        public void AddAction(string Path, string FileToAdd, ComboBox CB, ComboBox CB2, bool flag)  //添加动作
        {
            FileStream fs1 = new FileStream("D:\\ModelActionList.txt", FileMode.Append);
            FileStream fs2 = new FileStream("D:\\MatchActionList.txt", FileMode.Append);
            //打开文件进行写操作，注意文件的打开方式为FileMode.Append， 若是FileMode.Open在会在开头开始写，影响到原有的数据

            if (!flag) // Model
            {
                StreamWriter sw = new StreamWriter(fs1, Encoding.Default);  //Encoding.Default用于保证中文的编码正确
                sw.WriteLine(FileToAdd + " " + Path);//在动作名称的记录问价中添加新的动作的名字和路径
                sw.Flush();sw.Close(); fs1.Close();

                LinkFlag = true;

                CB.ItemsSource = null;   //解除ComBoBox和List的数据绑定
                Action.Add(FileToAdd); //向List中添加新的项
                CB.ItemsSource = Action; //将ComBoBox和List进行数据绑定    不先接触绑定就加入项有时无法显示新的项
            }
            else //Match
            {
                StreamWriter sw = new StreamWriter(fs2, Encoding.Default);  //Encoding.Default用于保证中文的编码正确
                sw.WriteLine(FileToAdd + " " + Path);//在动作名称的记录问价中添加新的动作的名字和路径
                sw.Flush();sw.Close(); fs2.Close();

                LinkFlag = true;

                CB2.ItemsSource = null;
                MatchAction.Add(FileToAdd); //向List中添加新的项
                CB2.ItemsSource = MatchAction;
            }
        }

        public void DeleteAction(string path, string filename, bool flag)  //从List删除项
        {
            File.Delete(path + filename +"-Angle" + ".txt");  //删除该名称动作的数据文件
            File.Delete(path + filename + "-JointPoint"+".txt");  //删除该名称动作的数据文件

            if (flag)
            {
                List<string> context = new List<string>(File.ReadAllLines("D:\\ModelActionList.txt", Encoding.Default));//先读取到内存变
                                                                                                                        //从ActionList文件中删除该动作和路径
                for (int i = context.Count - 1; i >= 0; i--)
                {
                    string[] str = new string[2];
                    str = context[i].Split(' ');
                    if (str[0] == filename)   //名称相同即可
                        context.Remove(context[i]);
                }
                File.WriteAllLines("D:\\ModelActionList.txt", context.ToArray(), Encoding.Default);//将修改后的List重新写回文件中
            }
            else
            {
                List<string> context = new List<string>(File.ReadAllLines("D:\\MatchActionList.txt", Encoding.Default));//先读取到内存变

                //从ActionList文件中删除该动作和路径
                for (int i = context.Count - 1; i >= 0; i--)
                {
                    string[] str = new string[2];
                    str = context[i].Split(' ');
                    if (str[0] == filename)   //名称相同即可
                        context.Remove(context[i]);
                }
                File.WriteAllLines("D:\\MatchActionList.txt", context.ToArray(), Encoding.Default); //将修改后的List重新写回文件中
            }
        }
    }
}
