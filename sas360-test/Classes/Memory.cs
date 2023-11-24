using System;
using System.ComponentModel;

namespace sas360_test
{
    public class SAS360CON_internal_cfg
    {
        public string Serial_number { get; set; } = string.Empty;

        public string ID_manufacturing { get; set; } = string.Empty;

        public MASK_TAG_ZONE_TYPE Tag_type { get; set; }

        public string Version_hw { get; set; } = string.Empty;

        public string Version_fw { get; set; } = string.Empty;

        public string Version_boot { get; set; } = string.Empty;

        public string RTU_slave_speed { get; set; } = string.Empty;
        public ushort RTU_slave_num { get; set; } = new ushort();

        public string Lin_master_speed { get; set; } = string.Empty;

        public ushort Consola_id { get; set; } = new ushort();

        public string RTC_fw_update { get; set; } = string.Empty;

        public string RTC_config_update { get; set; } = string.Empty;

        public ushort Change_counter { get; set; } = new ushort();

        public ushort CRC_config { get; set; } = new ushort();
    }


    public class SAS360CON_cfg_general
    {
        //Actuaciones E/ S
        public ushort[] Array_actuaciones_salidas { get; set; } = Array.Empty<ushort>();

        //Temporizadores y filtros
        public ushort Output_deactivation_delay_sec { get; set; } = new ushort();
        public ushort Area_zone_dist_cm { get; set; } = new ushort();
        public ushort Red_zone_alert_audio_repeat_sec { get; set; } = new ushort();

        //UWB COM
        public byte[] Array_lin_used { get; set; } = Array.Empty<byte>();
        public byte[] Array_lin_modbus_slave { get; set; } = Array.Empty<byte>();



        //UWB TAG config reserved
        public ushort Clear_undetected_tag_decseg { get; set; } = new ushort();



        //Calculadas config
        public string RTC_last_config_change { get; set; } = string.Empty;
        public ushort Change_counter { get; set; } = new ushort();
        public ushort CRC_config { get; set; } = new ushort();
    }


    public class SAS360CON_cfg_installation_client
    {
        public ushort Client { get; set; }
        public ushort Installation { get; set; }
        public ushort Vehicle_type { get; set; }
        public ushort Audio_language { get; set; }

        public ushort Audio_volume { get; set; }
    }

    public class SAS360CON_cfg_vehicle_cfg
    {
        public ushort[] Vehicle_dim_xy_cm { get; set; } = Array.Empty<ushort>();
        public short[,] Antenna_xy_cm { get; set; } = new short[0, 0];
    }

    public class SAS360CON_cfg_detection_area
    {
        public ushort[] Array_area_FRONT_ANRI_dist_cm { get; set; } = Array.Empty<ushort>();

        public ushort[] Array_area_RIGHT_ANRI_dist_cm { get; set; } = Array.Empty<ushort>();

        public ushort[] Array_area_BACK_ANRI_dist_cm { get; set; } = Array.Empty<ushort>();

        public ushort[] Array_area_LEFT_ANRI_dist_cm { get; set; } = Array.Empty<ushort>();

        public ushort Area_detection_distance_cm { get; set; } = new ushort();

        public ushort Area_change_hysteresis_cent_pct { get; set; } = new ushort();

        public ushort Sector_change_hysteresis_cent_pct { get; set; } = new ushort();

        public ushort Trilat_calc_enabled { get; set; } = new ushort();


        public ushort[] Array_area_DIST_ANTENA_ANRI_dist_cm { get; set; } = Array.Empty<ushort>();

        public ushort Gestion_avanzada_position_enable { get; set; } = new ushort();
    }

    public class SAS360CON_cfg_recording
    {
        //Recording
        public byte[] Array_recorded_register_index { get; set; } = Array.Empty<byte>();
        public byte[] Array_recorded_register_unit_codif { get; set; } = Array.Empty<byte>();
        public ushort[] Array_recorded_register_period_secs { get; set; } = Array.Empty<ushort>();
    }



    public class SAS360CON_image_general
    {
        public uint RTC_UTC_seconds { get; set; } = new uint();

        public ushort Milliseconds { get; set; } = new ushort();

        public ushort Watchdog_inc { get; set; } = new ushort();

        public uint Global_dmsec_counter { get; set; } = new uint();

