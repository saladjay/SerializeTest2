using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SerializeTest2
{
    [Serializable]
    public class VersionInfomation : ICloneable
    {
        public string[] AppID { get; set; }
        public string[] DeviceID { get; set; }
        public string[] MachineType { get; set; }
        public string ReleaseDate { get; set; }
        public string ReleaseVersion { get; set; }
        public string[] HardwareVersion { get; set; }
        public string[] Key { get; set; }
        public string Notes { get; set; }
        public byte[] Data { get; set; }
        /// <summary>
        /// which will be used when the static struction fuction runs.
        /// </summary>
        public static Action AnotherConstructionAction { get; set; }
        /// <summary>
        /// this function will be used when StaticStructionController's FunctionEnabled is false.
        /// </summary>
        public static Action CustomStruction { get; set; }
        private static string StaticReleaseData { get; set; }
        /// <summary>
        /// release date will be set automatically.
        /// </summary>
        public VersionInfomation()
        {
            ReleaseDate = StaticReleaseData;
        }
        /// <summary>
        /// in order to return an instance whose data property is null.
        /// </summary>
        /// <returns></returns>
        public VersionInfomation ReturnInstanceWithoutData()
        {
            return null;
        }
        /// <summary>
        /// save binary file in the fixed path, which equals with the ReadFromBin' file path.
        /// </summary>
        public void SaveToBin()
        {
            IFormatter formatter = new BinaryFormatter();
            string filePath = MyDirectoryHelper.CreateDir("VersionInfo") + "Config.bin";
            if (File.Exists(filePath))
            {
                return;
            }
            Stream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this);
            stream.Close();
        }
        /// <summary>
        /// save binary file in path.
        /// </summary>
        /// <param name="filePath"></param>
        public void SaveToBin(string filePath)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this);
            stream.Close();
        }
        /// <summary>
        /// read the binary file from the fixed path. it will return null if something goes wrong.
        /// </summary>
        /// <returns></returns>
        public static VersionInfomation ReadFromBin()
        {
            string filePath = MyDirectoryHelper.CreateDir("VersionInfo") + "Config.bin";
            return ReadFromBin(filePath);
        }
        /// <summary>
        /// read the binary file from the file path. it will return null if something goes wrong.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static VersionInfomation ReadFromBin(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
                    IFormatter formatter = new BinaryFormatter();
                    VersionInfomation versionInfomation = (VersionInfomation)formatter.Deserialize(stream);
                    stream.Close();
                    return versionInfomation;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// in order to create initial file, and put the release date into the initial file.
        /// </summary>
        static VersionInfomation()
        {
            if (StaticConstructionController.FunctionEnabled)
            {
                string filaPath = MyDirectoryHelper.CreateDir("VersionInfo") + "Config.ini";
                IniFileOperator iniFileOperator = new IniFileOperator(filaPath);
                string[] sections;
                if (iniFileOperator.GetAllSectionNames(out sections) == 0)
                {
                    if (sections.Contains("Release"))
                    {
                        StaticReleaseData = iniFileOperator.ReadString("Release", "Date", "");
                    }
                    else
                    {
                        iniFileOperator.IniWriteValue("Release", "Date", DateTime.Now.ToLongDateString());
                        StaticReleaseData = iniFileOperator.ReadString("Release", "Date", "");
                    }
                }
                else
                {
                    iniFileOperator.IniWriteValue("Config", "APP_ID", "0x0C");
                    iniFileOperator.IniWriteValue("Release", "Date", DateTime.Now.ToLongDateString());
                }
                Debug.WriteLine("the original struction function has been execute");
            }
            else
            {
                CustomStruction?.Invoke();
                Debug.WriteLine("the custom struction function has been execute");
            }
            AnotherConstructionAction?.Invoke();
            Debug.WriteLine("AnotherConstructionAction has been execute");
        }
        /// <summary>
        /// in order to transform string property into byte array, then output the byte array 
        /// </summary>
        /// <returns></returns>
        public byte[] ToByteArray()
        {
            List<byte> list = new List<byte>();
            list.Add(0x01);
            list.Add(0x20);
            list.Add(0x03);
            return list.ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            VersionInfomation versionInfomation = new VersionInfomation();
            List<string> strList = new List<string>();
            foreach (string str in this.AppID)
            {
                strList.Add(string.Copy(str));
            }
            versionInfomation.AppID = strList.ToArray();
            strList.Clear();

            return versionInfomation;
        }
    }

    public class StaticConstructionController
    {
        public static bool[] FunctionEnabledArray { get; set; }
        private static bool _FunctionEnabled = true;
        public static bool FunctionEnabled
        {
            get { return _FunctionEnabled; }
            set { _FunctionEnabled = value; }
        }
    }
}
