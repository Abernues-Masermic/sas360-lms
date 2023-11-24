using System.Globalization;
using System;

namespace sas360_test
{
    public static class Constants
    {
        public const string version = "2.5.0";

        public const int index_no_selected = -1;

        public const string Setting_dir = "Setting";
        public const string Log_dir = "LogFiles";
        public const string Memory_map_dir = "MemoryMap";
        public const string SAS360CON_CFG_dir = "SAS360CON_CFG";


        public const double CSV_NO_DEFINED_FORMAT = 512;
        public const double CSV_UTC_FORMAT = 255;
        public const int MAX_BITS_USHORT_VALUE = 16;


        public const int TEMPORIZADOR_COUNT = 3;
        public const int TIME_PROCESSING_TASK_COUNT = 6;

        public const int ANTENNA_COUNT = 3;
        public const int UWB_TOTAL_COUNT = 4;
        public const int DETECTION_AREA_COUNT = 4;

        public const int ASSIGNED_ID_SIZE = 8;

        public const int AUTOTEST_EA_LEDS_COUNT = 6;

        public const int TAGS_BASE_CON_STRUCT_NUM_REG = 20;
        public const int TAGS_EXTENDED_CON_STRUCT_NUM_REG = 10;
        public const int TAGS_BASE_UWB_STRUCT_NUM_REG = 16;
        public const int TAGS_EXTENDED_UWB_STRUCT_NUM_REG = 10;

        public const int ZONE_BASE_CON_STRUCT_NUM_REG = 16;
        public const int ZONE_EXTENDED_CON_STRUCT_NUM_REG = 10;

        public const int RECORDING_REG_SAS360CON_ARRAY = 8;

        public const int EVENT_LOG_STRUCT_NUM_BYTES= 32;
        public const int EVENT_LOG_STRUCT_NUM_REG = EVENT_LOG_STRUCT_NUM_BYTES / 2;
        public const int EVENT_LOG_REG_READ_MODBUS = EVENT_LOG_STRUCT_NUM_REG * 4;

        public const int HIST_LOG_STRUCT_NUM_BYTES = 16;
        public const int HIST_LOG_STRUCT_NUM_REG = HIST_LOG_STRUCT_NUM_BYTES / 2;
        public const int HIST_LOG_REG_READ_MODBUS = HIST_LOG_STRUCT_NUM_REG * 4;

        public const ushort MAX_REG_READ_MODBUS = 120;

        public static DateTime date_ref = DateTime.ParseExact("01/01/1970 00:00:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.CurrentCulture);
    }
}