        public MASK_SAS360CON_STATE? Sas360_state { get; set; } 
        public MASK_SAS360CON_SUBSTATE? Sas360_substate { get; set; }


        //Estado booleanos generales
        public ushort[] Array_codif_bits { get; set; } = Array.Empty<ushort>();

        public ushort[] Array_codif_management { get; set; } = Array.Empty<ushort>();

        public ushort[] Array_digital_states { get; set; } = Array.Empty<ushort>();


        //Entradas analogicas / sensores
        public ushort EA_4v1_power_mv { get; set; } = new ushort();
        public ushort EA_shunt_leds_ma { get; set; } = new ushort();
        public ushort Voltage_adcref_3v3_mv { get; set; } = new ushort();
    }


    public class SAS360CON_image_procesado_contadores
    {
        public ushort Total_polling_cycle_counter { get; set; } = new ushort() ;
        public ushort Polling_cycle_execution_time_dmsec { get; set; } = new ushort();
        public ushort Time_processing_int_1msec_actions_dmseg { get; set; } = new ushort();
        public ushort Time_processing_main_10msec_actions_dmseg { get; set; } = new ushort();
        public ushort Time_processing_main_100msec_actions_dmseg { get; set; } = new ushort();
        public ushort Time_processing_main_1sec_actions_dmseg { get; set; } = new ushort();
        public ushort Time_processing_int_1msec_MAX_actions_dmseg { get; set; } = new ushort();
        public ushort Time_processing_int_10msec_MAX_actions_dmseg { get; set; } = new ushort();
    }


    public class SAS360CON_image_nvreg_management
    {
        public ushort Internal_change_counter { get; set; } = new ushort();
        public ushort Config_con_change_counter { get; set; } = new ushort();
        public ushort Config_iot_change_counter { get; set; } = new ushort();
        public ushort Nvreg_change_counter { get; set; } = new ushort();

        public uint Last_recorded_event_absolute_index_copy { get; set; } = new uint();
        public uint Num_recorded_events_copy { get; set; } = new uint();
        public uint Last_recorded_event_array_position_copy { get; set; } = new uint();
    }


    public class SAS360CON_image_main_management
    {
        public ushort Internal_error { get; set; } = new ushort();
        public ushort Error_code_detail { get; set; } = new ushort();
        public ushort Active_warning_id { get; set; } = new ushort();
        public ushort Warning_exceded_T15C_tag_number { get; set; } = new ushort();
        public uint Last_event_log_rtc { get; set; } = new uint();

        public ushort Last_event_log_msec { get; set; } = new ushort();
        public ushort Last_event_log_id { get; set; } = new ushort();

        public ushort[] Last_event_log_value { get; set; } = Array.Empty<ushort>();

    }

    public class SAS360CON_image_lin_pooling
    {
        public ushort[] Array_lin_pooling_read_uwb { get; set; } = Array.Empty<ushort>();
        public ushort[] Array_lin_pooling_write_uwb { get; set; } = Array.Empty<ushort>();
        public ushort Lin_pooling_write_broadcast { get; set; } = new ushort();
        public ushort Lin_pooling_state { get; set; } = new ushort();
        public ushort Actual_pooled_uwb { get; set; } = new ushort();

        public ushort Actual_pooling_request_group { get; set; } = new ushort();
        public ushort Actual_pooling_request_index { get; set; } = new ushort();

        public ushort[] Array_lin_com_total_counter { get; set; } = Array.Empty<ushort>();
        public ushort[] Array_lin_com_error_counter { get; set; } = Array.Empty<ushort>();

        public ushort[] Array_lin_total_last_cycle_time { get; set; } = Array.Empty<ushort>();

        public ushort Total_lin_poolin_time { get; set; } = new ushort();
    }


