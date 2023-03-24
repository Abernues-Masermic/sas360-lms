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
        private List<TextBox> m_list_textbox_path= new();
        private List<Button> m_list_button_path = new();

        #endregion



        #region Constructor
        public SettingAppWindow()
        {
            InitializeComponent();

            #region Array controles

            m_list_checkbox_modbus_read = new List<CheckBox> {
               Checkbox_modbus_read_internal_config,
               Checkbox_modbus_read_config_sas360con,
               Checkbox_modbus_read_config_iot,
               Checkbox_modbus_read_image_sas360con,
               Checkbox_modbus_read_image_iot,

               Checkbox_modbus_read_console_closest_tags_base,
               Checkbox_modbus_read_console_closest_tags_extended,
               Checkbox_modbus_read_uwb_closest_tags_base,
               Checkbox_modbus_read_uwb_closest_tags_extended,

               Checkbox_modbus_read_console_closest_zone_base,
               Checkbox_modbus_read_console_closest_zone_extended,
               Checkbox_modbus_read_nvreg,
            };

            m_list_textbox_path = new List<TextBox>() {
                Textbox_internal_config,
                Textbox_config_sas360con,
                Textbox_config_iot,
                Textbox_image_sas360con,
                Textbox_image_iot,

                Textbox_console_closest_tags_base,
                Textbox_console_closest_tags_extended,
                Textbox_uwb_closest_tags_base,
                Textbox_uwb_closest_tags_extended,

                Textbox_console_closest_zone_base,
                Textbox_console_closest_zone_extended,
                Textbox_nvreg,

                Textbox_commands
            };

            m_list_button_path = new List<Button>() {
                Button_internal_config,
                Button_config_sas360con,
                Button_config_iot,
                Button_image_sas360con,
                Button_image_iot,

                Button_console_closest_tags_base,
                Button_console_closest_tags_extended,
                Button_uwb_closest_tags_base,
                Button_uwb_closest_tags_extended,

                Button_console_closest_zone_base,
                Button_console_closest_zone_extended,
                Button_nvreg,

                Button_commands
            };

            #endregion

            #region Decimal updown define values

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

            DecimalUpDown_UnitId.Value = 0;
            DecimalUpDown_UnitId.Minimum = 0;
            DecimalUpDown_UnitId.Maximum = 512;

            DecimalUpDown_conn_timeout.Value = 2000;
            DecimalUpDown_conn_timeout.Minimum = 100;
            DecimalUpDown_conn_timeout.Maximum = 10000;

            DecimalUpDown_read_memory_interval.Value = 2000;
            DecimalUpDown_read_memory_interval.Minimum = 100;
            DecimalUpDown_read_memory_interval.Maximum = 20000;



            DecimalUpDown_code_init.Value = 170;
            DecimalUpDown_code_init.Minimum = 0;
            DecimalUpDown_code_init.Maximum = 65535;
            DecimalUpDown_code_init.Increment = (decimal)1;

            DecimalUpDown_code_prod.Value = 90;
            DecimalUpDown_code_prod.Minimum = 0;
            DecimalUpDown_code_prod.Maximum = 65535;
            DecimalUpDown_code_prod.Increment = (decimal)1;


            DecimalUpDown_code_depu.Value = 90;
            DecimalUpDown_code_depu.Minimum = 0;
            DecimalUpDown_code_depu.Maximum = 65535;
            DecimalUpDown_code_depu.Increment = (decimal)1;

            #endregion
        }

        #endregion

        #region Loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region General

            Checkbox_simulator_mode.IsChecked = Globals.GetTheInstance().Simulator_mode == BIT_STATE.ON;

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

            for (int index_bit = 0; index_bit < Constants.MAX_BITS_USHORT_VALUE; index_bit++)
            {
                if (index_bit < m_list_checkbox_modbus_read.Count)
                {
                    m_list_checkbox_modbus_read[index_bit].IsChecked = Functions.IsBitSetTo1(Globals.GetTheInstance().Enable_read_memory_bits, index_bit);
                }
            }

            #endregion

            #region Write memory

            DecimalUpDown_code_init.Value = Globals.GetTheInstance().Code_init;
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

            #endregion

            #region Paths

            Textbox_internal_config.Text = Globals.GetTheInstance().Path_internal_config;
            Textbox_config_sas360con.Text = Globals.GetTheInstance().Path_config_sas360con;
            Textbox_config_iot.Text = Globals.GetTheInstance().Path_config_iot;
            Textbox_image_sas360con.Text = Globals.GetTheInstance().Path_image_sas360con;
            Textbox_image_iot.Text = Globals.GetTheInstance().Path_image_iot;

            Textbox_console_closest_tags_base.Text = Globals.GetTheInstance().Path_console_closest_tags_base;
            Textbox_console_closest_tags_extended.Text = Globals.GetTheInstance().Path_console_closest_tags_extended;
            Textbox_uwb_closest_tags_base.Text = Globals.GetTheInstance().Path_uwb_closest_tags_base;
            Textbox_uwb_closest_tags_extended.Text = Globals.GetTheInstance().Path_uwb_closest_tags_extended;

            Textbox_console_closest_zone_base.Text = Globals.GetTheInstance().Path_console_closest_zone_base;
            Textbox_console_closest_zone_extended.Text = Globals.GetTheInstance().Path_console_closest_zone_extended;
           
            Textbox_nvreg.Text = Globals.GetTheInstance().Path_nvreg;

            Textbox_commands.Text = Globals.GetTheInstance().Path_commands;

            #endregion
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
                ini_dir = System.IO. Path.GetDirectoryName(m_list_textbox_path[index].Text);
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

            #region General

            Globals.GetTheInstance().Simulator_mode = Checkbox_simulator_mode.IsChecked == true ? BIT_STATE.ON : BIT_STATE.OFF;

            double d_detection = (double)DecimalUpDown_panel_area.Value! * 100;
            Globals.GetTheInstance().Panel_area_cm = (int)d_detection;
            double d_grid = (double)DecimalUpDown_grid_area.Value! * 100;
            Globals.GetTheInstance().Grid_area_cm = (int)d_grid;
            Globals.GetTheInstance().Total_closest_tags = (int)DecimalUpDown_max_closest_tags.Value!;
            Globals.GetTheInstance().Total_closest_zone = (int)DecimalUpDown_max_closest_zone.Value!;

            Globals.GetTheInstance().DateFormat = Textbox_dateformat.Text;
            Globals.GetTheInstance().DateProvider = Combobox_provider.SelectedItem.ToString();

            #endregion

            #region Read memory

            int read_bit_value = 0;
            for (int index_bit = 0; index_bit < Constants.MAX_BITS_USHORT_VALUE; index_bit++)
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

            #endregion

            #region Write memory

            Globals.GetTheInstance().Code_init = (ushort)DecimalUpDown_code_init.Value;
            Globals.GetTheInstance().Code_prod = (ushort)DecimalUpDown_code_prod.Value;
            Globals.GetTheInstance().Code_depu = (ushort)DecimalUpDown_code_depu.Value;

            #endregion

            #region Communication

            Globals.GetTheInstance().Unit_id = (byte)DecimalUpDown_UnitId.Value!;
            Globals.GetTheInstance().Comm_port = Combobox_comm_port.SelectedValue!.ToString()!;
            Globals.GetTheInstance().Baud_rate = int.Parse(Combobox_baud_rate.SelectedValue!.ToString()!);

            Globals.GetTheInstance().Modbus_connection_timeout = (int)DecimalUpDown_conn_timeout.Value!;
            Globals.GetTheInstance().Read_memory_interval = (int)DecimalUpDown_read_memory_interval.Value!;

            #endregion

            #region Paths

            Globals.GetTheInstance().Path_internal_config = Textbox_internal_config.Text;
            Globals.GetTheInstance().Path_config_sas360con = Textbox_config_sas360con.Text;
            Globals.GetTheInstance().Path_config_iot = Textbox_config_iot.Text;
            Globals.GetTheInstance().Path_image_sas360con = Textbox_image_sas360con.Text;
            Globals.GetTheInstance().Path_image_iot = Textbox_image_iot.Text;

            Globals.GetTheInstance().Path_console_closest_tags_base = Textbox_console_closest_tags_base.Text;
            Globals.GetTheInstance().Path_console_closest_tags_extended = Textbox_console_closest_tags_extended.Text;
            Globals.GetTheInstance().Path_uwb_closest_tags_base = Textbox_uwb_closest_tags_base.Text;
            Globals.GetTheInstance().Path_uwb_closest_tags_extended = Textbox_uwb_closest_tags_extended.Text;

            Globals.GetTheInstance().Path_console_closest_zone_base = Textbox_console_closest_zone_base.Text;
            Globals.GetTheInstance().Path_console_closest_zone_extended = Textbox_console_closest_zone_extended.Text;
            Globals.GetTheInstance().Path_nvreg = Textbox_nvreg.Text;

            Globals.GetTheInstance().Path_commands = Textbox_commands.Text;

            #endregion

            if (Manage_file.Save_app_setting() && Manage_file.Save_comm_setting())
            {
                MessageBox.Show("CONFIG SAVED", "INFO", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
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
