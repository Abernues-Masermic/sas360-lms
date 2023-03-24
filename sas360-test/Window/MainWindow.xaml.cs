using CsvHelper;
using CsvHelper.Configuration;
using sas360_test.CustomControl;
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


namespace sas360_test
{

    public partial class MainWindow : Window
    {
        #region Main

        private List<Label> m_list_label_output_states = new();
        private List<Label> m_list_label_processing_task = new();
        private List<Label> m_list_label_popup_image_info = new();

        private List<Label> m_list_label_uwb_id = new();
        private List<Label> m_list_label_uwb_slave = new();
        private List<Label> m_list_label_uwb_lin_com = new();
        private List<Label> m_list_label_uwb_lin_error = new();
        private List<Label> m_list_label_uwb_cycle_time = new();
        private List<Label> m_list_label_uwb_state = new();
        private List<Label> m_list_label_uwb_num_tags = new();
        private List<Label> m_list_label_uwb_num_zones = new();

        #endregion

        #region Map

        private List<Rectangle> m_array_rectangle_antenna = new();
        private List<Ellipse> m_list_ellipse_sas360tag = new();
        private List<Grid> m_list_grid_sas360zone = new();
        private List<Ellipse> m_list_ellipse_int_sas360zone = new();
        private List<Ellipse> m_list_ellipse_ext_sas360zone = new();
        private Border[] m_array_border_detection = new Border[4];
        private Border[] m_array_led_detection = new Border[32];

        #endregion

        #region SAS360CON config

        #region Internal config

        private List<Label> m_list_label_edit_internal_config_title = new();
        private List<Button> m_list_button_edit_internal_config = new();
        private List<Label> m_list_label_edit_internal_config_value = new();

        #endregion

        #region Config SAS360CON

        private Label[,] m_array_config_label_antenna = new Label[3, 3];

        private List<Label> m_list_label_config_yellow_area = new();
        private List<Label> m_list_label_config_orange_area = new();
        private List<Label> m_list_label_config_red_area = new();
        private List<Label> m_list_label_config_hyst_out_area = new();

        private List<Border> m_list_border_sas360_config_rele1 = new();
        private List<Border> m_list_border_sas360_config_rele2 = new();
        private List<Border> m_list_border_sas360_config_rele3 = new();
        private List<Border> m_list_border_sas360_config_rele4 = new();
        private List<Border> m_list_border_sas360_config_trans1 = new();
        private List<Border> m_list_border_sas360_config_trans2 = new();

        private List<Label> m_list_label_uwb_config_id_lin = new();
        private List<Label> m_list_label_uwb_config_id_slave = new();

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

        private List<WrapPanel> m_list_wrappanel_commands = new();
        private List<Label> m_list_label_param_commands = new();
        private List<Xceed.Wpf.Toolkit.DecimalUpDown> m_list_decimalupdown_value_commands = new();
        private List<Label> m_list_label_type_commands = new();

        #endregion

        #region Memory

        private List<ListView> m_list_listview_memory = new();
        private List<Button> m_list_button_refresh_memory = new();
        private List<Button> m_list_button_new_memory = new();
        private List<Button> m_list_button_delete_memory = new();

        #endregion

        #region Collections

        private ObservableCollection<Sas360_uwb> m_collection_sas360_uwb;

        private ObservableCollection<Sas360_tag> m_collection_sas360_tag_processed;
        private ObservableCollection<Sas360_zone> m_collection_sas360_zone_processed;

        private ObservableCollection<Modbus_var> m_collection_internal_config;
        private ObservableCollection<Modbus_var> m_collection_config_sas360con;
        private ObservableCollection<Modbus_var> m_collection_config_iot;
        private ObservableCollection<Modbus_var> m_collection_image_sas360con;
        private ObservableCollection<Modbus_var> m_collection_image_iot;

        private ObservableCollection<Modbus_var> m_collection_console_closest_tags_base;
        private ObservableCollection<Modbus_var> m_collection_console_closest_tags_extended;
        private ObservableCollection<Modbus_var> m_collection_uwb_closest_tags_base;
        private ObservableCollection<Modbus_var> m_collection_uwb_closest_tags_extended;

        private ObservableCollection<Modbus_var> m_collection_console_closest_zone_base;
        private ObservableCollection<Modbus_var> m_collection_console_closest_zone_extended;

        private ObservableCollection<Modbus_var> m_collection_nvreg;

        private ObservableCollection<Modbus_command> m_collection_commands;

        private ObservableCollection<Event_log> m_collection_event_log;
        private ObservableCollection<Hist_log> m_collection_hist_log;

        #endregion

        private Sas360_tag m_selected_tag;
        private Sas360_zone m_selected_zone;

        private Modbus_var m_selected_modbus_var;
        private Modbus_command m_selected_modbus_command;
        private ushort m_command_watchdog = 0;

        private MEMORY_READ_STATE m_memory_read_state = MEMORY_READ_STATE.IMAGE_SAS360CON;

        private System.Timers.Timer m_timer_read_memory;
        private System.Timers.Timer m_timer_read_event_log;
        private System.Timers.Timer m_timer_read_hist_log;

        private bool b_read_event_log = false;
        private int m_total_reg_event_log;
        private int m_pos_reg_event_log = 0;

        private bool b_read_hist_log = false;
        private int m_total_reg_hist_log;
        private int m_pos_reg_hist_log = 0;



        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            Globals.GetTheInstance().Manage_delegate = new Manage_delegate();
            Globals.GetTheInstance().Manage_delegate.RTU_handler_event += new Manage_delegate.RTU_handler(RTU_events_to_main);

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

            m_list_label_processing_task.Add(Label_processing_task1);
            m_list_label_processing_task.Add(Label_processing_task2);
            m_list_label_processing_task.Add(Label_processing_task3);
            m_list_label_processing_task.Add(Label_processing_task4);
            m_list_label_processing_task.Add(Label_processing_task5);
            m_list_label_processing_task.Add(Label_processing_task6);

            m_list_label_uwb_id.Add(Label_name_uwb_id_value1);
            m_list_label_uwb_id.Add(Label_name_uwb_id_value2);
            m_list_label_uwb_id.Add(Label_name_uwb_id_value3);

            m_list_label_uwb_slave.Add(Label_name_uwb_slave_value1);
            m_list_label_uwb_slave.Add(Label_name_uwb_slave_value2);
            m_list_label_uwb_slave.Add(Label_name_uwb_slave_value3);

            m_list_label_uwb_lin_com.Add(Label_name_uwb_lin_com_value1);
            m_list_label_uwb_lin_com.Add(Label_name_uwb_lin_com_value2);
            m_list_label_uwb_lin_com.Add(Label_name_uwb_lin_com_value3);

            m_list_label_uwb_lin_error.Add(Label_name_uwb_lin_error_value1);
            m_list_label_uwb_lin_error.Add(Label_name_uwb_lin_error_value2);
            m_list_label_uwb_lin_error.Add(Label_name_uwb_lin_error_value3);

            m_list_label_uwb_cycle_time.Add(Label_name_uwb_cycle_time_value1);
            m_list_label_uwb_cycle_time.Add(Label_name_uwb_cycle_time_value2);
            m_list_label_uwb_cycle_time.Add(Label_name_uwb_cycle_time_value3);

            m_list_label_uwb_state.Add(Label_name_uwb_state_value1);
            m_list_label_uwb_state.Add(Label_name_uwb_state_value2);
            m_list_label_uwb_state.Add(Label_name_uwb_state_value3);

            m_list_label_uwb_num_tags.Add(Label_name_uwb_num_tags_value1);
            m_list_label_uwb_num_tags.Add(Label_name_uwb_num_tags_value2);
            m_list_label_uwb_num_tags.Add(Label_name_uwb_num_tags_value3);

            m_list_label_uwb_num_zones.Add(Label_name_uwb_num_zones_value1);
            m_list_label_uwb_num_zones.Add(Label_name_uwb_num_zones_value2);
            m_list_label_uwb_num_zones.Add(Label_name_uwb_num_zones_value3);

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
            m_list_label_popup_image_info.Add(Label_main_pooling_config);
            m_list_label_popup_image_info.Add(Label_main_pooling_state);

            #endregion

            #region SAS360 CONFIG

            #region Internal config

            m_list_label_edit_internal_config_title.Add(Label_config_serial_number_title);
            m_list_label_edit_internal_config_title.Add(Label_config_id_2lsb_sas360con_title);
            m_list_label_edit_internal_config_title.Add(Label_config_id_tag_sas360con_title);
            m_list_label_edit_internal_config_title.Add(Label_config_rtu_slave_speed_title);
            m_list_label_edit_internal_config_title.Add(Label_config_rtu_slave_num_title);
            m_list_label_edit_internal_config_title.Add(Label_config_modbus_lin_master_speed_title);

            m_list_label_edit_internal_config_value.Add(Label_config_serial_number_value);
            m_list_label_edit_internal_config_value.Add(Label_config_id_2lsb_sas360con_value);
            m_list_label_edit_internal_config_value.Add(Label_config_id_tag_sas360con_value);
            m_list_label_edit_internal_config_value.Add(Label_config_rtu_slave_speed_value);
            m_list_label_edit_internal_config_value.Add(Label_config_rtu_slave_num_value);
            m_list_label_edit_internal_config_value.Add(Label_config_modbus_lin_master_speed_value);

            m_list_button_edit_internal_config.Add(Button_edit_serial_number);
            m_list_button_edit_internal_config.Add(Button_edit_id_2lsb_sas360con);
            m_list_button_edit_internal_config.Add(Button_edit_id_tag_sas360con);
            m_list_button_edit_internal_config.Add(Button_edit_rtu_slave_speed);
            m_list_button_edit_internal_config.Add(Button_edit_rtu_slave_num);
            m_list_button_edit_internal_config.Add(Button_edit_modbus_lin_master_speed);


            #endregion

            #region Vehicle config

            m_array_config_label_antenna[(int)LABEL_ANTENNA_ARRAY_POS.POS_X, 0] = Label_config_antena1_pos_x;
            m_array_config_label_antenna[(int)LABEL_ANTENNA_ARRAY_POS.POS_X, 1] = Label_config_antena2_pos_x;
            m_array_config_label_antenna[(int)LABEL_ANTENNA_ARRAY_POS.POS_X, 2] = Label_config_antena3_pos_x;

            m_array_config_label_antenna[(int)LABEL_ANTENNA_ARRAY_POS.POS_Y, 0] = Label_config_antena1_pos_y;
            m_array_config_label_antenna[(int)LABEL_ANTENNA_ARRAY_POS.POS_Y, 1] = Label_config_antena2_pos_y;
            m_array_config_label_antenna[(int)LABEL_ANTENNA_ARRAY_POS.POS_Y, 2] = Label_config_antena3_pos_y;

            m_array_config_label_antenna[(int)LABEL_ANTENNA_ARRAY_POS.ORIENTATION, 0] = Label_config_antena1_orientation;
            m_array_config_label_antenna[(int)LABEL_ANTENNA_ARRAY_POS.ORIENTATION, 1] = Label_config_antena2_orientation;
            m_array_config_label_antenna[(int)LABEL_ANTENNA_ARRAY_POS.ORIENTATION, 2] = Label_config_antena3_orientation;

            #endregion

            #region Detection area

            m_list_label_config_yellow_area.Add(Label_config_yellow_area_front);
            m_list_label_config_yellow_area.Add(Label_config_yellow_area_right);
            m_list_label_config_yellow_area.Add(Label_config_yellow_area_back);
            m_list_label_config_yellow_area.Add(Label_config_yellow_area_left);

            m_list_label_config_orange_area.Add(Label_config_orange_area_front);
            m_list_label_config_orange_area.Add(Label_config_orange_area_right);
            m_list_label_config_orange_area.Add(Label_config_orange_area_back);
            m_list_label_config_orange_area.Add(Label_config_orange_area_left);

            m_list_label_config_red_area.Add(Label_config_red_area_front);
            m_list_label_config_red_area.Add(Label_config_red_area_right);
            m_list_label_config_red_area.Add(Label_config_red_area_back);
            m_list_label_config_red_area.Add(Label_config_red_area_left);

            m_list_label_config_hyst_out_area.Add(Label_config_hyst_out_area_d);
            m_list_label_config_hyst_out_area.Add(Label_config_hyst_out_area_a);
            m_list_label_config_hyst_out_area.Add(Label_config_hyst_out_area_n);
            m_list_label_config_hyst_out_area.Add(Label_config_hyst_out_area_r);
            m_list_label_config_hyst_out_area.Add(Label_config_sector_change_angle);
            m_list_label_config_hyst_out_area.Add(Label_config_closest_antenna_change_hyst);
            m_list_label_config_hyst_out_area.Add(Label_config_closest_xc_change_hyst);

            #endregion

            #region UWB CONFIG

            m_list_label_uwb_config_id_lin.Add(Label_uwb_config_id_lin1);
            m_list_label_uwb_config_id_lin.Add(Label_uwb_config_id_lin2);
            m_list_label_uwb_config_id_lin.Add(Label_uwb_config_id_lin3);

            m_list_label_uwb_config_id_slave.Add(Label_uwb_config_slave_lin1);
            m_list_label_uwb_config_id_slave.Add(Label_uwb_config_slave_lin2);
            m_list_label_uwb_config_id_slave.Add(Label_uwb_config_slave_lin3);

            #endregion

            #endregion

            #region I / O

            m_list_image_force_mode.Add(Image_edit_digital_outputs);
            m_list_image_force_mode.Add(Image_edit_leds);
            m_list_image_force_mode.Add(Image_edit_audio);
            m_list_image_force_mode.ForEach(image => image.Visibility = Visibility.Collapsed);

            #endregion

            #region Commands

            m_list_wrappanel_commands.Add(Wrappanel_param1);
            m_list_wrappanel_commands.Add(Wrappanel_param2);
            m_list_wrappanel_commands.Add(Wrappanel_param3);
            m_list_wrappanel_commands.Add(Wrappanel_param4);
            m_list_wrappanel_commands.Add(Wrappanel_param5);
            m_list_wrappanel_commands.Add(Wrappanel_param6);
            m_list_wrappanel_commands.Add(Wrappanel_param7);
            m_list_wrappanel_commands.Add(Wrappanel_param8);
            m_list_wrappanel_commands.Add(Wrappanel_param9);
            m_list_wrappanel_commands.ForEach(wrappanel => wrappanel.Visibility = Visibility.Collapsed);

            m_list_label_param_commands.Add(Label_param1);
            m_list_label_param_commands.Add(Label_param2);
            m_list_label_param_commands.Add(Label_param3);
            m_list_label_param_commands.Add(Label_param4);
            m_list_label_param_commands.Add(Label_param5);
            m_list_label_param_commands.Add(Label_param6);
            m_list_label_param_commands.Add(Label_param7);
            m_list_label_param_commands.Add(Label_param8);
            m_list_label_param_commands.Add(Label_param9);

            m_list_decimalupdown_value_commands.Add(DecimalUpDown_param1);
            m_list_decimalupdown_value_commands.Add(DecimalUpDown_param2);
            m_list_decimalupdown_value_commands.Add(DecimalUpDown_param3);
            m_list_decimalupdown_value_commands.Add(DecimalUpDown_param4);
            m_list_decimalupdown_value_commands.Add(DecimalUpDown_param5);
            m_list_decimalupdown_value_commands.Add(DecimalUpDown_param6);
            m_list_decimalupdown_value_commands.Add(DecimalUpDown_param7);
            m_list_decimalupdown_value_commands.Add(DecimalUpDown_param8);
            m_list_decimalupdown_value_commands.Add(DecimalUpDown_param9);

            m_list_decimalupdown_value_commands.ForEach(decimalupdown =>
            {
                decimalupdown.Value = 0;
                decimalupdown.Minimum = 0;
                decimalupdown.Maximum = ushort.MaxValue;
                decimalupdown.Increment = 1;
            });

            m_list_label_type_commands.Add(Label_type1);
            m_list_label_type_commands.Add(Label_type2);
            m_list_label_type_commands.Add(Label_type3);
            m_list_label_type_commands.Add(Label_type4);
            m_list_label_type_commands.Add(Label_type5);
            m_list_label_type_commands.Add(Label_type6);
            m_list_label_type_commands.Add(Label_type7);
            m_list_label_type_commands.Add(Label_type8);
            m_list_label_type_commands.Add(Label_type9);

            #endregion

            #region Listview

            m_list_listview_memory.Add(Listview_internal_config);
            m_list_listview_memory.Add(Listview_config_sas360con);
            m_list_listview_memory.Add(Listview_config_iot);
            m_list_listview_memory.Add(Listview_image_sas360con);
            m_list_listview_memory.Add(Listview_image_iot);
            m_list_listview_memory.Add(Listview_console_closest_tags_base);
            m_list_listview_memory.Add(Listview_console_closest_tags_extended);
            m_list_listview_memory.Add(Listview_uwb_closest_tags_base);
            m_list_listview_memory.Add(Listview_uwb_closest_tags_extended);
            m_list_listview_memory.Add(Listview_console_closest_zone_base);
            m_list_listview_memory.Add(Listview_console_closest_zone_extended);
            m_list_listview_memory.Add(Listview_nvreg);

            #endregion

            #region Buttons

            m_list_button_refresh_memory.Add(Button_refresh_internal_config);
            m_list_button_refresh_memory.Add(Button_refresh_config_sas360con);
            m_list_button_refresh_memory.Add(Button_refresh_config_iot);
            m_list_button_refresh_memory.Add(Button_refresh_image_sas360con);
            m_list_button_refresh_memory.Add(Button_refresh_image_iot);
            m_list_button_refresh_memory.Add(Button_refresh_con_closest_tag_base);
            m_list_button_refresh_memory.Add(Button_refresh_con_closest_tag_extended);
            m_list_button_refresh_memory.Add(Button_refresh_uwb_closest_tag_base);
            m_list_button_refresh_memory.Add(Button_refresh_uwb_closest_tag_extended);
            m_list_button_refresh_memory.Add(Button_refresh_con_closest_zone_base);
            m_list_button_refresh_memory.Add(Button_refresh_con_closest_zone_extended);
            m_list_button_refresh_memory.Add(Button_refresh_nvreg);

