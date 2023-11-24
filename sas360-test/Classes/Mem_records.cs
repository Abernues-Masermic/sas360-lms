using CsvHelper.Configuration;
using System;

namespace sas360_test
{
    public class Event_log
    {
        public uint Absolute_event_index { get; set; } = new uint();
        public string Log_rtc_value { get; set; } = string.Empty;
        public short Log_rtc_milisecs { get; set; } = new short();

        public short Event_id { get; set; } = new short();

        public short Val1 { get; set; } = new short();
        public short Val2 { get; set; } = new short();
        public short Val3 { get; set; } = new short();
        public short Val4 { get; set; } = new short();
        public short Val5 { get; set; } = new short();
        public short Val6 { get; set; } = new short();
        public short Val7 { get; set; } = new short();
        public short Val8 { get; set; } = new short();
        public uint Absolute_event_index_copy { get; set; } = new uint();
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
        public uint Absolute_historic_index { get; set; } = new int();
        public string Log_rtc_value { get; set; } = string.Empty;
        public ushort Log_rtc_milisecs { get; set; } = new int();
        public ushort Log_hist_register_type_codif { get; set; } = new int();
        public int Logged_historic_value_1 { get; set; } = new int();
    }

    internal class Hist_log_map : ClassMap<Hist_log>
    {
        public Hist_log_map()
        {
            AutoMap(System.Globalization.CultureInfo.CurrentCulture);
        }
    }
}
