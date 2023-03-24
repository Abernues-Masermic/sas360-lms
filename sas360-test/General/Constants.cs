using System.Globalization;
using System;

namespace sas360_test
{
    public static class Constants
    {
        public const string version = "2.0.0";

        public const int index_no_selected = -1;

        public const string Setting_dir = "Setting";
        public const string Log_dir = "LogFiles";
        public const string Memory_map_dir = "MemoryMap";


        public const int MAX_DETECTED_TAGS = 12;
        public const int MAX_DETECTED_ZONES = 16;

        public const double CSV_NO_DEFINED_FORMAT = 512;
        public const double CSV_UTC_FORMAT = 255;

        public const int MAX_BITS_USHORT_VALUE = 16;

        public const int PROCESSING_TASK_COUNT = 6;
        public const int LIN_TOTAL_COUNT = 8;
        public const int LIN_USED_COUNT = 3;

        public const int ID_SIZE = 8;

        public const int TAGS_BASE_CON_STRUCT_NUM_REG = 10;
        public const int TAGS_EXTENDED_CON_STRUCT_NUM_REG = 10;
        public const int TAGS_BASE_UWB_STRUCT_NUM_REG = 8;
        public const int TAGS_EXTENDED_UWB_STRUCT_NUM_REG = 10;

        public const int ZONE_BASE_CON_STRUCT_NUM_REG = 8;
        public const int ZONE_EXTENDED_CON_STRUCT_NUM_REG = 12;

        public const int COMMAND_STRUCT_NUM_REG = 12;
        public const int EVENT_LOG_STRUCT_NUM_REG= 12;
        public const int HIST_LOG_STRUCT_NUM_REG = 8;

        public const int MAX_REG_READ_MODBUS = 120;

        public static DateTime date_ref = DateTime.ParseExact("01/01/1970 00:00:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.CurrentCulture);
    }
}
