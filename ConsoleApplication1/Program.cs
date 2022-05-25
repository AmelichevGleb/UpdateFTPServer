using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        private static  csvParcer csvIP = new csvParcer();
        private static string pathForIP; // название файла с IP для прошивки
        private static string pathForCopy; // название файла для копирования на устройство
        private static string pathForDelete; // название файла для удаления с устройства 
        private static string login;
        private static string password;

        private static DeviceFirmware deviceFirmware = new DeviceFirmware();

        static void Main(string[] args)
        {
                pathForIP = args[0]; 
                pathForCopy = args[1];
                pathForDelete = args[2];
                login = args[3];
                password = args[4];

            FileInfo fileInf = new FileInfo(@"C:\content.txt");
            Task firmware;
            ConsoleKeyInfo clickExit = new ConsoleKeyInfo();

            int test =1;
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            FileBat fileBat = new FileBat();

            csvIP.CreateList(pathForIP); 

            switch(test){
                case 1:
                    while (true)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Для выключения нажать X+C");
                        if (Console.KeyAvailable == true)
                        {
                            clickExit = Console.ReadKey(true);
                            if (clickExit.Key == ConsoleKey.C)
                            {
                                break;
                            }
                        }     
                        firmware = new Task(()  => deviceFirmware.Firmware(csvIP, pathForCopy, pathForDelete, login, password));
                        //программа
                        firmware.Start();
                        firmware.Wait();
                        //программа
                    }
                    break;
            
            }
        }


    }
}