            m_list_button_new_memory.Add(Button_new_internal_config);
            m_list_button_new_memory.Add(Button_new_config_sas360con);
            m_list_button_new_memory.Add(Button_new_config_iot);
            m_list_button_new_memory.Add(Button_new_image_sas360con);
            m_list_button_new_memory.Add(Button_new_image_iot);
            m_list_button_new_memory.Add(Button_new_con_closest_tag_base);
            m_list_button_new_memory.Add(Button_new_con_closest_tag_extended);
            m_list_button_new_memory.Add(Button_new_uwb_closest_tag_base);
            m_list_button_new_memory.Add(Button_new_uwb_closest_tag_extended);
            m_list_button_new_memory.Add(Button_new_con_closest_zone_base);
            m_list_button_new_memory.Add(Button_new_con_closest_zone_extended);
            m_list_button_new_memory.Add(Button_new_nvreg);

            m_list_button_delete_memory.Add(Button_delete_internal_config);
            m_list_button_delete_memory.Add(Button_delete_config_sas360con);
            m_list_button_delete_memory.Add(Button_delete_config_iot);
            m_list_button_delete_memory.Add(Button_delete_image_sas360con);
            m_list_button_delete_memory.Add(Button_delete_image_iot);
            m_list_button_delete_memory.Add(Button_delete_con_closest_tag_base);
            m_list_button_delete_memory.Add(Button_delete_con_closest_tag_extended);
            m_list_button_delete_memory.Add(Button_delete_uwb_closest_tag_base);
            m_list_button_delete_memory.Add(Button_delete_uwb_closest_tag_extended);
            m_list_button_delete_memory.Add(Button_delete_con_closest_zone_base);
            m_list_button_delete_memory.Add(Button_delete_con_closest_zone_extended);
            m_list_button_delete_memory.Add(Button_delete_nvreg);

            #endregion

            #endregion


            #region Flash control led

            Ellipse_peaton_map.Fill = new SolidColorBrush(Colors.White);
            Ellipse_vehiculo_ligero.Fill = new SolidColorBrush(Colors.White);
            Ellipse_vehiculo_pesado.Fill = new SolidColorBrush(Colors.White);
            Ellipse_slow.Fill = new SolidColorBrush(Colors.White);
            Ellipse_on.Fill = new SolidColorBrush(Colors.White);
            Ellipse_driver.Fill = new SolidColorBrush(Colors.White);

            Grid_error_led_1.Visibility = Visibility.Collapsed;
            Grid_error_led_2.Visibility = Visibility.Collapsed;

            Storyboard sb_1 = Grid_error_led_1.FindResource("Storyboard_flash_error_1") as Storyboard;
            sb_1.Begin();
            Storyboard sb_2 = Grid_error_led_2.FindResource("Storyboard_flash_error_2") as Storyboard;
            sb_2.Begin();

            #endregion

            string s_panel_area = ((double)Globals.GetTheInstance().Panel_area_cm / 100).ToString();
            Label_panel_area.Content = $"{s_panel_area} M x {s_panel_area} M";

            string s_grid_area = ((double)Globals.GetTheInstance().Grid_area_cm / 100).ToString();
            Label_grid_area.Content = $"{s_grid_area} M x {s_grid_area} M";


            #region DecimalUpDown

            DecimalUpDown_uwb_tags.Value = 0;
            DecimalUpDown_uwb_tags.Minimum = 0;
            DecimalUpDown_uwb_tags.Maximum = Globals.GetTheInstance().Total_closest_tags;
            DecimalUpDown_uwb_tags.Increment = (decimal)1;

            #endregion


            #region Timers

            m_timer_read_memory = new System.Timers.Timer();
            m_timer_read_memory.Elapsed += Timer_read_memory_Tick;
            m_timer_read_memory.Interval = Globals.GetTheInstance().Read_memory_interval;
            m_timer_read_memory.Start();

            m_timer_read_event_log = new System.Timers.Timer();
            m_timer_read_event_log.Elapsed += Timer_read_event_log_Tick;
            m_timer_read_event_log.Interval = 200;
            m_timer_read_event_log.Stop();

            m_timer_read_hist_log = new System.Timers.Timer();
            m_timer_read_hist_log.Elapsed += Timer_read_hist_log_Tick;
            m_timer_read_hist_log.Interval = 200;
            m_timer_read_hist_log.Stop();

            #endregion

            #region Collections

            m_collection_sas360_tag_processed = new ObservableCollection<Sas360_tag>();
            m_collection_sas360_zone_processed = new ObservableCollection<Sas360_zone>();


            m_collection_internal_config = new ObservableCollection<Modbus_var>();
            m_collection_config_sas360con = new ObservableCollection<Modbus_var>();
            m_collection_config_iot = new ObservableCollection<Modbus_var>();
            m_collection_image_sas360con = new ObservableCollection<Modbus_var>();
            m_collection_image_iot = new ObservableCollection<Modbus_var>();

            m_collection_console_closest_tags_base = new ObservableCollection<Modbus_var>();
            m_collection_console_closest_tags_extended = new ObservableCollection<Modbus_var>();
            m_collection_uwb_closest_tags_base = new ObservableCollection<Modbus_var>();
            m_collection_uwb_closest_tags_extended = new ObservableCollection<Modbus_var>();

            m_collection_console_closest_zone_base = new ObservableCollection<Modbus_var>();
            m_collection_console_closest_zone_extended = new ObservableCollection<Modbus_var>();

            m_collection_nvreg = new ObservableCollection<Modbus_var>();

            m_collection_commands = new ObservableCollection<Modbus_command>();

            m_collection_event_log = new ObservableCollection<Event_log>();
            m_collection_hist_log = new ObservableCollection<Hist_log>();

            #endregion


            #region Memory classes

            Globals.GetTheInstance().Internal_config_sas360 = new();
            Globals.GetTheInstance().Config_sas360con_general = new()
            {
                Array_lin_used = new byte[Constants.LIN_TOTAL_COUNT],
                Array_lin_modbus_slave = new byte[Constants.LIN_TOTAL_COUNT],
                Array_actuaciones_salidas = new ushort[Enum.GetNames(typeof(ACTUACIONES_SALIDAS_POS_IN_ARRAY)).Length]
            };

            Globals.GetTheInstance().Config_sas360con_vehicle_configuration = new();
            Globals.GetTheInstance().Config_sas360con_detection_area = new();
            Globals.GetTheInstance().Config_sas360con_installation_client = new();

            Globals.GetTheInstance().Forced_mode_do_controls = new bool[Enum.GetNames<FORCE_MODE_CODIF>().Length];

            Globals.GetTheInstance().Image_sas360con_general = new()
            {
                Array_processing_task_msec = new ushort[Constants.PROCESSING_TASK_COUNT],
                Array_digital_states = new ushort[Enum.GetNames<DIGITAL_STATES_IN_LIST>().Length]
            };

            Globals.GetTheInstance().Image_sas360con_forced_mask = new()
            {
                Forced_mode = 514,
                Forced_mask_do1 = 515,
                Forced_mask_do2 = 516,
                Forced_mask_do3 = 517,
                Forced_mask_codif_led1 = 518,
                Forced_mask_codif_led2 = 519,
                Forced_mask_audio1 = 520,
                Forced_mask_audio2 = 521
            };

            Globals.GetTheInstance().Image_sas360con_lin_pooling = new()
            {
                Array_lin_com_total_counter = new ushort[Constants.LIN_TOTAL_COUNT],
                Array_lin_com_error_counter = new byte[Constants.LIN_TOTAL_COUNT],
                Array_lin_total_cycle_time = new byte[Constants.LIN_TOTAL_COUNT],
                Array_assigned_self_contag_id = new byte[Constants.ID_SIZE],
                Array_assigned_self_drvtag_id = new byte[Constants.ID_SIZE],
            };

            Globals.GetTheInstance().Image_sas360con_field_position = new();

            Globals.GetTheInstance().Image_sas360con_main_management = new()
            {
                Temp = new ushort[Enum.GetNames(typeof(TEMPORIZADOR_POS)).Length]
            };

            Globals.GetTheInstance().Image_sas360con_integrity_management = new();

            Globals.GetTheInstance().Image_sas360con_uwb = new()
            {
                UWB_codif_state = new ushort[Constants.LIN_TOTAL_COUNT],
                UWB_number_tags_detected = new byte[Constants.LIN_TOTAL_COUNT],
                UWB_number_zones_detected = new byte[Constants.LIN_TOTAL_COUNT],
            };



            Globals.GetTheInstance().Array_sas360_tag = new Sas360_tag[Globals.GetTheInstance().Total_closest_tags];
            for (int index = 0; index < Globals.GetTheInstance().Total_closest_tags; index++)
                Globals.GetTheInstance().Array_sas360_tag.SetValue(new Sas360_tag() { Index = index }, index);

            Globals.GetTheInstance().Array_sas360_zone = new Sas360_zone[Globals.GetTheInstance().Total_closest_zone];
            for (int index = 0; index < Globals.GetTheInstance().Total_closest_zone; index++)
                Globals.GetTheInstance().Array_sas360_zone.SetValue(new Sas360_zone() { Index = index }, index);

            Globals.GetTheInstance().Array_sas360_uwb = new Sas360_uwb[Constants.LIN_TOTAL_COUNT];
            for (int index = 0; index < Constants.LIN_TOTAL_COUNT; index++)
                Globals.GetTheInstance().Array_sas360_uwb.SetValue(new Sas360_uwb(), index);

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

            Refresh_lists();
        }

        #endregion


        #region Position SAS 360 controls

        private void Draw_map_controls()
        {
            double rectangle_width = Rectangle_sas360_data_draw.ActualWidth;
            double rectangle_height = Rectangle_sas360_data_draw.ActualHeight;

            Image_sas360_con.Visibility = Visibility.Hidden;

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
                ellipse_tag.Width = 10;
                ellipse_tag.Height = 10;
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
                Border border_rele1 = new()
                {
                    Width = 15,
                    Height = 10,
                    BorderThickness = new Thickness(1, 1, 2, 2),
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    CornerRadius = new CornerRadius(3),
                    Background = new SolidColorBrush(Colors.White),
                    Margin = new Thickness(0, 0, 10, 0),
                    Cursor = Cursors.Hand
                };
                border_rele1.MouseDown += new MouseButtonEventHandler(Border_rele2_MouseDown);

                m_list_border_sas360_config_rele1.Add(border_rele1);
                Wrappanel_output_actions_rele1.Children.Add(border_rele1);


                Border border_rele2 = new()
                {
                    Width = 15,
                    Height = 10,
                    BorderThickness = new Thickness(1, 1, 2, 2),
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    CornerRadius = new CornerRadius(3),
                    Background = new SolidColorBrush(Colors.White),
                    Margin = new Thickness(0, 0, 10, 0),
                    Cursor = Cursors.Hand
                };
                border_rele2.MouseDown += new MouseButtonEventHandler(Border_rele2_MouseDown);

                m_list_border_sas360_config_rele2.Add(border_rele2);
                Wrappanel_output_actions_rele2.Children.Add(border_rele2);


                Border border_rele3 = new()
                {
                    Width = 15,
                    Height = 10,
                    BorderThickness = new Thickness(1, 1, 2, 2),
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    CornerRadius = new CornerRadius(3),
                    Background = new SolidColorBrush(Colors.White),
                    Margin = new Thickness(0, 0, 10, 0),
                    Cursor = Cursors.Hand
                };
                border_rele3.MouseDown += new MouseButtonEventHandler(Border_rele3_MouseDown);

                m_list_border_sas360_config_rele3.Add(border_rele3);
                Wrappanel_output_actions_rele3.Children.Add(border_rele3);


                Border border_rele4 = new()
                {
                    Width = 15,
                    Height = 10,
                    BorderThickness = new Thickness(1, 1, 2, 2),
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    CornerRadius = new CornerRadius(3),
                    Background = new SolidColorBrush(Colors.White),
                    Margin = new Thickness(0, 0, 10, 0),
                    Cursor = Cursors.Hand
                };
                border_rele4.MouseDown += new MouseButtonEventHandler(Border_rele4_MouseDown);

                m_list_border_sas360_config_rele4.Add(border_rele4);
                Wrappanel_output_actions_rele4.Children.Add(border_rele4);


                Border border_trans1 = new()
                {
                    Width = 15,
                    Height = 10,
                    BorderThickness = new Thickness(1, 1, 2, 2),
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    CornerRadius = new CornerRadius(3),
                    Background = new SolidColorBrush(Colors.White),
                    Margin = new Thickness(0, 0, 10, 0),
                    Cursor = Cursors.Hand
                };
                border_trans1.MouseDown += new MouseButtonEventHandler(Border_trans1_MouseDown);

                m_list_border_sas360_config_trans1.Add(border_trans1);
                Wrappanel_output_actions_trans1.Children.Add(border_trans1);


                Border border_trans2 = new()
                {
                    Width = 15,
                    Height = 10,
                    BorderThickness = new Thickness(1, 1, 2, 2),
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    CornerRadius = new CornerRadius(3),
                    Background = new SolidColorBrush(Colors.White),
                    Margin = new Thickness(0, 0, 10, 0),
                    Cursor = Cursors.Hand
                };
                border_trans2.MouseDown += new MouseButtonEventHandler(Border_trans2_MouseDown);

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


        #region Disconnect controls

        private void Disconnect_controls()
        {
            m_list_ellipse_sas360tag.ForEach(sas360tag => sas360tag.Visibility = Visibility.Collapsed);
            m_list_grid_sas360zone.ForEach(sas360zone => sas360zone.Visibility = Visibility.Collapsed);

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






        #region Mover pantalla

        private void Title_MouseDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (e.ButtonState == MouseButtonState.Pressed)
                DragMove();
        }

        #endregion


        #region Modbus events to main

        public void RTU_events_to_main(object sender, RTU_handler_args args)
        {
            switch (args.RTU_action)
            {
                case RTU_ACTION.CONNECT:
                    {
                        Dispatcher.Invoke(() => ((Storyboard)Resources["BlinkConnectStoryboard"]).Begin());
                        Dispatcher.Invoke(() => Image_stop.Visibility = Visibility.Collapsed);
                        Dispatcher.Invoke(() => Image_start.Visibility = Visibility.Visible);

                        Dispatcher.Invoke(() => Read_memory(MEMORY_CONFIG_TYPE.INTERNAL_CONFIG));
                        Dispatcher.Invoke(() => Read_memory(MEMORY_CONFIG_TYPE.CONFIG_SAS360CON));
                        Dispatcher.Invoke(() => Read_memory(MEMORY_CONFIG_TYPE.CONFIG_IOT));

                        break;
                    }

                case RTU_ACTION.ERROR_CONNECT:
                    {
                        Dispatcher.Invoke(() => Image_stop.Visibility = Visibility.Visible);
                        Dispatcher.Invoke(() => Image_start.Visibility = Visibility.Collapsed);

                        break;
                    }

                case RTU_ACTION.DISCONNECT:
                    {
                        Dispatcher.Invoke(() => ((Storyboard)Resources["BlinkConnectStoryboard"]).Remove());
                        Dispatcher.Invoke(() => Image_stop.Visibility = Visibility.Visible);
                        Dispatcher.Invoke(() => Image_start.Visibility = Visibility.Collapsed);
                        Dispatcher.Invoke(() => Disconnect_controls());

                        break;
                    }

                case RTU_ACTION.READ:
                    {
                        break;
                    }
            }
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

            Globals.GetTheInstance().Array_sas360_tag = new Sas360_tag[Globals.GetTheInstance().Total_closest_tags];
            for (int index = 0; index < Globals.GetTheInstance().Total_closest_tags; index++)
                Globals.GetTheInstance().Array_sas360_tag.SetValue(new Sas360_tag(), index);

            Globals.GetTheInstance().Array_sas360_zone = new Sas360_zone[Globals.GetTheInstance().Total_closest_zone];
            for (int index = 0; index < Globals.GetTheInstance().Total_closest_zone; index++)
                Globals.GetTheInstance().Array_sas360_zone.SetValue(new Sas360_zone(), index);


            //Communication
            Globals.GetTheInstance().ManageComThread.Unit_id = Globals.GetTheInstance().Unit_id;
            Globals.GetTheInstance().ManageComThread.Comm_port = Globals.GetTheInstance().Comm_port;
            Globals.GetTheInstance().ManageComThread.Baud_rate = Globals.GetTheInstance().Baud_rate;
            m_timer_read_memory.Interval = Globals.GetTheInstance().Read_memory_interval;
        }

        #endregion



        #region Tab menu selection changed
        private void Tab_menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        #endregion


        #region Start / stop

        private void Image_stop_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Globals.GetTheInstance().ManageComThread.Connect();
        }

