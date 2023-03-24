namespace sas360_test
{
    public enum BIT_STATE
    {
        OFF = 0,
        ON = 1
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
        ERROR_READ = 5,
        ERROR_WRITE = 6,
    }


    public enum ENABLE_READ_MEMORY_BIT
    {
        SAS360CON_INTERNAL_CONFIG = 0,
        SAS360CON_CONFIG_SAS360CON = 1,
        SAS360CON_CONFIG_IOT_AZURE = 2,
        SAS360CON_IMAGEN_SAS360CON = 3,
        SAS360CON_IMAGEN_IOT_AZURE = 4,

        SAS360CON_TAG_CLOSEST_BASE_CON = 5,
        SAS360CON_TAG_CLOSEST_EXTENDED_CON = 6,
        SAS360CON_TAG_CLOSEST_BASE_UWB = 7,
        SAS360CON_TAG_CLOSEST_EXTENDED_UWB = 8,

        SAS360CON_ZONE_CLOSEST_BASE_CON = 9,
        SAS360CON_ZONE_CLOSEST_EXTENDED_CON = 10,

        SAS360CON_NVREG = 11,
    }



    public enum MEMORY_READ_STATE {
        IMAGE_SAS360CON = 0,
        IMAGE_IOT = 1,
        CONSOLE_CLOSEST_TAGS_BASE = 2,
        UWB_CLOSEST_TAGS_BASE = 3,
        CONSOLE_CLOSEST_ZONE_BASE = 4,
        NVREG = 5,
    }

    public enum MEMORY_MAP_READ
    {
        SAS360CON_INTERNAL_CONFIG = 100,
        SAS360CON_CONFIGURATION_SAS360CON = 200,
        SAS360CON_CONFIGURATION_IOT_AZURE = 400,
        SAS360CON_IMAGEN_SAS360CON = 600,
        SAS360CON_IMAGEN_IOT_AZURE = 800,
        SAS360CON_NVREG = 900,
        SAS360CON_TAG_12_CLOSEST_BASE_CON = 1000,
        SAS360CON_TAG_12_CLOSEST_EXTENDED_CON = 1100,
        SAS360CON_ZONE_16_CLOSEST_BASE_CON = 1400,
        SAS360CON_ZONE_16_CLOSEST_EXTENDED_CON = 1500,
        SAS360CON_TAG_12C_BASE_UWB_1 = 2000,
        SAS360CON_TAG_12C_BASE_UWB_2 = 2100,
        SAS360CON_TAG_12C_BASE_UWB_3 = 2200,
        SAS360CON_TAG_12C_EXTENDED_UWB_1 = 3000,
        SAS360CON_TAG_12C_EXTENDED_UWB_2 = 3200,
        SAS360CON_TAG_12C_EXTENDED_UWB_3 = 3400,
        SAS360CON_RECORDED_EVENTS = 5000,
        SAS360CON_RECORDS_HISTORICS = 10000,
    }

    public enum MEMORY_MAP_WRITE
    {
        SAS360CON_CONFIGURATION_SAS360 = 200,
        SAS360CON_CONFIGURATION_IOT = 400,
        SAS360CON_COMMANDS = 0
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

    public enum MEMORY_CONFIG_TYPE : int
    {
        INTERNAL_CONFIG = 0,
        CONFIG_SAS360CON = 1,
        CONFIG_IOT = 2,
        IMAGE_SAS360CON = 3,
        IMAGE_IOT = 4,
        CONSOLE_CLOSEST_TAGS_BASE = 5,
        CONSOLE_CLOSEST_TAGS_EXTENDED = 6,
        UWB_CLOSEST_TAGS_BASE = 7,
        UWB_CLOSEST_TAGS_EXTENDED = 8,
        CONSOLE_CLOSEST_ZONE_BASE = 9,
        CONSOLE_CLOSEST_ZONE_EXTENDED = 10,
        NVREG = 13,
    }



    public enum DETECTION_AREA_COLORS
    {
        RED = 0,
        ORANGE = 1,
        YELLOW = 2,
        GENERAL = 3,
    }

    public enum DETECTION_AREA_POS_IN_ARRAY
    {
        FRONT = 0,
        RIGHT = 1,
        BACK = 2,
        LEFT = 3
    }

    public enum ACTUACIONES_SALIDAS_POS_IN_ARRAY { 
        RELE_1 =0,
        RELE_2 = 1,
        RELE_3 = 2,
        RELE_4 = 3,
        TRANS_1 = 4,
        TRANS_2 = 5,
    }

    public enum HYSTERESYS_POS_IN_ARRAY {
        AREA_D = 0,
        AREA_A = 1,
        AREA_N = 2,
        AREA_R = 3,
        SECTOR_CHANGE_ANGLE = 4,
        CLOSEST_ANTENNA_CHANGE = 5,
        CLOSEST_5C_CHANGE = 6
    }


    public enum SAS360CON_STATE : byte
    {
        STATE_INIT_AUTODIAG_SAS360CON_U8 = 0,
        STATE_INIT_GESTION_DE_ACTUALIZACIONES_U8 = 1,
        STATE_INIT_VERIFICACIÓN_LIN_U8 = 2,
        STATE_INIT_AUTODET_SELF_CONTAG_U8 = 3,
        STATE_INIT_END_U8 = 4,
        STATE_STANDARD_DETECTION_U8 = 5,
        STATE_LOW_POWER_MODE_U8 = 6,
        STATE_LOW_INTERNAL_ERROR_U8 = 7,
        UNDEFINED = 8,
    }

    public enum SAS360CON_SUBSTATE : byte
    {
        SUBSTATE_INDEX_TBD_U8_0 = 0,
        SUBSTATE_INDEX_TBD_U8_1 = 1,
        SUBSTATE_INDEX_TBD_U8_2 = 2,
        UNDEFINED = 8,
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
        LIN_POOLING_CONFIG = 5,
        LIN_POOLING_STATE = 6,
    }


    public enum LABEL_ANTENNA_ARRAY_POS { 
        POS_X = 0, 
        POS_Y = 1,
        ORIENTATION = 2
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


    public enum MASK_INTERNAL_ERROR
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

    public enum MASK_LIN_POOLING_CONFIG
    {
        POOL_LIN1_ENABLED = 0,
        POOL_LIN2_ENABLED = 1,
        POOL_LIN3_ENABLED = 2,
        POOL_LIN4_RES_ENABLED = 3,
        POOL_LIN5_RES_ENABLED = 4,
        POOL_LIN6_RES_ENABLED = 5,
        POOL_LIN7_RES_ENABLED = 6,
        POOL_LIN8_RES_ENABLED = 7,
        POOL_UWB_IMAGE_READ_ENABLED = 8,
        POOL_UWB_5C_TAG_BASE_ENABLED = 9,
        POOL_UWB_5C_TAG_EXT_ENABLED = 10,
        POOL_12C_TAG_BASE_ENABLED = 11,
        POOL_12C_TAG_EXT_ENABLED = 12,
        POOL_15C_ZONE_BASE_ENABLED = 13,
        POOL_15C_ZONE_EXT_ENABLED = 14,
    }

    public enum MASK_LIN_STATE
    {
        STATE_LIN1_OK = 0,
        STATE_LIN2_OK = 1,
        STATE_LIN3_OK = 2,
        STATE_LIN4_RES_OK = 3,
        STATE_LIN5_RES_OK = 4,
        STATE_LIN6_RES_OK = 5,
        STATE_LIN7_RES_OK = 6,
        STATE_LIN8_RES_OK = 7,
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



    public enum SEND_COMMAND_STATE { 
        OK = 1,
        ERROR = 2,
        WARNING = 3
    }





    #region TAG

    public enum SAS360TAG_CODIF_STATE: byte
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

    public enum SAS360TAG_UWB_COMMAND_CODIF : byte
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

    public enum SAS360TAG_BATTERY_LEVEL : byte
    {
        LEVEL_00_00 = 0,
        LEVEL_12_50 = 1,
        LEVEL_25_00 = 2,
        LEVEL_27_50 = 3,
        LEVEL_50_00 = 4,
        LEVEL_62_50 = 5,
        LEVEL_75_00 = 6,
        LEVEL_87_50 = 7,
    }


    public enum SAS360ZONE_CODIF_STATE : byte
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

    public enum SAS360TAG_ZONE_TYPE
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
        OUTPUT_1 = 1,
        OUTPUT_2 = 2,
        OUTPUT_3 = 3,
        LED_1 = 4,
        LED_2 = 5,
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
