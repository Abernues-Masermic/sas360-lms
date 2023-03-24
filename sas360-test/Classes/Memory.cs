using System;
using System.ComponentModel;
using System.Security.Cryptography;

namespace sas360_test
{
    public class Internal_config_sas360
    {
        public string Serial_number { get; set; }

        public string ID_manufacturing { get; set; }

        public SAS360TAG_ZONE_TYPE Tag_type { get; set; }

        public string Version_hw { get; set; }

        public string Version_fw { get; set; }

        public string Version_boot { get; set; }

        public string RTU_slave_speed { get; set; }
        public ushort RTU_slave_num { get; set; }

        public string Lin_master_speed { get; set; }

        public string RTC_fw_update { get; set; }

        public string RTC_config_update { get; set; }

        public ushort CFG_change_counter { get; set; }

        public ushort CRC_config { get; set; }
    }


    public class Config_sas360con_general
    {
        public byte[] Array_lin_used { get; set; }
        public byte[] Array_lin_modbus_slave { get; set; }

        public ushort[] Array_actuaciones_salidas { get; set; }

        public string RTC_last_config_change { get; set; }

        public ushort Calc_config_change_counter { get; set; }

        public ushort CRC_config { get; set; }
    }

    public class Config_sas360con_vehicle_configuration
    {
        public int[] VehicleDim_xy { get; set; } //mm

        public ANTENNA_TYPE Antenna_type { get; set; }

        public int[,] Antenna_xy { get; set; } //mm

        public int[] Antena_angle { get; set; } //centdeg

        public ushort Num_closest_tags { get; set; }
    }

    public class Config_sas360con_installation_client
    {
        public ushort Client { get; set; }
        public ushort Installation { get; set; }
        public ushort Consola { get; set; }
        public ushort Audio { get; set; }
    }

    public class Config_sas360con_detection_area
    {
        public int Area_detection_radio { get; set; }

        public int Interior_distance_to_antennas { get; set; }


        public int[] Array_red_detection_distances { get; set; }

        public int[] Array_orange_detection_distances { get; set; }

        public int[] Array_yellow_detection_distances { get; set; }

        public int[] Array_hysteresys { get; set; }

    }




    public class Image_sas360con_general
    {
        public DateTime RTC_UTC { get; set; }
        public SAS360CON_STATE Sas360_state { get; set; }
        public SAS360CON_SUBSTATE Sas360_subtate { get; set; }

        public ushort EA_4v1_power { get; set; }
        public ushort EA_shunt_leds { get; set; }

        public ushort Autotest_bit_check_codif { get; set; }
        public ushort Autotest_ea_4v1_power_value_mv { get; set; }
        public ushort Reset_cause_codif { get; set; }

        public ushort[] Array_digital_states { get; set; }

        public ushort[] Array_processing_task_msec { get; set; }
    }

    public class Image_sas360con_forced_mask
    {
        public int Forced_mode { get; set; }
        public int Forced_mask_do1 { get; set; }
        public int Forced_mask_do2 { get; set; }
        public int Forced_mask_do3 { get; set; }
        public int Forced_mask_codif_led1 { get; set; }
        public int Forced_mask_codif_led2 { get; set; }
        public int Forced_mask_audio1 { get; set; }
        public int Forced_mask_audio2 { get; set; }
    }

    public class Image_sas360con_integrity_management
    {
        public ushort Lms_watchdog { get; set; }
        public ushort Internal_change_counter { get; set; }
        public ushort Config_con_change_counter { get; set; }
        public ushort Config_iot_change_counter { get; set; }
        public ushort Nvreg_change_counter { get; set; }

        public ushort Recorded_event_number { get; set; }

        public uint Recorded_historic_number { get; set; }

        public ushort Last_event_id { get; set; }
    }


    public class Image_sas360con_main_management
    {
        public ushort Internal_error { get; set; }
        public ushort Error_code_detail { get; set; }
        public ushort Management_codif { get; set; }
        public ushort[] Temp { get; set; }
    }

    public class Image_sas360con_lin_pooling
    {
        public ushort Lin_pooling_config_codif { get; set; }
        public ushort Lin_pooling_state { get; set; }
        public byte Actual_pooled_in { get; set; }
        public byte Actual_pooling_request { get; set; }