    public class SAS360CON_image_processed_tag
    {
        public ushort[] Array_assigned_self_contag_id { get; set; } = Array.Empty<ushort>();
        public ushort[] Array_assigned_self_drvtag_id { get; set; } = Array.Empty<ushort>();
        public byte[] Array_number_total_TAGS_in_area_DANR { get; set; } = Array.Empty<byte>();
        public byte[] Array_number_total_PED_in_area_DANR { get; set; } = Array.Empty<byte>();
        public byte[] Array_number_total_DRV_in_area_DANR { get; set; } = Array.Empty<byte>();
        public byte[] Array_number_total_LV_in_area_DANR { get; set; } = Array.Empty<byte>();
        public byte[] Array_number_total_HV_in_area_DANR { get; set; } = Array.Empty<byte>();
        public byte[] Array_number_total_ZONES_in_area_DANR { get; set; } = Array.Empty<byte>();
        public byte Number_zones_slow_range { get; set; } = new byte();
        public byte Reported_register_uwb_index { get; set; } = new byte();
        public byte Reported_register_tag_index { get; set; } = new byte();
        public ushort Estado_leds_amarillo { get; set; } = new ushort();
        public ushort Estado_leds_naranja { get; set; } = new ushort();
        public ushort Estado_leds_rojo { get; set; } = new ushort();
        public ushort Closest_DRVTAG_tagID_2LSB { get; set; } = new ushort();
    }

    public class SAS360CON_image_field_position
    {
        public int Installation_pos_x_cm { get; set; } = new int();
        public int Installation_pos_y_cm { get; set; } = new int();

        public int Latitud { get; set; } = new int();

        public int Longitud { get; set; } = new int();

        public ushort Contador_alerta_entrada_en_area_roja { get; set; } = new ushort();
        public ushort Contador_alerta_entrada_en_area_naranja { get; set; } = new ushort();
    }


    public class SAS360CON_maintennance
    {
        public ushort Autotest_bit_check_codif { get; set; } = new ushort();
        public ushort Autotest_ea_4V1_POWER_value_mV { get; set; } = new ushort();
        public ushort Autotest_reset_cause_codif { get; set; } = new ushort();
        public ushort Autotest_manteni_type_codif { get; set; } = new ushort();
        public ushort[] Autotest_ea_consumo_grupo_LEDS_ma { get; set; } = Array.Empty<ushort>();
        public ushort Forced_mask_DO1_INT { get; set; } = new ushort();
        public ushort Forced_mask_DO2_EXT { get; set; } = new ushort();
        public ushort Forced_mask_DO3_LED { get; set; } = new ushort();
        public ushort Forced_mask_codif_LED1 { get; set; } = new ushort();
        public ushort Forced_mask_codif_LED2 { get; set; } = new ushort();

        public ushort Forced_mask_AUDIO_1_to_play { get; set; } = new ushort();
        public ushort Forced_mask_AUDIO_2_to_play { get; set; } = new ushort();
    }

    public class SAS360CON_nvreg
    {
        public ushort Tam_bytes { get; set;}
        public ushort Nvreg_version { get; set; }

        public uint Events_last_event_offset_flash { get; set; }
        public uint Events_total_events_flash { get; set; }
        public uint Events_last_page_used_flash { get; set; }
        public uint Events_total_events { get; set; }
        public uint Ram_buffer_last_event_index { get; set; }

        public uint Historics_last_historic_offset_flash { get; set; }
        public uint Historics_total_historics_flash { get; set; }
        public uint Historics_last_page_used_flash { get; set; }

        public uint Nvreg_change_counter { get; set; }
        public ushort CRC_eeprom { get; set; }
    }



    public class SAS360CON_TAG : INotifyPropertyChanged
    {
        public int _index { get; set; }
        public int Index
        {
            get { return _index; }
            set { _index = value; OnPropertyChanged("Index"); }
        }


        //BASE
        public ushort ID_2LSB { get; set; } = new ushort();

        public byte Tag_type_value { get; set; } = new byte();
        public string Tag_type_id_grid { get; set; } = string.Empty;
        public MASK_TAG_ZONE_TYPE Tag_type { get; set; } = MASK_TAG_ZONE_TYPE.UNKNOWN;


        public byte FW_version_value { get; set; } = new byte();
        public string FW_version { get; set; } = string.Empty;

        public ushort Estado_codificado { get; set; }
        public byte Estado_ec { get; set; } = new byte();
        public byte Estado_rr { get; set; } = new byte();

        public short Calc_tag_position_abs_X_cm { get; set; }
        public short Calc_tag_position_abs_Y_cm { get; set; }

        public ushort Received_command { get; set; }
        public byte Received_command_ec { get; set; } = new byte();
        public byte Received_command_rr { get; set; } = new byte();

        public byte Battery_level_pct { get; set; } = new byte();
        public byte Zone_cfg_dist_m { get; set; } = new byte();


        public byte Consola_code_alarma { get; set; } = new byte();

        public ushort Reported_register { get; set; } = new ushort();

