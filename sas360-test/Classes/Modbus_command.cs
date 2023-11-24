using CsvHelper.Configuration;
using System;
using System.Collections.Generic;

namespace sas360_test
{
    public class Modbus_command
    {
        public int Index { get; set; } = new short();
        public string Name { get; set; } = string.Empty;    

        public string Param1 { get; set; } = string.Empty;
        public string Param2 { get; set; } = string.Empty;
        public string Param3 { get; set; } = string.Empty;
        public string Param4 { get; set; } = string.Empty;
        public string Param5 { get; set; } = string.Empty;
        public string Param6 { get; set; } = string.Empty;
        public string Param7 { get; set; } = string.Empty;  
        public string Param8 { get; set; } = string.Empty;
        public string Param9 { get; set; } = string.Empty;

        public List<string> List_param { get; set; } = new List<string>();
        public List<string> List_type { get; set; } = new List<string>();
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