        private void Image_start_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Globals.GetTheInstance().ManageComThread.Disconnect();
        }

        #endregion



        #region Timer read image


        private void Timer_read_memory_Tick(object sender, EventArgs e)
        {
            m_timer_read_memory.Stop();
            try
            {
                if (Globals.GetTheInstance().ManageComThread.Is_connected)
                {
                    MEMORY_CONFIG_TYPE memory_config_type =
                        m_memory_read_state == MEMORY_READ_STATE.IMAGE_SAS360CON ? MEMORY_CONFIG_TYPE.IMAGE_SAS360CON :
                        m_memory_read_state == MEMORY_READ_STATE.CONSOLE_CLOSEST_TAGS_BASE ? MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE :
                        m_memory_read_state == MEMORY_READ_STATE.UWB_CLOSEST_TAGS_BASE ? MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_BASE :
                        m_memory_read_state == MEMORY_READ_STATE.CONSOLE_CLOSEST_ZONE_BASE ? MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_BASE :
                        m_memory_read_state == MEMORY_READ_STATE.NVREG ? MEMORY_CONFIG_TYPE.NVREG :
                        MEMORY_CONFIG_TYPE.IMAGE_SAS360CON;

                    Dispatcher.Invoke(() => Read_memory(memory_config_type));

                    m_memory_read_state =
                        m_memory_read_state == MEMORY_READ_STATE.IMAGE_SAS360CON ? MEMORY_READ_STATE.CONSOLE_CLOSEST_TAGS_BASE :
                        m_memory_read_state == MEMORY_READ_STATE.CONSOLE_CLOSEST_TAGS_BASE ? MEMORY_READ_STATE.UWB_CLOSEST_TAGS_BASE :
                        m_memory_read_state == MEMORY_READ_STATE.UWB_CLOSEST_TAGS_BASE ? MEMORY_READ_STATE.CONSOLE_CLOSEST_ZONE_BASE :
                        m_memory_read_state == MEMORY_READ_STATE.CONSOLE_CLOSEST_ZONE_BASE ? MEMORY_READ_STATE.NVREG :
                        m_memory_read_state == MEMORY_READ_STATE.NVREG ? MEMORY_READ_STATE.IMAGE_SAS360CON :
                        MEMORY_READ_STATE.IMAGE_SAS360CON;
                }
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{GetType().Name} -> {nameof(Timer_read_memory_Tick)} -> {ex.Message}");
            }

            m_timer_read_memory.Start();
        }

        #endregion


        #region STATE TAB

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
                if (Listview_tag_processed.SelectedItem is Sas360_tag)
                {
                    m_selected_tag = Listview_tag_processed.SelectedItem as Sas360_tag;
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
                int selected_index = Globals.GetTheInstance().Array_sas360_tag.ToList().FindIndex(tag => tag.ID_2LSB == m_selected_tag.ID_2LSB);

                Listview_tag_processed.SelectedIndex = selected_index; //List

                //Map
                m_list_ellipse_sas360tag
                    .Select((value, index) => (value, index)).ToList()
                    .ForEach(ellipse =>
                    {
                        ellipse.value.StrokeThickness = ellipse.index != selected_index ? 1 : 8;
                    });
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
                if (Listview_zone_processed.SelectedItem is Sas360_zone)
                {
                    m_selected_zone = Listview_zone_processed.SelectedItem as Sas360_zone;
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

        #region Refresh tag controls

        private void Refresh_zone_controls()
        {
            if (m_selected_zone != null)
            {
                int selected_index = Globals.GetTheInstance().Array_sas360_zone.ToList().FindIndex(zone => zone.ID_2LSB == m_selected_zone.ID_2LSB);

                Listview_zone_processed.SelectedIndex = selected_index; //List

                //Map
                m_list_ellipse_int_sas360zone
                    .Select((value, index) => (value, index)).ToList()
                    .ForEach(ellipse =>
                    {
                        ellipse.value.StrokeThickness = ellipse.index != selected_index ? 1 : 8;
                    });
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
                Sas360_tag sas360_tag = Globals.GetTheInstance().Array_sas360_tag[index_sas360tag];
                string ID = sas360_tag.ID_2LSB.ToString();
                string tag_type = Manage_memory.SAS360TAG_ZONE_type(sas360_tag.Tag_type);

                string pos_x = sas360_tag.Pos_x / (double)100 + "M";
                string pos_y = sas360_tag.Pos_y / (double)100 + "M";

                Label_popup_id.Content = $"ID : {ID}";
                Label_popup_type.Content = tag_type;

                Label_popup_tag_zone.Content = "TAG";
                bool b_tag_ok = (sas360_tag.Codif_state & 0x01) == 0x01;
                Image_popup_tag_ok.Visibility = b_tag_ok ? Visibility.Visible : Visibility.Collapsed;
                Image_popup_tag_error.Visibility = !b_tag_ok ? Visibility.Visible : Visibility.Collapsed;

                Wrappanel_popup_battery_state.Visibility = Visibility.Visible;
                bool b_battery_ok = (sas360_tag.Codif_state & 0x04) == 0x04;
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
            m_selected_tag = Globals.GetTheInstance().Array_sas360_tag[index_sas360tag];

            Refresh_tag_controls();
        }

        #endregion


        #region Ellipse associated with SAS360 ZONE Mouse events

        private void EllipseZoneOnMouseEnter(object sender, MouseEventArgs e)
        {
            int index_sas360zone = Array.IndexOf(m_list_ellipse_int_sas360zone.ToArray(), sender);
            if (index_sas360zone != Constants.index_no_selected)
            {
                Sas360_zone sas360_zone = Globals.GetTheInstance().Array_sas360_zone[index_sas360zone];
                string ID = sas360_zone.ID_2LSB.ToString();
                string zone_type = Manage_memory.SAS360TAG_ZONE_type(sas360_zone.Zone_type);

                string pos_x = sas360_zone.Pos_x / (double)100 + "M";
                string pos_y = sas360_zone.Pos_y / (double)100 + "M";

                Label_popup_id.Content = $"ID : {ID}";
                Label_popup_type.Content = zone_type;

                Label_popup_tag_zone.Content = "ZONE";
                bool b_zone_ok = (sas360_zone.Codif_state & 0x01) == 0x01;
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
            m_selected_zone = Globals.GetTheInstance().Array_sas360_zone[index_sas360zone];

            Refresh_zone_controls();

        }

        #endregion


        #region Label info mouse events
        private void LabelInfoOnMouseEnter(object sender, MouseEventArgs e)
        {

            int index = Array.IndexOf(m_list_label_popup_image_info.ToArray(), sender);

            if (index == (int)LABEL_POPUP_IMAGE_READ.INPUT && Globals.GetTheInstance().Image_sas360con_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.INPUT] != 0x00)
            {
                Textblock_image_info.Text = Manage_memory.SAS360CON_input();
                Popup_image_info.IsOpen = true;
            }

            if (index == (int)LABEL_POPUP_IMAGE_READ.OUTPUT_INT && Globals.GetTheInstance().Image_sas360con_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.OUTPUT_1] != 0x00)
            {
                Textblock_image_info.Text = Manage_memory.SAS360CON_output_ext();
                Popup_image_info.IsOpen = true;
            }

            if (index == (int)LABEL_POPUP_IMAGE_READ.OUTPUT_EXT && Globals.GetTheInstance().Image_sas360con_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.OUTPUT_2] != 0x00)
            {
                Textblock_image_info.Text = Manage_memory.SAS360CON_output_ext();
                Popup_image_info.IsOpen = true;
            }

            if (index == (int)LABEL_POPUP_IMAGE_READ.OUTPUT_LED && Globals.GetTheInstance().Image_sas360con_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.OUTPUT_3] != 0x00)
            {
                Textblock_image_info.Text = Manage_memory.SAS360CON_output_led();
                Popup_image_info.IsOpen = true;
            }


            if (index == (int)LABEL_POPUP_IMAGE_READ.INTERNAL_ERROR && Globals.GetTheInstance().Image_sas360con_main_management.Internal_error != 0x00)
            {
                Textblock_image_info.Text = Manage_memory.SAS360CON_internal_error();
                Popup_image_info.IsOpen = true;
            }
            else if (index == (int)LABEL_POPUP_IMAGE_READ.LIN_POOLING_CONFIG && Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_config_codif != 0x00)
            {
                Textblock_image_info.Text = Manage_memory.SAS360CON_lin_pooling_config();
                Popup_image_info.IsOpen = true;
            }
            else if (index == (int)LABEL_POPUP_IMAGE_READ.LIN_POOLING_STATE && Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_state != 0x00)
            {
                Textblock_image_info.Text = Manage_memory.SAS360CON_lin_pooling_state();
                Popup_image_info.IsOpen = true;
            }
        }

        private void LabelInfoOnMouseLeave(object sender, MouseEventArgs e)
        {
            Popup_image_info.IsOpen = false;
        }

        #endregion

        #endregion


        #region MEMORY TAB

        #region Generate csv
        private void Button_generate_csv_Click(object sender, RoutedEventArgs e)
        {
            List<Modbus_var> list_modbus_var = Manage_memory.Generate_csv_data(Textbox_generate_csv.Text);
            try
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Convert data to CSV format?", "INFO", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly);
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

                    System.Windows.MessageBox.Show("CSV conversion finished", "INFO", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
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


        #region Read memory

        private void Read_memory(MEMORY_CONFIG_TYPE memory_config_type)
        {
            double rectangle_width = Rectangle_sas360_data_draw.ActualWidth;
            double rectangle_height = Rectangle_sas360_data_draw.ActualHeight;

            //Simulate data
            if (Globals.GetTheInstance().Simulator_mode == BIT_STATE.ON)
            {
                Manage_memory.Simulator_data(memory_config_type);

                switch (memory_config_type)
                {
                    case MEMORY_CONFIG_TYPE.INTERNAL_CONFIG:
                        {
                            CollectionViewSource.GetDefaultView(Listview_internal_config.ItemsSource).Refresh();

                            Label_config_serial_number_value.Content = Globals.GetTheInstance().Internal_config_sas360.Serial_number;
                            Label_config_id_2lsb_sas360con_value.Content = Globals.GetTheInstance().Internal_config_sas360.ID_manufacturing;
                            Label_config_id_tag_sas360con_value.Content = Manage_memory.SAS360TAG_ZONE_type(Globals.GetTheInstance().Internal_config_sas360.Tag_type);

                            Label_config_version_hw.Content = Globals.GetTheInstance().Internal_config_sas360.Version_hw;
                            Label_main_version_hw.Content = Globals.GetTheInstance().Internal_config_sas360.Version_hw;

                            Label_config_version_fw.Content = Globals.GetTheInstance().Internal_config_sas360.Version_fw;
                            Label_main_version_fw.Content = Globals.GetTheInstance().Internal_config_sas360.Version_fw;

                            Label_config_version_boot.Content = Globals.GetTheInstance().Internal_config_sas360.Version_boot;
                            Label_main_version_boot.Content = Globals.GetTheInstance().Internal_config_sas360.Version_fw;

                            Label_config_rtu_slave_speed_value.Content = Globals.GetTheInstance().Internal_config_sas360.RTU_slave_speed;
                            Label_config_rtu_slave_num_value.Content = Globals.GetTheInstance().Internal_config_sas360.RTU_slave_num;
                            Label_config_modbus_lin_master_speed_value.Content = Globals.GetTheInstance().Internal_config_sas360.Lin_master_speed;
                            Label_config_fw_update.Content = Globals.GetTheInstance().Internal_config_sas360.RTC_fw_update;
                            Label_config_last_cfg_change.Content = Globals.GetTheInstance().Internal_config_sas360.RTC_config_update;
                            Label_config_internal_cfg_change_counter.Content = Globals.GetTheInstance().Internal_config_sas360.CFG_change_counter;
                            Label_config_crc_config_interna.Content = $"0X{Globals.GetTheInstance().Internal_config_sas360.CRC_config.ToString("X4")}";

                            break;
                        }

                    case MEMORY_CONFIG_TYPE.CONFIG_SAS360CON:
                        {
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
                            CollectionViewSource.GetDefaultView(Listview_config_sas360con.ItemsSource).Refresh();

                            #region Installation / client definition

                            Label_main_installation_code.Content = Globals.GetTheInstance().Config_sas360con_installation_client.Installation;
                            Label_config_installation_code.Content = Globals.GetTheInstance().Config_sas360con_installation_client.Installation;

                            Label_main_client_code.Content = Globals.GetTheInstance().Config_sas360con_installation_client.Client;
                            Label_config_client_code.Content = Globals.GetTheInstance().Config_sas360con_installation_client.Client;

                            Label_main_console_code.Content = Globals.GetTheInstance().Config_sas360con_installation_client.Client;
                            Label_config_console_code.Content = Globals.GetTheInstance().Config_sas360con_installation_client.Consola;

                            Label_config_audio.Content =
                                Globals.GetTheInstance().Config_sas360con_installation_client.Audio == (byte)AUDIO.SPANISH ? "SPANISH" :
                                Globals.GetTheInstance().Config_sas360con_installation_client.Audio == (byte)AUDIO.ENGLISH ? "ENGLISH" : "NOT DEFINED";

                            #endregion


                            #region Label positioning info

                            Label_area_deteccion.Content = $"{Globals.GetTheInstance().Config_sas360con_detection_area.Area_detection_radio / (double)100} M";

                            Label_area_yellow.Content =
                                $"{(Globals.GetTheInstance().Config_sas360con_detection_area.Array_yellow_detection_distances[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT] / (double)100) + (Globals.GetTheInstance().Config_sas360con_detection_area.Array_yellow_detection_distances[(int)DETECTION_AREA_POS_IN_ARRAY.RIGHT] / (double)100)} M X " +
                                $"{(Globals.GetTheInstance().Config_sas360con_detection_area.Array_yellow_detection_distances[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT] / (double)100) + (Globals.GetTheInstance().Config_sas360con_detection_area.Array_yellow_detection_distances[(int)DETECTION_AREA_POS_IN_ARRAY.BACK] / (double)100)} M";

                            Label_area_orange.Content =
                                $"{(Globals.GetTheInstance().Config_sas360con_detection_area.Array_orange_detection_distances[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT] / (double)100) + (Globals.GetTheInstance().Config_sas360con_detection_area.Array_orange_detection_distances[(int)DETECTION_AREA_POS_IN_ARRAY.RIGHT] / (double)100)} M X " +
                                $"{(Globals.GetTheInstance().Config_sas360con_detection_area.Array_orange_detection_distances[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT] / (double)100) + (Globals.GetTheInstance().Config_sas360con_detection_area.Array_orange_detection_distances[(int)DETECTION_AREA_POS_IN_ARRAY.BACK] / (double)100)} M";


                            Label_area_red.Content =
                                $"{(Globals.GetTheInstance().Config_sas360con_detection_area.Array_red_detection_distances[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT] / (double)100) + (Globals.GetTheInstance().Config_sas360con_detection_area.Array_red_detection_distances[(int)DETECTION_AREA_POS_IN_ARRAY.RIGHT] / (double)100)} M X " +
                                $"{(Globals.GetTheInstance().Config_sas360con_detection_area.Array_red_detection_distances[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT] / (double)100) + (Globals.GetTheInstance().Config_sas360con_detection_area.Array_red_detection_distances[(int)DETECTION_AREA_POS_IN_ARRAY.BACK] / (double)100)} M";


                            Label_car_size.Content = $"{Globals.GetTheInstance().Config_sas360con_vehicle_configuration.VehicleDim_xy[0] / (double)100} M X {Globals.GetTheInstance().Config_sas360con_vehicle_configuration.VehicleDim_xy[1] / (double)100} M";

                            #endregion


                            #region Vehicle configuration

                            #region Config tab

                            Label_config_vehicle_dim.Content = $"{Globals.GetTheInstance().Config_sas360con_vehicle_configuration.VehicleDim_xy[0]} X {Globals.GetTheInstance().Config_sas360con_vehicle_configuration.VehicleDim_xy[1]}";
                            Label_config_antenna_config.Content = Globals.GetTheInstance().Config_sas360con_vehicle_configuration.Antenna_type;
                            Label_config_number_closest_tags.Content = Globals.GetTheInstance().Config_sas360con_vehicle_configuration.Num_closest_tags;

                            #endregion

                            #region Map

                            #region Vehicle

                            double vehicle_x = rectangle_width * (Globals.GetTheInstance().Config_sas360con_vehicle_configuration.VehicleDim_xy[0] / (double)Globals.GetTheInstance().Panel_area_cm);
                            double vehicle_y = rectangle_height * (Globals.GetTheInstance().Config_sas360con_vehicle_configuration.VehicleDim_xy[1] / (double)Globals.GetTheInstance().Panel_area_cm);

                            Image_sas360_con.Visibility = Visibility.Visible;
                            Image_sas360_con.Width = vehicle_x;
                            Image_sas360_con.Height = vehicle_y;

                            double left = rectangle_width / 2;
                            left -= vehicle_x / 2;

                            double top = rectangle_height / 2;
                            top -= vehicle_y / 2;

                            Canvas.SetLeft(Image_sas360_con, left);
                            Canvas.SetTop(Image_sas360_con, top);

                            #endregion

                            #region Antennas position

                            #region CONFIG TAB

                            for (int index_lin = 0; index_lin < Constants.LIN_USED_COUNT; index_lin++)
                            {
                                int[] antena_xy_cm = Functions.SliceRow(Globals.GetTheInstance().Config_sas360con_vehicle_configuration.Antenna_xy, index_lin).ToArray();
                                m_array_config_label_antenna[(int)LABEL_ANTENNA_ARRAY_POS.POS_X, index_lin].Content = antena_xy_cm[0];
                                m_array_config_label_antenna[(int)LABEL_ANTENNA_ARRAY_POS.POS_Y, index_lin].Content = antena_xy_cm[1];

                                m_array_config_label_antenna[(int)LABEL_ANTENNA_ARRAY_POS.ORIENTATION, index_lin].Content = Globals.GetTheInstance().Config_sas360con_vehicle_configuration.Antena_angle[index_lin];
                            }


                            #endregion

                            #region Map

                            m_array_rectangle_antenna
                                .Select((item, index) => new { Item = item, Position = index }).ToList()
                                .ForEach(rectangle =>
                                {
                                    #region Antennas angle

                                    double antenna_angle = Globals.GetTheInstance().Config_sas360con_vehicle_configuration.Antena_angle[rectangle.Position] / (double)100;
                                    RotateTransform rotate = new()
                                    {
                                        Angle = antenna_angle
                                    };
                                    m_array_rectangle_antenna[rectangle.Position].LayoutTransform = rotate;

                                    #endregion

                                    #region Antenna controls

                                    double antenna_width = m_array_rectangle_antenna[rectangle.Position].Width;
                                    double antenna_height = m_array_rectangle_antenna[rectangle.Position].Height;

                                    int[] antena_xy_cm = Functions.SliceRow(Globals.GetTheInstance().Config_sas360con_vehicle_configuration.Antenna_xy, rectangle.Position).ToArray();

                                    double antenna_x = rectangle_width * (Math.Abs(antena_xy_cm[0]) / (double)Globals.GetTheInstance().Panel_area_cm);
                                    antenna_x = antena_xy_cm[0] < 0 ? (antenna_x + (antenna_width / 2)) * -1 : antenna_x - (antenna_width / 2);
                                    antenna_x = (rectangle_width / 2) + antenna_x;
                                    Canvas.SetLeft(m_array_rectangle_antenna[rectangle.Position], antenna_x);

                                    double antenna_y = rectangle_width * (Math.Abs(antena_xy_cm[1]) / (double)Globals.GetTheInstance().Panel_area_cm);
                                    antenna_y = antena_xy_cm[1] > 0 ? (antenna_y + (antenna_height / 2)) * -1 : antenna_y - (antenna_height / 2);
                                    antenna_y = (rectangle_height / 2) + antenna_y;
                                    Canvas.SetTop(m_array_rectangle_antenna[rectangle.Position], antenna_y);

                                    rectangle.Item.Visibility = Visibility.Visible;

                                    #endregion

                                    Draw_config_sas360con_detection_areas();
                                });

                            #endregion

                            #endregion

                            #endregion

                            #endregion


                            #region Detection area

                            Label_config_detection_radio.Content = Globals.GetTheInstance().Config_sas360con_detection_area.Area_detection_radio;
                            Label_config_dist_to_antennas.Content = Globals.GetTheInstance().Config_sas360con_detection_area.Interior_distance_to_antennas;

                            m_list_label_config_yellow_area[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT].Content = Globals.GetTheInstance().Config_sas360con_detection_area.Array_yellow_detection_distances[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT];
                            m_list_label_config_yellow_area[(int)DETECTION_AREA_POS_IN_ARRAY.RIGHT].Content = Globals.GetTheInstance().Config_sas360con_detection_area.Array_yellow_detection_distances[(int)DETECTION_AREA_POS_IN_ARRAY.RIGHT];
                            m_list_label_config_yellow_area[(int)DETECTION_AREA_POS_IN_ARRAY.BACK].Content = Globals.GetTheInstance().Config_sas360con_detection_area.Array_yellow_detection_distances[(int)DETECTION_AREA_POS_IN_ARRAY.BACK];
                            m_list_label_config_yellow_area[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT].Content = Globals.GetTheInstance().Config_sas360con_detection_area.Array_yellow_detection_distances[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT];

                            m_list_label_config_orange_area[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT].Content = Globals.GetTheInstance().Config_sas360con_detection_area.Array_orange_detection_distances[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT];
                            m_list_label_config_orange_area[(int)DETECTION_AREA_POS_IN_ARRAY.RIGHT].Content = Globals.GetTheInstance().Config_sas360con_detection_area.Array_orange_detection_distances[(int)DETECTION_AREA_POS_IN_ARRAY.RIGHT];
                            m_list_label_config_orange_area[(int)DETECTION_AREA_POS_IN_ARRAY.BACK].Content = Globals.GetTheInstance().Config_sas360con_detection_area.Array_orange_detection_distances[(int)DETECTION_AREA_POS_IN_ARRAY.BACK];
                            m_list_label_config_orange_area[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT].Content = Globals.GetTheInstance().Config_sas360con_detection_area.Array_orange_detection_distances[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT];

                            m_list_label_config_red_area[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT].Content = Globals.GetTheInstance().Config_sas360con_detection_area.Array_red_detection_distances[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT];
                            m_list_label_config_red_area[(int)DETECTION_AREA_POS_IN_ARRAY.RIGHT].Content = Globals.GetTheInstance().Config_sas360con_detection_area.Array_red_detection_distances[(int)DETECTION_AREA_POS_IN_ARRAY.RIGHT];
                            m_list_label_config_red_area[(int)DETECTION_AREA_POS_IN_ARRAY.BACK].Content = Globals.GetTheInstance().Config_sas360con_detection_area.Array_red_detection_distances[(int)DETECTION_AREA_POS_IN_ARRAY.BACK];
                            m_list_label_config_red_area[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT].Content = Globals.GetTheInstance().Config_sas360con_detection_area.Array_red_detection_distances[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT];

                            m_list_label_config_hyst_out_area[(int)HYSTERESYS_POS_IN_ARRAY.AREA_D].Content = Globals.GetTheInstance().Config_sas360con_detection_area.Array_hysteresys[(int)HYSTERESYS_POS_IN_ARRAY.AREA_D];
                            m_list_label_config_hyst_out_area[(int)HYSTERESYS_POS_IN_ARRAY.AREA_A].Content = Globals.GetTheInstance().Config_sas360con_detection_area.Array_hysteresys[(int)HYSTERESYS_POS_IN_ARRAY.AREA_A];
                            m_list_label_config_hyst_out_area[(int)HYSTERESYS_POS_IN_ARRAY.AREA_N].Content = Globals.GetTheInstance().Config_sas360con_detection_area.Array_hysteresys[(int)HYSTERESYS_POS_IN_ARRAY.AREA_N];
                            m_list_label_config_hyst_out_area[(int)HYSTERESYS_POS_IN_ARRAY.AREA_R].Content = Globals.GetTheInstance().Config_sas360con_detection_area.Array_hysteresys[(int)HYSTERESYS_POS_IN_ARRAY.AREA_R];
                            m_list_label_config_hyst_out_area[(int)HYSTERESYS_POS_IN_ARRAY.SECTOR_CHANGE_ANGLE].Content = Globals.GetTheInstance().Config_sas360con_detection_area.Array_hysteresys[(int)HYSTERESYS_POS_IN_ARRAY.SECTOR_CHANGE_ANGLE];
                            m_list_label_config_hyst_out_area[(int)HYSTERESYS_POS_IN_ARRAY.CLOSEST_ANTENNA_CHANGE].Content = Globals.GetTheInstance().Config_sas360con_detection_area.Array_hysteresys[(int)HYSTERESYS_POS_IN_ARRAY.CLOSEST_ANTENNA_CHANGE];
                            m_list_label_config_hyst_out_area[(int)HYSTERESYS_POS_IN_ARRAY.CLOSEST_5C_CHANGE].Content = Globals.GetTheInstance().Config_sas360con_detection_area.Array_hysteresys[(int)HYSTERESYS_POS_IN_ARRAY.CLOSEST_5C_CHANGE];

                            #endregion

                            #region Actuaciones salidas

                            ushort rele1_value = Globals.GetTheInstance().Config_sas360con_general.Array_actuaciones_salidas[(int)ACTUACIONES_SALIDAS_POS_IN_ARRAY.RELE_1];
                            ushort rele2_value = Globals.GetTheInstance().Config_sas360con_general.Array_actuaciones_salidas[(int)ACTUACIONES_SALIDAS_POS_IN_ARRAY.RELE_2];
                            ushort rele3_value = Globals.GetTheInstance().Config_sas360con_general.Array_actuaciones_salidas[(int)ACTUACIONES_SALIDAS_POS_IN_ARRAY.RELE_3];
                            ushort rele4_value = Globals.GetTheInstance().Config_sas360con_general.Array_actuaciones_salidas[(int)ACTUACIONES_SALIDAS_POS_IN_ARRAY.RELE_4];
                            ushort trans1_value = Globals.GetTheInstance().Config_sas360con_general.Array_actuaciones_salidas[(int)ACTUACIONES_SALIDAS_POS_IN_ARRAY.TRANS_1];
                            ushort trans2_value = Globals.GetTheInstance().Config_sas360con_general.Array_actuaciones_salidas[(int)ACTUACIONES_SALIDAS_POS_IN_ARRAY.TRANS_2];

                            Label_config_output_actions_rele1.Content = $"0X{rele1_value.ToString("X4")}";
                            Label_config_output_actions_rele2.Content = $"0X{rele2_value.ToString("X4")}";
                            Label_config_output_actions_rele3.Content = $"0X{rele3_value.ToString("X4")}";
                            Label_config_output_actions_rele4.Content = $"0X{rele4_value.ToString("X4")}";
                            Label_config_output_actions_trans1.Content = $"0X{trans1_value.ToString("X4")}";
                            Label_config_output_actions_trans2.Content = $"0X{trans2_value.ToString("X4")}";

                            for (int bit_pos = 0; bit_pos < Constants.MAX_BITS_USHORT_VALUE; bit_pos++)
                            {
                                m_list_border_sas360_config_rele1[bit_pos].Background = Functions.IsBitSetTo1(rele1_value, bit_pos) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                                m_list_border_sas360_config_rele2[bit_pos].Background = Functions.IsBitSetTo1(rele2_value, bit_pos) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                                m_list_border_sas360_config_rele3[bit_pos].Background = Functions.IsBitSetTo1(rele3_value, bit_pos) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                                m_list_border_sas360_config_rele4[bit_pos].Background = Functions.IsBitSetTo1(rele4_value, bit_pos) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                                m_list_border_sas360_config_trans1[bit_pos].Background = Functions.IsBitSetTo1(trans1_value, bit_pos) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                                m_list_border_sas360_config_trans2[bit_pos].Background = Functions.IsBitSetTo1(trans2_value, bit_pos) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                            }

                            #endregion

                            #region UWB CONFIG

                            for (int index = 0; index < Constants.LIN_USED_COUNT; index++)
                            {
                                m_list_label_uwb_id[index].Content = Globals.GetTheInstance().Array_sas360_uwb[index].ID;
                                m_list_label_uwb_slave[index].Content = Globals.GetTheInstance().Array_sas360_uwb[index].Modbus_slave;


                                m_list_label_uwb_config_id_lin[index].Content = Globals.GetTheInstance().Array_sas360_uwb[index].ID;
                                m_list_label_uwb_config_id_slave[index].Content = Globals.GetTheInstance().Array_sas360_uwb[index].Modbus_slave;
                            }

                            #endregion

                            #region Calculadas config

                            Label_rtc_last_config.Content = Globals.GetTheInstance().Config_sas360con_general.RTC_last_config_change;
                            Label_config_change_counter.Content = Globals.GetTheInstance().Config_sas360con_general.RTC_last_config_change;
                            Label_crc_config.Content = Globals.GetTheInstance().Config_sas360con_general.CRC_config;

                            #endregion

                            break;
                        }

                    case MEMORY_CONFIG_TYPE.CONFIG_IOT:
                        {
                            break;
                        }


                    case MEMORY_CONFIG_TYPE.IMAGE_SAS360CON:
                        {
                            CollectionViewSource.GetDefaultView(Listview_image_sas360con.ItemsSource).Refresh();


                            //Boolean states main
                            Globals.GetTheInstance().Image_sas360con_general.Array_digital_states.ToList()
                                .Select((Value, Index) => new { Value, Position = Index }).ToList()
                                .ForEach(digital_state => m_list_label_output_states[digital_state.Position].Content = $"0X{digital_state.Value.ToString("X4")}");


                            #region boolean states maintenance

                            Label_main_state.Content = Manage_memory.SAS360CON_state_codif(Globals.GetTheInstance().Image_sas360con_general.Sas360_state);
                            Label_main_substate.Content = Globals.GetTheInstance().Image_sas360con_general.Sas360_subtate;

                            #region Digital inputs

                            ushort digital_input_value = Globals.GetTheInstance().Image_sas360con_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.INPUT];
                            Label_digital_input_value.Content = $"0x{digital_input_value:X4}";
                            int index_control = 0;
                            Enum.GetValues(typeof(MASK_CODIF_DI1)).Cast<MASK_CODIF_DI1>().ToList().ForEach(digital_input =>
                            {
                                m_list_maintenance_border_di[index_control++].Background = Functions.IsBitSetTo1(digital_input_value, (int)digital_input) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                            });

                            #endregion

                            #region Digital outputs

                            //Digital outputs 1
                            ushort digital_output_1_value = Globals.GetTheInstance().Image_sas360con_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.OUTPUT_1];
                            Label_digital_output_1_value.Content = $"0x{digital_output_1_value:X4}";
                            index_control = 0;
                            Enum.GetValues(typeof(FORCE_MASK_DO1)).Cast<FORCE_MASK_DO1>().ToList().ForEach(digital_output =>
                            {
                                m_list_maintenance_border_do_1[index_control++].Background = Functions.IsBitSetTo1(digital_output_1_value, (int)digital_output) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                            });

                            //Digital outputs 2
                            ushort digital_output_2_value = Globals.GetTheInstance().Image_sas360con_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.OUTPUT_2];
                            Label_digital_output_2_value.Content = $"0x{digital_output_2_value:X4}";
                            index_control = 0;
                            Enum.GetValues(typeof(FORCE_MASK_DO2)).Cast<FORCE_MASK_DO2>().ToList().ForEach(digital_output =>
                            {
                                m_list_maintenance_border_do_2[index_control++].Background = Functions.IsBitSetTo1(digital_output_2_value, (int)digital_output) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                            });

                            //Digital outputs 3
                            ushort digital_output_3_value = Globals.GetTheInstance().Image_sas360con_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.OUTPUT_3];
                            Label_digital_output_3_value.Content = $"0x{digital_output_3_value:X4}";
                            index_control = 0;
                            Enum.GetValues(typeof(FORCE_MASK_DO3)).Cast<FORCE_MASK_DO3>().ToList().ForEach(digital_output =>
                            {
                                m_list_maintenance_border_do_3[index_control++].Background = Functions.IsBitSetTo1(digital_output_3_value, (int)digital_output) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                            });

                            #endregion

                            #region Leds

                            ushort digital_led_1_value = Globals.GetTheInstance().Image_sas360con_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.LED_1];
                            Label_digital_led_1_value.Content = $"0x{digital_led_1_value:X4}";
                            ushort digital_led_2_value = Globals.GetTheInstance().Image_sas360con_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.LED_2];
                            Label_digital_led_2_value.Content = $"0x{digital_led_2_value:X4}";

                            const int max_leds = 28;
                            const int max_pos_value = 16;

                            #region Leds de control

                            byte[] array_byte_leds_1 = BitConverter.GetBytes(digital_led_1_value);
                            byte[] array_byte_leds_2 = BitConverter.GetBytes(digital_led_2_value);
                            IEnumerable<byte> array_byte_leds = array_byte_leds_1.Concat(array_byte_leds_2);
                            int leds_value = BitConverter.ToInt32(array_byte_leds.ToArray());

                            Grid_error_led_1.Visibility = Functions.IsBitSetTo1(leds_value, (int)CONTROL_LEDS_POS.ERROR) ? Visibility.Visible : Visibility.Collapsed;
                            Grid_error_led_2.Visibility = Functions.IsBitSetTo1(leds_value, (int)CONTROL_LEDS_POS.ERROR) ? Visibility.Visible : Visibility.Collapsed;
                            Ellipse_peaton_map.Fill = Functions.IsBitSetTo1(leds_value, (int)CONTROL_LEDS_POS.PEATON) ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.White);
                            Ellipse_vehiculo_ligero.Fill = Functions.IsBitSetTo1(leds_value, (int)CONTROL_LEDS_POS.VEHICULO_LIGERO) ? new SolidColorBrush(Colors.Orange) : new SolidColorBrush(Colors.White); ;
                            Ellipse_vehiculo_pesado.Fill = Functions.IsBitSetTo1(leds_value, (int)CONTROL_LEDS_POS.VEHICULO_PESADO) ? new SolidColorBrush(Color.FromArgb(0xff,0x83, 0xe0, 0xff)) : new SolidColorBrush(Colors.White);
                            Ellipse_slow.Fill = Functions.IsBitSetTo1(leds_value, (int)CONTROL_LEDS_POS.SLOW) ? new SolidColorBrush(Colors.Orange) : new SolidColorBrush(Colors.White);
                            Ellipse_on.Fill = Functions.IsBitSetTo1(leds_value, (int)CONTROL_LEDS_POS.ON) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                            Ellipse_driver.Fill = Functions.IsBitSetTo1(leds_value, (int)CONTROL_LEDS_POS.DRIVER) ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.White);

                            #endregion




                            for (int index_led_control = 0; index_led_control < max_leds; index_led_control++)
                            {
                                m_list_maintenance_border_led[index_led_control].Background = Functions.IsBitSetTo1(leds_value, index_led_control) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);

                                if (m_array_led_detection[index_led_control] != null)
                                    m_array_led_detection[index_led_control].Visibility = Functions.IsBitSetTo1(leds_value, index_led_control) ? Visibility.Visible : Visibility.Collapsed;
                            }

                            #endregion


                            #region Audio

                            ushort digital_audio_1_value = Globals.GetTheInstance().Image_sas360con_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.AUDIO_1];
                            Label_digital_audio_1_value.Content = $"0x{digital_audio_1_value:X4}";
                            ushort digital_audio_2_value = Globals.GetTheInstance().Image_sas360con_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.AUDIO_2];
                            Label_digital_audio_2_value.Content = $"0x{digital_audio_2_value:X4}";

                            const int max_audio = 32;
                            for (int index_audio_control = 0; index_audio_control < max_leds; index_audio_control++)
                            {
                                if (index_audio_control < max_pos_value)
                                    m_list_maintenance_border_audio[index_audio_control].Background = Functions.IsBitSetTo1(digital_audio_1_value, index_audio_control) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);

                                else
                                    m_list_maintenance_border_audio[index_audio_control].Background = Functions.IsBitSetTo1(digital_audio_2_value, index_audio_control - max_pos_value) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
                            }

                            #endregion

                            #endregion

                            #region Processing task

                            m_list_label_processing_task
                                .Select((Value, Index) => new { Control = Value, Pos = Index }).ToList()
                                .ForEach(label_processing => label_processing.Control.Content = Math.Round((Globals.GetTheInstance().Image_sas360con_general.Array_processing_task_msec[label_processing.Pos] / (double)1000), 2));

                            #endregion

                            #region Integrity & management

                            Label_main_lms_watchdog.Content = Globals.GetTheInstance().Image_sas360con_integrity_management.Lms_watchdog;
                            Label_main_last_event_id.Content = Globals.GetTheInstance().Image_sas360con_integrity_management.Last_event_id;

                            #endregion

                            #region Main management

                            Label_main_error_code.Content = $"0X{Globals.GetTheInstance().Image_sas360con_main_management.Internal_error:X}";
                            Label_main_pooling_config.Content = $"0X{Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_config_codif:X}";
                            Label_main_pooling_state.Content = $"0X{Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_state:X}";
                            Label_main_pooling_time.Content = Globals.GetTheInstance().Image_sas360con_lin_pooling.Total_lin_poolin_time;

                            #endregion

                            #region Lin pooling management

                            string s_self_contag_id = string.Empty;
                            Globals.GetTheInstance().Image_sas360con_lin_pooling.Array_assigned_self_contag_id.ToList()
                                .Select((Value, Index) => new { Value, Index }).ToList()
                                .ForEach(contag_id =>
                                {
                                    s_self_contag_id += $"{contag_id.Value}.";
                                });
                            Label_main_assigned_self_contag_id.Content = s_self_contag_id[..^1];

                            string s_self_drv_id = string.Empty;
                            Globals.GetTheInstance().Image_sas360con_lin_pooling.Array_assigned_self_drvtag_id.ToList()
                                .Select((Value, Index) => new { Value, Index }).ToList()
                                .ForEach(contag_id =>
                                {
                                    s_self_drv_id += $"{contag_id.Value}.";
                                });
                            Label_main_assigned_self_drvtag_id.Content = s_self_drv_id[..^1];

                            #endregion

                            #region SAS360CON FIELD POS

                            Label_main_vehicle_pos_x.Content = Globals.GetTheInstance().Image_sas360con_field_position.Installation_pos_x / (double)100;
                            Label_main_vehicle_pos_y.Content = Globals.GetTheInstance().Image_sas360con_field_position.Installation_pos_y / (double)100;
                            Label_main_vehicle_lat.Content = Math.Round(Globals.GetTheInstance().Image_sas360con_field_position.Latitud, 2);
                            Label_main_vehicle_lon.Content = Math.Round(Globals.GetTheInstance().Image_sas360con_field_position.Longitud, 2);

                            #endregion

                            #region UWB

                            Label_main_total_tags_detected.Content = Globals.GetTheInstance().Image_sas360con_uwb.Total_tags_detected;
                            Label_main_area_tags_detected.Content = Globals.GetTheInstance().Image_sas360con_uwb.Total_tags_area_detection;
                            Label_main_yellow_tags_detected.Content = Globals.GetTheInstance().Image_sas360con_uwb.Total_tags_area_yellow;
                            Label_main_orange_tags_detected.Content = Globals.GetTheInstance().Image_sas360con_uwb.Total_tags_area_orange;
                            Label_main_red_tags_detected.Content = Globals.GetTheInstance().Image_sas360con_uwb.Total_tags_area_red;
                            Label_main_total_zone_detected.Content = Globals.GetTheInstance().Image_sas360con_uwb.Total_zones_detected;

                            for (int index = 0; index < Constants.LIN_USED_COUNT; index++)
                            {
                                m_list_label_uwb_lin_com[index].Content = Globals.GetTheInstance().Array_sas360_uwb[index].Lin_comm_total;
                                m_list_label_uwb_lin_error[index].Content = Globals.GetTheInstance().Array_sas360_uwb[index].Lin_error_total;
                                m_list_label_uwb_cycle_time[index].Content = Globals.GetTheInstance().Array_sas360_uwb[index].Cycle_time;
                                m_list_label_uwb_state[index].Content = Globals.GetTheInstance().Array_sas360_uwb[index].Codif_state;
                                m_list_label_uwb_num_tags[index].Content = Globals.GetTheInstance().Array_sas360_uwb[index].Num_tags;
                                m_list_label_uwb_num_zones[index].Content = Globals.GetTheInstance().Array_sas360_uwb[index].Num_zones;
                            }

                            #endregion

                            break;
                        }

                    case MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE:
                        {
                            CollectionViewSource.GetDefaultView(Listview_console_closest_tags_base.ItemsSource).Refresh();
                            CollectionViewSource.GetDefaultView(Listview_tag_processed.ItemsSource).Refresh();

                            m_collection_sas360_tag_processed.Clear();

                            Globals.GetTheInstance().Array_sas360_tag.ToList()
                                .Select((item, index) => (item, index)).ToList()
                                .ForEach(sas360tag =>
                                {
                                    m_collection_sas360_tag_processed.Add(sas360tag.item);

                                    m_list_ellipse_sas360tag[sas360tag.index].Width =
                                        sas360tag.item.Tag_type == SAS360TAG_ZONE_TYPE.SAS360TAG_PED ? 13 :
                                        sas360tag.item.Tag_type == SAS360TAG_ZONE_TYPE.SAS360TAG_DRV ? 17 :
                                        sas360tag.item.Tag_type == SAS360TAG_ZONE_TYPE.SAS360CON_TAG_LV ? 21 :
                                        sas360tag.item.Tag_type == SAS360TAG_ZONE_TYPE.SAS360CON_TAG_HV ? 25 : 10;
                                    m_list_ellipse_sas360tag[sas360tag.index].Height = m_list_ellipse_sas360tag[sas360tag.index].Width;


                                    m_list_ellipse_sas360tag[sas360tag.index].Fill =
                                        sas360tag.item.Tag_type == SAS360TAG_ZONE_TYPE.SAS360TAG_PED ? new SolidColorBrush(Color.FromArgb(255, 53, 136, 237)) :
                                        sas360tag.item.Tag_type == SAS360TAG_ZONE_TYPE.SAS360TAG_DRV ? new SolidColorBrush(Color.FromArgb(255, 28, 106, 188)) :
                                        sas360tag.item.Tag_type == SAS360TAG_ZONE_TYPE.SAS360CON_TAG_LV ? new SolidColorBrush(Color.FromArgb(255, 28, 74, 130)) :
                                        sas360tag.item.Tag_type == SAS360TAG_ZONE_TYPE.SAS360CON_TAG_HV ? new SolidColorBrush(Color.FromArgb(255, 14, 38, 67)) : new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));



                                    double tag_x = rectangle_width * (Math.Abs(sas360tag.item.Pos_x) / (double)Globals.GetTheInstance().Panel_area_cm);
                                    tag_x = sas360tag.item.Pos_x < 0 ? (rectangle_width / 2) - tag_x : (rectangle_width / 2) + tag_x;
                                    tag_x -= m_list_ellipse_sas360tag[sas360tag.index].Width / 2;
                                    Canvas.SetLeft(m_list_ellipse_sas360tag[sas360tag.index], tag_x);

                                    double tag_y = rectangle_width * (Math.Abs(sas360tag.item.Pos_y) / (double)Globals.GetTheInstance().Panel_area_cm);
                                    tag_y = sas360tag.item.Pos_y < 0 ? (rectangle_height / 2) + tag_y : (rectangle_height / 2) - tag_y;
                                    tag_y -= m_list_ellipse_sas360tag[sas360tag.index].Height / 2;
                                    Canvas.SetTop(m_list_ellipse_sas360tag[sas360tag.index], tag_y);

                                    m_list_ellipse_sas360tag[sas360tag.index].Visibility = Visibility.Visible;
                                });

                            Refresh_tag_controls();

                            break;
                        }

                    case MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_BASE:
                        {
                            CollectionViewSource.GetDefaultView(Listview_console_closest_zone_base.ItemsSource).Refresh();
                            CollectionViewSource.GetDefaultView(Listview_zone_processed.ItemsSource).Refresh();

                            m_collection_sas360_zone_processed.Clear();

                            Globals.GetTheInstance().Array_sas360_zone.ToList()
                                .Select((item, index) => (item, index)).ToList()
                                .ForEach(sas360zone =>
                                {
                                    m_collection_sas360_zone_processed.Add(sas360zone.item);


                                    m_list_ellipse_int_sas360zone[sas360zone.index].Fill =
                                        sas360zone.item.Zone_type == SAS360TAG_ZONE_TYPE.SAS360ZONE_CIRC_R_SLOW ? new SolidColorBrush(Color.FromRgb(0xff, 0x46, 0x00)) :
                                        sas360zone.item.Zone_type == SAS360TAG_ZONE_TYPE.SAS360ZONE_REC_P1_SLOW ? new SolidColorBrush(Color.FromRgb(0xff, 0x7e, 0x00)) :
                                        sas360zone.item.Zone_type == SAS360TAG_ZONE_TYPE.SAS360ZONE_REC_P2_SLOW ? new SolidColorBrush(Color.FromRgb(0xff, 0xfc, 0x00)) :
                                        sas360zone.item.Zone_type == SAS360TAG_ZONE_TYPE.SAS360ZONE_REC_P3_SLOW ? new SolidColorBrush(Color.FromRgb(0x8c, 0xff, 0x00)) :
                                        sas360zone.item.Zone_type == SAS360TAG_ZONE_TYPE.SAS360ZONE_REC_P4_SLOW ? new SolidColorBrush(Color.FromRgb(0x06, 0xff, 0x00)) :
                                        sas360zone.item.Zone_type == SAS360TAG_ZONE_TYPE.SAS360ZONE_INHIBIT_RAD ? new SolidColorBrush(Color.FromRgb(0x00, 0xff, 0xb3)) :
                                        new SolidColorBrush(Colors.White);

                                    double zone_radio = rectangle_width * ((sas360zone.item.Radio_action) / (double)Globals.GetTheInstance().Panel_area_cm);
                                    m_list_ellipse_ext_sas360zone[sas360zone.index].Height = zone_radio * 2;
                                    m_list_ellipse_ext_sas360zone[sas360zone.index].Width = zone_radio * 2;


                                    double zone_x = rectangle_width * (Math.Abs(sas360zone.item.Pos_x) / (double)Globals.GetTheInstance().Panel_area_cm);
                                    zone_x = sas360zone.item.Pos_x < 0 ? (rectangle_width / 2) - zone_x : (rectangle_width / 2) + zone_x;
                                    zone_x -= zone_radio / 2;
                                    Canvas.SetLeft(m_list_grid_sas360zone[sas360zone.index], zone_x);

                                    double zone_y = rectangle_width * (Math.Abs(sas360zone.item.Pos_y) / (double)Globals.GetTheInstance().Panel_area_cm);
                                    zone_y = sas360zone.item.Pos_y < 0 ? (rectangle_height / 2) + zone_y : (rectangle_height / 2) - zone_y;
                                    zone_y -= zone_radio / 2;
                                    Canvas.SetTop(m_list_grid_sas360zone[sas360zone.index], zone_y);


                                    m_list_grid_sas360zone[sas360zone.index].Visibility = Visibility.Visible;
                                });

                            Refresh_zone_controls();

                            break;
                        }
                }
            }

            //Read data from sas360con
            else
            {
            }
        }

        #endregion



        #region Edit intternal config
        private void Button_edit_internal_config(object sender, RoutedEventArgs e)
        {
            Wrappanel_internal_config_send_state.Visibility = Visibility.Collapsed;

            if (Globals.GetTheInstance().ManageComThread.Is_connected)
            {
                int index = Array.IndexOf(m_list_button_edit_internal_config.ToArray(), sender as Button);

                string s_name_var = m_list_label_edit_internal_config_title[index].Content.ToString();

                EditInternalConfigWindow edit_internal = new()
                {
                    Name_internal_config = s_name_var.Substring(0, s_name_var.Length - 1),
                    Control_pos = (BUTTON_EDIT_INTERNAL_CONFIG_POS)index
                };
                edit_internal.ShowDialog();
                if (edit_internal.Save_changes)
                {
                    Wrappanel_internal_config_send_state.Visibility = Visibility.Visible;

                    m_selected_modbus_command = edit_internal.Modbus_command;

                    int[] values = new int[Constants.COMMAND_STRUCT_NUM_REG];
                    values[0] = m_selected_modbus_command.Index;
                    values[1] = m_command_watchdog++;

                    int index_param = 2;
                    edit_internal.List_values.ForEach(value => values[index_param] = value);


                    SEND_COMMAND_STATE state = Send_command(values);
                    switch (state)
                    {
                        case SEND_COMMAND_STATE.OK:
                            {
                                Image_internal_command_ok.Visibility = Visibility.Visible;
                                Image_internal_command_error.Visibility = Visibility.Collapsed;
                                Image_internal_command_warning.Visibility = Visibility.Collapsed;
                                break;
                            }

                        case SEND_COMMAND_STATE.ERROR:
                            {
                                Image_internal_command_ok.Visibility = Visibility.Collapsed;
                                Image_internal_command_error.Visibility = Visibility.Visible;
                                Image_internal_command_warning.Visibility = Visibility.Collapsed;
                                break;
                            }

                        case SEND_COMMAND_STATE.WARNING:
                            {
                                Image_internal_command_ok.Visibility = Visibility.Collapsed;
                                Image_internal_command_error.Visibility = Visibility.Collapsed;
                                Image_internal_command_warning.Visibility = Visibility.Visible;
                                break;
                            }
                    }
                }
            }
        }

        #endregion

        #region Refresh lists

        private void Button_refresh_lists_Click(object sender, RoutedEventArgs e)
        {
            int index = Array.IndexOf(m_list_button_refresh_memory.ToArray(), sender);

            Refresh_lists();

            Read_memory(MEMORY_CONFIG_TYPE.INTERNAL_CONFIG);
            Read_memory(MEMORY_CONFIG_TYPE.CONFIG_SAS360CON);
            Read_memory(MEMORY_CONFIG_TYPE.CONFIG_IOT);
        }

        private void Refresh_lists()
        {
            try
            {
                #region Lists memory

                Globals.GetTheInstance().List_internal_config = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.INTERNAL_CONFIG);
                Globals.GetTheInstance().List_config_sas360con = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.CONFIG_SAS360CON);
                Globals.GetTheInstance().List_config_iot = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.CONFIG_IOT);
                Globals.GetTheInstance().List_image_sas360con = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.IMAGE_SAS360CON);
                Globals.GetTheInstance().List_image_iot = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.IMAGE_IOT);

                Globals.GetTheInstance().List_console_closest_tags_base = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE);
                Globals.GetTheInstance().List_console_closest_tags_extended = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_EXTENDED);
                Globals.GetTheInstance().List_uwb_closest_tags_base = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_BASE);
                Globals.GetTheInstance().List_uwb_closest_tags_extended = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_EXTENDED);

                Globals.GetTheInstance().List_console_closest_zone_base = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_BASE);
                Globals.GetTheInstance().List_console_closest_zone_extended = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_EXTENDED);
                Globals.GetTheInstance().List_nvreg = Manage_memory.Load_memory_config(MEMORY_CONFIG_TYPE.NVREG);

                Globals.GetTheInstance().List_commands = Manage_memory.Load_memory_commands();
                Globals.GetTheInstance().List_commands.ForEach(commands =>
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

                Globals.GetTheInstance().List_event_log_read_reg = new();
                Globals.GetTheInstance().List_event_log = new();
                Globals.GetTheInstance().List_hist_log_read_reg = new();
                Globals.GetTheInstance().List_hist_log = new();

                #endregion


                #region Main

                m_collection_sas360_tag_processed.Clear();

                Listview_tag_processed.ItemsSource = m_collection_sas360_tag_processed;
                CollectionViewSource.GetDefaultView(Listview_tag_processed.ItemsSource).Refresh();

                m_collection_sas360_zone_processed.Clear();

                Listview_zone_processed.ItemsSource = m_collection_sas360_zone_processed;
                CollectionViewSource.GetDefaultView(Listview_zone_processed.ItemsSource).Refresh();

                #endregion


                #region Config

                m_collection_internal_config.Clear();

                Globals.GetTheInstance().List_internal_config.ForEach(internal_config => m_collection_internal_config.Add(internal_config));
                m_collection_internal_config = new ObservableCollection<Modbus_var>(m_collection_internal_config.OrderBy(internal_config => internal_config.Addr));
                Listview_internal_config.ItemsSource = m_collection_internal_config;
                CollectionViewSource.GetDefaultView(Listview_internal_config.ItemsSource).Refresh();


                m_collection_config_sas360con.Clear();

                Globals.GetTheInstance().List_config_sas360con.ForEach(config => m_collection_config_sas360con.Add(config));
                m_collection_config_sas360con = new ObservableCollection<Modbus_var>(m_collection_config_sas360con.OrderBy(config => config.Addr));
                Listview_config_sas360con.ItemsSource = m_collection_config_sas360con;
                CollectionViewSource.GetDefaultView(Listview_config_sas360con.ItemsSource).Refresh();


                m_collection_config_iot.Clear();

                Globals.GetTheInstance().List_config_iot.ForEach(config => m_collection_config_iot.Add(config));
                m_collection_config_iot = new ObservableCollection<Modbus_var>(m_collection_config_iot.OrderBy(config => config.Addr));
                Listview_config_iot.ItemsSource = m_collection_config_iot;
                CollectionViewSource.GetDefaultView(Listview_config_iot.ItemsSource).Refresh();


                m_collection_image_sas360con.Clear();

                Globals.GetTheInstance().List_image_sas360con.ForEach(image => m_collection_image_sas360con.Add(image));
                m_collection_image_sas360con = new ObservableCollection<Modbus_var>(m_collection_image_sas360con.OrderBy(image => image.Addr));
                Listview_image_sas360con.ItemsSource = m_collection_image_sas360con;
                CollectionViewSource.GetDefaultView(Listview_image_sas360con.ItemsSource).Refresh();

                #endregion

                #region Image

                m_collection_image_iot.Clear();

                Globals.GetTheInstance().List_image_iot.ForEach(image => m_collection_image_iot.Add(image));
                m_collection_image_iot = new ObservableCollection<Modbus_var>(m_collection_image_iot.OrderBy(image => image.Addr));
                Listview_image_iot.ItemsSource = m_collection_image_iot;
                CollectionViewSource.GetDefaultView(Listview_image_iot.ItemsSource).Refresh();

                #endregion

                #region Tags

                m_collection_console_closest_tags_base.Clear();

                Globals.GetTheInstance().List_console_closest_tags_base.ForEach(closest => m_collection_console_closest_tags_base.Add(closest));
                m_collection_console_closest_tags_base = new ObservableCollection<Modbus_var>(m_collection_console_closest_tags_base.OrderBy(closest => closest.Addr));
                Listview_console_closest_tags_base.ItemsSource = m_collection_console_closest_tags_base;
                CollectionViewSource.GetDefaultView(Listview_console_closest_tags_base.ItemsSource).Refresh();

                m_collection_console_closest_tags_extended.Clear();

                Globals.GetTheInstance().List_console_closest_tags_extended.ForEach(closest => m_collection_console_closest_tags_extended.Add(closest));
                m_collection_console_closest_tags_extended = new ObservableCollection<Modbus_var>(m_collection_console_closest_tags_extended.OrderBy(closest => closest.Addr));
                Listview_console_closest_tags_extended.ItemsSource = m_collection_console_closest_tags_extended;
                CollectionViewSource.GetDefaultView(Listview_console_closest_tags_extended.ItemsSource).Refresh();

                m_collection_uwb_closest_tags_base.Clear();

                Globals.GetTheInstance().List_uwb_closest_tags_base.ForEach(closest => m_collection_uwb_closest_tags_base.Add(closest));
                m_collection_uwb_closest_tags_base = new ObservableCollection<Modbus_var>(m_collection_uwb_closest_tags_base.OrderBy(closest => closest.Addr));
                Listview_uwb_closest_tags_base.ItemsSource = m_collection_uwb_closest_tags_base;
                CollectionViewSource.GetDefaultView(Listview_uwb_closest_tags_base.ItemsSource).Refresh();

                m_collection_uwb_closest_tags_extended.Clear();

                Globals.GetTheInstance().List_uwb_closest_tags_extended.ForEach(closest => m_collection_uwb_closest_tags_extended.Add(closest));
                m_collection_uwb_closest_tags_extended = new ObservableCollection<Modbus_var>(m_collection_uwb_closest_tags_extended.OrderBy(closest => closest.Addr));
                Listview_uwb_closest_tags_extended.ItemsSource = m_collection_uwb_closest_tags_extended;
                CollectionViewSource.GetDefaultView(Listview_uwb_closest_tags_extended.ItemsSource).Refresh();

                #endregion

                #region Zone

                m_collection_console_closest_zone_base.Clear();

                Globals.GetTheInstance().List_console_closest_zone_base.ForEach(closest => m_collection_console_closest_zone_base.Add(closest));
                m_collection_console_closest_zone_base = new ObservableCollection<Modbus_var>(m_collection_console_closest_zone_base.OrderBy(closest => closest.Addr));
                Listview_console_closest_zone_base.ItemsSource = m_collection_console_closest_zone_base;
                CollectionViewSource.GetDefaultView(Listview_console_closest_zone_base.ItemsSource).Refresh();

                m_collection_console_closest_zone_extended.Clear();

                Globals.GetTheInstance().List_console_closest_zone_extended.ForEach(closest => m_collection_console_closest_zone_extended.Add(closest));
                m_collection_console_closest_zone_extended = new ObservableCollection<Modbus_var>(m_collection_console_closest_zone_extended.OrderBy(closest => closest.Addr));
                Listview_console_closest_zone_extended.ItemsSource = m_collection_console_closest_zone_extended;
                CollectionViewSource.GetDefaultView(Listview_console_closest_zone_extended.ItemsSource).Refresh();

                #endregion

                #region nvreg

                m_collection_nvreg.Clear();

                Globals.GetTheInstance().List_nvreg.ForEach(closest => m_collection_nvreg.Add(closest));
                m_collection_nvreg = new ObservableCollection<Modbus_var>(m_collection_nvreg.OrderBy(nvreg => nvreg.Addr));
                Listview_nvreg.ItemsSource = m_collection_nvreg;
                CollectionViewSource.GetDefaultView(Listview_nvreg.ItemsSource).Refresh();

                #endregion

                #region Commands

                m_collection_commands.Clear();

                Globals.GetTheInstance().List_commands.ForEach(command => m_collection_commands.Add(command));
                m_collection_commands = new ObservableCollection<Modbus_command>(m_collection_commands.OrderBy(command => command.Index));
                Listview_commands.ItemsSource = m_collection_commands;
                CollectionViewSource.GetDefaultView(Listview_commands.ItemsSource).Refresh();

                #endregion

                #region Event log

                m_collection_event_log.Clear();
                Listview_event_log.ItemsSource = m_collection_event_log;
                CollectionViewSource.GetDefaultView(Listview_event_log.ItemsSource).Refresh();

                #endregion

                #region Hist log

                m_collection_hist_log.Clear();
                Listview_hist_log.ItemsSource = m_collection_hist_log;
                CollectionViewSource.GetDefaultView(Listview_hist_log.ItemsSource).Refresh();

                #endregion
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Refresh_lists)} -> {ex.Message}");
            }
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
            if (Listview_internal_config.SelectedItems.Count > 0)
            {
                if (Listview_internal_config.SelectedItem is Modbus_var)
                {
                    Modbus_var? selected_internal_config = Listview_internal_config.SelectedItem as Modbus_var;
                    m_selected_modbus_var = Globals.GetTheInstance().List_internal_config.First(modbus_var => modbus_var.Name.Equals(selected_internal_config!.Name));
                }
            }
        }

        #endregion

        #region Listview memory double click

        private void Listview_memory_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index_var = m_list_listview_memory.IndexOf(sender as ListView);

            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                Modbus_var modbus_var = item as Modbus_var;
                double addr_temp = modbus_var.Addr;

                SettingModbusVarWindow setting_var = new()
                {
                    Modbus_var = modbus_var
                };

                if (setting_var.ShowDialog() == true)
                {
                    List<Modbus_var> list_modbus_var_change =
                        index_var == (int)MEMORY_CONFIG_TYPE.INTERNAL_CONFIG ? Globals.GetTheInstance().List_internal_config :
                        index_var == (int)MEMORY_CONFIG_TYPE.CONFIG_SAS360CON ? Globals.GetTheInstance().List_config_sas360con :
                        index_var == (int)MEMORY_CONFIG_TYPE.CONFIG_IOT ? Globals.GetTheInstance().List_config_iot :
                        index_var == (int)MEMORY_CONFIG_TYPE.IMAGE_SAS360CON ? Globals.GetTheInstance().List_image_sas360con :
                        index_var == (int)MEMORY_CONFIG_TYPE.IMAGE_IOT ? Globals.GetTheInstance().List_image_iot :
                        index_var == (int)MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE ? Globals.GetTheInstance().List_console_closest_tags_base :
                        index_var == (int)MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_EXTENDED ? Globals.GetTheInstance().List_console_closest_tags_extended :
                        index_var == (int)MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_BASE ? Globals.GetTheInstance().List_uwb_closest_tags_base :
                        index_var == (int)MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_EXTENDED ? Globals.GetTheInstance().List_uwb_closest_tags_extended :
                        index_var == (int)MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_BASE ? Globals.GetTheInstance().List_console_closest_zone_base :
                        index_var == (int)MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_EXTENDED ? Globals.GetTheInstance().List_console_closest_zone_extended :
                        index_var == (int)MEMORY_CONFIG_TYPE.NVREG ? Globals.GetTheInstance().List_nvreg :

                        null;

                    int index_change = list_modbus_var_change.FindIndex(internal_config => internal_config.Addr == addr_temp);
                    list_modbus_var_change[index_change] = modbus_var;


                    bool save_ok = Manage_memory.Save_modbus_var((MEMORY_CONFIG_TYPE)index_var);
                    if (!save_ok)
                        System.Windows.MessageBox.Show("Error saving internal config.", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);

                    else
                        Refresh_lists();

                }
            }
        }


        #endregion

        #endregion

        #region UWB TAGS index changed

        private void DecimalUpDown_uwb_tags_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_collection_uwb_closest_tags_base != null)
            {
                Xceed.Wpf.Toolkit.DecimalUpDown current_decimal_updown = sender as Xceed.Wpf.Toolkit.DecimalUpDown;

                int first_memory_pos_tag_base_uwb = (int)MEMORY_MAP_READ.SAS360CON_TAG_12C_BASE_UWB_1;
                int last_memory_pos_tag_base_uwb = (int)MEMORY_MAP_READ.SAS360CON_TAG_12C_BASE_UWB_1 + (Globals.GetTheInstance().Total_closest_tags * 100);

                int first_memory_pos_tag_extended_uwb = (int)MEMORY_MAP_READ.SAS360CON_TAG_12C_EXTENDED_UWB_1;
                int last_memory_pos_tag_extended_uwb = (int)MEMORY_MAP_READ.SAS360CON_TAG_12C_EXTENDED_UWB_1 + (Globals.GetTheInstance().Total_closest_tags * 200);

                if (current_decimal_updown.Value != 0)
                {
                    first_memory_pos_tag_base_uwb = (int)MEMORY_MAP_READ.SAS360CON_TAG_12C_BASE_UWB_1 + (100 * ((int)current_decimal_updown.Value - 1));
                    last_memory_pos_tag_base_uwb = first_memory_pos_tag_base_uwb + 100;

                    first_memory_pos_tag_extended_uwb = (int)MEMORY_MAP_READ.SAS360CON_TAG_12C_EXTENDED_UWB_1 + (200 * ((int)current_decimal_updown.Value - 1));
                    last_memory_pos_tag_extended_uwb = first_memory_pos_tag_extended_uwb + 200;
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


        #endregion


        #region MAINTENANCE TAB

        #region Commands

        #region Listview commands events


        private void Listview_commands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Listview_commands.SelectedItems.Count > 0)
            {
                if (Listview_commands.SelectedItem is Modbus_command)
                {
                    m_list_wrappanel_commands.ForEach(wrappanel => wrappanel.Visibility = Visibility.Collapsed);

                    Modbus_command? selected_command = Listview_commands.SelectedItem as Modbus_command;
                    m_selected_modbus_command = Globals.GetTheInstance().List_commands.First(modbus_var => modbus_var.Name.Equals(selected_command!.Name));

                    Label_command_name.Content = m_selected_modbus_command.Name;

                    selected_command.List_param
                        .Select((Value, Index) => new { Name = Value, Pos = Index }).ToList()
                        .ForEach(param =>
                        {
                            m_list_wrappanel_commands[param.Pos].Visibility = param.Name == string.Empty ? Visibility.Collapsed : Visibility.Visible;

                            if (param.Name != string.Empty)
                            {
                                m_list_label_param_commands[param.Pos].Content = param.Name;
                                m_list_label_type_commands[param.Pos].Content =
                                    param.Name.Contains("u16") ? "UInt16" :
                                    param.Name.Contains("b16") ? "UInt16" :
                                    param.Name.Contains("u32") ? "UInt32" : "UInt16";
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

        private void Button_send_command_Click(object sender, RoutedEventArgs e)
        {
            int[] values = new int[Constants.COMMAND_STRUCT_NUM_REG];
            values[0] = m_selected_modbus_command.Index;
            values[1] = m_command_watchdog++;

            m_selected_modbus_command.List_param
                .Select((Value, Index) => new { Name = Value, Pos = Index }).ToList()
                .ForEach(param =>
                {
                    if (param.Name != string.Empty)
                    {
                        values[param.Pos + 2] = (int)m_list_decimalupdown_value_commands[param.Pos].Value;
                    }
                });

            SEND_COMMAND_STATE state = Send_command(values);
            switch (state)
            {
                case SEND_COMMAND_STATE.OK:
                    {
                        Image_command_ok.Visibility = Visibility.Visible;
                        Image_command_error.Visibility = Visibility.Collapsed;
                        Image_command_warning.Visibility = Visibility.Collapsed;
                        break;
                    }

                case SEND_COMMAND_STATE.ERROR:
                    {
                        Image_command_ok.Visibility = Visibility.Collapsed;
                        Image_command_error.Visibility = Visibility.Visible;
                        Image_command_warning.Visibility = Visibility.Collapsed;
                        break;
                    }

                case SEND_COMMAND_STATE.WARNING:
                    {
                        Image_command_ok.Visibility = Visibility.Collapsed;
                        Image_command_error.Visibility = Visibility.Collapsed;
                        Image_command_warning.Visibility = Visibility.Visible;
                        break;
                    }
            }
        }

        private SEND_COMMAND_STATE Send_command(int[] values)
        {

            SEND_COMMAND_STATE command_state = SEND_COMMAND_STATE.OK;
            try
            {
                if (Globals.GetTheInstance().ManageComThread.Is_connected && m_selected_modbus_command != null)
                {
                    //Comprobación del envío
                    bool send_ok = Globals.GetTheInstance().ManageComThread.Write_multiple_registers(0, values);
                    if (send_ok)
                    {
                        Tuple<bool, int[]> tuple_read = Globals.GetTheInstance().ManageComThread.Read_holding_registers_int32(0, Constants.COMMAND_STRUCT_NUM_REG);
                        if (tuple_read.Item1)
                        {
                            int[] registers = tuple_read.Item2;
                            command_state = (registers[0] == m_selected_modbus_command.Index) && (registers[1] == m_command_watchdog) ? SEND_COMMAND_STATE.OK : SEND_COMMAND_STATE.ERROR;
                        }
                        else
                            command_state = SEND_COMMAND_STATE.ERROR;

                    }
                    else
                        command_state = SEND_COMMAND_STATE.ERROR;


                    m_command_watchdog++;

                }
                else
                {
                    command_state = SEND_COMMAND_STATE.WARNING;
                }
            }
            catch (Exception ex)
            {
                command_state = SEND_COMMAND_STATE.WARNING;

                Manage_logs.SaveErrorValue($"{GetType().Name} -> {nameof(Button_send_command_Click)} -> {ex.Message}");
            }

            return command_state;
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

        #region SAS360 CONFIG OUTPUT

        private void Border_rele1_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void Border_rele2_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void Border_rele3_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void Border_rele4_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void Border_trans1_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void Border_trans2_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }
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


                //Write modbus data
                int force_mode_value = 0x00;
                Globals.GetTheInstance().Forced_mode_do_controls
                    .Select((item, index) => new { item, index }).ToList()
                    .ForEach(force_mode =>
                    {
                        if (force_mode.item)
                            force_mode_value = Functions.SetBitTo1(force_mode_value, force_mode.index);
                        else
                            force_mode_value = Functions.SetBitTo0(force_mode_value, force_mode.index);
                    });

                Globals.GetTheInstance().ManageComThread.Write_single_register(Globals.GetTheInstance().Image_sas360con_forced_mask.Forced_mode, force_mode_value);

                //Change control 
                Cursor cursor = Globals.GetTheInstance().Forced_mode_do_controls[index] ? Cursors.Hand : Cursors.Arrow;

                if (index == (int)FORCE_MODE_CODIF.M_FORCE_DIGITAL_OUTPUTS)
                {
                    m_list_maintenance_border_do_1.ForEach(border => border.Cursor = cursor);
                    m_list_maintenance_border_do_2.ForEach(border => border.Cursor = cursor);
                    m_list_maintenance_border_do_3.ForEach(border => border.Cursor = cursor);
                }
                else if (index == (int)FORCE_MODE_CODIF.M_FORCE_LEDS)
                {
                    m_list_maintenance_border_led.ForEach(border => border.Cursor = cursor);
                }
                else if (index == (int)FORCE_MODE_CODIF.M_AUDIO_TO_PLAY)
                {
                    m_list_maintenance_border_audio.ForEach(border => border.Cursor = cursor);
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
                    ushort selected_value = Globals.GetTheInstance().Image_sas360con_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.OUTPUT_1];

                    DigitalChangeWindow digital_window = new()
                    {
                        Digital_name = m_list_maintenance_label_do_1[index].Content?.ToString(),
                        Is_activated = Functions.IsBitSetTo1(selected_value, index)
                    };

                    digital_window.ShowDialog();
                    if (digital_window.Save_changes)
                    {
                        int changed_value = Functions.SetBitTo1(selected_value, index);
                        Globals.GetTheInstance().ManageComThread.Write_single_register(Globals.GetTheInstance().Image_sas360con_forced_mask.Forced_mask_do1, changed_value);
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
                    ushort selected_value = Globals.GetTheInstance().Image_sas360con_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.OUTPUT_2];

                    DigitalChangeWindow digital_window = new()
                    {
                        Digital_name = m_list_maintenance_label_do_2[index].Content?.ToString(),
                        Is_activated = Functions.IsBitSetTo1(selected_value, index)
                    };

                    digital_window.ShowDialog();
                    if (digital_window.Save_changes)
                    {
                        int changed_value = Functions.SetBitTo1(selected_value, index);
                        Globals.GetTheInstance().ManageComThread.Write_single_register(Globals.GetTheInstance().Image_sas360con_forced_mask.Forced_mask_do2, changed_value);
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
                    ushort selected_value = Globals.GetTheInstance().Image_sas360con_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.OUTPUT_3];

                    DigitalChangeWindow digital_window = new()
                    {
                        Digital_name = m_list_maintenance_label_do_3[index].Content?.ToString(),
                        Is_activated = Functions.IsBitSetTo1(selected_value, index)
                    };

                    digital_window.ShowDialog();
                    if (digital_window.Save_changes)
                    {
                        int changed_value = Functions.SetBitTo1(selected_value, index);
                        Globals.GetTheInstance().ManageComThread.Write_single_register(Globals.GetTheInstance().Image_sas360con_forced_mask.Forced_mask_do3, changed_value);
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
                    DIGITAL_STATES_IN_LIST STATE = index < 16 ? DIGITAL_STATES_IN_LIST.LED_1 : DIGITAL_STATES_IN_LIST.LED_2;
                    ushort selected_value = Globals.GetTheInstance().Image_sas360con_general.Array_digital_states[(int)STATE];
                    DigitalChangeWindow digital_window = new()
                    {
                        Digital_name = m_list_maintenance_label_led[index].Content?.ToString(),
                        Is_activated = Functions.IsBitSetTo1(selected_value, index)
                    };

                    digital_window.ShowDialog();
                    if (digital_window.Save_changes)
                    {
                        int changed_value = Functions.SetBitTo1(selected_value, index);
                        if (index < 16)
                            Globals.GetTheInstance().ManageComThread.Write_single_register(Globals.GetTheInstance().Image_sas360con_forced_mask.Forced_mask_codif_led1, changed_value);

                        else
                            Globals.GetTheInstance().ManageComThread.Write_single_register(Globals.GetTheInstance().Image_sas360con_forced_mask.Forced_mask_codif_led2, changed_value);

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
                    DIGITAL_STATES_IN_LIST STATE = index < 16 ? DIGITAL_STATES_IN_LIST.AUDIO_1 : DIGITAL_STATES_IN_LIST.AUDIO_2;
                    ushort selected_value = Globals.GetTheInstance().Image_sas360con_general.Array_digital_states[(int)STATE];

                    DigitalChangeWindow digital_window = new()
                    {
                        Digital_name = m_list_maintenance_label_audio[index].Content?.ToString(),
                        Is_activated = Functions.IsBitSetTo1(selected_value, index)
                    };

                    digital_window.ShowDialog();
                    if (digital_window.Save_changes)
                    {
                        int changed_value = Functions.SetBitTo1(selected_value, index);
                        if (index < 16)
                            Globals.GetTheInstance().ManageComThread.Write_single_register(Globals.GetTheInstance().Image_sas360con_forced_mask.Forced_mask_audio1, changed_value);

                        else
                            Globals.GetTheInstance().ManageComThread.Write_single_register(Globals.GetTheInstance().Image_sas360con_forced_mask.Forced_mask_audio2, changed_value);
                    }
                }
            }
        }

        #endregion

        #endregion


        #region MEM Records


        private void Button_read_event_log_Click(object sender, RoutedEventArgs e)
        {
            if (Globals.GetTheInstance().ManageComThread.Is_connected)
            {
                Image_warning_event_log.Visibility = Visibility.Collapsed;
                Image_read_ok_event_log.Visibility = Visibility.Collapsed;

                Globals.GetTheInstance().List_event_log_read_reg.Clear();
                Globals.GetTheInstance().List_event_log.Clear();
                m_collection_event_log.Clear();

                int num_event_logs = Globals.GetTheInstance().Image_sas360con_integrity_management.Recorded_event_number;
                m_pos_reg_event_log = 0;
                m_total_reg_event_log = Constants.EVENT_LOG_STRUCT_NUM_REG * num_event_logs;

                if (!b_read_event_log)
                {
                    Border_wait_event_log.Visibility = Visibility.Visible;
                    m_timer_read_event_log.Start();
                    b_read_event_log = true;
                }
            }
        }

        private void Button_stop_event_log_Click(object sender, RoutedEventArgs e)
        {
            b_read_event_log = false;
        }

        private void Button_export_event_log_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Directory.Exists($"{AppDomain.CurrentDomain.BaseDirectory}csv_files\\event_log"))
                    Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}csv_files\\event_log");

                Microsoft.Win32.SaveFileDialog save_file_Dialog = new()
                {
                    InitialDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}csv_files\\event_log",
                    FileName = $"Event_log_{DateTime.Now.Year:D4}{DateTime.Now.Month:D2}{DateTime.Now.Day:D2}",
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
                    event_log_csv_writer.WriteRecords(Globals.GetTheInstance().List_event_log);

                    MessageBox.Show("EVENT LOG CSV GENERADO", "INFO", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(MainWindow).Name} -> {nameof(Button_export_event_log_Click)} -> {ex.Message}");
            }
        }

        private void Timer_read_event_log_Tick(object sender, EventArgs e)
        {
            m_timer_read_event_log.Stop();

            try
            {
                if (m_pos_reg_event_log < m_total_reg_event_log)
                {
                    int pos_read_ini = (int)MEMORY_MAP_READ.SAS360CON_RECORDED_EVENTS + m_pos_reg_event_log;
                    int num_read_reg = m_pos_reg_event_log + Constants.MAX_REG_READ_MODBUS > m_total_reg_event_log ? m_total_reg_event_log - m_pos_reg_event_log : Constants.MAX_REG_READ_MODBUS;

                    //Simulate data
                    Tuple<bool, int[]> tuple_read;
                    if (Globals.GetTheInstance().Simulator_mode == BIT_STATE.ON)
                    {
                        Random random = new Random();
                        int[] array_read_reg = new int[num_read_reg];
                        for (int index = 0; index < array_read_reg.Length; index++)
                        {
                            array_read_reg[index] = random.Next(0, ushort.MaxValue);
                        }
                        tuple_read = new Tuple<bool, int[]>(true, array_read_reg);
                    }

                    //Read data from sas360con
                    else
                        tuple_read = Globals.GetTheInstance().ManageComThread.Read_holding_registers_int32(pos_read_ini, num_read_reg);


                    b_read_event_log = tuple_read.Item1;
                    if (b_read_event_log)
                        tuple_read.Item2.ToList().ForEach(event_reg => Globals.GetTheInstance().List_event_log_read_reg.Add(event_reg));

                    else
                        Dispatcher.Invoke(() => Image_warning_event_log.Visibility = Visibility.Visible);


                    m_pos_reg_event_log += Constants.MAX_REG_READ_MODBUS;
                }

                else
                {
                    Dispatcher.Invoke(() => Image_read_ok_event_log.Visibility = Visibility.Visible);
                    b_read_event_log = false;
                }


                if (!b_read_event_log)
                {
                    Dispatcher.Invoke(() => Border_wait_event_log.Visibility = Visibility.Collapsed);

                    //Decodificar
                    for (int index = 0; index < Globals.GetTheInstance().List_event_log_read_reg.Count; index++)
                    {
                        if (Globals.GetTheInstance().List_event_log_read_reg.Count - index >= Constants.EVENT_LOG_STRUCT_NUM_REG)
                        {
                            Event_log event_log = new();

                            event_log.Index = Globals.GetTheInstance().List_event_log_read_reg[index];

                            index++;


                            ushort[] array_rtc_value = new ushort[2] { (ushort)Globals.GetTheInstance().List_event_log_read_reg[index], (ushort)Globals.GetTheInstance().List_event_log_read_reg[index + 1] };
                            byte[] result = new byte[array_rtc_value.Length * sizeof(ushort)];
                            Buffer.BlockCopy(array_rtc_value, 0, result, 0, result.Length);
                            var array_rtc_value_uint32 = BitConverter.ToUInt32(result, 0);
                            event_log.RTC = Constants.date_ref.AddSeconds(array_rtc_value_uint32).ToString(Globals.GetTheInstance().DateFormat, new CultureInfo(Globals.GetTheInstance().DateProvider));

                            index += 2;


                            event_log.Miliseconds = Globals.GetTheInstance().List_event_log_read_reg[index];

                            index++;


                            event_log.Register_id = Globals.GetTheInstance().List_event_log_read_reg[index];

                            index++;


                            event_log.Val1 = Globals.GetTheInstance().List_event_log_read_reg[index];

                            index++;

                            event_log.Val2 = Globals.GetTheInstance().List_event_log_read_reg[index];

                            index++;

                            event_log.Val3 = Globals.GetTheInstance().List_event_log_read_reg[index];

                            index++;

                            event_log.Val4 = Globals.GetTheInstance().List_event_log_read_reg[index];

                            index++;

                            event_log.Val5 = Globals.GetTheInstance().List_event_log_read_reg[index];

                            index++;

                            event_log.Val6 = Globals.GetTheInstance().List_event_log_read_reg[index];

                            index++;

                            Globals.GetTheInstance().List_event_log.Add(event_log);
                        }
                    }

                    Dispatcher.Invoke(() => Globals.GetTheInstance().List_event_log.ForEach(event_log => m_collection_event_log.Add(event_log)));
                    Dispatcher.Invoke(() => CollectionViewSource.GetDefaultView(Listview_event_log.ItemsSource).Refresh());
                }
                else
                    m_timer_read_event_log.Start();
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() => Image_warning_event_log.Visibility = Visibility.Visible);
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Timer_read_event_log_Tick)} -> {ex.Message}");
            }
        }





        private void Button_read_hist_log_Click(object sender, RoutedEventArgs e)
        {

        }


        private void Timer_read_hist_log_Tick(object sender, EventArgs e)
        {
        }



        private void Button_export_hist_log_Click(object sender, RoutedEventArgs e)
        {

        }


        #endregion

        #endregion




        #region Draw detection areas

        private void Draw_config_sas360con_detection_areas()
        {
            double rectangle_width = Rectangle_sas360_data_draw.ActualWidth;
            double rectangle_height = Rectangle_sas360_data_draw.ActualHeight;


            double[] distances_yellow = Globals.GetTheInstance().Config_sas360con_detection_area.Array_yellow_detection_distances.ToList().ConvertAll(distance => rectangle_width * (distance / (double)Globals.GetTheInstance().Panel_area_cm)).ToArray();
            double[] distances_orange = Globals.GetTheInstance().Config_sas360con_detection_area.Array_orange_detection_distances.ToList().ConvertAll(distance => rectangle_width * (distance / (double)Globals.GetTheInstance().Panel_area_cm)).ToArray();
            double[] distances_red = Globals.GetTheInstance().Config_sas360con_detection_area.Array_red_detection_distances.ToList().ConvertAll(distance => rectangle_width * (distance / (double)Globals.GetTheInstance().Panel_area_cm)).ToArray();


            #region  Width - Height

            double yellow_width = distances_yellow[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT] + distances_yellow[(int)DETECTION_AREA_POS_IN_ARRAY.RIGHT];
            double yellow_height = distances_yellow[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT] + distances_yellow[(int)DETECTION_AREA_POS_IN_ARRAY.BACK];

            double orange_width = distances_orange[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT] + distances_orange[(int)DETECTION_AREA_POS_IN_ARRAY.RIGHT];
            double orange_height = distances_orange[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT] + distances_orange[(int)DETECTION_AREA_POS_IN_ARRAY.BACK];

            double red_width = distances_red[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT] + distances_red[(int)DETECTION_AREA_POS_IN_ARRAY.RIGHT];
            double red_height = distances_red[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT] + distances_red[(int)DETECTION_AREA_POS_IN_ARRAY.BACK];

            #endregion

            #region Diff

            double yellow_orange_width_diff = yellow_width - orange_width;
            double yellow_orange_height_diff = yellow_height - orange_height;

            double orange_red_width_diff = orange_width - red_width;
            double orange_red_height_diff = orange_height - red_height;

            double red_vehicle_width_diff = red_width - Image_sas360_con.Width;
            double red_vehicle_height_diff = red_height - Image_sas360_con.Height;

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
                Width = distances_yellow[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT] + distances_yellow[(int)DETECTION_AREA_POS_IN_ARRAY.RIGHT],
                Height = distances_yellow[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT] + distances_yellow[(int)DETECTION_AREA_POS_IN_ARRAY.BACK],
                Opacity = 1,
                CornerRadius = new CornerRadius(corner_radious)
            };

            Canvas_sas360_data_draw.Children.Add(border_detection_yellow_background);
            Canvas.SetLeft(border_detection_yellow_background, (rectangle_width / 2) - distances_yellow[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT]);
            Canvas.SetTop(border_detection_yellow_background, (rectangle_height / 2) - distances_yellow[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT]);
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
            Canvas.SetLeft(border_detection_yellow_up_limit, (rectangle_width / 2) - distances_yellow[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT]);
            Canvas.SetTop(border_detection_yellow_up_limit, (rectangle_height / 2) - distances_yellow[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT]);
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
            Canvas.SetLeft(border_detection_yellow_medium_limit, (rectangle_width / 2) - distances_yellow[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT] + (yellow_orange_width_diff / 4));
            Canvas.SetTop(border_detection_yellow_medium_limit, (rectangle_height / 2) - distances_yellow[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT] + (yellow_orange_height_diff / 4));
            Panel.SetZIndex(border_detection_yellow_medium_limit, 12);

            #endregion

            #region Down

            Border border_detection_yellow_down_limit = new()
            {
                BorderThickness = new Thickness(1),
                BorderBrush = limit_brush,
                Width = distances_orange[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT] + distances_orange[(int)DETECTION_AREA_POS_IN_ARRAY.RIGHT],
                Height = distances_orange[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT] + distances_orange[(int)DETECTION_AREA_POS_IN_ARRAY.BACK],
                Opacity = 0.2,
                CornerRadius = new CornerRadius(corner_radious)
            };

            Canvas_sas360_data_draw.Children.Add(border_detection_yellow_down_limit);
            Canvas.SetLeft(border_detection_yellow_down_limit, (rectangle_width / 2) - distances_orange[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT]);
            Canvas.SetTop(border_detection_yellow_down_limit, (rectangle_height / 2) - distances_orange[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT]);
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
                Width = distances_orange[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT] + distances_orange[(int)DETECTION_AREA_POS_IN_ARRAY.RIGHT],
                Height = distances_orange[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT] + distances_orange[(int)DETECTION_AREA_POS_IN_ARRAY.BACK],
                Opacity = 1,
                CornerRadius = new CornerRadius(corner_radious)
            };

            Canvas_sas360_data_draw.Children.Add(border_detection_orange_background);
            Canvas.SetLeft(border_detection_orange_background, (rectangle_width / 2) - distances_orange[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT]);
            Canvas.SetTop(border_detection_orange_background, (rectangle_height / 2) - distances_orange[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT]);
            Panel.SetZIndex(border_detection_orange_background, 13);

            #endregion

            #region Up

            Border border_detection_orange_up_limit = new()
            {
                BorderThickness = new Thickness(1),
                BorderBrush = limit_brush,
                Background = Brushes.Orange,
                Width = distances_orange[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT] + distances_orange[(int)DETECTION_AREA_POS_IN_ARRAY.RIGHT],
                Height = distances_orange[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT] + distances_orange[(int)DETECTION_AREA_POS_IN_ARRAY.BACK],
                Opacity = 0.3,
                CornerRadius = new CornerRadius(corner_radious)
            };

            Canvas_sas360_data_draw.Children.Add(border_detection_orange_up_limit);
            Canvas.SetLeft(border_detection_orange_up_limit, (rectangle_width / 2) - distances_orange[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT]);
            Canvas.SetTop(border_detection_orange_up_limit, (rectangle_height / 2) - distances_orange[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT]);
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
            Canvas.SetLeft(border_detection_orange_medium_limit, (rectangle_width / 2) - distances_orange[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT] + (orange_red_width_diff / 4));
            Canvas.SetTop(border_detection_orange_medium_limit, (rectangle_height / 2) - distances_orange[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT] + (orange_red_height_diff / 4));
            Panel.SetZIndex(border_detection_orange_medium_limit, 13);

            #endregion

            #region Down

            Border border_detection_orange_down_limit = new()
            {
                BorderThickness = new Thickness(1),
                BorderBrush = limit_brush,
                Width = distances_red[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT] + distances_red[(int)DETECTION_AREA_POS_IN_ARRAY.RIGHT],
                Height = distances_red[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT] + distances_red[(int)DETECTION_AREA_POS_IN_ARRAY.BACK],
                Opacity = 0.2,
                CornerRadius = new CornerRadius(corner_radious)
            };

            Canvas_sas360_data_draw.Children.Add(border_detection_orange_down_limit);

            Canvas.SetLeft(border_detection_orange_down_limit, (rectangle_width / 2) - distances_red[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT]);
            Canvas.SetTop(border_detection_orange_down_limit, (rectangle_height / 2) - distances_red[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT]);
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
                Width = distances_red[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT] + distances_red[(int)DETECTION_AREA_POS_IN_ARRAY.RIGHT],
                Height = distances_red[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT] + distances_red[(int)DETECTION_AREA_POS_IN_ARRAY.BACK],
                Opacity = 1,
                CornerRadius = new CornerRadius(corner_radious)
            };

            Canvas_sas360_data_draw.Children.Add(border_detection_red_background);
            Canvas.SetLeft(border_detection_red_background, (rectangle_width / 2) - distances_red[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT]);
            Canvas.SetTop(border_detection_red_background, (rectangle_height / 2) - distances_red[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT]);
            Panel.SetZIndex(border_detection_red_background, 14);

            #endregion

            #region Up

            Border border_detection_red_up_limit = new()
            {
                BorderThickness = new Thickness(1),
                BorderBrush = limit_brush,
                Background = Brushes.Red,
                Width = distances_red[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT] + distances_red[(int)DETECTION_AREA_POS_IN_ARRAY.RIGHT],
                Height = distances_red[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT] + distances_red[(int)DETECTION_AREA_POS_IN_ARRAY.BACK],
                Opacity = 0.2,
                CornerRadius = new CornerRadius(corner_radious)
            };

            Canvas_sas360_data_draw.Children.Add(border_detection_red_up_limit);
            Canvas.SetLeft(border_detection_red_up_limit, (rectangle_width / 2) - distances_red[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT]);
            Canvas.SetTop(border_detection_red_up_limit, (rectangle_height / 2) - distances_red[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT]);
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
            Canvas.SetLeft(border_detection_red_medium_limit, (rectangle_width / 2) - distances_red[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT] + (red_vehicle_width_diff / 4));
            Canvas.SetTop(border_detection_red_medium_limit, (rectangle_height / 2) - distances_red[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT] + (red_vehicle_height_diff / 4));
            Panel.SetZIndex(border_detection_red_medium_limit, 14);

            #endregion

            #region Down

            Border border_detection_red_down_limit = new()
            {
                BorderThickness = new Thickness(1),
                BorderBrush = limit_brush,
                Background = Brushes.White,
                Width = Image_sas360_con.Width + 20,
                Height = Image_sas360_con.Height + 20,
                Opacity = 1,
                CornerRadius = new CornerRadius(corner_radious)
            };

            Canvas_sas360_data_draw.Children.Add(border_detection_red_down_limit);

            double vehicle_left = Canvas.GetLeft(Image_sas360_con);
            double vehicle_top = Canvas.GetTop(Image_sas360_con);

            Canvas.SetLeft(border_detection_red_down_limit, vehicle_left - 10);
            Canvas.SetTop(border_detection_red_down_limit, vehicle_top - 10);

            Panel.SetZIndex(border_detection_red_down_limit, 15);

            #endregion

            #endregion


            #region Areas de deteccion individuales 

            #region Yellow

            double border_detection_yellow_left = Canvas.GetLeft(border_detection_yellow_up_limit);
            double border_detection_yellow_top = Canvas.GetTop(border_detection_yellow_up_limit);

            double yellow_led_width = yellow_width / 3;
            double yellow_led_height = yellow_height / 3;

            DashedBorder dashborder_detection_number_5 = new()
            {
                Background = Brushes.Yellow,
                Width = yellow_led_width - 1,
                Height = yellow_led_height - 1,
                Opacity = 1,
                UseDashedBorder = false,
                CornerRadius = new CornerRadius(corner_radious, 0, 0, 0),
                Visibility = Visibility.Collapsed
            };

            Canvas_sas360_data_draw.Children.Add(dashborder_detection_number_5);
            Canvas.SetLeft(dashborder_detection_number_5, border_detection_yellow_left + 1);
            Canvas.SetTop(dashborder_detection_number_5, border_detection_yellow_top + 1);
            Panel.SetZIndex(dashborder_detection_number_5, 12);

            m_array_led_detection[5] = dashborder_detection_number_5;


            DashedBorder dashborder_detection_number_6 = new()
            {
                Background = Brushes.Yellow,
                Width = yellow_led_width,
                Height = yellow_led_height - 2,
                Opacity = 1,
                UseDashedBorder = false,
                CornerRadius = new CornerRadius(0, 0, 0, 0),
                Visibility = Visibility.Collapsed
            };

            Canvas_sas360_data_draw.Children.Add(dashborder_detection_number_6);
            Canvas.SetLeft(dashborder_detection_number_6, border_detection_yellow_left + yellow_led_width);
            Canvas.SetTop(dashborder_detection_number_6, border_detection_yellow_top + 1);
            Panel.SetZIndex(dashborder_detection_number_6, 12);

            m_array_led_detection[6] = dashborder_detection_number_6;


            DashedBorder dashborder_detection_number_7 = new()
            {
                Background = Brushes.Yellow,
                Width = yellow_led_width - 1,
                Height = yellow_led_height - 1,
                Opacity = 1,
                UseDashedBorder = false,
                CornerRadius = new CornerRadius(0, corner_radious, 0, 0),
                Visibility = Visibility.Collapsed
            };

            Canvas_sas360_data_draw.Children.Add(dashborder_detection_number_7);
            Canvas.SetLeft(dashborder_detection_number_7, border_detection_yellow_left + (yellow_led_width * 2) - 2);
            Canvas.SetTop(dashborder_detection_number_7, border_detection_yellow_top + 2);
            Panel.SetZIndex(dashborder_detection_number_7, 12);

            m_array_led_detection[7] = dashborder_detection_number_7;


            DashedBorder dashborder_detection_number_17 = new()
            {
                Background = Brushes.Yellow,
                Width = yellow_led_width - 2,
                Height = yellow_led_height,
                Opacity = 1,
                UseDashedBorder = false,
                CornerRadius = new CornerRadius(0, 0, 0, 0),
                Visibility = Visibility.Collapsed
            };

            Canvas_sas360_data_draw.Children.Add(dashborder_detection_number_17);
            Canvas.SetLeft(dashborder_detection_number_17, border_detection_yellow_left + (yellow_led_width * 2) + 1);
            Canvas.SetTop(dashborder_detection_number_17, border_detection_yellow_top + yellow_led_height);
            Panel.SetZIndex(dashborder_detection_number_17, 12);

            m_array_led_detection[17] = dashborder_detection_number_17;


            DashedBorder dashborder_detection_number_24 = new()
            {
                Background = Brushes.Yellow,
                Width = yellow_led_width - 1,
                Height = yellow_led_height - 1,
                Opacity = 1,
                UseDashedBorder = false,
                CornerRadius = new CornerRadius(0, 0, corner_radious, 0),
                Visibility = Visibility.Collapsed
            };

            Canvas_sas360_data_draw.Children.Add(dashborder_detection_number_24);
            Canvas.SetLeft(dashborder_detection_number_24, border_detection_yellow_left + (yellow_led_width * 2));
            Canvas.SetTop(dashborder_detection_number_24, border_detection_yellow_top + (yellow_led_height * 2));
            Panel.SetZIndex(dashborder_detection_number_24, 12);

            m_array_led_detection[24] = dashborder_detection_number_24;


            DashedBorder dashborder_detection_number_23 = new()
            {
                Background = Brushes.Yellow,
                Width = yellow_led_width,
                Height = yellow_led_height - 2,
                Opacity = 1,
                UseDashedBorder = false,
                CornerRadius = new CornerRadius(0, 0, 0, 0),
                Visibility = Visibility.Collapsed
            };

            Canvas_sas360_data_draw.Children.Add(dashborder_detection_number_23);
            Canvas.SetLeft(dashborder_detection_number_23, border_detection_yellow_left + yellow_led_width);
            Canvas.SetTop(dashborder_detection_number_23, border_detection_yellow_top + (yellow_led_height * 2) + 1);
            Panel.SetZIndex(dashborder_detection_number_23, 12);

            m_array_led_detection[23] = dashborder_detection_number_23;



            DashedBorder dashborder_detection_number_22 = new()
            {
                Background = Brushes.Yellow,
                Width = yellow_led_width - 1,
                Height = yellow_led_height - 1,
                Opacity = 1,
                UseDashedBorder = false,
                CornerRadius = new CornerRadius(0, 0, 0, corner_radious),
                Visibility = Visibility.Collapsed
            };

            Canvas_sas360_data_draw.Children.Add(dashborder_detection_number_22);
            Canvas.SetLeft(dashborder_detection_number_22, border_detection_yellow_left + 1);
            Canvas.SetTop(dashborder_detection_number_22, border_detection_yellow_top + (yellow_led_height * 2));
            Panel.SetZIndex(dashborder_detection_number_22, 12);

            m_array_led_detection[22] = dashborder_detection_number_22;



            DashedBorder dashborder_detection_number_12 = new()
            {
                Background = Brushes.Yellow,
                Width = yellow_led_width - 2,
                Height = yellow_led_height,
                Opacity = 1,
                UseDashedBorder = false,
                CornerRadius = new CornerRadius(0, 0, 0, 0),
                Visibility = Visibility.Collapsed
            };

            Canvas_sas360_data_draw.Children.Add(dashborder_detection_number_12);
            Canvas.SetLeft(dashborder_detection_number_12, border_detection_yellow_left + 1);
            Canvas.SetTop(dashborder_detection_number_12, border_detection_yellow_top + yellow_led_height);
            Panel.SetZIndex(dashborder_detection_number_12, 12);

            m_array_led_detection[12] = dashborder_detection_number_12;


            #endregion

            #region Orange

            double border_detection_orange_left = Canvas.GetLeft(border_detection_orange_up_limit);
            double border_detection_orange_top = Canvas.GetTop(border_detection_orange_up_limit);

            double orange_led_width = orange_width / 3;
            double orange_led_height = orange_height / 3;


            DashedBorder dashborder_detection_number_8 = new()
            {
                Background = Brushes.Orange,
                Width = orange_led_width - 1,
                Height = orange_led_height - 1,
                Opacity = 1,
                UseDashedBorder = false,
                CornerRadius = new CornerRadius(corner_radious, 0, 0, 0),
                Visibility = Visibility.Collapsed
            };

            Canvas_sas360_data_draw.Children.Add(dashborder_detection_number_8);
            Canvas.SetLeft(dashborder_detection_number_8, border_detection_orange_left + 1);
            Canvas.SetTop(dashborder_detection_number_8, border_detection_orange_top + 1);
            Panel.SetZIndex(dashborder_detection_number_8, 13);

            m_array_led_detection[8] = dashborder_detection_number_8;


            DashedBorder dashborder_detection_number_9 = new()
            {
                Background = Brushes.Orange,
                Width = orange_led_width,
                Height = orange_led_height - 1,
                Opacity = 1,
                UseDashedBorder = false,
                CornerRadius = new CornerRadius(0, 0, 0, 0),
                Visibility = Visibility.Collapsed
            };

            Canvas_sas360_data_draw.Children.Add(dashborder_detection_number_9);
            Canvas.SetLeft(dashborder_detection_number_9, border_detection_orange_left + orange_led_width);
            Canvas.SetTop(dashborder_detection_number_9, border_detection_orange_top + 1);
            Panel.SetZIndex(dashborder_detection_number_9, 13);

            m_array_led_detection[9] = dashborder_detection_number_9;


            DashedBorder dashborder_detection_number_10 = new()
            {
                Background = Brushes.Orange,
                Width = orange_led_width - 1,
                Height = orange_led_height - 1,
                Opacity = 1,
                UseDashedBorder = false,
                CornerRadius = new CornerRadius(0, corner_radious, 0, 0),
                Visibility = Visibility.Collapsed
            };

            Canvas_sas360_data_draw.Children.Add(dashborder_detection_number_10);
            Canvas.SetLeft(dashborder_detection_number_10, border_detection_orange_left + (orange_led_width * 2));
            Canvas.SetTop(dashborder_detection_number_10, border_detection_orange_top + 1);
            Panel.SetZIndex(dashborder_detection_number_10, 13);

            m_array_led_detection[10] = dashborder_detection_number_10;


            DashedBorder dashborder_detection_number_16 = new()
            {
                Background = Brushes.Orange,
                Width = orange_led_width - 1,
                Height = orange_led_height,
                Opacity = 1,
                UseDashedBorder = false,
                CornerRadius = new CornerRadius(0, 0, 0, 0),
                Visibility = Visibility.Collapsed
            };

            Canvas_sas360_data_draw.Children.Add(dashborder_detection_number_16);
            Canvas.SetLeft(dashborder_detection_number_16, border_detection_orange_left + (orange_led_width * 2));
            Canvas.SetTop(dashborder_detection_number_16, border_detection_orange_top + orange_led_height);
            Panel.SetZIndex(dashborder_detection_number_16, 13);

            m_array_led_detection[16] = dashborder_detection_number_16;


            DashedBorder dashborder_detection_number_21 = new()
            {
                Background = Brushes.Orange,
                Width = orange_led_width - 1,
                Height = orange_led_height - 1,
                Opacity = 1,
                UseDashedBorder = false,
                CornerRadius = new CornerRadius(0, 0, corner_radious, 0),
                Visibility = Visibility.Collapsed
            };

            Canvas_sas360_data_draw.Children.Add(dashborder_detection_number_21);
            Canvas.SetLeft(dashborder_detection_number_21, border_detection_orange_left + (orange_led_width * 2));
            Canvas.SetTop(dashborder_detection_number_21, border_detection_orange_top + (orange_led_height * 2));
            Panel.SetZIndex(dashborder_detection_number_21, 13);

            m_array_led_detection[21] = dashborder_detection_number_21;


            DashedBorder dashborder_detection_number_20 = new()
            {
                Background = Brushes.Orange,
                Width = orange_led_width,
                Height = orange_led_height - 1,
                Opacity = 1,
                UseDashedBorder = false,
                CornerRadius = new CornerRadius(0, 0, 0, 0),
                Visibility = Visibility.Collapsed
            };

            Canvas_sas360_data_draw.Children.Add(dashborder_detection_number_20);
            Canvas.SetLeft(dashborder_detection_number_20, border_detection_orange_left + orange_led_width);
            Canvas.SetTop(dashborder_detection_number_20, border_detection_orange_top + (orange_led_height * 2));
            Panel.SetZIndex(dashborder_detection_number_20, 13);

            m_array_led_detection[20] = dashborder_detection_number_20;



            DashedBorder dashborder_detection_number_19 = new()
            {
                Background = Brushes.Orange,
                Width = orange_led_width - 1,
                Height = orange_led_height - 1,
                Opacity = 1,
                UseDashedBorder = false,
                CornerRadius = new CornerRadius(0, 0, 0, corner_radious),
                Visibility = Visibility.Collapsed
            };

            Canvas_sas360_data_draw.Children.Add(dashborder_detection_number_19);
            Canvas.SetLeft(dashborder_detection_number_19, border_detection_orange_left + 1);
            Canvas.SetTop(dashborder_detection_number_19, border_detection_orange_top + (orange_led_height * 2));
            Panel.SetZIndex(dashborder_detection_number_19, 13);

            m_array_led_detection[19] = dashborder_detection_number_19;



            DashedBorder dashborder_detection_number_13 = new()
            {
                Background = Brushes.Orange,
                Width = orange_led_width - 1,
                Height = orange_led_height,
                Opacity = 1,
                UseDashedBorder = false,
                CornerRadius = new CornerRadius(0, 0, 0, 0),
                Visibility = Visibility.Collapsed
            };

            Canvas_sas360_data_draw.Children.Add(dashborder_detection_number_13);
            Canvas.SetLeft(dashborder_detection_number_13, border_detection_orange_left + 1);
            Canvas.SetTop(dashborder_detection_number_13, border_detection_orange_top + orange_led_height);
            Panel.SetZIndex(dashborder_detection_number_13, 13);

            m_array_led_detection[13] = dashborder_detection_number_13;


            #endregion

            #region Red

            double border_detection_red_left = Canvas.GetLeft(border_detection_red_up_limit);
            double border_detection_red_top = Canvas.GetTop(border_detection_red_up_limit);

            double red_led_width = red_width / 3;
            double red_led_height = red_height;

            DashedBorder dashborder_detection_number_14 = new()
            {
                Background = Brushes.Red,
                Width = red_led_width - 1,
                Height = red_led_height - 2,
                Opacity = 1,
                UseDashedBorder = false,
                CornerRadius = new CornerRadius(corner_radious, 0, 0, corner_radious),
                Visibility = Visibility.Collapsed
            };

            Canvas_sas360_data_draw.Children.Add(dashborder_detection_number_14);
            Canvas.SetLeft(dashborder_detection_number_14, border_detection_red_left + 1);
            Canvas.SetTop(dashborder_detection_number_14, border_detection_red_top + 1);
            Panel.SetZIndex(dashborder_detection_number_14, 14);

            m_array_led_detection[14] = dashborder_detection_number_14;



            DashedBorder dashborder_detection_number_11 = new()
            {
                Background = Brushes.Red,
                Width = red_led_width,
                Height = red_led_height / 2,
                Opacity = 1,
                UseDashedBorder = false,
                CornerRadius = new CornerRadius(0, 0, 0, 0),
                Visibility = Visibility.Collapsed
            };

            Canvas_sas360_data_draw.Children.Add(dashborder_detection_number_11);
            Canvas.SetLeft(dashborder_detection_number_11, border_detection_red_left + red_led_width);
            Canvas.SetTop(dashborder_detection_number_11, border_detection_red_top + 1);
            Panel.SetZIndex(dashborder_detection_number_11, 14);

            m_array_led_detection[11] = dashborder_detection_number_11;



            DashedBorder dashborder_detection_number_15 = new()
            {
                Background = Brushes.Red,
                Width = red_led_width - 1,
                Height = red_led_height - 2,
                Opacity = 1,
                UseDashedBorder = false,
                CornerRadius = new CornerRadius(0, corner_radious, corner_radious, 0),
                Visibility = Visibility.Collapsed
            };

            Canvas_sas360_data_draw.Children.Add(dashborder_detection_number_15);
            Canvas.SetLeft(dashborder_detection_number_15, border_detection_red_left + (red_led_width * 2));
            Canvas.SetTop(dashborder_detection_number_15, border_detection_red_top + 1);
            Panel.SetZIndex(dashborder_detection_number_15, 14);

            m_array_led_detection[15] = dashborder_detection_number_15;



            DashedBorder dashborder_detection_number_18 = new()
            {
                Background = Brushes.Red,
                Width = red_led_width,
                Height = red_led_height / 2,
                Opacity = 1,
                UseDashedBorder = false,
                CornerRadius = new CornerRadius(0, 0, 0, 0),
                Visibility = Visibility.Collapsed
            };

            Canvas_sas360_data_draw.Children.Add(dashborder_detection_number_18);
            Canvas.SetLeft(dashborder_detection_number_18, border_detection_red_left + red_led_width);
            Canvas.SetTop(dashborder_detection_number_18, border_detection_red_top + (red_led_height / 2) - 1);
            Panel.SetZIndex(dashborder_detection_number_18, 14);

            m_array_led_detection[18] = dashborder_detection_number_18;


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
                color_anim.SetValue(Storyboard.TargetNameProperty, Ellipse_tag.Name);
                Storyboard storyboard = ((Storyboard)Resources["TagZoneStrokeStoryBoard"]);
                storyboard.Begin();
            }
        }

        #endregion


        #region Draw zones ellipses

        private void Draw_zone_ellipses()
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
                color_anim.SetValue(Storyboard.TargetNameProperty, Ellipse_zone_int.Name);
                Storyboard storyboard = ((Storyboard)Resources["TagZoneStrokeStoryBoard"]);
                storyboard.Begin();
            }
        }

        #endregion





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


    }
}