        public ushort[] Array_lin_com_total_counter { get; set; }
        public byte[] Array_lin_com_error_counter { get; set; }

        public byte[] Array_lin_total_cycle_time { get; set; }

        public ushort Total_lin_poolin_time { get; set; }

        public byte[] Array_assigned_self_contag_id { get; set; }
        public byte[] Array_assigned_self_drvtag_id { get; set; }
    }


    public class Image_sas360con_field_position
    {
        public uint Installation_pos_x { get; set; }
        public uint Installation_pos_y { get; set; }

        public double Latitud { get; set; }

        public double Longitud { get; set; }

        public uint Used_zone_id { get; set; }
    }

    public class Image_sas360con_uwb
    {
        public ushort[] UWB_codif_state { get; set; }
        public byte[] UWB_number_tags_detected { get; set; }
        public byte[] UWB_number_zones_detected { get; set; }
        public byte Total_tags_detected { get; set; }
        public byte Total_zones_detected { get; set; }

        public byte Total_tags_area_detection { get; set; }
        public byte Total_tags_area_yellow { get; set; }
        public byte Total_tags_area_orange { get; set; }
        public byte Total_tags_area_red { get; set; }
        public byte Total_tags_area_slow { get; set; }

        public byte Total_tags_area_charge { get; set; }
    }





    public class Sas360_tag : INotifyPropertyChanged
    {
        public int _index { get; set; }
        public int Index
        {
            get { return _index; }
            set { _index = value; OnPropertyChanged("Index"); }
        }


        //BASE
        public ushort ID_2LSB { get; set; }
        public byte Tag_type_value { get; set; }
        public SAS360TAG_ZONE_TYPE Tag_type { get; set; }

        public byte FW_version_value { get; set; }
        public string FW_version { get; set; }
        public ushort Codif_state { get; set; }

        public short Pos_x { get; set; }
        public short Pos_y { get; set; }
        public ushort UWB_command_codif { get; set; }
        public byte Battery_level { get; set; }
        public byte Console_code_alarm { get; set; }
        public ushort Reported_register { get; set; }
        public byte Calc_sector_leds { get; set; }
        public byte Calc_area { get; set; }


        //EXTENDED
        public ushort ID_bytes_1_2 { get; set; }
        public ushort ID_bytes_3_4 { get; set; }
        public ushort ID_bytes_5_6 { get; set; }
        public ushort ID_bytes_7_8 { get; set; }
        public ushort Distance { get; set; }
        public ushort PDOA_dec { get; set; }
        public ushort Time_of_flight_us { get; set; }

        public byte Last_charge_battery_level { get; set; }

        public byte Velocidad { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nameProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameProp));
        }
    }

    public class Sas360_zone : INotifyPropertyChanged
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
        public SAS360TAG_ZONE_TYPE Zone_type { get; set; }


        public byte FW_version_value { get; set; }
        public string FW_version { get; set; }

        public ushort Codif_state { get; set; }

        public short Pos_x { get; set; }
        public short Pos_y { get; set; }

        public ushort Radio_action { get; set; }
        public ushort Zone_cfg_pos_abs_x { get; set; }
        public ushort Zone_cfg_pos_abs_y { get; set; }


        //EXTENDED
        public ushort ID_bytes_3_4 { get; set; }
        public ushort ID_bytes_5_6 { get; set; }
        public ushort ID_bytes_7_8 { get; set; }
        public ushort Distance { get; set; }
        public ushort PDOA_dec { get; set; }
        public ushort Time_of_flight_us { get; set; }
        public short Latitude { get; set; }
        public short Longitude { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nameProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameProp));
        }
    }


    public class Sas360_uwb
    {
        public ushort ID { get; set; }
        public ushort Modbus_slave { get; set; }

        public ushort Lin_comm_total { get; set; }
        public byte Lin_error_total { get; set; }

        public byte Cycle_time { get; set; }

        public ushort Codif_state { get; set; }

        public byte Num_tags { get; set; }

        public byte Num_zones { get; set; }
    }



}
