using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace sas360_test
{
    public partial class EditInternalConfigWindow : Window
    {
        private List<WrapPanel> m_list_wrappanel_param = new();

        private List<Label> m_list_label_params = new();
        private List<DecimalUpDown> m_list_decimalupdown_value_commands = new();
        private int m_num_params;


        public bool Save_changes { get; set; }

        public string Name_internal_config { get; set; }

        public Modbus_command Modbus_command { get; set; }

        public List<ushort> List_values { get; set; }

        public BUTTON_EDIT_INTERNAL_CONFIG_POS Control_pos { get; set; }



        #region Constructor
        public EditInternalConfigWindow()
        {
            InitializeComponent();

            m_list_wrappanel_param.Add(Wrappanel_param1);
            m_list_wrappanel_param.Add(Wrappanel_param2);
            m_list_wrappanel_param.Add(Wrappanel_param3);
            m_list_wrappanel_param.Add(Wrappanel_param4);

            m_list_label_params.Add(Label_param1);
            m_list_label_params.Add(Label_param2);
            m_list_label_params.Add(Label_param3);
            m_list_label_params.Add(Label_param4);

            m_list_decimalupdown_value_commands.Add(DecimalUpDown_param1);
            m_list_decimalupdown_value_commands.Add(DecimalUpDown_param2);
            m_list_decimalupdown_value_commands.Add(DecimalUpDown_param3);
            m_list_decimalupdown_value_commands.Add(DecimalUpDown_param4);

            m_list_decimalupdown_value_commands.ForEach(decimalupdown =>
            {
                decimalupdown.Value = 0;
                decimalupdown.Minimum = 0;
                decimalupdown.Maximum = ushort.MaxValue;
                decimalupdown.Increment = 1;
            });
        }

        #endregion


        #region Loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Label_internal_config.Content = Name_internal_config;

            Save_changes = false;
            List_values = new()
            {
                Globals.GetTheInstance().Code_prod
            };

            try
            {
                switch (Control_pos)
                {
                    case BUTTON_EDIT_INTERNAL_CONFIG_POS.SERIAL_NUMBER:
                        {
                            m_num_params = 4;

                            Modbus_var modbus_var_1 = Globals.GetTheInstance().List_internal_config.First(internal_config => internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG);
                            Modbus_var modbus_var_2 = Globals.GetTheInstance().List_internal_config.First(internal_config => internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 1);
                            Modbus_var modbus_var_3 = Globals.GetTheInstance().List_internal_config.First(internal_config => internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 2);

                            Modbus_command = Globals.GetTheInstance().List_commands.First(command => command.Index == 212);

                            m_list_label_params[0].Content = Modbus_command.Param1;
                            m_list_label_params[1].Content = Modbus_command.Param2;
                            m_list_label_params[2].Content = Modbus_command.Param3;
                            m_list_label_params[3].Content = Modbus_command.Param4;

                            List_values.Add((ushort)modbus_var_1.Value);
                            List_values.Add((ushort)modbus_var_2.Value);
                            List_values.Add((ushort)modbus_var_3.Value);

                            break;
                        }

                    case BUTTON_EDIT_INTERNAL_CONFIG_POS.ID_MANUFACT:
                        {
                            m_num_params = 2;

                            Modbus_var modbus_var = Globals.GetTheInstance().List_internal_config.First(internal_config => internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 3);

                            Modbus_command = Globals.GetTheInstance().List_commands.First(command => command.Index == 213);
                            m_list_label_params[0].Content = Modbus_command.Param1;
                            m_list_label_params[1].Content = Modbus_command.Param2;
                            List_values.Add((ushort)modbus_var.Value);

                            break;
                        }

                    case BUTTON_EDIT_INTERNAL_CONFIG_POS.ID_TAG:
                        {
                            m_num_params = 2;
                            Modbus_var modbus_var = Globals.GetTheInstance().List_internal_config.First(internal_config => internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 4);

                            Modbus_command = Globals.GetTheInstance().List_commands.First(command => command.Index == 214);
                            m_list_label_params[0].Content = Modbus_command.Param1;
                            m_list_label_params[1].Content = Modbus_command.Param2;

                            int tag_id_3byte_1_lsb = modbus_var.Value >> 4;
                            List_values.Add((ushort)tag_id_3byte_1_lsb);

                            break;
                        }

                    case BUTTON_EDIT_INTERNAL_CONFIG_POS.RTU_SLAVE_SPEED:
                        {
                            m_num_params = 2;
                            Modbus_var modbus_var = Globals.GetTheInstance().List_internal_config.First(internal_config => internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 8);

                            Modbus_command = Globals.GetTheInstance().List_commands.First(command => command.Index == 221);
                            m_list_label_params[0].Content = Modbus_command.Param1;
                            m_list_label_params[1].Content = Modbus_command.Param2;
                            List_values.Add((ushort)modbus_var.Value);

                            break;
                        }

                    case BUTTON_EDIT_INTERNAL_CONFIG_POS.RTU_SLAVE_NUM:
                        {
                            m_num_params = 2;
                            Modbus_var modbus_var = Globals.GetTheInstance().List_internal_config.First(internal_config => internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 9);

                            Modbus_command = Globals.GetTheInstance().List_commands.First(command => command.Index == 222);

                            m_list_label_params[0].Content = Modbus_command.Param1;
                            m_list_label_params[1].Content = Modbus_command.Param2;
                            List_values.Add((ushort)modbus_var.Value);

                            break;
                        }

                    case BUTTON_EDIT_INTERNAL_CONFIG_POS.LIN_MASTER_SPEED:
                        {
                            m_num_params = 2;
                            Modbus_var modbus_var = Globals.GetTheInstance().List_internal_config.First(internal_config => internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 10);

                            Modbus_command = Globals.GetTheInstance().List_commands.First(command => command.Index == 223);

                            m_list_label_params[0].Content = Modbus_command.Param1;
                            m_list_label_params[1].Content = Modbus_command.Param2;
                            List_values.Add((ushort)modbus_var.Value);

                            break;
                        }
                }


                m_list_wrappanel_param.ForEach(param => param.Visibility = Visibility.Collapsed);
                for (int index = 0; index < m_num_params; index++)
                {
                    m_list_wrappanel_param[index].Visibility = Visibility.Visible;
                    m_list_decimalupdown_value_commands[index].Value = (decimal)List_values[index];
                }
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(EditInternalConfigWindow).Name} -> {nameof(Window_Loaded)} -> {ex.Message}");
            }
        }

        #endregion



        #region Save
        private void Button_save_Click(object sender, RoutedEventArgs e)
        {
            Save_changes= true;
            List_values.Clear();
            for (int index = 0; index < m_num_params; index++)
            {
                List_values.Add((ushort)m_list_decimalupdown_value_commands[index].Value);
            }

            Close();
        }

        #endregion


        #region Exit

        private void Button_exit_Click(object sender, RoutedEventArgs e)
        {
            Save_changes = false;
            Close();
        }

        #endregion



    }
}
