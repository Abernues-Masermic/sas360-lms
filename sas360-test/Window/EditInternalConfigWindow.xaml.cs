using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace sas360_test
{
    public partial class EditInternalConfigWindow : Window
    {
        private List<WrapPanel> m_list_wrappanel_param = new();

        private List<Label> m_list_label_params = new();
        private List<DecimalUpDown> m_list_decimalupdown_value_commands = new();
        private int m_num_params;


        public bool Save_changes { get; set; } = new bool();

        public string Name_internal_config { get; set; } = string.Empty;

        public Modbus_command Modbus_command { get; set; } = new Modbus_command();

        public List<int> List_values { get; set; } = new List<int>();

        public BUTTON_EDIT_INTERNAL_CONFIG_POS Control_pos { get; set; } = new BUTTON_EDIT_INTERNAL_CONFIG_POS();



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

                            Modbus_var modbus_var_1 = Globals.GetTheInstance().List_sas360con_internal_cfg.First(internal_config => internal_config.Addr ==  Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_INTERNAL_CFG]);
                            Modbus_var modbus_var_2 = Globals.GetTheInstance().List_sas360con_internal_cfg.First(internal_config => internal_config.Addr ==  Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_INTERNAL_CFG] + 1);
                            Modbus_var modbus_var_3 = Globals.GetTheInstance().List_sas360con_internal_cfg.First(internal_config => internal_config.Addr ==  Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_INTERNAL_CFG] + 2);

                            Modbus_command = Globals.GetTheInstance().List_sas360con_commands.First(command => command.Index == 212);

                            m_list_label_params[0].Content = Modbus_command.Param1;
                            m_list_label_params[1].Content = Modbus_command.Param2;
                            m_list_label_params[2].Content = Modbus_command.Param3;
                            m_list_label_params[3].Content = Modbus_command.Param4;

                            List_values.Add((int)modbus_var_1.Value);
                            List_values.Add((int)modbus_var_2.Value);
                            List_values.Add((int)modbus_var_3.Value);

                            break;
                        }

                    case BUTTON_EDIT_INTERNAL_CONFIG_POS.ID_MANUFACT:
                        {
                            m_num_params = 2;

                            Modbus_var modbus_var = Globals.GetTheInstance().List_sas360con_internal_cfg.First(internal_config => internal_config.Addr ==  Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_INTERNAL_CFG] + 3);

                            Modbus_command = Globals.GetTheInstance().List_sas360con_commands.First(command => command.Index == 213);
                            m_list_label_params[0].Content = Modbus_command.Param1;
                            m_list_label_params[1].Content = Modbus_command.Param2;
                            List_values.Add((int)modbus_var.Value);

                            break;
                        }

                    case BUTTON_EDIT_INTERNAL_CONFIG_POS.ID_TAG:
                        {
                            m_num_params = 2;
                            Modbus_var modbus_var = Globals.GetTheInstance().List_sas360con_internal_cfg.First(internal_config => internal_config.Addr ==  Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_INTERNAL_CFG] + 4);

                            Modbus_command = Globals.GetTheInstance().List_sas360con_commands.First(command => command.Index == 214);
                            m_list_label_params[0].Content = Modbus_command.Param1;
                            m_list_label_params[1].Content = Modbus_command.Param2;

                            int tag_id_3byte_1_lsb = modbus_var.Value; //El type tag son los 4 ultimos bits
                            List_values.Add(tag_id_3byte_1_lsb);

                            break;
                        }

                    case BUTTON_EDIT_INTERNAL_CONFIG_POS.RTU_SLAVE_NUM:
                        {
                            m_num_params = 2;
                            Modbus_var modbus_var = Globals.GetTheInstance().List_sas360con_internal_cfg.First(internal_config => internal_config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_INTERNAL_CFG] + 9);

                            Modbus_command = Globals.GetTheInstance().List_sas360con_commands.First(command => command.Index == 221);

                            m_list_label_params[0].Content = Modbus_command.Param1;
                            m_list_label_params[1].Content = Modbus_command.Param2;
                            List_values.Add((int)modbus_var.Value);

                            break;
                        }

                    case BUTTON_EDIT_INTERNAL_CONFIG_POS.RTU_SLAVE_SPEED:
                        {
                            m_num_params = 2;
                            Modbus_var modbus_var = Globals.GetTheInstance().List_sas360con_internal_cfg.First(internal_config => internal_config.Addr ==  Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_INTERNAL_CFG] + 8);

                            Modbus_command = Globals.GetTheInstance().List_sas360con_commands.First(command => command.Index == 222);
                            m_list_label_params[0].Content = Modbus_command.Param1;
                            m_list_label_params[1].Content = Modbus_command.Param2;
                            List_values.Add((int)modbus_var.Value);

                            break;
                        }

                    case BUTTON_EDIT_INTERNAL_CONFIG_POS.LIN_MASTER_SPEED:
                        {
                            m_num_params = 2;
                            Modbus_var modbus_var = Globals.GetTheInstance().List_sas360con_internal_cfg.First(internal_config => internal_config.Addr ==  Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_INTERNAL_CFG] + 10);

                            Modbus_command = Globals.GetTheInstance().List_sas360con_commands.First(command => command.Index == 223);

                            m_list_label_params[0].Content = Modbus_command.Param1;
                            m_list_label_params[1].Content = Modbus_command.Param2;
                            List_values.Add((int)modbus_var.Value);

                            break;
                        }
                }


                m_list_wrappanel_param.ForEach(param => param.Visibility = Visibility.Collapsed);
                for (int index = 0; index < m_num_params; index++)
                {
                    m_list_wrappanel_param[index].Visibility = Visibility.Visible;
                    m_list_decimalupdown_value_commands[index].Value = List_values[index];
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
            //ID TAG -> desplazar a posicion alta del byte
            if (Control_pos == BUTTON_EDIT_INTERNAL_CONFIG_POS.ID_TAG)
            {
                int byte_3er = (int)m_list_decimalupdown_value_commands[1].Value!; //El type tag son los 4 ultimos bits
                m_list_decimalupdown_value_commands[1].Value = byte_3er;
            }

            Save_changes= true;
            List_values.Clear();
            for (int index = 0; index < m_num_params; index++)
            {
                List_values.Add((int)m_list_decimalupdown_value_commands[index].Value!);
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
