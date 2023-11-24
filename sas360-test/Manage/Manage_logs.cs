using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sas360_test
{
    public static class Manage_logs
    {
        private static readonly Object SyncObj = new Object();

        public static void SaveLogValue(string valor)
        {
            try
            {
                if (Globals.GetTheInstance().Depur_mode == BIT_STATE.ON)
                {
                    string path = $"{AppDomain.CurrentDomain.BaseDirectory}\\{Constants.Log_dir}\\LogProgram.txt";

                    lock (SyncObj)
                    {
                        using StreamWriter writer = new(path, true);
                        writer.WriteLine($"{DateTime.Now:dd/MM/yyyy HH:mm:ss.fff}\t{valor}");
                        writer.Close();
                    }
                }
            }
            catch { }
        }

        public static void SaveErrorValue(string error)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                path += @"\" + Constants.Log_dir + @"\LogError.txt";

                lock (SyncObj)
                {
                    using StreamWriter writer = new(path, true);
                    writer.WriteLine(DateTime.Now + "\t" + error);
                    writer.Close();
                }
            }
            catch { }
        }


        public static void SaveModbusValue(string valor)
        {
            try
            {
                if (Globals.GetTheInstance().Depur_mode == BIT_STATE.ON)
                {
                    string path = $"{AppDomain.CurrentDomain.BaseDirectory}\\{Constants.Log_dir}\\LogModbus.txt";

                    lock (SyncObj)
                    {
                        using StreamWriter writer = new(path, true);
                        writer.WriteLine($"{DateTime.Now:dd/MM/yyyy HH:mm:ss.fff}\t{valor}");
                        writer.Close();
                    }
                }
            }
            catch { }
        }


        public static void SaveDataValue(string valor)
        {
            try
            {
                if (Globals.GetTheInstance().Depur_mode == BIT_STATE.ON)
                {
                    string path = $"{AppDomain.CurrentDomain.BaseDirectory}\\{Constants.Log_dir}\\LogData.txt";

                    lock (SyncObj)
                    {
                        using StreamWriter writer = new(path, true);
                        writer.WriteLine(DateTime.Now + "\t" + valor);
                        writer.Close();
                    }
                }
            }
            catch { }
        }
    }
}
