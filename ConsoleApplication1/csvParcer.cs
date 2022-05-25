using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{

    class csvParcer
    {
        public int countIP { get; set; }
        private List<csvParcer> CSV_Struct = new List<csvParcer>();
        private List<String> listIP = new List<string>(); // Cписок всех IP адресов

        public string IP { get; set; }

        public void piece(string line)
        {
            string[] parts = line.Split(',');  //Разделитель в CVS файле.
            IP = parts[0];
        }
        public void CreateList(string _path)
        {
            CSV_Struct = csvParcer.ReadFile(Environment.CurrentDirectory + "\\" + _path);
            Console.WriteLine(Environment.CurrentDirectory + "\\" + _path);

            foreach (csvParcer c in CSV_Struct)
            {
                listIP.Add(c.IP);
            }
        }
        public static List<csvParcer> ReadFile(string filename)
        {
            List<csvParcer> res = new List<csvParcer>();
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    csvParcer p = new csvParcer();
                    p.piece(line);
                    res.Add(p);
                }
            }
            return res;
        }
        public void DeleteElement(string _element)
        {
            listIP.Remove(_element);
            //записать в файл как прошитый  !!!!
        }

        public int CountElementList()
        { 
            return listIP.Count;
        }
        public void Show()
        {
            foreach (var list in listIP)
            {
                Console.WriteLine(list);
            }

        }
        public string ReturnIP(int _index)
        {
            string temp = null;
            for (int i = 0; i < listIP.Count; i++)
            {
                if (i == _index)
                {
                    temp = listIP[i];
                }
            }
            return temp;
        }
    }
}