        public byte Calc_uwb_detection { get; set; } = new byte();
        public byte Worst_time_last_success_decseg { get; set; } = new byte();

        public byte Calc_det_area { get; set; } = new byte();
        public byte Calc_direccion { get; set; } = new byte();
        public byte Calc_led_sector { get; set; } = new byte();

        public ushort Calc_num_det_ant { get; set; } = new ushort();

        public ushort Dist_closest_cm { get; set; } = new ushort();

        public ushort[] Dist_from_antenna_cm { get; set; } = Array.Empty<ushort>();
        public string Dist_from_antenna_grid { get; set; } = string.Empty;

        public byte[] Tag_time_last_success_decsec { get; set; } = Array.Empty<byte>();
        public string Time_last_success_grid { get; set; } = string.Empty;



        #region EXTENDED
        public ushort ID_2LSB_extended { get; set; } = new ushort();

        public ushort ID_bytes_3_4 { get; set; } = new ushort();
        public ushort ID_bytes_5_6 { get; set; } = new ushort();
        public ushort ID_bytes_7_8 { get; set; } = new ushort();
        public ushort PDOA_dec { get; set; } = new ushort();
        public ushort Time_of_flight_us { get; set; } = new ushort();

        public byte Last_charge_battery_level { get; set; } = new byte();

        public ushort Velocidad_tag_cm { get; set; } = new ushort();
        public ushort Tiempo_desde_carga { get; set; } = new ushort();

        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string nameProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameProp));
        }
    }

    public class SAS360CON_ZONE : INotifyPropertyChanged
    {
        public int _index { get; set; }
        public int Index
        {
            get { return _index; }
            set { _index = value; OnPropertyChanged("Index"); }
        }


        //BASE
        public ushort ID_2LSB { get; set; }

        public byte Zone_type_value { get; set; }
        public string Zone_type_id_grid { get; set; } = string.Empty;
        public MASK_TAG_ZONE_TYPE Zone_type { get; set; }


        public byte FW_version_value { get; set; }
        public string FW_version { get; set; } = string.Empty;

        public ushort Estado_codificado { get; set; }
        public byte Estado_ec { get; set; }
        public byte Estado_rr { get; set; }

        public short Calc_zone_position_abs_X_cm { get; set; }
        public short Calc_zone_position_abs_Y_cm { get; set; }

        public ushort Received_command { get; set; }
        public byte Received_command_ec { get; set; }
        public byte Received_command_rr { get; set; }

        public ushort Radio_action { get; set; }
        public ushort Reported_register { get; set; }

        public ushort Dist_closest_cm { get; set; } = new ushort();

        public ushort[] Dist_from_antenna_cm { get; set; } = Array.Empty<ushort>();
        public string Dist_from_antenna_grid { get; set; } = string.Empty;


        #region EXTENDED

        public ushort ID_2LSB_extended { get; set; }
        public ushort ID_bytes_3_4 { get; set; }
        public ushort ID_bytes_5_6 { get; set; }
        public ushort ID_bytes_7_8 { get; set; }
        public ushort Pos_x { get; set; }
        public ushort Pos_y { get; set; }
        public short Latitude { get; set; }
        public short Longitude { get; set; }

        #endregion


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string nameProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameProp));
        }
    }


    public class SAS360CON_UWB
    {
        //Individual management
        public ushort Lin { get; set; }
        public ushort Slave { get; set; }

        public ushort Pool_read { get; set; }

        public ushort Pool_write { get; set; }

        public ushort Comm_total { get; set; }
        public ushort Com_error { get; set; }

        public ushort Cycle_time { get; set; }


        //Imagen uwb
        public uint RTC_UTC_value { get; set; }
        public string RTC_UTC_DATE { get; set; } = string.Empty;
        public ushort RTC_millisecs { get; set; }
        public ushort Watchdog_inc { get; set; }
        public ushort Codif_state { get; set; }
        public ushort Delay_in_image_pool { get; set; }

        public ushort Num_tags { get; set; }
        public ushort Num_zones { get; set; }

        public byte[] Array_contag_id { get; set; } = Array.Empty<byte>();
        public byte[] Array_drvtag_id { get; set; } = Array.Empty<byte>();

        public ushort Antenna_number_in_CON { get; set; }

        public ushort Installation_ID { get; set; }

        public ushort Reported_register { get; set; }
        public ushort War_error_id { get; set; }
    }





}
