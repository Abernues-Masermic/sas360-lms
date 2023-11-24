using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;
using Microsoft.Win32;
using CsvHelper;
using CsvHelper.Configuration;
using sas360_test.CustomControl;


namespace sas360_test
{

    public partial class MainWindow : Window
    {
        #region Array controles

        #region Main

        private List<Label> m_list_label_main_total_tags = new();
        private List<Label> m_list_label_main_total_ped = new();
        private List<Label> m_list_label_main_total_drv = new();
        private List<Label> m_list_label_main_total_lv = new();
        private List<Label> m_list_label_main_total_hv = new();
        private List<Label> m_list_label_main_total_zones = new();

        private List<Label> m_list_label_output_states = new();
        private List<Label> m_list_label_popup_image_info = new();

        private List<Label> m_list_label_uwb_management_lin_slave = new();
        private List<Label> m_list_label_uwb_management_pool_read = new();
        private List<Label> m_list_label_uwb_management_pool_write = new();
        private List<Label> m_list_label_uwb_management_com_total = new();
        private List<Label> m_list_label_uwb_management_com_error = new();
        private List<Label> m_list_label_uwb_management_cycle_time = new();

        private List<Label> m_list_label_uwb_name_value = new();
        private List<Label> m_list_label_uwb_image_rtc_value = new();
        private List<Label> m_list_label_uwb_image_rtc_milisecs = new();
        private List<Label> m_list_label_uwb_image_watchdog = new();
        private List<Label> m_list_label_uwb_image_codif_state = new();
        private List<Label> m_list_label_uwb_image_num_tags = new();
        private List<Label> m_list_label_uwb_image_num_zones = new();
        private List<Label> m_list_label_uwb_image_rreg_value = new();
        private List<Label> m_list_label_uwb_image_error_id = new();

        #endregion

        #region Map

        private List<Rectangle> m_array_rectangle_antenna = new();
        private List<Ellipse> m_list_ellipse_sas360tag = new();
        private List<Grid> m_list_grid_sas360zone = new();
        private List<Ellipse> m_list_ellipse_int_sas360zone = new();
        private List<Ellipse> m_list_ellipse_ext_sas360zone = new();
        private Border[] m_array_map_border_detection = new Border[4];

        #endregion

        #region SAS360CON config

        #region SAS360CON internal cfg

        private List<Label> m_list_label_edit_internal_config_title = new();
        private List<Button> m_list_button_edit_internal_config = new();
        private List<Label> m_list_label_edit_internal_config_value = new();

        #endregion

        #region SAS360CON cfg

        private List<TextBox> m_list_textbox_sas360con_cfg_installation = new();
        private List<TextBox> m_list_textbox_sas360con_cfg_vehicle = new();
        private List<TextBox> m_list_textbox_sas360con_cfg_detection_area = new();

        private List<TextBox> m_list_textbox_config_area_FRONT_ANRI = new();
        private List<TextBox> m_list_textbox_config_area_RIGHT_ANRI = new();
        private List<TextBox> m_list_textbox_config_area_BACK_ANRI = new();
        private List<TextBox> m_list_textbox_config_area_LEFT_ANRI = new();
        private List<TextBox> m_list_textbox_config_area_DIST_ANTENA_ANRI = new();

        private List<Border> m_list_border_sas360_config_rele1 = new();
        private List<Border> m_list_border_sas360_config_rele2 = new();
        private List<Border> m_list_border_sas360_config_rele3 = new();
        private List<Border> m_list_border_sas360_config_rele4 = new();
        private List<Border> m_list_border_sas360_config_trans1 = new();
        private List<Border> m_list_border_sas360_config_trans2 = new();

        private List<TextBox> m_list_textbox_sas360con_cfg_uwb_communication = new();
        private List<TextBox> m_list_textbox_sas360con_cfg_miscellaneous = new();

        private List<TextBox> m_list_textbox_sas360con_cfg_recording_index = new();
        private List<TextBox> m_list_textbox_sas360con_cfg_recording_unit = new();
        private List<TextBox> m_list_textbox_sas360con_cfg_recording_period = new();

        #endregion

        #endregion

        #region I/O maintenance visualization

        private List<Border> m_list_border_force_mode = new();
        private List<Image> m_list_image_force_mode = new();
        private List<Border> m_list_maintenance_border_di = new();
        private List<Border> m_list_maintenance_border_do_1 = new();
        private List<Border> m_list_maintenance_border_do_2 = new();
        private List<Border> m_list_maintenance_border_do_3 = new();
        private List<Border> m_list_maintenance_border_led = new();
        private List<Border> m_list_maintenance_border_audio = new();
        private List<Label> m_list_maintenance_label_di = new();
        private List<Label> m_list_maintenance_label_do_1 = new();
        private List<Label> m_list_maintenance_label_do_2 = new();
        private List<Label> m_list_maintenance_label_do_3 = new();
        private List<Label> m_list_maintenance_label_led = new();
        private List<Label> m_list_maintenance_label_audio = new();

        #endregion

        #region Commands

        private List<WrapPanel> m_list_wrappanel_general_commands = new();
        private List<Label> m_list_label_general_param_commands = new();
        private List<Xceed.Wpf.Toolkit.DecimalUpDown> m_list_decimalupdown_general_value_commands = new();
        private List<Label> m_list_label_general_type_commands = new();

        #endregion

        #region Memory

        private List<ListView> m_list_listview_memory = new();
        private List<Button> m_list_button_refresh_memory = new();
        private List<Button> m_list_button_new_memory = new();
        private List<Button> m_list_button_delete_memory = new();

        #endregion

        #region Select memory sections

        private List<ToggleButton> m_list_togglebutton_sas360con_config = new();
        private List<ToggleButton> m_list_togglebutton_sas360con_image = new();

        #endregion

        #endregion

        #region Collections

        private ObservableCollection<SAS360CON_UWB> m_collection_sas360_uwb = new ObservableCollection<SAS360CON_UWB>();

        private ObservableCollection<SAS360CON_TAG> m_collection_sas360_tag_processed;
        private ObservableCollection<SAS360CON_ZONE> m_collection_sas360_zone_processed;

        private ObservableCollection<Modbus_var> m_collection_sas360con_internal_cfg;
        private ObservableCollection<Modbus_var> m_collection_sas360con_cfg;
        private ObservableCollection<Modbus_var> m_collection_iot_cfg;
        private ObservableCollection<Modbus_var> m_collection_sas360con_image;
        private ObservableCollection<Modbus_var> m_collection_iot_image;
        private ObservableCollection<Modbus_var> m_collection_sas360con_maintennance;

        private ObservableCollection<Modbus_var> m_collection_uwb_internal_cfg;
        private ObservableCollection<Modbus_var> m_collection_uwb_image;

        private ObservableCollection<Modbus_var> m_collection_console_closest_tags_base;
        private ObservableCollection<Modbus_var> m_collection_console_closest_tags_extended;
        private ObservableCollection<Modbus_var> m_collection_uwb_closest_tags_base;
        private ObservableCollection<Modbus_var> m_collection_uwb_closest_tags_extended;

        private ObservableCollection<Modbus_var> m_collection_console_closest_zone_base;
        private ObservableCollection<Modbus_var> m_collection_console_closest_zone_extended;

        private ObservableCollection<Modbus_var> m_collection_sas360con_nvreg;

        private ObservableCollection<Modbus_command> m_collection_sas360con_commands;

        private ObservableCollection<Event_log> m_collection_sas360con_event_log;
        private ObservableCollection<Hist_log> m_collection_sas360con_hist_log;

        #endregion

        #region Timers

        private System.Timers.Timer m_timer_manage_memory;
        private System.Timers.Timer m_timer_read_last_received_command;
        private System.Timers.Timer m_timer_read_sas360con_event_log;
        private System.Timers.Timer m_timer_read_sas360con_hist_log;
        private System.Timers.Timer m_timer_write_all_sas360con_config;
        private System.Timers.Timer m_timer_write_struct_sas360con_config;

        private System.Timers.Timer m_timer_write_timeout;
        private System.Timers.Timer m_timer_sas360con_log_timeout;

        private System.Timers.Timer m_timer_wait_send_command;
        private System.Timers.Timer m_timer_wait_close;

        private bool m_received_resp;
        private int m_wait_ticks_read = 0;

        private bool m_read_write_enable = false;
        private MEMORY_READ_STATE m_memory_read_state = MEMORY_READ_STATE.SAS360CON_INTERNAL_CFG;

        #endregion

        private SAS360CON_TAG? m_selected_tag;
        private SAS360CON_ZONE? m_selected_zone;


        #region Write

        private Modbus_var? m_selected_modbus_var;
        private Modbus_command? m_selected_modbus_command;
        private COMMAND_WRITE_LOCATION m_selected_command_write_location;
        private COMMAND_WRITE_LOCATION m_write_all_config_state;
        private int m_command_watchdog = 0;

        private bool m_is_writing_all_config = false;
        private bool m_is_writing = false;
        private int[] m_array_send_values = Array.Empty<int>();

        #endregion


        #region SAS360 CON config edit backup values

        private List<string> m_list_value_sas360con_cfg_installation = new();
        private List<string> m_list_value_sas360con_cfg_vehicle = new();
        private List<string> m_list_value_sas360con_cfg_detection_area = new();
        private List<string> m_list_value_sas360con_cfg_output_leds = new();
        private List<string> m_list_value_sas360con_cfg_uwb_communication = new();
        private List<string> m_list_value_sas360con_cfg_recording_index = new();
        private List<string> m_list_value_sas360con_cfg_recording_unit = new();
        private List<string> m_list_value_sas360con_cfg_recording_period = new();
        private List<string> m_list_value_sas360con_cfg_miscellaneous = new();

        #endregion


        #region Maintenance event - hist log

        private MEMORY_CONFIG_TYPE m_memory_config_event_hist;
        private bool m_read_events_hist_param = false;
        private int m_empty_reg_event_hist = 0;

        private bool m_is_reading_event_hist = false;
        private ushort m_pos_modbus_read_event_hist;
        private uint m_num_abs_read_event_hist;
        private uint m_num_modbus_read_event_hist;

        private DateTime m_start_date_read_event_hist;
        private bool m_received_resp_event_hist_timeout;

        #endregion



        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(DomainHandler);

            Label_ver.Content = $"LOCAL MONITORING SOFTWARE v.{Constants.version}"; ;

            Globals.GetTheInstance().Manage_delegate = new Manage_delegate();
            Globals.GetTheInstance().Manage_delegate.RTU_handler_event += new Manage_delegate.RTU_handler(RTU_events_to_main);

            Globals.GetTheInstance().List_last_command_send_data = new();
            Globals.GetTheInstance().List_last_command_receive_data = new();

            Manage_file.Create_directories();
            Manage_file.Create_files();
            Manage_file.Load_app_setting();
            Manage_file.Load_comm_setting();

            #region Array controles

            #region Main

            m_list_label_output_states.Add(Label_main_input);
            m_list_label_output_states.Add(Label_main_output_int);
            m_list_label_output_states.Add(Label_main_output_ext);
            m_list_label_output_states.Add(Label_main_output_led);
            m_list_label_output_states.Add(Label_main_output_codif_led1);
            m_list_label_output_states.Add(Label_main_output_codif_led2);
            m_list_label_output_states.Add(Label_main_output_audio1);
            m_list_label_output_states.Add(Label_main_output_audio2);


            m_list_label_main_total_tags.Add(Label_total_tags_D);
            m_list_label_main_total_tags.Add(Label_total_tags_A);
            m_list_label_main_total_tags.Add(Label_total_tags_N);
            m_list_label_main_total_tags.Add(Label_total_tags_R);

            m_list_label_main_total_ped.Add(Label_total_ped_D);
            m_list_label_main_total_ped.Add(Label_total_ped_A);
            m_list_label_main_total_ped.Add(Label_total_ped_N);
            m_list_label_main_total_ped.Add(Label_total_ped_R);

            m_list_label_main_total_drv.Add(Label_total_drv_D);
            m_list_label_main_total_drv.Add(Label_total_drv_A);
            m_list_label_main_total_drv.Add(Label_total_drv_N);
            m_list_label_main_total_drv.Add(Label_total_drv_R);

            m_list_label_main_total_lv.Add(Label_total_lv_D);
            m_list_label_main_total_lv.Add(Label_total_lv_A);
            m_list_label_main_total_lv.Add(Label_total_lv_N);
            m_list_label_main_total_lv.Add(Label_total_lv_R);

            m_list_label_main_total_hv.Add(Label_total_hv_D);
            m_list_label_main_total_hv.Add(Label_total_hv_A);
            m_list_label_main_total_hv.Add(Label_total_hv_N);
            m_list_label_main_total_hv.Add(Label_total_hv_R);

            m_list_label_main_total_zones.Add(Label_total_zones_D);
            m_list_label_main_total_zones.Add(Label_total_zones_A);
            m_list_label_main_total_zones.Add(Label_total_zones_N);
            m_list_label_main_total_zones.Add(Label_total_zones_R);


            #region Individual UWB management

            m_list_label_uwb_management_lin_slave.Add(Label_uwb_management_lin_slave_value1);
            m_list_label_uwb_management_lin_slave.Add(Label_uwb_management_lin_slave_value2);
            m_list_label_uwb_management_lin_slave.Add(Label_uwb_management_lin_slave_value3);
            m_list_label_uwb_management_lin_slave.Add(Label_uwb_management_lin_slave_value4);

            m_list_label_uwb_management_pool_read.Add(Label_uwb_management_pool_read_value1);
            m_list_label_uwb_management_pool_read.Add(Label_uwb_management_pool_read_value2);
            m_list_label_uwb_management_pool_read.Add(Label_uwb_management_pool_read_value3);
            m_list_label_uwb_management_pool_read.Add(Label_uwb_management_pool_read_value4);

            m_list_label_uwb_management_pool_write.Add(Label_uwb_management_pool_write_value1);
            m_list_label_uwb_management_pool_write.Add(Label_uwb_management_pool_write_value2);
            m_list_label_uwb_management_pool_write.Add(Label_uwb_management_pool_write_value3);
            m_list_label_uwb_management_pool_write.Add(Label_uwb_management_pool_write_value4);


            m_list_label_uwb_management_com_total.Add(Label_uwb_management_com_total_value1);
            m_list_label_uwb_management_com_total.Add(Label_uwb_management_com_total_value2);
            m_list_label_uwb_management_com_total.Add(Label_uwb_management_com_total_value3);
            m_list_label_uwb_management_com_total.Add(Label_uwb_management_com_total_value4);

            m_list_label_uwb_management_com_error.Add(Label_uwb_management_com_error_value1);
            m_list_label_uwb_management_com_error.Add(Label_uwb_management_com_error_value2);
            m_list_label_uwb_management_com_error.Add(Label_uwb_management_com_error_value3);
            m_list_label_uwb_management_com_error.Add(Label_uwb_management_com_error_value4);

            m_list_label_uwb_management_cycle_time.Add(Label_uwb_management_cycle_time_value1);
            m_list_label_uwb_management_cycle_time.Add(Label_uwb_management_cycle_time_value2);
            m_list_label_uwb_management_cycle_time.Add(Label_uwb_management_cycle_time_value3);
            m_list_label_uwb_management_cycle_time.Add(Label_uwb_management_cycle_time_value4);

            #endregion

            #region Imagen UWB

            m_list_label_uwb_name_value.Add(Label_uwb_image_name_value1);
            m_list_label_uwb_name_value.Add(Label_uwb_image_name_value2);
            m_list_label_uwb_name_value.Add(Label_uwb_image_name_value3);
            m_list_label_uwb_name_value.Add(Label_uwb_image_name_value4);

            m_list_label_uwb_image_rtc_value.Add(Label_uwb_image_rtc_value1);
            m_list_label_uwb_image_rtc_value.Add(Label_uwb_image_rtc_value2);
            m_list_label_uwb_image_rtc_value.Add(Label_uwb_image_rtc_value3);
            m_list_label_uwb_image_rtc_value.Add(Label_uwb_image_rtc_value4);

            m_list_label_uwb_image_rtc_milisecs.Add(Label_uwb_image_milisecs_value1);
            m_list_label_uwb_image_rtc_milisecs.Add(Label_uwb_image_milisecs_value2);
            m_list_label_uwb_image_rtc_milisecs.Add(Label_uwb_image_milisecs_value3);
            m_list_label_uwb_image_rtc_milisecs.Add(Label_uwb_image_milisecs_value4);

            m_list_label_uwb_image_watchdog.Add(Label_uwb_image_watchdog_value1);
            m_list_label_uwb_image_watchdog.Add(Label_uwb_image_watchdog_value2);
            m_list_label_uwb_image_watchdog.Add(Label_uwb_image_watchdog_value3);
            m_list_label_uwb_image_watchdog.Add(Label_uwb_image_watchdog_value4);

            m_list_label_uwb_image_codif_state.Add(Label_uwb_image_codif_state_value1);
            m_list_label_uwb_image_codif_state.Add(Label_uwb_image_codif_state_value2);
            m_list_label_uwb_image_codif_state.Add(Label_uwb_image_codif_state_value3);
            m_list_label_uwb_image_codif_state.Add(Label_uwb_image_codif_state_value4);

            m_list_label_uwb_image_num_tags.Add(Label_uwb_image_num_tags_value1);
            m_list_label_uwb_image_num_tags.Add(Label_uwb_image_num_tags_value2);
            m_list_label_uwb_image_num_tags.Add(Label_uwb_image_num_tags_value3);
            m_list_label_uwb_image_num_tags.Add(Label_uwb_image_num_tags_value4);

            m_list_label_uwb_image_num_zones.Add(Label_uwb_image_num_zones_value1);
            m_list_label_uwb_image_num_zones.Add(Label_uwb_image_num_zones_value2);
            m_list_label_uwb_image_num_zones.Add(Label_uwb_image_num_zones_value3);
            m_list_label_uwb_image_num_zones.Add(Label_uwb_image_num_zones_value4);

            m_list_label_uwb_image_rreg_value.Add(Label_uwb_image_rreg_value1);
            m_list_label_uwb_image_rreg_value.Add(Label_uwb_image_rreg_value2);
            m_list_label_uwb_image_rreg_value.Add(Label_uwb_image_rreg_value3);
            m_list_label_uwb_image_rreg_value.Add(Label_uwb_image_rreg_value4);

            m_list_label_uwb_image_error_id.Add(Label_uwb_image_error_id_value1);
            m_list_label_uwb_image_error_id.Add(Label_uwb_image_error_id_value2);
            m_list_label_uwb_image_error_id.Add(Label_uwb_image_error_id_value3);
            m_list_label_uwb_image_error_id.Add(Label_uwb_image_error_id_value4);

            #endregion


            #endregion

            #region Map

            m_array_rectangle_antenna = new List<Rectangle>
            {
                Rectangle_antena1, Rectangle_antena2, Rectangle_antena3
            };
            m_array_rectangle_antenna.ForEach(rect => rect.Visibility = Visibility.Collapsed);

            Draw_tag_ellipses();
            Draw_zone_ellipses();

            #endregion

            #region Pop-up

            m_list_label_popup_image_info.Add(Label_main_input);
            m_list_label_popup_image_info.Add(Label_main_output_int);
            m_list_label_popup_image_info.Add(Label_main_output_ext);
            m_list_label_popup_image_info.Add(Label_main_output_led);
            m_list_label_popup_image_info.Add(Label_main_error_code);
            m_list_label_popup_image_info.Add(Label_main_pooling_state_uwb);

            #endregion

            #region SAS360 CONFIG

            #region Internal config

            m_list_label_edit_internal_config_title.Add(Label_config_serial_number_title);
            m_list_label_edit_internal_config_title.Add(Label_config_id_2lsb_title);
            m_list_label_edit_internal_config_title.Add(Label_config_tag_type_title);
            m_list_label_edit_internal_config_title.Add(Label_config_rtu_slave_speed_title);
            m_list_label_edit_internal_config_title.Add(Label_config_rtu_slave_num_title);
            m_list_label_edit_internal_config_title.Add(Label_config_modbus_lin_master_speed_title);

            m_list_label_edit_internal_config_value.Add(Label_config_serial_number_value);
            m_list_label_edit_internal_config_value.Add(Label_config_id_2lsb_value);
            m_list_label_edit_internal_config_value.Add(Label_config_tag_type_value);
            m_list_label_edit_internal_config_value.Add(Label_config_rtu_slave_speed_value);
            m_list_label_edit_internal_config_value.Add(Label_config_rtu_slave_num_value);
            m_list_label_edit_internal_config_value.Add(Label_config_modbus_lin_master_speed_value);

            m_list_button_edit_internal_config.Add(Button_config_edit_serial_number);
            m_list_button_edit_internal_config.Add(Button_config_edit_id_2lsb);
            m_list_button_edit_internal_config.Add(Button_config_edit_tag_type);
            m_list_button_edit_internal_config.Add(Button_config_edit_rtu_slave_speed);
            m_list_button_edit_internal_config.Add(Button_config_edit_rtu_slave_num);
            m_list_button_edit_internal_config.Add(Button_config_edit_modbus_lin_master_speed);

            m_list_button_edit_internal_config.ForEach(button => button.IsEnabled = false);

            #endregion

            #region Config sas360con

            #region Installation

            m_list_textbox_sas360con_cfg_installation.Add(Textbox_config_client_id);
            m_list_textbox_sas360con_cfg_installation.Add(Textbox_config_installation_id);
            m_list_textbox_sas360con_cfg_installation.Add(Textbox_config_vehicle_type_id);

            #endregion

            #region Vehicle config

            m_list_textbox_sas360con_cfg_vehicle.Add(Textbox_config_vehicle_dim_x);
            m_list_textbox_sas360con_cfg_vehicle.Add(Textbox_config_vehicle_dim_y);
            m_list_textbox_sas360con_cfg_vehicle.Add(Textbox_config_antena1_pos_x);
            m_list_textbox_sas360con_cfg_vehicle.Add(Textbox_config_antena1_pos_y);
            m_list_textbox_sas360con_cfg_vehicle.Add(Textbox_config_antena2_pos_x);
            m_list_textbox_sas360con_cfg_vehicle.Add(Textbox_config_antena2_pos_y);
            m_list_textbox_sas360con_cfg_vehicle.Add(Textbox_config_antena3_pos_x);
            m_list_textbox_sas360con_cfg_vehicle.Add(Textbox_config_antena3_pos_y);

            #endregion

            #region Detection area

            m_list_textbox_config_area_FRONT_ANRI.Add(Textbox_config_yellow_area_front);
            m_list_textbox_config_area_FRONT_ANRI.Add(Textbox_config_orange_area_front);
            m_list_textbox_config_area_FRONT_ANRI.Add(Textbox_config_red_area_front);
            m_list_textbox_config_area_FRONT_ANRI.Add(Textbox_config_int_area_front);

            m_list_textbox_config_area_RIGHT_ANRI.Add(Textbox_config_yellow_area_right);
            m_list_textbox_config_area_RIGHT_ANRI.Add(Textbox_config_orange_area_right);
            m_list_textbox_config_area_RIGHT_ANRI.Add(Textbox_config_red_area_right);
            m_list_textbox_config_area_RIGHT_ANRI.Add(Textbox_config_int_area_right);

            m_list_textbox_config_area_BACK_ANRI.Add(Textbox_config_yellow_area_back);
            m_list_textbox_config_area_BACK_ANRI.Add(Textbox_config_orange_area_back);
            m_list_textbox_config_area_BACK_ANRI.Add(Textbox_config_red_area_back);
            m_list_textbox_config_area_BACK_ANRI.Add(Textbox_config_int_area_back);

            m_list_textbox_config_area_LEFT_ANRI.Add(Textbox_config_yellow_area_left);
            m_list_textbox_config_area_LEFT_ANRI.Add(Textbox_config_orange_area_left);
            m_list_textbox_config_area_LEFT_ANRI.Add(Textbox_config_red_area_left);
            m_list_textbox_config_area_LEFT_ANRI.Add(Textbox_config_int_area_left);

            m_list_textbox_config_area_DIST_ANTENA_ANRI.Add(Textbox_config_yellow_area_dist_antena);
            m_list_textbox_config_area_DIST_ANTENA_ANRI.Add(Textbox_config_orange_area_dist_antena);
            m_list_textbox_config_area_DIST_ANTENA_ANRI.Add(Textbox_config_red_area_dist_antena);
            m_list_textbox_config_area_DIST_ANTENA_ANRI.Add(Textbox_config_int_area_dist_antena);

            for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
            {
                m_list_textbox_sas360con_cfg_detection_area.Add(m_list_textbox_config_area_FRONT_ANRI[index]);
            }
            for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
            {
                m_list_textbox_sas360con_cfg_detection_area.Add(m_list_textbox_config_area_RIGHT_ANRI[index]);
            }
            for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
            {
                m_list_textbox_sas360con_cfg_detection_area.Add(m_list_textbox_config_area_BACK_ANRI[index]);
            }
            for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
            {
                m_list_textbox_sas360con_cfg_detection_area.Add(m_list_textbox_config_area_LEFT_ANRI[index]);
            }

            m_list_textbox_sas360con_cfg_detection_area.Add(Textbox_config_area_detection_distance);
            m_list_textbox_sas360con_cfg_detection_area.Add(Textbox_config_area_change_hyst);
            m_list_textbox_sas360con_cfg_detection_area.Add(Textbox_config_sector_change_hyst);

            for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
            {
                m_list_textbox_sas360con_cfg_detection_area.Add(m_list_textbox_config_area_DIST_ANTENA_ANRI[index]);
            }

            #endregion

            #region UWB COM config

            m_list_textbox_sas360con_cfg_uwb_communication.Add(Textbox_uwb_com_config_id_lin1);
            m_list_textbox_sas360con_cfg_uwb_communication.Add(Textbox_uwb_com_config_id_lin2);
            m_list_textbox_sas360con_cfg_uwb_communication.Add(Textbox_uwb_com_config_id_lin3);
            m_list_textbox_sas360con_cfg_uwb_communication.Add(Textbox_uwb_com_config_id_lin4);

            m_list_textbox_sas360con_cfg_uwb_communication.Add(Textbox_uwb_com_config_slave_lin1);
            m_list_textbox_sas360con_cfg_uwb_communication.Add(Textbox_uwb_com_config_slave_lin2);
            m_list_textbox_sas360con_cfg_uwb_communication.Add(Textbox_uwb_com_config_slave_lin3);
            m_list_textbox_sas360con_cfg_uwb_communication.Add(Textbox_uwb_com_config_slave_lin4);

            #endregion

            #region MISCELLANEOUS

            m_list_textbox_sas360con_cfg_miscellaneous.Add(Textbox_miscellaneous_output_deactivation_delay);
            m_list_textbox_sas360con_cfg_miscellaneous.Add(Textbox_miscellaneous_area_zone_dist);
            m_list_textbox_sas360con_cfg_miscellaneous.Add(Textbox_miscellaneous_red_zone_alert_audio_repeat_sec);
            m_list_textbox_sas360con_cfg_miscellaneous.Add(Textbox_miscellaneous_tag_list_clear_timeout);

            #endregion

            #region RECORDING

            m_list_textbox_sas360con_cfg_recording_index.Add(Textbox_recording_config_index_1);
            m_list_textbox_sas360con_cfg_recording_index.Add(Textbox_recording_config_index_2);
            m_list_textbox_sas360con_cfg_recording_index.Add(Textbox_recording_config_index_3);
            m_list_textbox_sas360con_cfg_recording_index.Add(Textbox_recording_config_index_4);
            m_list_textbox_sas360con_cfg_recording_index.Add(Textbox_recording_config_index_5);
            m_list_textbox_sas360con_cfg_recording_index.Add(Textbox_recording_config_index_6);
            m_list_textbox_sas360con_cfg_recording_index.Add(Textbox_recording_config_index_7);
            m_list_textbox_sas360con_cfg_recording_index.Add(Textbox_recording_config_index_8);

            m_list_textbox_sas360con_cfg_recording_unit.Add(Textbox_recording_config_unit_codif_1);
            m_list_textbox_sas360con_cfg_recording_unit.Add(Textbox_recording_config_unit_codif_2);
            m_list_textbox_sas360con_cfg_recording_unit.Add(Textbox_recording_config_unit_codif_3);
            m_list_textbox_sas360con_cfg_recording_unit.Add(Textbox_recording_config_unit_codif_4);
            m_list_textbox_sas360con_cfg_recording_unit.Add(Textbox_recording_config_unit_codif_5);
            m_list_textbox_sas360con_cfg_recording_unit.Add(Textbox_recording_config_unit_codif_6);
            m_list_textbox_sas360con_cfg_recording_unit.Add(Textbox_recording_config_unit_codif_7);
            m_list_textbox_sas360con_cfg_recording_unit.Add(Textbox_recording_config_unit_codif_8);

            m_list_textbox_sas360con_cfg_recording_period.Add(Textbox_recording_config_period_1);
            m_list_textbox_sas360con_cfg_recording_period.Add(Textbox_recording_config_period_2);
            m_list_textbox_sas360con_cfg_recording_period.Add(Textbox_recording_config_period_3);
            m_list_textbox_sas360con_cfg_recording_period.Add(Textbox_recording_config_period_4);
            m_list_textbox_sas360con_cfg_recording_period.Add(Textbox_recording_config_period_5);
            m_list_textbox_sas360con_cfg_recording_period.Add(Textbox_recording_config_period_6);
            m_list_textbox_sas360con_cfg_recording_period.Add(Textbox_recording_config_period_7);
            m_list_textbox_sas360con_cfg_recording_period.Add(Textbox_recording_config_period_8);

            #endregion

            #region Buttons

            Button_send_SAS360CON_CFG_installation.Visibility = Visibility.Collapsed;
            Button_send_SAS360CON_CFG_vehicle_config.Visibility = Visibility.Collapsed;
            Button_send_SAS360CON_CFG_detection_area.Visibility = Visibility.Collapsed;
            Button_send_SAS360CON_CFG_output_actions.Visibility = Visibility.Collapsed;
            Button_send_SAS360CON_CFG_uwb_com_config.Visibility = Visibility.Collapsed;
            Button_send_SAS360CON_CFG_miscellaneous.Visibility = Visibility.Collapsed;
            Button_send_SAS360CON_CFG_recordings.Visibility = Visibility.Collapsed;

            #endregion

            #endregion

            #endregion

            #region I / O

            m_list_image_force_mode.Add(Image_edit_digital_outputs);
            m_list_image_force_mode.Add(Image_edit_leds);
            m_list_image_force_mode.Add(Image_edit_audio);
            m_list_image_force_mode.ForEach(image => image.Visibility = Visibility.Collapsed);

            #endregion

            #region Commands

            m_list_wrappanel_general_commands.Add(Wrappanel_param1);
            m_list_wrappanel_general_commands.Add(Wrappanel_param2);
            m_list_wrappanel_general_commands.Add(Wrappanel_param3);
            m_list_wrappanel_general_commands.Add(Wrappanel_param4);
            m_list_wrappanel_general_commands.Add(Wrappanel_param5);
            m_list_wrappanel_general_commands.Add(Wrappanel_param6);
            m_list_wrappanel_general_commands.Add(Wrappanel_param7);
            m_list_wrappanel_general_commands.Add(Wrappanel_param8);
            m_list_wrappanel_general_commands.Add(Wrappanel_param9);
            m_list_wrappanel_general_commands.Add(Wrappanel_param10);
            m_list_wrappanel_general_commands.ForEach(wrappanel => wrappanel.Visibility = Visibility.Collapsed);

            m_list_label_general_param_commands.Add(Label_param1);
            m_list_label_general_param_commands.Add(Label_param2);
            m_list_label_general_param_commands.Add(Label_param3);
            m_list_label_general_param_commands.Add(Label_param4);
            m_list_label_general_param_commands.Add(Label_param5);
            m_list_label_general_param_commands.Add(Label_param6);
            m_list_label_general_param_commands.Add(Label_param7);
            m_list_label_general_param_commands.Add(Label_param8);
            m_list_label_general_param_commands.Add(Label_param9);
            m_list_label_general_param_commands.Add(Label_param10);

            m_list_decimalupdown_general_value_commands.Add(DecimalUpDown_param1);
            m_list_decimalupdown_general_value_commands.Add(DecimalUpDown_param2);
            m_list_decimalupdown_general_value_commands.Add(DecimalUpDown_param3);
            m_list_decimalupdown_general_value_commands.Add(DecimalUpDown_param4);
            m_list_decimalupdown_general_value_commands.Add(DecimalUpDown_param5);
            m_list_decimalupdown_general_value_commands.Add(DecimalUpDown_param6);
            m_list_decimalupdown_general_value_commands.Add(DecimalUpDown_param7);
            m_list_decimalupdown_general_value_commands.Add(DecimalUpDown_param8);
            m_list_decimalupdown_general_value_commands.Add(DecimalUpDown_param9);
            m_list_decimalupdown_general_value_commands.Add(DecimalUpDown_param10);

            m_list_decimalupdown_general_value_commands.ForEach(decimalupdown =>
            {
                decimalupdown.Value = 0;
                decimalupdown.Minimum = 0;
                decimalupdown.Maximum = ushort.MaxValue;
                decimalupdown.Increment = 1;
            });

            m_list_label_general_type_commands.Add(Label_type1);
            m_list_label_general_type_commands.Add(Label_type2);
            m_list_label_general_type_commands.Add(Label_type3);
            m_list_label_general_type_commands.Add(Label_type4);
            m_list_label_general_type_commands.Add(Label_type5);
            m_list_label_general_type_commands.Add(Label_type6);
            m_list_label_general_type_commands.Add(Label_type7);
            m_list_label_general_type_commands.Add(Label_type8);
            m_list_label_general_type_commands.Add(Label_type9);
            m_list_label_general_type_commands.Add(Label_type10);

            #endregion

            #region Listview

            m_list_listview_memory.Add(Listview_sas360con_internal_cfg);
            m_list_listview_memory.Add(Listview_sas360con_cfg);
            m_list_listview_memory.Add(Listview_iot_cfg);
            m_list_listview_memory.Add(Listview_sas360con_image);
            m_list_listview_memory.Add(Listview_iot_image);

            m_list_listview_memory.Add(Listview_uwb_internal_cfg);
            m_list_listview_memory.Add(Listview_uwb_image);

            m_list_listview_memory.Add(Listview_console_closest_tags_base);
            m_list_listview_memory.Add(Listview_console_closest_tags_extended);
            m_list_listview_memory.Add(Listview_console_closest_zone_base);
            m_list_listview_memory.Add(Listview_console_closest_zone_extended);
            m_list_listview_memory.Add(Listview_uwb_closest_tags_base);
            m_list_listview_memory.Add(Listview_uwb_closest_tags_extended);

            m_list_listview_memory.Add(Listview_sas360con_nvreg);

            #endregion

            #region Buttons

            m_list_button_refresh_memory.Add(Button_refresh_internal_config);
            m_list_button_refresh_memory.Add(Button_refresh_sas360con_cfg);
            m_list_button_refresh_memory.Add(Button_refresh_iot_cfg);
            m_list_button_refresh_memory.Add(Button_refresh_sas360con_image);
            m_list_button_refresh_memory.Add(Button_refresh_iot_image);
            m_list_button_refresh_memory.Add(Button_refresh_con_closest_tag_base);
            m_list_button_refresh_memory.Add(Button_refresh_con_closest_tag_extended);
            m_list_button_refresh_memory.Add(Button_refresh_con_closest_zone_base);
            m_list_button_refresh_memory.Add(Button_refresh_con_closest_zone_extended);
            m_list_button_refresh_memory.Add(Button_refresh_uwb_closest_tag_base);
            m_list_button_refresh_memory.Add(Button_refresh_uwb_closest_tag_extended);

            m_list_button_refresh_memory.Add(Button_refresh_sas360con_nvreg);

            m_list_button_new_memory.Add(Button_new_sas360con_internal_cfg);
            m_list_button_new_memory.Add(Button_new_sas360con_cfg);
            m_list_button_new_memory.Add(Button_new_iot_cfg);
            m_list_button_new_memory.Add(Button_new_sas360con_image);
            m_list_button_new_memory.Add(Button_new_iot_image);
            m_list_button_new_memory.Add(Button_new_con_closest_tag_base);
            m_list_button_new_memory.Add(Button_new_con_closest_tag_extended);
            m_list_button_new_memory.Add(Button_new_con_closest_zone_base);
            m_list_button_new_memory.Add(Button_new_con_closest_zone_extended);
            m_list_button_new_memory.Add(Button_new_uwb_closest_tag_base);
            m_list_button_new_memory.Add(Button_new_uwb_closest_tag_extended);
            m_list_button_new_memory.Add(Button_new_sas360con_nvreg);

            m_list_button_delete_memory.Add(Button_delete_sas360con_internal_cfg);
            m_list_button_delete_memory.Add(Button_delete_sas360con_cfg);
            m_list_button_delete_memory.Add(Button_delete_iot_cfg);
            m_list_button_delete_memory.Add(Button_delete_sas360con_image);
            m_list_button_delete_memory.Add(Button_delete_iot_image);
            m_list_button_delete_memory.Add(Button_delete_con_closest_tag_base);
            m_list_button_delete_memory.Add(Button_delete_con_closest_tag_extended);
            m_list_button_delete_memory.Add(Button_delete_con_closest_zone_base);
            m_list_button_delete_memory.Add(Button_delete_con_closest_zone_extended);
            m_list_button_delete_memory.Add(Button_delete_uwb_closest_tag_base);
            m_list_button_delete_memory.Add(Button_delete_uwb_closest_tag_extended);
            m_list_button_delete_memory.Add(Button_delete_sas360con_nvreg);

            #endregion

            #region Select memory sections

            m_list_togglebutton_sas360con_config.Add(ToggleButton_sas360con_cfg_estructure);
            m_list_togglebutton_sas360con_config.Add(ToggleButton_sas360con_cfg_installation);
            m_list_togglebutton_sas360con_config.Add(ToggleButton_sas360con_cfg_vehicle_config);
            m_list_togglebutton_sas360con_config.Add(ToggleButton_sas360con_cfg_detection_area);
            m_list_togglebutton_sas360con_config.Add(ToggleButton_sas360con_cfg_entradas_salidas);
            m_list_togglebutton_sas360con_config.Add(ToggleButton_sas360con_cfg_temp_filtros);
            m_list_togglebutton_sas360con_config.Add(ToggleButton_sas360con_cfg_uwb_com);
            m_list_togglebutton_sas360con_config.Add(ToggleButton_sas360con_cfg_uwb_tag);
            m_list_togglebutton_sas360con_config.Add(ToggleButton_sas360con_cfg_recording);
            m_list_togglebutton_sas360con_config.Add(ToggleButton_sas360con_cfg_reserved);
            m_list_togglebutton_sas360con_config.Add(ToggleButton_sas360con_cfg_calculadas);
            m_list_togglebutton_sas360con_config.Add(ToggleButton_sas360con_cfg_all);

            m_list_togglebutton_sas360con_image.Add(ToggleButton_sas360con_image_estados_booleanos);
            m_list_togglebutton_sas360con_image.Add(ToggleButton_sas360con_image_ea);
            m_list_togglebutton_sas360con_image.Add(ToggleButton_sas360con_image_tiempo_procesado);
            m_list_togglebutton_sas360con_image.Add(ToggleButton_sas360con_image_nvreg);
            m_list_togglebutton_sas360con_image.Add(ToggleButton_sas360con_image_main);
            m_list_togglebutton_sas360con_image.Add(ToggleButton_sas360con_image_lin_polling);
            m_list_togglebutton_sas360con_image.Add(ToggleButton_sas360con_image_processed_tags);
            m_list_togglebutton_sas360con_image.Add(ToggleButton_sas360con_image_field_pos);
            m_list_togglebutton_sas360con_image.Add(ToggleButton_sas360con_image_all);

            #endregion

            #endregion

            #region DecimalUpDown

            DecimalUpDown_num_array_generate_csv.Value = 12;
            DecimalUpDown_num_array_generate_csv.Minimum = 0;
            DecimalUpDown_num_array_generate_csv.Maximum = 100;
            DecimalUpDown_num_array_generate_csv.Increment = 1;

            DecimalUpDown_num_uwb_generate_csv.Value = 0;
            DecimalUpDown_num_uwb_generate_csv.Minimum = 0;
            DecimalUpDown_num_uwb_generate_csv.Maximum = 8;
            DecimalUpDown_num_uwb_generate_csv.Increment = 1;

            DecimalUpDown_audio_language.Value = 0;
            DecimalUpDown_audio_language.Minimum = 0;
            DecimalUpDown_audio_language.Maximum = 1;

            DecimalUpDown_audio_volume.Value = 0;
            DecimalUpDown_audio_volume.Minimum = 0;
            DecimalUpDown_audio_volume.Maximum = 100;

            DecimalUpDown_config_trilat_calc_enabled.Value = 0;
            DecimalUpDown_config_trilat_calc_enabled.Minimum = 0;
            DecimalUpDown_config_trilat_calc_enabled.Maximum = 1;

            DecimalUpDown_config_gestion_avanzada_pos_enable.Value = 0;
            DecimalUpDown_config_gestion_avanzada_pos_enable.Minimum = 0;
            DecimalUpDown_config_gestion_avanzada_pos_enable.Maximum = 1;

            DecimalUpDown_index_con_tags.Value = 0;
            DecimalUpDown_index_con_tags.Minimum = 0;
            DecimalUpDown_index_con_tags.Maximum = Globals.GetTheInstance().Total_closest_tags;
            DecimalUpDown_index_con_tags.Increment = 1;

            DecimalUpDown_index_uwb_tags.Value = 0;
            DecimalUpDown_index_uwb_tags.Minimum = 0;
            DecimalUpDown_index_uwb_tags.Maximum = 4;
            DecimalUpDown_index_uwb_tags.Increment = 1;

            DecimalUpDown_reported_register.Value = 0;
            DecimalUpDown_reported_register.Minimum = 0;
            DecimalUpDown_reported_register.Maximum = ushort.MaxValue;
            DecimalUpDown_reported_register.Increment = 1;

            DecimalUpDown_sas360con_event_log_read_pos.Value = 0;
            DecimalUpDown_sas360con_event_log_read_pos.Minimum = 0;
            DecimalUpDown_sas360con_event_log_read_pos.Maximum = 9999999;
            DecimalUpDown_sas360con_event_log_read_pos.Increment = 4;

            DecimalUpDown_sas360con_event_log_num.Value = 0;
            DecimalUpDown_sas360con_event_log_num.Minimum = 0;
            DecimalUpDown_sas360con_event_log_num.Maximum = 9999999;
            DecimalUpDown_sas360con_event_log_num.Increment = 4;

            DecimalUpDown_sas360con_hist_log_num.Value = 0;
            DecimalUpDown_sas360con_hist_log_num.Minimum = 0;
            DecimalUpDown_sas360con_hist_log_num.Maximum = 9999999;

            #endregion

            #region Flash control led

            Grid_error_led_1.Visibility = Visibility.Collapsed;
            Grid_error_led_2.Visibility = Visibility.Collapsed;

            Storyboard? sb_1 = Grid_error_led_1.FindResource("Storyboard_flash_error_1") as Storyboard;
            sb_1!.Begin();
            Storyboard? sb_2 = Grid_error_led_2.FindResource("Storyboard_flash_error_2") as Storyboard;
            sb_2!.Begin();

            #endregion

            string s_panel_area = $"{decimal.Divide(Globals.GetTheInstance().Panel_area_cm, 100)}";
            Label_panel_area.Content = $"{s_panel_area} x {s_panel_area}";

            string s_grid_area = $"{decimal.Divide(Globals.GetTheInstance().Grid_area_cm, 100)}";
            Label_grid_area.Content = $"{s_grid_area} x {s_grid_area}";


            #region Timers

            m_timer_manage_memory = new System.Timers.Timer();
            m_timer_manage_memory.Elapsed += Timer_manage_memory_Tick!;
            m_timer_manage_memory.Interval = Globals.GetTheInstance().Read_memory_interval;
            m_timer_manage_memory.Start();

            m_timer_read_last_received_command = new System.Timers.Timer();
            m_timer_read_last_received_command.Elapsed += Timer_read_last_received_command_Tick!;
            m_timer_read_last_received_command.Interval = Globals.GetTheInstance().Wait_send_write_interval;
            m_timer_read_last_received_command.Stop();

            #region Log timers

            m_timer_read_sas360con_event_log = new System.Timers.Timer();
            m_timer_read_sas360con_event_log.Elapsed += Timer_read_sas360con_event_log_Tick!;
            m_timer_read_sas360con_event_log.Interval = Globals.GetTheInstance().Read_log_interval;
            m_timer_read_sas360con_event_log.Stop();

            m_timer_read_sas360con_hist_log = new System.Timers.Timer();
            m_timer_read_sas360con_hist_log.Elapsed += Timer_read_sas360con_hist_log_Tick!;
            m_timer_read_sas360con_hist_log.Interval = Globals.GetTheInstance().Read_log_interval;
            m_timer_read_sas360con_hist_log.Stop();

            m_timer_sas360con_log_timeout = new System.Timers.Timer();
            m_timer_sas360con_log_timeout.Elapsed += Timer_sas360con_log_timeout_Tick!;
            m_timer_sas360con_log_timeout.Interval = Globals.GetTheInstance().Comm_timeout_interval;
            m_timer_sas360con_log_timeout.Stop();

            #endregion

            #region Write timers

            m_timer_write_all_sas360con_config = new System.Timers.Timer();
            m_timer_write_all_sas360con_config.Elapsed += Timer_write_all_sas360con_config_Tick!;
            m_timer_write_all_sas360con_config.Interval = Globals.GetTheInstance().Wait_send_write_interval * 3;
            m_timer_write_all_sas360con_config.Stop();

            m_timer_write_struct_sas360con_config = new System.Timers.Timer();
            m_timer_write_struct_sas360con_config.Elapsed += Timer_write_struct_sas360con_config_Tick!;
            m_timer_write_struct_sas360con_config.Interval = Globals.GetTheInstance().Wait_send_write_interval;
            m_timer_write_struct_sas360con_config.Stop();

            m_timer_write_timeout = new System.Timers.Timer();
            m_timer_write_timeout.Elapsed += Timer_write_timeout_Tick!;
            m_timer_write_timeout.Interval = Globals.GetTheInstance().Comm_timeout_interval;
            m_timer_write_timeout.Stop();

            #endregion

            #region Wait timers

            m_timer_wait_send_command = new System.Timers.Timer();
            m_timer_wait_send_command.Elapsed += Timer_wait_send_command_Tick!;
            m_timer_wait_send_command.Interval = Globals.GetTheInstance().Wait_send_write_interval;
            m_timer_wait_send_command.Stop();

            m_timer_wait_close = new System.Timers.Timer();
            m_timer_wait_close.Elapsed += Timer_wait_close_Tick!;
            m_timer_wait_close.Interval = 500;
            m_timer_wait_close.Stop();

            #endregion

            #endregion


            #region Collections

            m_collection_sas360_tag_processed = new ObservableCollection<SAS360CON_TAG>();
            m_collection_sas360_zone_processed = new ObservableCollection<SAS360CON_ZONE>();


            m_collection_sas360con_internal_cfg = new ObservableCollection<Modbus_var>();
            m_collection_sas360con_cfg = new ObservableCollection<Modbus_var>();
            m_collection_iot_cfg = new ObservableCollection<Modbus_var>();
            m_collection_sas360con_image = new ObservableCollection<Modbus_var>();
            m_collection_iot_image = new ObservableCollection<Modbus_var>();
            m_collection_sas360con_maintennance = new ObservableCollection<Modbus_var>();

            m_collection_uwb_internal_cfg = new ObservableCollection<Modbus_var>();
            m_collection_uwb_image = new ObservableCollection<Modbus_var>();

            m_collection_console_closest_tags_base = new ObservableCollection<Modbus_var>();
            m_collection_console_closest_tags_extended = new ObservableCollection<Modbus_var>();
            m_collection_uwb_closest_tags_base = new ObservableCollection<Modbus_var>();
            m_collection_uwb_closest_tags_extended = new ObservableCollection<Modbus_var>();

            m_collection_console_closest_zone_base = new ObservableCollection<Modbus_var>();
            m_collection_console_closest_zone_extended = new ObservableCollection<Modbus_var>();

            m_collection_sas360con_nvreg = new ObservableCollection<Modbus_var>();

            m_collection_sas360con_commands = new ObservableCollection<Modbus_command>();

            m_collection_sas360con_event_log = new ObservableCollection<Event_log>();
            m_collection_sas360con_hist_log = new ObservableCollection<Hist_log>();

            #endregion


            #region Memory classes

            Globals.GetTheInstance().SAS360CON_internal_cfg = new();

            Globals.GetTheInstance().SAS360CON_cfg_general = new()
            {
                Array_lin_used = new byte[Constants.UWB_TOTAL_COUNT],
                Array_lin_modbus_slave = new byte[Constants.UWB_TOTAL_COUNT],
                Array_actuaciones_salidas = new ushort[Enum.GetNames(typeof(ACTUACIONES_SALIDAS_POS_IN_ARRAY)).Length]
            };
            Globals.GetTheInstance().SAS360CON_cfg_installation_client = new();
            Globals.GetTheInstance().SAS360CON_cfg_vehicle_cfg = new()
            {
                Vehicle_dim_xy_cm = new ushort[2],
                Antenna_xy_cm = new short[Constants.ANTENNA_COUNT, 2]
            };
            Globals.GetTheInstance().SAS360CON_cfg_detection_area = new()
            {
                Array_area_FRONT_ANRI_dist_cm = new ushort[Constants.DETECTION_AREA_COUNT],
                Array_area_RIGHT_ANRI_dist_cm = new ushort[Constants.DETECTION_AREA_COUNT],
                Array_area_BACK_ANRI_dist_cm = new ushort[Constants.DETECTION_AREA_COUNT],
                Array_area_LEFT_ANRI_dist_cm = new ushort[Constants.DETECTION_AREA_COUNT],
                Array_area_DIST_ANTENA_ANRI_dist_cm = new ushort[Constants.DETECTION_AREA_COUNT]
            };
            Globals.GetTheInstance().SAS360CON_cfg_recording = new()
            {
                Array_recorded_register_index = new byte[Constants.RECORDING_REG_SAS360CON_ARRAY],
                Array_recorded_register_unit_codif = new byte[Constants.RECORDING_REG_SAS360CON_ARRAY],
                Array_recorded_register_period_secs = new ushort[Constants.RECORDING_REG_SAS360CON_ARRAY]
            };

            Globals.GetTheInstance().Forced_mode_do_controls = new bool[Enum.GetNames<FORCE_MODE_CODIF>().Length];

            Globals.GetTheInstance().SAS360CON_image_general = new()
            {
                Array_codif_bits = new ushort[2],
                Array_codif_management = new ushort[2],
                Array_digital_states = new ushort[Enum.GetNames<DIGITAL_STATES_IN_LIST>().Length],
            };

            Globals.GetTheInstance().SAS360CON_image_nvreg_management = new();
            Globals.GetTheInstance().SAS360CON_image_main_management = new()
            {
                Last_event_log_value = new ushort[2]
            };
            Globals.GetTheInstance().SAS360CON_image_lin_pooling = new()
            {
                Array_lin_pooling_read_uwb = new ushort[Constants.UWB_TOTAL_COUNT],
                Array_lin_pooling_write_uwb = new ushort[Constants.UWB_TOTAL_COUNT],
                Array_lin_com_total_counter = new ushort[Constants.UWB_TOTAL_COUNT],
                Array_lin_com_error_counter = new ushort[Constants.UWB_TOTAL_COUNT],
                Array_lin_total_last_cycle_time = new ushort[Constants.UWB_TOTAL_COUNT],
            };
            Globals.GetTheInstance().SAS360CON_image_processed_tag = new()
            {
                Array_assigned_self_contag_id = new ushort[2],
                Array_assigned_self_drvtag_id = new ushort[2],
                Array_number_total_TAGS_in_area_DANR = new byte[Constants.DETECTION_AREA_COUNT],
                Array_number_total_PED_in_area_DANR = new byte[Constants.DETECTION_AREA_COUNT],
                Array_number_total_DRV_in_area_DANR = new byte[Constants.DETECTION_AREA_COUNT],
                Array_number_total_LV_in_area_DANR = new byte[Constants.DETECTION_AREA_COUNT],
                Array_number_total_HV_in_area_DANR = new byte[Constants.DETECTION_AREA_COUNT],
                Array_number_total_ZONES_in_area_DANR = new byte[Constants.DETECTION_AREA_COUNT],
            };
            Globals.GetTheInstance().SAS360CON_image_field_position = new();

            Globals.GetTheInstance().SAS360CON_maintennance = new()
            {
                Autotest_ea_consumo_grupo_LEDS_ma = new ushort[Constants.AUTOTEST_EA_LEDS_COUNT]
            };

            Globals.GetTheInstance().Array_SAS360CON_TAG = new SAS360CON_TAG[Globals.GetTheInstance().Total_closest_tags];
            for (int index = 0; index < Globals.GetTheInstance().Total_closest_tags; index++)
            {
                Globals.GetTheInstance().Array_SAS360CON_TAG.SetValue(new SAS360CON_TAG()
                {
                    Index = index,
                    Dist_from_antenna_cm = new ushort[Constants.UWB_TOTAL_COUNT],
                    Tag_time_last_success_decsec = new byte[Constants.UWB_TOTAL_COUNT]
                }, index);
            }

            Globals.GetTheInstance().Array_SAS360CON_ZONE = new SAS360CON_ZONE[Globals.GetTheInstance().Total_closest_zone];
            for (int index = 0; index < Globals.GetTheInstance().Total_closest_zone; index++)
            {
                Globals.GetTheInstance().Array_SAS360CON_ZONE.SetValue(new SAS360CON_ZONE()
                {
                    Index = index,
                    Dist_from_antenna_cm = new ushort[Constants.UWB_TOTAL_COUNT],
                }, index);
            }

            Globals.GetTheInstance().Array_SAS360CON_UWB = new SAS360CON_UWB[Constants.UWB_TOTAL_COUNT];
            for (int index = 0; index < Constants.UWB_TOTAL_COUNT; index++)
            {
                Globals.GetTheInstance().Array_SAS360CON_UWB.SetValue(new SAS360CON_UWB(), index);
            }

            Globals.GetTheInstance().SAS360CON_nvreg = new();

            #endregion

            Globals.GetTheInstance().ManageComThread = new()
            {
                Unit_id = Globals.GetTheInstance().Unit_id,
                Comm_port = Globals.GetTheInstance().Comm_port,
                Baud_rate = Globals.GetTheInstance().Baud_rate
            };
        }

        #endregion

        #region Loaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Draw_map_controls();
            Draw_sas360con_config_io_controls();
            Draw_sas360con_image_io_controls();

            Load_lists();
        }

        #endregion


        #region Domain handler

        static void DomainHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            Manage_logs.SaveErrorValue("MyHandler caught : " + e.Message);
            Manage_logs.SaveErrorValue($"Runtime terminating: {args.IsTerminating}");
        }

        #endregion


        #region Draw controls

        #region Draw map controls

        private void Draw_map_controls()
        {
            double rectangle_width = Rectangle_sas360_data_draw.ActualWidth;
            double rectangle_height = Rectangle_sas360_data_draw.ActualHeight;

            Image_vehicle.Visibility = Visibility.Hidden;

            #region DEFINE CENTER

            Ellipse ellipse_center = new();
            SolidColorBrush mySolidColorBrush = new()
            {
                Color = Colors.Red
            };
            ellipse_center.Fill = mySolidColorBrush;
            ellipse_center.StrokeThickness = 2;
            ellipse_center.Stroke = Brushes.Red;
            ellipse_center.Width = 5;
            ellipse_center.Height = 5;
            Canvas_sas360_data_draw.Children.Add(ellipse_center);
            Canvas.SetLeft(ellipse_center, rectangle_width / 2);
            Canvas.SetTop(ellipse_center, rectangle_height / 2);
            Canvas.SetZIndex(ellipse_center, 2000);

            #endregion

            #region SAS360TAG

            var stroke_brush = new SolidColorBrush(Color.FromArgb(255, 0x4b, 0x81, 0xa9));

            DropShadowEffect tag_zone_bitmap_effect = new()
            {
                Color = Colors.Black,
                Direction = -5,
                ShadowDepth = 1,
                BlurRadius = 0.1
            };

            m_list_ellipse_sas360tag.ForEach(ellipse_tag =>
            {
                ellipse_tag.Visibility = Visibility.Collapsed;
                ellipse_tag.StrokeThickness = 1;
                ellipse_tag.Stroke = stroke_brush;
                ellipse_tag.Width = 13;
                ellipse_tag.Height = 13;
                ellipse_tag.Cursor = Cursors.Hand;
                ellipse_tag.Effect = tag_zone_bitmap_effect;

                Panel.SetZIndex(ellipse_tag, 200);

                ColorAnimation? color_animation_stroke = ((Storyboard)Resources["TagZoneStrokeStoryBoard"]).Children[0] as ColorAnimation;
                color_animation_stroke?.SetValue(Storyboard.TargetNameProperty, ellipse_tag.Name);
                ((Storyboard)Resources["TagZoneStrokeStoryBoard"]).Begin();
            });

            #endregion

            #region SAS360ZONE

            m_list_grid_sas360zone.ForEach(grid_zone =>
            {
                Panel.SetZIndex(grid_zone, 200);
                grid_zone.Visibility = Visibility.Collapsed;
            });


            m_list_ellipse_int_sas360zone.ForEach(ellipse_int_zone =>
            {
                ellipse_int_zone.StrokeThickness = 1;
                ellipse_int_zone.Stroke = stroke_brush;
                ellipse_int_zone.Cursor = Cursors.Hand;
                ellipse_int_zone.Effect = tag_zone_bitmap_effect;

                ColorAnimation? color_animation_stroke = ((Storyboard)Resources["TagZoneStrokeStoryBoard"]).Children[0] as ColorAnimation;
                color_animation_stroke?.SetValue(Storyboard.TargetNameProperty, ellipse_int_zone.Name);
                ((Storyboard)Resources["TagZoneStrokeStoryBoard"]).Begin();
            });

            #endregion

            #region GRID LINE

            #region Y AXIS

            double line_y_distance = rectangle_height * (Globals.GetTheInstance().Grid_area_cm / (double)Globals.GetTheInstance().Panel_area_cm);
            double line_y_pos_up = (rectangle_height / 2);
            double line_y_pos_down = (rectangle_height / 2);

            while (line_y_pos_up + line_y_distance <= rectangle_height)
            {
                Draw_grid_line(0, rectangle_width, line_y_pos_up, line_y_pos_up);
                line_y_pos_up += line_y_distance;

                Draw_grid_line(0, rectangle_width, line_y_pos_down, line_y_pos_down);
                line_y_pos_down -= line_y_distance;
            }

            #endregion

            #region X AXIS

            double line_x_distance = rectangle_width * (Globals.GetTheInstance().Grid_area_cm / (double)Globals.GetTheInstance().Panel_area_cm);
            double line_x_pos_up = (rectangle_width / 2);
            double line_x_pos_down = (rectangle_height / 2);

            while (line_x_pos_up + line_x_distance <= rectangle_width)
            {
                Draw_grid_line(line_x_pos_up, line_x_pos_up, 0, rectangle_height);
                line_x_pos_up += line_x_distance;

                Draw_grid_line(line_x_pos_down, line_x_pos_down, 0, rectangle_height);
                line_x_pos_down -= line_x_distance;
            }

            #endregion

            #endregion
        }

        #endregion

        #region Draw grid line

        private void Draw_grid_line(double x1, double x2, double y1, double y2)
        {
            DoubleCollection double_collect = new()
            {
                2,
                4
            };

            Line line_up = new()
            {
                X1 = x1,
                X2 = x2,
                Y1 = y1,
                Y2 = y2,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Color.FromArgb(70, 0, 0, 0)),
                StrokeDashArray = double_collect,
            };
            Canvas_sas360_data_draw.Children.Add(line_up);
            Canvas.SetZIndex(line_up, 1000);
        }

        #endregion

        #region Draw sas360con config I/O controls

        private void Draw_sas360con_config_io_controls()
        {
            for (int index = 0; index < Constants.MAX_BITS_USHORT_VALUE; index++)
            {
                string s_content =
                    index == 0 ? "DET" :
                    index == 1 ? "AMA" :
                    index == 2 ? "NAR" :
                    index == 3 ? "ROJ" :
                    index == 8 ? "P/D" :
                    index == 9 ? "H/L" :
                    index == 10 ? "SC" :
                    index == 14 ? "SLOW" :
                    index == 15 ? "NO" : string.Empty;

                Label label_io = new()
                {
                    Style = this.FindResource("Label_standard") as Style,
                    Width = 30,
                    Height = 20,
                    Padding = new Thickness(0),
                    Content = s_content,
                    Foreground = new SolidColorBrush(Color.FromArgb(255, 59, 84, 101)),
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 0, 6, 0),
                };
                Wrappanel_output_actions_functionality.Children.Add(label_io);

                Border border_rele1 = new()
                {
                    Width = 30,
                    Height = 20,
                    BorderThickness = new Thickness(1, 1, 2, 2),
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    CornerRadius = new CornerRadius(3),
                    Background = new SolidColorBrush(Colors.White),
                    Margin = new Thickness(0, 0, 6, 0),
                };
                border_rele1.MouseDown -= new MouseButtonEventHandler(Border_rele1_MouseDown);

                m_list_border_sas360_config_rele1.Add(border_rele1);
                Wrappanel_output_actions_rele1.Children.Add(border_rele1);


                Border border_rele2 = new()
                {
                    Width = 30,
                    Height = 20,
                    BorderThickness = new Thickness(1, 1, 2, 2),
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    CornerRadius = new CornerRadius(3),
                    Background = new SolidColorBrush(Colors.White),
                    Margin = new Thickness(0, 0, 6, 0),
                };
                border_rele2.MouseDown -= new MouseButtonEventHandler(Border_rele2_MouseDown);

                m_list_border_sas360_config_rele2.Add(border_rele2);
                Wrappanel_output_actions_rele2.Children.Add(border_rele2);


                Border border_rele3 = new()
                {
                    Width = 30,
                    Height = 20,
                    BorderThickness = new Thickness(1, 1, 2, 2),
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    CornerRadius = new CornerRadius(3),
                    Background = new SolidColorBrush(Colors.White),
                    Margin = new Thickness(0, 0, 6, 0),
                };
                border_rele3.MouseDown -= new MouseButtonEventHandler(Border_rele3_MouseDown);

                m_list_border_sas360_config_rele3.Add(border_rele3);
                Wrappanel_output_actions_rele3.Children.Add(border_rele3);


                Border border_rele4 = new()
                {
                    Width = 30,
                    Height = 20,
                    BorderThickness = new Thickness(1, 1, 2, 2),
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    CornerRadius = new CornerRadius(3),
                    Background = new SolidColorBrush(Colors.White),
                    Margin = new Thickness(0, 0, 6, 0),
                };
                border_rele4.MouseDown -= new MouseButtonEventHandler(Border_rele4_MouseDown);

                m_list_border_sas360_config_rele4.Add(border_rele4);
                Wrappanel_output_actions_rele4.Children.Add(border_rele4);


                Border border_trans1 = new()
                {
                    Width = 30,
                    Height = 20,
                    BorderThickness = new Thickness(1, 1, 2, 2),
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    CornerRadius = new CornerRadius(3),
                    Background = new SolidColorBrush(Colors.White),
                    Margin = new Thickness(0, 0, 6, 0),
                };
                border_trans1.MouseDown -= new MouseButtonEventHandler(Border_trans1_MouseDown);

                m_list_border_sas360_config_trans1.Add(border_trans1);
                Wrappanel_output_actions_trans1.Children.Add(border_trans1);


                Border border_trans2 = new()
                {
                    Width = 30,
                    Height = 20,
                    BorderThickness = new Thickness(1, 1, 2, 2),
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    CornerRadius = new CornerRadius(3),
                    Background = new SolidColorBrush(Colors.White),
                    Margin = new Thickness(0, 0, 6, 0),
                };
                border_trans2.MouseDown -= new MouseButtonEventHandler(Border_trans2_MouseDown);

                m_list_border_sas360_config_trans2.Add(border_trans2);
                Wrappanel_output_actions_trans2.Children.Add(border_trans2);
            }
        }

        #endregion

        #region Draw sas360con image I/O controls

        private void Draw_sas360con_image_io_controls()
        {
            Enum.GetValues(typeof(FORCE_MODE_CODIF)).Cast<FORCE_MODE_CODIF>().ToList().ForEach(force_mode =>
            {
                string s_name =
                    force_mode == FORCE_MODE_CODIF.M_FORCE_DIGITAL_OUTPUTS ? "DIGITAL OUTPUTS" :
                    force_mode == FORCE_MODE_CODIF.M_FORCE_LEDS ? "LEDS" :
                    force_mode == FORCE_MODE_CODIF.M_AUDIO_TO_PLAY ? "AUDIO TO PLAY" : string.Empty;

                WrapPanel wrappanel_force_mode = new() { Orientation = Orientation.Horizontal, Margin = new Thickness(2), Height = 17 };
                Label label_force_mode = new()
                {
                    Style = this.FindResource("Label_standard_bold") as Style,
                    Content = s_name,
                    Margin = new Thickness(0, 0, 15, 0)
                };

                Border border_force_mode = new()
                {
                    Width = 30,
                    Height = 15,
                    BorderThickness = new Thickness(1, 1, 2, 2),
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    CornerRadius = new CornerRadius(3),
                    Background = new SolidColorBrush(Colors.White),
                    Margin = new Thickness(0, 0, 35, 0),
                    Cursor = Cursors.Hand
                };

                border_force_mode.MouseDown += new MouseButtonEventHandler(ForceMode_MouseDown);

                m_list_border_force_mode.Add(border_force_mode);

                wrappanel_force_mode.Children.Add(label_force_mode);
                wrappanel_force_mode.Children.Add(border_force_mode);
                Wrappanel_force_mode_general.Children.Add(wrappanel_force_mode);
            });

            Enum.GetValues(typeof(MASK_CODIF_DI1)).Cast<MASK_CODIF_DI1>().ToList().ForEach(di =>
            {
                string s_name =
                    di == MASK_CODIF_DI1.M_DI_DEBUG_SWITCH ? "DEBUG SWITCH" :
                    di == MASK_CODIF_DI1.M_DI_RESET_SWITCH ? "RESET SWITCH" :
                    di == MASK_CODIF_DI1.M_DI_MOD_POWER_SAVE ? "POWER SAVE" :
                    di == MASK_CODIF_DI1.M_DI_MOD_STATUS ? "STATUS MODE" :
                    di == MASK_CODIF_DI1.M_DI_MOD_RI ? "RI MODE" :
                    di == MASK_CODIF_DI1.M_DI_VER_HW_0 ? "VER HW 0" :
                    di == MASK_CODIF_DI1.M_DI_VER_HW_1 ? "VER HW 1" :
                    di == MASK_CODIF_DI1.M_DI_UWB_INT ? "UWT INT" :
                    di == MASK_CODIF_DI1.DI_ACCEL_INT1 ? "ACCEL INT 1" :
                    di == MASK_CODIF_DI1.DI_ACCEL_INT2 ? "ACCEL INT 2" : "RESERVED";

                WrapPanel wrap_panel_di = new() { Orientation = Orientation.Horizontal, Margin = new Thickness(2), Height = 17 };
                Label label_di = new()
                {
                    Style = this.FindResource("Label_standard_bold") as Style,
                    Width = 105,
                    Content = s_name
                };
                m_list_maintenance_label_di.Add(label_di);


                Border border_di = new()
                {
                    Width = 30,
                    Height = 15,
                    BorderThickness = new Thickness(1, 1, 2, 2),
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    CornerRadius = new CornerRadius(3),
                    Background = new SolidColorBrush(Colors.White)
                };

                m_list_maintenance_border_di.Add(border_di);

                wrap_panel_di.Children.Add(label_di);
                wrap_panel_di.Children.Add(border_di);
                Wrappanel_general_di.Children.Add(wrap_panel_di);

            });

            Enum.GetValues(typeof(FORCE_MASK_DO1)).Cast<FORCE_MASK_DO1>().ToList().ForEach(digital_output =>
            {
                string s_name =
                    digital_output == FORCE_MASK_DO1.M_DO_EN_REG_4V1 ? "REG 4V1" :
                    digital_output == FORCE_MASK_DO1.M_DO_EN_DF_SWITCH ? "DF SWITCH" :
                    digital_output == FORCE_MASK_DO1.M_DO_EN_CONNBOARD ? "CONNBOARD" :
                    digital_output == FORCE_MASK_DO1.M_DO_EN_LIN_1 ? "LIN1" :
                    digital_output == FORCE_MASK_DO1.M_DO_EN_LIN_2 ? "LIN2" :
                    digital_output == FORCE_MASK_DO1.M_DO_EN_LIN_3 ? "LIN3" :
                    digital_output == FORCE_MASK_DO1.M_DO_DEBUG_LED1 ? "DEBUG LED1" :
                    digital_output == FORCE_MASK_DO1.M_DO_EN_LED_DRIVER ? "LED DRIVER" :
                    digital_output == FORCE_MASK_DO1.M_DO_MOD_RESET ? "RESET" :
                    digital_output == FORCE_MASK_DO1.M_DO_MOD_POWER_KEY ? "POWER KEY" :
                    digital_output == FORCE_MASK_DO1.M_DO_MOD_DTR ? "DTR" :
                    digital_output == FORCE_MASK_DO1.M_DO_ACCEL_WAKE_UP ? "WAKE UP" :
                    string.Empty;

                WrapPanel wrap_panel_do = new() { Orientation = Orientation.Horizontal, Margin = new Thickness(2), Height = 17 };
                Label label_do = new()
                {
                    Style = this.FindResource("Label_standard_bold") as Style,
                    Width = 105,
                    Content = s_name
                };
                m_list_maintenance_label_do_1.Add(label_do);

                Border border_do = new()
                {
                    Width = 30,
                    Height = 15,
                    BorderThickness = new Thickness(1, 1, 2, 2),
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    CornerRadius = new CornerRadius(3),
                    Background = new SolidColorBrush(Colors.White),
                };
                border_do.MouseDown += new MouseButtonEventHandler(Digital_output_1_MouseDown);

                m_list_maintenance_border_do_1.Add(border_do);

                wrap_panel_do.Children.Add(label_do);
                wrap_panel_do.Children.Add(border_do);
                Wrappanel_general_do1.Children.Add(wrap_panel_do);

            });

            Enum.GetValues(typeof(FORCE_MASK_DO2)).Cast<FORCE_MASK_DO2>().ToList().ForEach(digital_output =>
            {
                string s_name =
                    digital_output == FORCE_MASK_DO2.M_DO_RELE_1 ? "RELE 1" :
                    digital_output == FORCE_MASK_DO2.M_DO_RELE_2 ? "RELE 2" :
                    digital_output == FORCE_MASK_DO2.M_DO_RELE_3 ? "RELE 3" :
                    digital_output == FORCE_MASK_DO2.M_DO_RELE_4 ? "RELE 4" :
                    digital_output == FORCE_MASK_DO2.M_DO_TRANSISTOR_1 ? "TRANSISTOR 1" :
                    digital_output == FORCE_MASK_DO2.M_DO_TRANSISTOR_2 ? "TRANSISTOR 2" :
                    digital_output == FORCE_MASK_DO2.M_DO_EN_12VOUT ? "12 VOUT" :
                    string.Empty;

                WrapPanel wrap_panel_do = new() { Orientation = Orientation.Horizontal, Margin = new Thickness(2), Height = 17 };
                Label label_do = new()
                {
                    Style = this.FindResource("Label_standard_bold") as Style,
                    Width = 105,
                    Content = s_name
                };
                m_list_maintenance_label_do_2.Add(label_do);

                Border border_do = new()
                {
                    Width = 30,
                    Height = 15,
                    BorderThickness = new Thickness(1, 1, 2, 2),
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    CornerRadius = new CornerRadius(3),
                    Background = new SolidColorBrush(Colors.White)
                };
                border_do.MouseDown += new MouseButtonEventHandler(Digital_output_2_MouseDown);

                m_list_maintenance_border_do_2.Add(border_do);

                wrap_panel_do.Children.Add(label_do);
                wrap_panel_do.Children.Add(border_do);
                Wrappanel_general_do2.Children.Add(wrap_panel_do);
            });

            Enum.GetValues(typeof(FORCE_MASK_DO3)).Cast<FORCE_MASK_DO3>().ToList().ForEach(digital_output =>
            {
                string s_name =
                    digital_output == FORCE_MASK_DO3.M_DO_LED_P2_A ? "LED P2 A" :
                    digital_output == FORCE_MASK_DO3.M_DO_LED_P7_A ? "LED P7 A" :
                    digital_output == FORCE_MASK_DO3.M_DO_LED_P8_A ? "LED P8 A" :
                    digital_output == FORCE_MASK_DO3.M_DO_LED_P9_A ? "LED P9 A" :
                    digital_output == FORCE_MASK_DO3.M_DO_LED_P10_A ? "LED P10 A" :
                    digital_output == FORCE_MASK_DO3.M_DO_LED_P11_A ? "LED P11 A" :
                    digital_output == FORCE_MASK_DO3.M_DO_LED_P1_K ? "LED P1 K" :
                    digital_output == FORCE_MASK_DO3.M_DO_LED_P3_K ? "LED P3 K" :
                    digital_output == FORCE_MASK_DO3.M_DO_LED_P4_K ? "LED P4 K" :
                    digital_output == FORCE_MASK_DO3.M_DO_LED_P5_K ? "LED P5 K" :
                    digital_output == FORCE_MASK_DO3.M_DO_LED_P6_K ? "LED P6 K" :
                    string.Empty;

                WrapPanel wrap_panel_do = new() { Orientation = Orientation.Horizontal, Margin = new Thickness(2), Height = 17 };
                Label label_do = new()
                {
                    Style = this.FindResource("Label_standard_bold") as Style,
                    Width = 105,
                    Content = s_name
                };
                m_list_maintenance_label_do_3.Add(label_do);

                Border border_do = new()
                {
                    Width = 30,
                    Height = 15,
                    BorderThickness = new Thickness(1, 1, 2, 2),
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    CornerRadius = new CornerRadius(3),
                    Background = new SolidColorBrush(Colors.White)
                };
                border_do.MouseDown += new MouseButtonEventHandler(Digital_output_3_MouseDown);

                m_list_maintenance_border_do_3.Add(border_do);

                wrap_panel_do.Children.Add(label_do);
                wrap_panel_do.Children.Add(border_do);
                Wrappanel_general_do3.Children.Add(wrap_panel_do);
            });

            const int max_audio = 32;

            List<string> list_led = new() {
                "MX LED 1", "MX LED 2", "MX LED 3", "MX LED 4", "MX LED 5", "MX LED 6", "MX LED 7", "MX LED 8", "MX LED 9", "MX LED 10", "MX LED 11", "MX LED 12", "MX LED 13",
                "MX LED 14", "MX LED 15", "MX LED 16", "MX LED 17", "MX LED 18", "MX LED 19", "MX LED 20", "MX LED 21", "MX LED 22", "MX LED 23", "MX LED 24", "MX LED 25",
                "MX LED 26", "MX LED 27", "RESERVA", "UWB GPIO0 LED3" };
            list_led
                .Select((Value, Index) => new { Value, Index }).ToList()
                .ForEach(led =>
            {
                WrapPanel wrap_panel_led = new() { Orientation = Orientation.Horizontal, Margin = new Thickness(2), Height = 17 };
                Label label_led = new()
                {
                    Style = this.FindResource("Label_standard_bold") as Style,
                    Width = 105,
                    Content = led.Value,
                };
                m_list_maintenance_label_led.Add(label_led);

                Border border_led = new()
                {
                    Width = 30,
                    Height = 15,
                    BorderThickness = new Thickness(1, 1, 2, 2),
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    CornerRadius = new CornerRadius(3),
                    Background = new SolidColorBrush(Colors.White),
                    Margin = new Thickness(0, 0, 35, 0)
                };
                border_led.MouseDown += new MouseButtonEventHandler(Led_MouseDown);

                m_list_maintenance_border_led.Add(border_led);

                wrap_panel_led.Children.Add(label_led);
                wrap_panel_led.Children.Add(border_led);

                if (led.Index < Constants.MAX_BITS_USHORT_VALUE)
                    Stackpanel_led_1.Children.Add(wrap_panel_led);
                else
                    Stackpanel_led_2.Children.Add(wrap_panel_led);
            });


            for (int index_audio = 0; index_audio < max_audio; index_audio++)
            {
                WrapPanel wrap_panel_audio = new() { Orientation = Orientation.Horizontal, Margin = new Thickness(2), Height = 17 };
                Label label_audio = new()
                {
                    Style = this.FindResource("Label_standard_bold") as Style,
                    Width = 105,
                    Content = $"AUDIO {index_audio:D2}",
                };
                m_list_maintenance_label_audio.Add(label_audio);

                Border border_audio = new()
                {
                    Width = 30,
                    Height = 15,
                    BorderThickness = new Thickness(1, 1, 2, 2),
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    CornerRadius = new CornerRadius(3),
                    Background = new SolidColorBrush(Colors.White),
                    Margin = new Thickness(0, 0, 35, 0)
                };
                border_audio.MouseDown += new MouseButtonEventHandler(Audio_MouseDown);

                m_list_maintenance_border_audio.Add(border_audio);

                wrap_panel_audio.Children.Add(label_audio);
                wrap_panel_audio.Children.Add(border_audio);

                if (index_audio < Constants.MAX_BITS_USHORT_VALUE)
                    Stackpanel_audio_1.Children.Add(wrap_panel_audio);
                else
                    Stackpanel_audio_2.Children.Add(wrap_panel_audio);

            }
        }

        #endregion

        #endregion


        #region Mover pantalla

        private void Title_MouseDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (e.ButtonState == MouseButtonState.Pressed)
                DragMove();
        }

        #endregion

        #region Image logo -> about 
        private void Image_logo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AboutWindow about = new AboutWindow();
            about.ShowDialog();
        }

        #endregion

        #region Setting app

        private void Button_setting_app_Click(object sender, RoutedEventArgs e)
        {
            if (Globals.GetTheInstance().ManageComThread.Is_connected)
                Globals.GetTheInstance().ManageComThread.Disconnect();

            SettingAppWindow setting_app = new()
            {
                Left = this.Left,
                Top = this.Top,
            };
            setting_app.ShowDialog();

            //General
            string s_panel_area = ((double)Globals.GetTheInstance().Panel_area_cm / 100).ToString();
            Label_panel_area.Content = $"{s_panel_area} M x {s_panel_area} M";

            string s_grid_area = ((double)Globals.GetTheInstance().Grid_area_cm / 100).ToString();
            Label_grid_area.Content = $"{s_grid_area} M x {s_grid_area} M";

            DecimalUpDown_index_con_tags.Maximum = Globals.GetTheInstance().Total_closest_tags;

            Globals.GetTheInstance().Array_SAS360CON_TAG = new SAS360CON_TAG[Globals.GetTheInstance().Total_closest_tags];
            for (int index = 0; index < Globals.GetTheInstance().Total_closest_tags; index++)
            {
                Globals.GetTheInstance().Array_SAS360CON_TAG.SetValue(new SAS360CON_TAG()
                {
                    Index = index,
                    Dist_from_antenna_cm = new ushort[Constants.UWB_TOTAL_COUNT],
                    Tag_time_last_success_decsec = new byte[Constants.UWB_TOTAL_COUNT]
                }, index);
            }


            Globals.GetTheInstance().Array_SAS360CON_ZONE = new SAS360CON_ZONE[Globals.GetTheInstance().Total_closest_zone];
            for (int index = 0; index < Globals.GetTheInstance().Total_closest_zone; index++)
            {
                Globals.GetTheInstance().Array_SAS360CON_ZONE.SetValue(new SAS360CON_ZONE()
                {
                    Index = index,
                    Dist_from_antenna_cm = new ushort[Constants.UWB_TOTAL_COUNT],
                }, index);
            }


            //Communication
            Globals.GetTheInstance().ManageComThread.Unit_id = Globals.GetTheInstance().Unit_id;
            Globals.GetTheInstance().ManageComThread.Comm_port = Globals.GetTheInstance().Comm_port;
            Globals.GetTheInstance().ManageComThread.Baud_rate = Globals.GetTheInstance().Baud_rate;
            m_timer_manage_memory.Interval = Globals.GetTheInstance().Read_memory_interval;
            m_timer_read_sas360con_event_log.Interval = Globals.GetTheInstance().Read_log_interval;
            m_timer_read_sas360con_hist_log.Interval = Globals.GetTheInstance().Read_log_interval;
        }

        #endregion



        #region Modbus events to main

        public void RTU_events_to_main(object sender, RTU_handler_args args)
        {
            switch (args.RTU_action)
            {
                case RTU_ACTION.CONNECT:
                    {
                        Globals.GetTheInstance().ManageComThread.Is_connected = true;

                        Dispatcher.Invoke(() =>
                        {
                            m_read_write_enable = true;
                            m_received_resp = true;
                            m_wait_ticks_read = 0;

                            Globals.GetTheInstance().SAS360con_internal_config_read = false;
                            Globals.GetTheInstance().SAS360iot_config_read = false;
                            Globals.GetTheInstance().SAS360con_config_read = false;
                            Globals.GetTheInstance().SAS360con_uwb_internal_config_read = false;

                            m_memory_read_state = MEMORY_READ_STATE.SAS360CON_INTERNAL_CFG;

                            ((Storyboard)Resources["BlinkConnectStoryboard"]).Begin();
                            Image_stop.Visibility = Visibility.Collapsed;
                            Image_start.Visibility = Visibility.Visible;

                            m_list_button_edit_internal_config.ForEach(button => button.IsEnabled = true);

                            Checkbox_edit_SAS360CON_CFG_installation.IsEnabled = true;
                            Checkbox_edit_SAS360CON_CFG_vehicle_config.IsEnabled = true;
                            Checkbox_edit_SAS360CON_CFG_detection_area.IsEnabled = true;
                            Checkbox_edit_SAS360CON_CFG_output_actions.IsEnabled = true;
                            Checkbox_edit_SAS360CON_CFG_uwb_com_config.IsEnabled = true;
                            Checkbox_edit_SAS360CON_CFG_miscellaneous.IsEnabled = true;
                            Checkbox_edit_SAS360CON_CFG_recordings.IsEnabled = true;

                            Button_send_config_tab_sas360con.IsEnabled = true;
                        });

                        break;
                    }

                case RTU_ACTION.ERROR_CONNECT:
                    {
                        Dispatcher.Invoke(() => Image_stop.Visibility = Visibility.Visible);
                        Dispatcher.Invoke(() => Image_start.Visibility = Visibility.Collapsed);
                        Dispatcher.Invoke(() => Image_connection_warning.Visibility = Visibility.Visible);

                        break;
                    }

                case RTU_ACTION.DISCONNECT:
                    {
                        m_read_events_hist_param = false;
                        m_read_write_enable = false;
                        Globals.GetTheInstance().ManageComThread.Is_connected = false;

                        Dispatcher.Invoke(() => ((Storyboard)Resources["BlinkConnectStoryboard"]).Remove());
                        Dispatcher.Invoke(() => Image_stop.Visibility = Visibility.Visible);
                        Dispatcher.Invoke(() => Image_start.Visibility = Visibility.Collapsed);
                        Dispatcher.Invoke(() => Disconnect_controls());

                        break;
                    }

                case RTU_ACTION.DISCONNECT_FROM_READ_WRITE:
                    {
                        m_timer_wait_close.Start();
                        m_read_write_enable = false;

                        Dispatcher.Invoke(() => Grid_start_stop.IsEnabled = false);
                        Dispatcher.Invoke(() => ((Storyboard)Resources["BlinkConnectStoryboard"]).Remove());
                        Dispatcher.Invoke(() => Ellipse_blink.Fill = new SolidColorBrush(Colors.Red));
                        Dispatcher.Invoke(() => Image_stop.Visibility = Visibility.Visible);
                        Dispatcher.Invoke(() => Image_start.Visibility = Visibility.Collapsed);
                        Dispatcher.Invoke(() => Disconnect_controls());

                        break;
                    }

                case RTU_ACTION.READ:
                    {
                        m_received_resp = true;
                        Manage_logs.SaveModbusValue($"RECEIVED RESPONSE -> {m_received_resp}");

                        Manage_memory.Load_modbus_data(args.List_data, args.Memory_config_type);
                        Dispatcher.Invoke(() =>
                        {
                            bool update_ok =
                                args.Memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_INTERNAL_CFG ? Update_SAS360CON_internal_cfg_memory_info() :
                                args.Memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_CFG ? Update_SAS360CON_cfg_memory_info() :
                                args.Memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_IMAGE ? Update_SAS360CON_image_memory_info() :
                                args.Memory_config_type == MEMORY_CONFIG_TYPE.UWB_INTERNAL_CFG ? Update_UWB_internal_cfg_memory_info() :
                                args.Memory_config_type == MEMORY_CONFIG_TYPE.UWB_IMAGE ? Update_UWB_image_memory_info() :
                                args.Memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_MAINTENNANCE ? Update_SAS360CON_maintennance_memory_info() :
                                args.Memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE_3 ? Update_CONSOLE_closest_tags_base_memory_info() :
                                args.Memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_BASE_2 ? Update_CONSOLE_closest_zone_base_memory_info() :
                                args.Memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_NVREG ? Update_SAS360CON_nvreg_memory_info() :
                                args.Memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_COMMANDS ? Update_SAS360CON_commands_info(args.List_data) :
                                args.Memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_EVENT_LOG ? Update_SAS360CON_event_log_memory_info(args.List_data) :
                                args.Memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_HIST_LOG ? Update_SAS360CON_hist_log_memory_info(args.List_data) : true;
                        });

                        break;
                    }

                case RTU_ACTION.WRITE:
                    {
                        if (args.Memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_COMMANDS || args.Memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_CFG)
                        {
                            Manage_logs.SaveLogValue($"WRITE COMMAND OK -> {m_selected_command_write_location}");

                            bool is_command = args.Memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_COMMANDS;
                            bool refresh_config = args.Memory_config_type != MEMORY_CONFIG_TYPE.SAS360CON_COMMANDS;

                            switch (m_selected_command_write_location)
                            {
                                case COMMAND_WRITE_LOCATION.MAINTENNANCE:
                                    {
                                        Dispatcher.Invoke(() => Image_send_general_command_ok.Visibility = Visibility.Visible);

                                        break;
                                    }

                                case COMMAND_WRITE_LOCATION.RTC_UTC:
                                    {

                                        break;
                                    }

                                case COMMAND_WRITE_LOCATION.REPORTED_REGISTER:
                                    {

                                        break;
                                    }

                                case COMMAND_WRITE_LOCATION.INTERNAL_CONFIG:
                                    {
                                        break;
                                    }

                                case COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_INSTALLATION:
                                    {
                                        Dispatcher.Invoke(() =>
                                        {
                                            Uncheck_SAS360CON_CFG_installation_controls();
                                            Image_SAS360CON_CFG_installation_ok.Visibility = Visibility.Visible;
                                        });

                                        break;
                                    }

                                case COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_VEHICLE_CONFIG:
                                    {
                                        Dispatcher.Invoke(() =>
                                        {
                                            Uncheck_SAS360CON_CFG_vehicle_controls();
                                            Image_SAS360CON_CFG_vehicle_config_ok.Visibility = Visibility.Visible;

                                        });

                                        break;
                                    }

                                case COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_DETECTION_AREA:
                                    {
                                        Dispatcher.Invoke(() =>
                                        {
                                            Uncheck_SAS360CON_CFG_detection_area();
                                            Image_SAS360CON_CFG_detection_area_ok.Visibility = Visibility.Visible;
                                        });

                                        break;
                                    }

                                case COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_UWB_COMM:
                                    {
                                        Dispatcher.Invoke(() =>
                                        {
                                            Uncheck_SAS360CON_CFG_uwb_com_config();
                                            Image_SAS360CON_CFG_uwb_com_config_ok.Visibility = Visibility.Visible;
                                        });

                                        break;
                                    }

                                case COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_MISCELLANEOUS:
                                    {
                                        Dispatcher.Invoke(() =>
                                        {
                                            Uncheck_SAS360CON_CFG_miscellaneous();
                                            Image_SAS360CON_CFG_miscellaneous_ok.Visibility = Visibility.Visible;
                                        });

                                        break;
                                    }

                                default:
                                    {
                                        refresh_config = false;
                                        break;
                                    }
                            }

                            if (is_command)
                                m_timer_read_last_received_command.Start();

                            if (refresh_config)
                            {
                                m_is_writing = false;
                                m_timer_write_timeout.Stop();

                                Globals.GetTheInstance().SAS360con_internal_config_read = false;
                                Globals.GetTheInstance().SAS360iot_config_read = false;
                                Globals.GetTheInstance().SAS360con_config_read = false;
                                Globals.GetTheInstance().SAS360con_uwb_internal_config_read = false;
                            }
                        }

                        break;
                    }
            }
        }

        #endregion


        #region Tab menu selection changed
        private void Tab_menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabControl_menu.SelectedIndex == (int)MAIN_MENU_TAB.CONFIG)
            {
            }
        }

        #endregion


        #region Start / stop

        private void Image_sfront_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image_connection_warning.Visibility = Visibility.Collapsed;
            Globals.GetTheInstance().ManageComThread.Connect();
        }

        private void Image_start_MouseDown(object sender, MouseButtonEventArgs e)
        {
            m_timer_wait_close.Start();
            Grid_start_stop.IsEnabled = false;
            m_read_write_enable = false;
        }

        private void Timer_wait_close_Tick(object sender, EventArgs e)
        {
            m_timer_wait_close.Stop();
            Dispatcher.Invoke(() => Grid_start_stop.IsEnabled = true);
            Globals.GetTheInstance().ManageComThread.Disconnect();
        }

        #endregion


        #region Disconnect controls

        private void Disconnect_controls()
        {
            m_list_ellipse_sas360tag.ForEach(sas360tag => sas360tag.Visibility = Visibility.Collapsed);
            m_list_grid_sas360zone.ForEach(sas360zone => sas360zone.Visibility = Visibility.Collapsed);

            m_list_button_edit_internal_config.ForEach(button => button.IsEnabled = false);

            Checkbox_edit_SAS360CON_CFG_installation.IsEnabled = false;
            Checkbox_edit_SAS360CON_CFG_vehicle_config.IsEnabled = false;
            Checkbox_edit_SAS360CON_CFG_detection_area.IsEnabled = false;
            Checkbox_edit_SAS360CON_CFG_output_actions.IsEnabled = false;
            Checkbox_edit_SAS360CON_CFG_uwb_com_config.IsEnabled = false;
            Checkbox_edit_SAS360CON_CFG_miscellaneous.IsEnabled = false;
            Checkbox_edit_SAS360CON_CFG_recordings.IsEnabled = false;

            Checkbox_edit_SAS360CON_CFG_installation.IsChecked = false;
            Checkbox_edit_SAS360CON_CFG_vehicle_config.IsChecked = false;
            Checkbox_edit_SAS360CON_CFG_detection_area.IsChecked = false;
            Checkbox_edit_SAS360CON_CFG_output_actions.IsChecked = false;
            Checkbox_edit_SAS360CON_CFG_uwb_com_config.IsChecked = false;
            Checkbox_edit_SAS360CON_CFG_miscellaneous.IsChecked = false;
            Checkbox_edit_SAS360CON_CFG_recordings.IsChecked = false;

            Button_send_config_tab_sas360con.IsEnabled = false;

            m_list_maintenance_border_di.ForEach(border => border.Background = new SolidColorBrush(Colors.White));
            m_list_maintenance_border_do_1.ForEach(border => border.Background = new SolidColorBrush(Colors.White));
            m_list_maintenance_border_do_2.ForEach(border => border.Background = new SolidColorBrush(Colors.White));
            m_list_maintenance_border_do_3.ForEach(border => border.Background = new SolidColorBrush(Colors.White));
            m_list_maintenance_border_led.ForEach(border => border.Background = new SolidColorBrush(Colors.White));
            m_list_maintenance_border_audio.ForEach(border => border.Background = new SolidColorBrush(Colors.White));

            m_list_border_force_mode.ForEach(border => border.Background = new SolidColorBrush(Colors.White));
            Globals.GetTheInstance().Forced_mode_do_controls.ToList().ForEach(mode => mode = false);
        }

        #endregion


        #region Memory map functions

        #region Timer manage memory data

        private void Timer_manage_memory_Tick(object sender, EventArgs e)
        {
            m_timer_manage_memory.Stop();
            try
            {
                if (Globals.GetTheInstance().ManageComThread.Is_connected && m_read_write_enable && !m_is_writing && !m_is_writing_all_config & !m_is_reading_event_hist)
                {
                    MEMORY_CONFIG_TYPE memory_config_type =
                        m_memory_read_state == MEMORY_READ_STATE.SAS360CON_INTERNAL_CFG ? MEMORY_CONFIG_TYPE.SAS360CON_INTERNAL_CFG :
                        m_memory_read_state == MEMORY_READ_STATE.SAS360CON_CFG ? MEMORY_CONFIG_TYPE.SAS360CON_CFG :
                        m_memory_read_state == MEMORY_READ_STATE.IOT_CFG ? MEMORY_CONFIG_TYPE.IOT_CFG :
                        m_memory_read_state == MEMORY_READ_STATE.UWB_INTERNAL_CFG ? MEMORY_CONFIG_TYPE.UWB_INTERNAL_CFG :

                        m_memory_read_state == MEMORY_READ_STATE.SAS360CON_IMAGE ? MEMORY_CONFIG_TYPE.SAS360CON_IMAGE :
                        m_memory_read_state == MEMORY_READ_STATE.UWB_IMAGE ? MEMORY_CONFIG_TYPE.UWB_IMAGE :

                        m_memory_read_state == MEMORY_READ_STATE.SAS360CON_MAINTENNANCE ? MEMORY_CONFIG_TYPE.SAS360CON_MAINTENNANCE :

                        m_memory_read_state == MEMORY_READ_STATE.CONSOLE_CLOSEST_TAGS_BASE_1 ? MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE_1 :
                        m_memory_read_state == MEMORY_READ_STATE.CONSOLE_CLOSEST_TAGS_BASE_2 ? MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE_2 :
                        m_memory_read_state == MEMORY_READ_STATE.CONSOLE_CLOSEST_TAGS_BASE_3 ? MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE_3 :

                        m_memory_read_state == MEMORY_READ_STATE.CONSOLE_CLOSEST_ZONE_BASE_1 ? MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_BASE_1 :
                        m_memory_read_state == MEMORY_READ_STATE.CONSOLE_CLOSEST_ZONE_BASE_2 ? MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_BASE_2 :

                        m_memory_read_state == MEMORY_READ_STATE.UWB_CLOSEST_TAGS_BASE ? MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_BASE :
                        m_memory_read_state == MEMORY_READ_STATE.UWB_CLOSEST_ZONE_BASE ? MEMORY_CONFIG_TYPE.UWB_CLOSEST_ZONE_BASE :

                        m_memory_read_state == MEMORY_READ_STATE.SAS360CON_NVREG ? MEMORY_CONFIG_TYPE.SAS360CON_NVREG :

                        MEMORY_CONFIG_TYPE.SAS360CON_IMAGE;

                    bool manage_memory_data_execute =
                        (memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_INTERNAL_CFG && !Globals.GetTheInstance().SAS360con_internal_config_read) ||
                        (memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_CFG && !Globals.GetTheInstance().SAS360con_config_read) ||
                        (memory_config_type == MEMORY_CONFIG_TYPE.IOT_CFG && !Globals.GetTheInstance().SAS360iot_config_read) ||
                        (memory_config_type == MEMORY_CONFIG_TYPE.UWB_INTERNAL_CFG && !Globals.GetTheInstance().SAS360con_uwb_internal_config_read) ||
                        (memory_config_type != MEMORY_CONFIG_TYPE.SAS360CON_INTERNAL_CFG && memory_config_type != MEMORY_CONFIG_TYPE.SAS360CON_CFG && memory_config_type != MEMORY_CONFIG_TYPE.IOT_CFG && memory_config_type != MEMORY_CONFIG_TYPE.UWB_INTERNAL_CFG);

                    if (memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_INTERNAL_CFG)
                    {
                        Manage_logs.SaveModbusValue("---------------------------------------------------------------------------------");
                        Manage_logs.SaveModbusValue("---------------------------------------------------------------------------------");
                        Manage_logs.SaveModbusValue("---------------------------------------------------------------------------------");
                        Manage_logs.SaveDataValue("---------------------------------------------------------------------------------");
                        Manage_logs.SaveDataValue("---------------------------------------------------------------------------------");
                        Manage_logs.SaveDataValue("---------------------------------------------------------------------------------");
                    }

                    bool enable_read_reg = m_received_resp;
                    if (!enable_read_reg)
                    {
                        enable_read_reg = m_wait_ticks_read++ > 3;
                        string s_info = enable_read_reg ? "TIMEOUT RESPUESTA (REPETICION TRAMA)" : "TIMEOUT RESPUESTA (WAIT TICK)";
                        Manage_logs.SaveModbusValue(s_info);
                    }

                    if (enable_read_reg)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            m_timer_manage_memory.Interval = manage_memory_data_execute ? Globals.GetTheInstance().Read_memory_interval : 1;

                            if (manage_memory_data_execute)
                            {
                                Manage_memory_data(memory_config_type);
                            }
                        });

                        m_memory_read_state =
                            m_memory_read_state == MEMORY_READ_STATE.SAS360CON_INTERNAL_CFG ? MEMORY_READ_STATE.SAS360CON_CFG :
                            m_memory_read_state == MEMORY_READ_STATE.SAS360CON_CFG ? MEMORY_READ_STATE.IOT_CFG :
                            m_memory_read_state == MEMORY_READ_STATE.IOT_CFG ? MEMORY_READ_STATE.UWB_INTERNAL_CFG :
                            m_memory_read_state == MEMORY_READ_STATE.UWB_INTERNAL_CFG ? MEMORY_READ_STATE.SAS360CON_IMAGE :

                            m_memory_read_state == MEMORY_READ_STATE.SAS360CON_IMAGE ? MEMORY_READ_STATE.UWB_IMAGE :
                            m_memory_read_state == MEMORY_READ_STATE.UWB_IMAGE ? MEMORY_READ_STATE.SAS360CON_MAINTENNANCE :

                            m_memory_read_state == MEMORY_READ_STATE.SAS360CON_MAINTENNANCE ? MEMORY_READ_STATE.CONSOLE_CLOSEST_TAGS_BASE_1 :

                            m_memory_read_state == MEMORY_READ_STATE.CONSOLE_CLOSEST_TAGS_BASE_1 ? MEMORY_READ_STATE.CONSOLE_CLOSEST_TAGS_BASE_2 :
                            m_memory_read_state == MEMORY_READ_STATE.CONSOLE_CLOSEST_TAGS_BASE_2 ? MEMORY_READ_STATE.CONSOLE_CLOSEST_TAGS_BASE_3 :
                            m_memory_read_state == MEMORY_READ_STATE.CONSOLE_CLOSEST_TAGS_BASE_3 ? MEMORY_READ_STATE.CONSOLE_CLOSEST_ZONE_BASE_1 :

                            m_memory_read_state == MEMORY_READ_STATE.CONSOLE_CLOSEST_ZONE_BASE_1 ? MEMORY_READ_STATE.CONSOLE_CLOSEST_ZONE_BASE_2 :
                            m_memory_read_state == MEMORY_READ_STATE.CONSOLE_CLOSEST_ZONE_BASE_2 ? MEMORY_READ_STATE.UWB_CLOSEST_TAGS_BASE :

                            m_memory_read_state == MEMORY_READ_STATE.UWB_CLOSEST_TAGS_BASE ? MEMORY_READ_STATE.UWB_CLOSEST_ZONE_BASE :

                            m_memory_read_state == MEMORY_READ_STATE.UWB_CLOSEST_ZONE_BASE ? MEMORY_READ_STATE.SAS360CON_NVREG :
                            m_memory_read_state == MEMORY_READ_STATE.SAS360CON_NVREG ? MEMORY_READ_STATE.SAS360CON_INTERNAL_CFG :
                            MEMORY_READ_STATE.SAS360CON_INTERNAL_CFG;
                    }
                }
                else if (m_is_writing || m_is_writing_all_config)
                    Manage_logs.SaveModbusValue("TIMER MANAGE MEMORY DISABLE -> WRITING IN MEMORY");

                else if (m_is_reading_event_hist)
                    Manage_logs.SaveModbusValue("TIMER MANAGE MEMORY DISABLE -> READING MEMORY LOG");
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{GetType().Name} -> {nameof(Timer_manage_memory_Tick)} -> {ex.Message}");
            }

            m_timer_manage_memory.Start();
        }

        #endregion

        #region Manage_memory data

        private bool Manage_memory_data(MEMORY_CONFIG_TYPE memory_config_type)
        {
            bool enable_read_memory = true;
            bool read_memory_ok = false;

            //Simulate data
            if (Globals.GetTheInstance().Simulator_mode == BIT_STATE.ON)
            {
                read_memory_ok = Manage_memory.Simulator_data(memory_config_type);
                Dispatcher.Invoke(() =>
                {
                    bool update_ok =
                        memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_INTERNAL_CFG ? Update_SAS360CON_internal_cfg_memory_info() :
                        memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_CFG ? Update_SAS360CON_cfg_memory_info() :
                        memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_IMAGE ? Update_SAS360CON_image_memory_info() :
                        memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_MAINTENNANCE ? Update_SAS360CON_maintennance_memory_info() :
                        memory_config_type == MEMORY_CONFIG_TYPE.UWB_INTERNAL_CFG ? Update_UWB_internal_cfg_memory_info() :
                        memory_config_type == MEMORY_CONFIG_TYPE.UWB_IMAGE ? Update_UWB_image_memory_info() :
                        memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE_3 ? Update_CONSOLE_closest_tags_base_memory_info() :
                        memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_BASE_2 ? Update_CONSOLE_closest_zone_base_memory_info() :
                        memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_NVREG ? Update_SAS360CON_nvreg_memory_info() : false;
                });
            }

            //Read data from sas360con
            else
            {
                ushort start_address = 0;
                ushort number_of_registers = 0;
                switch (memory_config_type)
                {
                    case MEMORY_CONFIG_TYPE.SAS360CON_INTERNAL_CFG:
                        {
                            enable_read_memory =
                                Functions.IsBitSetTo1(Globals.GetTheInstance().Enable_read_memory_bits, (int)ENABLE_READ_MEMORY_BIT.SAS360CON_INTERNAL_CFG) &&
                                Globals.GetTheInstance().List_sas360con_internal_cfg.Count > 0;

                            start_address = Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_INTERNAL_CFG];
                            number_of_registers = Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_INTERNAL_CFG];
                            break;
                        }

                    case MEMORY_CONFIG_TYPE.SAS360CON_CFG:
                        {
                            enable_read_memory =
                                Functions.IsBitSetTo1(Globals.GetTheInstance().Enable_read_memory_bits, (int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG) &&
                                Globals.GetTheInstance().List_sas360con_cfg.Count > 0;

                            start_address = Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG];
                            number_of_registers = Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG];
                            break;
                        }

                    case MEMORY_CONFIG_TYPE.IOT_CFG:
                        {
                            enable_read_memory = Functions.IsBitSetTo1(Globals.GetTheInstance().Enable_read_memory_bits, (int)ENABLE_READ_MEMORY_BIT.IOT_CFG);
                            start_address = Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.IOT_CFG];
                            number_of_registers = Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.IOT_CFG];
                            break;
                        }

                    case MEMORY_CONFIG_TYPE.SAS360CON_IMAGE:
                        {
                            enable_read_memory =
                                Functions.IsBitSetTo1(Globals.GetTheInstance().Enable_read_memory_bits, (int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE) &&
                                Globals.GetTheInstance().List_sas360con_image.Count > 0;

                            start_address = Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE];
                            number_of_registers = Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE];
                            break;
                        }

                    case MEMORY_CONFIG_TYPE.SAS360CON_MAINTENNANCE:
                        {
                            enable_read_memory =
                                Functions.IsBitSetTo1(Globals.GetTheInstance().Enable_read_memory_bits, (int)ENABLE_READ_MEMORY_BIT.SAS360CON_MAINTENNANCE) &&
                                Globals.GetTheInstance().List_sas360con_image.Count > 0;

                            start_address = (Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_MAINTENNANCE]);
                            number_of_registers = (Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_MAINTENNANCE]);

                            break;
                        }

                    case MEMORY_CONFIG_TYPE.UWB_INTERNAL_CFG:
                        {
                            enable_read_memory =
                                Functions.IsBitSetTo1(Globals.GetTheInstance().Enable_read_memory_bits, (int)ENABLE_READ_MEMORY_BIT.UWB_INTERNAL_CFG) &&
                                Globals.GetTheInstance().List_uwb_internal_cfg.Count > 0;

                            start_address = Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.UWB_INTERNAL_CFG];
                            number_of_registers = Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.UWB_INTERNAL_CFG];

                            break;
                        }

                    case MEMORY_CONFIG_TYPE.UWB_IMAGE:
                        {
                            enable_read_memory =
                                Functions.IsBitSetTo1(Globals.GetTheInstance().Enable_read_memory_bits, (int)ENABLE_READ_MEMORY_BIT.UWB_IMAGE) &&
                                Globals.GetTheInstance().List_uwb_image.Count > 0;

                            start_address = Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.UWB_IMAGE];
                            number_of_registers = Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.UWB_IMAGE];

                            break;
                        }

                    case MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE_1:
                        {
                            enable_read_memory =
                                Functions.IsBitSetTo1(Globals.GetTheInstance().Enable_read_memory_bits, (int)ENABLE_READ_MEMORY_BIT.CONSOLE_CLOSEST_TAGS_BASE) &&
                                Globals.GetTheInstance().List_console_closest_tags_base.Count > 0;

                            start_address = Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.CONSOLE_CLOSEST_TAGS_BASE];
                            number_of_registers = Constants.MAX_REG_READ_MODBUS;

                            break;
                        }

                    case MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE_2:
                        {
                            enable_read_memory =
                                Functions.IsBitSetTo1(Globals.GetTheInstance().Enable_read_memory_bits, (int)ENABLE_READ_MEMORY_BIT.CONSOLE_CLOSEST_TAGS_BASE) &&
                                Globals.GetTheInstance().List_console_closest_tags_base.Count > 0;

                            start_address = (ushort)(Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.CONSOLE_CLOSEST_TAGS_BASE] + Constants.MAX_REG_READ_MODBUS);
                            number_of_registers = Constants.MAX_REG_READ_MODBUS;

                            break;
                        }

                    case MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE_3:
                        {
                            enable_read_memory =
                                Functions.IsBitSetTo1(Globals.GetTheInstance().Enable_read_memory_bits, (int)ENABLE_READ_MEMORY_BIT.CONSOLE_CLOSEST_TAGS_BASE) &&
                                Globals.GetTheInstance().List_console_closest_tags_base.Count > 0;

                            start_address = (ushort)(Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.CONSOLE_CLOSEST_TAGS_BASE] + (Constants.MAX_REG_READ_MODBUS * 2));
                            number_of_registers = (ushort)(Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.CONSOLE_CLOSEST_TAGS_BASE] - (Constants.MAX_REG_READ_MODBUS * 2));

                            break;
                        }

                    case MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_BASE_1:
                        {
                            enable_read_memory =
                                Functions.IsBitSetTo1(Globals.GetTheInstance().Enable_read_memory_bits, (int)ENABLE_READ_MEMORY_BIT.CONSOLE_CLOSEST_ZONE_BASE) &&
                                Globals.GetTheInstance().List_console_closest_zone_base.Count > 0;

                            start_address = Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.CONSOLE_CLOSEST_ZONE_BASE];
                            number_of_registers = Constants.MAX_REG_READ_MODBUS;
                            break;
                        }

                    case MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_BASE_2:
                        {
                            enable_read_memory =
                                Functions.IsBitSetTo1(Globals.GetTheInstance().Enable_read_memory_bits, (int)ENABLE_READ_MEMORY_BIT.CONSOLE_CLOSEST_ZONE_BASE) &&
                                Globals.GetTheInstance().List_console_closest_zone_base.Count > 0;


                            start_address = (ushort)(Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.CONSOLE_CLOSEST_ZONE_BASE] + Constants.MAX_REG_READ_MODBUS);
                            number_of_registers = (ushort)(Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.CONSOLE_CLOSEST_ZONE_BASE] - Constants.MAX_REG_READ_MODBUS);
                            break;
                        }

                    case MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_BASE:
                        {
                            enable_read_memory =
                                Functions.IsBitSetTo1(Globals.GetTheInstance().Enable_read_memory_bits, (int)ENABLE_READ_MEMORY_BIT.UWB_CLOSEST_TAGS_BASE) &&
                                 Globals.GetTheInstance().List_uwb_closest_tags_base.Count > 0;

                            start_address = (ushort)Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.UWB_CLOSEST_TAGS_BASE];
                            number_of_registers = Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.UWB_CLOSEST_TAGS_BASE];
                            break;
                        }


                    case MEMORY_CONFIG_TYPE.UWB_CLOSEST_ZONE_BASE:
                        {
                            enable_read_memory = enable_read_memory = Functions.IsBitSetTo1(Globals.GetTheInstance().Enable_read_memory_bits, (int)ENABLE_READ_MEMORY_BIT.UWB_CLOSEST_ZONE_BASE);
                            break;
                        }

                    case MEMORY_CONFIG_TYPE.SAS360CON_NVREG:
                        {
                            enable_read_memory =
                              Functions.IsBitSetTo1(Globals.GetTheInstance().Enable_read_memory_bits, (int)ENABLE_READ_MEMORY_BIT.SAS360CON_NVREG) &&
                              Globals.GetTheInstance().List_sas360con_nvreg.Count > 0;

                            start_address = (Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_NVREG]);
                            number_of_registers = (Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_NVREG]);

                            break;
                        }
                }

                if (enable_read_memory)
                {
                    Manage_logs.SaveModbusValue($"READ HOLDING REGISTER ({memory_config_type}) -> INI: {start_address} / COUNT: {number_of_registers}");

                    read_memory_ok = Globals.GetTheInstance().ManageComThread.Read_holding_registers_int32(start_address, number_of_registers, memory_config_type);
                    if (!read_memory_ok)
                        Manage_logs.SaveModbusValue($"ERROR READING HOLDING REG -> {memory_config_type}");

                    else
                    {
                        m_received_resp = false;
                        m_wait_ticks_read = 0;
                    }
                }
            }

            return read_memory_ok;
        }

        #endregion

        #region Update windows values

        #region Update SAS360CON internal cfg memory info

        private bool Update_SAS360CON_internal_cfg_memory_info()
        {
            bool update_ok = false;
            Globals.GetTheInstance().SAS360con_internal_config_read = true;

            try
            {
                CollectionViewSource.GetDefaultView(Listview_sas360con_internal_cfg.ItemsSource).Refresh();

                Label_main_serial_number.Content = Globals.GetTheInstance().SAS360CON_internal_cfg.Serial_number;
                Label_config_serial_number_value.Content = Globals.GetTheInstance().SAS360CON_internal_cfg.Serial_number;

                Label_main_tag_id_fab.Content = Globals.GetTheInstance().SAS360CON_internal_cfg.ID_manufacturing;
                Label_config_id_2lsb_value.Content = Globals.GetTheInstance().SAS360CON_internal_cfg.ID_manufacturing;

                Label_main_tag_type.Content = $"{(byte)Globals.GetTheInstance().SAS360CON_internal_cfg.Tag_type!} - {Manage_memory.SAS360TAG_ZONE_TYPE(Globals.GetTheInstance().SAS360CON_internal_cfg.Tag_type)}";
                Label_config_tag_type_value.Content = $"{(byte)Globals.GetTheInstance().SAS360CON_internal_cfg.Tag_type!} - {Manage_memory.SAS360TAG_ZONE_TYPE(Globals.GetTheInstance().SAS360CON_internal_cfg.Tag_type)}";

                Label_main_version_hw.Content = Globals.GetTheInstance().SAS360CON_internal_cfg.Version_hw;
                Label_config_version_hw.Content = Globals.GetTheInstance().SAS360CON_internal_cfg.Version_hw;

                Label_main_version_fw.Content = Globals.GetTheInstance().SAS360CON_internal_cfg.Version_fw;
                Label_config_version_fw.Content = Globals.GetTheInstance().SAS360CON_internal_cfg.Version_fw;


                Label_main_version_boot.Content = Globals.GetTheInstance().SAS360CON_internal_cfg.Version_boot;
                Label_config_version_boot.Content = Globals.GetTheInstance().SAS360CON_internal_cfg.Version_boot;

                Label_config_rtu_slave_speed_value.Content = Globals.GetTheInstance().SAS360CON_internal_cfg.RTU_slave_speed;
                Label_config_rtu_slave_num_value.Content = Globals.GetTheInstance().SAS360CON_internal_cfg.RTU_slave_num;
                Label_config_modbus_lin_master_speed_value.Content = Globals.GetTheInstance().SAS360CON_internal_cfg.Lin_master_speed;

                Label_main_consola_id.Content = Globals.GetTheInstance().SAS360CON_internal_cfg.Consola_id;
                Label_config_consola_id_value.Content = Globals.GetTheInstance().SAS360CON_internal_cfg.Consola_id;

                Label_config_fw_update.Content = Globals.GetTheInstance().SAS360CON_internal_cfg.RTC_fw_update;
                Label_config_last_cfg_change.Content = Globals.GetTheInstance().SAS360CON_internal_cfg.RTC_config_update;
                Label_config_internal_cfg_change_counter.Content = Globals.GetTheInstance().SAS360CON_internal_cfg.Change_counter;
                Label_config_crc_config_interna.Content = $"0X{Globals.GetTheInstance().SAS360CON_internal_cfg.CRC_config:X4}";

                update_ok = true;
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Update_SAS360CON_internal_cfg_memory_info)} -> {ex.Message}");
            }

            return update_ok;
        }

        #endregion

        #region Update SAS360CON cfg memory info

        private bool Update_SAS360CON_cfg_memory_info()
        {
            bool update_ok = false;
            Globals.GetTheInstance().SAS360con_config_read = true;

            try
            {
                Button_send_config_tab_sas360con.FontSize = 14;
                Button_send_config_tab_sas360con.Foreground = new SolidColorBrush(Colors.Black);

                double rectangle_width = Rectangle_sas360_data_draw.ActualWidth;
                double rectangle_height = Rectangle_sas360_data_draw.ActualHeight;

                #region Clear detection controls in map

                IEnumerable<Ellipse> list_ellipse_control = Canvas_sas360_data_draw.Children.OfType<Ellipse>();
                List<string> list_ellipse_names = new() { "Ellipse_detection_general", "Ellipse_detection_yellow", "Ellipse_detection_orange", "Ellipse_detection_red" };
                list_ellipse_control.ToList().ForEach(ellipse =>
                {
                    if (list_ellipse_names.Contains(ellipse.Name))
                    {
                        Canvas_sas360_data_draw.Children.Remove(ellipse);
                    }
                });

                #endregion

                CollectionViewSource.GetDefaultView(Listview_tag_processed.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(Listview_sas360con_cfg.ItemsSource).Refresh();

                Update_SAS360CON_cfg_instalation_client();
                Update_SAS360CON_cfg_vehicle_configuration();
                Update_SAS360CON_cfg_detection_area();
                Update_SAS360CON_cfg_output_actions();
                Update_SAS360CON_cfg_uwb_config();
                Update_SAS360CON_cfg_recording();
                Update_SAS360CON_cfg_miscellaneous();
                Update_SAS360CON_cfg_calculadas();

                update_ok = true;
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Update_SAS360CON_cfg_memory_info)} -> {ex.Message}");
            }

            return update_ok;
        }

        private void Update_SAS360CON_cfg_instalation_client()
        {
            try
            {
                Label_main_client_id.Content = Globals.GetTheInstance().SAS360CON_cfg_installation_client.Client.ToString();
                Textbox_config_client_id.Text = Globals.GetTheInstance().SAS360CON_cfg_installation_client.Client.ToString();

                Label_main_installation_id.Content = Globals.GetTheInstance().SAS360CON_cfg_installation_client.Installation.ToString();
                Textbox_config_installation_id.Text = Globals.GetTheInstance().SAS360CON_cfg_installation_client.Installation.ToString();


                Label_main_vehicle_type_id.Content = Globals.GetTheInstance().SAS360CON_cfg_installation_client.Vehicle_type.ToString();
                Textbox_config_vehicle_type_id.Text = Globals.GetTheInstance().SAS360CON_cfg_installation_client.Vehicle_type.ToString();

                DecimalUpDown_audio_language.Value = Globals.GetTheInstance().SAS360CON_cfg_installation_client.Audio_language;
                DecimalUpDown_audio_volume.Value = Globals.GetTheInstance().SAS360CON_cfg_installation_client.Audio_volume;
            }
            catch { }
        }

        private void Update_SAS360CON_cfg_vehicle_configuration()
        {
            double rectangle_width = Rectangle_sas360_data_draw.ActualWidth;
            double rectangle_height = Rectangle_sas360_data_draw.ActualHeight;


            #region MAP

            if (Globals.GetTheInstance().Draw_map == BIT_STATE.ON)
            {
                #region Vehicle

                double vehicle_x = rectangle_width * (double)decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_vehicle_cfg.Vehicle_dim_xy_cm[0], Globals.GetTheInstance().Panel_area_cm);
                double vehicle_y = rectangle_height * (double)decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_vehicle_cfg.Vehicle_dim_xy_cm[1], Globals.GetTheInstance().Panel_area_cm);

                Image_vehicle.Visibility = Visibility.Visible;
                Image_vehicle.Width = vehicle_x;
                Image_vehicle.Height = vehicle_y;

                double left = rectangle_width / 2;
                left -= vehicle_x / 2;

                double top = rectangle_height / 2;
                top -= vehicle_y / 2;

                Canvas.SetLeft(Image_vehicle, left);
                Canvas.SetTop(Image_vehicle, top);

                #endregion

                #region Antennas

                m_array_rectangle_antenna
                    .Select((item, index) => new { Item = item, Position = index }).ToList()
                    .ForEach(rectangle =>
                    {
                        #region Antennas angle

                        //double antenna_angle = (double) decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_vehicle_cfg.Antena_angle_cm[rectangle.Position], 100);
                        RotateTransform rotate = new()
                        {
                            Angle = rectangle.Position == 0 ? 135 : rectangle.Position == 1 ? 45 : 180
                        };
                        m_array_rectangle_antenna[rectangle.Position].LayoutTransform = rotate;

                        #endregion

                        #region Antennas position

                        /*
                            double antenna_width = m_array_rectangle_antenna[rectangle.Position].Width;
                            double antenna_height = m_array_rectangle_antenna[rectangle.Position].Height;

                            short[] antena_xy_cm = Functions.SliceRow(Globals.GetTheInstance().SAS360CON_cfg_vehicle_cfg.Antenna_xy, rectangle.Position).ToArray();

                            double antenna_x = rectangle_width * (Math.Abs(antena_xy_cm[0]) / (double)Globals.GetTheInstance().Panel_area_cm);
                            antenna_x = antena_xy_cm[0] < 0 ? (antenna_x + (antenna_width / 2)) * -1 : antenna_x - (antenna_width / 2);
                            antenna_x = (rectangle_width / 2) + antenna_x;
                            Canvas.SetLeft(m_array_rectangle_antenna[rectangle.Position], antenna_x);

                            double antenna_y = rectangle_width * (Math.Abs(antena_xy_cm[1]) / (double)Globals.GetTheInstance().Panel_area_cm);
                            antenna_y = antena_xy_cm[1] > 0 ? (antenna_y + (antenna_height / 2)) * -1 : antenna_y - (antenna_height / 2);
                            antenna_y = (rectangle_height / 2) + antenna_y;
                            Canvas.SetTop(m_array_rectangle_antenna[rectangle.Position], antenna_y);

                            rectangle.Item.Visibility = Visibility.Visible;
                        */

                        #endregion

                    });

                #endregion
            }

            #endregion

            #region CONFIG TAB

            Textbox_config_vehicle_dim_x.Text = $"{decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_vehicle_cfg.Vehicle_dim_xy_cm[0], 100):0.00}";
            Textbox_config_vehicle_dim_y.Text = $"{decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_vehicle_cfg.Vehicle_dim_xy_cm[1], 100):0.00}";

            int index_control_antenna = 2;
            for (int index = 0; index < Constants.ANTENNA_COUNT; index++)
            {
                m_list_textbox_sas360con_cfg_vehicle[index_control_antenna].Text = $"{decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_vehicle_cfg.Antenna_xy_cm[index, 0], 100):0.00}";
                m_list_textbox_sas360con_cfg_vehicle[index_control_antenna + 1].Text = $"{decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_vehicle_cfg.Antenna_xy_cm[index, 1], 100):0.00}";

                index_control_antenna += 2;
            }

            #endregion
        }

        private void Update_SAS360CON_cfg_detection_area()
        {
            #region Chart legend

            Label_area_yellow.Content =
                $"{decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_LEFT_ANRI_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW], 100) + decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_RIGHT_ANRI_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW], 100):0.00} X " +
                $"{decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_FRONT_ANRI_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW], 100) + decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_BACK_ANRI_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW], 100):0.00} ";

            Label_area_orange.Content =
                $"{decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_LEFT_ANRI_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE], 100) + decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_RIGHT_ANRI_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE], 100):0.00} X " +
                $"{decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_FRONT_ANRI_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE], 100) + decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_BACK_ANRI_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE], 100):0.00} ";

            Label_area_red.Content =
                $"{decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_LEFT_ANRI_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.RED], 100) + decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_RIGHT_ANRI_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.RED], 100):0.00} X " +
                $"{decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_FRONT_ANRI_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.RED], 100) + decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_BACK_ANRI_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.RED], 100):0.00} ";

            Label_area_interior.Content =
                $"{decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_LEFT_ANRI_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.INTERIOR], 100) + decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_RIGHT_ANRI_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.INTERIOR], 100):0.00} X " +
                $"{decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_FRONT_ANRI_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.INTERIOR], 100) + decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_BACK_ANRI_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.INTERIOR], 100):0.00}";


            Label_car_size.Content = $"{decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_vehicle_cfg.Vehicle_dim_xy_cm[0], 100)} X {decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_vehicle_cfg.Vehicle_dim_xy_cm[1], 100)}";

            #endregion

            #region Config tab

            for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
            {
                m_list_textbox_config_area_FRONT_ANRI[index].Text = $"{decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_FRONT_ANRI_dist_cm[index], 100):0.00}";
                m_list_textbox_config_area_RIGHT_ANRI[index].Text = $"{decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_RIGHT_ANRI_dist_cm[index], 100):0.00}";
                m_list_textbox_config_area_BACK_ANRI[index].Text = $"{decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_BACK_ANRI_dist_cm[index], 100):0.00}";
                m_list_textbox_config_area_LEFT_ANRI[index].Text = $"{decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_LEFT_ANRI_dist_cm[index], 100):0.00}";
                m_list_textbox_config_area_DIST_ANTENA_ANRI[index].Text = $"{decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_DIST_ANTENA_ANRI_dist_cm[index], 100):0.00}";
            }

            Textbox_config_area_detection_distance.Text = $"{decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_detection_area.Area_detection_distance_cm, 100):0.00}";
            Textbox_config_area_change_hyst.Text = $"{decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_detection_area.Area_change_hysteresis_cent_pct, 100)}";
            Textbox_config_sector_change_hyst.Text = $"{decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_detection_area.Sector_change_hysteresis_cent_pct, 100)}";
            DecimalUpDown_config_trilat_calc_enabled.Value = Globals.GetTheInstance().SAS360CON_cfg_detection_area.Trilat_calc_enabled;
            DecimalUpDown_config_gestion_avanzada_pos_enable.Value = Globals.GetTheInstance().SAS360CON_cfg_detection_area.Gestion_avanzada_position_enable;


            #endregion

            //Draw_config_sas360con_detection_areas();
            Draw_config_sas360con_detection_areas_polygon();
        }

        private void Update_SAS360CON_cfg_output_actions()
        {
            ushort rele1_value = Globals.GetTheInstance().SAS360CON_cfg_general.Array_actuaciones_salidas[(int)ACTUACIONES_SALIDAS_POS_IN_ARRAY.RELE_1];
            ushort rele2_value = Globals.GetTheInstance().SAS360CON_cfg_general.Array_actuaciones_salidas[(int)ACTUACIONES_SALIDAS_POS_IN_ARRAY.RELE_2];
            ushort rele3_value = Globals.GetTheInstance().SAS360CON_cfg_general.Array_actuaciones_salidas[(int)ACTUACIONES_SALIDAS_POS_IN_ARRAY.RELE_3];
            ushort rele4_value = Globals.GetTheInstance().SAS360CON_cfg_general.Array_actuaciones_salidas[(int)ACTUACIONES_SALIDAS_POS_IN_ARRAY.RELE_4];
            ushort trans1_value = Globals.GetTheInstance().SAS360CON_cfg_general.Array_actuaciones_salidas[(int)ACTUACIONES_SALIDAS_POS_IN_ARRAY.TRANS_1];
            ushort trans2_value = Globals.GetTheInstance().SAS360CON_cfg_general.Array_actuaciones_salidas[(int)ACTUACIONES_SALIDAS_POS_IN_ARRAY.TRANS_2];

            Label_config_output_actions_rele1.Content = $"0X{rele1_value:X4}";
            Label_config_output_actions_rele2.Content = $"0X{rele2_value:X4}";
            Label_config_output_actions_rele3.Content = $"0X{rele3_value:X4}";
            Label_config_output_actions_rele4.Content = $"0X{rele4_value:X4}";
            Label_config_output_actions_trans1.Content = $"0X{trans1_value:X4}";
            Label_config_output_actions_trans2.Content = $"0X{trans2_value:X4}";

            for (int bit_pos = 0; bit_pos < Constants.MAX_BITS_USHORT_VALUE; bit_pos++)
            {
                m_list_border_sas360_config_rele1[bit_pos].Background = Functions.IsBitSetTo1(rele1_value, bit_pos) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                m_list_border_sas360_config_rele2[bit_pos].Background = Functions.IsBitSetTo1(rele2_value, bit_pos) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                m_list_border_sas360_config_rele3[bit_pos].Background = Functions.IsBitSetTo1(rele3_value, bit_pos) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                m_list_border_sas360_config_rele4[bit_pos].Background = Functions.IsBitSetTo1(rele4_value, bit_pos) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                m_list_border_sas360_config_trans1[bit_pos].Background = Functions.IsBitSetTo1(trans1_value, bit_pos) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                m_list_border_sas360_config_trans2[bit_pos].Background = Functions.IsBitSetTo1(trans2_value, bit_pos) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
            }
        }

        private void Update_SAS360CON_cfg_uwb_config()
        {
            for (int index = 0; index < Constants.UWB_TOTAL_COUNT; index++)
            {
                m_list_label_uwb_management_lin_slave[index].Content = $"{Globals.GetTheInstance().Array_SAS360CON_UWB[index].Lin} / {Globals.GetTheInstance().Array_SAS360CON_UWB[index].Slave}";

                m_list_textbox_sas360con_cfg_uwb_communication[index].Text = Globals.GetTheInstance().Array_SAS360CON_UWB[index].Lin.ToString();
                m_list_textbox_sas360con_cfg_uwb_communication[index + Constants.UWB_TOTAL_COUNT].Text = Globals.GetTheInstance().Array_SAS360CON_UWB[index].Slave.ToString();
            }
        }

        private void Update_SAS360CON_cfg_recording()
        {
            for (int index = 0; index < Constants.RECORDING_REG_SAS360CON_ARRAY; index++)
            {
                m_list_textbox_sas360con_cfg_recording_index[index].Text = Globals.GetTheInstance().SAS360CON_cfg_recording.Array_recorded_register_index[index].ToString();
                m_list_textbox_sas360con_cfg_recording_unit[index].Text = Globals.GetTheInstance().SAS360CON_cfg_recording.Array_recorded_register_unit_codif[index].ToString();
                m_list_textbox_sas360con_cfg_recording_period[index].Text = Globals.GetTheInstance().SAS360CON_cfg_recording.Array_recorded_register_period_secs[index].ToString();
            }
        }

        private void Update_SAS360CON_cfg_miscellaneous()
        {
            Textbox_miscellaneous_output_deactivation_delay.Text = Globals.GetTheInstance().SAS360CON_cfg_general.Output_deactivation_delay_sec.ToString();
            Textbox_miscellaneous_area_zone_dist.Text = $"{decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_general.Area_zone_dist_cm, 100):0.00}";
            Textbox_miscellaneous_red_zone_alert_audio_repeat_sec.Text = Globals.GetTheInstance().SAS360CON_cfg_general.Red_zone_alert_audio_repeat_sec.ToString();
            Textbox_miscellaneous_tag_list_clear_timeout.Text = $"{decimal.Divide(Globals.GetTheInstance().SAS360CON_cfg_general.Clear_undetected_tag_decseg, 100):0.00}";
        }

        private void Update_SAS360CON_cfg_calculadas()
        {
            Label_rtc_last_config.Content = Globals.GetTheInstance().SAS360CON_cfg_general.RTC_last_config_change;
            Label_config_change_counter.Content = Globals.GetTheInstance().SAS360CON_cfg_general.Change_counter;
            Label_crc_config.Content = Globals.GetTheInstance().SAS360CON_cfg_general.CRC_config;
        }

        #endregion

        #region Update SAS360CON image memory info

        private bool Update_SAS360CON_image_memory_info()
        {
            bool update_ok = false;
            try
            {
                CollectionViewSource.GetDefaultView(Listview_sas360con_image.ItemsSource).Refresh();

                Update_SAS360CON_image_estados_booleanos();
                Update_SAS360CON_image_entradas_sensores();
                Update_SAS360CON_image_nvreg_management();
                Update_SAS360CON_image_main_management();
                Update_SAS360CON_image_lin_pooling();
                Update_SAS360CON_image_con_processed_tags();

                update_ok = true;
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Update_SAS360CON_image_memory_info)} -> {ex.Message}");
            }

            return update_ok;
        }

        private void Update_SAS360CON_image_estados_booleanos()
        {
            const int max_leds = 28;
            const int max_pos_value = 16;

            #region Main tab

            Label_main_codif_bits_1.Content = $"0X{Globals.GetTheInstance().SAS360CON_image_general.Array_codif_bits[0]:X4}";
            Label_main_codif_management_1.Content = $"0X{Globals.GetTheInstance().SAS360CON_image_general.Array_codif_management[0]:X4}";
            Label_main_codif_management_2.Content = $"0X{Globals.GetTheInstance().SAS360CON_image_general.Array_codif_management[1]:X4}";

            Globals.GetTheInstance().SAS360CON_image_general.Array_digital_states.ToList()
                .Select((Value, Index) => new { Value, Position = Index }).ToList()
                .ForEach(digital_state => m_list_label_output_states[digital_state.Position].Content = $"0X{digital_state.Value:X4}");

            Label_main_state.Content = Globals.GetTheInstance().SAS360CON_image_general.Sas360_state;
            Label_main_substate.Content = (ushort)Globals.GetTheInstance().SAS360CON_image_general.Sas360_substate!;
            Label_main_master_watchdog.Content = Globals.GetTheInstance().SAS360CON_image_general.Watchdog_inc;
            Label_main_rtc_utc_sec.Content = Globals.GetTheInstance().SAS360CON_image_general.RTC_UTC_seconds;
            Label_main_rtc_utc_date.Content = $"{Constants.date_ref.Date.AddSeconds(Globals.GetTheInstance().SAS360CON_image_general.RTC_UTC_seconds):dd/MM/yyyy HH:mm:ss}";
            Label_main_rtc_milliseconds.Content = $"{Globals.GetTheInstance().SAS360CON_image_general.Milliseconds:D3}";

            #endregion

            #region Digital inputs

            ushort digital_input_value = Globals.GetTheInstance().SAS360CON_image_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.INPUT];
            Label_digital_input_value.Content = $"0x{digital_input_value:X4}";
            int index_control = 0;
            Enum.GetValues(typeof(MASK_CODIF_DI1)).Cast<MASK_CODIF_DI1>().ToList().ForEach(digital_input =>
            {
                m_list_maintenance_border_di[index_control++].Background = Functions.IsBitSetTo1(digital_input_value, (int)digital_input) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
            });

            #endregion

            #region Digital outputs

            if (Globals.GetTheInstance().Forced_mode_do_controls[(int)FORCE_MODE_CODIF.M_FORCE_DIGITAL_OUTPUTS] == false)
            {
                //Digital outputs 1 INT
                ushort digital_output_1_value = Globals.GetTheInstance().SAS360CON_image_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.OUTPUT_1_INT];

                Label_digital_output_1_value.Content = $"0x{digital_output_1_value:X4}";
                index_control = 0;
                Enum.GetValues(typeof(FORCE_MASK_DO1)).Cast<FORCE_MASK_DO1>().ToList().ForEach(digital_output =>
                {
                    m_list_maintenance_border_do_1[index_control++].Background = Functions.IsBitSetTo1(digital_output_1_value, (int)digital_output) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                });

                //Digital outputs 2 EXT
                ushort digital_output_2_value = Globals.GetTheInstance().SAS360CON_image_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.OUTPUT_2_EXT];
                Label_digital_output_2_value.Content = $"0x{digital_output_2_value:X4}";
                index_control = 0;
                Enum.GetValues(typeof(FORCE_MASK_DO2)).Cast<FORCE_MASK_DO2>().ToList().ForEach(digital_output =>
                {
                    m_list_maintenance_border_do_2[index_control++].Background = Functions.IsBitSetTo1(digital_output_2_value, (int)digital_output) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                });

                //Digital outputs 3 LED
                ushort digital_output_3_value = Globals.GetTheInstance().SAS360CON_image_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.OUTPUT_3_LED];
                Label_digital_output_3_value.Content = $"0x{digital_output_3_value:X4}";
                index_control = 0;
                Enum.GetValues(typeof(FORCE_MASK_DO3)).Cast<FORCE_MASK_DO3>().ToList().ForEach(digital_output =>
                {
                    m_list_maintenance_border_do_3[index_control++].Background = Functions.IsBitSetTo1(digital_output_3_value, (int)digital_output) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                });
            }

            #endregion

            #region Codif leds

            if (Globals.GetTheInstance().Forced_mode_do_controls[(int)FORCE_MODE_CODIF.M_FORCE_LEDS] == false)
            {
                ushort codif_led1_value = Globals.GetTheInstance().SAS360CON_image_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.CODIF_LED_1];
                Label_codif_led1_value.Content = $"0x{codif_led1_value:X4}";
                ushort codif_led2_value = Globals.GetTheInstance().SAS360CON_image_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.CODIF_LED_2];
                Label_codif_led2_value.Content = $"0x{codif_led2_value:X4}";

                #region Leds general

                byte[] array_byte_codif_led1 = BitConverter.GetBytes(codif_led1_value);
                byte[] array_byte_codif_led2 = BitConverter.GetBytes(codif_led2_value);
                IEnumerable<byte> array_byte_codif_led = array_byte_codif_led1.Concat(array_byte_codif_led2);
                int codif_led_value = BitConverter.ToInt32(array_byte_codif_led.ToArray());

                bool error_led = Functions.IsBitSetTo1(codif_led_value, (int)CONTROL_LEDS_POS.ERROR);
                Grid_error_led_1.Visibility = error_led ? Visibility.Visible : Visibility.Collapsed;
                Grid_error_led_2.Visibility = error_led ? Visibility.Visible : Visibility.Collapsed;

                if (error_led)
                {
                    Label_error_led_1.Content = Globals.GetTheInstance().SAS360CON_image_general.Sas360_state == MASK_SAS360CON_STATE.INTERNAL_ERROR ? "ERROR" : "ALERTA";
                    Label_error_led_2.Content = Globals.GetTheInstance().SAS360CON_image_general.Sas360_state == MASK_SAS360CON_STATE.INTERNAL_ERROR ? "ERROR" : "ALERTA";
                }

                #endregion

                #region Loop leds value

                for (int index_codif_led = 0; index_codif_led < max_leds; index_codif_led++)
                {
                    //if (m_array_map_border_led_detection[index_codif_led] != null)
                    //    m_array_map_border_led_detection[index_codif_led].Visibility = Functions.IsBitSetTo1(codif_led_value, index_codif_led) ? Visibility.Visible : Visibility.Collapsed;

                    m_list_maintenance_border_led[index_codif_led].Background = Functions.IsBitSetTo1(codif_led_value, index_codif_led) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);

                }

                #endregion

                #region Leds pantalla maintennance

                Ellipse_error1_1.Fill = Functions.IsBitSetTo1(codif_led_value, 0) ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.White);
                Ellipse_error2_1.Fill = Functions.IsBitSetTo1(codif_led_value, 0) ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.White);
                Ellipse_error3_1.Fill = Functions.IsBitSetTo1(codif_led_value, 0) ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.White);

                Ellipse_warning_ped_2.Fill = Functions.IsBitSetTo1(codif_led_value, 1) ? new SolidColorBrush(Colors.LightCoral) : new SolidColorBrush(Colors.White);
                Ellipse_warning_ligero_3.Fill = Functions.IsBitSetTo1(codif_led_value, 2) ? new SolidColorBrush(Color.FromArgb(255, 0xFD, 0XC4, 0X5C)) : new SolidColorBrush(Colors.White);
                Ellipse_warning_pesado_4.Fill = Functions.IsBitSetTo1(codif_led_value, 3) ? new SolidColorBrush(Colors.LightBlue) : new SolidColorBrush(Colors.White);

                Led_yellow_5.Fill = Functions.IsBitSetTo1(codif_led_value, 4) ? new SolidColorBrush(Colors.Yellow) : new SolidColorBrush(Colors.White);
                Led_yellow_6.Fill = Functions.IsBitSetTo1(codif_led_value, 5) ? new SolidColorBrush(Colors.Yellow) : new SolidColorBrush(Colors.White);
                Led_yellow_7.Fill = Functions.IsBitSetTo1(codif_led_value, 6) ? new SolidColorBrush(Colors.Yellow) : new SolidColorBrush(Colors.White);
                Ellipse_orange_8.Fill = Functions.IsBitSetTo1(codif_led_value, 7) ? new SolidColorBrush(Color.FromArgb(255, 0xFD, 0XC4, 0X5C)) : new SolidColorBrush(Colors.White);
                Ellipse_orange_9.Fill = Functions.IsBitSetTo1(codif_led_value, 8) ? new SolidColorBrush(Color.FromArgb(255, 0xFD, 0XC4, 0X5C)) : new SolidColorBrush(Colors.White);
                Ellipse_orange_10.Fill = Functions.IsBitSetTo1(codif_led_value, 9) ? new SolidColorBrush(Color.FromArgb(255, 0xFD, 0XC4, 0X5C)) : new SolidColorBrush(Colors.White);
                Ellipse_red_11.Fill = Functions.IsBitSetTo1(codif_led_value, 10) ? new SolidColorBrush(Colors.LightCoral) : new SolidColorBrush(Colors.White);
                Led_yellow_12.Fill = Functions.IsBitSetTo1(codif_led_value, 11) ? new SolidColorBrush(Colors.Yellow) : new SolidColorBrush(Colors.White);
                Ellipse_orange_13.Fill = Functions.IsBitSetTo1(codif_led_value, 12) ? new SolidColorBrush(Color.FromArgb(255, 0xFD, 0XC4, 0X5C)) : new SolidColorBrush(Colors.White);
                Ellipse_red_14.Fill = Functions.IsBitSetTo1(codif_led_value, 13) ? new SolidColorBrush(Colors.LightCoral) : new SolidColorBrush(Colors.White);
                Ellipse_red_15.Fill = Functions.IsBitSetTo1(codif_led_value, 14) ? new SolidColorBrush(Colors.LightCoral) : new SolidColorBrush(Colors.White);
                Ellipse_orange_16.Fill = Functions.IsBitSetTo1(codif_led_value, 15) ? new SolidColorBrush(Color.FromArgb(255, 0xFD, 0XC4, 0X5C)) : new SolidColorBrush(Colors.White);
                Led_yellow_17.Fill = Functions.IsBitSetTo1(codif_led_value, 16) ? new SolidColorBrush(Colors.Yellow) : new SolidColorBrush(Colors.White);
                Ellipse_red_18.Fill = Functions.IsBitSetTo1(codif_led_value, 17) ? new SolidColorBrush(Colors.LightCoral) : new SolidColorBrush(Colors.White);
                Ellipse_orange_19.Fill = Functions.IsBitSetTo1(codif_led_value, 18) ? new SolidColorBrush(Color.FromArgb(255, 0xFD, 0XC4, 0X5C)) : new SolidColorBrush(Colors.White);
                Ellipse_orange_20.Fill = Functions.IsBitSetTo1(codif_led_value, 19) ? new SolidColorBrush(Color.FromArgb(255, 0xFD, 0XC4, 0X5C)) : new SolidColorBrush(Colors.White);
                Ellipse_orange_21.Fill = Functions.IsBitSetTo1(codif_led_value, 20) ? new SolidColorBrush(Color.FromArgb(255, 0xFD, 0XC4, 0X5C)) : new SolidColorBrush(Colors.White);
                Led_yellow_22.Fill = Functions.IsBitSetTo1(codif_led_value, 21) ? new SolidColorBrush(Colors.Yellow) : new SolidColorBrush(Colors.White);
                Led_yellow_23.Fill = Functions.IsBitSetTo1(codif_led_value, 22) ? new SolidColorBrush(Colors.Yellow) : new SolidColorBrush(Colors.White);
                Led_yellow_24.Fill = Functions.IsBitSetTo1(codif_led_value, 23) ? new SolidColorBrush(Colors.Yellow) : new SolidColorBrush(Colors.White);
                Ellipse_slow_maintennance_25.Fill = Functions.IsBitSetTo1(codif_led_value, 24) ? new SolidColorBrush(Color.FromArgb(255, 0xFD, 0XC4, 0X5C)) : new SolidColorBrush(Colors.White);
                Ellipse_on_maintennance_26.Fill = Functions.IsBitSetTo1(codif_led_value, 25) ? new SolidColorBrush(Colors.LimeGreen) : new SolidColorBrush(Colors.White);
                Ellipse_driver_maintennance_27.Fill = Functions.IsBitSetTo1(codif_led_value, 26) ? new SolidColorBrush(Colors.LightCoral) : new SolidColorBrush(Colors.White);

                #endregion
            }

            #endregion

            #region Audio

            if (Globals.GetTheInstance().Forced_mode_do_controls[(int)FORCE_MODE_CODIF.M_AUDIO_TO_PLAY] == false)
            {
                ushort digital_audio_1_value = Globals.GetTheInstance().SAS360CON_image_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.AUDIO_1];
                Label_digital_audio_1_value.Content = $"0x{digital_audio_1_value:X4}";
                ushort digital_audio_2_value = Globals.GetTheInstance().SAS360CON_image_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.AUDIO_2];
                Label_digital_audio_2_value.Content = $"0x{digital_audio_2_value:X4}";

                const int max_audio = 32;
                for (int index_audio_control = 0; index_audio_control < max_audio; index_audio_control++)
                {
                    if (index_audio_control < max_pos_value)
                        m_list_maintenance_border_audio[index_audio_control].Background = Functions.IsBitSetTo1(digital_audio_1_value, index_audio_control) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);

                    else
                        m_list_maintenance_border_audio[index_audio_control].Background = Functions.IsBitSetTo1(digital_audio_2_value, index_audio_control - max_pos_value) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                }
            }

            #endregion
        }

        private void Update_SAS360CON_image_entradas_sensores()
        {
        }

        private void Update_SAS360CON_image_nvreg_management()
        {
            Label_main_int_cfg_change_count.Content = $"{Globals.GetTheInstance().SAS360CON_image_nvreg_management.Internal_change_counter} / {Globals.GetTheInstance().SAS360CON_image_nvreg_management.Config_con_change_counter}";
            Label_main_iot_nvr_change_count.Content = $"{Globals.GetTheInstance().SAS360CON_image_nvreg_management.Config_iot_change_counter} / {Globals.GetTheInstance().SAS360CON_image_nvreg_management.Nvreg_change_counter}";

            Label_main_event_log_total.Content = Globals.GetTheInstance().SAS360CON_image_nvreg_management.Num_recorded_events_copy;

            Label_maintennance_event_log_last_abs_index.Content = Globals.GetTheInstance().SAS360CON_image_nvreg_management.Last_recorded_event_absolute_index_copy;
            Label_maintennance_event_log_num_recorded.Content = Globals.GetTheInstance().SAS360CON_image_nvreg_management.Num_recorded_events_copy;
            Label_maintennance_event_log_last_recorded_pos.Content = Globals.GetTheInstance().SAS360CON_image_nvreg_management.Last_recorded_event_array_position_copy;

            if (!m_read_events_hist_param)
            {
                DecimalUpDown_sas360con_event_log_read_pos.Maximum = Globals.GetTheInstance().SAS360CON_image_nvreg_management.Last_recorded_event_array_position_copy;
                DecimalUpDown_sas360con_event_log_read_pos.Value = Globals.GetTheInstance().SAS360CON_image_nvreg_management.Last_recorded_event_array_position_copy;

                DecimalUpDown_sas360con_event_log_num.Maximum = Globals.GetTheInstance().SAS360CON_image_nvreg_management.Num_recorded_events_copy;
                DecimalUpDown_sas360con_event_log_num.Value = Globals.GetTheInstance().SAS360CON_image_nvreg_management.Num_recorded_events_copy;
                m_read_events_hist_param = true;
            }
        }

        private void Update_SAS360CON_image_main_management()
        {
            Label_main_error_code.Content = $"0X{Globals.GetTheInstance().SAS360CON_image_main_management.Internal_error:X4}";
            Label_main_war_code.Content = $"0X{Globals.GetTheInstance().SAS360CON_image_main_management.Active_warning_id:X4}";

            Label_main_event_log_last_id.Content = Globals.GetTheInstance().SAS360CON_image_main_management.Last_event_log_value[0];
            Label_main_hist_log_last_id.Content = Globals.GetTheInstance().SAS360CON_image_main_management.Last_event_log_value[1];

        }

        private void Update_SAS360CON_image_lin_pooling()
        {
            Label_main_pooling_state_uwb.Content = $"0X{Globals.GetTheInstance().SAS360CON_image_lin_pooling.Lin_pooling_state:X4}";
            Label_main_pooling_broadcast.Content = $"0X{Globals.GetTheInstance().SAS360CON_image_lin_pooling.Lin_pooling_write_broadcast:X4}";

            Label_main_pooled_uwb.Content = $"0X{Globals.GetTheInstance().SAS360CON_image_lin_pooling.Actual_pooled_uwb:X4}";
            Label_main_pooled_group.Content = $"0X{Globals.GetTheInstance().SAS360CON_image_lin_pooling.Actual_pooling_request_group:X4}";
            Label_main_pooled_index.Content = $"0X{Globals.GetTheInstance().SAS360CON_image_lin_pooling.Actual_pooling_request_index:X4}";


            for (int index = 0; index < Constants.UWB_TOTAL_COUNT; index++)
            {
                m_list_label_uwb_management_pool_read[index].Content = Globals.GetTheInstance().Array_SAS360CON_UWB[index].Pool_read;
                m_list_label_uwb_management_pool_write[index].Content = Globals.GetTheInstance().Array_SAS360CON_UWB[index].Pool_write;
                m_list_label_uwb_management_com_total[index].Content = Globals.GetTheInstance().Array_SAS360CON_UWB[index].Comm_total;
                m_list_label_uwb_management_com_error[index].Content = Globals.GetTheInstance().Array_SAS360CON_UWB[index].Com_error;
                m_list_label_uwb_management_cycle_time[index].Content = Globals.GetTheInstance().Array_SAS360CON_UWB[index].Cycle_time;
            }
        }

        private void Update_SAS360CON_image_con_processed_tags()
        {
            Label_main_assigned_self_contag_id.Content = $"{Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_assigned_self_contag_id[1]:D5}.{Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_assigned_self_contag_id[0]:D5}";
            Label_main_assigned_self_drvtag_id.Content = $"{Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_assigned_self_drvtag_id[1]:D5}.{Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_assigned_self_drvtag_id[0]:D5}"; ;
            Label_main_zones_slow_range.Content = Globals.GetTheInstance().SAS360CON_image_processed_tag.Number_zones_slow_range;

            for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
            {
                m_list_label_main_total_tags[index].Content = Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_number_total_TAGS_in_area_DANR[index];
                m_list_label_main_total_ped[index].Content = Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_number_total_PED_in_area_DANR[index];
                m_list_label_main_total_drv[index].Content = Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_number_total_DRV_in_area_DANR[index];
                m_list_label_main_total_lv[index].Content = Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_number_total_LV_in_area_DANR[index];
                m_list_label_main_total_hv[index].Content = Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_number_total_HV_in_area_DANR[index];
                m_list_label_main_total_zones[index].Content = Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_number_total_ZONES_in_area_DANR[index];
            }
        }

        #endregion

        #region Update SAS360CON maintennance memory info

        private bool Update_SAS360CON_maintennance_memory_info()
        {
            bool update_ok = false;
            try
            {
                CollectionViewSource.GetDefaultView(Listview_sas360con_maintennance.ItemsSource).Refresh();
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Update_SAS360CON_maintennance_memory_info)} -> {ex.Message}");
            }

            return update_ok;
        }

        #endregion

        #region Update UWB internal cfg info

        private bool Update_UWB_internal_cfg_memory_info()
        {
            bool update_ok = false;
            Globals.GetTheInstance().SAS360con_uwb_internal_config_read = true;

            try
            {
                CollectionViewSource.GetDefaultView(Listview_uwb_internal_cfg.ItemsSource).Refresh();

                update_ok = true;
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Update_UWB_internal_cfg_memory_info)} -> {ex.Message}");
            }

            return update_ok;
        }

        #endregion

        #region Update UWB image memory info

        private bool Update_UWB_image_memory_info()
        {
            bool update_ok = false;
            try
            {
                CollectionViewSource.GetDefaultView(Listview_uwb_image.ItemsSource).Refresh();

                for (int index = 0; index < Constants.UWB_TOTAL_COUNT; index++)
                {
                    bool uwb_ok = Globals.GetTheInstance().Array_SAS360CON_UWB[index].RTC_UTC_value != 0;

                    m_list_label_uwb_name_value[index].Foreground = uwb_ok ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.Red);

                    m_list_label_uwb_image_rtc_value[index].Content = uwb_ok ? Globals.GetTheInstance().Array_SAS360CON_UWB[index].RTC_UTC_value : "-";
                    m_list_label_uwb_image_rtc_milisecs[index].Content = uwb_ok ? Globals.GetTheInstance().Array_SAS360CON_UWB[index].RTC_millisecs : "-";
                    m_list_label_uwb_image_watchdog[index].Content = uwb_ok ? Globals.GetTheInstance().Array_SAS360CON_UWB[index].Watchdog_inc : "-";
                    m_list_label_uwb_image_codif_state[index].Content = uwb_ok ? Globals.GetTheInstance().Array_SAS360CON_UWB[index].Codif_state : "-";
                    m_list_label_uwb_image_num_tags[index].Content = uwb_ok ? Globals.GetTheInstance().Array_SAS360CON_UWB[index].Num_tags : "-";
                    m_list_label_uwb_image_num_zones[index].Content = uwb_ok ? Globals.GetTheInstance().Array_SAS360CON_UWB[index].Num_zones : "-";
                    m_list_label_uwb_image_rreg_value[index].Content = uwb_ok ? Globals.GetTheInstance().Array_SAS360CON_UWB[index].Reported_register : "-";
                    m_list_label_uwb_image_error_id[index].Content = uwb_ok ? Globals.GetTheInstance().Array_SAS360CON_UWB[index].War_error_id : "-";
                }

                update_ok = true;
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Update_UWB_image_memory_info)} -> {ex.Message}");
            }

            return update_ok;
        }

        #endregion


        #region Update CONSOLE closest tag base

        private bool Update_CONSOLE_closest_tags_base_memory_info()
        {
            bool update_ok = false;
            try
            {
                double rectangle_width = Rectangle_sas360_data_draw.ActualWidth;
                double rectangle_height = Rectangle_sas360_data_draw.ActualHeight;

                CollectionViewSource.GetDefaultView(Listview_console_closest_tags_base.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(Listview_tag_processed.ItemsSource).Refresh();

                m_list_ellipse_sas360tag.ForEach(ellipse => ellipse.Visibility = Visibility.Collapsed);
                m_collection_sas360_tag_processed.Clear();

                Globals.GetTheInstance().Array_SAS360CON_TAG.ToList()
                    .Select((item, index) => (item, index)).ToList()
                    .ForEach(sas360tag =>
                    {
                        if (sas360tag.item.ID_2LSB > 0)
                        {
                            m_collection_sas360_tag_processed.Add(sas360tag.item);

                            m_list_ellipse_sas360tag[sas360tag.index].Fill =
                                sas360tag.item.Tag_type == MASK_TAG_ZONE_TYPE.SAS360TAG_PED ? new SolidColorBrush(Color.FromRgb(0xC9, 0xD4, 0x04)) :
                                sas360tag.item.Tag_type == MASK_TAG_ZONE_TYPE.SAS360TAG_DRV ? new SolidColorBrush(Color.FromRgb(0xC1, 0x39, 0xEC)) :
                                sas360tag.item.Tag_type == MASK_TAG_ZONE_TYPE.SAS360CON_TAG_LV ? new SolidColorBrush(Color.FromRgb(0xFF, 0x7E, 0x00)) :
                                sas360tag.item.Tag_type == MASK_TAG_ZONE_TYPE.SAS360CON_TAG_HV ? new SolidColorBrush(Color.FromRgb(0x00, 0x6D, 0xFF)) :
                                sas360tag.item.Tag_type == MASK_TAG_ZONE_TYPE.SAS360ZONE_CIRC_R_SLOW ? new SolidColorBrush(Color.FromRgb(0x01, 0x28, 0x75)) :
                                new SolidColorBrush(Colors.White);

                            double tag_x = rectangle_width * (Math.Abs(sas360tag.item.Calc_tag_position_abs_X_cm) / (double)Globals.GetTheInstance().Panel_area_cm);
                            tag_x = sas360tag.item.Calc_tag_position_abs_X_cm < 0 ? (rectangle_width / 2) - tag_x : (rectangle_width / 2) + tag_x;
                            tag_x -= m_list_ellipse_sas360tag[sas360tag.index].Width / 2;
                            Canvas.SetLeft(m_list_ellipse_sas360tag[sas360tag.index], tag_x);

                            double tag_y = rectangle_width * (Math.Abs(sas360tag.item.Calc_tag_position_abs_Y_cm) / (double)Globals.GetTheInstance().Panel_area_cm);
                            tag_y = sas360tag.item.Calc_tag_position_abs_Y_cm < 0 ? (rectangle_height / 2) + tag_y : (rectangle_height / 2) - tag_y;
                            tag_y -= m_list_ellipse_sas360tag[sas360tag.index].Height / 2;
                            Canvas.SetTop(m_list_ellipse_sas360tag[sas360tag.index], tag_y);

                            m_list_ellipse_sas360tag[sas360tag.index].Visibility = Visibility.Visible;

                        }
                    });

                Refresh_tag_controls();

                update_ok = true;
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Update_CONSOLE_closest_tags_base_memory_info)} -> {ex.Message}");
            }

            return update_ok;

        }

        #endregion

        #region Update CONSOLE closest zone base

        private bool Update_CONSOLE_closest_zone_base_memory_info()
        {
            bool update_ok = false;

            try
            {
                double rectangle_width = Rectangle_sas360_data_draw.ActualWidth;
                double rectangle_height = Rectangle_sas360_data_draw.ActualHeight;

                CollectionViewSource.GetDefaultView(Listview_console_closest_zone_base.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(Listview_zone_processed.ItemsSource).Refresh();

                m_list_grid_sas360zone.ForEach(grid => grid.Visibility = Visibility.Collapsed);
                m_collection_sas360_zone_processed.Clear();

                Globals.GetTheInstance().Array_SAS360CON_ZONE.ToList()
                    .Select((item, index) => (item, index)).ToList()
                    .ForEach(sas360zone =>
                    {
                        if (sas360zone.item.ID_2LSB > 0)
                        {
                            m_collection_sas360_zone_processed.Add(sas360zone.item);

                            m_list_ellipse_int_sas360zone[sas360zone.index].Fill =
                                sas360zone.item.Zone_type == MASK_TAG_ZONE_TYPE.SAS360ZONE_CIRC_R_SLOW ? new SolidColorBrush(Color.FromRgb(0xff, 0x46, 0x00)) :
                                sas360zone.item.Zone_type == MASK_TAG_ZONE_TYPE.SAS360ZONE_REC_P1_SLOW ? new SolidColorBrush(Color.FromRgb(0xff, 0x7e, 0x00)) :
                                sas360zone.item.Zone_type == MASK_TAG_ZONE_TYPE.SAS360ZONE_REC_P2_SLOW ? new SolidColorBrush(Color.FromRgb(0xff, 0xfc, 0x00)) :
                                sas360zone.item.Zone_type == MASK_TAG_ZONE_TYPE.SAS360ZONE_REC_P3_SLOW ? new SolidColorBrush(Color.FromRgb(0x8c, 0xff, 0x00)) :
                                sas360zone.item.Zone_type == MASK_TAG_ZONE_TYPE.SAS360ZONE_REC_P4_SLOW ? new SolidColorBrush(Color.FromRgb(0x06, 0xff, 0x00)) :
                                sas360zone.item.Zone_type == MASK_TAG_ZONE_TYPE.SAS360ZONE_INHIBIT_RAD ? new SolidColorBrush(Color.FromRgb(0x00, 0xff, 0xb3)) :
                                new SolidColorBrush(Colors.White);

                            double zone_radio = rectangle_width * ((sas360zone.item.Radio_action) / (double)Globals.GetTheInstance().Panel_area_cm);
                            m_list_ellipse_ext_sas360zone[sas360zone.index].Height = zone_radio * 2;
                            m_list_ellipse_ext_sas360zone[sas360zone.index].Width = zone_radio * 2;


                            double zone_x = rectangle_width * (Math.Abs(sas360zone.item.Calc_zone_position_abs_X_cm) / (double)Globals.GetTheInstance().Panel_area_cm);
                            zone_x = sas360zone.item.Calc_zone_position_abs_X_cm < 0 ? (rectangle_width / 2) - zone_x : (rectangle_width / 2) + zone_x;
                            zone_x -= zone_radio / 2;
                            Canvas.SetLeft(m_list_grid_sas360zone[sas360zone.index], zone_x);

                            double zone_y = rectangle_width * (Math.Abs(sas360zone.item.Calc_zone_position_abs_Y_cm) / (double)Globals.GetTheInstance().Panel_area_cm);
                            zone_y = sas360zone.item.Calc_zone_position_abs_Y_cm < 0 ? (rectangle_height / 2) + zone_y : (rectangle_height / 2) - zone_y;
                            zone_y -= zone_radio / 2;
                            Canvas.SetTop(m_list_grid_sas360zone[sas360zone.index], zone_y);


                            m_list_grid_sas360zone[sas360zone.index].Visibility = Visibility.Visible;
                        }
                    });

                Refresh_zone_controls();

                update_ok = true;
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Update_CONSOLE_closest_zone_base_memory_info)} -> {ex.Message}");
            }

            return update_ok;
        }

        #endregion


        #region Update SAS360CON internal cfg memory info

        private bool Update_SAS360CON_nvreg_memory_info()
        {
            bool update_ok = false;

            try
            {
                CollectionViewSource.GetDefaultView(Listview_sas360con_nvreg.ItemsSource).Refresh();
                update_ok = true;
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Update_SAS360CON_nvreg_memory_info)} -> {ex.Message}");
            }

            return update_ok;
        }

        #endregion


        #region Update commands info

        private bool Update_SAS360CON_commands_info(List<ushort> list_data)
        {
            bool update_ok = false;

            try
            {
                List<int> list_i_data = new();
                list_data.ForEach(i_value =>
                {
                    byte[] array_value = BitConverter.GetBytes(i_value).ToArray();
                    list_i_data.Add(BitConverter.ToUInt16(array_value));
                });

                Globals.GetTheInstance().List_last_command_receive_data = list_i_data;
                string s_data = string.Empty;
                Globals.GetTheInstance().List_last_command_receive_data.ForEach(data => s_data += $"0X{data:X4} ");
                Label_last_received_command.Content = s_data;

                bool check_command_ok = Globals.GetTheInstance().List_last_command_send_data.SequenceEqual(Globals.GetTheInstance().List_last_command_receive_data);

                if (check_command_ok)
                {
                    Globals.GetTheInstance().SAS360con_internal_config_read = false;
                    Globals.GetTheInstance().SAS360iot_config_read = false;
                    Globals.GetTheInstance().SAS360con_config_read = false;
                    Globals.GetTheInstance().SAS360con_uwb_internal_config_read = false;
                }

                switch (m_selected_command_write_location)
                {
                    case COMMAND_WRITE_LOCATION.MAINTENNANCE:
                        {
                            Image_receive_general_command_ok.Visibility = check_command_ok ? Visibility.Visible : Visibility.Collapsed;
                            Image_receive_general_command_error.Visibility = check_command_ok ? Visibility.Collapsed : Visibility.Visible;
                            break;
                        }

                    case COMMAND_WRITE_LOCATION.INTERNAL_CONFIG:
                        {
                            Image_internal_command_ok.Visibility = check_command_ok ? Visibility.Visible : Visibility.Collapsed;
                            Image_internal_command_error.Visibility = check_command_ok ? Visibility.Collapsed : Visibility.Visible;
                            break;
                        }
                }

                m_is_writing = false;
                m_timer_write_timeout.Stop();
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Update_SAS360CON_commands_info)} -> {ex.Message}");
            }

            return update_ok;
        }

        #endregion


        #region Update SAS360CON event log memory info

        private bool Update_SAS360CON_event_log_memory_info(List<ushort> list_data)
        {
            bool update_ok = false;

            try
            {
                Manage_logs.SaveModbusValue($"RECEIVED HOLDING REGISTER ({MEMORY_CONFIG_TYPE.SAS360CON_EVENT_LOG}) -> INI: {m_pos_modbus_read_event_hist} / COUNT: {Constants.EVENT_LOG_REG_READ_MODBUS}");
                Manage_logs.SaveModbusValue($"***********************************************************");

                //--- Para habilitar siguiente peticion ---
                m_received_resp_event_hist_timeout = true;
                m_pos_modbus_read_event_hist--;
                if (m_pos_modbus_read_event_hist < 0)
                {
                    m_pos_modbus_read_event_hist = (ushort)(Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_RECORD_EVENTS] + Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_RECORD_EVENTS]);
                }

                m_num_modbus_read_event_hist--;
                //-----------------------------------------

                List<Event_log> list_event_log = new();

                int index_list = 0;
                while (index_list < list_data.Count)
                {
                    Event_log event_log = new();

                    ushort[] array_ushort_absolute_id = new ushort[] { list_data[index_list], list_data[index_list + 1] };
                    byte[] array_byte_absolute_id_1 = BitConverter.GetBytes(array_ushort_absolute_id[0]);
                    byte[] array_byte_absolute_id_2 = BitConverter.GetBytes(array_ushort_absolute_id[1]);

                    byte[] array_byte_absolute_id_concat = array_byte_absolute_id_1.Concat(array_byte_absolute_id_2).ToArray();
                    event_log.Absolute_event_index = BitConverter.ToUInt32(array_byte_absolute_id_concat);

                    index_list += 2;

                    ushort[] array_ushort_log_rtc = new ushort[] { list_data[index_list], list_data[index_list + 1] };
                    byte[] array_byte_log_rtc_1 = BitConverter.GetBytes(array_ushort_log_rtc[0]);
                    byte[] array_byte_log_rtc_2 = BitConverter.GetBytes(array_ushort_log_rtc[1]);

                    byte[] array_byte_log_rtc_concat = array_byte_log_rtc_1.Concat(array_byte_log_rtc_2).ToArray();
                    uint uint_log_rtc_value = BitConverter.ToUInt32(array_byte_log_rtc_concat);
                    event_log.Log_rtc_value = $"{Constants.date_ref.Date.AddSeconds(uint_log_rtc_value):dd/MM/yyyy HH:mm:ss}";

                    index_list += 2;

                    event_log.Log_rtc_milisecs = short.Parse(list_data[index_list].ToString("X"), NumberStyles.HexNumber);

                    index_list++;

                    event_log.Event_id = short.Parse(list_data[index_list].ToString("X"), NumberStyles.HexNumber);

                    index_list++;

                    event_log.Val1 = short.Parse(list_data[index_list].ToString("X"), NumberStyles.HexNumber);

                    index_list++;

                    event_log.Val2 = short.Parse(list_data[index_list].ToString("X"), NumberStyles.HexNumber);

                    index_list++;

                    event_log.Val3 = short.Parse(list_data[index_list].ToString("X"), NumberStyles.HexNumber);

                    index_list++;

                    event_log.Val4 = short.Parse(list_data[index_list].ToString("X"), NumberStyles.HexNumber);

                    index_list++;

                    event_log.Val5 = short.Parse(list_data[index_list].ToString("X"), NumberStyles.HexNumber);

                    index_list++;

                    event_log.Val6 = short.Parse(list_data[index_list].ToString("X"), NumberStyles.HexNumber);

                    index_list++;

                    event_log.Val7 = short.Parse(list_data[index_list].ToString("X"), NumberStyles.HexNumber);

                    index_list++;

                    event_log.Val8 = short.Parse(list_data[index_list].ToString("X"), NumberStyles.HexNumber);

                    index_list++;

                    ushort[] array_ushort_absolute_id_copy = new ushort[] { list_data[index_list], list_data[index_list + 1] };
                    byte[] array_byte_absolute_id_copy_1 = BitConverter.GetBytes(array_ushort_absolute_id_copy[0]);
                    byte[] array_byte_absolute_id_copy_2 = BitConverter.GetBytes(array_ushort_absolute_id_copy[1]);

                    byte[] array_byte_absolute_id_copy_concat = array_byte_absolute_id_copy_1.Concat(array_byte_absolute_id_copy_2).ToArray();
                    event_log.Absolute_event_index_copy = BitConverter.ToUInt32(array_byte_absolute_id_copy_concat);

                    index_list += 2;

                    list_event_log.Add(event_log);
                }

                //La primera vez eliminamos los registros de eventos sobrantes
                if (m_empty_reg_event_hist > 0)
                {
                    list_event_log = list_event_log.GetRange(0, m_empty_reg_event_hist - 1);
                    m_empty_reg_event_hist = 0;
                }

                list_event_log.Reverse();

                list_event_log.ForEach(event_log => Globals.GetTheInstance().List_sas360con_event_log.Add(event_log));

                update_ok = true;
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Update_SAS360CON_event_log_memory_info)} -> {ex.Message}");
            }

            return update_ok;
        }

        #endregion


        #region Update SAS360CON hist log memory info

        private bool Update_SAS360CON_hist_log_memory_info(List<ushort> list_data)
        {
            bool update_ok = false;

            try
            {
                Manage_logs.SaveModbusValue($"RECEIVED HOLDING REGISTER ({MEMORY_CONFIG_TYPE.SAS360CON_HIST_LOG}) -> INI: {m_pos_modbus_read_event_hist} / COUNT: {Constants.HIST_LOG_REG_READ_MODBUS}");
                Manage_logs.SaveModbusValue($"***********************************************************");

                update_ok = true;
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Update_SAS360CON_hist_log_memory_info)} -> {ex.Message}");
            }

            return update_ok;
        }

        #endregion

        #endregion

        #region Load lists

        private void Load_lists()
        {
            try
            {
                #region Load lists from memory

                Globals.GetTheInstance().List_sas360con_internal_cfg = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.SAS360CON_INTERNAL_CFG);

                Globals.GetTheInstance().List_sas360con_cfg = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.SAS360CON_CFG);
                ToggleButton_sas360con_cfg_all.IsChecked = true;
                Globals.GetTheInstance().List_sas360con_cfg_filter.Clear();
                Globals.GetTheInstance().List_sas360con_cfg.ForEach(modbus_var => Globals.GetTheInstance().List_sas360con_cfg_filter.Add(modbus_var));

                Globals.GetTheInstance().List_iot_cfg = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.IOT_CFG);

                Globals.GetTheInstance().List_sas360con_image = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.SAS360CON_IMAGE);
                ToggleButton_sas360con_image_all.IsChecked = true;
                Globals.GetTheInstance().List_sas360con_image_filter.Clear();
                Globals.GetTheInstance().List_sas360con_image.ForEach(modbus_var => Globals.GetTheInstance().List_sas360con_image_filter.Add(modbus_var));

                Globals.GetTheInstance().List_iot_image = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.IOT_IMAGE);
                Globals.GetTheInstance().List_sas360con_maintennance = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.SAS360CON_MAINTENNANCE);

                Globals.GetTheInstance().List_uwb_internal_cfg = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.UWB_INTERNAL_CFG);
                Globals.GetTheInstance().List_uwb_image = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.UWB_IMAGE);

                Globals.GetTheInstance().List_console_closest_tags_base = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE_1);
                Globals.GetTheInstance().List_console_closest_tags_extended = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_EXTENDED);
                Globals.GetTheInstance().List_uwb_closest_tags_base = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_BASE);
                Globals.GetTheInstance().List_uwb_closest_tags_extended = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_EXTENDED);

                Globals.GetTheInstance().List_console_closest_zone_base = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_BASE_1);
                Globals.GetTheInstance().List_console_closest_zone_extended = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_EXTENDED);

                Globals.GetTheInstance().List_sas360con_nvreg = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.SAS360CON_NVREG);

                Globals.GetTheInstance().List_sas360con_commands = Manage_memory.Load_memory_commands();
                Globals.GetTheInstance().List_sas360con_commands.ForEach(commands =>
                {
                    commands.List_param = new();
                    commands.List_type = new();
                    if (commands.Param1 != string.Empty)
                        commands.List_param.Add(commands.Param1);
                    if (commands.Param2 != string.Empty)
                        commands.List_param.Add(commands.Param2);
                    if (commands.Param3 != string.Empty)
                        commands.List_param.Add(commands.Param3);
                    if (commands.Param4 != string.Empty)
                        commands.List_param.Add(commands.Param4);
                    if (commands.Param5 != string.Empty)
                        commands.List_param.Add(commands.Param5);
                    if (commands.Param6 != string.Empty)
                        commands.List_param.Add(commands.Param6);
                    if (commands.Param7 != string.Empty)
                        commands.List_param.Add(commands.Param7);
                    if (commands.Param8 != string.Empty)
                        commands.List_param.Add(commands.Param8);
                    if (commands.Param9 != string.Empty)
                        commands.List_param.Add(commands.Param9);
                });

                Globals.GetTheInstance().List_sas360con_event_log = new();
                Globals.GetTheInstance().List_sas360con_hist_log = new();

                #endregion

                Manage_memory.List_memory_fields_type_pos();

                #region Refresh list sources

                Refresh_list_closest_tags_zones_main();

                Refresh_list_SAS360CON_internal_cfg();
                Refresh_list_SAS360CON_cfg();
                Refresh_list_iot_cfg();
                Refresh_list_uwb_internal_cfg();

                Refresh_list_SAS360CON_image();
                Refresh_list_SAS360IOT_image();
                Refresh_list_SAS360UWB_image();

                Refresh_list_SAS360CON_maintennance();
                Refresh_list_SAS360CON_nvreg();
                Refresh_list_SAS360CON_commands();

                Refresh_list_SAS360CON_event_log();
                Refresh_list_SAS360CON_hist_log();

                Refresh_list_closest_tags_base();
                Refresh_list_closest_tags_extended();

                Refresh_list_closest_zones_base();
                Refresh_list_closest_zones_extended();

                Refresh_list_uwb_tags_base();
                Refresh_list_uwb_tags_extended();

                #endregion
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Load_lists)} -> {ex.Message}");
            }
        }

        #endregion

        #endregion


        #region Timer write timeout

        private void Timer_write_timeout_Tick(object sender, EventArgs e)
        {
            Manage_logs.SaveLogValue($"WRITE TIMEOUT -> {m_selected_command_write_location}");

            m_timer_write_timeout.Stop();
            m_is_writing = false;

            Dispatcher.Invoke(() =>
            {
                switch (m_selected_command_write_location)
                {
                    case COMMAND_WRITE_LOCATION.MAINTENNANCE:
                        {
                            Image_send_general_command_error.Visibility = Visibility.Visible;
                            break;
                        }
                    case COMMAND_WRITE_LOCATION.RTC_UTC:
                        {

                            break;
                        }
                    case COMMAND_WRITE_LOCATION.REPORTED_REGISTER:
                        {
                            Image_main_reported_register_error.Visibility = Visibility.Visible;
                            break;
                        }

                    case COMMAND_WRITE_LOCATION.INTERNAL_CONFIG:
                        {
                            Image_internal_command_error.Visibility = Visibility.Visible;
                            break;
                        }
                    case COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_INSTALLATION:
                        {
                            Image_SAS360CON_CFG_installation_error.Visibility = Visibility.Visible;
                            break;
                        }
                    case COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_VEHICLE_CONFIG:
                        {
                            Image_SAS360CON_CFG_vehicle_config_error.Visibility = Visibility.Visible;
                            break;
                        }
                    case COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_DETECTION_AREA:
                        {
                            Image_SAS360CON_CFG_detection_area_error.Visibility = Visibility.Visible;
                            break;
                        }
                    case COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_UWB_COMM:
                        {
                            Image_SAS360CON_CFG_uwb_com_config_error.Visibility = Visibility.Visible;
                            break;
                        }
                    case COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_MISCELLANEOUS:
                        {
                            Image_SAS360CON_CFG_miscellaneous_error.Visibility = Visibility.Visible;
                            break;
                        }
                }
            });

        }

        #endregion



        #region STATE TAB

        #region RTC UTC change

        private void Border_rtc_utc_date_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2 && Globals.GetTheInstance().ManageComThread.Is_connected)
            {
                Image_main_utc_change_warning.Visibility = Visibility.Collapsed;
                Image_main_utc_change_error.Visibility = Visibility.Collapsed;

                EditDateTimeWindow edit_date = new()
                {
                    Name_var = "RTC VALUE UTC",
                    Value_var = Constants.date_ref.AddSeconds(Globals.GetTheInstance().SAS360CON_image_general.RTC_UTC_seconds)
                };
                edit_date.ShowDialog();
                if (edit_date.Save_changes)
                {
                    try
                    {
                        int[] array_values = new int[Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_COMMAND]];
                        m_selected_modbus_command = Globals.GetTheInstance().List_sas360con_commands.First(command => command.Name.Equals("CMD_SET_UTC_HOUR_U16"));
                        array_values[0] = m_selected_modbus_command.Index;
                        array_values[1] = m_command_watchdog++;

                        uint seconds = (uint)edit_date.Value_var.Subtract(Constants.date_ref).TotalSeconds;
                        byte[] array_seconds_value = BitConverter.GetBytes(seconds);
                        array_values[2] = BitConverter.ToUInt16(array_seconds_value, 0);
                        array_values[3] = BitConverter.ToUInt16(array_seconds_value, 2);
                        array_values[4] = 0;


                        Send_command(array_values, COMMAND_WRITE_LOCATION.RTC_UTC);
                    }
                    catch
                    {
                        Image_main_utc_change_error.Visibility = Visibility.Visible;
                    }
                }
            }
        }


        #region Send current date

        private void Button_send_current_datetime_Click(object sender, RoutedEventArgs e)
        {
            uint u32_rtc_utc = (uint)DateTime.Now.Subtract(Constants.date_ref).TotalSeconds;
            try
            {
                if (Globals.GetTheInstance().ManageComThread.Is_connected)
                {
                    int[] array_values = new int[Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_COMMAND]];
                    m_selected_modbus_command = Globals.GetTheInstance().List_sas360con_commands.First(command => command.Name.Equals("CMD_SET_UTC_HOUR_U16"));
                    array_values[0] = m_selected_modbus_command.Index;
                    array_values[1] = m_command_watchdog++;

                    byte[] array_seconds_value = BitConverter.GetBytes(u32_rtc_utc);
                    array_values[2] = BitConverter.ToUInt16(array_seconds_value, 0);
                    array_values[3] = BitConverter.ToUInt16(array_seconds_value, 2);
                    array_values[4] = 0;

                    Send_command(array_values, COMMAND_WRITE_LOCATION.RTC_UTC);

                }
            }
            catch
            {
                Image_main_utc_change_error.Visibility = Visibility.Visible;
            }
        }

        #endregion


        #endregion

        #region Listview selected

        #region Listview uwb selection changed

        private void Listview_sas360_uwb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        #endregion

        #region Listview uwb double click

        private void Listview_sas360_uwb_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        #endregion

        #region Tags

        #region Listview tag selection changed

        private void Listview_tag_processed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Listview_tag_processed.SelectedItems.Count > 0)
            {
                if (Listview_tag_processed.SelectedItem is SAS360CON_TAG)
                {
                    m_selected_tag = Listview_tag_processed.SelectedItem as SAS360CON_TAG;
                    Refresh_tag_controls();
                }
            }
        }

        #endregion

        #region Listview tag double click

        private void Listview_tag_processed_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        }

        #endregion

        #region Refresh tag controls

        private void Refresh_tag_controls()
        {
            if (m_selected_tag != null)
            {
                int selected_index = Globals.GetTheInstance().Array_SAS360CON_TAG.ToList().FindIndex(tag => tag.ID_2LSB == m_selected_tag.ID_2LSB);

                Listview_tag_processed.SelectedIndex = selected_index; //List

                m_list_ellipse_sas360tag
                .Select((value, index) => (value, index)).ToList()
                .ForEach(ellipse => ellipse.value.StrokeThickness = ellipse.index != selected_index ? 1 : 8);
            }
        }

        #endregion

        #endregion

        #region Zones

        #region Listview zone selection changed

        private void Listview_zone_processed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Listview_zone_processed.SelectedItems.Count > 0)
            {
                if (Listview_zone_processed.SelectedItem is SAS360CON_ZONE)
                {
                    m_selected_zone = Listview_zone_processed.SelectedItem as SAS360CON_ZONE;
                    Refresh_zone_controls();
                }
            }
        }

        #endregion

        #region Listview zone double click

        private void Listview_zone_processed_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        }

        #endregion

        #region Refresh zone controls

        private void Refresh_zone_controls()
        {
            if (m_selected_zone != null)
            {
                int selected_index = Globals.GetTheInstance().Array_SAS360CON_ZONE.ToList().FindIndex(zone => zone.ID_2LSB == m_selected_zone.ID_2LSB);

                Listview_zone_processed.SelectedIndex = selected_index; //List

                //if (Globals.GetTheInstance().Draw_map == BIT_STATE.ON)
                //{
                m_list_ellipse_int_sas360zone
                .Select((value, index) => (value, index)).ToList()
                .ForEach(ellipse => ellipse.value.StrokeThickness = ellipse.index != selected_index ? 1 : 8);
                //}
            }
        }

        #endregion

        #endregion

        #endregion


        #region Ellipse associated with SAS360 TAG Mouse events

        private void EllipseTagOnMouseEnter(object sender, MouseEventArgs e)
        {
            int index_sas360tag = Array.IndexOf(m_list_ellipse_sas360tag.ToArray(), sender);
            if (index_sas360tag != Constants.index_no_selected)
            {
                SAS360CON_TAG sas360_tag = Globals.GetTheInstance().Array_SAS360CON_TAG[index_sas360tag];
                string ID = sas360_tag.ID_2LSB.ToString();
                string tag_type = Manage_memory.SAS360TAG_ZONE_TYPE(sas360_tag.Tag_type);

                string pos_x = $"{decimal.Divide(sas360_tag.Calc_tag_position_abs_X_cm, 100)} M";
                string pos_y = $"{decimal.Divide(sas360_tag.Calc_tag_position_abs_Y_cm, 100)} M";

                Label_popup_id.Content = $"ID : {ID}";
                Label_popup_type.Content = tag_type;

                Label_popup_tag_zone.Content = "TAG";
                bool b_tag_ok = (sas360_tag.Estado_codificado & 0x01) == 0x01;
                Image_popup_tag_ok.Visibility = b_tag_ok ? Visibility.Visible : Visibility.Collapsed;
                Image_popup_tag_error.Visibility = !b_tag_ok ? Visibility.Visible : Visibility.Collapsed;

                Wrappanel_popup_battery_state.Visibility = Visibility.Visible;
                bool b_battery_ok = (sas360_tag.Estado_codificado & 0x04) == 0x04;
                Image_popup_battery_ok.Visibility = b_battery_ok ? Visibility.Visible : Visibility.Collapsed;
                Image_popup_battery_error.Visibility = !b_battery_ok ? Visibility.Visible : Visibility.Collapsed;

                Label_popup_pos_x.Content = $"POS X : {pos_x}";
                Label_popup_pos_y.Content = $"POS Y : {pos_y}";

                Wrappanel_popup_zone_radio_accion.Visibility = Visibility.Collapsed;

                Popup_Sas360TagZone.IsOpen = true;
            }
        }

        private void EllipseTagOnMouseLeave(object sender, MouseEventArgs e)
        {
            Popup_Sas360TagZone.IsOpen = false;
        }

        private void EllipseTagMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int index_sas360tag = Array.IndexOf(m_list_ellipse_sas360tag.ToArray(), sender);
            m_selected_tag = Globals.GetTheInstance().Array_SAS360CON_TAG[index_sas360tag];

            Refresh_tag_controls();
        }

        #endregion


        #region Ellipse associated with SAS360 ZONE Mouse events

        private void EllipseZoneOnMouseEnter(object sender, MouseEventArgs e)
        {
            int index_sas360zone = Array.IndexOf(m_list_ellipse_int_sas360zone.ToArray(), sender);
            if (index_sas360zone != Constants.index_no_selected)
            {
                SAS360CON_ZONE sas360_zone = Globals.GetTheInstance().Array_SAS360CON_ZONE[index_sas360zone];
                string ID = sas360_zone.ID_2LSB.ToString();
                string zone_type = Manage_memory.SAS360TAG_ZONE_TYPE(sas360_zone.Zone_type);

                string pos_x = $"{decimal.Divide(sas360_zone.Calc_zone_position_abs_X_cm, 100)} M";
                string pos_y = $"{decimal.Divide(sas360_zone.Calc_zone_position_abs_Y_cm, 100)} M";

                Label_popup_id.Content = $"ID : {ID}";
                Label_popup_type.Content = zone_type;

                Label_popup_tag_zone.Content = "ZONE";
                bool b_zone_ok = (sas360_zone.Estado_codificado & 0x01) == 0x01;
                Image_popup_tag_ok.Visibility = b_zone_ok ? Visibility.Visible : Visibility.Collapsed;
                Image_popup_tag_error.Visibility = !b_zone_ok ? Visibility.Visible : Visibility.Collapsed;

                Wrappanel_popup_battery_state.Visibility = Visibility.Collapsed;

                Label_popup_pos_x.Content = $"POS X : {pos_x}";
                Label_popup_pos_y.Content = $"POS Y : {pos_y}";

                Wrappanel_popup_zone_radio_accion.Visibility = Visibility.Visible;
                Label_zone_radio_accion.Content = $"RADIO ACCION : {sas360_zone.Radio_action / 100} M";

                Popup_Sas360TagZone.IsOpen = true;
            }
        }

        private void EllipseZoneOnMouseLeave(object sender, MouseEventArgs e)
        {
            Popup_Sas360TagZone.IsOpen = false;
        }

        private void EllipseZoneMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int index_sas360zone = Array.IndexOf(m_list_ellipse_int_sas360zone.ToArray(), sender);
            m_selected_zone = Globals.GetTheInstance().Array_SAS360CON_ZONE[index_sas360zone];

            Refresh_zone_controls();

        }

        #endregion


        #region Label info mouse events
        private void LabelInfoOnMouseEnter(object sender, MouseEventArgs e)
        {

            int index = Array.IndexOf(m_list_label_popup_image_info.ToArray(), sender);

            if (index == (int)LABEL_POPUP_IMAGE_READ.INPUT && Globals.GetTheInstance().SAS360CON_image_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.INPUT] != 0x00)
            {
                Textblock_image_info.Text = Manage_memory.SAS360CON_DI1();
                Popup_image_info.IsOpen = true;
            }

            if (index == (int)LABEL_POPUP_IMAGE_READ.OUTPUT_INT && Globals.GetTheInstance().SAS360CON_image_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.OUTPUT_1_INT] != 0x00)
            {
                Textblock_image_info.Text = Manage_memory.SAS360CON_DO1();
                Popup_image_info.IsOpen = true;
            }

            if (index == (int)LABEL_POPUP_IMAGE_READ.OUTPUT_EXT && Globals.GetTheInstance().SAS360CON_image_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.OUTPUT_2_EXT] != 0x00)
            {
                Textblock_image_info.Text = Manage_memory.SAS360CON_DO2();
                Popup_image_info.IsOpen = true;
            }

            if (index == (int)LABEL_POPUP_IMAGE_READ.OUTPUT_LED && Globals.GetTheInstance().SAS360CON_image_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.OUTPUT_3_LED] != 0x00)
            {
                Textblock_image_info.Text = Manage_memory.SAS360CON_DO3();
                Popup_image_info.IsOpen = true;
            }


            if (index == (int)LABEL_POPUP_IMAGE_READ.INTERNAL_ERROR && Globals.GetTheInstance().SAS360CON_image_main_management.Internal_error != 0x00)
            {
                Textblock_image_info.Text = Manage_memory.SAS360CON_internal_error();
                Popup_image_info.IsOpen = true;
            }
            else if (index == (int)LABEL_POPUP_IMAGE_READ.LIN_POOLING_READ && Globals.GetTheInstance().SAS360CON_image_lin_pooling.Array_lin_pooling_read_uwb[0] != 0x00)
            {
                Textblock_image_info.Text = Manage_memory.SAS360CON_lin_read_pool();
                Popup_image_info.IsOpen = true;
            }
            else if (index == (int)LABEL_POPUP_IMAGE_READ.LIN_POOLING_WRITE && Globals.GetTheInstance().SAS360CON_image_lin_pooling.Array_lin_pooling_write_uwb[0] != 0x00)
            {
                Textblock_image_info.Text = Manage_memory.SAS360CON_lin_write_pool();
                Popup_image_info.IsOpen = true;
            }
            else if (index == (int)LABEL_POPUP_IMAGE_READ.LIN_POOLING_WRITE_BROADCAST && Globals.GetTheInstance().SAS360CON_image_lin_pooling.Lin_pooling_write_broadcast != 0x00)
            {
                Textblock_image_info.Text = Manage_memory.SAS360CON_lin_write_pool_broadcast();
                Popup_image_info.IsOpen = true;
            }
            else if (index == (int)LABEL_POPUP_IMAGE_READ.LIN_POOLING_STATE && Globals.GetTheInstance().SAS360CON_image_lin_pooling.Lin_pooling_state != 0x00)
            {
                Textblock_image_info.Text = Manage_memory.SAS360CON_lin_pool_state();
                Popup_image_info.IsOpen = true;
            }
        }

        private void LabelInfoOnMouseLeave(object sender, MouseEventArgs e)
        {
            Popup_image_info.IsOpen = false;
        }

        #endregion

        #region Send command reported register

        private void Button_send_reported_register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Image_main_reported_register_warning.Visibility = Visibility.Hidden;
                Image_main_reported_register_error.Visibility = Visibility.Hidden;
                int[] array_values = new int[Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_COMMAND]];
                array_values[0] = Globals.GetTheInstance().List_sas360con_commands.First(command => command.Name.Equals("CMD_MANTENI_SET_UWB_REPORTED_REGISTER_INDEX_U16")).Index;
                array_values[1] = m_command_watchdog++;
                array_values[2] = (int)DecimalUpDown_reported_register.Value!;

                Send_command(array_values, COMMAND_WRITE_LOCATION.REPORTED_REGISTER);
            }
            catch
            {
                Image_main_reported_register_error.Visibility = Visibility.Visible;
            }
        }

        #endregion

        #endregion

        #region CONFIG TAB

        #region Edit SAS360CON internal cfg
        private void Button_edit_internal_config(object sender, RoutedEventArgs e)
        {
            if (!m_is_writing && !m_is_writing_all_config && !m_is_reading_event_hist)
            {
                int index = Array.IndexOf(m_list_button_edit_internal_config.ToArray(), sender as Button);
                string? s_name_var = m_list_label_edit_internal_config_title[index].Content.ToString();

                Wrappanel_internal_config_send_state.Visibility = Visibility.Collapsed;

                EditInternalConfigWindow edit_internal = new()
                {
                    Name_internal_config = s_name_var![..^1],
                    Control_pos = (BUTTON_EDIT_INTERNAL_CONFIG_POS)index
                };
                edit_internal.ShowDialog();
                if (edit_internal.Save_changes)
                {
                    Image_internal_command_ok.Visibility = Visibility.Collapsed;
                    Image_internal_command_error.Visibility = Visibility.Collapsed;
                    Image_internal_command_warning.Visibility = Visibility.Collapsed;
                    Wrappanel_internal_config_send_state.Visibility = Visibility.Visible;

                    m_selected_modbus_command = edit_internal.Modbus_command;

                    int[] array_values = new int[Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_COMMAND]];
                    array_values[0] = m_selected_modbus_command.Index;
                    array_values[1] = m_command_watchdog++;

                    int index_param = 2;
                    edit_internal.List_values.ForEach(value =>
                    {
                        array_values[index_param] = value;
                        index_param++;
                    });

                    Send_command(array_values, COMMAND_WRITE_LOCATION.INTERNAL_CONFIG);
                }
            }
        }

        #endregion

        #region Edit SAS360CON config

        #region SAS360CON config submenu selecion

        private void TabControl_memory_config_submenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabControl_memory_config_submenu.SelectedIndex == (int)CONFIG_MENU_TAB.SAS360CON_CFG)
            {
            }
        }

        #endregion


        #region Edit Installation

        #region CHECK - UNCHECK

        private void Checkbox_edit_SAS360CON_CFG_installation_Checked(object sender, RoutedEventArgs e)
        {
            m_list_value_sas360con_cfg_installation.Clear();

            m_list_textbox_sas360con_cfg_installation.ForEach(edit =>
            {
                edit.BorderThickness = new Thickness(1, 1, 2, 2);
                edit.Background = new SolidColorBrush(Colors.White);
                edit.Foreground = new SolidColorBrush(Color.FromArgb(255, 59, 84, 101));
                edit.IsEnabled = true;
                m_list_value_sas360con_cfg_installation.Add(edit.Text);
            });
            DecimalUpDown_audio_language.Foreground = new SolidColorBrush(Colors.Black);
            DecimalUpDown_audio_language.IsEnabled = true;
            m_list_value_sas360con_cfg_installation.Add(DecimalUpDown_audio_language.Value.ToString()!);

            DecimalUpDown_audio_volume.Foreground = new SolidColorBrush(Colors.Black);
            DecimalUpDown_audio_volume.IsEnabled = true;
            m_list_value_sas360con_cfg_installation.Add(DecimalUpDown_audio_volume.Value.ToString()!);

            Image_SAS360CON_CFG_installation_ok.Visibility = Visibility.Collapsed;
            Image_SAS360CON_CFG_installation_error.Visibility = Visibility.Collapsed;
            Image_SAS360CON_CFG_installation_warning.Visibility = Visibility.Collapsed;

            Button_send_SAS360CON_CFG_installation.Visibility = Visibility.Visible;
        }

        private void Checkbox_edit_SAS360CON_CFG_installation_Unchecked(object sender, RoutedEventArgs e)
        {
            #region Valores provisionales

            for (int index = 0; index < 3; index++)
                m_list_textbox_sas360con_cfg_installation[index].Text = m_list_value_sas360con_cfg_installation[index];

            if (decimal.TryParse(m_list_value_sas360con_cfg_installation[3], out decimal dec_value))
                DecimalUpDown_audio_language.Value = dec_value;

            if (decimal.TryParse(m_list_value_sas360con_cfg_installation[4], out dec_value))
                DecimalUpDown_audio_volume.Value = dec_value;

            #endregion

            Uncheck_SAS360CON_CFG_installation_controls();
        }

        private void Uncheck_SAS360CON_CFG_installation_controls()
        {
            m_list_textbox_sas360con_cfg_installation.ForEach(edit =>
            {
                edit.BorderThickness = new Thickness(0);
                edit.Background = new SolidColorBrush(Colors.Transparent);
                edit.Foreground = new SolidColorBrush(Colors.Black);
                edit.IsEnabled = false;
            });
            DecimalUpDown_audio_language.Foreground = new SolidColorBrush(Colors.Gray);
            DecimalUpDown_audio_language.IsEnabled = false;
            DecimalUpDown_audio_volume.Foreground = new SolidColorBrush(Colors.Gray);
            DecimalUpDown_audio_volume.IsEnabled = false;

            Button_send_SAS360CON_CFG_installation.Visibility = Visibility.Collapsed;
            Checkbox_edit_SAS360CON_CFG_installation.IsChecked = false;
        }

        #endregion

        #region SEND

        private void Button_send_SAS360CON_CFG_installation_Click(object sender, RoutedEventArgs e)
        {
            Send_SAS360CON_CFG_installation();
        }

        private void Send_SAS360CON_CFG_installation()
        {
            if ((!m_is_writing && !m_is_reading_event_hist) || m_is_writing_all_config)
            {
                Image_SAS360CON_CFG_installation_ok.Visibility = Visibility.Collapsed;
                Image_SAS360CON_CFG_installation_error.Visibility = Visibility.Collapsed;
                Image_SAS360CON_CFG_installation_warning.Visibility = Visibility.Collapsed;

                m_selected_command_write_location = COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_INSTALLATION;
                m_is_writing = true;
                m_timer_write_struct_sas360con_config.Start();
            }
        }

        #endregion

        #endregion


        #region Edit vehicle configuration

        #region CHECK - UNCHECK

        private void Checkbox_edit_SAS360CON_CFG_vehicle_config_Checked(object sender, RoutedEventArgs e)
        {
            m_list_value_sas360con_cfg_vehicle.Clear();

            m_list_textbox_sas360con_cfg_vehicle.ForEach(edit =>
            {
                edit.BorderThickness = new Thickness(1, 1, 2, 2);
                edit.Background = new SolidColorBrush(Colors.White);
                edit.Foreground = new SolidColorBrush(Color.FromArgb(255, 59, 84, 101));
                edit.IsEnabled = true;

                m_list_value_sas360con_cfg_vehicle.Add(edit.Text);
            });

            Image_SAS360CON_CFG_vehicle_config_ok.Visibility = Visibility.Collapsed;
            Image_SAS360CON_CFG_vehicle_config_error.Visibility = Visibility.Collapsed;
            Image_SAS360CON_CFG_vehicle_config_warning.Visibility = Visibility.Collapsed;

            Button_send_SAS360CON_CFG_vehicle_config.Visibility = Visibility.Visible;
        }

        private void Checkbox_edit_SAS360CON_CFG_vehicle_config_Unchecked(object sender, RoutedEventArgs e)
        {
            //Valores provisionales
            m_list_value_sas360con_cfg_vehicle
                .Select((Value, Index) => new { Value, Index }).ToList()
                .ForEach(backup => m_list_textbox_sas360con_cfg_vehicle[backup.Index].Text = backup.Value);

            Uncheck_SAS360CON_CFG_vehicle_controls();
        }

        private void Uncheck_SAS360CON_CFG_vehicle_controls()
        {
            m_list_textbox_sas360con_cfg_vehicle.ForEach(edit =>
            {
                edit.BorderThickness = new Thickness(0);
                edit.Background = new SolidColorBrush(Colors.Transparent);
                edit.Foreground = new SolidColorBrush(Colors.Black);
                edit.IsEnabled = false;
            });

            Button_send_SAS360CON_CFG_vehicle_config.Visibility = Visibility.Collapsed;
            Checkbox_edit_SAS360CON_CFG_vehicle_config.IsChecked = false;
        }

        #endregion

        #region SEND
        private void Button_send_SAS360CON_CFG_vehicle_config_Click(object sender, RoutedEventArgs e)
        {
            Send_SAS360CON_CFG_vehicle_config();
        }

        private void Send_SAS360CON_CFG_vehicle_config()
        {
            if ((!m_is_writing && !m_is_reading_event_hist) || m_is_writing_all_config)
            {
                Image_SAS360CON_CFG_vehicle_config_ok.Visibility = Visibility.Collapsed;
                Image_SAS360CON_CFG_vehicle_config_error.Visibility = Visibility.Collapsed;
                Image_SAS360CON_CFG_vehicle_config_warning.Visibility = Visibility.Collapsed;

                m_selected_command_write_location = COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_VEHICLE_CONFIG;
                m_is_writing = true;
                m_timer_write_struct_sas360con_config.Start();
            }
        }

        #endregion

        #endregion


        #region Edit detection area

        #region CHECK - UNCHECK

        private void Checkbox_edit_SAS360CON_CFG_detection_area_Checked(object sender, RoutedEventArgs e)
        {
            m_list_value_sas360con_cfg_detection_area.Clear();

            m_list_textbox_sas360con_cfg_detection_area.ForEach(edit =>
            {
                edit.BorderThickness = new Thickness(1, 1, 2, 2);
                edit.Background = new SolidColorBrush(Colors.White);
                edit.Foreground = new SolidColorBrush(Color.FromArgb(255, 59, 84, 101));
                edit.IsEnabled = true;

                m_list_value_sas360con_cfg_detection_area.Add(edit.Text);
            });

            DecimalUpDown_config_trilat_calc_enabled.Foreground = new SolidColorBrush(Colors.Black);
            DecimalUpDown_config_trilat_calc_enabled.IsEnabled = true;
            m_list_value_sas360con_cfg_detection_area.Add(DecimalUpDown_config_trilat_calc_enabled.Value.ToString()!);

            DecimalUpDown_config_gestion_avanzada_pos_enable.Foreground = new SolidColorBrush(Colors.Black);
            DecimalUpDown_config_gestion_avanzada_pos_enable.IsEnabled = true;
            m_list_value_sas360con_cfg_detection_area.Add(DecimalUpDown_config_gestion_avanzada_pos_enable.Value.ToString()!);

            Image_SAS360CON_CFG_detection_area_ok.Visibility = Visibility.Collapsed;
            Image_SAS360CON_CFG_detection_area_error.Visibility = Visibility.Collapsed;
            Image_SAS360CON_CFG_detection_area_warning.Visibility = Visibility.Collapsed;

            Button_send_SAS360CON_CFG_detection_area.Visibility = Visibility.Visible;
        }

        private void Checkbox_edit_SAS360CON_CFG_detection_area_Unchecked(object sender, RoutedEventArgs e)
        {
            //Valores provisionales
            for (int index = 0; index < m_list_value_sas360con_cfg_detection_area.Count - 2; index++)
            {
                m_list_textbox_sas360con_cfg_detection_area[index].Text = m_list_value_sas360con_cfg_detection_area[index];
            }

            if (decimal.TryParse(m_list_value_sas360con_cfg_detection_area[m_list_value_sas360con_cfg_detection_area.Count - 2], out decimal dec_value))
                DecimalUpDown_config_trilat_calc_enabled.Value = dec_value;

            if (decimal.TryParse(m_list_value_sas360con_cfg_detection_area[m_list_value_sas360con_cfg_detection_area.Count - 1], out dec_value))
                DecimalUpDown_config_gestion_avanzada_pos_enable.Value = dec_value;

            Uncheck_SAS360CON_CFG_detection_area();
        }

        private void Uncheck_SAS360CON_CFG_detection_area()
        {
            m_list_textbox_sas360con_cfg_detection_area.ForEach(edit =>
            {
                edit.BorderThickness = new Thickness(0);
                edit.Background = new SolidColorBrush(Colors.Transparent);
                edit.Foreground = new SolidColorBrush(Colors.Black);
                edit.IsEnabled = false;
            });

            DecimalUpDown_config_trilat_calc_enabled.Foreground = new SolidColorBrush(Colors.Gray);
            DecimalUpDown_config_trilat_calc_enabled.IsEnabled = false;

            DecimalUpDown_config_gestion_avanzada_pos_enable.Foreground = new SolidColorBrush(Colors.Gray);
            DecimalUpDown_config_gestion_avanzada_pos_enable.IsEnabled = false;

            Button_send_SAS360CON_CFG_detection_area.Visibility = Visibility.Collapsed;
            Checkbox_edit_SAS360CON_CFG_detection_area.IsChecked = false;
        }

        #endregion

        #region SEND

        private void Button_send_SAS360CON_CFG_detection_area_Click(object sender, RoutedEventArgs e)
        {
            Send_SAS360CON_CFG_detection_area();
        }

        private void Send_SAS360CON_CFG_detection_area()
        {
            if ((!m_is_writing && !m_is_reading_event_hist) || m_is_writing_all_config)
            {
                Image_SAS360CON_CFG_detection_area_ok.Visibility = Visibility.Collapsed;
                Image_SAS360CON_CFG_detection_area_error.Visibility = Visibility.Collapsed;
                Image_SAS360CON_CFG_detection_area_warning.Visibility = Visibility.Collapsed;

                m_selected_command_write_location = COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_DETECTION_AREA;
                m_is_writing = true;
                m_timer_write_struct_sas360con_config.Start();
            }
        }

        #endregion

        #endregion


        #region Edit output actions

        #region CHECK - UNCHECK

        private void Checkbox_edit_SAS360CON_CFG_output_actions_Checked(object sender, RoutedEventArgs e)
        {
            m_list_value_sas360con_cfg_output_leds.Clear();
            List<int> list_output_leds = Convert_output_leds_into_values();
            m_list_value_sas360con_cfg_output_leds = list_output_leds.ConvertAll(value => value.ToString());

            m_list_border_sas360_config_rele1.ForEach(output =>
            {
                Brush led_color = output.Background;
                if (led_color.ToString() == Colors.Green.ToString())
                    output.Background = new SolidColorBrush(Colors.Blue);

                output.Cursor = Cursors.Hand;
                output.MouseDown += new MouseButtonEventHandler(Border_rele1_MouseDown);
            });

            m_list_border_sas360_config_rele2.ForEach(output =>
            {
                Brush led_color = output.Background;
                if (led_color.ToString() == Colors.Green.ToString())
                    output.Background = new SolidColorBrush(Colors.Blue);

                output.Cursor = Cursors.Hand;
                output.MouseDown += new MouseButtonEventHandler(Border_rele2_MouseDown);
            });

            m_list_border_sas360_config_rele3.ForEach(output =>
            {
                Brush led_color = output.Background;
                if (led_color.ToString() == Colors.Green.ToString())
                    output.Background = new SolidColorBrush(Colors.Blue);

                output.Cursor = Cursors.Hand;
                output.MouseDown += new MouseButtonEventHandler(Border_rele3_MouseDown);
            });

            m_list_border_sas360_config_rele4.ForEach(output =>
            {
                Brush led_color = output.Background;
                if (led_color.ToString() == Colors.Green.ToString())
                    output.Background = new SolidColorBrush(Colors.Blue);

                output.Cursor = Cursors.Hand;
                output.MouseDown += new MouseButtonEventHandler(Border_rele4_MouseDown);
            });

            m_list_border_sas360_config_trans1.ForEach(output =>
            {
                Brush led_color = output.Background;
                if (led_color.ToString() == Colors.Green.ToString())
                    output.Background = new SolidColorBrush(Colors.Blue);

                output.Cursor = Cursors.Hand;
                output.MouseDown += new MouseButtonEventHandler(Border_trans1_MouseDown);
            });

            m_list_border_sas360_config_trans2.ForEach(output =>
            {
                Brush led_color = output.Background;
                if (led_color.ToString() == Colors.Green.ToString())
                    output.Background = new SolidColorBrush(Colors.Blue);

                output.Cursor = Cursors.Hand;
                output.MouseDown += new MouseButtonEventHandler(Border_trans2_MouseDown);
            });


            Image_SAS360CON_CFG_output_actions_ok.Visibility = Visibility.Collapsed;
            Image_SAS360CON_CFG_output_actions_error.Visibility = Visibility.Collapsed;
            Image_SAS360CON_CFG_output_actions_warning.Visibility = Visibility.Collapsed;

            Button_send_SAS360CON_CFG_output_actions.Visibility = Visibility.Visible;
        }

        private void Checkbox_edit_SAS360CON_CFG_output_actions_Unchecked(object sender, RoutedEventArgs e)
        {
            //Valores provisionales
            for (int bit_pos = 0; bit_pos < Constants.MAX_BITS_USHORT_VALUE; bit_pos++)
            {
                m_list_border_sas360_config_rele1[bit_pos].Background = Functions.IsBitSetTo1(ushort.Parse(m_list_value_sas360con_cfg_output_leds[0]), bit_pos) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                m_list_border_sas360_config_rele2[bit_pos].Background = Functions.IsBitSetTo1(ushort.Parse(m_list_value_sas360con_cfg_output_leds[1]), bit_pos) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                m_list_border_sas360_config_rele3[bit_pos].Background = Functions.IsBitSetTo1(ushort.Parse(m_list_value_sas360con_cfg_output_leds[2]), bit_pos) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                m_list_border_sas360_config_rele4[bit_pos].Background = Functions.IsBitSetTo1(ushort.Parse(m_list_value_sas360con_cfg_output_leds[3]), bit_pos) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                m_list_border_sas360_config_trans1[bit_pos].Background = Functions.IsBitSetTo1(ushort.Parse(m_list_value_sas360con_cfg_output_leds[4]), bit_pos) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                m_list_border_sas360_config_trans2[bit_pos].Background = Functions.IsBitSetTo1(ushort.Parse(m_list_value_sas360con_cfg_output_leds[5]), bit_pos) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
            }

            Uncheck_SAS360CON_CFG_output_actions();
        }


        private void Uncheck_SAS360CON_CFG_output_actions()
        {
            m_list_border_sas360_config_rele1.ForEach(output =>
            {
                Brush led_color = output.Background;
                if (led_color.ToString() == Colors.Blue.ToString())
                    output.Background = new SolidColorBrush(Colors.Green);

                output.Cursor = Cursors.None;
                output.MouseDown -= new MouseButtonEventHandler(Border_rele1_MouseDown);
            });

            m_list_border_sas360_config_rele2.ForEach(output =>
            {
                Brush led_color = output.Background;
                if (led_color.ToString() == Colors.Blue.ToString())
                    output.Background = new SolidColorBrush(Colors.Green);

                output.Cursor = Cursors.None;
                output.MouseDown -= new MouseButtonEventHandler(Border_rele2_MouseDown);
            });

            m_list_border_sas360_config_rele3.ForEach(output =>
            {
                Brush led_color = output.Background;
                if (led_color.ToString() == Colors.Blue.ToString())
                    output.Background = new SolidColorBrush(Colors.Green);

                output.Cursor = Cursors.None;
                output.MouseDown -= new MouseButtonEventHandler(Border_rele3_MouseDown);
            });

            m_list_border_sas360_config_rele4.ForEach(output =>
            {
                Brush led_color = output.Background;
                if (led_color.ToString() == Colors.Blue.ToString())
                    output.Background = new SolidColorBrush(Colors.Green);

                output.Cursor = Cursors.None;
                output.MouseDown -= new MouseButtonEventHandler(Border_rele4_MouseDown);
            });

            m_list_border_sas360_config_trans1.ForEach(output =>
            {
                Brush led_color = output.Background;
                if (led_color.ToString() == Colors.Blue.ToString())
                    output.Background = new SolidColorBrush(Colors.Green);

                output.Cursor = Cursors.None;
                output.MouseDown -= new MouseButtonEventHandler(Border_trans1_MouseDown);
            });

            m_list_border_sas360_config_trans2.ForEach(output =>
            {
                Brush led_color = output.Background;
                if (led_color.ToString() == Colors.Blue.ToString())
                    output.Background = new SolidColorBrush(Colors.Green);

                output.Cursor = Cursors.None;
                output.MouseDown -= new MouseButtonEventHandler(Border_trans2_MouseDown);
            });

            Button_send_SAS360CON_CFG_output_actions.Visibility = Visibility.Collapsed;
            Checkbox_edit_SAS360CON_CFG_output_actions.IsChecked = false;
        }

        #endregion

        #region SEND

        private void Button_send_SAS360CON_CFG_output_actions_Click(object sender, RoutedEventArgs e)
        {
            Send_SAS360CON_CFG_output_actions();
        }

        private void Send_SAS360CON_CFG_output_actions()
        {
            if ((!m_is_writing && !m_is_reading_event_hist) || m_is_writing_all_config)
            {
                Image_SAS360CON_CFG_output_actions_ok.Visibility = Visibility.Collapsed;
                Image_SAS360CON_CFG_output_actions_error.Visibility = Visibility.Collapsed;
                Image_SAS360CON_CFG_output_actions_warning.Visibility = Visibility.Collapsed;

                m_selected_command_write_location = COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_OUTPUT;
                m_is_writing = true;
                m_timer_write_struct_sas360con_config.Start();
            }
        }

        #endregion

        #region I/ O mouse down

        private void Border_rele1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int index = Array.IndexOf(m_list_border_sas360_config_rele1.ToArray(), sender);

            Brush led_color = m_list_border_sas360_config_rele1[index].Background;
            m_list_border_sas360_config_rele1[index].Background = led_color.ToString() == Colors.White.ToString() ? new SolidColorBrush(Colors.Blue) : new SolidColorBrush(Colors.White);

        }

        private void Border_rele2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int index = Array.IndexOf(m_list_border_sas360_config_rele2.ToArray(), sender);

            Brush led_color = m_list_border_sas360_config_rele2[index].Background;
            m_list_border_sas360_config_rele2[index].Background = led_color.ToString() == Colors.White.ToString() ? new SolidColorBrush(Colors.Blue) : new SolidColorBrush(Colors.White);
        }

        private void Border_rele3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int index = Array.IndexOf(m_list_border_sas360_config_rele3.ToArray(), sender);

            Brush led_color = m_list_border_sas360_config_rele3[index].Background;
            m_list_border_sas360_config_rele3[index].Background = led_color.ToString() == Colors.White.ToString() ? new SolidColorBrush(Colors.Blue) : new SolidColorBrush(Colors.White);
        }

        private void Border_rele4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int index = Array.IndexOf(m_list_border_sas360_config_rele4.ToArray(), sender);

            Brush led_color = m_list_border_sas360_config_rele4[index].Background;
            m_list_border_sas360_config_rele4[index].Background = led_color.ToString() == Colors.White.ToString() ? new SolidColorBrush(Colors.Blue) : new SolidColorBrush(Colors.White);
        }

        private void Border_trans1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int index = Array.IndexOf(m_list_border_sas360_config_trans1.ToArray(), sender);

            Brush led_color = m_list_border_sas360_config_trans1[index].Background;
            m_list_border_sas360_config_trans1[index].Background = led_color.ToString() == Colors.White.ToString() ? new SolidColorBrush(Colors.Blue) : new SolidColorBrush(Colors.White);
        }

        private void Border_trans2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int index = Array.IndexOf(m_list_border_sas360_config_trans2.ToArray(), sender);

            Brush led_color = m_list_border_sas360_config_trans2[index].Background;
            m_list_border_sas360_config_trans2[index].Background = led_color.ToString() == Colors.White.ToString() ? new SolidColorBrush(Colors.Blue) : new SolidColorBrush(Colors.White);
        }

        #endregion

        #region Convert output leds into values

        private List<int> Convert_output_leds_into_values()
        {
            List<int> list_output_leds = new();

            int rele1_value = 0x00;
            m_list_border_sas360_config_rele1
                .Select((Value, Index) => new { Control = Value, Position = Index }).ToList()
                .ForEach(output =>
                {
                    Brush led_color = output.Control.Background;
                    if (led_color.ToString() != Colors.White.ToString())
                    {
                        rele1_value = Functions.SetBitTo1(rele1_value, output.Position);
                    }
                });

            list_output_leds.Add((int)rele1_value);

            int rele2_value = 0x00;
            m_list_border_sas360_config_rele2
                .Select((Value, Index) => new { Control = Value, Position = Index }).ToList()
                .ForEach(output =>
                {
                    Brush led_color = output.Control.Background;
                    if (led_color.ToString() != Colors.White.ToString())
                    {
                        rele2_value = Functions.SetBitTo1(rele2_value, output.Position);
                    }
                });

            list_output_leds.Add((int)rele2_value);


            int rele3_value = 0x00;
            m_list_border_sas360_config_rele3
                .Select((Value, Index) => new { Control = Value, Position = Index }).ToList()
                .ForEach(output =>
                {
                    Brush led_color = output.Control.Background;
                    if (led_color.ToString() != Colors.White.ToString())
                    {
                        rele3_value = Functions.SetBitTo1(rele3_value, output.Position);
                    }
                });

            list_output_leds.Add((int)rele3_value);

            int rele4_value = 0x00;
            m_list_border_sas360_config_rele4
                .Select((Value, Index) => new { Control = Value, Position = Index }).ToList()
                .ForEach(output =>
                {
                    Brush led_color = output.Control.Background;
                    if (led_color.ToString() != Colors.White.ToString())
                    {
                        rele4_value = Functions.SetBitTo1(rele4_value, output.Position);
                    }
                });

            list_output_leds.Add((int)rele4_value);

            int trans1_value = 0x00;
            m_list_border_sas360_config_trans1
                .Select((Value, Index) => new { Control = Value, Position = Index }).ToList()
                .ForEach(output =>
                {
                    Brush led_color = output.Control.Background;
                    if (led_color.ToString() != Colors.White.ToString())
                    {
                        trans1_value = Functions.SetBitTo1(trans1_value, output.Position);
                    }
                });

            list_output_leds.Add((int)trans1_value);

            int trans2_value = 0x00;
            m_list_border_sas360_config_trans2
                .Select((Value, Index) => new { Control = Value, Position = Index }).ToList()
                .ForEach(output =>
                {
                    Brush led_color = output.Control.Background;
                    if (led_color.ToString() != Colors.White.ToString())
                    {
                        trans2_value = Functions.SetBitTo1(trans2_value, output.Position);
                    }
                });

            list_output_leds.Add((int)trans2_value);

            return list_output_leds;
        }

        #endregion

        #endregion


        #region Edit uwb com config

        #region CHECK - UNCHECK

        private void Checkbox_edit_SAS360CON_CFG_uwb_com_config_Checked(object sender, RoutedEventArgs e)
        {
            m_list_value_sas360con_cfg_uwb_communication.Clear();

            m_list_textbox_sas360con_cfg_uwb_communication.ForEach(edit =>
            {
                edit.BorderThickness = new Thickness(1, 1, 2, 2);
                edit.Background = new SolidColorBrush(Colors.White);
                edit.Foreground = new SolidColorBrush(Color.FromArgb(255, 59, 84, 101));
                edit.IsEnabled = true;

                m_list_value_sas360con_cfg_uwb_communication.Add(edit.Text);
            });


            Image_SAS360CON_CFG_uwb_com_config_ok.Visibility = Visibility.Collapsed;
            Image_SAS360CON_CFG_uwb_com_config_error.Visibility = Visibility.Collapsed;
            Image_SAS360CON_CFG_uwb_com_config_warning.Visibility = Visibility.Collapsed;

            Button_send_SAS360CON_CFG_uwb_com_config.Visibility = Visibility.Visible;
        }

        private void Checkbox_edit_SAS360CON_CFG_uwb_com_config_Unchecked(object sender, RoutedEventArgs e)
        {
            //Valores provisionales
            m_list_value_sas360con_cfg_uwb_communication
                .Select((Value, Index) => new { Value, Index }).ToList()
                .ForEach(backup => m_list_textbox_sas360con_cfg_uwb_communication[backup.Index].Text = backup.Value);

            Uncheck_SAS360CON_CFG_uwb_com_config();
        }

        private void Uncheck_SAS360CON_CFG_uwb_com_config()
        {
            m_list_textbox_sas360con_cfg_uwb_communication.ForEach(edit =>
            {
                edit.BorderThickness = new Thickness(0);
                edit.Background = new SolidColorBrush(Colors.Transparent);
                edit.Foreground = new SolidColorBrush(Colors.Black);
                edit.IsEnabled = false;
            });

            Button_send_SAS360CON_CFG_uwb_com_config.Visibility = Visibility.Collapsed;
            Checkbox_edit_SAS360CON_CFG_uwb_com_config.IsChecked = false;
        }

        #endregion

        #region SEND

        private void Button_send_SAS360CON_CFG_uwb_com_config_Click(object sender, RoutedEventArgs e)
        {
            Send_SAS360CON_CFG_uwb_com_config();
        }

        private void Send_SAS360CON_CFG_uwb_com_config()
        {
            if ((!m_is_writing && !m_is_reading_event_hist) || m_is_writing_all_config)
            {
                Image_SAS360CON_CFG_uwb_com_config_ok.Visibility = Visibility.Collapsed;
                Image_SAS360CON_CFG_uwb_com_config_error.Visibility = Visibility.Collapsed;
                Image_SAS360CON_CFG_uwb_com_config_warning.Visibility = Visibility.Collapsed;

                m_selected_command_write_location = COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_UWB_COMM;
                m_is_writing = true;
                m_timer_write_struct_sas360con_config.Start();
            }
        }

        #endregion

        #endregion


        #region Edit recordings

        #region CHECK - UNCHECK

        private void Checkbox_edit_SAS360CON_CFG_recordings_Checked(object sender, RoutedEventArgs e)
        {
            m_list_value_sas360con_cfg_recording_index.Clear();

            m_list_textbox_sas360con_cfg_recording_index.ForEach(edit =>
            {
                edit.BorderThickness = new Thickness(1, 1, 2, 2);
                edit.Background = new SolidColorBrush(Colors.White);
                edit.Foreground = new SolidColorBrush(Color.FromArgb(255, 59, 84, 101));
                edit.IsEnabled = true;

                m_list_value_sas360con_cfg_recording_index.Add(edit.Text);
            });

            m_list_value_sas360con_cfg_recording_unit.Clear();

            m_list_textbox_sas360con_cfg_recording_unit.ForEach(edit =>
            {
                edit.BorderThickness = new Thickness(1, 1, 2, 2);
                edit.Background = new SolidColorBrush(Colors.White);
                edit.Foreground = new SolidColorBrush(Color.FromArgb(255, 59, 84, 101));
                edit.IsEnabled = true;

                m_list_value_sas360con_cfg_recording_unit.Add(edit.Text);
            });

            m_list_value_sas360con_cfg_recording_period.Clear();

            m_list_textbox_sas360con_cfg_recording_period.ForEach(edit =>
            {
                edit.BorderThickness = new Thickness(1, 1, 2, 2);
                edit.Background = new SolidColorBrush(Colors.White);
                edit.Foreground = new SolidColorBrush(Color.FromArgb(255, 59, 84, 101));
                edit.IsEnabled = true;

                m_list_value_sas360con_cfg_recording_period.Add(edit.Text);
            });

            Image_SAS360CON_CFG_recordings_ok.Visibility = Visibility.Collapsed;
            Image_SAS360CON_CFG_recordings_error.Visibility = Visibility.Collapsed;
            Image_SAS360CON_CFG_recordings_warning.Visibility = Visibility.Collapsed;

            Button_send_SAS360CON_CFG_recordings.Visibility = Visibility.Visible;
        }

        private void Checkbox_edit_SAS360CON_CFG_recordings_Unchecked(object sender, RoutedEventArgs e)
        {
            //Valores provisionales
            m_list_value_sas360con_cfg_recording_index
                .Select((Value, Index) => new { Value, Index }).ToList()
                .ForEach(backup => m_list_textbox_sas360con_cfg_recording_index[backup.Index].Text = backup.Value);

            m_list_value_sas360con_cfg_recording_unit
                .Select((Value, Index) => new { Value, Index }).ToList()
                .ForEach(backup => m_list_textbox_sas360con_cfg_recording_unit[backup.Index].Text = backup.Value);

            m_list_value_sas360con_cfg_recording_period
                .Select((Value, Index) => new { Value, Index }).ToList()
                .ForEach(backup => m_list_textbox_sas360con_cfg_recording_period[backup.Index].Text = backup.Value);

            Uncheck_SAS360CON_CFG_recording();
        }

        private void Uncheck_SAS360CON_CFG_recording()
        {
            m_list_textbox_sas360con_cfg_recording_index.ForEach(edit =>
            {
                edit.BorderThickness = new Thickness(0);
                edit.Background = new SolidColorBrush(Colors.Transparent);
                edit.Foreground = new SolidColorBrush(Colors.Black);
                edit.IsEnabled = false;
            });

            m_list_textbox_sas360con_cfg_recording_unit.ForEach(edit =>
            {
                edit.BorderThickness = new Thickness(0);
                edit.Background = new SolidColorBrush(Colors.Transparent);
                edit.Foreground = new SolidColorBrush(Colors.Black);
                edit.IsEnabled = false;
            });

            m_list_textbox_sas360con_cfg_recording_period.ForEach(edit =>
            {
                edit.BorderThickness = new Thickness(0);
                edit.Background = new SolidColorBrush(Colors.Transparent);
                edit.Foreground = new SolidColorBrush(Colors.Black);
                edit.IsEnabled = false;
            });

            Button_send_SAS360CON_CFG_recordings.Visibility = Visibility.Collapsed;
            Checkbox_edit_SAS360CON_CFG_recordings.IsChecked = false;
        }

        #endregion

        #region SEND

        private void Button_send_SAS360CON_CFG_recordings_Click(object sender, RoutedEventArgs e)
        {
            Send_SAS360CON_CFG_recording();
        }

        private void Send_SAS360CON_CFG_recording()
        {
            if ((!m_is_writing && !m_is_reading_event_hist) || m_is_writing_all_config)
            {
                Image_SAS360CON_CFG_recordings_ok.Visibility = Visibility.Collapsed;
                Image_SAS360CON_CFG_recordings_error.Visibility = Visibility.Collapsed;
                Image_SAS360CON_CFG_recordings_warning.Visibility = Visibility.Collapsed;

                m_selected_command_write_location = COMMAND_WRITE_LOCATION.SAS360CON_CFG_RECORDING;
                m_is_writing = true;
                m_timer_write_struct_sas360con_config.Start();
            }

        }

        #endregion

        #endregion


        #region Edit miscellaneous config

        #region CHECK - UNCHECK

        private void Checkbox_edit_SAS360CON_CFG_miscellaneous_Checked(object sender, RoutedEventArgs e)
        {
            m_list_value_sas360con_cfg_miscellaneous.Clear();

            m_list_textbox_sas360con_cfg_miscellaneous.ForEach(edit =>
            {
                edit.BorderThickness = new Thickness(1, 1, 2, 2);
                edit.Background = new SolidColorBrush(Colors.White);
                edit.Foreground = new SolidColorBrush(Color.FromArgb(255, 59, 84, 101));
                edit.IsEnabled = true;

                m_list_value_sas360con_cfg_miscellaneous.Add(edit.Text);
            });

            Image_SAS360CON_CFG_miscellaneous_ok.Visibility = Visibility.Collapsed;
            Image_SAS360CON_CFG_miscellaneous_error.Visibility = Visibility.Collapsed;
            Image_SAS360CON_CFG_miscellaneous_warning.Visibility = Visibility.Collapsed;

            Button_send_SAS360CON_CFG_miscellaneous.Visibility = Visibility.Visible;
        }

        private void Checkbox_edit_SAS360CON_CFG_miscellaneous_Unchecked(object sender, RoutedEventArgs e)
        {
            //Valores provisionales
            m_list_value_sas360con_cfg_miscellaneous
                .Select((Value, Index) => new { Value, Index }).ToList()
                .ForEach(backup => m_list_textbox_sas360con_cfg_miscellaneous[backup.Index].Text = backup.Value);

            Uncheck_SAS360CON_CFG_miscellaneous();
        }

        private void Uncheck_SAS360CON_CFG_miscellaneous()
        {
            m_list_textbox_sas360con_cfg_miscellaneous.ForEach(edit =>
            {
                edit.BorderThickness = new Thickness(0);
                edit.Background = new SolidColorBrush(Colors.Transparent);
                edit.Foreground = new SolidColorBrush(Colors.Black);
                edit.IsEnabled = false;
            });

            Button_send_SAS360CON_CFG_miscellaneous.Visibility = Visibility.Collapsed;
            Checkbox_edit_SAS360CON_CFG_miscellaneous.IsChecked = false;
        }


        #endregion

        #region SEND

        private void Button_send_SAS360CON_CFG_miscellaneous_Click(object sender, RoutedEventArgs e)
        {
            Send_SAS360CON_CFG_miscellaneous();
        }

        private void Send_SAS360CON_CFG_miscellaneous()
        {
            if ((!m_is_writing && !m_is_reading_event_hist) || m_is_writing_all_config)
            {
                Image_SAS360CON_CFG_miscellaneous_ok.Visibility = Visibility.Collapsed;
                Image_SAS360CON_CFG_miscellaneous_error.Visibility = Visibility.Collapsed;
                Image_SAS360CON_CFG_miscellaneous_warning.Visibility = Visibility.Collapsed;

                m_selected_command_write_location = COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_MISCELLANEOUS;
                m_is_writing = true;
                m_timer_write_struct_sas360con_config.Start();
            }
        }

        #endregion

        #endregion


        #region SAVE - LOAD - SEND ALL CONFIG 

        private void Button_save_config_tab_sas360con_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double d_value;
                ushort u16_value;

                #region Installation client definition

                try
                {
                    int index_installation_client = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + 2);

                    m_list_textbox_sas360con_cfg_installation.ForEach(config_value =>
                    {
                        if (ushort.TryParse(config_value.Text.ToString(), out ushort value))
                            Globals.GetTheInstance().List_sas360con_cfg[index_installation_client].Edit_value = value.ToString();

                        index_installation_client++;
                    });
                    Globals.GetTheInstance().List_sas360con_cfg[index_installation_client].Edit_value = DecimalUpDown_audio_language.Value.ToString()!;

                    index_installation_client++;

                    Globals.GetTheInstance().List_sas360con_cfg[index_installation_client].Edit_value = DecimalUpDown_audio_volume.Value.ToString()!;

                    index_installation_client++; //reserved
                }
                catch (Exception ex)
                {
                    Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Button_save_config_tab_sas360con_Click)} (Installation client) -> {ex.Message}");
                }

                #endregion

                #region Vehicle configuration

                try
                {
                    int index_vehicle_config = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + 8);

                    if (double.TryParse(Textbox_config_vehicle_dim_x.Text, out d_value))
                    {
                        Globals.GetTheInstance().List_sas360con_cfg[index_vehicle_config].Edit_value = Math.Round(decimal.Multiply((decimal)d_value, 100), 0).ToString();
                        index_vehicle_config++;
                    }

                    if (double.TryParse(Textbox_config_vehicle_dim_y.Text, out d_value))
                    {
                        Globals.GetTheInstance().List_sas360con_cfg[index_vehicle_config].Edit_value = Math.Round(decimal.Multiply((decimal)d_value, 100), 0).ToString();
                        index_vehicle_config++;
                    }

                    #region Antenna pos

                    if (double.TryParse(Textbox_config_antena1_pos_x.Text, out d_value))
                    {
                        Globals.GetTheInstance().List_sas360con_cfg[index_vehicle_config].Edit_value = Math.Round(decimal.Multiply((decimal)d_value, 100), 0).ToString();
                        index_vehicle_config++;
                    }

                    if (double.TryParse(Textbox_config_antena1_pos_y.Text, out d_value))
                    {
                        Globals.GetTheInstance().List_sas360con_cfg[index_vehicle_config].Edit_value = Math.Round(decimal.Multiply((decimal)d_value, 100), 0).ToString();
                        index_vehicle_config++;
                    }

                    if (double.TryParse(Textbox_config_antena2_pos_x.Text, out d_value))
                    {
                        Globals.GetTheInstance().List_sas360con_cfg[index_vehicle_config].Edit_value = Math.Round(decimal.Multiply((decimal)d_value, 100), 0).ToString();
                        index_vehicle_config++;
                    }

                    if (double.TryParse(Textbox_config_antena2_pos_y.Text, out d_value))
                    {
                        Globals.GetTheInstance().List_sas360con_cfg[index_vehicle_config].Edit_value = Math.Round(decimal.Multiply((decimal)d_value, 100), 0).ToString();
                        index_vehicle_config++;
                    }


                    if (double.TryParse(Textbox_config_antena3_pos_x.Text, out d_value))
                    {
                        Globals.GetTheInstance().List_sas360con_cfg[index_vehicle_config].Edit_value = Math.Round(decimal.Multiply((decimal)d_value, 100), 0).ToString();
                        index_vehicle_config++;
                    }

                    if (double.TryParse(Textbox_config_antena3_pos_y.Text, out d_value))
                    {
                        Globals.GetTheInstance().List_sas360con_cfg[index_vehicle_config].Edit_value = Math.Round(decimal.Multiply((decimal)d_value, 100), 0).ToString();
                        index_vehicle_config++;
                    }

                    #endregion 
                }
                catch (Exception ex)
                {
                    Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Button_save_config_tab_sas360con_Click)} (Vehicle configuration) -> {ex.Message}");
                }

                #endregion

                #region Detection area definition

                try
                {
                    int index_detection_area = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + 20);

                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                    {
                        if (double.TryParse(m_list_textbox_config_area_FRONT_ANRI[index].Text.ToString(), out d_value))
                        {
                            Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Edit_value = Math.Round(decimal.Multiply((decimal)d_value, 100), 0).ToString();
                            index_detection_area++;
                        }
                    }

                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                    {
                        if (double.TryParse(m_list_textbox_config_area_RIGHT_ANRI[index].Text.ToString(), out d_value))
                        {
                            Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Edit_value = Math.Round(decimal.Multiply((decimal)d_value, 100), 0).ToString();
                            index_detection_area++;
                        }
                    }

                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                    {
                        if (double.TryParse(m_list_textbox_config_area_BACK_ANRI[index].Text.ToString(), out d_value))
                        {
                            Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Edit_value = Math.Round(decimal.Multiply((decimal)d_value, 100), 0).ToString();
                            index_detection_area++;
                        }
                    }

                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                    {
                        if (double.TryParse(m_list_textbox_config_area_LEFT_ANRI[index].Text.ToString(), out d_value))
                        {
                            Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Edit_value = Math.Round(decimal.Multiply((decimal)d_value, 100), 0).ToString();
                            index_detection_area++;
                        }
                    }


                    if (double.TryParse(Textbox_config_area_detection_distance.Text.ToString(), out d_value))
                    {
                        Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Edit_value = Math.Round(decimal.Multiply((decimal)d_value, 100), 0).ToString();
                        index_detection_area++;
                    }

                    if (double.TryParse(Textbox_config_area_change_hyst.Text.ToString(), out d_value))
                    {
                        Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Edit_value = Math.Round(decimal.Multiply((decimal)d_value, 100), 0).ToString();
                        index_detection_area++;
                    }

                    if (double.TryParse(Textbox_config_sector_change_hyst.Text.ToString(), out d_value))
                    {
                        Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Edit_value = Math.Round(decimal.Multiply((decimal)d_value, 100), 0).ToString();
                        index_detection_area++;
                    }

                    Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Edit_value = DecimalUpDown_config_trilat_calc_enabled.Value.ToString()!;

                    index_detection_area++;

                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                    {
                        if (double.TryParse(m_list_textbox_config_area_DIST_ANTENA_ANRI[index].Text.ToString(), out d_value))
                        {
                            Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Edit_value = Math.Round(decimal.Multiply((decimal)d_value, 100), 0).ToString();
                            index_detection_area++;
                        }
                    }

                    Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Edit_value = DecimalUpDown_config_gestion_avanzada_pos_enable.Value.ToString()!;
                }
                catch (Exception ex)
                {
                    Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Button_save_config_tab_sas360con_Click)} (Detection area) -> {ex.Message}");
                }

                #endregion

                #region Actuaciones entradas / salidas

                try
                {
                    int index_actuaciones_e_a = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + 50);

                    List<int> list_output_leds = Convert_output_leds_into_values();
                    list_output_leds.ForEach(output_led =>
                    {
                        Globals.GetTheInstance().List_sas360con_cfg[index_actuaciones_e_a].Edit_value = output_led.ToString();
                        index_actuaciones_e_a++;
                    });
                }
                catch (Exception ex)
                {
                    Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Button_save_config_tab_sas360con_Click)} (Actuaciones E/S) -> {ex.Message}");
                }

                #endregion

                #region UWB COM config reserved

                try
                {
                    int index_uwb_com_config = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + 70);

                    m_list_textbox_sas360con_cfg_uwb_communication.ForEach(config_value =>
                    {
                        if (byte.TryParse(config_value.Text.ToString(), out byte value))
                        {
                            Globals.GetTheInstance().List_sas360con_cfg[index_uwb_com_config].Edit_value = value.ToString();
                            index_uwb_com_config++;
                        }
                    });
                }
                catch (Exception ex)
                {
                    Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Button_save_config_tab_sas360con_Click)} (UWB COM config) -> {ex.Message}");
                }

                #endregion

                #region Miscellaneous

                try
                {

                    #region Los tres primeros -> temporizadores y filtros

                    int index_temp_filters_config = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + 60);

                    if (ushort.TryParse(Textbox_miscellaneous_output_deactivation_delay.Text, out u16_value))
                    {
                        Globals.GetTheInstance().List_sas360con_cfg[index_temp_filters_config].Edit_value = u16_value.ToString();
                        index_temp_filters_config++;
                    }

                    if (double.TryParse(Textbox_miscellaneous_area_zone_dist.Text, out d_value))
                    {
                        Globals.GetTheInstance().List_sas360con_cfg[index_temp_filters_config].Edit_value = Math.Round(decimal.Multiply((decimal)d_value, 100), 0).ToString();
                        index_temp_filters_config++;
                    }

                    if (ushort.TryParse(Textbox_miscellaneous_red_zone_alert_audio_repeat_sec.Text, out u16_value))
                    {
                        Globals.GetTheInstance().List_sas360con_cfg[index_temp_filters_config].Edit_value = u16_value.ToString();
                        index_temp_filters_config++;
                    }

                    #endregion

                    #region El siguiente UWB tag config

                    int index_uwb_tag_config = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + 80);

                    if (double.TryParse(Textbox_miscellaneous_tag_list_clear_timeout.Text.ToString(), out d_value))
                    {
                        Globals.GetTheInstance().List_sas360con_cfg[index_uwb_tag_config].Edit_value = Math.Round(decimal.Multiply((decimal)d_value, 100), 0).ToString();
                    }

                    index_uwb_tag_config++;

                    #endregion
                }
                catch (Exception ex)
                {
                    Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Button_save_config_tab_sas360con_Click)} (Miscellaneous) -> {ex.Message}");
                }

                #endregion

                #region Recording

                try
                {
                    int index_recording_config = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + 90);

                    for (int index = 0; index < Constants.RECORDING_REG_SAS360CON_ARRAY; index++)
                    {
                        if (byte.TryParse(m_list_textbox_sas360con_cfg_recording_index[index].Text.ToString(), out byte recording_value))
                        {
                            Globals.GetTheInstance().List_sas360con_cfg[index_recording_config].Edit_value = recording_value.ToString();
                            index_recording_config++;
                        }
                    }
                    for (int index = 0; index < Constants.RECORDING_REG_SAS360CON_ARRAY; index++)
                    {
                        if (byte.TryParse(m_list_textbox_sas360con_cfg_recording_unit[index].Text.ToString(), out byte recording_value))
                        {
                            Globals.GetTheInstance().List_sas360con_cfg[index_recording_config].Edit_value = recording_value.ToString();
                            index_recording_config++;
                        }
                    }
                    for (int index = 0; index < Constants.RECORDING_REG_SAS360CON_ARRAY; index++)
                    {
                        if (ushort.TryParse(m_list_textbox_sas360con_cfg_recording_period[index].Text.ToString(), out ushort u_recording_value))
                        {
                            Globals.GetTheInstance().List_sas360con_cfg[index_recording_config].Edit_value = u_recording_value.ToString();
                            index_recording_config++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Button_save_config_tab_sas360con_Click)} (Recording) -> {ex.Message}");
                }

                #endregion

                #region Variables que no están definidas en pantalla -> Registrar el valor leido

                try
                {
                    for (int index = 0; index < Globals.GetTheInstance().List_sas360con_cfg.Count; index++)
                    {
                        if (Globals.GetTheInstance().List_sas360con_cfg[index] != null)
                        {
                            if (string.IsNullOrEmpty(Globals.GetTheInstance().List_sas360con_cfg[index].Edit_value))
                                if (Globals.GetTheInstance().List_sas360con_cfg[index].Value != null)
                                    Globals.GetTheInstance().List_sas360con_cfg[index].Edit_value = Globals.GetTheInstance().List_sas360con_cfg[index].Value!.ToString();
                        }
                    }
                }
                catch { }

                #endregion


                #region Load / save window

                LoadSaveConfigConWindow load_save = new()
                {
                    Program_actions = PROGRAM_ACTIONS.SAVE,
                    List_modbus_var = Globals.GetTheInstance().List_sas360con_cfg
                };
                load_save.ShowDialog();

                #endregion
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Button_save_config_tab_sas360con_Click)} -> {ex.Message}");
            }
        }

        private void Button_load_config_tab_sas360con_Click(object sender, RoutedEventArgs e)
        {
            string ini_dir = $"{AppDomain.CurrentDomain.BaseDirectory}{Constants.SAS360CON_CFG_dir}";
            if (!Directory.Exists(ini_dir))
            {
                Directory.CreateDirectory(ini_dir);
            }

            var fileDialog = new OpenFileDialog
            {
                InitialDirectory = ini_dir,
                Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*"
            };
            if (fileDialog.ShowDialog() == true)
            {
                m_collection_sas360con_cfg.Clear();

                //Guardar valores del array modbus de lectura en el array load en la posición asociada
                List<Modbus_var> list_modbus_var = Manage_memory.Load_memory_list_from_csv(fileDialog.FileName);
                list_modbus_var.ForEach(modbus_var_load =>
                {

                    Modbus_var? modbus_var_sas360con = Globals.GetTheInstance().List_sas360con_cfg.FirstOrDefault(sas360_config => modbus_var_load.Addr == sas360_config.Addr && sas360_config.Value != null);
                    if (modbus_var_sas360con != null)
                    {
                        if (modbus_var_sas360con.Value != null)
                        {
                            modbus_var_load.Value = modbus_var_sas360con.Value;
                        }
                    }
                });

                LoadSaveConfigConWindow load_save = new()
                {
                    Program_actions = PROGRAM_ACTIONS.LOAD,
                    List_modbus_var = list_modbus_var
                };
                load_save.ShowDialog();

                if (load_save.Load_save_ok)
                {
                    #region Refresh SAS360CON config lists

                    Globals.GetTheInstance().List_sas360con_cfg.ForEach(config_sas360 => m_collection_sas360con_cfg.Add(config_sas360));
                    m_collection_sas360con_cfg = new ObservableCollection<Modbus_var>(m_collection_sas360con_cfg.OrderBy(sas360_config => sas360_config.Addr));
                    Listview_sas360con_cfg.ItemsSource = m_collection_sas360con_cfg;
                    CollectionViewSource.GetDefaultView(Listview_sas360con_cfg.ItemsSource).Refresh();

                    #endregion

                    Manage_memory.Load_SAS360CON_cfg_modbus_data();
                    Update_SAS360CON_cfg_memory_info();

                    MessageBox.Show("Sas360CON CFG data loaded", "OK", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);

                    Button_send_config_tab_sas360con.FontSize = 18;
                    Button_send_config_tab_sas360con.Foreground = new SolidColorBrush(Colors.DarkRed);
                }
            }
        }

        private void Button_send_config_tab_sas360con_Click(object sender, RoutedEventArgs e)
        {
            if (!m_is_writing_all_config)
            {
                #region Clear all controls

                Image_SAS360CON_CFG_installation_ok.Visibility = Visibility.Collapsed;
                Image_SAS360CON_CFG_installation_error.Visibility = Visibility.Collapsed;
                Image_SAS360CON_CFG_installation_warning.Visibility = Visibility.Collapsed;

                Image_SAS360CON_CFG_vehicle_config_ok.Visibility = Visibility.Collapsed;
                Image_SAS360CON_CFG_vehicle_config_error.Visibility = Visibility.Collapsed;
                Image_SAS360CON_CFG_vehicle_config_warning.Visibility = Visibility.Collapsed;

                Image_SAS360CON_CFG_detection_area_ok.Visibility = Visibility.Collapsed;
                Image_SAS360CON_CFG_detection_area_error.Visibility = Visibility.Collapsed;
                Image_SAS360CON_CFG_detection_area_warning.Visibility = Visibility.Collapsed;

                Image_SAS360CON_CFG_uwb_com_config_ok.Visibility = Visibility.Collapsed;
                Image_SAS360CON_CFG_uwb_com_config_error.Visibility = Visibility.Collapsed;
                Image_SAS360CON_CFG_uwb_com_config_warning.Visibility = Visibility.Collapsed;

                Image_SAS360CON_CFG_miscellaneous_ok.Visibility = Visibility.Collapsed;
                Image_SAS360CON_CFG_miscellaneous_error.Visibility = Visibility.Collapsed;
                Image_SAS360CON_CFG_miscellaneous_warning.Visibility = Visibility.Collapsed;

                #endregion

                Button_send_config_tab_sas360con.FontSize = 14;
                Button_send_config_tab_sas360con.Foreground = new SolidColorBrush(Colors.Black);
                Stackpanel_edit_sas360con.IsEnabled = false;

                m_is_writing_all_config = true;
                m_write_all_config_state = COMMAND_WRITE_LOCATION.INTERNAL_CONFIG;
                m_timer_write_all_sas360con_config.Start();
            }
        }

        private void Timer_write_all_sas360con_config_Tick(object sender, EventArgs e)
        {
            m_timer_write_all_sas360con_config.Stop();

            bool restart_timer = true;

            Dispatcher.Invoke(() =>
            {
                switch (m_write_all_config_state)
                {
                    case COMMAND_WRITE_LOCATION.INTERNAL_CONFIG:
                        {
                            Send_SAS360CON_CFG_installation();
                            m_write_all_config_state = COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_VEHICLE_CONFIG;
                            break;
                        }

                    case COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_VEHICLE_CONFIG:
                        {
                            Send_SAS360CON_CFG_vehicle_config();
                            m_write_all_config_state = COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_DETECTION_AREA;
                            break;
                        }

                    case COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_DETECTION_AREA:
                        {
                            Send_SAS360CON_CFG_detection_area();
                            m_write_all_config_state = COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_UWB_COMM;
                            break;
                        }

                    case COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_UWB_COMM:
                        {
                            Send_SAS360CON_CFG_uwb_com_config();
                            m_write_all_config_state = COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_MISCELLANEOUS;
                            break;
                        }

                    case COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_MISCELLANEOUS:
                        {
                            Send_SAS360CON_CFG_miscellaneous();
                            m_write_all_config_state = COMMAND_WRITE_LOCATION.NOT_DEFINED;
                            break;
                        }

                    default:
                        {
                            Stackpanel_edit_sas360con.IsEnabled = true;
                            m_is_writing_all_config = false;
                            restart_timer = false;
                            break;
                        }
                }

            });

            if (restart_timer)
                m_timer_write_all_sas360con_config.Start();
        }

        #endregion


        #region Write sas360con config por partes

        private void Timer_write_struct_sas360con_config_Tick(object sender, EventArgs e)
        {
            m_timer_write_struct_sas360con_config.Stop();
            m_timer_write_timeout.Start();

            Dispatcher.Invoke(() =>
            {
                switch (m_selected_command_write_location)
                {
                    case COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_INSTALLATION:
                        {
                            try
                            {
                                Manage_logs.SaveLogValue("WRITE SAS360CON CONFIG INSTALLATION");

                                ushort start_address = (ushort)(Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + MEMORY_SAS360CON_CFG_FIELD_POS_INI.INSTALLATION);
                                int[] array_values = new int[(int)MEMORY_SAS360CON_CFG_FIELD_SIZE.INSTALLATION];

                                m_list_textbox_sas360con_cfg_installation
                                    .Select((Value, Index) => new { Control = Value, Position = Index }).ToList()
                                    .ForEach(config_value =>
                                    {
                                        if (int.TryParse(config_value.Control.Text.ToString(), out int value))
                                            array_values[config_value.Position] = value;

                                    });

                                array_values[3] = (int)DecimalUpDown_audio_language.Value!;
                                array_values[4] = (int)DecimalUpDown_audio_volume.Value!;

                                Globals.GetTheInstance().ManageComThread.Write_multiple_registers(start_address, array_values, MEMORY_CONFIG_TYPE.SAS360CON_CFG);
                            }
                            catch (Exception ex)
                            {
                                Image_SAS360CON_CFG_installation_error.Visibility = Visibility.Visible;
                                Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Timer_write_struct_sas360con_config_Tick)} (SAS360CON_CONFIG_INSTALLATION) -> {ex.Message}");
                            }

                            break;
                        }

                    case COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_VEHICLE_CONFIG:
                        {
                            try
                            {
                                Manage_logs.SaveLogValue("WRITE SAS360CON CONFIG VEHICLE CONFIG");

                                ushort start_address = (ushort)(Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + MEMORY_SAS360CON_CFG_FIELD_POS_INI.VEHICLE_CFG);

                                #region Get data from controls

                                double d_value;
                                decimal dec_value;

                                List<int> list_values = new();

                                #region Dimensions

                                if (double.TryParse(Textbox_config_vehicle_dim_x.Text, out d_value))
                                    list_values.Add((int)decimal.Multiply((decimal)d_value, 100));

                                if (double.TryParse(Textbox_config_vehicle_dim_y.Text, out d_value))
                                    list_values.Add((int)decimal.Multiply((decimal)d_value, 100));

                                #endregion

                                #region Antenna pos

                                if (decimal.TryParse(Textbox_config_antena1_pos_x.Text, out dec_value))
                                    list_values.Add((int)decimal.Multiply(dec_value, 100));

                                if (decimal.TryParse(Textbox_config_antena1_pos_y.Text, out dec_value))
                                    list_values.Add((int)decimal.Multiply(dec_value, 100));

                                if (decimal.TryParse(Textbox_config_antena2_pos_x.Text, out dec_value))
                                    list_values.Add((int)decimal.Multiply(dec_value, 100));

                                if (decimal.TryParse(Textbox_config_antena2_pos_y.Text, out dec_value))
                                    list_values.Add((int)decimal.Multiply(dec_value, 100));

                                if (decimal.TryParse(Textbox_config_antena3_pos_x.Text, out dec_value))
                                    list_values.Add((int)decimal.Multiply(dec_value, 100));

                                if (decimal.TryParse(Textbox_config_antena3_pos_y.Text, out dec_value))
                                    list_values.Add((int)decimal.Multiply(dec_value, 100));

                                #endregion

                                #endregion

                                Globals.GetTheInstance().ManageComThread.Write_multiple_registers(start_address, list_values.ToArray(), MEMORY_CONFIG_TYPE.SAS360CON_CFG);
                            }
                            catch (Exception ex)
                            {
                                Image_SAS360CON_CFG_vehicle_config_error.Visibility = Visibility.Visible;
                                Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Timer_write_struct_sas360con_config_Tick)} (SAS360CON_CONFIG_VEHICLE_CONFIG) -> {ex.Message}");
                            }

                            break;
                        }

                    case COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_DETECTION_AREA:
                        {
                            try
                            {
                                Manage_logs.SaveLogValue("WRITE SAS360CON CONFIG DETECTION AREA");

                                ushort start_address = (ushort)(Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + MEMORY_SAS360CON_CFG_FIELD_POS_INI.DETECTION_AREA);

                                #region Get data from controls

                                int array_pos = 0;
                                int[] array_values = new int[25];

                                double d_value;

                                for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                                {
                                    if (double.TryParse(m_list_textbox_config_area_FRONT_ANRI[index].Text.ToString(), out d_value))
                                        array_values[array_pos] = (int)decimal.Multiply((decimal)d_value, 100);

                                    array_pos++;
                                }

                                for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                                {
                                    if (double.TryParse(m_list_textbox_config_area_RIGHT_ANRI[index].Text.ToString(), out d_value))
                                        array_values[array_pos] = (int)decimal.Multiply((decimal)d_value, 100);

                                    array_pos++;
                                }

                                for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                                {
                                    if (double.TryParse(m_list_textbox_config_area_BACK_ANRI[index].Text.ToString(), out d_value))
                                        array_values[array_pos] = (int)decimal.Multiply((decimal)d_value, 100);

                                    array_pos++;
                                }

                                for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                                {
                                    if (double.TryParse(m_list_textbox_config_area_LEFT_ANRI[index].Text.ToString(), out d_value))
                                        array_values[array_pos] = (int)decimal.Multiply((decimal)d_value, 100);

                                    array_pos++;
                                }


                                if (double.TryParse(Textbox_config_area_detection_distance.Text.ToString(), out d_value))
                                    array_values[array_pos] = (int)decimal.Multiply((decimal)d_value, 100);

                                array_pos++;

                                if (double.TryParse(Textbox_config_area_change_hyst.Text.ToString(), out d_value))
                                    array_values[array_pos] = (int)decimal.Multiply((decimal)d_value, 100);

                                array_pos++;

                                if (double.TryParse(Textbox_config_sector_change_hyst.Text.ToString(), out d_value))
                                    array_values[array_pos] = (int)decimal.Multiply((decimal)d_value, 100);

                                array_pos++;

                                array_values[array_pos] = (int)DecimalUpDown_config_trilat_calc_enabled.Value!;

                                array_pos++;

                                for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                                {
                                    if (double.TryParse(m_list_textbox_config_area_DIST_ANTENA_ANRI[index].Text.ToString(), out d_value))
                                        array_values[array_pos] = (int)decimal.Multiply((decimal)d_value, 100);

                                    array_pos++;
                                }

                                array_values[array_pos] = (int)DecimalUpDown_config_gestion_avanzada_pos_enable.Value!;

                                #endregion


                                Globals.GetTheInstance().ManageComThread.Write_multiple_registers(start_address, array_values, MEMORY_CONFIG_TYPE.SAS360CON_CFG);
                            }
                            catch (Exception ex)
                            {
                                Image_SAS360CON_CFG_detection_area_error.Visibility = Visibility.Visible;
                                Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Timer_write_struct_sas360con_config_Tick)} (SAS360CON_CONFIG_DETECTION_AREA) -> {ex.Message}");
                            }

                            break;
                        }

                    case COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_OUTPUT:
                        {
                            try
                            {
                                Manage_logs.SaveLogValue("WRITE SAS360CON CONFIG OUTPUT");

                                ushort start_address = (ushort)(Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + MEMORY_SAS360CON_CFG_FIELD_POS_INI.E_S);

                                #region Get data from controls

                                List<int> list_output_leds = Convert_output_leds_into_values();
                                int[] array_values = list_output_leds.ToArray();

                                #endregion

                                Globals.GetTheInstance().ManageComThread.Write_multiple_registers(start_address, array_values, MEMORY_CONFIG_TYPE.SAS360CON_CFG);
                            }
                            catch (Exception ex)
                            {
                                Image_SAS360CON_CFG_output_actions_error.Visibility = Visibility.Visible;
                                Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Timer_write_struct_sas360con_config_Tick)} (SAS360CON_CONFIG_OUTPUT) -> {ex.Message}");
                            }

                            break;
                        }

                    case COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_UWB_COMM:
                        {
                            try
                            {
                                Manage_logs.SaveLogValue("WRITE SAS360CON CONFIG UWB COMM");

                                ushort start_address = (ushort)(Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + MEMORY_SAS360CON_CFG_FIELD_POS_INI.UWB_COM);
                                List<int> list_values = new();

                                #region Get data from controls

                                for (int index = 0; index < m_list_textbox_sas360con_cfg_uwb_communication.Count; index++)
                                {
                                    if (byte.TryParse(m_list_textbox_sas360con_cfg_uwb_communication[index].Text, out byte byte_value_1) && byte.TryParse(m_list_textbox_sas360con_cfg_uwb_communication[index + 1].Text, out byte byte_value_2))
                                    {
                                        byte[] array_byte_value = new byte[2] { byte_value_1, byte_value_2 };
                                        ushort i_value = BitConverter.ToUInt16(array_byte_value);
                                        list_values.Add(i_value);
                                    }

                                    index++;
                                }

                                #endregion

                                Globals.GetTheInstance().ManageComThread.Write_multiple_registers(start_address, list_values.ToArray(), MEMORY_CONFIG_TYPE.SAS360CON_CFG);
                            }
                            catch (Exception ex)
                            {
                                Image_SAS360CON_CFG_uwb_com_config_error.Visibility = Visibility.Visible;
                                Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Timer_write_struct_sas360con_config_Tick)} (SAS360CON_CONFIG_UWB_COMM) -> {ex.Message}");
                            }

                            break;
                        }

                    case COMMAND_WRITE_LOCATION.SAS360CON_CFG_RECORDING:
                        {
                            try
                            {
                                Manage_logs.SaveLogValue("WRITE SAS360CON CFG RECORDING");

                                ushort start_address = (ushort)(Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + MEMORY_SAS360CON_CFG_FIELD_POS_INI.RECORDING);

                                #region Get data from the controls

                                int[] array_values = new int[Constants.RECORDING_REG_SAS360CON_ARRAY * 3];

                                for (int index = 0; index < Constants.RECORDING_REG_SAS360CON_ARRAY; index++)
                                {
                                    if (byte.TryParse(m_list_textbox_sas360con_cfg_recording_index[index].Text.ToString(), out byte value))
                                        array_values[index] = value;

                                    if (byte.TryParse(m_list_textbox_sas360con_cfg_recording_unit[index].Text.ToString(), out value))
                                        array_values[index + Constants.RECORDING_REG_SAS360CON_ARRAY] = value;

                                    if (short.TryParse(m_list_textbox_sas360con_cfg_recording_unit[index].Text.ToString(), out short short_value))
                                        array_values[index + (Constants.RECORDING_REG_SAS360CON_ARRAY * 2)] = short_value;

                                }

                                #endregion

                                Globals.GetTheInstance().ManageComThread.Write_multiple_registers(start_address, array_values, MEMORY_CONFIG_TYPE.SAS360CON_CFG);
                            }
                            catch (Exception ex)
                            {
                                Image_SAS360CON_CFG_recordings_error.Visibility = Visibility.Visible;
                                Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Timer_write_struct_sas360con_config_Tick)} (SAS360CON_CFG_RECORDING) -> {ex.Message}");
                            }

                            break;
                        }

                    case COMMAND_WRITE_LOCATION.SAS360CON_CONFIG_MISCELLANEOUS:
                        {
                            try
                            {
                                Manage_logs.SaveLogValue("WRITE SAS360CON CONFIG MISCELLANEOUS");


                                List<int> list_values = new();

                                ushort start_address = (ushort)(Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + MEMORY_SAS360CON_CFG_FIELD_POS_INI.TEMP_FILTERS);
                                ushort end_address = (ushort)(Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + MEMORY_SAS360CON_CFG_FIELD_POS_INI.RECORDING);

                                int first_list_miscellaneous = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == start_address);
                                int last_list_miscellaneous = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == end_address);

                                //Guardar una copia de los datos
                                bool is_first_byte = true;
                                byte first_byte = 0;
                                for (int index_list = first_list_miscellaneous; index_list < last_list_miscellaneous; index_list++)
                                {
                                    Modbus_var modbus_var = Globals.GetTheInstance().List_sas360con_cfg[index_list];
                                    if (modbus_var.TypeName.Equals("Byte"))
                                    {
                                        if (is_first_byte)
                                        {
                                            first_byte = modbus_var.Value;
                                            is_first_byte = false;
                                        }
                                        else
                                        {
                                            byte[] array_byte = { first_byte, modbus_var.Value };
                                            list_values.Add(BitConverter.ToUInt16(array_byte));
                                            is_first_byte = true;
                                        }
                                    }
                                    else if (modbus_var.TypeName.Equals("UInt32") || modbus_var.TypeName.Equals("Int32") || modbus_var.TypeName.Equals("UTC"))
                                    {
                                        byte[] array_values = BitConverter.GetBytes(modbus_var.Value);
                                        ushort first_value = BitConverter.ToUInt16(array_values, 0);
                                        list_values.Add(first_value);
                                        ushort second_value = BitConverter.ToUInt16(array_values, 2);
                                        list_values.Add(second_value);
                                    }
                                    else
                                    {
                                        list_values.Add(modbus_var.Value);
                                    }
                                }


                                //Los 3 primeros temporizadores y filtros
                                if (int.TryParse(Textbox_miscellaneous_output_deactivation_delay.Text.ToString(), out int i_value))
                                    list_values[0] = i_value;

                                if (double.TryParse(Textbox_miscellaneous_area_zone_dist.Text, out double d_value))
                                    list_values[1] = (int)decimal.Multiply((decimal)d_value, 100);

                                if (int.TryParse(Textbox_miscellaneous_red_zone_alert_audio_repeat_sec.Text, out i_value))
                                    list_values[2] = i_value;

                                //el último UWB tag config                              
                                if (int.TryParse(Textbox_miscellaneous_tag_list_clear_timeout.Text.ToString(), out i_value))
                                    list_values[20] = i_value;


                                Globals.GetTheInstance().ManageComThread.Write_multiple_registers(start_address, list_values.ToArray(), MEMORY_CONFIG_TYPE.SAS360CON_CFG);


                            }
                            catch (Exception ex)
                            {
                                Image_SAS360CON_CFG_miscellaneous_error.Visibility = Visibility.Visible;
                                Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Timer_write_struct_sas360con_config_Tick)} (SAS360CON_CONFIG_MISCELLANEOUS) -> {ex.Message}");
                            }

                            break;
                        }
                }
            });
        }

        #endregion

        #endregion

        #endregion

        #region MEMORY TAB

        #region CSV

        #region Generate csv
        private void Button_generate_csv_Click(object sender, RoutedEventArgs e)
        {
            List<Modbus_var> list_modbus_var = Manage_memory.Generate_csv_data(Textbox_generate_csv.Text);
            try
            {
                MessageBoxResult result = MessageBox.Show("Convert data to CSV format?", "INFO", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly);
                if (result == MessageBoxResult.Yes)
                {
                    Textbox_generate_csv.Clear();
                    string header = "Addr;Name;TypeName;Unit;Format";
                    Textbox_generate_csv.AppendText($"{header}{Environment.NewLine}");
                    list_modbus_var.ForEach(modbus_var =>
                    {
                        string s_line = $"{modbus_var.Addr};{modbus_var.Name};{modbus_var.TypeName};{modbus_var.Unit};{modbus_var.Format}";
                        Textbox_generate_csv.AppendText($"{s_line}{Environment.NewLine}");
                    });

                    MessageBox.Show("CSV conversion finished", "INFO", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{GetType().Name} -> {nameof(Button_generate_csv_Click)} -> {ex.Message}");
            }
        }

        #endregion

        #region Save csv

        private void Button_save_csv_Click(object sender, RoutedEventArgs e)
        {
            if (Textbox_generate_csv.Text != string.Empty)
                Manage_memory.Save_csv_file(Textbox_generate_csv.Text);
        }

        #endregion

        #region Clear
        private void Button_clear_csv_Click(object sender, RoutedEventArgs e)
        {
            Textbox_generate_csv.Clear();
        }

        #endregion

        #region Create structure array

        private void Button_create_structure_array_csv_Click(object sender, RoutedEventArgs e)
        {
            RADIO_CSV_TAG_ZONE_TYPE tag_zone_type =
                Radiobutton_tag_generate_csv.IsChecked == true ? RADIO_CSV_TAG_ZONE_TYPE.TAG :
                Radiobutton_zone_generate_csv.IsChecked == true ? RADIO_CSV_TAG_ZONE_TYPE.ZONE : RADIO_CSV_TAG_ZONE_TYPE.NONE;


            RADIO_CSV_MEMORY_TYPE memory_type =
                Radiobutton_none_generate_csv.IsChecked == true ? RADIO_CSV_MEMORY_TYPE.NONE :
                Radiobutton_base_generate_csv.IsChecked == true ? RADIO_CSV_MEMORY_TYPE.BASE :
                Radiobutton_base_generate_csv.IsChecked == true ? RADIO_CSV_MEMORY_TYPE.EXTENDED : RADIO_CSV_MEMORY_TYPE.NONE;

            List<Modbus_var> list_modbus_var = Manage_memory.Create_structure_array(Textbox_generate_csv.Text, (int)DecimalUpDown_num_array_generate_csv.Value!, (int)DecimalUpDown_num_uwb_generate_csv.Value!, tag_zone_type, memory_type);
            try
            {
                Textbox_generate_csv.Clear();
                string header = "Addr;Name;TypeName;Unit;Format";
                Textbox_generate_csv.AppendText($"{header}{Environment.NewLine}");
                list_modbus_var.ForEach(modbus_var =>
                {
                    string s_line = $"{modbus_var.Addr};{modbus_var.Name};{modbus_var.TypeName};{modbus_var.Unit};{modbus_var.Format}";
                    Textbox_generate_csv.AppendText($"{s_line}{Environment.NewLine}");
                });

                MessageBox.Show("Structure array created", "INFO", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{GetType().Name} -> {nameof(Button_create_structure_array_csv_Click)} -> {ex.Message}");
            }
        }

        #endregion

        #endregion

        #region Refresh list button

        private void Button_refresh_lists_Click(object sender, RoutedEventArgs e)
        {
            int index = Array.IndexOf(m_list_button_refresh_memory.ToArray(), sender);

            Load_lists();

            Manage_memory_data(MEMORY_CONFIG_TYPE.SAS360CON_INTERNAL_CFG);
            Manage_memory_data(MEMORY_CONFIG_TYPE.SAS360CON_CFG);
            Manage_memory_data(MEMORY_CONFIG_TYPE.IOT_CFG);
            Manage_memory_data(MEMORY_CONFIG_TYPE.UWB_INTERNAL_CFG);
        }

        #endregion

        #region new var

        private void Button_new_var_Click(object sender, RoutedEventArgs e)
        {
            int index = Array.IndexOf(m_list_button_new_memory.ToArray(), sender);

        }

        #endregion

        #region Delete var
        private void Button_delete_var_Click(object sender, RoutedEventArgs e)
        {
            int index = Array.IndexOf(m_list_button_delete_memory.ToArray(), sender);
        }

        #endregion


        #region Listview memory events

        #region Listview memory selection changed

        private void Listview_memory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index_list = m_list_listview_memory.IndexOf(sender as ListView);

            List<Modbus_var> list_modbus_var_selection =
                index_list == (int)MEMORY_CONFIG_TYPE.SAS360CON_INTERNAL_CFG ? Globals.GetTheInstance().List_sas360con_internal_cfg :
                index_list == (int)MEMORY_CONFIG_TYPE.SAS360CON_CFG ? Globals.GetTheInstance().List_sas360con_cfg :
                index_list == (int)MEMORY_CONFIG_TYPE.IOT_CFG ? Globals.GetTheInstance().List_iot_cfg :
                index_list == (int)MEMORY_CONFIG_TYPE.SAS360CON_IMAGE ? Globals.GetTheInstance().List_sas360con_image :
                index_list == (int)MEMORY_CONFIG_TYPE.IOT_IMAGE ? Globals.GetTheInstance().List_iot_image :

                index_list == (int)MEMORY_CONFIG_TYPE.SAS360CON_MAINTENNANCE ? Globals.GetTheInstance().List_sas360con_maintennance :

                index_list == (int)MEMORY_CONFIG_TYPE.UWB_INTERNAL_CFG ? Globals.GetTheInstance().List_uwb_internal_cfg :
                index_list == (int)MEMORY_CONFIG_TYPE.UWB_IMAGE ? Globals.GetTheInstance().List_uwb_image :

                index_list == (int)MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE_1 ? Globals.GetTheInstance().List_console_closest_tags_base :
                index_list == (int)MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_EXTENDED ? Globals.GetTheInstance().List_console_closest_tags_extended :
                index_list == (int)MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_BASE_1 ? Globals.GetTheInstance().List_console_closest_zone_base :
                index_list == (int)MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_EXTENDED ? Globals.GetTheInstance().List_console_closest_zone_extended :

                index_list == (int)MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_BASE ? Globals.GetTheInstance().List_uwb_closest_tags_base :
                index_list == (int)MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_EXTENDED ? Globals.GetTheInstance().List_uwb_closest_tags_extended :

                index_list == (int)MEMORY_CONFIG_TYPE.SAS360CON_NVREG ? Globals.GetTheInstance().List_sas360con_nvreg :

                new List<Modbus_var>();


            var item = (sender as ListView)?.SelectedItem;
            if (item != null)
            {
                if (item is Modbus_var)
                {
                    Modbus_var? modbus_var = item as Modbus_var;
                    m_selected_modbus_var = list_modbus_var_selection.First(modbus_var => modbus_var.Name.Equals(modbus_var!.Name));
                }
            }
        }

        #endregion

        #region Listview memory double click

        private void Listview_memory_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index_list = m_list_listview_memory.IndexOf(sender as ListView);

            var item = (sender as ListView)?.SelectedItem;
            if (item != null)
            {
                Modbus_var? modbus_var = item as Modbus_var;
                double addr_temp = modbus_var!.Addr;

                SettingModbusVarWindow setting_var = new()
                {
                    Modbus_var = modbus_var
                };

                if (setting_var.ShowDialog() == true)
                {
                    List<Modbus_var> list_modbus_var_change =
                        index_list == (int)MEMORY_CONFIG_TYPE.SAS360CON_INTERNAL_CFG ? Globals.GetTheInstance().List_sas360con_internal_cfg :
                        index_list == (int)MEMORY_CONFIG_TYPE.SAS360CON_CFG ? Globals.GetTheInstance().List_sas360con_cfg :
                        index_list == (int)MEMORY_CONFIG_TYPE.IOT_CFG ? Globals.GetTheInstance().List_iot_cfg :
                        index_list == (int)MEMORY_CONFIG_TYPE.SAS360CON_IMAGE ? Globals.GetTheInstance().List_sas360con_image :
                        index_list == (int)MEMORY_CONFIG_TYPE.IOT_IMAGE ? Globals.GetTheInstance().List_iot_image :

                        index_list == (int)MEMORY_CONFIG_TYPE.SAS360CON_MAINTENNANCE ? Globals.GetTheInstance().List_sas360con_maintennance :

                        index_list == (int)MEMORY_CONFIG_TYPE.UWB_INTERNAL_CFG ? Globals.GetTheInstance().List_uwb_internal_cfg :
                        index_list == (int)MEMORY_CONFIG_TYPE.UWB_IMAGE ? Globals.GetTheInstance().List_uwb_image :

                        index_list == (int)MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE_1 ? Globals.GetTheInstance().List_console_closest_tags_base :
                        index_list == (int)MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_EXTENDED ? Globals.GetTheInstance().List_console_closest_tags_extended :
                        index_list == (int)MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_BASE_1 ? Globals.GetTheInstance().List_console_closest_zone_base :
                        index_list == (int)MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_EXTENDED ? Globals.GetTheInstance().List_console_closest_zone_extended :

                        index_list == (int)MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_BASE ? Globals.GetTheInstance().List_uwb_closest_tags_base :
                        index_list == (int)MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_EXTENDED ? Globals.GetTheInstance().List_uwb_closest_tags_extended :

                        index_list == (int)MEMORY_CONFIG_TYPE.SAS360CON_NVREG ? Globals.GetTheInstance().List_sas360con_nvreg :

                        new List<Modbus_var>();

                    int index_change = list_modbus_var_change.FindIndex(internal_config => internal_config.Addr == addr_temp);
                    list_modbus_var_change[index_change] = modbus_var;


                    bool save_ok = Manage_memory.Save_modbus_var((MEMORY_CONFIG_TYPE)index_list);
                    if (!save_ok)
                        MessageBox.Show("Error saving internal config.", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);

                    else
                        Load_lists();

                }
            }
        }

        #endregion

        #endregion

        #region CON TAGS index changed

        private void DecimalUpDown_index_con_tags_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Xceed.Wpf.Toolkit.DecimalUpDown? current_decimal_updown = sender as Xceed.Wpf.Toolkit.DecimalUpDown;

            if (m_collection_console_closest_tags_base != null)
            {
                int first_memory_pos_tag_base = 0;
                int last_memory_pos_tag_base = 0;
                if (current_decimal_updown!.Value != 0)
                {
                    first_memory_pos_tag_base = Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.CONSOLE_CLOSEST_TAGS_BASE] + (Constants.TAGS_BASE_CON_STRUCT_NUM_REG * ((int)current_decimal_updown.Value! - 1));
                    last_memory_pos_tag_base = first_memory_pos_tag_base + Constants.TAGS_BASE_CON_STRUCT_NUM_REG;
                }
                else
                {
                    first_memory_pos_tag_base = Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.CONSOLE_CLOSEST_TAGS_BASE];
                    last_memory_pos_tag_base = first_memory_pos_tag_base + Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.CONSOLE_CLOSEST_TAGS_BASE];
                }

                m_collection_console_closest_tags_base.Clear();

                Globals.GetTheInstance().List_console_closest_tags_base
                    .Where(closest => (closest.Addr >= first_memory_pos_tag_base) && (closest.Addr < last_memory_pos_tag_base)).ToList()
                    .ForEach(closest => m_collection_console_closest_tags_base.Add(closest));

                m_collection_console_closest_tags_base = new ObservableCollection<Modbus_var>(m_collection_console_closest_tags_base.OrderBy(closest => closest.Addr));
                Listview_console_closest_tags_base.ItemsSource = m_collection_console_closest_tags_base;
                CollectionViewSource.GetDefaultView(Listview_console_closest_tags_base.ItemsSource).Refresh();
            }
        }

        #endregion

        #region UWB TAGS index changed

        private void DecimalUpDown_index_uwb_tags_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_collection_uwb_closest_tags_base != null)
            {
                Xceed.Wpf.Toolkit.DecimalUpDown? current_decimal_updown = sender as Xceed.Wpf.Toolkit.DecimalUpDown;

                int first_memory_pos_tag_base_uwb = 0;
                int last_memory_pos_tag_base_uwb = 0;

                int first_memory_pos_tag_extended_uwb = 0;
                int last_memory_pos_tag_extended_uwb = 0;

                if (current_decimal_updown!.Value != 0)
                {
                    first_memory_pos_tag_base_uwb = Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.UWB_CLOSEST_TAGS_BASE] + (Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.UWB_CLOSEST_TAGS_BASE] * ((int)current_decimal_updown.Value! - 1));
                    last_memory_pos_tag_base_uwb = first_memory_pos_tag_base_uwb + Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.UWB_CLOSEST_TAGS_BASE];

                    first_memory_pos_tag_extended_uwb = Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.UWB_CLOSEST_TAGS_EXTENDED] + (Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.UWB_CLOSEST_TAGS_EXTENDED] * ((int)current_decimal_updown.Value - 1));
                    last_memory_pos_tag_extended_uwb = first_memory_pos_tag_extended_uwb + Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.UWB_CLOSEST_TAGS_EXTENDED];
                }
                else
                {
                    first_memory_pos_tag_base_uwb = Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.UWB_CLOSEST_TAGS_BASE];
                    last_memory_pos_tag_base_uwb = Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.UWB_CLOSEST_TAGS_BASE] + (Globals.GetTheInstance().Total_closest_tags * Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.UWB_CLOSEST_TAGS_BASE]);

                    first_memory_pos_tag_extended_uwb = Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.UWB_CLOSEST_TAGS_EXTENDED];
                    last_memory_pos_tag_extended_uwb = Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.UWB_CLOSEST_TAGS_EXTENDED] + (Globals.GetTheInstance().Total_closest_tags * Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.UWB_CLOSEST_TAGS_EXTENDED]);
                }


                //BASE
                m_collection_uwb_closest_tags_base.Clear();

                Globals.GetTheInstance().List_uwb_closest_tags_base
                    .Where(closest => (closest.Addr >= first_memory_pos_tag_base_uwb) && (closest.Addr < last_memory_pos_tag_base_uwb)).ToList()
                    .ForEach(closest => m_collection_uwb_closest_tags_base.Add(closest));

                m_collection_uwb_closest_tags_base = new ObservableCollection<Modbus_var>(m_collection_uwb_closest_tags_base.OrderBy(closest => closest.Addr));
                Listview_uwb_closest_tags_base.ItemsSource = m_collection_uwb_closest_tags_base;
                CollectionViewSource.GetDefaultView(Listview_uwb_closest_tags_base.ItemsSource).Refresh();


                //EXTENDED
                m_collection_uwb_closest_tags_extended.Clear();

                Globals.GetTheInstance().List_uwb_closest_tags_extended
                    .Where(closest => (closest.Addr >= first_memory_pos_tag_extended_uwb) && (closest.Addr < last_memory_pos_tag_extended_uwb)).ToList()
                    .ForEach(closest => m_collection_uwb_closest_tags_extended.Add(closest));

                m_collection_uwb_closest_tags_extended = new ObservableCollection<Modbus_var>(m_collection_uwb_closest_tags_extended.OrderBy(closest => closest.Addr));
                Listview_uwb_closest_tags_extended.ItemsSource = m_collection_uwb_closest_tags_extended;
                CollectionViewSource.GetDefaultView(Listview_uwb_closest_tags_extended.ItemsSource).Refresh();
            }
        }

        #endregion


        #region Tooglebutton sas360con cfg sections

        private void ToggleButton_sas360con_cfg_Click(object sender, RoutedEventArgs e)
        {
            int index_control = m_list_togglebutton_sas360con_config.IndexOf(sender as ToggleButton);
            m_list_togglebutton_sas360con_config
                .Select((Value, Index) => new { Control = Value, Position = Index }).ToList()
                .Where(toogle => toogle.Position != index_control).ToList()
                .All(toogle => { toogle.Control.IsChecked = false; return true; });

            int ini_pos =
                index_control == (int)MEMORY_SAS360CON_CFG_FIELD_POS_INDEX.ESTRUCTURE ? (int)MEMORY_SAS360CON_CFG_FIELD_POS_INI.ESTRUCTURE :
                index_control == (int)MEMORY_SAS360CON_CFG_FIELD_POS_INDEX.INSTALLATION ? (int)MEMORY_SAS360CON_CFG_FIELD_POS_INI.INSTALLATION :
                index_control == (int)MEMORY_SAS360CON_CFG_FIELD_POS_INDEX.VEHICLE_CFG ? (int)MEMORY_SAS360CON_CFG_FIELD_POS_INI.VEHICLE_CFG :
                index_control == (int)MEMORY_SAS360CON_CFG_FIELD_POS_INDEX.DETECTION_AREA ? (int)MEMORY_SAS360CON_CFG_FIELD_POS_INI.DETECTION_AREA :
                index_control == (int)MEMORY_SAS360CON_CFG_FIELD_POS_INDEX.E_S ? (int)MEMORY_SAS360CON_CFG_FIELD_POS_INI.E_S :
                index_control == (int)MEMORY_SAS360CON_CFG_FIELD_POS_INDEX.TEMP_FILTERS ? (int)MEMORY_SAS360CON_CFG_FIELD_POS_INI.TEMP_FILTERS :
                index_control == (int)MEMORY_SAS360CON_CFG_FIELD_POS_INDEX.UWB_COM ? (int)MEMORY_SAS360CON_CFG_FIELD_POS_INI.UWB_COM :
                index_control == (int)MEMORY_SAS360CON_CFG_FIELD_POS_INDEX.UWB_TAG ? (int)MEMORY_SAS360CON_CFG_FIELD_POS_INI.UWB_TAG :
                index_control == (int)MEMORY_SAS360CON_CFG_FIELD_POS_INDEX.RECORDING ? (int)MEMORY_SAS360CON_CFG_FIELD_POS_INI.RECORDING :
                index_control == (int)MEMORY_SAS360CON_CFG_FIELD_POS_INDEX.RESERVED_FUTURE ? (int)MEMORY_SAS360CON_CFG_FIELD_POS_INI.RESERVED_FUTURE :
                index_control == (int)MEMORY_SAS360CON_CFG_FIELD_POS_INDEX.CALCULADAS ? (int)MEMORY_SAS360CON_CFG_FIELD_POS_INI.CALCULADAS :
                (int)MEMORY_SAS360CON_CFG_FIELD_POS_INI.ESTRUCTURE;

            int fin_pos =
                index_control == (int)MEMORY_SAS360CON_CFG_FIELD_POS_INDEX.ESTRUCTURE ? (int)MEMORY_SAS360CON_CFG_FIELD_POS_INI.INSTALLATION :
                index_control == (int)MEMORY_SAS360CON_CFG_FIELD_POS_INDEX.INSTALLATION ? (int)MEMORY_SAS360CON_CFG_FIELD_POS_INI.VEHICLE_CFG :
                index_control == (int)MEMORY_SAS360CON_CFG_FIELD_POS_INDEX.VEHICLE_CFG ? (int)MEMORY_SAS360CON_CFG_FIELD_POS_INI.DETECTION_AREA :
                index_control == (int)MEMORY_SAS360CON_CFG_FIELD_POS_INDEX.DETECTION_AREA ? (int)MEMORY_SAS360CON_CFG_FIELD_POS_INI.E_S :
                index_control == (int)MEMORY_SAS360CON_CFG_FIELD_POS_INDEX.E_S ? (int)MEMORY_SAS360CON_CFG_FIELD_POS_INI.TEMP_FILTERS :
                index_control == (int)MEMORY_SAS360CON_CFG_FIELD_POS_INDEX.TEMP_FILTERS ? (int)MEMORY_SAS360CON_CFG_FIELD_POS_INI.UWB_COM :
                index_control == (int)MEMORY_SAS360CON_CFG_FIELD_POS_INDEX.UWB_COM ? (int)MEMORY_SAS360CON_CFG_FIELD_POS_INI.UWB_TAG :
                index_control == (int)MEMORY_SAS360CON_CFG_FIELD_POS_INDEX.UWB_TAG ? (int)MEMORY_SAS360CON_CFG_FIELD_POS_INI.RECORDING :
                index_control == (int)MEMORY_SAS360CON_CFG_FIELD_POS_INDEX.RECORDING ? (int)MEMORY_SAS360CON_CFG_FIELD_POS_INI.RESERVED_FUTURE :
                index_control == (int)MEMORY_SAS360CON_CFG_FIELD_POS_INDEX.RESERVED_FUTURE ? (int)MEMORY_SAS360CON_CFG_FIELD_POS_INI.CALCULADAS :
                index_control == (int)MEMORY_SAS360CON_CFG_FIELD_POS_INDEX.CALCULADAS ? Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] :
                Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG];

            #region Filtrar variables en la lista

            Globals.GetTheInstance().List_sas360con_cfg_filter.Clear();
            Globals.GetTheInstance().List_sas360con_cfg.ForEach(modbus_var =>
            {
                if (modbus_var.Addr >= (Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + ini_pos) && (modbus_var.Addr < Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + fin_pos))
                    Globals.GetTheInstance().List_sas360con_cfg_filter.Add(modbus_var);
            });

            m_collection_sas360con_cfg.Clear();

            Globals.GetTheInstance().List_sas360con_cfg_filter.ForEach(config => m_collection_sas360con_cfg.Add(config));
            m_collection_sas360con_cfg = new ObservableCollection<Modbus_var>(m_collection_sas360con_cfg.OrderBy(config => config.Addr));
            Listview_sas360con_cfg.ItemsSource = m_collection_sas360con_cfg;
            CollectionViewSource.GetDefaultView(Listview_sas360con_cfg.ItemsSource).Refresh();

            #endregion
        }

        #endregion

        #region Tooglebutton sas360con image sections

        private void ToggleButton_sas360con_image_Click(object sender, RoutedEventArgs e)
        {
            int index_control = m_list_togglebutton_sas360con_image.IndexOf(sender as ToggleButton);
            m_list_togglebutton_sas360con_image
                .Select((Value, Index) => new { Control = Value, Position = Index }).ToList()
                .Where(toogle => toogle.Position != index_control).ToList()
                .All(toogle => { toogle.Control.IsChecked = false; return true; });

            int ini_pos =
                index_control == (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INDEX.ESTADOS_BOOLEANOS ? (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INI.ESTADOS_BOOLEANOS :
                index_control == (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INDEX.EA_SENSORES ? (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INI.EA_SENSORES :
                index_control == (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INDEX.TIEMPO_PROCESADO ? (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INI.TIEMPO_PROCESADO :
                index_control == (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INDEX.NVREG ? (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INI.NVREG :
                index_control == (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INDEX.MAIN ? (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INI.MAIN :
                index_control == (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INDEX.LIN_POLLING ? (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INI.LIN_POLLING :
                index_control == (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INDEX.PROCESSED_TAGS ? (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INI.PROCESSED_TAGS :
                index_control == (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INDEX.FIELD_POS ? (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INI.FIELD_POS :
                (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INI.ESTADOS_BOOLEANOS;

            int fin_pos =
                index_control == (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INDEX.ESTADOS_BOOLEANOS ? (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INI.EA_SENSORES :
                index_control == (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INDEX.EA_SENSORES ? (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INI.TIEMPO_PROCESADO :
                index_control == (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INDEX.TIEMPO_PROCESADO ? (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INI.NVREG :
                index_control == (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INDEX.NVREG ? (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INI.MAIN :
                index_control == (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INDEX.MAIN ? (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INI.LIN_POLLING :
                index_control == (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INDEX.LIN_POLLING ? (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INI.PROCESSED_TAGS :
                index_control == (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INDEX.PROCESSED_TAGS ? (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INI.FIELD_POS :
                index_control == (int)MEMORY_SAS360CON_IMAGE_FIELD_POS_INDEX.FIELD_POS ? Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE] :
                Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE];


            #region Filtrar variables en la lista

            Globals.GetTheInstance().List_sas360con_image_filter.Clear();
            Globals.GetTheInstance().List_sas360con_image.ForEach(modbus_var =>
            {
                if (modbus_var.Addr >= (Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE] + ini_pos) && (modbus_var.Addr < Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE] + fin_pos))
                    Globals.GetTheInstance().List_sas360con_image_filter.Add(modbus_var);
            });

            m_collection_sas360con_image.Clear();

            Globals.GetTheInstance().List_sas360con_image_filter.ForEach(image => m_collection_sas360con_image.Add(image));
            m_collection_sas360con_image = new ObservableCollection<Modbus_var>(m_collection_sas360con_image.OrderBy(image => image.Addr));
            Listview_sas360con_image.ItemsSource = m_collection_sas360con_image;
            CollectionViewSource.GetDefaultView(Listview_sas360con_image.ItemsSource).Refresh();

            #endregion

        }

        #endregion

        #endregion

        #region MAINTENANCE TAB

        #region Commands

        #region Listview commands events

        private void Listview_commands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Listview_sas360con_commands.SelectedItems.Count > 0)
            {
                if (Listview_sas360con_commands.SelectedItem is Modbus_command)
                {
                    m_list_wrappanel_general_commands.ForEach(wrappanel => wrappanel.Visibility = Visibility.Collapsed);

                    Modbus_command? selected_command = Listview_sas360con_commands.SelectedItem as Modbus_command;
                    m_selected_modbus_command = Globals.GetTheInstance().List_sas360con_commands.First(modbus_var => modbus_var.Name.Equals(selected_command!.Name));

                    Label_command_name.Content = m_selected_modbus_command.Name;

                    selected_command!.List_param
                        .Select((Value, Index) => new { Name = Value, Pos = Index }).ToList()
                        .ForEach(param =>
                        {
                            m_list_wrappanel_general_commands[param.Pos].Visibility = param.Name == string.Empty ? Visibility.Collapsed : Visibility.Visible;
                            m_list_decimalupdown_general_value_commands[param.Pos].Value = 0;

                            if (param.Name != string.Empty)
                            {
                                m_list_label_general_param_commands[param.Pos].Content = param.Name;
                                m_list_label_general_type_commands[param.Pos].Content =
                                    param.Name.Contains("u16") ? "UInt16" :
                                    param.Name.Contains("b16") ? "UInt16" :
                                    param.Name.Contains("u32") ? "UInt32" : "UInt16";

                                m_list_decimalupdown_general_value_commands[param.Pos].Maximum = m_list_label_general_type_commands[param.Pos].Content.Equals("UInt16") ? ushort.MaxValue : int.MaxValue;
                            }
                        });
                }
            }
        }

        private void Listview_commands_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        #endregion

        #region Send command

        private void Initialite_command_image_states()
        {
            Image_send_general_command_ok.Visibility = Visibility.Collapsed;
            Image_send_general_command_error.Visibility = Visibility.Collapsed;
            Image_send_general_command_warning.Visibility = Visibility.Collapsed;

            Image_receive_general_command_ok.Visibility = Visibility.Collapsed;
            Image_receive_general_command_error.Visibility = Visibility.Collapsed;
            Image_receive_general_command_warning.Visibility = Visibility.Collapsed;
        }

        private void Button_send_general_command_Click(object sender, RoutedEventArgs e)
        {
            if (m_selected_modbus_command != null)
            {
                Initialite_command_image_states();

                int[] array_values = new int[Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_COMMAND]];
                array_values[0] = m_selected_modbus_command.Index;
                array_values[1] = m_command_watchdog++;

                int param_pos = 2;

                m_selected_modbus_command.List_param
                    .Select((Value, Index) => new { Name = Value, Control_pos = Index }).ToList()
                    .ForEach(param =>
                    {
                        if (m_list_label_general_type_commands[param.Control_pos].Content.Equals("UInt16"))
                        {
                            array_values[param_pos] = (int)m_list_decimalupdown_general_value_commands[param.Control_pos].Value!;
                            param_pos++;
                        }
                        else
                        {
                            int param_value = (int)m_list_decimalupdown_general_value_commands[param.Control_pos].Value!;
                            byte[] array_param_value = BitConverter.GetBytes(param_value);

                            array_values[param_pos] = BitConverter.ToUInt16(array_param_value, 0);
                            array_values[param_pos + 1] = BitConverter.ToUInt16(array_param_value, 2);

                            param_pos += 2;
                        }

                    });

                Send_command(array_values, COMMAND_WRITE_LOCATION.MAINTENNANCE);
            }
        }

        private void Send_command(int[] array_values, COMMAND_WRITE_LOCATION command_write_location)
        {
            if (!m_is_writing && !m_is_writing_all_config && !m_is_reading_event_hist)
            {
                m_selected_command_write_location = command_write_location;
                m_array_send_values = array_values;
                m_is_writing = true;
                m_timer_wait_send_command.Start();
            }
        }

        private void Timer_wait_send_command_Tick(object sender, EventArgs e)
        {
            m_timer_write_timeout.Start();
            m_timer_wait_send_command.Stop();

            try
            {
                if (Globals.GetTheInstance().Simulator_mode == BIT_STATE.ON)
                    MessageBox.Show("Cannot send command in simulator mode", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);

                else if (Globals.GetTheInstance().ManageComThread.Is_connected && m_selected_modbus_command != null)
                {
                    Manage_logs.SaveLogValue($"SEND COMMAND -> {m_selected_modbus_command.Name}");

                    bool send_ok = Globals.GetTheInstance().ManageComThread.Write_multiple_registers(0, m_array_send_values, MEMORY_CONFIG_TYPE.SAS360CON_COMMANDS);
                    if (send_ok)
                    {
                        Globals.GetTheInstance().List_last_command_send_data.Clear();
                        Globals.GetTheInstance().List_last_command_send_data.AddRange(m_array_send_values);
                        string s_data = string.Empty;
                        Globals.GetTheInstance().List_last_command_send_data.ForEach(data => s_data += $"0X{data:X4} ");
                        Dispatcher.Invoke(() => Label_send_command.Content = s_data);

                        m_command_watchdog++;
                    }
                }
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{GetType().Name} -> {nameof(Timer_wait_send_command_Tick)} -> {ex.Message}");
            }
        }

        private void Timer_read_last_received_command_Tick(object sender, EventArgs e)
        {
            m_timer_read_last_received_command.Stop();
            try
            {
                bool read_ok = Globals.GetTheInstance().ManageComThread.Read_holding_registers_int32(Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_COMMAND], Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_COMMAND], MEMORY_CONFIG_TYPE.SAS360CON_COMMANDS);
                if (!read_ok)
                    Manage_logs.SaveLogValue($"ERROR READING LAST RECEIVED COMMAND -> {MEMORY_CONFIG_TYPE.SAS360CON_COMMANDS}");

                Dispatcher.Invoke(() =>
                {
                    switch (m_selected_command_write_location)
                    {
                        case COMMAND_WRITE_LOCATION.MAINTENNANCE:
                            {
                                Image_receive_general_command_ok.Visibility = read_ok ? Visibility.Visible : Visibility.Collapsed;
                                Image_receive_general_command_error.Visibility = read_ok ? Visibility.Collapsed : Visibility.Visible;
                                break;
                            }

                        case COMMAND_WRITE_LOCATION.INTERNAL_CONFIG:
                            {
                                Image_internal_command_ok.Visibility = read_ok ? Visibility.Visible : Visibility.Collapsed;
                                Image_internal_command_error.Visibility = read_ok ? Visibility.Collapsed : Visibility.Visible;
                                break;
                            }
                    }
                });
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{GetType().Name} -> {nameof(Timer_read_last_received_command_Tick)} -> {ex.Message}");
                Dispatcher.Invoke(() => Image_receive_general_command_warning.Visibility = Visibility.Visible);
            }

            m_timer_write_timeout.Stop();
            m_is_writing = false;
        }

        #endregion

        #region Refresh command

        private void Button_refresh_command_Click(object sender, RoutedEventArgs e)
        {
        }

        #endregion

        #region New command

        private void Button_new_command_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Delete command
        private void Button_delete_command_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #endregion

        #region I / O events

        #region Force mode mouse down

        private void ForceMode_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Globals.GetTheInstance().ManageComThread.Is_connected)
            {
                int index = Array.IndexOf(m_list_border_force_mode.ToArray(), sender as Border);

                //Change mode state
                Globals.GetTheInstance().Forced_mode_do_controls[index] = !Globals.GetTheInstance().Forced_mode_do_controls[index];
                m_list_border_force_mode[index].Background = Globals.GetTheInstance().Forced_mode_do_controls[index] ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                m_list_image_force_mode[index].Visibility = Globals.GetTheInstance().Forced_mode_do_controls[index] ? Visibility.Visible : Visibility.Collapsed;

                //Change control 
                Cursor cursor = Globals.GetTheInstance().Forced_mode_do_controls[index] ? Cursors.Hand : Cursors.Arrow;

                if (index == (int)FORCE_MODE_CODIF.M_FORCE_DIGITAL_OUTPUTS)
                {
                    m_list_maintenance_border_do_1.ForEach(border => border.Cursor = cursor);
                    m_list_maintenance_border_do_2.ForEach(border => border.Cursor = cursor);
                    m_list_maintenance_border_do_3.ForEach(border => border.Cursor = cursor);

                    //Load maintenance forced values
                    if (Globals.GetTheInstance().Forced_mode_do_controls[index] == true)
                    {
                        int index_control = 0;

                        #region Digital outputs 1 INT

                        ushort digital_forced_do_1_value = Globals.GetTheInstance().SAS360CON_maintennance.Forced_mask_DO1_INT;

                        Label_digital_output_1_value.Content = $"0x{digital_forced_do_1_value:X4}";
                        index_control = 0;
                        Enum.GetValues(typeof(FORCE_MASK_DO1)).Cast<FORCE_MASK_DO1>().ToList().ForEach(digital_output =>
                        {
                            m_list_maintenance_border_do_1[index_control++].Background = Functions.IsBitSetTo1(digital_forced_do_1_value, (int)digital_output) ? new SolidColorBrush(Colors.Blue) : new SolidColorBrush(Colors.White);
                        });

                        #endregion

                        #region Digital outputs 2 EXT

                        ushort digital_forced_do_2_value = Globals.GetTheInstance().SAS360CON_maintennance.Forced_mask_DO2_EXT;

                        Label_digital_output_2_value.Content = $"0x{digital_forced_do_2_value:X4}";
                        index_control = 0;
                        Enum.GetValues(typeof(FORCE_MASK_DO2)).Cast<FORCE_MASK_DO2>().ToList().ForEach(digital_output =>
                        {
                            m_list_maintenance_border_do_2[index_control++].Background = Functions.IsBitSetTo1(digital_forced_do_2_value, (int)digital_output) ? new SolidColorBrush(Colors.Blue) : new SolidColorBrush(Colors.White);
                        });

                        #endregion

                        #region Digital outputs 3 EXT

                        ushort digital_forced_do_3_value = Globals.GetTheInstance().SAS360CON_maintennance.Forced_mask_DO3_LED;

                        Label_digital_output_3_value.Content = $"0x{digital_forced_do_3_value:X4}";
                        index_control = 0;
                        Enum.GetValues(typeof(FORCE_MASK_DO3)).Cast<FORCE_MASK_DO2>().ToList().ForEach(digital_output =>
                        {
                            m_list_maintenance_border_do_3[index_control++].Background = Functions.IsBitSetTo1(digital_forced_do_3_value, (int)digital_output) ? new SolidColorBrush(Colors.Blue) : new SolidColorBrush(Colors.White);
                        });

                        #endregion
                    }
                }
                else if (index == (int)FORCE_MODE_CODIF.M_FORCE_LEDS)
                {
                    m_list_maintenance_border_led.ForEach(border => border.Cursor = cursor);

                    #region Digital outputs codif leds

                    ushort digital_forced_codif_leds_1_value = Globals.GetTheInstance().SAS360CON_maintennance.Forced_mask_codif_LED1;
                    Label_codif_led1_value.Content = $"0x{digital_forced_codif_leds_1_value:X4}";

                    ushort digital_forced_codif_leds_2_value = Globals.GetTheInstance().SAS360CON_maintennance.Forced_mask_codif_LED2;
                    Label_codif_led2_value.Content = $"0x{digital_forced_codif_leds_2_value:X4}";

                    byte[] array_byte_codif_led1 = BitConverter.GetBytes(digital_forced_codif_leds_1_value);
                    byte[] array_byte_codif_led2 = BitConverter.GetBytes(digital_forced_codif_leds_2_value);
                    IEnumerable<byte> array_byte_codif_led = array_byte_codif_led1.Concat(array_byte_codif_led2);
                    int codif_led_value = BitConverter.ToInt32(array_byte_codif_led.ToArray());

                    const int max_leds = 28;
                    for (int index_codif_led = 0; index_codif_led < max_leds; index_codif_led++)
                    {
                        m_list_maintenance_border_led[index_codif_led].Background = Functions.IsBitSetTo1(codif_led_value, index_codif_led) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);

                    }

                    #endregion
                }
                else if (index == (int)FORCE_MODE_CODIF.M_AUDIO_TO_PLAY)
                {
                    m_list_maintenance_border_audio.ForEach(border => border.Cursor = cursor);

                    #region Digital outputs codif audio

                    ushort digital_forced_codif_audio_1_value = Globals.GetTheInstance().SAS360CON_maintennance.Forced_mask_AUDIO_1_to_play;
                    Label_digital_audio_1_value.Content = $"0x{digital_forced_codif_audio_1_value:X4}";

                    ushort digital_forced_codif_audio_2_value = Globals.GetTheInstance().SAS360CON_maintennance.Forced_mask_AUDIO_2_to_play;
                    Label_digital_audio_2_value.Content = $"0x{digital_forced_codif_audio_2_value:X4}";

                    byte[] array_byte_codif_audio1 = BitConverter.GetBytes(digital_forced_codif_audio_1_value);
                    byte[] array_byte_codif_audio2 = BitConverter.GetBytes(digital_forced_codif_audio_2_value);
                    IEnumerable<byte> array_byte_codif_audio = array_byte_codif_audio1.Concat(array_byte_codif_audio2);
                    int codif_audio_value = BitConverter.ToInt32(array_byte_codif_audio.ToArray());

                    const int max_audio = 32;
                    for (int index_codif_audio = 0; index_codif_audio < max_audio; index_codif_audio++)
                    {
                        m_list_maintenance_border_audio[index_codif_audio].Background = Functions.IsBitSetTo1(codif_audio_value, index_codif_audio) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);

                    }

                    #endregion
                }
            }
        }

        #endregion

        #region Digital input /output mouse down

        private void Digital_output_1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Globals.GetTheInstance().Forced_mode_do_controls[(int)FORCE_MODE_CODIF.M_FORCE_DIGITAL_OUTPUTS])
            {
                int index = Array.IndexOf(m_list_maintenance_border_do_1.ToArray(), sender as Border);
                if (e.ClickCount == 2)
                {
                    ushort selected_value = Globals.GetTheInstance().SAS360CON_maintennance.Forced_mask_DO1_INT;

                    DigitalChangeWindow digital_window = new()
                    {
                        Digital_name = m_list_maintenance_label_do_1[index].Content?.ToString()!,
                        Is_activated = Functions.IsBitSetTo1(selected_value, index)
                    };

                    digital_window.ShowDialog();
                    if (digital_window.Save_changes)
                    {
                        int changed_value = Functions.SetBitTo1(selected_value, index);
                        //Globals.GetTheInstance().ManageComThread.Write_single_register(Globals.GetTheInstance().SAS360CON_image_forced_mask.Forced_mask_do1, changed_value);
                    }
                }
            }
        }

        private void Digital_output_2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Globals.GetTheInstance().Forced_mode_do_controls[(int)FORCE_MODE_CODIF.M_FORCE_DIGITAL_OUTPUTS])
            {
                int index = Array.IndexOf(m_list_maintenance_border_do_2.ToArray(), sender as Border);
                if (e.ClickCount == 2)
                {
                    ushort selected_value = Globals.GetTheInstance().SAS360CON_maintennance.Forced_mask_DO2_EXT;

                    DigitalChangeWindow digital_window = new()
                    {
                        Digital_name = m_list_maintenance_label_do_2[index].Content?.ToString()!,
                        Is_activated = Functions.IsBitSetTo1(selected_value, index)
                    };

                    digital_window.ShowDialog();
                    if (digital_window.Save_changes)
                    {
                        int changed_value = Functions.SetBitTo1(selected_value, index);
                        //Globals.GetTheInstance().ManageComThread.Write_single_register(Globals.GetTheInstance().SAS360CON_image_forced_mask.Forced_mask_do2, changed_value);
                    }
                }
            }
        }

        private void Digital_output_3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Globals.GetTheInstance().Forced_mode_do_controls[(int)FORCE_MODE_CODIF.M_FORCE_DIGITAL_OUTPUTS])
            {
                int index = Array.IndexOf(m_list_maintenance_border_do_3.ToArray(), sender as Border);
                if (e.ClickCount == 2)
                {
                    ushort selected_value = Globals.GetTheInstance().SAS360CON_maintennance.Forced_mask_DO3_LED;

                    DigitalChangeWindow digital_window = new()
                    {
                        Digital_name = m_list_maintenance_label_do_3[index].Content.ToString()!,
                        Is_activated = Functions.IsBitSetTo1(selected_value, index)
                    };

                    digital_window.ShowDialog();
                    if (digital_window.Save_changes)
                    {
                        int changed_value = Functions.SetBitTo1(selected_value, index);
                        //Globals.GetTheInstance().ManageComThread.Write_single_register(Globals.GetTheInstance().SAS360CON_image_forced_mask.Forced_mask_do3, changed_value);
                    }
                }
            }
        }

        private void Led_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Globals.GetTheInstance().Forced_mode_do_controls[(int)FORCE_MODE_CODIF.M_FORCE_LEDS])
            {
                int index = Array.IndexOf(m_list_maintenance_border_led.ToArray(), sender as Border);
                if (e.ClickCount == 2)
                {
                    MAINT_FORCED_DIGITAL_STATES_IN_LIST STATE = index < 16 ? MAINT_FORCED_DIGITAL_STATES_IN_LIST.CODIF_LED_1 : MAINT_FORCED_DIGITAL_STATES_IN_LIST.CODIF_LED_2;
                    ushort selected_value = Globals.GetTheInstance().SAS360CON_maintennance.Forced_mask_codif_LED2;
                    DigitalChangeWindow digital_window = new()
                    {
                        Digital_name = m_list_maintenance_label_led[index].Content.ToString()!,
                        Is_activated = Functions.IsBitSetTo1(selected_value, index)
                    };

                    digital_window.ShowDialog();
                    if (digital_window.Save_changes)
                    {
                        /*
                        int changed_value = Functions.SetBitTo1(selected_value, index);
                        if (index < 16)
                            Globals.GetTheInstance().ManageComThread.Write_single_register(Globals.GetTheInstance().SAS360CON_image_forced_mask.Forced_mask_codif_led1, changed_value);

                        else
                            Globals.GetTheInstance().ManageComThread.Write_single_register(Globals.GetTheInstance().SAS360CON_image_forced_mask.Forced_mask_codif_led2, changed_value);
                        */
                    }
                }
            }
        }

        private void Audio_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Globals.GetTheInstance().Forced_mode_do_controls[(int)FORCE_MODE_CODIF.M_AUDIO_TO_PLAY])
            {
                int index = Array.IndexOf(m_list_maintenance_border_audio.ToArray(), sender as Border);
                if (e.ClickCount == 2)
                {
                    ushort selected_value = index < 16 ? Globals.GetTheInstance().SAS360CON_maintennance.Forced_mask_AUDIO_1_to_play : Globals.GetTheInstance().SAS360CON_maintennance.Forced_mask_AUDIO_2_to_play;
                    DigitalChangeWindow digital_window = new()
                    {
                        Digital_name = m_list_maintenance_label_audio[index].Content?.ToString()!,
                        Is_activated = Functions.IsBitSetTo1(selected_value, index)
                    };

                    digital_window.ShowDialog();
                    if (digital_window.Save_changes)
                    {
                        /*
                        int changed_value = Functions.SetBitTo1(selected_value, index);
                        if (index < 16)
                            Globals.GetTheInstance().ManageComThread.Write_single_register(Globals.GetTheInstance().SAS360CON_image_forced_mask.Forced_mask_audio1, changed_value);

                        else
                            Globals.GetTheInstance().ManageComThread.Write_single_register(Globals.GetTheInstance().SAS360CON_image_forced_mask.Forced_mask_audio2, changed_value);
                        */
                    }
                }
            }
        }

        #endregion

        #endregion

        #region MEM records

        #region SAS360con events

        #region Last events log selection changed
        private void Combobox_sas360con_event_log_last_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selection_index = Combobox_sas360con_event_log_last.SelectedIndex;
            if (Stackpanel_sas360con_event_log_manual != null)
                Stackpanel_sas360con_event_log_manual.IsEnabled = selection_index == 0;
        }

        #endregion

        #region Read SAS360CON event

        private void Button_read_sas360con_event_log_Click(object sender, RoutedEventArgs e)
        {
            if (Globals.GetTheInstance().ManageComThread.Is_connected & m_read_events_hist_param)
            {
                Image_read_warning_sas360con_event_log.Visibility = Visibility.Collapsed;
                Image_read_ok_sas360con_event_log.Visibility = Visibility.Collapsed;

                Globals.GetTheInstance().List_sas360con_event_log.Clear();
                m_collection_sas360con_event_log.Clear();

                ComboBoxItem last_events_selected_item = (ComboBoxItem)Combobox_sas360con_event_log_last.SelectedItem;
                string last_events_selected_value = last_events_selected_item.Content.ToString()!;

                int pos_relative_event_log = (int)DecimalUpDown_sas360con_event_log_read_pos.Value! / 4;
                m_empty_reg_event_hist = (int)DecimalUpDown_sas360con_event_log_read_pos.Value! % 4;
                m_pos_modbus_read_event_hist = (ushort)(Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_RECORD_EVENTS] + pos_relative_event_log);

                m_num_abs_read_event_hist = Combobox_sas360con_event_log_last.SelectedIndex == 0 ? (uint)DecimalUpDown_sas360con_event_log_num.Value! : uint.Parse(last_events_selected_value);
                m_num_modbus_read_event_hist = m_num_abs_read_event_hist / 4;

                if (!m_is_writing && !m_is_writing_all_config && !m_is_reading_event_hist)
                {
                    ProgressBar_events_log.Value = 0;
                    Border_wait_sas360con_event_log.Visibility = Visibility.Visible;
                    m_memory_config_event_hist = MEMORY_CONFIG_TYPE.SAS360CON_EVENT_LOG;
                    m_is_reading_event_hist = true;
                    m_received_resp = true;
                    m_start_date_read_event_hist = DateTime.Now;
                    Label_events_reg_time.Content = 0;
                    m_timer_read_sas360con_event_log.Start();
                }
            }
        }

        #endregion

        #region Timer read event log


        private void Timer_read_sas360con_event_log_Tick(object sender, EventArgs e)
        {
            m_timer_read_sas360con_event_log.Stop();

            try
            {
                if (m_is_reading_event_hist && m_num_modbus_read_event_hist > 0)
                {
                    Dispatcher.Invoke(() =>
                    {
                        double progress_value = ((m_num_abs_read_event_hist - (m_num_modbus_read_event_hist * 4)) * 100) / m_num_abs_read_event_hist;
                        ProgressBar_events_log.Value = progress_value;

                        Label_events_reg_time.Content = DateTime.Now.Subtract(m_start_date_read_event_hist).ToString(@"mm\:ss");
                        Label_events_reg_progress.Content = $"{m_num_abs_read_event_hist - (m_num_modbus_read_event_hist * 4)} / {m_num_abs_read_event_hist}";
                    });


                    bool enable_read_reg = m_received_resp;
                    if (!enable_read_reg)
                    {
                        enable_read_reg = m_wait_ticks_read++ > 3;
                        string s_info = enable_read_reg ? "TIMEOUT RESPUESTA (REPETICION TRAMA)" : "TIMEOUT RESPUESTA (WAIT TICK)";
                        Manage_logs.SaveModbusValue(s_info);
                    }

                    if (enable_read_reg)
                    {
                        m_wait_ticks_read = 0;
                        m_received_resp = false;

                        Manage_logs.SaveModbusValue($"READ HOLDING REGISTER ({MEMORY_CONFIG_TYPE.SAS360CON_EVENT_LOG}) -> INI: {m_pos_modbus_read_event_hist} / COUNT: {Constants.EVENT_LOG_REG_READ_MODBUS}");

                        bool read_memory_ok = Globals.GetTheInstance().ManageComThread.Read_holding_registers_int32(m_pos_modbus_read_event_hist, Constants.EVENT_LOG_REG_READ_MODBUS, MEMORY_CONFIG_TYPE.SAS360CON_EVENT_LOG);
                        if (!read_memory_ok)
                            Manage_logs.SaveModbusValue($"ERROR READING HOLDING REG -> {MEMORY_CONFIG_TYPE.SAS360CON_EVENT_LOG}");
                    }

                    m_timer_read_sas360con_event_log.Start();

                }
                else if (!m_is_reading_event_hist)
                {
                    Dispatcher.Invoke(() =>
                    {
                        Image_read_warning_sas360con_event_log.Visibility = Visibility.Visible;
                        Border_wait_sas360con_event_log.Visibility = Visibility.Collapsed;
                        Globals.GetTheInstance().List_sas360con_event_log.ForEach(event_log => m_collection_sas360con_event_log.Add(event_log));
                        CollectionViewSource.GetDefaultView(Listview_sas360con_event_log.ItemsSource).Refresh();
                    });

                    Manage_logs.SaveModbusValue($"READING HOLDING REG -> {MEMORY_CONFIG_TYPE.SAS360CON_EVENT_LOG} STOPPED");
                }
                else if (m_num_modbus_read_event_hist == 0)
                {
                    Manage_logs.SaveModbusValue($"READING HOLDING REG -> {MEMORY_CONFIG_TYPE.SAS360CON_EVENT_LOG} FINISHED");

                    m_timer_sas360con_log_timeout.Stop();
                    m_is_reading_event_hist = false;

                    Dispatcher.Invoke(() =>
                    {
                        Image_read_ok_sas360con_event_log.Visibility = Visibility.Visible;
                        Border_wait_sas360con_event_log.Visibility = Visibility.Collapsed;
                        Globals.GetTheInstance().List_sas360con_event_log.ForEach(event_log => m_collection_sas360con_event_log.Add(event_log));
                        CollectionViewSource.GetDefaultView(Listview_sas360con_event_log.ItemsSource).Refresh();
                    });
                }
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() => Image_read_warning_sas360con_event_log.Visibility = Visibility.Visible);
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Timer_read_sas360con_event_log_Tick)} -> {ex.Message}");
            }

        }

        #endregion

        #region Stop read sas360con event log

        private void Button_sfront_sas360con_event_log_Click(object sender, RoutedEventArgs e)
        {
            m_timer_sas360con_log_timeout.Stop();
            m_is_reading_event_hist = false;
        }

        #endregion

        #region Export sas360con event log

        private void Button_export_sas360con_event_log_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Directory.Exists($"{AppDomain.CurrentDomain.BaseDirectory}csv_files\\event_log"))
                    Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}csv_files\\event_log");

                SaveFileDialog save_file_Dialog = new()
                {
                    InitialDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}csv_files\\event_log",
                    FileName = $"EVENTS_{Globals.GetTheInstance().SAS360CON_internal_cfg.Consola_id}_{DateTime.Now.Year:D4}{DateTime.Now.Month:D2}{DateTime.Now.Day:D2}",
                    Title = "Indique un nombre para el fichero CSV",
                    DefaultExt = "csv",
                    Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*"
                };
                if (save_file_Dialog.ShowDialog() == true)
                {
                    CultureInfo culture_info = new("es-ES"); //El separador decimal en el fichero es la coma
                    var config = new CsvConfiguration(culture_info) { Delimiter = ";", Encoding = Encoding.UTF8, HasHeaderRecord = true };

                    using TextWriter event_log_writer = new StreamWriter(save_file_Dialog.FileName, false);
                    using var event_log_csv_writer = new CsvWriter(event_log_writer, config);
                    event_log_csv_writer.Context.RegisterClassMap<Event_log_map>();
                    event_log_csv_writer.WriteRecords(Globals.GetTheInstance().List_sas360con_event_log);

                    MessageBox.Show("SAS360CON EVENT LOG CSV GENERADO", "INFO", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Button_export_sas360con_event_log_Click)} -> {ex.Message}");
            }
        }

        #endregion

        #endregion


        #region SAS360con hist

        #region Read SAS360CON hist

        private void Button_read_sas360con_hist_log_Click(object sender, RoutedEventArgs e)
        {
            if (Globals.GetTheInstance().ManageComThread.Is_connected)
            {
                Image_warning_sas360con_hist_log.Visibility = Visibility.Collapsed;
                Image_read_ok_sas360con_hist_log.Visibility = Visibility.Collapsed;

                Globals.GetTheInstance().List_sas360con_hist_log.Clear();
                m_collection_sas360con_hist_log.Clear();
            }
        }

        #endregion

        #region Timer read hist log

        private void Timer_read_sas360con_hist_log_Tick(object sender, EventArgs e)
        {


        }

        #endregion

        #region Stop read sas360con hist log

        private void Button_sfront_sas360con_hist_log_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Export sas360con hist log

        private void Button_export_sas360con_hist_log_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #endregion


        #region SAS360CON LOG timeout

        private void Timer_sas360con_log_timeout_Tick(object sender, EventArgs e)
        {
            if (m_received_resp_event_hist_timeout)
            {
                m_received_resp_event_hist_timeout = false;
            }
            else
            {
                Dispatcher.Invoke(() => Image_read_warning_sas360con_event_log.Visibility = Visibility.Visible);

                m_timer_sas360con_log_timeout.Stop();
                Manage_logs.SaveModbusValue($"READING HOLDING REG -> {MEMORY_CONFIG_TYPE.SAS360CON_EVENT_LOG} TIMEOUT");
                m_is_reading_event_hist = false;
            }
        }

        #endregion

        #endregion

        #endregion




        #region Draw

        #region Draw detection areas

        private void Draw_config_sas360con_detection_areas()
        {
            if (Globals.GetTheInstance().Draw_map == BIT_STATE.ON)
            {
                double rectangle_width = Rectangle_sas360_data_draw.ActualWidth;
                double rectangle_height = Rectangle_sas360_data_draw.ActualHeight;

                ushort[] Array_area_FRONT_ANRI_dist_pixel = Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_FRONT_ANRI_dist_cm.Select(area => Convert.ToUInt16(rectangle_height * (area / (double)Globals.GetTheInstance().Panel_area_cm))).ToArray();
                ushort[] Array_area_LEFT_ANRI_dist_pixel = Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_LEFT_ANRI_dist_cm.Select(area => Convert.ToUInt16(rectangle_width * (area / (double)Globals.GetTheInstance().Panel_area_cm))).ToArray();
                ushort[] Array_area_BACK_ANRI_dist_pixel = Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_BACK_ANRI_dist_cm.Select(area => Convert.ToUInt16(rectangle_height * (area / (double)Globals.GetTheInstance().Panel_area_cm))).ToArray();
                ushort[] Array_area_RIGHT_ANRI_dist_pixel = Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_RIGHT_ANRI_dist_cm.Select(area => Convert.ToUInt16(rectangle_width * (area / (double)Globals.GetTheInstance().Panel_area_cm))).ToArray();

                #region Width - Height

                double yellow_height = Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW] + Array_area_BACK_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW];
                double yellow_width = Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW] + Array_area_RIGHT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW];

                double orange_height = Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE] + Array_area_BACK_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE];
                double orange_width = Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE] + Array_area_RIGHT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE];

                double red_height = Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.RED] + Array_area_BACK_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.RED];
                double red_width = Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.RED] + Array_area_RIGHT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.RED];

                #endregion

                #region Diff

                double yellow_orange_width_diff = yellow_width - orange_width;
                double yellow_orange_height_diff = yellow_height - orange_height;

                double orange_red_width_diff = orange_width - red_width;
                double orange_red_height_diff = orange_height - red_height;

                double red_vehicle_width_diff = red_width - Image_vehicle.Width;
                double red_vehicle_height_diff = red_height - Image_vehicle.Height;

                #endregion

                #region Delete existing areas

                var borders = Canvas_sas360_data_draw.Children.OfType<Border>().ToList();
                foreach (var border in borders)
                {
                    Canvas_sas360_data_draw.Children.Remove(border);
                }

                #endregion


                double corner_radious = 50;
                SolidColorBrush limit_brush = new(Color.FromArgb(255, 38, 149, 206));



                #region Yellow

                #region Backgroud white

                Border border_detection_yellow_background = new()
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = limit_brush,
                    Background = Brushes.White,
                    Width = yellow_width,
                    Height = yellow_height,
                    Opacity = 1,
                    CornerRadius = new CornerRadius(corner_radious)
                };

                Canvas_sas360_data_draw.Children.Add(border_detection_yellow_background);
                Canvas.SetLeft(border_detection_yellow_background, (rectangle_width / 2) - Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW]);
                Canvas.SetTop(border_detection_yellow_background, (rectangle_height / 2) - Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW]);
                Panel.SetZIndex(border_detection_yellow_background, 11);

                #endregion

                #region Up

                Border border_detection_yellow_up_limit = new()
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = limit_brush,
                    Background = Brushes.Yellow,
                    Width = yellow_width,
                    Height = yellow_height,
                    Opacity = 0.1,
                    CornerRadius = new CornerRadius(corner_radious)
                };

                Canvas_sas360_data_draw.Children.Add(border_detection_yellow_up_limit);
                Canvas.SetLeft(border_detection_yellow_up_limit, (rectangle_width / 2) - Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW]);
                Canvas.SetTop(border_detection_yellow_up_limit, (rectangle_height / 2) - Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW]);
                Panel.SetZIndex(border_detection_yellow_up_limit, 12);

                #endregion

                #region Medium

                Border border_detection_yellow_medium_limit = new()
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = limit_brush,
                    Width = yellow_width - yellow_orange_width_diff / 2,
                    Height = yellow_height - yellow_orange_height_diff / 2,
                    Opacity = 0.2,
                    CornerRadius = new CornerRadius(corner_radious)
                };

                Canvas_sas360_data_draw.Children.Add(border_detection_yellow_medium_limit);
                Canvas.SetLeft(border_detection_yellow_medium_limit, (rectangle_width / 2) - Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW] + (yellow_orange_width_diff / 4));
                Canvas.SetTop(border_detection_yellow_medium_limit, (rectangle_height / 2) - Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW] + (yellow_orange_height_diff / 4));
                Panel.SetZIndex(border_detection_yellow_medium_limit, 12);

                #endregion

                #region Down

                Border border_detection_yellow_down_limit = new()
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = limit_brush,
                    Width = orange_width,
                    Height = orange_height,
                    Opacity = 0.2,
                    CornerRadius = new CornerRadius(corner_radious)
                };

                Canvas_sas360_data_draw.Children.Add(border_detection_yellow_down_limit);
                Canvas.SetLeft(border_detection_yellow_down_limit, (rectangle_width / 2) - Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE]);
                Canvas.SetTop(border_detection_yellow_down_limit, (rectangle_height / 2) - Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE]);
                Panel.SetZIndex(border_detection_yellow_down_limit, 12);

                #endregion

                #endregion


                #region Orange

                #region background white

                Border border_detection_orange_background = new()
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = limit_brush,
                    Background = Brushes.White,
                    Width = orange_width,
                    Height = orange_height,
                    Opacity = 1,
                    CornerRadius = new CornerRadius(corner_radious)
                };

                Canvas_sas360_data_draw.Children.Add(border_detection_orange_background);
                Canvas.SetLeft(border_detection_orange_background, (rectangle_width / 2) - Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE]);
                Canvas.SetTop(border_detection_orange_background, (rectangle_height / 2) - Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE]);
                Panel.SetZIndex(border_detection_orange_background, 13);

                #endregion

                #region Up

                Border border_detection_orange_up_limit = new()
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = limit_brush,
                    Background = Brushes.Orange,
                    Width = orange_width,
                    Height = orange_height,
                    Opacity = 0.3,
                    CornerRadius = new CornerRadius(corner_radious)
                };

                Canvas_sas360_data_draw.Children.Add(border_detection_orange_up_limit);
                Canvas.SetLeft(border_detection_orange_up_limit, (rectangle_width / 2) - Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE]);
                Canvas.SetTop(border_detection_orange_up_limit, (rectangle_height / 2) - Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE]);
                Panel.SetZIndex(border_detection_orange_up_limit, 13);

                #endregion

                #region Medium

                Border border_detection_orange_medium_limit = new()
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = limit_brush,
                    Width = orange_width - orange_red_width_diff / 2,
                    Height = orange_height - orange_red_height_diff / 2,
                    Opacity = 0.2,
                    CornerRadius = new CornerRadius(corner_radious)
                };

                Canvas_sas360_data_draw.Children.Add(border_detection_orange_medium_limit);
                Canvas.SetLeft(border_detection_orange_medium_limit, (rectangle_width / 2) - Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE] + (orange_red_width_diff / 4));
                Canvas.SetTop(border_detection_orange_medium_limit, (rectangle_height / 2) - Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE] + (orange_red_height_diff / 4));
                Panel.SetZIndex(border_detection_orange_medium_limit, 13);

                #endregion

                #region Down

                Border border_detection_orange_down_limit = new()
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = limit_brush,
                    Width = red_width,
                    Height = red_height,
                    Opacity = 0.2,
                    CornerRadius = new CornerRadius(corner_radious)
                };

                Canvas_sas360_data_draw.Children.Add(border_detection_orange_down_limit);

                Canvas.SetLeft(border_detection_orange_down_limit, (rectangle_width / 2) - Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.RED]);
                Canvas.SetTop(border_detection_orange_down_limit, (rectangle_height / 2) - Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.RED]);
                Panel.SetZIndex(border_detection_orange_down_limit, 13);

                #endregion


                #endregion


                #region Red

                #region Backgroud white

                Border border_detection_red_background = new()
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = limit_brush,
                    Background = Brushes.White,
                    Width = red_width,
                    Height = red_height,
                    Opacity = 1,
                    CornerRadius = new CornerRadius(corner_radious)
                };

                Canvas_sas360_data_draw.Children.Add(border_detection_red_background);
                Canvas.SetLeft(border_detection_red_background, (rectangle_width / 2) - Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.RED]);
                Canvas.SetTop(border_detection_red_background, (rectangle_height / 2) - Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.RED]);
                Panel.SetZIndex(border_detection_red_background, 14);

                #endregion

                #region Up

                Border border_detection_red_up_limit = new()
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = limit_brush,
                    Background = Brushes.Red,
                    Width = red_width,
                    Height = red_height,
                    Opacity = 0.2,
                    CornerRadius = new CornerRadius(corner_radious)
                };

                Canvas_sas360_data_draw.Children.Add(border_detection_red_up_limit);
                Canvas.SetLeft(border_detection_red_up_limit, (rectangle_width / 2) - Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.RED]);
                Canvas.SetTop(border_detection_red_up_limit, (rectangle_height / 2) - Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.RED]);
                Panel.SetZIndex(border_detection_red_up_limit, 14);

                #endregion

                #region Medium

                Border border_detection_red_medium_limit = new()
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = limit_brush,
                    Width = red_width - red_vehicle_width_diff / 2,
                    Height = red_height - red_vehicle_height_diff / 2,
                    Opacity = 0.2,
                    CornerRadius = new CornerRadius(corner_radious)
                };

                Canvas_sas360_data_draw.Children.Add(border_detection_red_medium_limit);
                Canvas.SetLeft(border_detection_red_medium_limit, (rectangle_width / 2) - Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.RED] + (red_vehicle_width_diff / 4));
                Canvas.SetTop(border_detection_red_medium_limit, (rectangle_height / 2) - Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.RED] + (red_vehicle_height_diff / 4));
                Panel.SetZIndex(border_detection_red_medium_limit, 14);

                #endregion

                #region Down

                Border border_detection_red_down_limit = new()
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = limit_brush,
                    Background = Brushes.White,
                    Width = Image_vehicle.Width + 20,
                    Height = Image_vehicle.Height + 20,
                    Opacity = 1,
                    CornerRadius = new CornerRadius(corner_radious)
                };

                Canvas_sas360_data_draw.Children.Add(border_detection_red_down_limit);

                double vehicle_left = Canvas.GetLeft(Image_vehicle);
                double vehicle_top = Canvas.GetTop(Image_vehicle);

                Canvas.SetLeft(border_detection_red_down_limit, vehicle_left - 10);
                Canvas.SetTop(border_detection_red_down_limit, vehicle_top - 10);

                Panel.SetZIndex(border_detection_red_down_limit, 15);

                #endregion

                #endregion

            }
        }

        #endregion

        #region Draw detection area polygon

        private void Draw_config_sas360con_detection_areas_polygon()
        {
            double center_left = Canvas_sas360_data_draw.ActualWidth / 2;
            double center_top = Canvas_sas360_data_draw.ActualHeight / 2;


            const double angle_in_grades = 30;
            double angle_in_radians = (angle_in_grades * Math.PI) / 180;

            double rectangle_width = Rectangle_sas360_data_draw.ActualWidth;
            double rectangle_height = Rectangle_sas360_data_draw.ActualHeight;

            ushort[] Array_area_FRONT_ANRI_dist_pixel = Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_FRONT_ANRI_dist_cm.Select(area => Convert.ToUInt16(rectangle_height * (area / (double)Globals.GetTheInstance().Panel_area_cm))).ToArray();
            ushort[] Array_area_LEFT_ANRI_dist_pixel = Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_LEFT_ANRI_dist_cm.Select(area => Convert.ToUInt16(rectangle_width * (area / (double)Globals.GetTheInstance().Panel_area_cm))).ToArray();
            ushort[] Array_area_BACK_ANRI_dist_pixel = Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_BACK_ANRI_dist_cm.Select(area => Convert.ToUInt16(rectangle_height * (area / (double)Globals.GetTheInstance().Panel_area_cm))).ToArray();
            ushort[] Array_area_RIGHT_ANRI_dist_pixel = Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_RIGHT_ANRI_dist_cm.Select(area => Convert.ToUInt16(rectangle_width * (area / (double)Globals.GetTheInstance().Panel_area_cm))).ToArray();

            #region Width - Height

            double yellow_height = Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW] + Array_area_BACK_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW];
            double yellow_width = Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW] + Array_area_RIGHT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW];

            double orange_height = Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE] + Array_area_BACK_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE];
            double orange_width = Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE] + Array_area_RIGHT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE];

            double red_height = Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.RED] + Array_area_BACK_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.RED];
            double red_width = Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.RED] + Array_area_RIGHT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.RED];

            #endregion

            #region Diff

            double yellow_orange_width_diff = yellow_width - orange_width;
            double yellow_orange_height_diff = yellow_height - orange_height;

            double orange_red_width_diff = orange_width - red_width;
            double orange_red_height_diff = orange_height - red_height;

            double red_vehicle_width_diff = red_width - Image_vehicle.Width;
            double red_vehicle_height_diff = red_height - Image_vehicle.Height;

            #endregion

            #region Delete existing areas

            var areas = Canvas_sas360_data_draw.Children.OfType<Polygon>().ToList();
            foreach (var area in areas)
                Canvas_sas360_data_draw.Children.Remove(area);

            #endregion

            SolidColorBrush limit_brush = new(Color.FromArgb(255, 38, 149, 206));

            #region Yellow

            #region Area yellow background

            double cateto_ady_left_yellow_background = Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW];
            double lado_opuesto_left_yellow_background = cateto_ady_left_yellow_background * Math.Tan(angle_in_radians);

            double cateto_ady_front_yellow_background = Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW];
            double lado_opuesto_front_yellow_background = cateto_ady_front_yellow_background * Math.Tan(angle_in_radians);

            List<Point> polyPoints_yellow_background = new()
            {
                new Point(-cateto_ady_left_yellow_background, lado_opuesto_left_yellow_background),
                new Point(-cateto_ady_left_yellow_background, -lado_opuesto_left_yellow_background),
                new Point(-lado_opuesto_front_yellow_background, -cateto_ady_front_yellow_background),
                new Point(lado_opuesto_front_yellow_background, -cateto_ady_front_yellow_background),
                new Point(cateto_ady_left_yellow_background, -lado_opuesto_left_yellow_background),
                new Point(cateto_ady_left_yellow_background, lado_opuesto_left_yellow_background),
                new Point(lado_opuesto_front_yellow_background, cateto_ady_front_yellow_background),
                new Point(-lado_opuesto_front_yellow_background, cateto_ady_front_yellow_background),
            };

            Polygon polygon_yellow_background = new()
            {
                Stroke = limit_brush,
                Opacity = 1,
                Fill = Brushes.White,
                StrokeThickness = 2,
                Points = new PointCollection(polyPoints_yellow_background)
            };

            Canvas_sas360_data_draw.Children.Add(polygon_yellow_background);

            Canvas.SetLeft(polygon_yellow_background, center_left);
            Canvas.SetTop(polygon_yellow_background, center_top);

            #endregion

            #region Area yellow ext

            double cateto_ady_left_yellow_ext = Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW];
            double lado_opuesto_left_yellow_ext = cateto_ady_left_yellow_ext * Math.Tan(angle_in_radians);

            double cateto_ady_front_yellow_ext = Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW];
            double lado_opuesto_front_yellow_ext = cateto_ady_front_yellow_ext * Math.Tan(angle_in_radians);

            List<Point> polyPoints_yellow_ext = new()
            {
                new Point(-cateto_ady_left_yellow_ext, lado_opuesto_left_yellow_ext),
                new Point(-cateto_ady_left_yellow_ext, -lado_opuesto_left_yellow_ext),
                new Point(-lado_opuesto_front_yellow_ext, -cateto_ady_front_yellow_ext),
                new Point(lado_opuesto_front_yellow_ext, -cateto_ady_front_yellow_ext),
                new Point(cateto_ady_left_yellow_ext, -lado_opuesto_left_yellow_ext),
                new Point(cateto_ady_left_yellow_ext, lado_opuesto_left_yellow_ext),
                new Point(lado_opuesto_front_yellow_ext, cateto_ady_front_yellow_ext),
                new Point(-lado_opuesto_front_yellow_ext, cateto_ady_front_yellow_ext),
            };

            Polygon polygon_yellow_ext = new()
            {
                Stroke = limit_brush,
                Opacity = 0.3,
                Fill = Brushes.Yellow,
                StrokeThickness = 2,
                Points = new PointCollection(polyPoints_yellow_ext)
            };

            Canvas_sas360_data_draw.Children.Add(polygon_yellow_ext);

            Canvas.SetLeft(polygon_yellow_ext, center_left);
            Canvas.SetTop(polygon_yellow_ext, center_top);

            #endregion
 
            #region Area yellow medium

            double cateto_ady_left_yellow_medium = Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW] - (yellow_orange_width_diff / 4);
            double lado_opuesto_left_yellow_medium = cateto_ady_left_yellow_medium * Math.Tan(angle_in_radians);

            double cateto_ady_front_yellow_medium = Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW] - (yellow_orange_width_diff / 4);
            double lado_opuesto_front_yellow_medium = cateto_ady_front_yellow_medium * Math.Tan(angle_in_radians);

            List<Point> polyPoints_yellow_medium = new()
            {
                new Point(-cateto_ady_left_yellow_medium, lado_opuesto_left_yellow_medium),
                new Point(-cateto_ady_left_yellow_medium, -lado_opuesto_left_yellow_medium),
                new Point(-lado_opuesto_front_yellow_medium, -cateto_ady_front_yellow_medium),
                new Point(lado_opuesto_front_yellow_medium, -cateto_ady_front_yellow_medium),
                new Point(cateto_ady_left_yellow_medium, -lado_opuesto_left_yellow_medium),
                new Point(cateto_ady_left_yellow_medium, lado_opuesto_left_yellow_medium),
                new Point(lado_opuesto_front_yellow_medium, cateto_ady_front_yellow_medium),
                new Point(-lado_opuesto_front_yellow_medium, cateto_ady_front_yellow_medium),
            };

            Polygon polygon_yellow_medium = new()
            {
                Stroke = limit_brush,
                Opacity = 0.3,
                StrokeThickness = 1,
                Points = new PointCollection(polyPoints_yellow_medium)
            };

            Canvas_sas360_data_draw.Children.Add(polygon_yellow_medium);

            Canvas.SetLeft(polygon_yellow_medium, center_left);
            Canvas.SetTop(polygon_yellow_medium, center_top);

            #endregion

            #endregion


            #region Orange

            #region Area orange background

            double cateto_ady_left_orange_background = Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE];
            double lado_opuesto_left_orange_background = cateto_ady_left_orange_background * Math.Tan(angle_in_radians);

            double cateto_ady_front_orange_background = Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE];
            double lado_opuesto_front_orange_background = cateto_ady_front_orange_background * Math.Tan(angle_in_radians);

            List<Point> polyPoints_orange_background = new()
            {
                new Point(-cateto_ady_left_orange_background, lado_opuesto_left_orange_background),
                new Point(-cateto_ady_left_orange_background, -lado_opuesto_left_orange_background),
                new Point(-lado_opuesto_front_orange_background, -cateto_ady_front_orange_background),
                new Point(lado_opuesto_front_orange_background, -cateto_ady_front_orange_background),
                new Point(cateto_ady_left_orange_background, -lado_opuesto_left_orange_background),
                new Point(cateto_ady_left_orange_background, lado_opuesto_left_orange_background),
                new Point(lado_opuesto_front_orange_background, cateto_ady_front_orange_background),
                new Point(-lado_opuesto_front_orange_background, cateto_ady_front_orange_background),
            };

            Polygon polygon_orange_background = new()
            {
                Stroke = limit_brush,
                Opacity = 1,
                Fill = Brushes.White,
                StrokeThickness = 2,
                Points = new PointCollection(polyPoints_orange_background)
            };

            Canvas_sas360_data_draw.Children.Add(polygon_orange_background);

            Canvas.SetLeft(polygon_orange_background, center_left);
            Canvas.SetTop(polygon_orange_background, center_top);

            #endregion

            #region Area orange ext

            double cateto_ady_left_orange_ext = Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE];
            double lado_opuesto_left_orange_ext = cateto_ady_left_orange_ext * Math.Tan(angle_in_radians);

            double cateto_ady_front_orange_ext = Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE];
            double lado_opuesto_front_orange_ext = cateto_ady_front_orange_ext * Math.Tan(angle_in_radians);

            List<Point> polyPoints_orange_ext = new()
            {
                new Point(-cateto_ady_left_orange_ext, lado_opuesto_left_orange_ext),
                new Point(-cateto_ady_left_orange_ext, -lado_opuesto_left_orange_ext),
                new Point(-lado_opuesto_front_orange_ext, -cateto_ady_front_orange_ext),
                new Point(lado_opuesto_front_orange_ext, -cateto_ady_front_orange_ext),
                new Point(cateto_ady_left_orange_ext, -lado_opuesto_left_orange_ext),
                new Point(cateto_ady_left_orange_ext, lado_opuesto_left_orange_ext),
                new Point(lado_opuesto_front_orange_ext, cateto_ady_front_orange_ext),
                new Point(-lado_opuesto_front_orange_ext, cateto_ady_front_orange_ext),
            };

            Polygon polygon_orange_ext = new()
            {
                Stroke = limit_brush,
                Opacity = 0.3,
                Fill = Brushes.Orange,
                StrokeThickness = 2,
                Points = new PointCollection(polyPoints_orange_ext)
            };

            Canvas_sas360_data_draw.Children.Add(polygon_orange_ext);

            Canvas.SetLeft(polygon_orange_ext, center_left);
            Canvas.SetTop(polygon_orange_ext, center_top);

            #endregion

            #region Area orange medium

            double cateto_ady_left_orange_medium = Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE] - (orange_red_width_diff / 4);
            double lado_opuesto_left_orange_medium = cateto_ady_left_orange_medium * Math.Tan(angle_in_radians);

            double cateto_ady_front_orange_medium = Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE] - (orange_red_width_diff / 4);
            double lado_opuesto_front_orange_medium = cateto_ady_front_orange_medium * Math.Tan(angle_in_radians);

            List<Point> polyPoints_orange_medium = new()
            {
                new Point(-cateto_ady_left_orange_medium, lado_opuesto_left_orange_medium),
                new Point(-cateto_ady_left_orange_medium, -lado_opuesto_left_orange_medium),
                new Point(-lado_opuesto_front_orange_medium, -cateto_ady_front_orange_medium),
                new Point(lado_opuesto_front_orange_medium, -cateto_ady_front_orange_medium),
                new Point(cateto_ady_left_orange_medium, -lado_opuesto_left_orange_medium),
                new Point(cateto_ady_left_orange_medium, lado_opuesto_left_orange_medium),
                new Point(lado_opuesto_front_orange_medium, cateto_ady_front_orange_medium),
                new Point(-lado_opuesto_front_orange_medium, cateto_ady_front_orange_medium),
            };

            Polygon polygon_orange_medium = new()
            {
                Stroke = limit_brush,
                Opacity = 0.3,
                StrokeThickness = 1,
                Points = new PointCollection(polyPoints_orange_medium)
            };

            Canvas_sas360_data_draw.Children.Add(polygon_orange_medium);

            Canvas.SetLeft(polygon_orange_medium, center_left);
            Canvas.SetTop(polygon_orange_medium, center_top);

            #endregion

            #endregion

            #region Red

            #region Area red background

            double cateto_ady_left_red_background = Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.RED];
            double lado_opuesto_left_red_background = cateto_ady_left_red_background * Math.Tan(angle_in_radians);

            double cateto_ady_front_red_background = Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.RED];
            double lado_opuesto_front_red_background = cateto_ady_front_red_background * Math.Tan(angle_in_radians);

            List<Point> polyPoints_red_background = new()
            {
                new Point(-cateto_ady_left_red_background, lado_opuesto_left_red_background),
                new Point(-cateto_ady_left_red_background, -lado_opuesto_left_red_background),
                new Point(-lado_opuesto_front_red_background, -cateto_ady_front_red_background),
                new Point(lado_opuesto_front_red_background, -cateto_ady_front_red_background),
                new Point(cateto_ady_left_red_background, -lado_opuesto_left_red_background),
                new Point(cateto_ady_left_red_background, lado_opuesto_left_red_background),
                new Point(lado_opuesto_front_red_background, cateto_ady_front_red_background),
                new Point(-lado_opuesto_front_red_background, cateto_ady_front_red_background),
            };

            Polygon polygon_red_background = new()
            {
                Stroke = limit_brush,
                Opacity = 1,
                Fill = Brushes.White,
                StrokeThickness = 2,
                Points = new PointCollection(polyPoints_red_background)
            };

            Canvas_sas360_data_draw.Children.Add(polygon_red_background);

            Canvas.SetLeft(polygon_red_background, center_left);
            Canvas.SetTop(polygon_red_background, center_top);

            #endregion

            #region Area red ext

            double cateto_ady_left_red_ext = Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.RED];
            double lado_opuesto_left_red_ext = cateto_ady_left_red_ext * Math.Tan(angle_in_radians);

            double cateto_ady_front_red_ext = Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.RED];
            double lado_opuesto_front_red_ext = cateto_ady_front_red_ext * Math.Tan(angle_in_radians);

            List<Point> polyPoints_red_ext = new()
            {
                new Point(-cateto_ady_left_red_ext, lado_opuesto_left_red_ext),
                new Point(-cateto_ady_left_red_ext, -lado_opuesto_left_red_ext),
                new Point(-lado_opuesto_front_red_ext, -cateto_ady_front_red_ext),
                new Point(lado_opuesto_front_red_ext, -cateto_ady_front_red_ext),
                new Point(cateto_ady_left_red_ext, -lado_opuesto_left_red_ext),
                new Point(cateto_ady_left_red_ext, lado_opuesto_left_red_ext),
                new Point(lado_opuesto_front_red_ext, cateto_ady_front_red_ext),
                new Point(-lado_opuesto_front_red_ext, cateto_ady_front_red_ext),
            };

            Polygon polygon_red_ext = new()
            {
                Stroke = limit_brush,
                Opacity = 0.3,
                Fill = Brushes.Red,
                StrokeThickness = 2,
                Points = new PointCollection(polyPoints_red_ext)
            };

            Canvas_sas360_data_draw.Children.Add(polygon_red_ext);

            Canvas.SetLeft(polygon_red_ext, center_left);
            Canvas.SetTop(polygon_red_ext, center_top);

            #endregion

            #region Area red medium

            double cateto_ady_left_red_medium = Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.RED] / 4;
            double lado_opuesto_left_red_medium = cateto_ady_left_red_medium * Math.Tan(angle_in_radians);

            double cateto_ady_front_red_medium = Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.RED] / 4;
            double lado_opuesto_front_red_medium = cateto_ady_front_red_medium * Math.Tan(angle_in_radians);

            List<Point> polyPoints_red_medium = new()
            {
                new Point(-cateto_ady_left_red_medium, lado_opuesto_left_red_medium),
                new Point(-cateto_ady_left_red_medium, -lado_opuesto_left_red_medium),
                new Point(-lado_opuesto_front_red_medium, -cateto_ady_front_red_medium),
                new Point(lado_opuesto_front_red_medium, -cateto_ady_front_red_medium),
                new Point(cateto_ady_left_red_medium, -lado_opuesto_left_red_medium),
                new Point(cateto_ady_left_red_medium, lado_opuesto_left_red_medium),
                new Point(lado_opuesto_front_red_medium, cateto_ady_front_red_medium),
                new Point(-lado_opuesto_front_red_medium, cateto_ady_front_red_medium),
            };

            Polygon polygon_red_medium = new()
            {
                Stroke = limit_brush,
                Opacity = 0.3,
                StrokeThickness = 1,
                Points = new PointCollection(polyPoints_red_medium)
            };

            Canvas_sas360_data_draw.Children.Add(polygon_red_medium);

            Canvas.SetLeft(polygon_red_medium, center_left);
            Canvas.SetTop(polygon_red_medium, center_top);

            #endregion

            #region Area red down

            double cateto_ady_left_red_int = Array_area_LEFT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.RED] / 2;
            double lado_opuesto_left_red_int = cateto_ady_left_red_int * Math.Tan(angle_in_radians);

            double cateto_ady_front_red_int = Array_area_FRONT_ANRI_dist_pixel[(int)DETECTION_AREA_POS_IN_ARRAY.RED] / 2;
            double lado_opuesto_front_red_int = cateto_ady_front_red_int * Math.Tan(angle_in_radians);


            List<Point> polyPoints_red_int = new()
            {
                new Point(-cateto_ady_left_red_int, lado_opuesto_left_red_int),
                new Point(-cateto_ady_left_red_int, -lado_opuesto_left_red_int),
                new Point(-lado_opuesto_front_red_int, -cateto_ady_front_red_int),
                new Point(lado_opuesto_front_red_int, -cateto_ady_front_red_int),
                new Point(cateto_ady_left_red_int, -lado_opuesto_left_red_int),
                new Point(cateto_ady_left_red_int, lado_opuesto_left_red_int),
                new Point(lado_opuesto_front_red_int, cateto_ady_front_red_int),
                new Point(-lado_opuesto_front_red_int, cateto_ady_front_red_int),
            };

            Polygon polygon_red_int = new()
            {
                Stroke = limit_brush,
                Opacity = 1,
                Fill = Brushes.White,
                StrokeThickness = 2,
                Points = new PointCollection(polyPoints_red_int)
            };

            Canvas_sas360_data_draw.Children.Add(polygon_red_int);

            Canvas.SetLeft(polygon_red_int, center_left);
            Canvas.SetTop(polygon_red_int, center_top);

            #endregion

            #endregion

        }

        #endregion


        #region Draw tag ellipses

        private void Draw_tag_ellipses()
        {

            for (int index = 0; index < Globals.GetTheInstance().Total_closest_tags; index++)
            {
                Ellipse Ellipse_tag = new Ellipse
                {
                    Name = $"Ellipse_tag{index}",
                    Width = 13,
                    Height = 13,
                    Stroke = new SolidColorBrush(Colors.Black),
                };
                RegisterName($"Ellipse_tag{index}", Ellipse_tag);

                Ellipse_tag.MouseEnter += new MouseEventHandler(EllipseTagOnMouseEnter);
                Ellipse_tag.MouseLeave += new MouseEventHandler(EllipseTagOnMouseLeave);
                Ellipse_tag.MouseLeftButtonDown += new MouseButtonEventHandler(EllipseTagMouseLeftButtonDown);
                Canvas_sas360_data_draw.Children.Add(Ellipse_tag);
                Panel.SetZIndex(Ellipse_tag, 1000);

                m_list_ellipse_sas360tag.Add(Ellipse_tag);

                ColorAnimation? color_anim = ((Storyboard)Resources["TagZoneStrokeStoryBoard"]).Children[0] as ColorAnimation;
                color_anim!.SetValue(Storyboard.TargetNameProperty, Ellipse_tag.Name);
                Storyboard storyboard = ((Storyboard)Resources["TagZoneStrokeStoryBoard"]);
                storyboard.Begin();
            }
        }

        #endregion

        #region Draw zones ellipses

        private void Draw_zone_ellipses()
        {
            if (Globals.GetTheInstance().Draw_map == BIT_STATE.ON)
            {
                DoubleCollection stroke_array = new(new double[] { 5, 1 });

                for (int index = 0; index < Globals.GetTheInstance().Total_closest_zone; index++)
                {
                    Grid Grid_zone = new()
                    {
                        Name = $"Grid_zone{index}",
                    };
                    RegisterName($"Grid_zone{index}", Grid_zone);

                    Ellipse Ellipse_zone_int = new Ellipse
                    {
                        Name = $"Ellipse_zone_int{index}",
                        Width = 13,
                        Height = 13,
                        Stroke = new SolidColorBrush(Colors.Black),
                    };
                    RegisterName($"Ellipse_zone_int{index}", Ellipse_zone_int);

                    Ellipse_zone_int.MouseEnter += new MouseEventHandler(EllipseZoneOnMouseEnter);
                    Ellipse_zone_int.MouseLeave += new MouseEventHandler(EllipseZoneOnMouseLeave);
                    Ellipse_zone_int.MouseLeftButtonDown += new MouseButtonEventHandler(EllipseZoneMouseLeftButtonDown);

                    Ellipse Ellipse_zone_ext = new()
                    {
                        Name = $"Ellipse_zone_ext{index}",
                        Width = 40,
                        Height = 40,
                        Stroke = new SolidColorBrush(Colors.Black),
                        StrokeDashArray = stroke_array
                    };
                    RegisterName($"Ellipse_zone_ext{index}", Ellipse_zone_ext);

                    Grid_zone.Children.Add(Ellipse_zone_int);
                    Grid_zone.Children.Add(Ellipse_zone_ext);
                    Canvas_sas360_data_draw.Children.Add(Grid_zone);
                    Panel.SetZIndex(Grid_zone, 1000);

                    m_list_grid_sas360zone.Add(Grid_zone);
                    m_list_ellipse_ext_sas360zone.Add(Ellipse_zone_ext);
                    m_list_ellipse_int_sas360zone.Add(Ellipse_zone_int);

                    ColorAnimation? color_anim = ((Storyboard)Resources["TagZoneStrokeStoryBoard"]).Children[0] as ColorAnimation;
                    color_anim!.SetValue(Storyboard.TargetNameProperty, Ellipse_zone_int.Name);
                    Storyboard storyboard = ((Storyboard)Resources["TagZoneStrokeStoryBoard"]);
                    storyboard.Begin();
                }
            }
        }

        #endregion

        #endregion



        #region Refresh list sources

        private void Refresh_list_closest_tags_zones_main()
        {
            m_collection_sas360_tag_processed.Clear();

            Listview_tag_processed.ItemsSource = m_collection_sas360_tag_processed;
            CollectionViewSource.GetDefaultView(Listview_tag_processed.ItemsSource).Refresh();

            m_collection_sas360_zone_processed.Clear();

            Listview_zone_processed.ItemsSource = m_collection_sas360_zone_processed;
            CollectionViewSource.GetDefaultView(Listview_zone_processed.ItemsSource).Refresh();
        }

        private void Refresh_list_closest_tags_base()
        {
            m_collection_console_closest_tags_base.Clear();

            Globals.GetTheInstance().List_console_closest_tags_base.ForEach(closest => m_collection_console_closest_tags_base.Add(closest));
            m_collection_console_closest_tags_base = new ObservableCollection<Modbus_var>(m_collection_console_closest_tags_base.OrderBy(closest => closest.Addr));
            Listview_console_closest_tags_base.ItemsSource = m_collection_console_closest_tags_base;
            CollectionViewSource.GetDefaultView(Listview_console_closest_tags_base.ItemsSource).Refresh();
        }

        private void Refresh_list_closest_tags_extended()
        {
            m_collection_console_closest_tags_extended.Clear();

            Globals.GetTheInstance().List_console_closest_tags_extended.ForEach(closest => m_collection_console_closest_tags_extended.Add(closest));
            m_collection_console_closest_tags_extended = new ObservableCollection<Modbus_var>(m_collection_console_closest_tags_extended.OrderBy(closest => closest.Addr));
            Listview_console_closest_tags_extended.ItemsSource = m_collection_console_closest_tags_extended;
            CollectionViewSource.GetDefaultView(Listview_console_closest_tags_extended.ItemsSource).Refresh();
        }

        private void Refresh_list_closest_zones_base()
        {
            m_collection_console_closest_zone_base.Clear();

            Globals.GetTheInstance().List_console_closest_zone_base.ForEach(closest => m_collection_console_closest_zone_base.Add(closest));
            m_collection_console_closest_zone_base = new ObservableCollection<Modbus_var>(m_collection_console_closest_zone_base.OrderBy(closest => closest.Addr));
            Listview_console_closest_zone_base.ItemsSource = m_collection_console_closest_zone_base;
            CollectionViewSource.GetDefaultView(Listview_console_closest_zone_base.ItemsSource).Refresh();
        }

        private void Refresh_list_closest_zones_extended()
        {
            m_collection_console_closest_zone_extended.Clear();

            Globals.GetTheInstance().List_console_closest_zone_extended.ForEach(closest => m_collection_console_closest_zone_extended.Add(closest));
            m_collection_console_closest_zone_extended = new ObservableCollection<Modbus_var>(m_collection_console_closest_zone_extended.OrderBy(closest => closest.Addr));
            Listview_console_closest_zone_extended.ItemsSource = m_collection_console_closest_zone_extended;
            CollectionViewSource.GetDefaultView(Listview_console_closest_zone_extended.ItemsSource).Refresh();
        }

        private void Refresh_list_uwb_tags_base()
        {
            m_collection_uwb_closest_tags_base.Clear();

            Globals.GetTheInstance().List_uwb_closest_tags_base.ForEach(closest => m_collection_uwb_closest_tags_base.Add(closest));
            m_collection_uwb_closest_tags_base = new ObservableCollection<Modbus_var>(m_collection_uwb_closest_tags_base.OrderBy(closest => closest.Addr));
            Listview_uwb_closest_tags_base.ItemsSource = m_collection_uwb_closest_tags_base;
            CollectionViewSource.GetDefaultView(Listview_uwb_closest_tags_base.ItemsSource).Refresh();
        }

        private void Refresh_list_uwb_tags_extended()
        {
            m_collection_uwb_closest_tags_extended.Clear();

            Globals.GetTheInstance().List_uwb_closest_tags_extended.ForEach(closest => m_collection_uwb_closest_tags_extended.Add(closest));
            m_collection_uwb_closest_tags_extended = new ObservableCollection<Modbus_var>(m_collection_uwb_closest_tags_extended.OrderBy(closest => closest.Addr));
            Listview_uwb_closest_tags_extended.ItemsSource = m_collection_uwb_closest_tags_extended;
            CollectionViewSource.GetDefaultView(Listview_uwb_closest_tags_extended.ItemsSource).Refresh();
        }


        private void Refresh_list_SAS360CON_internal_cfg()
        {
            m_collection_sas360con_internal_cfg.Clear();

            Globals.GetTheInstance().List_sas360con_internal_cfg.ForEach(internal_config => m_collection_sas360con_internal_cfg.Add(internal_config));
            m_collection_sas360con_internal_cfg = new ObservableCollection<Modbus_var>(m_collection_sas360con_internal_cfg.OrderBy(internal_config => internal_config.Addr));
            Listview_sas360con_internal_cfg.ItemsSource = m_collection_sas360con_internal_cfg;
            CollectionViewSource.GetDefaultView(Listview_sas360con_internal_cfg.ItemsSource).Refresh();
        }

        private void Refresh_list_SAS360CON_cfg()
        {
            m_collection_sas360con_cfg.Clear();

            Globals.GetTheInstance().List_sas360con_cfg.ForEach(config => m_collection_sas360con_cfg.Add(config));
            m_collection_sas360con_cfg = new ObservableCollection<Modbus_var>(m_collection_sas360con_cfg.OrderBy(config => config.Addr));
            Listview_sas360con_cfg.ItemsSource = m_collection_sas360con_cfg;
            CollectionViewSource.GetDefaultView(Listview_sas360con_cfg.ItemsSource).Refresh();
        }

        private void Refresh_list_iot_cfg()
        {
            m_collection_iot_cfg.Clear();

            Globals.GetTheInstance().List_iot_cfg.ForEach(config => m_collection_iot_cfg.Add(config));
            m_collection_iot_cfg = new ObservableCollection<Modbus_var>(m_collection_iot_cfg.OrderBy(config => config.Addr));
            Listview_iot_cfg.ItemsSource = m_collection_iot_cfg;
            CollectionViewSource.GetDefaultView(Listview_iot_cfg.ItemsSource).Refresh();
        }

        private void Refresh_list_uwb_internal_cfg()
        {
            m_collection_uwb_internal_cfg.Clear();

            Globals.GetTheInstance().List_uwb_internal_cfg.ForEach(config => m_collection_uwb_internal_cfg.Add(config));
            m_collection_uwb_internal_cfg = new ObservableCollection<Modbus_var>(m_collection_uwb_internal_cfg.OrderBy(config => config.Addr));
            Listview_uwb_internal_cfg.ItemsSource = m_collection_uwb_internal_cfg;
            CollectionViewSource.GetDefaultView(Listview_uwb_internal_cfg.ItemsSource).Refresh();
        }

        private void Refresh_list_SAS360CON_image()
        {
            m_collection_sas360con_image.Clear();

            Globals.GetTheInstance().List_sas360con_image_filter.ForEach(image => m_collection_sas360con_image.Add(image));
            m_collection_sas360con_image = new ObservableCollection<Modbus_var>(m_collection_sas360con_image.OrderBy(image => image.Addr));
            Listview_sas360con_image.ItemsSource = m_collection_sas360con_image;
            CollectionViewSource.GetDefaultView(Listview_sas360con_image.ItemsSource).Refresh();
        }

        private void Refresh_list_SAS360IOT_image()
        {
            m_collection_iot_image.Clear();

            Globals.GetTheInstance().List_iot_image.ForEach(image => m_collection_iot_image.Add(image));
            m_collection_iot_image = new ObservableCollection<Modbus_var>(m_collection_iot_image.OrderBy(image => image.Addr));
            Listview_iot_image.ItemsSource = m_collection_iot_image;
            CollectionViewSource.GetDefaultView(Listview_iot_image.ItemsSource).Refresh();
        }

        private void Refresh_list_SAS360UWB_image()
        {
            m_collection_uwb_image.Clear();

            Globals.GetTheInstance().List_uwb_image.ForEach(image => m_collection_uwb_image.Add(image));
            m_collection_uwb_image = new ObservableCollection<Modbus_var>(m_collection_uwb_image.OrderBy(image => image.Addr));
            Listview_uwb_image.ItemsSource = m_collection_uwb_image;
            CollectionViewSource.GetDefaultView(Listview_uwb_image.ItemsSource).Refresh();
        }

        private void Refresh_list_SAS360CON_maintennance()
        {
            m_collection_sas360con_maintennance.Clear();

            Globals.GetTheInstance().List_sas360con_maintennance.ForEach(maint => m_collection_sas360con_maintennance.Add(maint));
            m_collection_sas360con_maintennance = new ObservableCollection<Modbus_var>(m_collection_sas360con_maintennance.OrderBy(maint => maint.Addr));
            Listview_sas360con_maintennance.ItemsSource = m_collection_sas360con_maintennance;
            CollectionViewSource.GetDefaultView(Listview_sas360con_maintennance.ItemsSource).Refresh();
        }

        private void Refresh_list_SAS360CON_nvreg()
        {
            m_collection_sas360con_nvreg.Clear();

            Globals.GetTheInstance().List_sas360con_nvreg.ForEach(nvreg => m_collection_sas360con_nvreg.Add(nvreg));
            m_collection_sas360con_nvreg = new ObservableCollection<Modbus_var>(m_collection_sas360con_nvreg.OrderBy(nvreg => nvreg.Addr));
            Listview_sas360con_nvreg.ItemsSource = m_collection_sas360con_nvreg;
            CollectionViewSource.GetDefaultView(Listview_sas360con_nvreg.ItemsSource).Refresh();
        }

        private void Refresh_list_SAS360CON_commands()
        {
            m_collection_sas360con_commands.Clear();

            Globals.GetTheInstance().List_sas360con_commands.ForEach(command => m_collection_sas360con_commands.Add(command));
            m_collection_sas360con_commands = new ObservableCollection<Modbus_command>(m_collection_sas360con_commands.OrderBy(command => command.Index));
            Listview_sas360con_commands.ItemsSource = m_collection_sas360con_commands;
            CollectionViewSource.GetDefaultView(Listview_sas360con_commands.ItemsSource).Refresh();
        }

        private void Refresh_list_SAS360CON_event_log()
        {
            m_collection_sas360con_event_log.Clear();
            Listview_sas360con_event_log.ItemsSource = m_collection_sas360con_event_log;
            CollectionViewSource.GetDefaultView(Listview_sas360con_event_log.ItemsSource).Refresh();
        }

        private void Refresh_list_SAS360CON_hist_log()
        {
            m_collection_sas360con_hist_log.Clear();
            Listview_sas360con_hist_log.ItemsSource = m_collection_sas360con_hist_log;
            CollectionViewSource.GetDefaultView(Listview_sas360con_hist_log.ItemsSource).Refresh();
        }

        #endregion


        #region Menu window

        #region Minimize

        private void Button_minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }


        #endregion

        #region Maximize

        private void Button_maximize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            Button_maximize.Visibility = Visibility.Collapsed;
            Button_normal_window.Visibility = Visibility.Visible;
        }

        #endregion

        #region Normal window

        private void Button_normal_window_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            Button_maximize.Visibility = Visibility.Visible;
            Button_normal_window.Visibility = Visibility.Collapsed;
        }

        #endregion

        #region Exit

        private void Button_exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion

        #endregion


    }
}
