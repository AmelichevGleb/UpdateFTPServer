using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Logs
    {
        private string LogsInBatFile = Environment.CurrentDirectory + "//" + "file.log";
        private string PathLogs = Environment.CurrentDirectory + "//" + "logs.txt";
        private void CreateFile()
        {

            if (!File.Exists(PathLogs)) { File.Create(PathLogs); }
        }
        private void CopyFile()
        {

        }
        private void DeleteFile()
        {

            File.Delete(LogsInBatFile);
        }

        public async void TextFormatting(string _ip)
        {
            try
            {
                CreateFile();
                System.IO.StreamReader sr = new System.IO.StreamReader(LogsInBatFile, Encoding.Default);
                System.IO.StreamWriter sw = new System.IO.StreamWriter(PathLogs, true);
                sw.WriteLine(Convert.ToString("===============" + _ip + "=============== \n"));
                sw.Write(sr.ReadToEnd());
                sr.Close();
                sw.Close();
                DeleteFile();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
