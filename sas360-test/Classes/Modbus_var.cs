using CsvHelper.Configuration;
using System;

namespace sas360_test
{
    public class Modbus_var
    {
        public double Addr { get; set; }
        public string Name { get; set; }

        public string TypeName { get; set; }
        public Type Type { get; set; }

        public string Unit { get; set; }

        public double Format { get; set; }

        public dynamic Value { get; set; }
    }

    internal class Modbus_var_map : ClassMap<Modbus_var>
    {
        public Modbus_var_map()
        {
            AutoMap(System.Globalization.CultureInfo.CurrentCulture);

            Map(m => m.Type).Ignore();
            Map(m => m.Value).Ignore();
        }
    }
}
