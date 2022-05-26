using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    

    class FileBat
    {
        private const string openFTP = "open ";
        private const string commandPut = "put ";
        private const string commandGet = "get ";
        private const string commandLR = "literal reset ";
        private const string commandLs = "ls ";
        private const string commandDel = "del ";
        private const string commandQuit = "quit ";
        private const string slash = "\\";


        private const string OpenString_2 = "220 Welcome FTP server.";
        private const string AuthorizationString_6 = "230 User logged in, proceed. Logged out if appropriate.";
        private const string DeleteString_8 = "250 command successful.";
        private const string PutString_10 = "200 Command ok.";
        private const string OpenDataString_11 = "150 Opening data connections.";
        private const string CloseDataString_12 = "226 Closing data connection. Requested file action successful.";

        /*
  1      ftp> Связь с 192.168.166.40.
  2      open 192.168.166.40
  3      220 Welcome FTP server.
  4      Пользователь (192.168.166.40:(none)): 
  5      331 Need password.
  6
  7      230 User logged in, proceed. Logged out if appropriate.
  8      ftp> del \test.txt
  9      250 command successful.
   10     ftp> put C:\Users\Kniitmy\Desktop\FTP_Server\ConsoleApplication1\bin\Debug\test.txt
     11   200 Command ok.
      12  150 Opening data connections.
      13  226 Closing data connection. Requested file action successful.
      14  ftp> ftp> literal reset 
       15 quit 
        */


        //   private const string fileInServer = "\\test.txt";
        private string OpenIP(string _ip) //+
        {
            return openFTP + _ip; // open 192.168.166.40
        }
        private string CommandPut(string _path) //+
        {
            return commandPut + _path; //put C:\test.txt
        }
        private string CommandGet(string _path)
        {
            return commandGet + _path;
        }
        private string CommandLR() //+
        {
            return commandLR;
        }
        private string CommandQuit() //+
        {
            return commandQuit;
        }
        private string CommandLS() //+ 
        {
            return commandLs;
        }
        private string CommandDel(string _path) //+
        {
            return commandDel + _path;
        }
        
        public void BatFile(string _ip, string _path, string _pathDel, string _user, string _password)
        {
            try {
                File.WriteAllText(Environment.CurrentDirectory + "\\TestFile.bat", "ftp -i -s:"+Environment.CurrentDirectory+ "\\commands.txt >> file.log \n timeout 10 \n exit");
                //Console.ReadKey();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            CommandsSwipFile(_ip,_path,_pathDel, _user, _password);
        }
       
        private void CommandsSendFile(string _ip, string _path,string _user, string _password )
        {
            string path = Environment.CurrentDirectory;
            try
            {
                File.WriteAllText(path + "\\commands.txt", 
                   OpenIP(_ip) +"\n"
                   + _user + "\n"
                   + _password + "\n"
                   + CommandPut(path + slash + _path) + "\n"
                   + CommandLR() +
                   "\n"
                   + CommandQuit() + "\n");
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }
        private void CommandsDeleteFile(string _ip, string _pathDel, string _user, string _password)
        {
            string path = Environment.CurrentDirectory;
            try
            {
                File.WriteAllText(path + "\\commands.txt",
                   OpenIP(_ip) + "\n"
                   + _user + "\n"
                   + _password + "\n"
                   + CommandDel("\\"+ _pathDel) + "\n"
                   + CommandLR() +
                   "\n"
                   + CommandQuit() + "\n");
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }
        private void CommandsShowFile(string _ip, string _user, string _password)
        {
            string path = Environment.CurrentDirectory;
            try
            {
                File.WriteAllText(path + "\\commands.txt",
                   OpenIP(_ip) + "\n"
                   + _user + "\n"
                   + _password + "\n"
                   + CommandLS() + "\n"
                   + CommandLR() +
                   "\n"
                   + CommandQuit() + "\n");
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }
        private void CommandsSwipFile(string _ip, string _path, string _pathDel, string _user, string _password)
        {
            string path = Environment.CurrentDirectory;
            try
            {
                File.WriteAllText(path + "\\commands.txt",
                   OpenIP(_ip) + "\n"
                   + _user + "\n"
                   + _password + "\n"
                   + CommandDel("\\" + _pathDel) + "\n"
                   + CommandPut(path + slash + _path) + "\n"
                   + CommandLR() +
                   "\n"
                   + CommandQuit() + "\n");
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }
        bool[] Massive = new bool[6];

        private async void LogAnalysis()
        {


            try
            {
                using (StreamReader reader = new StreamReader(Environment.CurrentDirectory + "\\file.log"))
                {
                    string line;
                    Array.Clear(Massive, 0, 5);
                    int countLine = 0;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        if (countLine == 2)
                        {
                            if (line == OpenString_2)
                            {
                                Massive[0] = true;
                            }
                            else
                            {
                                Massive[0] = false;
                            }
                        }
                        if (countLine == 6)
                        {
                            if (line == AuthorizationString_6)
                            {
                                Massive[1] = true;
                            }
                            else
                            {
                                Massive[1] = false;
                            }
                        }
                        if (countLine == 8)
                        {
                            if (line == DeleteString_8)
                            {
                                Massive[2] = true;
                            }
                            else
                            {
                                Massive[2] = false;
                            }
                        }
                        if (countLine == 10)
                        {
                            if (line == PutString_10)
                            {
                                Massive[3] = true;
                            }
                            else
                            {
                                Massive[3] = false;
                            }
                        }
                        if (countLine == 11)
                        {
                            if (line == OpenDataString_11)
                            {
                                Massive[4] = true;
                            }
                            else
                            {
                                Massive[4] = false;
                            }
                        }
                        if (countLine == 12)
                        {
                            if (line == CloseDataString_12)
                            {
                                Massive[5] = true;
                                break;
                            }
                            else
                            {
                                Massive[5] = false;
                                break;
                            }
                        }

                        countLine++;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public bool ResultFirmware()
        {
            LogAnalysis();
            int count = 0;
            for (int i = 0; i < Massive.Length; i++)
            {
                Thread.Sleep(20);
                //  Console.WriteLine("номер {0} состояние {1}", i, Massive[i]);
                if (Massive[i] == true)
                {
                   
                    count++;
                }
            }
            if (count == 6) { return true; }
            else return false; 
        }
    }
}
