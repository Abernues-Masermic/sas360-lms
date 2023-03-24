using CsvHelper.Configuration;
using System;

namespace sas360_test
{
    public class Event_log
    {
        public int Index { get; set; }
        public string RTC { get; set; }
        public int Miliseconds { get; set; }

        public int Register_id { get; set; }

        public int Val1 { get; set; }
        public int Val2 { get; set; }
        public int Val3 { get; set; }
        public int Val4 { get; set; }
        public int Val5 { get; set; }
        public int Val6 { get; set; }
    }

    internal class Event_log_map : ClassMap<Event_log>
    {
        public Event_log_map()
        {
            AutoMap(System.Globalization.CultureInfo.CurrentCulture);
        }
    }


    public class Hist_log
    {
        public int Index { get; set; }
        public string RTC { get; set; }
        public int Miliseconds { get; set; }
        public int Type_codif { get; set; }
        public int Logged_value { get; set; }
    }

    internal class Hist_log_map : ClassMap<Hist_log>
    {
        public Hist_log_map()
        {
            AutoMap(System.Globalization.CultureInfo.CurrentCulture);
        }
    }
}
