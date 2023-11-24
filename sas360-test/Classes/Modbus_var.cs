using CsvHelper.Configuration;
using System;

namespace sas360_test
{
    public class Modbus_var
    {
        public double Addr { get; set; } = new double();
        public string Name { get; set; } = string.Empty;

        public string TypeName { get; set; } = string.Empty;
        public Type? Type { get; set; }

        public string Unit { get; set; } = string.Empty;

        public double Format { get; set; }

        public dynamic? Value { get; set; } 

        public string Edit_value { get; set; } = string.Empty;
    }

    internal class Modbus_var_map : ClassMap<Modbus_var>
    {
        public Modbus_var_map(bool validate_value_field)
        {
            AutoMap(System.Globalization.CultureInfo.CurrentCulture);

            Map(m => m.Type).Ignore();
            Map(m => m.Value).Ignore();

            if (!validate_value_field) 
                Map(m => m.Edit_value).Ignore();
        }
    }
}
