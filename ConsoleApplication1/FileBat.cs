using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
                File.WriteAllText(Environment.CurrentDirectory + "\\TestFile.bat", "ftp -i -s:"+Environment.CurrentDirectory+"\\commands.txt \n timeout 10 \n exit");
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
    }
}
