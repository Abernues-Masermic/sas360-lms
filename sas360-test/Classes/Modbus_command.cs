using CsvHelper.Configuration;
using System;
using System.Collections.Generic;

namespace sas360_test
{
    public class Modbus_command
    {
        public int Index { get; set; }
        public string Name { get; set; }

        public string Param1 { get; set; }
        public string Param2 { get; set; }
        public string Param3 { get; set; }
        public string Param4 { get; set; }
        public string Param5 { get; set; }
        public string Param6 { get; set; }
        public string Param7 { get; set; }
        public string Param8 { get; set; }
        public string Param9 { get; set; }

        public List<string> List_param { get; set; }
        public List<string> List_type { get; set; }
    }

    internal class Modbus_command_map : ClassMap<Modbus_command>
    {
        public Modbus_command_map()
        {
            AutoMap(System.Globalization.CultureInfo.CurrentCulture);

            Map(m => m.List_param).Ignore();
            Map(m => m.List_type).Ignore();
        }
    }
}
