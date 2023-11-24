using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace sas360_test
{
    public partial class SettingAppWindow : Window
    {

        #region Array controles

        private List<CheckBox> m_list_checkbox_modbus_read = new();
        private List<Xceed.Wpf.Toolkit.DecimalUpDown> m_list_decimal_modbus_read_ini = new();
        private List<Xceed.Wpf.Toolkit.DecimalUpDown> m_list_decimal_modbus_read_size = new();

        private List<TextBox> m_list_textbox_path= new();
        private List<Button> m_list_button_path = new();

        #endregion


        #region Constructor
        public SettingAppWindow()
        {
            InitializeComponent();

            #region Array controles

            m_list_checkbox_modbus_read = new List<CheckBox> {
               Checkbox_modbus_read_sas360con_internal_cfg,
               Checkbox_modbus_read_sas360con_cfg,
               Checkbox_modbus_read_iot_cfg,
               Checkbox_modbus_read_sas360con_image,
               Checkbox_modbus_read_iot_image,
               Checkbox_modbus_read_sas360con_maintennance,

               Checkbox_modbus_read_uwb_internal_cfg,
               Checkbox_modbus_read_uwb_image,

               Checkbox_modbus_read_console_closest_tags_base,
               Checkbox_modbus_read_console_closest_tags_extended,
               Checkbox_modbus_read_console_closest_zone_base,
               Checkbox_modbus_read_console_closest_zone_extended,

               Checkbox_modbus_read_uwb_closest_tags_base,
               Checkbox_modbus_read_uwb_closest_tags_extended,
               Checkbox_modbus_read_uwb_closest_zone_base,
               Checkbox_modbus_read_uwb_closest_zone_extended,

               Checkbox_modbus_read_sas360con_nvreg,
               Checkbox_modbus_read_sas360con_command,
               Checkbox_modbus_read_sas360con_event_log,
               Checkbox_modbus_read_sas360con_hist_log
            };

            m_list_textbox_path = new List<TextBox>() {
                Textbox_sas360con_internal_cfg,
                Textbox_sas360con_cfg,
                Textbox_iot_cfg,
                Textbox_sas360con_image,
                Textbox_iot_image,
                Textbox_sas360con_maintennance,

                Textbox_uwb_internal_cfg,
                Textbox_uwb_image,

                Textbox_console_closest_tags_base,
                Textbox_console_closest_tags_extended,
                Textbox_console_closest_zone_base,
                Textbox_console_closest_zone_extended,

                Textbox_uwb_closest_tags_base,
                Textbox_uwb_closest_tags_extended,
                Textbox_uwb_closest_zone_base,
                Textbox_uwb_closest_zone_extended,

                Textbox_sas360con_nvreg,
                Textbox_sas360con_commands
            };

            m_list_button_path = new List<Button>() {
                Button_sas360con_internal_cfg,
                Button_sas360con_cfg,
                Button_iot_cfg,
                Button_sas360con_image,
                Button_iot_image,
                Button_sas360con_maintennance,

                Button_uwb_internal_cfg,
                Button_uwb_image,

                Button_console_closest_tags_base,
                Button_console_closest_tags_extended,
                Button_console_closest_zone_base,
                Button_console_closest_zone_extended,

                Button_uwb_closest_tags_base,
                Button_uwb_closest_tags_extended,
                Button_uwb_closest_zone_base,
                Button_uwb_closest_zone_extended,

                Button_sas360con_nvreg,
                Button_sas360con_commands
            };

            #endregion

            #region Decimal updown define values

            #region General

            DecimalUpDown_panel_area.Value = 5;
            DecimalUpDown_panel_area.Minimum = 0;
            DecimalUpDown_panel_area.Maximum = 100;
            DecimalUpDown_panel_area.Increment = (decimal)0.1;

            DecimalUpDown_grid_area.Value = 5;
            DecimalUpDown_grid_area.Minimum = 0;
            DecimalUpDown_grid_area.Maximum = 100;
            DecimalUpDown_grid_area.Increment = (decimal)0.1;

            DecimalUpDown_max_closest_tags.Value = 12;
            DecimalUpDown_max_closest_tags.Minimum = 0;
            DecimalUpDown_max_closest_tags.Maximum = 30;

            DecimalUpDown_max_closest_zone.Value = 16;
            DecimalUpDown_max_closest_zone.Minimum = 0;
            DecimalUpDown_max_closest_zone.Maximum = 30;

            #endregion

            #region Communication

            DecimalUpDown_UnitId.Value = 0;
            DecimalUpDown_UnitId.Minimum = 0;
            DecimalUpDown_UnitId.Maximum = 512;

            DecimalUpDown_conn_timeout.Value = 2000;
            DecimalUpDown_conn_timeout.Minimum = 10;
            DecimalUpDown_conn_timeout.Maximum = 10000;

            DecimalUpDown_read_memory_interval.Value = 2000;
            DecimalUpDown_read_memory_interval.Minimum = 2;
            DecimalUpDown_read_memory_interval.Maximum = 20000;

            DecimalUpDown_read_log_interval.Value = 200;
            DecimalUpDown_read_log_interval.Minimum = 10;
            DecimalUpDown_read_log_interval.Maximum = 20000;

            DecimalUpDown_wait_send_write_interval.Value = 500;
            DecimalUpDown_wait_send_write_interval.Minimum = 10;
            DecimalUpDown_wait_send_write_interval.Maximum = 20000;

            DecimalUpDown_comm_timeout_interval.Value = 3000;
            DecimalUpDown_comm_timeout_interval.Minimum = 10;
            DecimalUpDown_comm_timeout_interval.Maximum = 20000;

            #endregion

            #region Modbus read

            #region Ini

            DecimalUpDown_read_sas360con_internal_cfg_ini.Value = 0;
            DecimalUpDown_read_sas360con_internal_cfg_ini.Minimum = 0;
            DecimalUpDown_read_sas360con_internal_cfg_ini.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_ini.Add(DecimalUpDown_read_sas360con_internal_cfg_ini);

            DecimalUpDown_read_sas360con_cfg_ini.Value = 0;
            DecimalUpDown_read_sas360con_cfg_ini.Minimum = 0;
            DecimalUpDown_read_sas360con_cfg_ini.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_ini.Add(DecimalUpDown_read_sas360con_cfg_ini);

            DecimalUpDown_read_iot_cfg_ini.Value = 0;
            DecimalUpDown_read_iot_cfg_ini.Minimum = 0;
            DecimalUpDown_read_iot_cfg_ini.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_ini.Add(DecimalUpDown_read_iot_cfg_ini);

            DecimalUpDown_read_sas360con_image_ini.Value = 0;
            DecimalUpDown_read_sas360con_image_ini.Minimum = 0;
            DecimalUpDown_read_sas360con_image_ini.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_ini.Add(DecimalUpDown_read_sas360con_image_ini);

            DecimalUpDown_read_iot_image_ini.Value = 0;
            DecimalUpDown_read_iot_image_ini.Minimum = 0;
            DecimalUpDown_read_iot_image_ini.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_ini.Add(DecimalUpDown_read_iot_image_ini);

            DecimalUpDown_read_sas360con_maintennance_ini.Value = 0;
            DecimalUpDown_read_sas360con_maintennance_ini.Minimum = 0;
            DecimalUpDown_read_sas360con_maintennance_ini.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_ini.Add(DecimalUpDown_read_sas360con_maintennance_ini);


            DecimalUpDown_read_uwb_internal_cfg_ini.Value = 0;
            DecimalUpDown_read_uwb_internal_cfg_ini.Minimum = 0;
            DecimalUpDown_read_uwb_internal_cfg_ini.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_ini.Add(DecimalUpDown_read_uwb_internal_cfg_ini);

            DecimalUpDown_read_uwb_image_ini.Value = 0;
            DecimalUpDown_read_uwb_image_ini.Minimum = 0;
            DecimalUpDown_read_uwb_image_ini.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_ini.Add(DecimalUpDown_read_uwb_image_ini);



            DecimalUpDown_read_console_closest_tags_base_ini.Value = 0;
            DecimalUpDown_read_console_closest_tags_base_ini.Minimum = 0;
            DecimalUpDown_read_console_closest_tags_base_ini.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_ini.Add(DecimalUpDown_read_console_closest_tags_base_ini);

            DecimalUpDown_read_console_closest_tags_extended_ini.Value = 0;
            DecimalUpDown_read_console_closest_tags_extended_ini.Minimum = 0;
            DecimalUpDown_read_console_closest_tags_extended_ini.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_ini.Add(DecimalUpDown_read_console_closest_tags_extended_ini);

            DecimalUpDown_read_console_closest_zone_base_ini.Value = 0;
            DecimalUpDown_read_console_closest_zone_base_ini.Minimum = 0;
            DecimalUpDown_read_console_closest_zone_base_ini.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_ini.Add(DecimalUpDown_read_console_closest_zone_base_ini);

            DecimalUpDown_read_console_closest_zone_extended_ini.Value = 0;
            DecimalUpDown_read_console_closest_zone_extended_ini.Minimum = 0;
            DecimalUpDown_read_console_closest_zone_extended_ini.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_ini.Add(DecimalUpDown_read_console_closest_zone_extended_ini);



            DecimalUpDown_read_uwb_closest_tags_base_ini.Value = 0;
            DecimalUpDown_read_uwb_closest_tags_base_ini.Minimum = 0;
            DecimalUpDown_read_uwb_closest_tags_base_ini.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_ini.Add(DecimalUpDown_read_uwb_closest_tags_base_ini);

            DecimalUpDown_read_uwb_closest_tags_extended_ini.Value = 0;
            DecimalUpDown_read_uwb_closest_tags_extended_ini.Minimum = 0;
            DecimalUpDown_read_uwb_closest_tags_extended_ini.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_ini.Add(DecimalUpDown_read_uwb_closest_tags_extended_ini);

            DecimalUpDown_read_uwb_closest_zone_base_ini.Value = 0;
            DecimalUpDown_read_uwb_closest_zone_base_ini.Minimum = 0;
            DecimalUpDown_read_uwb_closest_zone_base_ini.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_ini.Add(DecimalUpDown_read_uwb_closest_zone_base_ini);

            DecimalUpDown_read_uwb_closest_zone_extended_ini.Value = 0;
            DecimalUpDown_read_uwb_closest_zone_extended_ini.Minimum = 0;
            DecimalUpDown_read_uwb_closest_zone_extended_ini.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_ini.Add(DecimalUpDown_read_uwb_closest_zone_extended_ini);



            DecimalUpDown_read_sas360con_nvreg_ini.Value = 0;
            DecimalUpDown_read_sas360con_nvreg_ini.Minimum = 0;
            DecimalUpDown_read_sas360con_nvreg_ini.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_ini.Add(DecimalUpDown_read_sas360con_nvreg_ini);

            DecimalUpDown_read_sas360con_command_ini.Value = 0;
            DecimalUpDown_read_sas360con_command_ini.Minimum = 0;
            DecimalUpDown_read_sas360con_command_ini.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_ini.Add(DecimalUpDown_read_sas360con_command_ini);

            DecimalUpDown_read_sas360con_event_log_ini.Value = 0;
            DecimalUpDown_read_sas360con_event_log_ini.Minimum = 0;
            DecimalUpDown_read_sas360con_event_log_ini.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_ini.Add(DecimalUpDown_read_sas360con_event_log_ini);

            DecimalUpDown_read_sas360con_hist_log_ini.Value = 0;
            DecimalUpDown_read_sas360con_hist_log_ini.Minimum = 0;
            DecimalUpDown_read_sas360con_hist_log_ini.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_ini.Add(DecimalUpDown_read_sas360con_hist_log_ini);

            #endregion

            #region Size

            DecimalUpDown_read_sas360con_internal_cfg_size.Value = 0;
            DecimalUpDown_read_sas360con_internal_cfg_size.Minimum = 0;
            DecimalUpDown_read_sas360con_internal_cfg_size.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_size.Add(DecimalUpDown_read_sas360con_internal_cfg_size);

            DecimalUpDown_read_sas360con_cfg_size.Value = 0;
            DecimalUpDown_read_sas360con_cfg_size.Minimum = 0;
            DecimalUpDown_read_sas360con_cfg_size.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_size.Add(DecimalUpDown_read_sas360con_cfg_size);

            DecimalUpDown_read_iot_cfg_size.Value = 0;
            DecimalUpDown_read_iot_cfg_size.Minimum = 0;
            DecimalUpDown_read_iot_cfg_size.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_size.Add(DecimalUpDown_read_iot_cfg_size);

            DecimalUpDown_read_sas360con_image_size.Value = 0;
            DecimalUpDown_read_sas360con_image_size.Minimum = 0;
            DecimalUpDown_read_sas360con_image_size.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_size.Add(DecimalUpDown_read_sas360con_image_size);

            DecimalUpDown_read_iot_image_size.Value = 0;
            DecimalUpDown_read_iot_image_size.Minimum = 0;
            DecimalUpDown_read_iot_image_size.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_size.Add(DecimalUpDown_read_iot_image_size);

            DecimalUpDown_read_sas360con_maintennance_size.Value = 0;
            DecimalUpDown_read_sas360con_maintennance_size.Minimum = 0;
            DecimalUpDown_read_sas360con_maintennance_size.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_size.Add(DecimalUpDown_read_sas360con_maintennance_size);


            DecimalUpDown_read_uwb_internal_cfg_size.Value = 0;
            DecimalUpDown_read_uwb_internal_cfg_size.Minimum = 0;
            DecimalUpDown_read_uwb_internal_cfg_size.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_size.Add(DecimalUpDown_read_uwb_internal_cfg_size);

            DecimalUpDown_read_uwb_image_size.Value = 0;
            DecimalUpDown_read_uwb_image_size.Minimum = 0;
            DecimalUpDown_read_uwb_image_size.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_size.Add(DecimalUpDown_read_uwb_image_size);



            DecimalUpDown_read_console_closest_tags_base_size.Value = 0;
            DecimalUpDown_read_console_closest_tags_base_size.Minimum = 0;
            DecimalUpDown_read_console_closest_tags_base_size.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_size.Add(DecimalUpDown_read_console_closest_tags_base_size);

            DecimalUpDown_read_console_closest_tags_extended_size.Value = 0;
            DecimalUpDown_read_console_closest_tags_extended_size.Minimum = 0;
            DecimalUpDown_read_console_closest_tags_extended_size.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_size.Add(DecimalUpDown_read_console_closest_tags_extended_size);

            DecimalUpDown_read_console_closest_zone_base_size.Value = 0;
            DecimalUpDown_read_console_closest_zone_base_size.Minimum = 0;
            DecimalUpDown_read_console_closest_zone_base_size.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_size.Add(DecimalUpDown_read_console_closest_zone_base_size);

            DecimalUpDown_read_console_closest_zone_extended_size.Value = 0;
            DecimalUpDown_read_console_closest_zone_extended_size.Minimum = 0;
            DecimalUpDown_read_console_closest_zone_extended_size.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_size.Add(DecimalUpDown_read_console_closest_zone_extended_size);




            DecimalUpDown_read_uwb_closest_tags_base_size.Value = 0;
            DecimalUpDown_read_uwb_closest_tags_base_size.Minimum = 0;
            DecimalUpDown_read_uwb_closest_tags_base_size.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_size.Add(DecimalUpDown_read_uwb_closest_tags_base_size);

            DecimalUpDown_read_uwb_closest_tags_extended_size.Value = 0;
            DecimalUpDown_read_uwb_closest_tags_extended_size.Minimum = 0;
            DecimalUpDown_read_uwb_closest_tags_extended_size.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_size.Add(DecimalUpDown_read_uwb_closest_tags_extended_size);

            DecimalUpDown_read_uwb_closest_zone_base_size.Value = 0;
            DecimalUpDown_read_uwb_closest_zone_base_size.Minimum = 0;
            DecimalUpDown_read_uwb_closest_zone_base_size.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_size.Add(DecimalUpDown_read_uwb_closest_zone_base_size);

            DecimalUpDown_read_uwb_closest_zone_extended_size.Value = 0;
            DecimalUpDown_read_uwb_closest_zone_extended_size.Minimum = 0;
            DecimalUpDown_read_uwb_closest_zone_extended_size.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_size.Add(DecimalUpDown_read_uwb_closest_zone_extended_size);


            DecimalUpDown_read_sas360con_nvreg_size.Value = 0;
            DecimalUpDown_read_sas360con_nvreg_size.Minimum = 0;
            DecimalUpDown_read_sas360con_nvreg_size.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_size.Add(DecimalUpDown_read_sas360con_nvreg_size);

            DecimalUpDown_read_sas360con_command_size.Value = 0;
            DecimalUpDown_read_sas360con_command_size.Minimum = 0;
            DecimalUpDown_read_sas360con_command_size.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_size.Add(DecimalUpDown_read_sas360con_command_size);


            DecimalUpDown_read_sas360con_event_log_size.Value = 0;
            DecimalUpDown_read_sas360con_event_log_size.Minimum = 0;
            DecimalUpDown_read_sas360con_event_log_size.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_size.Add(DecimalUpDown_read_sas360con_event_log_size);

            DecimalUpDown_read_sas360con_hist_log_size.Value = 0;
            DecimalUpDown_read_sas360con_hist_log_size.Minimum = 0;
            DecimalUpDown_read_sas360con_hist_log_size.Maximum = ushort.MaxValue;
            m_list_decimal_modbus_read_size.Add(DecimalUpDown_read_sas360con_hist_log_size);

            #endregion

            #endregion

            #region Modbus write

            DecimalUpDown_code_ini.Value = 170;
            DecimalUpDown_code_ini.Minimum = 0;
            DecimalUpDown_code_ini.Maximum = 65535;
            DecimalUpDown_code_ini.Increment = (decimal)1;

            DecimalUpDown_code_prod.Value = 90;
            DecimalUpDown_code_prod.Minimum = 0;
            DecimalUpDown_code_prod.Maximum = 65535;
            DecimalUpDown_code_prod.Increment = (decimal)1;

            DecimalUpDown_code_depu.Value = 90;
            DecimalUpDown_code_depu.Minimum = 0;
            DecimalUpDown_code_depu.Maximum = 65535;
            DecimalUpDown_code_depu.Increment = (decimal)1;

            #endregion

            #endregion
        }

        #endregion


        #region Loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                #region General

                Checkbox_depur_mode.IsChecked = Globals.GetTheInstance().Depur_mode == BIT_STATE.ON;
                Checkbox_simulator_mode.IsChecked = Globals.GetTheInstance().Simulator_mode == BIT_STATE.ON;
                Checkbox_draw_map.IsChecked = Globals.GetTheInstance().Draw_map == BIT_STATE.ON;

                double d_panel = (double)Globals.GetTheInstance().Panel_area_cm / 100;
                double d_grid = (double)Globals.GetTheInstance().Grid_area_cm / 100;

                DecimalUpDown_panel_area.Value = (decimal)d_panel;
                DecimalUpDown_grid_area.Value = (decimal)d_grid;
                DecimalUpDown_max_closest_tags.Value = Globals.GetTheInstance().Total_closest_tags;
                DecimalUpDown_max_closest_zone.Value = Globals.GetTheInstance().Total_closest_zone;

                Textbox_dateformat.Text = Globals.GetTheInstance().DateFormat;

                Combobox_provider.ItemsSource = CultureInfo.GetCultures(CultureTypes.AllCultures);
                Combobox_provider.Text = Globals.GetTheInstance().DateProvider;

                #endregion

                #region Read memory

                for (int index_bit = 0; index_bit < m_list_checkbox_modbus_read.Count; index_bit++)
                {
                    if (index_bit < m_list_checkbox_modbus_read.Count)
                    {
                        m_list_checkbox_modbus_read[index_bit].IsChecked = Functions.IsBitSetTo1(Globals.GetTheInstance().Enable_read_memory_bits, index_bit);
                    }
                }

                for (int index = 0; index < Enum.GetNames(typeof(ENABLE_READ_MEMORY_BIT)).Length; index++)
                {
                    m_list_decimal_modbus_read_ini[index].Value = Globals.GetTheInstance().Memory_map_ini[index];
                    m_list_decimal_modbus_read_size[index].Value = Globals.GetTheInstance().Memory_map_size[index];
                }

                #endregion

                #region Write memory

                DecimalUpDown_code_ini.Value = Globals.GetTheInstance().Code_ini;
                DecimalUpDown_code_prod.Value = Globals.GetTheInstance().Code_prod;
                DecimalUpDown_code_depu.Value = Globals.GetTheInstance().Code_depu;

                #endregion


                #region Communication

                Refresh_com_ports();

                List<int> list_baud_rate = new();
                Array baud_rate_values = Enum.GetValues(typeof(BAUD_RATE));
                foreach (BAUD_RATE value in baud_rate_values)
                    list_baud_rate.Add((int)value);
                Combobox_baud_rate.ItemsSource = list_baud_rate;

                DecimalUpDown_UnitId.Value = Globals.GetTheInstance().Unit_id;
                Combobox_comm_port.SelectedValue = Globals.GetTheInstance().Comm_port;
                Combobox_baud_rate.SelectedValue = Globals.GetTheInstance().Baud_rate;

                DecimalUpDown_conn_timeout.Value = Globals.GetTheInstance().Modbus_connection_timeout;
                DecimalUpDown_read_memory_interval.Value = Globals.GetTheInstance().Read_memory_interval;
                DecimalUpDown_read_log_interval.Value = Globals.GetTheInstance().Read_log_interval;
                DecimalUpDown_wait_send_write_interval.Value = Globals.GetTheInstance().Wait_send_write_interval;
                DecimalUpDown_comm_timeout_interval.Value = Globals.GetTheInstance().Comm_timeout_interval;

                #endregion

                #region Paths

                Textbox_sas360con_internal_cfg.Text = Globals.GetTheInstance().Path_sas360con_internal_cfg;
                Textbox_sas360con_cfg.Text = Globals.GetTheInstance().Path_sas360con_cfg;
                Textbox_iot_cfg.Text = Globals.GetTheInstance().Path_iot_cfg;
                Textbox_sas360con_image.Text = Globals.GetTheInstance().Path_sas360con_image;
                Textbox_iot_image.Text = Globals.GetTheInstance().Path_iot_image;
                Textbox_sas360con_maintennance.Text = Globals.GetTheInstance().Path_sas360con_maintennance;

                Textbox_uwb_internal_cfg.Text = Globals.GetTheInstance().Path_uwb_internal_cfg;
                Textbox_uwb_image.Text = Globals.GetTheInstance().Path_uwb_image;

                Textbox_console_closest_tags_base.Text = Globals.GetTheInstance().Path_console_closest_tags_base;
                Textbox_console_closest_tags_extended.Text = Globals.GetTheInstance().Path_console_closest_tags_extended;
                Textbox_console_closest_zone_base.Text = Globals.GetTheInstance().Path_console_closest_zone_base;
                Textbox_console_closest_zone_extended.Text = Globals.GetTheInstance().Path_console_closest_zone_extended;

                Textbox_uwb_closest_tags_base.Text = Globals.GetTheInstance().Path_uwb_closest_tags_base;
                Textbox_uwb_closest_tags_extended.Text = Globals.GetTheInstance().Path_uwb_closest_tags_extended;
                Textbox_uwb_closest_zone_base.Text = Globals.GetTheInstance().Path_uwb_closest_zone_base;
                Textbox_uwb_closest_zone_extended.Text = Globals.GetTheInstance().Path_uwb_closest_zone_extended;

                Textbox_sas360con_nvreg.Text = Globals.GetTheInstance().Path_sas360con_nvreg;
                Textbox_sas360con_commands.Text = Globals.GetTheInstance().Path_sas360con_commands;

                #endregion
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{GetType().Name} -> {nameof(Window_Loaded)} -> {ex.Message}");
            }
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


        #region Refresh comm

        private void Button_refresh_comm_Click(object sender, RoutedEventArgs e)
        {
            Refresh_com_ports();
        }

        private void Refresh_com_ports()
        {
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            Combobox_comm_port.ItemsSource = null;
            Combobox_comm_port.Items.Clear();
            Combobox_comm_port.ItemsSource = ports;
            Combobox_comm_port.SelectedIndex = 0;
        }

        #endregion


       

        #region Button path

        private void Button_path_click(object sender, RoutedEventArgs e)
        {
            int index = Array.IndexOf(m_list_button_path.ToArray(), sender);

            string ini_dir = $"{AppDomain.CurrentDomain.BaseDirectory}{Constants.Memory_map_dir}";
            if (File.Exists(m_list_textbox_path[index].Text)) { 
                ini_dir = Path.GetDirectoryName(m_list_textbox_path[index].Text)!;
            }


            var fileDialog = new OpenFileDialog
            {
                InitialDirectory = ini_dir,
                Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*"
            };

            if (fileDialog.ShowDialog() == true)
                m_list_textbox_path[index].Text = fileDialog.FileName;
        }

        #endregion


        #region Save
        private void Button_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region General

                Globals.GetTheInstance().Depur_mode = Checkbox_depur_mode.IsChecked == true ? BIT_STATE.ON : BIT_STATE.OFF;
                Globals.GetTheInstance().Simulator_mode = Checkbox_simulator_mode.IsChecked == true ? BIT_STATE.ON : BIT_STATE.OFF;
                Globals.GetTheInstance().Draw_map = Checkbox_draw_map.IsChecked == true ? BIT_STATE.ON : BIT_STATE.OFF;

                double d_detection = (double)DecimalUpDown_panel_area.Value! * 100;
                Globals.GetTheInstance().Panel_area_cm = (int)d_detection;
                double d_grid = (double)DecimalUpDown_grid_area.Value! * 100;
                Globals.GetTheInstance().Grid_area_cm = (int)d_grid;
                Globals.GetTheInstance().Total_closest_tags = (int)DecimalUpDown_max_closest_tags.Value!;
                Globals.GetTheInstance().Total_closest_zone = (int)DecimalUpDown_max_closest_zone.Value!;

                Globals.GetTheInstance().DateFormat = Textbox_dateformat.Text;
                Globals.GetTheInstance().DateProvider = Combobox_provider.SelectedItem.ToString()!;

                #endregion

                #region Read memory

                int read_bit_value = 0;
                for (int index_bit = 0; index_bit < m_list_checkbox_modbus_read.Count; index_bit++)
                {
                    if (index_bit < m_list_checkbox_modbus_read.Count)
                    {
                        if (m_list_checkbox_modbus_read[index_bit].IsChecked == true)
                        {
                            read_bit_value = Functions.SetBitTo1(read_bit_value, index_bit);
                        }
                    }
                }
                Globals.GetTheInstance().Enable_read_memory_bits = read_bit_value;

                for (int index = 0; index < Enum.GetNames(typeof(ENABLE_READ_MEMORY_BIT)).Length; index++)
                {
                    Globals.GetTheInstance().Memory_map_ini[index] = (ushort)m_list_decimal_modbus_read_ini[index].Value!;
                    Globals.GetTheInstance().Memory_map_size[index] = (ushort)m_list_decimal_modbus_read_size[index].Value!;
                }

                #endregion

                #region Write memory

                Globals.GetTheInstance().Code_ini = (short)DecimalUpDown_code_ini.Value!;
                Globals.GetTheInstance().Code_prod = (short)DecimalUpDown_code_prod.Value!;
                Globals.GetTheInstance().Code_depu = (short)DecimalUpDown_code_depu.Value!;

                #endregion

                #region Communication

                Globals.GetTheInstance().Unit_id = (byte)DecimalUpDown_UnitId.Value!;

                Globals.GetTheInstance().Comm_port = Combobox_comm_port.SelectedValue != null ? Combobox_comm_port.SelectedValue!.ToString()! : string.Empty;
                Globals.GetTheInstance().Baud_rate = int.Parse(Combobox_baud_rate.SelectedValue!.ToString()!);

                Globals.GetTheInstance().Modbus_connection_timeout = (int)DecimalUpDown_conn_timeout.Value!;
                Globals.GetTheInstance().Read_memory_interval = (int)DecimalUpDown_read_memory_interval.Value!;
                Globals.GetTheInstance().Read_log_interval = (int)DecimalUpDown_read_log_interval.Value!;
                Globals.GetTheInstance().Wait_send_write_interval = (int)DecimalUpDown_wait_send_write_interval.Value!;
                Globals.GetTheInstance().Comm_timeout_interval = (int)DecimalUpDown_comm_timeout_interval.Value!;

                #endregion

                #region Paths

                Globals.GetTheInstance().Path_sas360con_internal_cfg = Textbox_sas360con_internal_cfg.Text;
                Globals.GetTheInstance().Path_sas360con_cfg = Textbox_sas360con_cfg.Text;
                Globals.GetTheInstance().Path_iot_cfg = Textbox_iot_cfg.Text;
                Globals.GetTheInstance().Path_sas360con_image = Textbox_sas360con_image.Text;
                Globals.GetTheInstance().Path_iot_image = Textbox_iot_image.Text;
                Globals.GetTheInstance().Path_sas360con_maintennance = Textbox_sas360con_maintennance.Text;

                Globals.GetTheInstance().Path_uwb_internal_cfg = Textbox_uwb_internal_cfg.Text;
                Globals.GetTheInstance().Path_uwb_image = Textbox_uwb_image.Text;

                Globals.GetTheInstance().Path_console_closest_tags_base = Textbox_console_closest_tags_base.Text;
                Globals.GetTheInstance().Path_console_closest_tags_extended = Textbox_console_closest_tags_extended.Text;
                Globals.GetTheInstance().Path_console_closest_zone_base = Textbox_console_closest_zone_base.Text;
                Globals.GetTheInstance().Path_console_closest_zone_extended = Textbox_console_closest_zone_extended.Text;

                Globals.GetTheInstance().Path_uwb_closest_tags_base = Textbox_uwb_closest_tags_base.Text;
                Globals.GetTheInstance().Path_uwb_closest_tags_extended = Textbox_uwb_closest_tags_extended.Text;
                Globals.GetTheInstance().Path_uwb_closest_zone_base = Textbox_uwb_closest_zone_base.Text;
                Globals.GetTheInstance().Path_uwb_closest_zone_extended = Textbox_uwb_closest_zone_extended.Text;

                Globals.GetTheInstance().Path_sas360con_nvreg = Textbox_sas360con_nvreg.Text;
                Globals.GetTheInstance().Path_sas360con_commands = Textbox_sas360con_commands.Text;

                #endregion

                if (Manage_file.Save_app_setting() && Manage_file.Save_comm_setting())
                {
                    MessageBox.Show("CONFIG SAVED", "INFO", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                }

            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{GetType().Name} -> {nameof(Button_save_Click)} -> {ex.Message}");
            }
        }

        #endregion

        #region Exit

        private void Button_exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion
    }
}
