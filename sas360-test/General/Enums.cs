namespace sas360_test
{
    public enum BIT_STATE
    {
        OFF = 0,
        ON = 1
    }


    public enum PROGRAM_ACTIONS {
        SAVE = 0,
        LOAD = 1
    }

    public enum BAUD_RATE
    {
        BAUD_9600 = 9600,
        BAUD_19200 = 19200,
        BAUD_38400 = 38400,
        BAUD_57600 = 57600,
        BAUD_115200 = 115200,
    }

    public enum MASTER_SPEED {
        _9600 = 5,
        _19200 = 7,
        _38400 = 8,
        _57600 = 10,
        _11520 = 11,
    }

    public enum RTU_ACTION
    {
        CONNECT = 0,
        DISCONNECT = 1,
        READ = 2,
        WRITE = 3,
        ERROR_CONNECT = 4,
        DISCONNECT_FROM_READ_WRITE = 5,
    }


    public  enum COMMAND_WRITE_LOCATION
    {
        NOT_DEFINED = -1,
        MAINTENNANCE = 0,
        RTC_UTC = 1,
        REPORTED_REGISTER = 2,
        INTERNAL_CONFIG = 3,
        SAS360CON_CONFIG_INSTALLATION = 4,
        SAS360CON_CONFIG_VEHICLE_CONFIG = 5,
        SAS360CON_CONFIG_DETECTION_AREA = 6,
        SAS360CON_CONFIG_UWB_COMM= 7,
        SAS360CON_CONFIG_MISCELLANEOUS = 8,
        SAS360CON_CONFIG_OUTPUT = 9,
        SAS360CON_CFG_RECORDING = 10,
    }

    public enum SEND_COMMAND_STATE
    {
        OK = 1,
        ERROR = 2,
        WARNING = 3
    }


    public enum MAIN_MENU_TAB
    {
        STATE = 0,
        CONFIG = 1,
        MEMORY = 2,
        MAINTENANCE = 3
    }

    public enum CONFIG_MENU_TAB
    {
        SAS360CON_INTERNAL_CFG =0,
        SAS360CON_CFG = 1,
        IOT_CFG = 2
    }


    #region MEMORY

    public enum ENABLE_READ_MEMORY_BIT
    {
        SAS360CON_INTERNAL_CFG = 0,
        SAS360CON_CFG = 1,
        IOT_CFG = 2,
        SAS360CON_IMAGE = 3,
        IOT_IMAGE = 4,
        SAS360CON_MAINTENNANCE = 5,

        UWB_INTERNAL_CFG = 6,
        UWB_IMAGE = 7,

        CONSOLE_CLOSEST_TAGS_BASE = 8,
        CONSOLE_CLOSEST_TAGS_EXTENDED = 9,

        CONSOLE_CLOSEST_ZONE_BASE = 10,
        CONSOLE_CLOSEST_ZONE_EXTENDED = 11,

        UWB_CLOSEST_TAGS_BASE = 12,
        UWB_CLOSEST_TAGS_EXTENDED = 13,
        UWB_CLOSEST_ZONE_BASE = 14,
        UWB_CLOSEST_ZONE_EXTENDED = 15,

        SAS360CON_NVREG = 16,
        SAS360CON_COMMAND = 17,

        SAS360CON_RECORD_EVENTS = 18,
        SAS360CON_RECORD_HISTS = 19,
    }

    public enum MEMORY_READ_STATE {
        SAS360CON_INTERNAL_CFG = 0,
        SAS360CON_CFG = 1,
        IOT_CFG = 2,
        UWB_INTERNAL_CFG = 3,

        SAS360CON_IMAGE = 4,
        IOT_IMAGE = 5,
        SAS360CON_MAINTENNANCE = 6,

        UWB_IMAGE = 7,

        CONSOLE_CLOSEST_TAGS_BASE_1 = 8,
        CONSOLE_CLOSEST_TAGS_BASE_2 = 9,
        CONSOLE_CLOSEST_TAGS_BASE_3 = 10,

        CONSOLE_CLOSEST_ZONE_BASE_1 = 11,
        CONSOLE_CLOSEST_ZONE_BASE_2 = 12,

        UWB_CLOSEST_TAGS_BASE = 13,
        UWB_CLOSEST_ZONE_BASE = 14,

        SAS360CON_NVREG = 15,
    }

    public enum MEMORY_CONFIG_TYPE : int
    {
        NONE = -1,

        SAS360CON_INTERNAL_CFG = 0,
        SAS360CON_CFG = 1,
        IOT_CFG = 2,
        SAS360CON_IMAGE = 3,
        IOT_IMAGE = 4,
        SAS360CON_MAINTENNANCE = 5,

        UWB_INTERNAL_CFG = 6,
        UWB_IMAGE = 7,

        CONSOLE_CLOSEST_TAGS_BASE_1 = 8,
        CONSOLE_CLOSEST_TAGS_BASE_2 = 9,
        CONSOLE_CLOSEST_TAGS_BASE_3 = 10,
        CONSOLE_CLOSEST_TAGS_EXTENDED = 11,

        CONSOLE_CLOSEST_ZONE_BASE_1 = 12,
        CONSOLE_CLOSEST_ZONE_BASE_2 = 13,
        CONSOLE_CLOSEST_ZONE_EXTENDED = 14,

        UWB_CLOSEST_TAGS_BASE = 15,
        UWB_CLOSEST_TAGS_EXTENDED = 16,
        UWB_CLOSEST_ZONE_BASE = 17,
        UWB_CLOSEST_ZONE_EXTENDED = 18,

        SAS360CON_NVREG = 19,
        SAS360CON_COMMANDS = 20,

        SAS360CON_EVENT_LOG = 21,
        SAS360CON_HIST_LOG = 22,
    }


    public enum MEMORY_SAS360CON_CFG_FIELD_POS_INDEX
    {
        ESTRUCTURE = 0,
        INSTALLATION = 1,
        VEHICLE_CFG = 2,
        DETECTION_AREA = 3,
        E_S = 4,
        TEMP_FILTERS = 5,
        UWB_COM = 6,
        UWB_TAG = 7,
        RECORDING = 8,
        RESERVED_FUTURE = 9,
        CALCULADAS = 10
    }

    public enum MEMORY_SAS360CON_IMAGE_FIELD_POS_INDEX
    {
        ESTADOS_BOOLEANOS = 0,
        EA_SENSORES = 1,
        TIEMPO_PROCESADO = 2,
        NVREG = 3,
        MAIN = 4,
        LIN_POLLING = 5,
        PROCESSED_TAGS = 6,
        FIELD_POS = 7,
    }


    public enum MEMORY_SAS360CON_CFG_FIELD_POS_INI
    {
        ESTRUCTURE = 0,
        INSTALLATION = 2,
        VEHICLE_CFG = 8,
        DETECTION_AREA = 20,
        E_S = 50,
        TEMP_FILTERS = 60,
        UWB_COM = 70,
        UWB_TAG = 80,
        RECORDING = 90,
        RESERVED_FUTURE = 106,
        CALCULADAS = 116
    }

    public enum MEMORY_SAS360CON_IMAGE_FIELD_POS_INI
    {
        ESTADOS_BOOLEANOS =  0,
        EA_SENSORES = 20,
        TIEMPO_PROCESADO = 30,
        NVREG = 40,
        MAIN = 50,
        LIN_POLLING = 60,
        PROCESSED_TAGS = 84,
        FIELD_POS = 110,
    }


    public enum MEMORY_SAS360CON_CFG_FIELD_SIZE
    {
        ESTRUCTURE = 2,
        INSTALLATION = 6,
        VEHICLE_CFG = 12,
        DETECTION_AREA = 30,
        E_S = 10,
        TEMP_FILTERS = 10,
        UWB_COM = 10,
        UWB_TAG = 10,
        RECORDING = 16,
        RESERVED_FUTURE = 10,
        CALCULADAS = 4
    }

    public enum MEMORY_SAS360CON_IMAGE_FIELD_SIZE
    {
        ESTADOS_BOOLEANOS = 20,
        EA_SENSORES = 10,
        TIEMPO_PROCESADO = 10,
        NVREG = 10,
        MAIN = 10,
        LIN_POLLING = 24,
        PROCESSED_TAGS = 26,
        FIELD_POS = 10,
    }


    public enum MEMORY_FIELD_POS_IN_ARRAY : int
    {
        ADDR = 0,
        POS = 1,
        SIZE = 2,
        VAR_TYPE = 3,
        VAR_NAME = 4,
        UNIT = 7,
        FORMAT = 8
    }

    public enum MEMORY_FIELD_POS_CSV : int
    {
        ADDR = 0,
        NAME = 1,
        TYPE_NAME = 2,
        UNIT = 3,
        FORMAT = 4
    }


    #endregion



    public enum DETECTION_AREA_POS_IN_ARRAY
    {
        YELLOW = 0,
        ORANGE = 1,
        RED = 2,
        INTERIOR = 3,
    }

    public enum ACTUACIONES_SALIDAS_POS_IN_ARRAY {
        RELE_1 = 0,
        RELE_2 = 1,
        RELE_3 = 2,
        RELE_4 = 3,
        TRANS_1 = 4,
        TRANS_2 = 5,
    }


    public enum BUTTON_EDIT_INTERNAL_CONFIG_POS {
        SERIAL_NUMBER = 0,
        ID_MANUFACT = 1,
        ID_TAG = 2,
        RTU_SLAVE_SPEED = 3,
        RTU_SLAVE_NUM = 4,
        LIN_MASTER_SPEED = 5
    }

    public enum LABEL_POPUP_IMAGE_READ
    {
        INPUT = 0,
        OUTPUT_INT = 1,
        OUTPUT_EXT = 2,
        OUTPUT_LED = 3,
        INTERNAL_ERROR = 4,
        LIN_POOLING_READ= 5,
        LIN_POOLING_WRITE = 6,
        LIN_POOLING_WRITE_BROADCAST = 7,
        LIN_POOLING_STATE = 8,
    }

    public enum LABEL_ANTENNA_ARRAY_POS {
        POS_X = 0,
        POS_Y = 1,
    }


    public enum RADIO_CSV_TAG_ZONE_TYPE{
        NONE =0,
        TAG =1,
        ZONE = 2
    }

    public enum RADIO_CSV_MEMORY_TYPE { 
        NONE = 0,
        BASE = 1,
        EXTENDED = 2,
    }


    public enum AUDIO
    {
        SPANISH = 0,
        ENGLISH = 1
    }

    public enum ANTENNA_TYPE
    {
        NONE = 0,
        FL_FR_RC = 1,
    }

    public enum POOLIN_REQUEST { 
        _5C_BASE =1,
        _12C_BASE = 2,
        _12C_EXTENDED = 3,
        _12C_IMAGE = 4,
    }

    public enum TEMPORIZADOR_POS{
        ESTADOS =0,
        SUBSTADOS = 1,
        SUPERVISOR = 2
    }

    public enum CONTROL_LEDS_POS { 
        ERROR =0,
        PEATON = 1,
        VEHICULO_LIGERO = 2,
        VEHICULO_PESADO = 3,
        SLOW = 24,
        ON = 25,
        DRIVER = 26

    }


    #region MASK

    public enum MASK_SAS360CON_STATE : ushort
    {
        NOT_DEFINED = 0,
        INIT_AUTODIAG_SAS360CON = 1,
        INIT_GESTION_UPDATES = 2,
        NIT_VERIFICACION_LIN = 3,
        INIT_AUTODET_SELF_CONTAG = 4,
        INIT_END = 5,
        STANDARD_DETECTION = 6,
        LOW_POWER_MODE = 7,
        INTERNAL_ERROR = 8,
        STATE_UNKNOWN = 9,
    }

    public enum MASK_SAS360CON_SUBSTATE : ushort
    {
        INDEX_VALUE_0 = 0,
        INDEX_VALUE_1 = 1,
        INDEX_VALUE_2 = 2,
        INDEX_VALUE_3 = 3,
        INDEX_VALUE_4 = 4,
        NOT_DEFINED = 5,
    }

    public enum MASK_AUTOTEST
    {
        DWM_OK = 0,
        AUTOTEST_LEDS_OK = 1,
        FLASH_MEMORY = 2
    }

    public enum MASK_RESET_CAUSE
    {
        POWER_ON = 0,
        WATCHDOG = 1,
        SOFTWARE_RESET = 2,
        OTHER = 3
    }

    public enum MASK_IN_MNG {
        FIRMWARE_UPDATE_PENDING = 0,
        CONFIG_UPDATE_PENDING = 1,
        FIRMWARE_UPDATE_EXECUTED = 2,
        CONFIG_UPDATE_EXECUTED = 3
    }


    public enum MASK_ERROR
    {
        SAS360CON_ERROR = 0,
        INIT_SELF_CONTAG_DETECTION = 1,
        INIT_SAS36OUWB_DETECTION = 2,
        FORCED_DIGITAL_OUTPUTS = 3,
        FORCED_LEDS = 4,
        FORCED_AUDIOS = 5,
        OPE_SELF_CONTAG_DETECTION = 6,
        OPE_SAS36OUWB_DETECTION = 7,
        OPE_TAG_CONFIG_MODE = 8,
    }

    public enum MASK_LIN_READ_POOL
    {
        IMAGE_ENABLED = 0,
        _12C_TAG_BASE_ENABLED = 1,
        _12C_TAG_EXT_ENABLED = 2,
        _12C_ZONE_BASE_ENABLED = 3,
        _12C_ZONE_EXT_ENABLED = 4,
        INTERNAL_CONFIG_SINGLE = 5,
        LAST_COMMAND_SINGLE = 6
    }

    public enum MASK_LIN_WRITE_POOL
    {
        CMD_SET_RTC_SINGLE =0,
        CMD_CLEAR_RESET_SINGLE = 1,
        CMD_SET_CONFIG_SINGLE = 2,
        CMD_SET_SELF_CONTAG_SINGLE = 3,
        CMD_SET_SELF_DRVTAG_SINGLE = 4,
        CMD_GATEWAY_TAG_ARRAY = 8,
        CMD_GATEWAY_ZONE_ARRAY = 9
    }

    public enum MASK_LIN_WRITE_POOL_BROADCAST { 
        CMD_TAG_BROADCAST_CODIF_1_5 =0,
        CMD_TAG_BROADCAST_CODIF_6_10 = 1,
        CMD_TAG_BROADCAST_CODIF_11_15 = 2,
        CMD_ZONE_BROADCAST_CODIF_1_5 = 3,
        CMD_ZONE_BROADCAST_CODIF_6_10 = 4,
        CMD_ZONE_BROADCAST_CODIF_11_15 = 5,
    }

    public enum MASK_LIN_STATE:ushort
    {
        COM_UWB1_OK = 0,
        COM_UWB2_OK = 1,
        COM_UWB3_OK = 2,
        COM_UWB4_OK = 3,
        INTEGRITY_UWB_1_OK = 4,
        INTEGRITY_UWB_2_OK = 5,
        INTEGRITY_UWB_3_OK = 6,
        INTEGRITY_UWB_4_OK = 7,
    }

    public enum MASK_UWB_CODIF
    {
        STATUS_OK =0,
        INTEGRITY_OK = 1,
        CONFIGURED_OK = 2,
        POWER_ON_RESET_CLEARED = 3,
        SELF_CONTAG_DETECTED = 4,
        SELF_DRVTAG_DETECTED = 5,
        SELF_CONTAG_CONFIGURED_OK = 6,
        SELF_DRVTAG_CONFIGURED_OK = 7,
        SELF_CON_CODE_CONFIGURED_OK = 8,
        ERROR_BIT0 = 12,
        ERROR_BIT1 = 13,
        ERROR_BIT2 = 14,
        ERROR_BIT3 = 15,
    }

    #endregion



    #region TAG -ZONE

    public enum MASK_TAG_STATUS: byte
    {
        TAG_OK = 0,
        INTENAL_ERROR = 1,
        BATTERY_OK = 2,
        BATTERY_CHARGING = 3,
        VIBRATING = 4,
        MAINTENANCE_MODE_ACTIVE = 5,
        ALARMA_AREA_A = 6,
        ALARMA_AREA_N = 7,
        ALARMA_AREA_R = 8,
        ALARMA_CONSOLA_NOT_OK = 9,
        TBD = 10,
        AREA_DETECCION = 11,
        ERROR_CODE_BIT0 = 12,
        ERROR_CODE_BIT1 = 13,
        ERROR_CODE_BIT2 = 14,
        ERROR_CODE_BIT3 = 15,
    }

    public enum MASK_SAS360TAG_UWB_COMMAND : byte
    {
        CMD_UWBx_ORIGING_BIT0 = 0,
        CMD_UWBx_ORIGING_BIT1 = 1,
        CMD_UWBx_ORIGING_BIT2 = 2,
        ALARMA_AREA_A_ACTIVE = 3,
        ALARMA_AREA_N_ACTIVE = 4,
        ALARMA_AREA_R_ACTIVE = 5,
        CONSOLA_OK = 6,
        TBD0 = 7,
        TBD1 = 8,
        TBD2 = 9,
        TBD3 = 10,
        TBD4 = 11,
        REPORTED_REGISTER_BIT0 = 12,
        REPORTED_REGISTER_BIT1 = 13,
        REPORTED_REGISTER_BIT2 = 14,
        REPORTED_REGISTER_BIT3 = 15,
    }


    public enum MASK_ZONE_STATUS : byte
    {
        ZONE_OK = 0,
        ZONE_INTERNAL_ERROR = 1,
        TBD_1 = 2,
        TBD_2 = 3,
        TBD_3 = 4,
        TBD_4 = 5,
        TBD_5 = 6,
        TBD_6 = 7,
        REPORTED_REGISTER_BIT0 = 8,
        REPORTED_REGISTER_BIT1 = 9,
        REPORTED_REGISTER_BIT2 = 10,
        REPORTED_REGISTER_BIT3 = 11,
        ERROR_CODE_BIT0 = 12,
        ERROR_CODE_BIT1 = 13,
        ERROR_CODE_BIT2 = 14,
        ERROR_CODE_BIT3 = 15,
    }

    public enum MASK_TAG_ZONE_TYPE
    {
        SAS360TAG_PED = 0x00,
        SAS360TAG_DRV = 0x01,
        SAS360CON_TAG_LV = 0x02,
        SAS360CON_TAG_HV = 0x03,
        RESERVED = 0x04,
        SAS360ZONE_CIRC_R_SLOW = 0x05,
        SAS360ZONE_REC_P1_SLOW = 0x06,
        SAS360ZONE_REC_P2_SLOW = 0x07,
        SAS360ZONE_REC_P3_SLOW = 0x08,
        SAS360ZONE_REC_P4_SLOW = 0x09,
        SAS360ZONE_INHIBIT_RAD = 0x0A,
        UNKNOWN = 255,
    }


    #endregion


    #region DIGITAL STATES

    public enum DIGITAL_STATES_IN_LIST { 
        INPUT =0,
        OUTPUT_1_INT = 1,
        OUTPUT_2_EXT = 2,
        OUTPUT_3_LED = 3,
        CODIF_LED_1 = 4,
        CODIF_LED_2 = 5,
        AUDIO_1 = 6,
        AUDIO_2 = 7,
    }
    public enum MAINT_FORCED_DIGITAL_STATES_IN_LIST
    {
        OUTPUT_1_INT = 1,
        OUTPUT_2_EXT = 2,
        OUTPUT_3_LED = 3,
        CODIF_LED_1 = 4,
        CODIF_LED_2 = 5,
        AUDIO_1 = 6,
        AUDIO_2 = 7,
    }


    public enum MASK_CODIF_DI1
    {
        M_DI_DEBUG_SWITCH = 0,
        M_DI_RESET_SWITCH = 1,
        M_DI_MOD_POWER_SAVE = 2,
        M_DI_MOD_STATUS = 3,
        M_DI_MOD_RI = 4,
        M_DI_VER_HW_0 = 5,
        M_DI_VER_HW_1 = 6,
        RESERVED_7 = 7,
        M_DI_UWB_INT = 8,
        DI_ACCEL_INT1 = 9,
        DI_ACCEL_INT2 = 10,
    }

    public enum FORCE_MASK_DO1
    {
        M_DO_EN_REG_4V1 = 0,
        M_DO_EN_DF_SWITCH = 1,
        M_DO_EN_CONNBOARD = 2,
        M_DO_EN_LIN_1 = 3,
        M_DO_EN_LIN_2 = 4,
        M_DO_EN_LIN_3 = 5,
        M_DO_DEBUG_LED1 = 6,
        M_DO_EN_LED_DRIVER = 7,
        M_DO_MOD_RESET = 8,
        M_DO_MOD_POWER_KEY = 9,
        M_DO_MOD_DTR = 10,
        M_DO_ACCEL_WAKE_UP = 11
    }

    public enum FORCE_MASK_DO2
    {
        M_DO_RELE_1 =0,
        M_DO_RELE_2 = 1,
        M_DO_RELE_3 = 2,
        M_DO_RELE_4 = 3,
        M_DO_TRANSISTOR_1 = 4,
        M_DO_TRANSISTOR_2 = 5,
        M_DO_EN_12VOUT = 6
    }

    public enum FORCE_MASK_DO3
    {
        M_DO_LED_P2_A = 0,
        M_DO_LED_P7_A = 1,
        M_DO_LED_P8_A = 2,
        M_DO_LED_P9_A = 3,
        M_DO_LED_P10_A = 4,
        M_DO_LED_P11_A = 5,
        M_DO_LED_P1_K = 6,
        M_DO_LED_P3_K = 6,
        M_DO_LED_P4_K = 6,
        M_DO_LED_P5_K = 6,
        M_DO_LED_P6_K = 6,
    }

    public enum FORCE_MODE_CODIF
    {
        M_FORCE_DIGITAL_OUTPUTS = 0,
        M_FORCE_LEDS = 1,
        M_AUDIO_TO_PLAY = 2,
    }


    #endregion
}
