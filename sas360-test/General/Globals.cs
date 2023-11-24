using System;
using System.Collections.Generic;


namespace sas360_test
{
    internal class Globals
    {
        private static Globals? instance = null;
        public static Globals GetTheInstance()
        {
            instance ??= new Globals();

            return instance;
        }

        public Manage_delegate Manage_delegate { get; set; } = new Manage_delegate();
        public Manage_comm_thread ManageComThread { get; set; } = new Manage_comm_thread();


        #region Memory classes

        public SAS360CON_internal_cfg SAS360CON_internal_cfg { get; set; } = new SAS360CON_internal_cfg();

        #region SAS360CON cfg

        public SAS360CON_cfg_general SAS360CON_cfg_general { get; set; } = new SAS360CON_cfg_general();

        public SAS360CON_cfg_vehicle_cfg SAS360CON_cfg_vehicle_cfg { get; set; } = new SAS360CON_cfg_vehicle_cfg();

        public SAS360CON_cfg_installation_client SAS360CON_cfg_installation_client { get; set; } = new SAS360CON_cfg_installation_client();

        public SAS360CON_cfg_detection_area SAS360CON_cfg_detection_area { get; set; } = new SAS360CON_cfg_detection_area();

        public SAS360CON_cfg_recording SAS360CON_cfg_recording { get; set; } = new SAS360CON_cfg_recording();

        #endregion

        #region Image

        public SAS360CON_image_general SAS360CON_image_general { get; set; } = new SAS360CON_image_general();

        public SAS360CON_image_procesado_contadores SAS360CON_image_procesado_contadores { get; set; } = new SAS360CON_image_procesado_contadores();
        public SAS360CON_image_nvreg_management SAS360CON_image_nvreg_management { get; set; } = new SAS360CON_image_nvreg_management();

        public SAS360CON_image_main_management SAS360CON_image_main_management { get; set; } = new SAS360CON_image_main_management();

        public SAS360CON_image_lin_pooling SAS360CON_image_lin_pooling { get; set; } = new SAS360CON_image_lin_pooling();

        public SAS360CON_image_processed_tag SAS360CON_image_processed_tag { get; set; } = new SAS360CON_image_processed_tag();

        public SAS360CON_image_field_position SAS360CON_image_field_position { get; set; } = new SAS360CON_image_field_position();

        #endregion

        public SAS360CON_maintennance SAS360CON_maintennance { get; set; } = new SAS360CON_maintennance();

        public SAS360CON_TAG[] Array_SAS360CON_TAG { get; set; } = Array.Empty<SAS360CON_TAG>();

        public SAS360CON_ZONE[] Array_SAS360CON_ZONE { get; set; } = Array.Empty<SAS360CON_ZONE>();

        public SAS360CON_UWB[] Array_SAS360CON_UWB { get; set; } = Array.Empty<SAS360CON_UWB>();


        public SAS360CON_nvreg SAS360CON_nvreg { get; set; } = new SAS360CON_nvreg();

        #endregion


        #region Modbus array data union

        public ushort[] Array_data_closest_tag_base_1 { get; set; } = Array.Empty<ushort>();
        public ushort[] Array_data_closest_tag_base_2 { get; set; } = Array.Empty<ushort>();
        public ushort[] Array_data_closest_tag_base_3 { get; set; } = Array.Empty<ushort>();
        public ushort[] Array_data_closest_tag_base_all { get; set; } = Array.Empty<ushort>();

        public ushort[] Array_data_closest_zone_base_1 { get; set; } = Array.Empty<ushort>();
        public ushort[] Array_data_closest_zone_base_2 { get; set; } = Array.Empty<ushort>();
        public ushort[] Array_data_closest_zone_base_all { get; set; } = Array.Empty<ushort>();

        #endregion


        #region Memory list

        public List<Modbus_var> List_sas360con_internal_cfg { get; set; } = new List<Modbus_var>();
        
        public List<Modbus_var> List_sas360con_cfg { get; set; } = new List<Modbus_var>();
        public List<Modbus_var> List_sas360con_cfg_filter { get; set; } = new List<Modbus_var>();


        public List<Modbus_var> List_iot_cfg { get; set; } = new List<Modbus_var>();

        public List<Modbus_var> List_sas360con_image { get; set; } = new List<Modbus_var>();
        public List<Modbus_var> List_sas360con_image_filter { get; set; } = new List<Modbus_var>();

        public List<Modbus_var> List_iot_image { get; set; } = new List<Modbus_var>();

        public List<Modbus_var> List_sas360con_maintennance { get; set; } = new List<Modbus_var>();

        public List<Modbus_var> List_uwb_internal_cfg { get; set; } = new List<Modbus_var>();
        public List<Modbus_var> List_uwb_image { get; set; } = new List<Modbus_var>();

        public List<Modbus_var> List_console_closest_tags_base { get; set; } = new List<Modbus_var>();
        public List<Modbus_var> List_console_closest_tags_extended { get; set; } = new List<Modbus_var>();
        public List<Modbus_var> List_console_closest_zone_base { get; set; } = new List<Modbus_var>();
        public List<Modbus_var> List_console_closest_zone_extended { get; set; } = new List<Modbus_var>();

        public List<Modbus_var> List_uwb_closest_tags_base { get; set; } = new List<Modbus_var>();
        public List<Modbus_var> List_uwb_closest_tags_extended { get; set; } = new List<Modbus_var>();
        public List<Modbus_var> List_uwb_closest_zone_base { get; set; } = new List<Modbus_var>();
        public List<Modbus_var> List_uwb_closest_zone_extended { get; set; } = new List<Modbus_var>();


