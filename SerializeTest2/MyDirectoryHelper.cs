using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializeTest2
{
    public class MyDirectoryHelper
    {
        private static StringBuilder _MainDirectory = new StringBuilder(AppDomain.CurrentDomain.BaseDirectory);
        //private static OpenFileDialog openFileDialog = new OpenFileDialog();
        //private static SaveFileDialog saveFileDialog = new SaveFileDialog();
        public static string CreateDir(string subdir)
        {
            StringBuilder stringBuilder = new StringBuilder(_MainDirectory.ToString());
            string path = stringBuilder.Append(subdir).ToString();
            if (Directory.Exists(path))
            {
                Console.WriteLine("此文件夹已经存在，无需创建！");
                return stringBuilder.Append('\\').ToString();
            }
            else
            {
                Directory.CreateDirectory(path);
                Console.WriteLine(path + " 创建成功!");
                return stringBuilder.Append('\\').ToString();
            }
        }

        public static void CreateNameDir(string name)
        {
            if (name.Length != 0)
            {
                CreateDir(name);
            }
            else
            {
                Console.WriteLine("必须指定文件夹名称，才能创建！");
            }
        }

        public static string[] GetAllFileName(string path, string fileType)
        {
            return Directory.GetFiles(path, fileType);
        }

        //public static string OpenFileDialog(string fileType, string directory)
        //{
        //    string directoryPath = CreateDir(directory);
        //    openFileDialog.InitialDirectory = directoryPath;
        //    openFileDialog.Filter = string.Format("{0} File|*.{1}", fileType, fileType.ToLower());
        //    if (openFileDialog.ShowDialog() == true)
        //    {
        //        return openFileDialog.FileName;
        //    }
        //    else
        //    {
        //        return default(string);
        //    }
        //}

        //public static string SaveFileDialog(string fileType, string directory)
        //{
        //    string directoryPath = CreateDir(directory);
        //    saveFileDialog.InitialDirectory = directoryPath;
        //    saveFileDialog.Filter = string.Format("{0} File|*.{1}", fileType, fileType.ToLower());
        //    if (saveFileDialog.ShowDialog() == true)
        //    {
        //        return saveFileDialog.FileName;
        //    }
        //    else
        //    {
        //        return default(string);
        //    }
        //}

        //public static string SaveFileDialog(string fileDesciption, string fileType, string directory)
        //{
        //    string directoryPath = CreateDir(directory);
        //    saveFileDialog.InitialDirectory = directoryPath;
        //    saveFileDialog.Filter = string.Format("{0} File|*.{1}", fileDesciption, fileType);
        //    if (saveFileDialog.ShowDialog() == true)
        //    {
        //        return saveFileDialog.FileName;
        //    }
        //    else
        //    {
        //        return default(string);
        //    }
        //}

        public static readonly string ExcelDirectory = "Excel";
    }
}
