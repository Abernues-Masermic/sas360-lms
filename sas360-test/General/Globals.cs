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

        public Manage_delegate Manage_delegate { get; set; }
        public Manage_comm_thread ManageComThread { get; set; }


        #region Memory classes

        public Internal_config_sas360 Internal_config_sas360 { get; set; }

        public Config_sas360con_general Config_sas360con_general { get; set; }

        public Config_sas360con_vehicle_configuration Config_sas360con_vehicle_configuration { get; set; }

        public Config_sas360con_installation_client Config_sas360con_installation_client { get; set; }

        public Config_sas360con_detection_area Config_sas360con_detection_area { get; set; }




        public Image_sas360con_general Image_sas360con_general { get; set; }

        public Image_sas360con_forced_mask Image_sas360con_forced_mask { get; set; }

        public Image_sas360con_integrity_management Image_sas360con_integrity_management { get; set; }

        public Image_sas360con_lin_pooling Image_sas360con_lin_pooling { get; set; }

        public Image_sas360con_field_position Image_sas360con_field_position{ get; set; }

        public Image_sas360con_main_management Image_sas360con_main_management { get; set; }

        public Image_sas360con_uwb Image_sas360con_uwb { get; set; }


        public Sas360_tag[] Array_sas360_tag { get; set; }

        public Sas360_zone[] Array_sas360_zone { get; set; }

        public Sas360_uwb[] Array_sas360_uwb { get; set; }

        #endregion


        #region Memory list

        public List<Modbus_var> List_internal_config { get; set; }
        public List<Modbus_var> List_config_sas360con { get; set; }
        public List<Modbus_var> List_config_iot { get; set; }
        public List<Modbus_var> List_image_sas360con { get; set; }
        public List<Modbus_var> List_image_iot { get; set; }

        public List<Modbus_var> List_console_closest_tags_base { get; set; }
        public List<Modbus_var> List_console_closest_tags_extended { get; set; }
        public List<Modbus_var> List_uwb_closest_tags_base { get; set; }
        public List<Modbus_var> List_uwb_closest_tags_extended { get; set; }

        public List<Modbus_var> List_console_closest_zone_base { get; set; }
        public List<Modbus_var> List_console_closest_zone_extended { get; set; }
    
        public List<Modbus_var> List_nvreg { get; set; }

        public List<Modbus_command> List_commands { get; set; }


        public List<int> List_event_log_read_reg { get; set; }
        public List<Event_log> List_event_log { get; set; }

        public List<int> List_hist_log_read_reg { get; set; }
        public List<Hist_log> List_hist_log { get; set; }

        #endregion


        public bool[] Forced_mode_do_controls { get; set; }


        #region Settings

        #region General

        public BIT_STATE Simulator_mode { get; set; }

        public int Panel_area_cm { get; set; }

        public int Grid_area_cm { get; set; }

        public int Total_closest_tags { get; set; }
        public int Total_closest_zone { get; set; }

        public string DateFormat { get; set; }
        public string DateProvider { get; set; }

        #endregion

        #region Modbus read- write

        public int Enable_read_memory_bits { get; set; }

        public ushort Code_init { get; set; }
        public ushort Code_prod { get; set; }
        public ushort Code_depu { get; set; }

        #endregion

        #region Paths

        public string Path_internal_config { get; set; }
        public string Path_config_sas360con { get; set; }
        public string Path_config_iot { get; set; }
        public string Path_image_sas360con { get; set; }
        public string Path_image_iot { get; set; }

        public string Path_console_closest_tags_base { get; set; }
        public string Path_console_closest_tags_extended { get; set; }
        public string Path_uwb_closest_tags_base { get; set; }
        public string Path_uwb_closest_tags_extended { get; set; }

        public string Path_console_closest_zone_base { get; set; }
        public string Path_console_closest_zone_extended { get; set; }

        public string Path_nvreg { get; set; }

        public string Path_commands { get; set; }

        #endregion

        #region Comunication

        public int Modbus_connection_timeout { get; set; }
        public int Read_memory_interval { get; set; }

        public string Comm_port { get; set; }
        public int Baud_rate { get; set; }
        public byte Unit_id { get; set; }

        #endregion

        #endregion

    }
}