        public List<Modbus_var> List_sas360con_nvreg { get; set; } = new List<Modbus_var>();
        public List<Modbus_command> List_sas360con_commands { get; set; } = new List<Modbus_command>();

        public List<Event_log> List_sas360con_event_log { get; set; } = new List<Event_log>();

        public List<Hist_log> List_sas360con_hist_log { get; set; } = new List<Hist_log>();

        #endregion


        #region Memory list field type pos

        public List<int> List_sas360con_internal_cfg_byte_pos { get; set; } = new List<int>();
        public List<int> List_sas360con_internal_cfg_u32_pos { get; set; } = new List<int>();

        public List<int> List_sas360con_cfg_byte_pos { get; set; } = new List<int>();
        public List<int> List_sas360con_cfg_int16_pos { get; set; } = new List<int>();
        public List<int> List_sas360con_cfg_u32_pos { get; set; } = new List<int>();



        public List<int> List_sas360con_image_byte_pos { get; set; } = new List<int>();
        public List<int> List_sas360con_image_u32_pos { get; set; } = new List<int>();
        public List<int> List_sas360con_image_s32_pos { get; set; } = new List<int>();
        public List<int> List_sas360con_image_float_pos { get; set; } = new List<int>();


        public List<int> List_sas360con_maintennance_byte_pos { get; set; } = new List<int>();
        public List<int> List_sas360con_maintennance_u32_pos { get; set; } = new List<int>();


        public List<int> List_uwb_internal_cfg_u32_pos { get; set; } = new List<int>();


        public List<int> List_uwb_image_byte_pos { get; set; } = new List<int>();
        public List<int> List_uwb_image_u32_pos { get; set; } = new List<int>();


        public List<int> List_console_closest_tags_base_byte_pos { get; set; } = new List<int>();
        public List<int> List_console_closest_tags_base_int16_pos { get; set; } = new List<int>();
        public List<int> List_console_closest_tags_base_u32_pos { get; set; } = new List<int>();

        public List<int> List_console_closest_zone_base_byte_pos { get; set; } = new List<int>();
        public List<int> List_console_closest_zone_base_int16_pos { get; set; } = new List<int>();
        public List<int> List_console_closest_zone_base_u32_pos { get; set; } = new List<int>();


        public List<int> List_sas360con_nvreg_byte_pos { get; set; } = new List<int>();
        public List<int> List_sas360con_nvreg_u32_pos { get; set; } = new List<int>();

        #endregion


        #region Memory read state

        public bool SAS360con_internal_config_read { get; set; } = false;
        public bool SAS360iot_config_read { get; set; } = false;
        public bool SAS360con_config_read { get; set; } = false;
        public bool SAS360con_uwb_internal_config_read{ get; set; } = false;

        #endregion


        public bool[] Forced_mode_do_controls { get; set; } = Array.Empty<bool>();

        public List<int> List_last_command_send_data { get; set; } = new List<int>();
        public List<int> List_last_command_receive_data { get; set; } = new List<int>();


        #region Settings

        #region General

        public BIT_STATE Depur_mode { get; set; }
        public BIT_STATE Simulator_mode { get; set; }
        public BIT_STATE Draw_map { get; set; }

        public int Panel_area_cm { get; set; } = new int();

        public int Grid_area_cm { get; set; } = new int();

        public int Total_closest_tags { get; set; } = new int();
        public int Total_closest_zone { get; set; } = new int();

        public string DateFormat { get; set; } = string.Empty;
        public string DateProvider { get; set; } = string.Empty;

        #endregion

        #region Modbus read - write

        public int Enable_read_memory_bits { get; set; } = new int();

        public ushort[] Memory_map_ini { get; set; } = Array.Empty<ushort>();

        public ushort[] Memory_map_size { get; set; } = Array.Empty<ushort>();

        public int Code_ini { get; set; } = new short();
        public int Code_prod { get; set; } = new short();
        public int Code_depu { get; set; } = new short();

        #endregion

        #region Paths

        public string Path_sas360con_internal_cfg { get; set; } = string.Empty;
        public string Path_sas360con_cfg { get; set; } = string.Empty;
        public string Path_iot_cfg { get; set; } = string.Empty;
        public string Path_sas360con_image { get; set; } = string.Empty;
        public string Path_iot_image { get; set; } = string.Empty;
        public string Path_sas360con_maintennance { get; set; } = string.Empty;


        public string Path_uwb_internal_cfg { get; set; } = string.Empty;
        public string Path_uwb_image { get; set; } = string.Empty;

        public string Path_console_closest_tags_base { get; set; } = string.Empty;
        public string Path_console_closest_tags_extended { get; set; } = string.Empty;
        public string Path_console_closest_zone_base { get; set; } = string.Empty;
        public string Path_console_closest_zone_extended { get; set; } = string.Empty;


        public string Path_uwb_closest_tags_base { get; set; } = string.Empty;
        public string Path_uwb_closest_tags_extended { get; set; } = string.Empty;
        public string Path_uwb_closest_zone_base { get; set; } = string.Empty;
        public string Path_uwb_closest_zone_extended { get; set; } = string.Empty;


        public string Path_sas360con_nvreg { get; set; } = string.Empty;

        public string Path_sas360con_commands { get; set; } = string.Empty;

        #endregion

        #region Comunication

        public int Modbus_connection_timeout { get; set; } = new int();
        public int Read_memory_interval { get; set; } = new int();
        public int Read_log_interval { get; set; } = new int();
        public int Wait_send_write_interval { get; set; } = new int();
        public int Comm_timeout_interval { get; set; } = new int();

        public string Comm_port { get; set; } = string.Empty;
        public int Baud_rate { get; set; } = new int();
        public byte Unit_id { get; set; } = new byte();

        #endregion

        #endregion

    }
}
