using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class DeviceFirmware
    {
        private FileBat fileBat = new FileBat();
        public void Firmware(csvParcer _csvParcer,string _pathForCopy,string _pathForDelete, string _login, string _password)
        {
            ConsoleKeyInfo clickExit = new ConsoleKeyInfo();
            PingOptions options = new PingOptions();
           
            Ping pingSender = new Ping();
            Task task2;

            try
            {

                for (int i = 0; i < _csvParcer.CountElementList(); i++)
                {

                    if (Console.KeyAvailable == true)
                    {
                        clickExit = Console.ReadKey(true);
                        if (clickExit.Key == ConsoleKey.X)
                        {
                            break;
                        }
                    }

                    options.DontFragment = true;
                      int timeout = 120;
                    PingReply reply = pingSender.Send(_csvParcer.ReturnIP(i), timeout);
                    Console.WriteLine(_csvParcer.ReturnIP(i));
                    if (reply.Status == IPStatus.Success)
                    {
                        Console.WriteLine("Прошивка устройства IP - " + _csvParcer.ReturnIP(i));

                        task2 = new Task(WaitingEndFirmware);
                        //программа
                        task2.Start();
                        task2.Wait();
                        fileBat.BatFile(_csvParcer.ReturnIP(i), _pathForCopy, _pathForDelete, _login, _password);

                        if (fileBat.ResultFirmware())
                        {
                            Thread.Sleep(2000);
                            Console.WriteLine("Устройство {0} - успешно прошито ", _csvParcer.ReturnIP(i));
                            _csvParcer.DeleteElement(_csvParcer.ReturnIP(i));
                        }
                        else { Console.WriteLine("Устройство {0} - устройство не прошито ", _csvParcer.ReturnIP(i)); }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); Thread.Sleep(5000); }
            
        }

        //Ожидание завершения прошивки
        static void WaitingEndFirmware()
        {
            PingOptions options = new PingOptions();
            FileBat fileBat = new FileBat();
            Ping pingSender = new Ping();
            ProcessStartInfo p;

            try
            {
                p = new ProcessStartInfo();
                p.FileName = Environment.CurrentDirectory + "//TestFile.bat";
                Process.Start(p);

                int count = 0;
                //проверка кол-ва открытых консолей
                foreach (Process process in Process.GetProcesses())
                {
                    if (process.ProcessName == "cmd")
                    {
                        count++;
                    }
                }
                while (true)
                {
                    //ожидание закрытия консоли
                    int countCheck = 0;
                    foreach (Process process in Process.GetProcesses())
                    {
                        if (process.ProcessName == "cmd")
                        {
                            countCheck++;
                        }
                    }
                    // Если файл консоль с прошивкой закрылась завершить прошивку
                    if (count - 1 == countCheck)
                    {

                        break;
                    }
                    Thread.Sleep(1000);
                }
            }

            catch (Exception ex) { Console.WriteLine(ex); }

        }
    }
}
