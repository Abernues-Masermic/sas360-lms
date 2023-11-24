using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;

namespace sas360_test
{
    public partial class LoadSaveConfigConWindow : Window
    {
        public List<Modbus_var> List_modbus_var { get; set; } = new List<Modbus_var>();

        public PROGRAM_ACTIONS Program_actions { get; set; }

        public bool Load_save_ok {get; set;}

        private ObservableCollection<Modbus_var> m_collection_load_save = new ObservableCollection<Modbus_var>();




        #region Constructor
        public LoadSaveConfigConWindow()
        {
            InitializeComponent();
        }

        #endregion


        #region Mover pantalla

        private void Title_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (e.ButtonState == MouseButtonState.Pressed)
                DragMove();
        }

        #endregion


        #region Loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load_save_ok = false;

            Button_save_data_config_sas360con.Visibility = Program_actions == PROGRAM_ACTIONS.SAVE ? Visibility.Visible : Visibility.Collapsed;
            Button_load_data_config_sas360con.Visibility = Program_actions == PROGRAM_ACTIONS.LOAD ? Visibility.Visible : Visibility.Collapsed;

            m_collection_load_save = new ObservableCollection<Modbus_var>();
            List_modbus_var.ForEach(config_data => m_collection_load_save.Add(config_data));
            m_collection_load_save = new(m_collection_load_save.OrderBy(config_data => config_data.Addr));
            Listview_load_save_config.ItemsSource = m_collection_load_save;
            CollectionViewSource.GetDefaultView(Listview_load_save_config.ItemsSource).Refresh();
        }

        #endregion



        #region Save - Load

        private void Button_save_data_config_sas360con_Click(object sender, RoutedEventArgs e)
        {
            string ini_dir = $"{AppDomain.CurrentDomain.BaseDirectory}{Constants.SAS360CON_CFG_dir}";
            if (!Directory.Exists(ini_dir))
            {
                Directory.CreateDirectory(ini_dir);
            }


            var fileDialog = new SaveFileDialog
            {
                FileName = $"sas360con_cfg_{DateTime.Now.Year:D4}{DateTime.Now.Month:D2}{DateTime.Now.Day:D2}",
                InitialDirectory = ini_dir,
                Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*"
            };

            if (fileDialog.ShowDialog() == true)
            {
                bool save_ok = Manage_memory.Save_memory_list_to_csv(List_modbus_var, fileDialog.FileName);
                string s_info = save_ok ? "Sas360config saved" : "Error saving Sas360config";
                MessageBox.Show(s_info, "OK", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);

                Load_save_ok = true;
                Close();
            }
        }

        private void Button_load_data_config_sas360con_Click(object sender, RoutedEventArgs e)
        {
            Globals.GetTheInstance().List_sas360con_cfg.Clear();
            Modbus_var[] array_modbus_var = new Modbus_var[List_modbus_var.Count];
            List_modbus_var.CopyTo(0, array_modbus_var, 0, List_modbus_var.Count);
            Globals.GetTheInstance().List_sas360con_cfg = array_modbus_var.ToList();

            Globals.GetTheInstance().List_sas360con_cfg.ForEach(config_sas360con => {

                switch (config_sas360con.TypeName)
                {
                    case "Byte":
                        {
                            if (byte.TryParse(config_sas360con.Edit_value.ToString(), out byte value))
                                config_sas360con.Value = byte.Parse(config_sas360con.Edit_value.ToString());

                            break;
                        }

                    case "UInt16":
                        {
                            if (ushort.TryParse(config_sas360con.Edit_value.ToString(), out ushort value))
                                config_sas360con.Value = ushort.Parse(config_sas360con.Edit_value.ToString());

                            break;
                        }

                    case "Int16":
                        {
                            if (short.TryParse(config_sas360con.Edit_value.ToString(), out short value))
                                config_sas360con.Value = short.Parse(config_sas360con.Edit_value.ToString());

                            break;
                        }

                    case "UInt32":
                        {
                            if (int.TryParse(config_sas360con.Edit_value.ToString(), out int value))
                                config_sas360con.Value = int.Parse(config_sas360con.Edit_value.ToString());

                            break;
                        }

                    case "Int32":
                        {
                            if (int.TryParse(config_sas360con.Edit_value.ToString(), out int value))
                                config_sas360con.Value = int.Parse(config_sas360con.Edit_value.ToString());

                            break;
                        }

                    case "UTC":
                        {
                            config_sas360con.Value = config_sas360con.Edit_value.ToString();

                            break;
                        }

                    case "Single":
                        {
                            if (float.TryParse(config_sas360con.Value!.ToString(), out float value))
                                config_sas360con.Value = float.Parse(config_sas360con.Value.ToString());

                            break;
                        }
                }
            });

            Load_save_ok = true;
            Close();
        }

        #endregion



        #region Exit

        private void Button_exit_Click(object sender, RoutedEventArgs e)
        {
            Load_save_ok = false;
            Close();
        }


        #endregion


    }
}
