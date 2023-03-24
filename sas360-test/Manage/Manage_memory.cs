using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows;

namespace sas360_test
{
    public class Manage_memory
    {

        public static List<Modbus_var> Load_memory_config(MEMORY_CONFIG_TYPE memory_config_type)
        {
            List<Modbus_var> list_modbus_var = new();

            string file_path =
                memory_config_type == MEMORY_CONFIG_TYPE.INTERNAL_CONFIG ? Globals.GetTheInstance().Path_internal_config :
                memory_config_type == MEMORY_CONFIG_TYPE.CONFIG_SAS360CON ? Globals.GetTheInstance().Path_config_sas360con :
                memory_config_type == MEMORY_CONFIG_TYPE.CONFIG_SAS360CON ? Globals.GetTheInstance().Path_config_iot :
                memory_config_type == MEMORY_CONFIG_TYPE.IMAGE_SAS360CON ? Globals.GetTheInstance().Path_image_sas360con :
                memory_config_type == MEMORY_CONFIG_TYPE.IMAGE_IOT ? Globals.GetTheInstance().Path_image_iot :

                memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE ? Globals.GetTheInstance().Path_console_closest_tags_base :
                memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_EXTENDED ? Globals.GetTheInstance().Path_console_closest_tags_extended :
                memory_config_type == MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_BASE ? Globals.GetTheInstance().Path_uwb_closest_tags_base :
                memory_config_type == MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_EXTENDED ? Globals.GetTheInstance().Path_uwb_closest_tags_extended :

                memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_BASE ? Globals.GetTheInstance().Path_console_closest_zone_base :
                memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_EXTENDED ? Globals.GetTheInstance().Path_console_closest_zone_extended :

                string.Empty;

            try
            {
                if (File.Exists(file_path))
                {
                    using TextReader reader = new StreamReader(file_path);
                    CultureInfo culture_info = new("es-ES"); //El separador decimal en el fichero es la coma
                    var config = new CsvConfiguration(culture_info) { Delimiter = ";", Encoding = Encoding.UTF8, HasHeaderRecord = true, MissingFieldFound = null };
                    using var csv_reader = new CsvReader(reader, config);
                    csv_reader.Context.RegisterClassMap<Modbus_var_map>();

                    list_modbus_var = csv_reader.GetRecords<Modbus_var>().ToList();
                }
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_file).Name} -> {nameof(Load_memory_config)} -> {ex.Message}");
            }


            return list_modbus_var;
        }


        public static List<Modbus_command> Load_memory_commands()
        {
            List<Modbus_command> list_modbus_commands = new();

            try
            {
                if (File.Exists(Globals.GetTheInstance().Path_commands))
                {
                    using TextReader reader = new StreamReader(Globals.GetTheInstance().Path_commands);
                    CultureInfo culture_info = new("es-ES"); //El separador decimal en el fichero es la coma
                    var config = new CsvConfiguration(culture_info) { Delimiter = ";", Encoding = Encoding.UTF8, HasHeaderRecord = true, MissingFieldFound = null };
                    using var csv_reader = new CsvReader(reader, config);
                    csv_reader.Context.RegisterClassMap<Modbus_command_map>();

                    list_modbus_commands = csv_reader.GetRecords<Modbus_command>().ToList();
                }
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_file).Name} -> {nameof(Load_memory_commands)} -> {ex.Message}");
            }

            return list_modbus_commands;
        }


        public static List<Modbus_var> Generate_csv_data(string textbox_data)
        {
            List<string> list_types = new() { "_SOFT_U8BITS", "_SOFT_U16BITS", "_SOFT_U32BITS", "_SOFT_U64BITS", "_SOFT_S8BITS", "_SOFT_S16BITS", "_SOFT_S32BITS", "_SOFT_S64BITS", "_SOFT_F32BITS" };

            List<Modbus_var> list_modbus_var = new();

            try
            {
                string[] sep = new string[] { "\r\n" };
                string[] memory_lines = textbox_data.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                memory_lines.ToList()
                    .Select((item, index) => new { Line = item, Position = index }).ToList()
                    .ForEach(line_pos =>
                    {
                        try
                        {

                            string[] fields = line_pos.Line.Split(" ");
                            if (double.TryParse(fields[(int)MEMORY_FIELD_POS_IN_ARRAY.ADDR], out double d_val) &&
                                double.TryParse(fields[(int)MEMORY_FIELD_POS_IN_ARRAY.SIZE], out d_val) &&
                                double.TryParse(fields[(int)MEMORY_FIELD_POS_IN_ARRAY.POS], out d_val) &&
                                list_types.Contains(fields[(int)MEMORY_FIELD_POS_IN_ARRAY.VAR_TYPE]))
                            {
                                Type? type =
                                    fields[(int)MEMORY_FIELD_POS_IN_ARRAY.VAR_TYPE] == "_SOFT_U8BITS" ? Type.GetType("System.Byte") :
                                    fields[(int)MEMORY_FIELD_POS_IN_ARRAY.VAR_TYPE] == "_SOFT_U16BITS" ? Type.GetType("System.UInt16") :
                                    fields[(int)MEMORY_FIELD_POS_IN_ARRAY.VAR_TYPE] == "_SOFT_U32BITS" ? Type.GetType("System.UInt32") :
                                    fields[(int)MEMORY_FIELD_POS_IN_ARRAY.VAR_TYPE] == "_SOFT_U64BITS" ? Type.GetType("System.UInt64") :
                                    fields[(int)MEMORY_FIELD_POS_IN_ARRAY.VAR_TYPE] == "_SOFT_S8BITS" ? Type.GetType("System.Int8") :
                                    fields[(int)MEMORY_FIELD_POS_IN_ARRAY.VAR_TYPE] == "_SOFT_S16BITS" ? Type.GetType("System.Int16") :
                                    fields[(int)MEMORY_FIELD_POS_IN_ARRAY.VAR_TYPE] == "_SOFT_S32BITS" ? Type.GetType("System.Int32") :
                                    fields[(int)MEMORY_FIELD_POS_IN_ARRAY.VAR_TYPE] == "_SOFT_S64BITS" ? Type.GetType("System.Int64") :
                                    fields[(int)MEMORY_FIELD_POS_IN_ARRAY.VAR_TYPE] == "_SOFT_F32BITS" ? Type.GetType("System.Single") : null;

                                double format_value =
                                    fields[(int)MEMORY_FIELD_POS_IN_ARRAY.FORMAT] == "UTC" ? Constants.CSV_NO_DEFINED_FORMAT :
                                    double.TryParse(fields[(int)MEMORY_FIELD_POS_IN_ARRAY.FORMAT], out double val) ? double.Parse(fields[(int)MEMORY_FIELD_POS_IN_ARRAY.FORMAT]) : Constants.CSV_NO_DEFINED_FORMAT;

                                list_modbus_var.Add(new Modbus_var()
                                {
                                    Addr = double.Parse(fields[(int)MEMORY_FIELD_POS_IN_ARRAY.ADDR]),
                                    Name = fields[(int)MEMORY_FIELD_POS_IN_ARRAY.VAR_NAME],
                                    Type = type!,
                                    TypeName = type!.Name,
                                    Unit = fields[(int)MEMORY_FIELD_POS_IN_ARRAY.UNIT],
                                    Format = format_value,
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Generate_csv_data)} -> Error decodifing memory lines -> Line Number : {line_pos.Position} -> {ex.Message}");
                        }
                    });
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Generate_csv_data)} -> {ex.Message}");
            }

            return list_modbus_var;
        }

        public static bool Save_csv_file(string textbox_data)
        {
            bool save_ok = false;

            try
            {
                List<Modbus_var> list_modbus_var = new();

                bool save_csv = true;
                int check_index = 0;
                string line_info = string.Empty;
                try
                {
                    string[] sep = new string[] { "\r\n" };
                    string[] memory_lines = textbox_data.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                    memory_lines = memory_lines.Skip(1).ToArray();
                    memory_lines.ToList()
                        .Select((item, index) => new { Line = item, Position = index }).ToList()
                        .ForEach(line_pos =>
                        {
                            check_index = line_pos.Position;
                            line_info = line_pos.Line;

                            string[] fields = line_pos.Line.Split(";");
                            list_modbus_var.Add(new Modbus_var()
                            {
                                Addr = double.Parse(fields[(int)MEMORY_FIELD_POS_CSV.ADDR]),
                                Name = fields[(int)MEMORY_FIELD_POS_CSV.NAME],
                                TypeName = fields[(int)MEMORY_FIELD_POS_CSV.TYPE_NAME],
                                Unit = fields[(int)MEMORY_FIELD_POS_CSV.UNIT],
                                Format = double.Parse(fields[(int)MEMORY_FIELD_POS_CSV.FORMAT]),
                            });
                        });
                }
                catch (Exception ex)
                {
                    save_csv = false;
                    MessageBox.Show("Error data conversion", "INFO", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                    Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Save_csv_file)} -> Generate modbus var list -> {ex.Message} -> Error pos : {check_index} -> Error line : {line_info} ");
                }

                if (save_csv)
                {
                    MessageBoxResult result = MessageBox.Show("Save CSV data format?", "INFO", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
                    if (result == MessageBoxResult.Yes)
                    {
                        var fileDialog = new SaveFileDialog
                        {
                            InitialDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}{Constants.Memory_map_dir}",
                            Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*"
                        };

                        if (fileDialog.ShowDialog() == true)
                        {
                            using TextWriter writer = new StreamWriter(fileDialog.FileName, false);
                            CultureInfo culture_info = new("es-ES"); //El separador decimal en el fichero es la coma
                            var config = new CsvConfiguration(culture_info) { Delimiter = ";", Encoding = Encoding.UTF8, HasHeaderRecord = true };
                            using var csv_writer = new CsvWriter(writer, config);
                            csv_writer.Context.RegisterClassMap<Modbus_var_map>();
                            csv_writer.WriteRecords(list_modbus_var);

                            save_ok = true;

                            MessageBox.Show("CSV data save finished", "INFO", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name}-> {nameof(Save_csv_file)} -> {ex.Message}");
            }

            return save_ok;
        }

        public static bool Save_modbus_var(MEMORY_CONFIG_TYPE memory_config_type)
        {
            bool save_ok = true;

            try
            {
                string file_path =
                    memory_config_type == MEMORY_CONFIG_TYPE.INTERNAL_CONFIG ? Globals.GetTheInstance().Path_internal_config :
                    memory_config_type == MEMORY_CONFIG_TYPE.CONFIG_SAS360CON ? Globals.GetTheInstance().Path_config_sas360con :
                    memory_config_type == MEMORY_CONFIG_TYPE.CONFIG_IOT ? Globals.GetTheInstance().Path_config_iot :
                    memory_config_type == MEMORY_CONFIG_TYPE.IMAGE_SAS360CON ? Globals.GetTheInstance().Path_image_sas360con :
                    memory_config_type == MEMORY_CONFIG_TYPE.IMAGE_IOT ? Globals.GetTheInstance().Path_image_iot :

                    memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE ? Globals.GetTheInstance().Path_console_closest_tags_base :
                    memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_EXTENDED ? Globals.GetTheInstance().Path_console_closest_tags_extended :
                    memory_config_type == MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_BASE ? Globals.GetTheInstance().Path_uwb_closest_tags_base :
                    memory_config_type == MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_EXTENDED ? Globals.GetTheInstance().Path_uwb_closest_tags_extended :

                    memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_BASE ? Globals.GetTheInstance().Path_console_closest_zone_base :
                    memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_EXTENDED ? Globals.GetTheInstance().Path_console_closest_zone_extended :

                    string.Empty;

                List<Modbus_var> list_modbus_var =
                    memory_config_type == MEMORY_CONFIG_TYPE.INTERNAL_CONFIG ? Globals.GetTheInstance().List_internal_config :
                    memory_config_type == MEMORY_CONFIG_TYPE.CONFIG_SAS360CON ? Globals.GetTheInstance().List_config_sas360con :
                    memory_config_type == MEMORY_CONFIG_TYPE.CONFIG_IOT ? Globals.GetTheInstance().List_config_iot :
                    memory_config_type == MEMORY_CONFIG_TYPE.IMAGE_SAS360CON ? Globals.GetTheInstance().List_image_sas360con :
                    memory_config_type == MEMORY_CONFIG_TYPE.IMAGE_IOT ? Globals.GetTheInstance().List_image_iot :

                    memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE ? Globals.GetTheInstance().List_console_closest_tags_base :
                    memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_EXTENDED ? Globals.GetTheInstance().List_console_closest_tags_extended :
                    memory_config_type == MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_BASE ? Globals.GetTheInstance().List_uwb_closest_tags_base :
                    memory_config_type == MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_EXTENDED ? Globals.GetTheInstance().List_uwb_closest_tags_extended :

                    memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_BASE ? Globals.GetTheInstance().List_console_closest_zone_base :
                    memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_EXTENDED ? Globals.GetTheInstance().List_console_closest_zone_extended :
                    memory_config_type == MEMORY_CONFIG_TYPE.NVREG ? Globals.GetTheInstance().List_nvreg :

                    null;



                CultureInfo culture_info = new("es-ES"); //El separador decimal en el fichero es la coma
                var config = new CsvConfiguration(culture_info) { Delimiter = ";", Encoding = Encoding.UTF8, HasHeaderRecord = true };

                using TextWriter modbus_var_writer = new StreamWriter(file_path, false);
                using var modbus_var_csv_writer = new CsvWriter(modbus_var_writer, config);
                modbus_var_csv_writer.Context.RegisterClassMap<Modbus_var_map>();
                modbus_var_csv_writer.WriteRecords(list_modbus_var);
            }
            catch (Exception ex)
            {
                save_ok = false;
                Manage_logs.SaveErrorValue($"{typeof(Manage_file).Name} -> {nameof(Save_modbus_var)} -> {ex.Message}");
            }

            return save_ok;
        }


        public static void Simulator_data(MEMORY_CONFIG_TYPE memory_config_type)
        {
            try
            {
                Random random = new();

                switch (memory_config_type)
                {
                    case MEMORY_CONFIG_TYPE.INTERNAL_CONFIG:
                        {
                            Globals.GetTheInstance().List_internal_config.ForEach(internal_config =>
                            {
                                try
                                {
                                    //Serial number
                                    if (internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG || internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 1 || internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 2)
                                        internal_config.Value = random.Next(1, 9912);

                                    //ID manufacturing
                                    else if (internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 3)
                                    {
                                        internal_config.Value = random.Next(1, 9912);
                                        Globals.GetTheInstance().Internal_config_sas360.ID_manufacturing = internal_config.Value.ToString();
                                    }

                                    //Tag type
                                    else if (internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 4)
                                    {
                                        Random r = new();
                                        int tag_type = r.Next(0, 4);
                                        int tag_id_3byte = tag_type << 4;
                                        internal_config.Value = tag_id_3byte;
                                        Globals.GetTheInstance().Internal_config_sas360.Tag_type = Enum.IsDefined(typeof(SAS360TAG_ZONE_TYPE), tag_type) ? (SAS360TAG_ZONE_TYPE)tag_type : SAS360TAG_ZONE_TYPE.UNKNOWN;
                                    }

                                    //Version
                                    else if (internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 5 || internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 6 || internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 7)
                                        internal_config.Value = random.Next(100, 200);

                                    //RTU slave
                                    else if (internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 8)
                                        internal_config.Value = random.Next(5, 10);

                                    //Modbus slave
                                    else if (internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 9)
                                        internal_config.Value = random.Next(1, 247);

                                    //Lin master speed
                                    else if (internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 10)
                                        internal_config.Value = random.Next(5, 10);

                                    //RTC FW UPDATE
                                    else if (internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 14)
                                    {
                                        ushort[] array_ushort_rtc_fw = new ushort[] { 24000, 32014 };
                                        byte[] array_byte_rtc_fw_1 = BitConverter.GetBytes(array_ushort_rtc_fw[1]);
                                        byte[] array_byte_rtc_fw_2 = BitConverter.GetBytes(array_ushort_rtc_fw[0]);
                                        byte[] aray_byte_rtc_fw = array_byte_rtc_fw_1.Concat(array_byte_rtc_fw_2).ToArray();
                                        UInt32 u32_rtc_fw = BitConverter.ToUInt32(aray_byte_rtc_fw);
                                        internal_config.Value = BitConverter.ToUInt32(aray_byte_rtc_fw, 0);

                                        DateTime date_rtc_fw = Constants.date_ref.Date.AddSeconds(u32_rtc_fw);
                                        Globals.GetTheInstance().Internal_config_sas360.RTC_fw_update = date_rtc_fw.ToString(Globals.GetTheInstance().DateFormat, new CultureInfo(Globals.GetTheInstance().DateProvider));
                                    }


                                    //RTC LAST CONFIG CHANGE
                                    else if (internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 16)
                                    {
                                        ushort[] array_ushort_rtc_cfg = new ushort[] { 25000, 32014 };
                                        byte[] array_byte_rtc_cfg_1 = BitConverter.GetBytes(array_ushort_rtc_cfg[1]);
                                        byte[] array_byte_rtc_cfg_2 = BitConverter.GetBytes(array_ushort_rtc_cfg[0]);
                                        byte[] aray_byte_rtc_cfg = array_byte_rtc_cfg_1.Concat(array_byte_rtc_cfg_2).ToArray();
                                        UInt32 u32_rtc_cfg = BitConverter.ToUInt32(aray_byte_rtc_cfg);
                                        internal_config.Value = BitConverter.ToUInt32(aray_byte_rtc_cfg, 0);

                                        DateTime date_rtc_cfg= Constants.date_ref.Date.AddSeconds(u32_rtc_cfg);
                                        Globals.GetTheInstance().Internal_config_sas360.RTC_config_update = date_rtc_cfg.ToString(Globals.GetTheInstance().DateFormat, new CultureInfo(Globals.GetTheInstance().DateProvider));
                                    }

                                    //Other values
                                    else
                                        internal_config.Value = random.Next(1, 999);

                                }
                                catch (Exception ex)
                                {
                                    Manage_logs.SaveErrorValue($"{typeof(Manage_file).Name} -> {nameof(Simulator_data)} -> {MEMORY_CONFIG_TYPE.INTERNAL_CONFIG} -> ADDR : {internal_config.Addr} ->  {ex.Message}");
                                }
                            });

                            #region Serial number

                            List<Modbus_var> list_modbus_sn = Globals.GetTheInstance().List_internal_config
                                .Where(internal_config =>
                                internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG ||
                                internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 1 ||
                                internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 2)
                                .OrderBy(internal_config => internal_config.Addr).ToList();

                            int index_array = 0;
                            string[] array_serial_number = new string[3];
                            list_modbus_sn.ForEach(modbus_sn =>
                            {
                                byte[] byte_serial_number = BitConverter.GetBytes(modbus_sn.Value);
                                ushort u16_serial_number = BitConverter.ToUInt16(byte_serial_number, 0);
                                array_serial_number[index_array++] = u16_serial_number.ToString("D4");
                            });

                            Globals.GetTheInstance().Internal_config_sas360.Serial_number = $"{array_serial_number[0]}.{array_serial_number[1]}.{array_serial_number[2]}";

                            #endregion

                            #region Versiones

                            List<Modbus_var> list_modbus_versiones = Globals.GetTheInstance().List_internal_config
                                .Where(internal_config => internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 5 || internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 6 || internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 7)
                                .OrderBy(internal_config => internal_config.Addr).ToList();

                            index_array = 0;
                            list_modbus_versiones.ForEach(modbus_version =>
                            {
                                byte[] byte_version = BitConverter.GetBytes(modbus_version.Value);
                                ushort u16_version = BitConverter.ToUInt16(byte_version, 0);

                                string s_version = u16_version.ToString("D3");
                                string s_version_dot = string.Empty;
                                int index_version = 0;
                                do
                                    s_version_dot += $"{s_version[index_version]}.";
                                while (index_version++ < s_version.Length - 2);
                                s_version_dot += s_version[index_version];


                                if (index_array == 0)
                                    Globals.GetTheInstance().Internal_config_sas360.Version_hw = s_version_dot;

                                else if (index_array == 1)
                                    Globals.GetTheInstance().Internal_config_sas360.Version_fw = s_version_dot;

                                else if (index_array == 2)
                                    Globals.GetTheInstance().Internal_config_sas360.Version_boot = s_version_dot;

                                index_array++;
                            });

                            #endregion

                            #region Modbus speed

                            List<Modbus_var> list_modbus_speed = Globals.GetTheInstance().List_internal_config
    .Where(internal_config => internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 8 || internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 10)
    .OrderBy(internal_config => internal_config.Addr).ToList();

                            Globals.GetTheInstance().Internal_config_sas360.RTU_slave_speed = SAS360CON_modbus_speed((ushort)list_modbus_speed[0].Value);
                            Globals.GetTheInstance().Internal_config_sas360.Lin_master_speed = SAS360CON_modbus_speed((ushort)list_modbus_speed[1].Value);

                            #endregion

                            //Slave number
                            Globals.GetTheInstance().Internal_config_sas360.RTU_slave_num = (ushort) Globals.GetTheInstance().List_internal_config.First(internal_config => internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 9).Value;


                            Globals.GetTheInstance().Internal_config_sas360.CFG_change_counter = (ushort) Globals.GetTheInstance().List_internal_config.First(internal_config => internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 18).Value;
                            Globals.GetTheInstance().Internal_config_sas360.CRC_config = (ushort) Globals.GetTheInstance().List_internal_config.First(internal_config => internal_config.Addr == (int)MEMORY_MAP_READ.SAS360CON_INTERNAL_CONFIG + 19).Value;

                            break;
                        }

                    case MEMORY_CONFIG_TYPE.CONFIG_SAS360CON:
                        {
                            try
                            {
                                #region Installation client definition

                                Globals.GetTheInstance().Config_sas360con_installation_client.Client = (ushort)random.Next(ushort.MinValue, ushort.MaxValue);
                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + 2).Value = Globals.GetTheInstance().Config_sas360con_installation_client.Client;

                                Globals.GetTheInstance().Config_sas360con_installation_client.Installation = (ushort)random.Next(ushort.MinValue, ushort.MaxValue);
                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + 3).Value = Globals.GetTheInstance().Config_sas360con_installation_client.Installation;

                                Globals.GetTheInstance().Config_sas360con_installation_client.Consola = (ushort)random.Next(ushort.MinValue, ushort.MaxValue);
                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + 4).Value = Globals.GetTheInstance().Config_sas360con_installation_client.Consola;

                                Globals.GetTheInstance().Config_sas360con_installation_client.Audio = (ushort)random.Next(0, 1);
                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + 5).Value = Globals.GetTheInstance().Config_sas360con_installation_client.Audio;

                                #endregion

                                #region Vehicle dimensions

                                int vehicle_width = 150;
                                int vehicle_height = 300;

                                byte[] byte_vehicle_dim_y = BitConverter.GetBytes(vehicle_width);
                                byte[] byte_vehicle_dim_x = BitConverter.GetBytes(vehicle_height);
                                byte[] byte_vehicle_dim = new byte[4];
                                Array.Copy(byte_vehicle_dim_y, 0, byte_vehicle_dim, 0, 2);
                                Array.Copy(byte_vehicle_dim_x, 0, byte_vehicle_dim, 2, 2);
                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + 6).Value = BitConverter.ToInt32(byte_vehicle_dim, 0);
                                Globals.GetTheInstance().Config_sas360con_vehicle_configuration.VehicleDim_xy = new int[2] { vehicle_width, vehicle_height };

                                #endregion

                                //Antena type
                                int antenna_config = random.Next(0, 1);
                                Globals.GetTheInstance().Config_sas360con_vehicle_configuration.Antenna_type = (ANTENNA_TYPE)antenna_config;
                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + 8).Value = antenna_config;

                                //Closest tag
                                Globals.GetTheInstance().Config_sas360con_vehicle_configuration.Num_closest_tags = (ushort)random.Next(1, Constants.MAX_DETECTED_TAGS);
                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + 9).Value = Globals.GetTheInstance().Config_sas360con_vehicle_configuration.Num_closest_tags;

                                #region Antenna pos

                                byte[] byte_ant1_y = BitConverter.GetBytes(100);
                                byte[] byte_ant1_x = BitConverter.GetBytes(-75);
                                byte[] byte_ant1 = new byte[4];
                                Array.Copy(byte_ant1_y, 0, byte_ant1, 0, 2);
                                Array.Copy(byte_ant1_x, 0, byte_ant1, 2, 2);
                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + 10).Value = BitConverter.ToInt32(byte_ant1, 0);


                                byte[] byte_ant2_y = BitConverter.GetBytes(100);
                                byte[] byte_ant2_x = BitConverter.GetBytes(75);
                                byte[] byte_ant2 = new byte[4];
                                Array.Copy(byte_ant2_y, 0, byte_ant2, 0, 2);
                                Array.Copy(byte_ant2_x, 0, byte_ant2, 2, 2);
                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + 12).Value = BitConverter.ToInt32(byte_ant2, 0);

                                byte[] byte_ant3_y = BitConverter.GetBytes(-150);
                                byte[] byte_ant3_x = BitConverter.GetBytes(0);
                                byte[] byte_ant3 = new byte[4];
                                Array.Copy(byte_ant3_y, 0, byte_ant3, 0, 2);
                                Array.Copy(byte_ant3_x, 0, byte_ant3, 2, 2);
                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + 14).Value = BitConverter.ToInt32(byte_ant3, 0);

                                Globals.GetTheInstance().Config_sas360con_vehicle_configuration.Antenna_xy = new int[5, 2];
                                Globals.GetTheInstance().Config_sas360con_vehicle_configuration.Antenna_xy[0, 0] = BitConverter.ToInt16(byte_ant1_x, 0);
                                Globals.GetTheInstance().Config_sas360con_vehicle_configuration.Antenna_xy[0, 1] = BitConverter.ToInt16(byte_ant1_y, 0);
                                Globals.GetTheInstance().Config_sas360con_vehicle_configuration.Antenna_xy[1, 0] = BitConverter.ToInt16(byte_ant2_x, 0);
                                Globals.GetTheInstance().Config_sas360con_vehicle_configuration.Antenna_xy[1, 1] = BitConverter.ToInt16(byte_ant2_y, 0);
                                Globals.GetTheInstance().Config_sas360con_vehicle_configuration.Antenna_xy[2, 0] = BitConverter.ToInt16(byte_ant3_x, 0);
                                Globals.GetTheInstance().Config_sas360con_vehicle_configuration.Antenna_xy[2, 1] = BitConverter.ToInt16(byte_ant3_y, 0);

                                #endregion

                                #region Antenna orientation

                                List<int> list_angles = new() { -4500, 4500, 18000 };

                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + 26).Value = list_angles[0];
                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + 27).Value = list_angles[1];
                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + 28).Value = list_angles[2];
                                Globals.GetTheInstance().Config_sas360con_vehicle_configuration.Antena_angle = new int[3] { list_angles[0], list_angles[1], list_angles[2] };

                                #endregion

                                #region Detection area

                                const int area_detection_start = 34;


                                Globals.GetTheInstance().Config_sas360con_detection_area.Area_detection_radio = 500;
                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + area_detection_start).Value = Globals.GetTheInstance().Config_sas360con_detection_area.Area_detection_radio;

                                Globals.GetTheInstance().Config_sas360con_detection_area.Interior_distance_to_antennas = 20;
                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + area_detection_start + 1).Value = Globals.GetTheInstance().Config_sas360con_detection_area.Interior_distance_to_antennas;

                                #region Yellow

                                int[] detection_dist_yellow = new int[4];
                                detection_dist_yellow[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT] = 480;
                                detection_dist_yellow[(int)DETECTION_AREA_POS_IN_ARRAY.RIGHT] = 400;
                                detection_dist_yellow[(int)DETECTION_AREA_POS_IN_ARRAY.BACK] = 480;
                                detection_dist_yellow[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT] = 400;
                                Globals.GetTheInstance().Config_sas360con_detection_area.Array_yellow_detection_distances = detection_dist_yellow;

                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + area_detection_start + 2).Value = detection_dist_yellow[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT];
                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + area_detection_start + 5).Value = detection_dist_yellow[(int)DETECTION_AREA_POS_IN_ARRAY.RIGHT];
                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + area_detection_start + 8).Value = detection_dist_yellow[(int)DETECTION_AREA_POS_IN_ARRAY.BACK];
                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + area_detection_start + 11).Value = detection_dist_yellow[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT];

                                #endregion

                                #region Orange

                                int[] detection_dist_orange = new int[4];
                                detection_dist_orange[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT] = 350;
                                detection_dist_orange[(int)DETECTION_AREA_POS_IN_ARRAY.RIGHT] = 280;
                                detection_dist_orange[(int)DETECTION_AREA_POS_IN_ARRAY.BACK] = 350;
                                detection_dist_orange[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT] = 280;
                                Globals.GetTheInstance().Config_sas360con_detection_area.Array_orange_detection_distances = detection_dist_orange;

                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + area_detection_start + 3).Value = detection_dist_orange[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT];
                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + area_detection_start + 6).Value = detection_dist_orange[(int)DETECTION_AREA_POS_IN_ARRAY.RIGHT];
                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + area_detection_start + 9).Value = detection_dist_orange[(int)DETECTION_AREA_POS_IN_ARRAY.BACK];
                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + area_detection_start + 12).Value = detection_dist_orange[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT];

                                #endregion

                                #region Red

                                int[] detection_dist_red = new int[4];
                                detection_dist_red[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT] = 250;
                                detection_dist_red[(int)DETECTION_AREA_POS_IN_ARRAY.RIGHT] = 200;
                                detection_dist_red[(int)DETECTION_AREA_POS_IN_ARRAY.BACK] = 250;
                                detection_dist_red[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT] = 200;
                                Globals.GetTheInstance().Config_sas360con_detection_area.Array_red_detection_distances = detection_dist_red;

                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + area_detection_start + 4).Value = detection_dist_red[(int)DETECTION_AREA_POS_IN_ARRAY.FRONT];
                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + area_detection_start + 7).Value = detection_dist_red[(int)DETECTION_AREA_POS_IN_ARRAY.RIGHT];
                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + area_detection_start + 10).Value = detection_dist_red[(int)DETECTION_AREA_POS_IN_ARRAY.BACK];
                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + area_detection_start + 13).Value = detection_dist_red[(int)DETECTION_AREA_POS_IN_ARRAY.LEFT];

                                #endregion

                                #region Hysteresys

                                List<int> list_hysteresys = new() { 50, 50, 50, 50, 50, 50, 10 };
                                Globals.GetTheInstance().Config_sas360con_detection_area.Array_hysteresys = list_hysteresys.ToArray();

                                int pos_hyst = Globals.GetTheInstance().List_config_sas360con
                                    .Select((Value, Index) => new { Value, Index }).ToList()
                                    .First(config => config.Value.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + 48).Index;

                                for (int index_hyst = 0; index_hyst < list_hysteresys.Count; index_hyst++)
                                {
                                    Globals.GetTheInstance().List_config_sas360con[pos_hyst++].Value = list_hysteresys[index_hyst];
                                }

                                #endregion

                                #endregion

                                #region Actuaciones salidas

                                int index_pos_actuaciones_salidas = (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + 56;
                                int index_actuaciones_salidas_in_list = Globals.GetTheInstance().List_config_sas360con.FindIndex(config => config.Addr == index_pos_actuaciones_salidas);
                                for (int index = 0; index < Enum.GetNames(typeof(ACTUACIONES_SALIDAS_POS_IN_ARRAY)).Length; index++)
                                {
                                    Globals.GetTheInstance().Config_sas360con_general.Array_actuaciones_salidas[index] = (ushort)random.Next(0, ushort.MaxValue);
                                   
                                    Globals.GetTheInstance().List_config_sas360con[index_actuaciones_salidas_in_list].Value = Globals.GetTheInstance().Config_sas360con_general.Array_actuaciones_salidas[index];
                                    index_actuaciones_salidas_in_list++;
                                }

                                #endregion

                                #region UWB CONFIG

                                for (int index = 0; index < Constants.LIN_TOTAL_COUNT; index++)
                                {
                                    Globals.GetTheInstance().Config_sas360con_general.Array_lin_used[index] = (byte)random.Next(0, 8);
                                    Globals.GetTheInstance().Config_sas360con_general.Array_lin_modbus_slave[index] = (byte)random.Next(0, 255);
                                }

                                int index_pos_uwb_config = (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + 80;

                                int index_pos_uwb_in_list = Globals.GetTheInstance().List_config_sas360con.FindIndex(config => config.Addr == index_pos_uwb_config);
                                for (int index_uwb = 0; index_uwb < Constants.LIN_TOTAL_COUNT; index_uwb++)
                                {
                                    Globals.GetTheInstance().List_config_sas360con[index_pos_uwb_in_list].Value = Globals.GetTheInstance().Config_sas360con_general.Array_lin_used[index_uwb];
                                    index_pos_uwb_in_list++;
                                }

                                for (int index_uwb = 0; index_uwb < Constants.LIN_TOTAL_COUNT; index_uwb++)
                                {
                                    Globals.GetTheInstance().List_config_sas360con[index_pos_uwb_in_list].Value = Globals.GetTheInstance().Config_sas360con_general.Array_lin_modbus_slave[index_uwb];
                                    index_pos_uwb_in_list++;
                                }

                                for (int index_uwb = 0; index_uwb < Constants.LIN_TOTAL_COUNT; index_uwb++)
                                {
                                    Globals.GetTheInstance().Array_sas360_uwb[index_uwb].ID = Globals.GetTheInstance().Config_sas360con_general.Array_lin_used[index_uwb];
                                    Globals.GetTheInstance().Array_sas360_uwb[index_uwb].Modbus_slave = Globals.GetTheInstance().Config_sas360con_general.Array_lin_modbus_slave[index_uwb];
                                }

                                #endregion

                                #region Calculadas config

                                ushort[] rtc_last_config_change_value = new ushort[] { 25500, 32014 };
                                byte[] byte_rtc_last_config_change_1 = BitConverter.GetBytes(rtc_last_config_change_value[1]);
                                byte[] byte_rtc_last_config_change_2 = BitConverter.GetBytes(rtc_last_config_change_value[0]);
                                byte[] rtc_last_config_change = byte_rtc_last_config_change_1.Concat(byte_rtc_last_config_change_2).ToArray();
                                uint u32_rtc_last_config_change = BitConverter.ToUInt32(rtc_last_config_change, 0);

                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + 116).Value = u32_rtc_last_config_change;

                                DateTime date_rtc_last_config_change = Constants.date_ref.Date.AddSeconds(u32_rtc_last_config_change);
                                Globals.GetTheInstance().Config_sas360con_general.RTC_last_config_change = date_rtc_last_config_change.ToString(Globals.GetTheInstance().DateFormat, new CultureInfo(Globals.GetTheInstance().DateProvider));

                                Globals.GetTheInstance().Config_sas360con_general.Calc_config_change_counter = (ushort)random.Next(1, ushort.MaxValue);
                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + 118).Value = Globals.GetTheInstance().Config_sas360con_general.Calc_config_change_counter;

                                Globals.GetTheInstance().Config_sas360con_general.CRC_config = (ushort)random.Next(1, ushort.MaxValue);
                                Globals.GetTheInstance().List_config_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_CONFIGURATION_SAS360CON + 119).Value = Globals.GetTheInstance().Config_sas360con_general.CRC_config;

                                #endregion

                            }
                            catch (Exception ex)
                            {
                                Manage_logs.SaveErrorValue($"{typeof(Manage_file).Name} -> {nameof(Simulator_data)} -> {MEMORY_CONFIG_TYPE.CONFIG_SAS360CON} -> {ex.Message}");
                            }

                            break;
                        }

                    case MEMORY_CONFIG_TYPE.CONFIG_IOT:
                        {
                            break;
                        }

                    case MEMORY_CONFIG_TYPE.IMAGE_SAS360CON:
                        {
                            try
                            {
                                #region RTC VALUE UTC

                                ushort[] rtc_utc_value = new ushort[] { 24500, 32014 };
                                byte[] byte_rtc_utc_1 = BitConverter.GetBytes(rtc_utc_value[1]);
                                byte[] byte_rtc_utc_2 = BitConverter.GetBytes(rtc_utc_value[0]);
                                byte[] rtc_utc = new byte[4];
                                Array.Copy(byte_rtc_utc_1, 0, rtc_utc, 0, 2);
                                Array.Copy(byte_rtc_utc_2, 0, rtc_utc, 2, 2);
                                uint u32_rtc_utc = BitConverter.ToUInt32(rtc_utc, 0);
                                DateTime date_rtc_utc = Constants.date_ref.Date.AddSeconds(u32_rtc_utc);
                                Globals.GetTheInstance().Image_sas360con_general.RTC_UTC = date_rtc_utc;
                                Globals.GetTheInstance().List_image_sas360con.First(config => config.Addr == (double)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON).Value = u32_rtc_utc;

                                #endregion

                                Globals.GetTheInstance().List_image_sas360con.First(config => config.Addr == (double)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 2).Value = 127654; //Milliseconds

                                #region ESTADOS BOOLEANOS

                                //STATES
                                byte sas360_state = (byte)random.Next(1, 7);
                                byte sas360_substate = (byte)random.Next(1, 3);
                                byte[] byte_states = new byte[2] { sas360_state, sas360_substate };
                                Globals.GetTheInstance().Image_sas360con_general.Sas360_state = Enum.IsDefined(typeof(SAS360CON_STATE), sas360_state) ? (SAS360CON_STATE)sas360_state : SAS360CON_STATE.UNDEFINED;
                                Globals.GetTheInstance().Image_sas360con_general.Sas360_subtate = Enum.IsDefined(typeof(SAS360CON_SUBSTATE), sas360_substate) ? (SAS360CON_SUBSTATE)sas360_substate : SAS360CON_SUBSTATE.UNDEFINED;

                                Globals.GetTheInstance().List_image_sas360con.First(config => config.Addr == (double)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 3).Value = sas360_state;
                                Globals.GetTheInstance().List_image_sas360con.First(config => config.Addr == (double)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 3.5).Value = sas360_substate;


                                //DIGITAL STATES
                                int start_pos_digital = (int)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 6;
                                int end_pos_digital = (int)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 13;
                                int index_in_list = 0;
                                for (int index_pos = start_pos_digital; index_pos <= end_pos_digital; index_pos++)
                                {
                                    ushort state_value = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().Image_sas360con_general.Array_digital_states[index_in_list++] = state_value;
                                    Globals.GetTheInstance().List_image_sas360con.First(config => config.Addr == index_pos).Value = state_value;
                                }

                                #endregion

                                #region Forced outputs

                                #endregion

                                #region Entradas analógicas

                                Globals.GetTheInstance().Image_sas360con_general.EA_4v1_power = (ushort) random.Next(0, ushort.MaxValue);
                                Globals.GetTheInstance().Image_sas360con_general.EA_shunt_leds = (ushort)random.Next(0, ushort.MaxValue);

                                Globals.GetTheInstance().List_image_sas360con.First(config => config.Addr == (double)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 22).Value = Globals.GetTheInstance().Image_sas360con_general.EA_4v1_power;
                                Globals.GetTheInstance().List_image_sas360con.First(config => config.Addr == (double)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 23).Value = Globals.GetTheInstance().Image_sas360con_general.EA_shunt_leds;

                                #endregion

                                #region Registros de autodiagnóstico

                                Globals.GetTheInstance().Image_sas360con_general.Autotest_bit_check_codif = (ushort)random.Next(0, ushort.MaxValue);
                                Globals.GetTheInstance().List_image_sas360con.First(config => config.Addr == (double)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 26).Value = Globals.GetTheInstance().Image_sas360con_general.Autotest_bit_check_codif;

                                Globals.GetTheInstance().Image_sas360con_general.Autotest_ea_4v1_power_value_mv = (ushort)random.Next(0, ushort.MaxValue);
                                Globals.GetTheInstance().List_image_sas360con.First(config => config.Addr == (double)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 27).Value = Globals.GetTheInstance().Image_sas360con_general.Autotest_ea_4v1_power_value_mv;

                                Globals.GetTheInstance().Image_sas360con_general.Reset_cause_codif = (ushort)random.Next(0, ushort.MaxValue);
                                Globals.GetTheInstance().List_image_sas360con.First(config => config.Addr == (double)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 28).Value = Globals.GetTheInstance().Image_sas360con_general.Reset_cause_codif;

                                #endregion


                                #region PROCESSING TASK

                                int index_processing_tak = Globals.GetTheInstance().List_image_sas360con.FindIndex(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 30);
                                for (int index = 0; index < Constants.PROCESSING_TASK_COUNT; index++)
                                {
                                    Globals.GetTheInstance().Image_sas360con_general.Array_processing_task_msec[index] = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_image_sas360con[index_processing_tak].Value = Globals.GetTheInstance().Image_sas360con_general.Array_processing_task_msec[index];

                                    index_processing_tak++;
                                }

                                #endregion

                                #region INTEGRITY MANAGEMENT

                                Globals.GetTheInstance().Image_sas360con_integrity_management.Lms_watchdog = (ushort)random.Next(0, ushort.MaxValue);
                                Globals.GetTheInstance().List_image_sas360con.First(config => config.Addr == (double)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 36).Value = Globals.GetTheInstance().Image_sas360con_integrity_management.Lms_watchdog;

                                Globals.GetTheInstance().Image_sas360con_integrity_management.Internal_change_counter = (ushort)random.Next(0, ushort.MaxValue);
                                Globals.GetTheInstance().List_image_sas360con.First(config => config.Addr == (double)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 37).Value = Globals.GetTheInstance().Image_sas360con_integrity_management.Internal_change_counter;

                                Globals.GetTheInstance().Image_sas360con_integrity_management.Config_con_change_counter = (ushort)random.Next(0, ushort.MaxValue);
                                Globals.GetTheInstance().List_image_sas360con.First(config => config.Addr == (double)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 38).Value = Globals.GetTheInstance().Image_sas360con_integrity_management.Config_con_change_counter;

                                Globals.GetTheInstance().Image_sas360con_integrity_management.Config_iot_change_counter = (ushort)random.Next(0, ushort.MaxValue);
                                Globals.GetTheInstance().List_image_sas360con.First(config => config.Addr == (double)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 39).Value = Globals.GetTheInstance().Image_sas360con_integrity_management.Config_iot_change_counter;

                                Globals.GetTheInstance().Image_sas360con_integrity_management.Nvreg_change_counter = (ushort)random.Next(0, ushort.MaxValue);
                                Globals.GetTheInstance().List_image_sas360con.First(config => config.Addr == (double)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 40).Value = Globals.GetTheInstance().Image_sas360con_integrity_management.Nvreg_change_counter;

                                Globals.GetTheInstance().Image_sas360con_integrity_management.Recorded_event_number = (ushort)random.Next(0, 30);
                                Globals.GetTheInstance().List_image_sas360con.First(config => config.Addr == (double)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 41).Value = Globals.GetTheInstance().Image_sas360con_integrity_management.Recorded_event_number;

                                Globals.GetTheInstance().Image_sas360con_integrity_management.Recorded_historic_number = (uint)random.Next(0, 30);
                                Globals.GetTheInstance().List_image_sas360con.First(config => config.Addr == (double)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 42).Value = Globals.GetTheInstance().Image_sas360con_integrity_management.Recorded_historic_number;

                                Globals.GetTheInstance().Image_sas360con_integrity_management.Last_event_id = (ushort)random.Next(0, ushort.MaxValue);
                                Globals.GetTheInstance().List_image_sas360con.First(config => config.Addr == (double)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 42).Value = Globals.GetTheInstance().Image_sas360con_integrity_management.Last_event_id;

                                #endregion

                                #region MAIN MANAGEMENT

                                int start_pos = (int)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 50;
                                int end_pos = (int)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 57;
                                for (int index_pos = start_pos; index_pos <= end_pos; index_pos++)
                                {
                                    Globals.GetTheInstance().List_image_sas360con.First(config => config.Addr == index_pos).Value = (ushort)random.Next(0, ushort.MaxValue);
                                }

                                Globals.GetTheInstance().Image_sas360con_main_management.Internal_error = Globals.GetTheInstance().List_image_sas360con.First(config => config.Addr == (double)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 50).Value;

                                #endregion

                                #region LIN POOLING

                                Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_config_codif = (ushort)random.Next(0, ushort.MaxValue);
                                Globals.GetTheInstance().List_image_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 58).Value = Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_config_codif;

                                Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_state = (ushort)random.Next(0, ushort.MaxValue);
                                Globals.GetTheInstance().List_image_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 59).Value = Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_state;

                                Globals.GetTheInstance().Image_sas360con_lin_pooling.Actual_pooled_in = (byte)random.Next(1, 3);
                                Globals.GetTheInstance().List_image_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 60).Value = Globals.GetTheInstance().Image_sas360con_lin_pooling.Actual_pooled_in;

                                Globals.GetTheInstance().Image_sas360con_lin_pooling.Actual_pooling_request = (byte)random.Next(1, 4);
                                Globals.GetTheInstance().List_image_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 60.5).Value = Globals.GetTheInstance().Image_sas360con_lin_pooling.Actual_pooling_request;

                                Globals.GetTheInstance().Image_sas360con_lin_pooling.Total_lin_poolin_time = (ushort)random.Next(0, ushort.MaxValue);
                                Globals.GetTheInstance().List_image_sas360con.First(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 78).Value = Globals.GetTheInstance().Image_sas360con_lin_pooling.Total_lin_poolin_time;


                                int list_pos_com_total = Globals.GetTheInstance().List_image_sas360con.FindIndex(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 62);
                                int list_pos_com_error = Globals.GetTheInstance().List_image_sas360con.FindIndex(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 70);
                                int list_pos_total_cycle = Globals.GetTheInstance().List_image_sas360con.FindIndex(config => config.Addr == (int)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 74);
                                for (int index_lin = 0; index_lin < Constants.LIN_TOTAL_COUNT; index_lin++)
                                {
                                    Globals.GetTheInstance().Image_sas360con_lin_pooling.Array_lin_com_total_counter[index_lin] = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().Array_sas360_uwb[index_lin].Lin_comm_total = Globals.GetTheInstance().Image_sas360con_lin_pooling.Array_lin_com_total_counter[index_lin];
                                    Globals.GetTheInstance().List_image_sas360con[list_pos_com_total].Value = Globals.GetTheInstance().Image_sas360con_lin_pooling.Array_lin_com_total_counter[index_lin];
                                    list_pos_com_total++;

                                    Globals.GetTheInstance().Image_sas360con_lin_pooling.Array_lin_com_error_counter[index_lin] = (byte)random.Next(0, byte.MaxValue);
                                    Globals.GetTheInstance().Array_sas360_uwb[index_lin].Lin_error_total = Globals.GetTheInstance().Image_sas360con_lin_pooling.Array_lin_com_error_counter[index_lin];
                                    Globals.GetTheInstance().List_image_sas360con[list_pos_com_error].Value = Globals.GetTheInstance().Image_sas360con_lin_pooling.Array_lin_com_error_counter[index_lin];
                                    list_pos_com_error++;

                                    Globals.GetTheInstance().Image_sas360con_lin_pooling.Array_lin_total_cycle_time[index_lin] = (byte)random.Next(0, byte.MaxValue);
                                    Globals.GetTheInstance().Array_sas360_uwb[index_lin].Cycle_time = Globals.GetTheInstance().Image_sas360con_lin_pooling.Array_lin_total_cycle_time[index_lin];
                                    Globals.GetTheInstance().List_image_sas360con[list_pos_total_cycle].Value = Globals.GetTheInstance().Image_sas360con_lin_pooling.Array_lin_total_cycle_time[index_lin];
                                    list_pos_total_cycle++;
                                }


                                #region Assigned ID

                                for (int index = 0; index < Constants.ID_SIZE; index++)
                                {
                                    Globals.GetTheInstance().Image_sas360con_lin_pooling.Array_assigned_self_contag_id[index] = (byte)random.Next(0, byte.MaxValue);
                                    Globals.GetTheInstance().Image_sas360con_lin_pooling.Array_assigned_self_drvtag_id[index] = (byte)random.Next(0, byte.MaxValue);
                                }
                                int assigned_pos_in_list = Globals.GetTheInstance().List_image_sas360con
                                    .Select((Value, Index) => new { Value, Index }).ToList()
                                    .First(config => config.Value.Addr == (int)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 80).Index;

                                Globals.GetTheInstance().Image_sas360con_lin_pooling.Array_assigned_self_contag_id.ToList().ForEach(contag_id => {
                                    Globals.GetTheInstance().List_image_sas360con[assigned_pos_in_list].Value = contag_id;
                                    assigned_pos_in_list++;
                                });
                                Globals.GetTheInstance().Image_sas360con_lin_pooling.Array_assigned_self_drvtag_id.ToList().ForEach(drvtag_id => {
                                    Globals.GetTheInstance().List_image_sas360con[assigned_pos_in_list].Value = drvtag_id;
                                    assigned_pos_in_list++;
                                });

                                #endregion


                                #endregion

                                #region FIELD POSITION

                                int field_pos_in_list = Globals.GetTheInstance().List_image_sas360con
                                    .Select((Value, Index) => new { Value, Index }).ToList()
                                    .First(config => config.Value.Addr == (int)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 88).Index;

                                Globals.GetTheInstance().Image_sas360con_field_position.Installation_pos_x = (uint)random.Next(0, ushort.MaxValue);
                                Globals.GetTheInstance().List_image_sas360con[field_pos_in_list].Value = Globals.GetTheInstance().Image_sas360con_field_position.Installation_pos_x;
                                field_pos_in_list++;

                                Globals.GetTheInstance().Image_sas360con_field_position.Installation_pos_y = (uint)random.Next(0, ushort.MaxValue);
                                Globals.GetTheInstance().List_image_sas360con[field_pos_in_list].Value = Globals.GetTheInstance().Image_sas360con_field_position.Installation_pos_y;
                                field_pos_in_list++;

                                Globals.GetTheInstance().Image_sas360con_field_position.Latitud = random.NextDouble() * 100;
                                Globals.GetTheInstance().List_image_sas360con[field_pos_in_list].Value = Globals.GetTheInstance().Image_sas360con_field_position.Latitud;
                                field_pos_in_list++;

                                Globals.GetTheInstance().Image_sas360con_field_position.Longitud = random.NextDouble() * 100;
                                Globals.GetTheInstance().List_image_sas360con[field_pos_in_list].Value = Globals.GetTheInstance().Image_sas360con_field_position.Longitud;
                                field_pos_in_list++;

                                Globals.GetTheInstance().Image_sas360con_field_position.Used_zone_id = (uint)random.Next(0, int.MaxValue);
                                Globals.GetTheInstance().List_image_sas360con[field_pos_in_list].Value = Globals.GetTheInstance().Image_sas360con_field_position.Used_zone_id;

                                #endregion

                                #region UWB

                                int index_pos_uwb = (int)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 100;

                                for (int index_lin = 0; index_lin < Constants.LIN_TOTAL_COUNT; index_lin++)
                                {
                                    Globals.GetTheInstance().Image_sas360con_uwb.UWB_codif_state[index_lin] = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().Array_sas360_uwb[index_lin].Codif_state = Globals.GetTheInstance().Image_sas360con_uwb.UWB_codif_state[index_lin];

                                    Globals.GetTheInstance().Image_sas360con_uwb.UWB_number_tags_detected[index_lin] = (byte)random.Next(0, Constants.MAX_DETECTED_TAGS);
                                    Globals.GetTheInstance().Array_sas360_uwb[index_lin].Num_tags = Globals.GetTheInstance().Image_sas360con_uwb.UWB_number_tags_detected[index_lin];

                                    Globals.GetTheInstance().Image_sas360con_uwb.UWB_number_zones_detected[index_lin] = (byte)random.Next(0, Constants.MAX_DETECTED_ZONES);
                                    Globals.GetTheInstance().Array_sas360_uwb[index_lin].Num_zones = Globals.GetTheInstance().Image_sas360con_uwb.UWB_number_zones_detected[index_lin];
                                }

                                int index_pos_uwb_in_list = Globals.GetTheInstance().List_image_sas360con.FindIndex(config => config.Addr == (double)MEMORY_MAP_READ.SAS360CON_IMAGEN_SAS360CON + 116);

                                Globals.GetTheInstance().Image_sas360con_uwb.Total_tags_detected = (byte)random.Next(0, Constants.MAX_DETECTED_TAGS);
                                Globals.GetTheInstance().List_image_sas360con[index_pos_uwb_in_list].Value = Globals.GetTheInstance().Image_sas360con_uwb.Total_tags_detected;
                                index_pos_uwb_in_list++;

                                Globals.GetTheInstance().Image_sas360con_uwb.Total_zones_detected = (byte)random.Next(0, Constants.MAX_DETECTED_ZONES);
                                Globals.GetTheInstance().List_image_sas360con[index_pos_uwb_in_list].Value = Globals.GetTheInstance().Image_sas360con_uwb.Total_zones_detected;
                                index_pos_uwb_in_list++;

                                Globals.GetTheInstance().Image_sas360con_uwb.Total_tags_area_detection = (byte)random.Next(0, Constants.MAX_DETECTED_TAGS);
                                Globals.GetTheInstance().List_image_sas360con[index_pos_uwb_in_list].Value = Globals.GetTheInstance().Image_sas360con_uwb.Total_tags_area_detection;
                                index_pos_uwb_in_list++;

                                Globals.GetTheInstance().Image_sas360con_uwb.Total_tags_area_yellow = (byte)random.Next(0, Constants.MAX_DETECTED_TAGS);
                                Globals.GetTheInstance().List_image_sas360con[index_pos_uwb_in_list].Value = Globals.GetTheInstance().Image_sas360con_uwb.Total_tags_area_yellow;
                                index_pos_uwb_in_list++;

                                Globals.GetTheInstance().Image_sas360con_uwb.Total_tags_area_orange = (byte)random.Next(0, Constants.MAX_DETECTED_TAGS);
                                Globals.GetTheInstance().List_image_sas360con[index_pos_uwb_in_list].Value = Globals.GetTheInstance().Image_sas360con_uwb.Total_tags_area_orange;
                                index_pos_uwb_in_list++;

                                Globals.GetTheInstance().Image_sas360con_uwb.Total_tags_area_red = (byte)random.Next(0, Constants.MAX_DETECTED_TAGS);
                                Globals.GetTheInstance().List_image_sas360con[index_pos_uwb_in_list].Value = Globals.GetTheInstance().Image_sas360con_uwb.Total_tags_area_red;
                                index_pos_uwb_in_list++;

                                Globals.GetTheInstance().Image_sas360con_uwb.Total_tags_area_slow = (byte)random.Next(0, Constants.MAX_DETECTED_TAGS);
                                Globals.GetTheInstance().List_image_sas360con[index_pos_uwb_in_list].Value = Globals.GetTheInstance().Image_sas360con_uwb.Total_tags_area_slow;
                                index_pos_uwb_in_list++;

                                Globals.GetTheInstance().Image_sas360con_uwb.Total_tags_area_charge = (byte)random.Next(0, Constants.MAX_DETECTED_TAGS);
                                Globals.GetTheInstance().List_image_sas360con[index_pos_uwb_in_list].Value = Globals.GetTheInstance().Image_sas360con_uwb.Total_tags_area_charge;
                                index_pos_uwb_in_list++;


                                int index_pos_codif_state = Globals.GetTheInstance().List_image_sas360con.FindIndex(config => config.Addr == index_pos_uwb);
                                for (int index_lin = 0; index_lin < Constants.LIN_TOTAL_COUNT; index_lin++)
                                {
                                    Globals.GetTheInstance().List_image_sas360con[index_pos_codif_state].Value = Globals.GetTheInstance().Image_sas360con_uwb.UWB_codif_state[index_lin];
                                    index_pos_codif_state++;
                                }
                                index_pos_uwb += Constants.LIN_TOTAL_COUNT;

                                int index_pos_tags = Globals.GetTheInstance().List_image_sas360con.FindIndex(config => config.Addr == index_pos_uwb);
                                for (int index_lin = 0; index_lin < Constants.LIN_TOTAL_COUNT; index_lin++)
                                {
                                    Globals.GetTheInstance().List_image_sas360con[index_pos_tags].Value = Globals.GetTheInstance().Image_sas360con_uwb.UWB_number_tags_detected[index_lin];
                                    index_pos_tags++;
                                }
                                index_pos_uwb += Constants.LIN_TOTAL_COUNT / 2;

                                int index_pos_zones = Globals.GetTheInstance().List_image_sas360con.FindIndex(config => config.Addr == index_pos_uwb);
                                for (int index_lin = 0; index_lin < Constants.LIN_TOTAL_COUNT; index_lin++)
                                {
                                    Globals.GetTheInstance().List_image_sas360con[index_pos_zones].Value = Globals.GetTheInstance().Image_sas360con_uwb.UWB_number_zones_detected[index_lin];
                                    index_pos_zones++;
                                }
                                index_pos_uwb += Constants.LIN_TOTAL_COUNT / 2;

                                #endregion
                            }
                            catch (Exception ex)
                            {
                                Manage_logs.SaveErrorValue($"{typeof(Manage_file).Name} -> {nameof(Simulator_data)} -> {MEMORY_CONFIG_TYPE.IMAGE_SAS360CON} ->  {ex.Message}");
                            }

                            break;
                        }

                    case MEMORY_CONFIG_TYPE.IMAGE_IOT:
                        {
                            break;
                        }

                    case MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE:
                        {
                            try
                            {
                                Random r = new();

                                int list_pos = Globals.GetTheInstance().List_console_closest_tags_base.FindIndex(memory_tags => memory_tags.Addr == (int)MEMORY_MAP_READ.SAS360CON_TAG_12_CLOSEST_BASE_CON);
                                for (int index_tag = 0; index_tag < Globals.GetTheInstance().Total_closest_tags; index_tag++)
                                {
                                    ushort tag_id_2lsb = (ushort)((int)MEMORY_MAP_READ.SAS360CON_TAG_12_CLOSEST_BASE_CON + (index_tag * Constants.TAGS_BASE_CON_STRUCT_NUM_REG));

                                    int identifier_zone = r.Next(0x00, 0x04);
                                    int tag_type = r.Next(0, 4);
                                    int tag_id_3byte = (identifier_zone << 4) ^ tag_type;

                                    byte fw_ver = (byte)r.Next(0, byte.MaxValue);
                                    byte[] array_values_1 = new byte[] { (byte)tag_id_3byte, fw_ver };

                                    ushort codif_state = (ushort)r.Next(0, ushort.MaxValue);
                                    short pos_x_cm = (short)r.Next(-600, 600);
                                    short pos_y_cm = (short)r.Next(-600, 600);
                                    ushort uwb_command_codif = (ushort)r.Next(0, ushort.MaxValue);

                                    byte battery_level = (byte)r.Next(0, 4);
                                    byte console_inst_alarm_code = (byte)r.Next(0, byte.MaxValue);
                                    byte[] array_values_2 = new byte[] { battery_level, console_inst_alarm_code };

                                    ushort reported_reg = (ushort)r.Next(0, ushort.MaxValue);

                                    byte sector_led = (byte)random.Next(1, 30);
                                    byte calc_area = (byte)random.Next(1, 30);
                                    byte[] array_values_3 = new byte[] { sector_led, calc_area };

                                    ushort calc_varios = 0;
                                    ushort reserva = 0;


                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = tag_id_2lsb;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = tag_id_3byte;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = fw_ver;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = codif_state;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = pos_x_cm;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = pos_y_cm;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = uwb_command_codif;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = battery_level;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = console_inst_alarm_code;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = reported_reg;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = sector_led;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = calc_area;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = calc_varios;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = reserva;

                                    Sas360_tag sas360_tag = Globals.GetTheInstance().Array_sas360_tag[index_tag];
                                    sas360_tag.ID_2LSB = tag_id_2lsb;

                                    sas360_tag.Tag_type_value = (byte) tag_type;
                                    sas360_tag.Tag_type = Enum.IsDefined(typeof(SAS360TAG_ZONE_TYPE), tag_type) ? (SAS360TAG_ZONE_TYPE)tag_type : SAS360TAG_ZONE_TYPE.UNKNOWN;

                                    sas360_tag.FW_version_value = fw_ver;
                                    sas360_tag.FW_version = string.Empty;
                                    for (int index_fw = 0; index_fw < fw_ver.ToString().Length; index_fw++) {
                                        sas360_tag.FW_version += fw_ver.ToString().Substring(index_fw, 1);
                                        sas360_tag.FW_version += ".";
                                    }

                                    sas360_tag.Codif_state = codif_state;
                                    sas360_tag.Pos_x = pos_x_cm;
                                    sas360_tag.Pos_y = pos_y_cm;
                                    sas360_tag.UWB_command_codif = uwb_command_codif;
                                    sas360_tag.Battery_level = (byte)battery_level;
                                    sas360_tag.Reported_register = reported_reg;
                                    sas360_tag.Calc_sector_leds = sector_led;
                                    sas360_tag.Calc_area = calc_area;
                                }
                            }
                            catch (Exception ex)
                            {
                                Manage_logs.SaveErrorValue($"{typeof(Manage_file).Name} -> {nameof(Simulator_data)} -> {MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE} -> {ex.Message}");
                            }

                            break;
                        }

                    case MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_EXTENDED:
                        {
                            break;
                        }

                    case MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_BASE:
                        {
                            break;
                        }

                    case MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_EXTENDED:
                        {
                            break;
                        }


                    case MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_BASE:
                        {
                            try
                            {
                                Random r = new();

                                int list_pos = Globals.GetTheInstance().List_console_closest_zone_base.FindIndex(memory_tags => memory_tags.Addr == (int)MEMORY_MAP_READ.SAS360CON_ZONE_16_CLOSEST_BASE_CON);
                                for (int index_zone = 0; index_zone < Globals.GetTheInstance().Total_closest_zone; index_zone++)
                                {
                                    ushort zone_id_2lsb = (ushort)((int)MEMORY_MAP_READ.SAS360CON_ZONE_16_CLOSEST_BASE_CON + (index_zone * Constants.ZONE_BASE_CON_STRUCT_NUM_REG));

                                    int identifier_zone = r.Next(0, 7);
                                    int zone_type = r.Next(0x05, 0x0A);
                                    int zone_id_3byte = (identifier_zone << 4) ^ zone_type;

                                    byte fw_ver = (byte)r.Next(0, byte.MaxValue);
                                    byte[] array_values_1 = new byte[] { (byte)zone_id_3byte, fw_ver };

                                    ushort codif_state = (ushort)r.Next(0, ushort.MaxValue);
                                    short pos_x_cm = (short)r.Next(-600, 600);
                                    short pos_y_cm = (short)r.Next(-600, 600);

                                    ushort radio_action = (ushort)r.Next(40, 100);

                                    ushort cfg_pos_abs_x = (ushort)r.Next(0, ushort.MaxValue);
                                    ushort cfg_pos_abs_y = (ushort)r.Next(0, ushort.MaxValue);


                                    Globals.GetTheInstance().List_console_closest_zone_base[list_pos++].Value = zone_id_2lsb;
                                    Globals.GetTheInstance().List_console_closest_zone_base[list_pos++].Value = zone_id_3byte;
                                    Globals.GetTheInstance().List_console_closest_zone_base[list_pos++].Value = fw_ver;
                                    Globals.GetTheInstance().List_console_closest_zone_base[list_pos++].Value = codif_state;
                                    Globals.GetTheInstance().List_console_closest_zone_base[list_pos++].Value = pos_x_cm;
                                    Globals.GetTheInstance().List_console_closest_zone_base[list_pos++].Value = pos_y_cm;
                                    Globals.GetTheInstance().List_console_closest_zone_base[list_pos++].Value = radio_action;
                                    Globals.GetTheInstance().List_console_closest_zone_base[list_pos++].Value = cfg_pos_abs_x;
                                    Globals.GetTheInstance().List_console_closest_zone_base[list_pos++].Value = cfg_pos_abs_y;

                                    Sas360_zone sas360_zone = Globals.GetTheInstance().Array_sas360_zone[index_zone];
                                    sas360_zone.ID_2LSB = zone_id_2lsb;

                                    sas360_zone.Zone_type_value = (byte)zone_type;
                                    sas360_zone.Zone_type = Enum.IsDefined(typeof(SAS360TAG_ZONE_TYPE), zone_type) ? (SAS360TAG_ZONE_TYPE)zone_type : SAS360TAG_ZONE_TYPE.UNKNOWN;

                                    sas360_zone.FW_version_value = fw_ver;
                                    sas360_zone.FW_version = string.Empty;
                                    for (int index_fw = 0; index_fw < fw_ver.ToString().Length; index_fw++)
                                    {
                                        sas360_zone.FW_version += fw_ver.ToString().Substring(index_fw, 1);
                                        sas360_zone.FW_version += ".";
                                    }

                                    sas360_zone.Codif_state = codif_state;

                                    sas360_zone.Pos_x = pos_x_cm;
                                    sas360_zone.Pos_y = pos_y_cm;

                                    sas360_zone.Radio_action = radio_action;

                                    sas360_zone.Zone_cfg_pos_abs_x = cfg_pos_abs_x;
                                    sas360_zone.Zone_cfg_pos_abs_y = cfg_pos_abs_y;
                                }
                            }
                            catch (Exception ex)
                            {
                                Manage_logs.SaveErrorValue($"{typeof(Manage_file).Name} -> {nameof(Simulator_data)} -> {MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_BASE} -> {ex.Message}");
                            }


                            break;
                        }

                    case MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_EXTENDED:
                        {
                            break;
                        }

                    case MEMORY_CONFIG_TYPE.NVREG:
                        {
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_file).Name} -> {nameof(Simulator_data)} -> {memory_config_type} -> {ex.Message}");
            }
        }






        public static string SAS360CON_modbus_speed(ushort value)
        {
            string s_modbus =
                value == (ushort)MASTER_SPEED._9600 ? "9600" :
                value == (ushort)MASTER_SPEED._19200 ? "19200" :
                value == (ushort)MASTER_SPEED._38400 ? "38400" :
                value == (ushort)MASTER_SPEED._57600 ? "57600" :
                value == (ushort)MASTER_SPEED._11520 ? "115200" : "9600";

            return s_modbus;
        }


        public static string SAS360CON_state_codif(SAS360CON_STATE con_state)
        {
            string s_status =
               con_state == SAS360CON_STATE.STATE_INIT_AUTODIAG_SAS360CON_U8 ? "INI AUTODIAG" :
               con_state == SAS360CON_STATE.STATE_INIT_GESTION_DE_ACTUALIZACIONES_U8 ? "INI UPDATE" :
               con_state == SAS360CON_STATE.STATE_INIT_VERIFICACIÓN_LIN_U8 ? "INI CHECK LIN" :
               con_state == SAS360CON_STATE.STATE_INIT_AUTODET_SELF_CONTAG_U8 ? "INI AUTODETECT" :
               con_state == SAS360CON_STATE.STATE_INIT_END_U8 ? "INI END" :
               con_state == SAS360CON_STATE.STATE_STANDARD_DETECTION_U8 ? "STANDARD DETECT" :
               con_state == SAS360CON_STATE.STATE_LOW_POWER_MODE_U8 ? "LOW POWER" :
               con_state == SAS360CON_STATE.STATE_LOW_INTERNAL_ERROR_U8 ? "LOW INTERNA ERROR" : "UNDEFINED";

            return s_status;
        }


        public static string SAS360TAG_ZONE_type(SAS360TAG_ZONE_TYPE tag_type)
        {
            string s_type =
               tag_type == SAS360TAG_ZONE_TYPE.SAS360TAG_PED ? "PEATON" :
               tag_type == SAS360TAG_ZONE_TYPE.SAS360TAG_DRV ? "CONDUCTOR" :
               tag_type == SAS360TAG_ZONE_TYPE.SAS360CON_TAG_LV ? "CON TAG LV" :
               tag_type == SAS360TAG_ZONE_TYPE.SAS360CON_TAG_HV ? "CON TAG HV" :
               tag_type == SAS360TAG_ZONE_TYPE.SAS360ZONE_CIRC_R_SLOW ? "ZONE CIRCLE R SLOW" :
               tag_type == SAS360TAG_ZONE_TYPE.SAS360ZONE_REC_P1_SLOW ? "ZONE RECT P1 SLOW" :
               tag_type == SAS360TAG_ZONE_TYPE.SAS360ZONE_REC_P2_SLOW ? "ZONE RECT P2 SLOW" :
               tag_type == SAS360TAG_ZONE_TYPE.SAS360ZONE_REC_P3_SLOW ? "ZONE RECT P3 SLOW" :
               tag_type == SAS360TAG_ZONE_TYPE.SAS360ZONE_REC_P4_SLOW ? "ZONE RECT P4 SLOW" :
               tag_type == SAS360TAG_ZONE_TYPE.SAS360ZONE_INHIBIT_RAD ? "ZONE INHIBIT RAD" :
               "UNKNOWN";

            return s_type;
        }




        public static string SAS360CON_input()
        {
            string s_input = string.Empty;

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)MASK_CODIF_DI1.M_DI_DEBUG_SWITCH))
                s_input += "DEBUG SWITCH\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)MASK_CODIF_DI1.M_DI_RESET_SWITCH))
                s_input += "RESET SWITCH\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)MASK_CODIF_DI1.M_DI_MOD_POWER_SAVE))
                s_input += "MOD POWER SAVE\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)MASK_CODIF_DI1.M_DI_MOD_STATUS))
                s_input += "MOD STATUS\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)MASK_CODIF_DI1.M_DI_MOD_RI))
                s_input += "MOD RI\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)MASK_CODIF_DI1.M_DI_VER_HW_0))
                s_input += "VER HW 1\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)MASK_CODIF_DI1.M_DI_VER_HW_1))
                s_input += "VER HW 2\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)MASK_CODIF_DI1.M_DI_UWB_INT))
                s_input += "UWB INT\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)MASK_CODIF_DI1.DI_ACCEL_INT1))
                s_input += "ACCEL INT1\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)MASK_CODIF_DI1.DI_ACCEL_INT2))
                s_input += "ACCEL INT2\r\n";


            if (s_input != string.Empty)
                s_input = s_input[..^2];

            return s_input;
        }


        public static string SAS360CON_output_int()
        {
            string s_output = string.Empty;

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO1.M_DO_EN_REG_4V1))
                s_output += "M_DO_EN_REG_4V1 \r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO1.M_DO_EN_DF_SWITCH))
                s_output += "M_DO_EN_DF_SWITCH \r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO1.M_DO_EN_CONNBOARD))
                s_output += "M_DO_EN_CONNBOARD \r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO1.M_DO_EN_LIN_1))
                s_output += "M_DO_EN_LIN_1 \r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO1.M_DO_EN_LIN_2))
                s_output += "M_DO_EN_LIN_2 \r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO1.M_DO_EN_LIN_3))
                s_output += "M_DO_EN_LIN_3 \r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO1.M_DO_DEBUG_LED1))
                s_output += "M_DO_DEBUG_LED1 \r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO1.M_DO_EN_LED_DRIVER))
                s_output += "M_DO_EN_LED_DRIVER \r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO1.M_DO_MOD_RESET))
                s_output += "M_DO_MOD_RESET \r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO1.M_DO_MOD_POWER_KEY))
                s_output += "M_DO_MOD_POWER_KEY \r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO1.M_DO_MOD_DTR))
                s_output += "M_DO_MOD_DTR \r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO1.M_DO_ACCEL_WAKE_UP))
                s_output += "M_DO_ACCEL_WAKE_UP \r\n";


            if (s_output != string.Empty)
                s_output = s_output[..^2];

            return s_output;
        }


        public static string SAS360CON_output_ext()
        {
            string s_output = string.Empty;

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO2.M_DO_RELE_1))
                s_output += "M_DO_RELE_1\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO2.M_DO_RELE_2))
                s_output += "M_DO_RELE_2\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO2.M_DO_RELE_3))
                s_output += "M_DO_RELE_3\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO2.M_DO_RELE_4))
                s_output += "M_DO_RELE_4\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO2.M_DO_TRANSISTOR_1))
                s_output += "M_DO_TRANSISTOR_1\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO2.M_DO_TRANSISTOR_2))
                s_output += "M_DO_TRANSISTOR_2\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO2.M_DO_EN_12VOUT))
                s_output += "M_DO_EN_12VOUT\r\n";


            if (s_output != string.Empty)
                s_output = s_output[..^2];

            return s_output;
        }

        public static string SAS360CON_output_led()
        {
            string s_output = string.Empty;

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO3.M_DO_LED_P2_A))
                s_output += "M_DO_LED_P2_A\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO3.M_DO_LED_P7_A))
                s_output += "M_DO_LED_P7_A\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO3.M_DO_LED_P8_A))
                s_output += "M_DO_LED_P8_A\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO3.M_DO_LED_P9_A))
                s_output += "M_DO_LED_P9_A\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO3.M_DO_LED_P10_A))
                s_output += "M_DO_LED_P10_A\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO3.M_DO_LED_P11_A))
                s_output += "M_DO_LED_P11_A\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO3.M_DO_LED_P1_K))
                s_output += "M_DO_LED_P1_K\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO3.M_DO_LED_P3_K))
                s_output += "M_DO_LED_P3_K\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO3.M_DO_LED_P4_K))
                s_output += "M_DO_LED_P4_K\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO3.M_DO_LED_P5_K))
                s_output += "M_DO_LED_P5_K\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)FORCE_MASK_DO3.M_DO_LED_P6_K))
                s_output += "M_DO_LED_P6_K\r\n";


            if (s_output != string.Empty)
                s_output = s_output[..^2];

            return s_output;
        }


        public static string SAS360CON_internal_error()
        {
            string s_internal_error = string.Empty;

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)MASK_INTERNAL_ERROR.SAS360CON_ERROR))
                s_internal_error += "SAS360CON ERROR\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)MASK_INTERNAL_ERROR.INIT_SELF_CONTAG_DETECTION))
                s_internal_error += "INIT SELF CONTAG DETECTION\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)MASK_INTERNAL_ERROR.INIT_SAS36OUWB_DETECTION))
                s_internal_error += "INIT SAS36OUWB DETECTION\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)MASK_INTERNAL_ERROR.FORCED_DIGITAL_OUTPUTS))
                s_internal_error += "FORCED DIGITAL OUTPUTS\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)MASK_INTERNAL_ERROR.FORCED_LEDS))
                s_internal_error += "FORCED LEDS\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)MASK_INTERNAL_ERROR.FORCED_AUDIOS))
                s_internal_error += "FORCED AUDIOS\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)MASK_INTERNAL_ERROR.OPE_SELF_CONTAG_DETECTION))
                s_internal_error += "OPE SELF CONTAG DETECTION\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)MASK_INTERNAL_ERROR.OPE_SAS36OUWB_DETECTION))
                s_internal_error += "OPE SAS36OUWB DETECTION\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_main_management.Internal_error, (int)MASK_INTERNAL_ERROR.OPE_TAG_CONFIG_MODE))
                s_internal_error += "OPE TAG CONFIG MODE\r\n";


            if (s_internal_error != string.Empty)
                s_internal_error = s_internal_error[..^2];

            return s_internal_error;
        }

        public static string SAS360CON_lin_pooling_config()
        {

            string s_lin_pool = string.Empty;

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_config_codif, (int)MASK_LIN_POOLING_CONFIG.POOL_LIN1_ENABLED))
                s_lin_pool += "POOL LIN1 ENABLED\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_config_codif, (int)MASK_LIN_POOLING_CONFIG.POOL_LIN2_ENABLED))
                s_lin_pool += "POOL LIN2 ENABLED\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_config_codif, (int)MASK_LIN_POOLING_CONFIG.POOL_LIN3_ENABLED))
                s_lin_pool += "POOL LIN3 ENABLED\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_config_codif, (int)MASK_LIN_POOLING_CONFIG.POOL_LIN4_RES_ENABLED))
                s_lin_pool += "POOL LIN4 ENABLED\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_config_codif, (int)MASK_LIN_POOLING_CONFIG.POOL_LIN5_RES_ENABLED))
                s_lin_pool += "POOL LIN5 ENABLED\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_config_codif, (int)MASK_LIN_POOLING_CONFIG.POOL_LIN6_RES_ENABLED))
                s_lin_pool += "POOL LIN6 ENABLED\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_config_codif, (int)MASK_LIN_POOLING_CONFIG.POOL_LIN7_RES_ENABLED))
                s_lin_pool += "POOL LIN7 ENABLED\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_config_codif, (int)MASK_LIN_POOLING_CONFIG.POOL_LIN8_RES_ENABLED))
                s_lin_pool += "POOL LIN8 ENABLED\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_config_codif, (int)MASK_LIN_POOLING_CONFIG.POOL_UWB_IMAGE_READ_ENABLED))
                s_lin_pool += "POOL UWB IMAGE READ ENABLED\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_config_codif, (int)MASK_LIN_POOLING_CONFIG.POOL_UWB_5C_TAG_BASE_ENABLED))
                s_lin_pool += "POOL UWB 5C TAG BASE ENABLED\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_config_codif, (int)MASK_LIN_POOLING_CONFIG.POOL_UWB_5C_TAG_EXT_ENABLED))
                s_lin_pool += "POOL UWB 5C TAG EXT ENABLED\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_config_codif, (int)MASK_LIN_POOLING_CONFIG.POOL_12C_TAG_BASE_ENABLED))
                s_lin_pool += "POOL 12C TAG BASE ENABLED\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_config_codif, (int)MASK_LIN_POOLING_CONFIG.POOL_12C_TAG_EXT_ENABLED))
                s_lin_pool += "POOL 12C TAG EXT ENABLED\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_config_codif, (int)MASK_LIN_POOLING_CONFIG.POOL_15C_ZONE_BASE_ENABLED))
                s_lin_pool += "POOL 15C ZONE BASE ENABLED\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_config_codif, (int)MASK_LIN_POOLING_CONFIG.POOL_15C_ZONE_EXT_ENABLED))
                s_lin_pool += "POOL 15C ZONE EXT ENABLED\r\n";

            if (s_lin_pool != string.Empty)
                s_lin_pool = s_lin_pool[..^2];

            return s_lin_pool;
        }

        public static string SAS360CON_lin_pooling_state() {

            string s_lin_ext_pool = string.Empty;

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_state, (int)MASK_LIN_STATE.STATE_LIN1_OK))
                s_lin_ext_pool += "STATE LIN1 OK\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_state, (int)MASK_LIN_STATE.STATE_LIN2_OK))
                s_lin_ext_pool += "STATE LIN2 OK\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_state, (int)MASK_LIN_STATE.STATE_LIN3_OK))
                s_lin_ext_pool += "STATE LIN3 OK\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_state, (int)MASK_LIN_STATE.STATE_LIN4_RES_OK))
                s_lin_ext_pool += "STATE LIN4 RES OK\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_state, (int)MASK_LIN_STATE.STATE_LIN5_RES_OK))
                s_lin_ext_pool += "STATE LIN5 RES OK\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_state, (int)MASK_LIN_STATE.STATE_LIN6_RES_OK))
                s_lin_ext_pool += "STATE LIN6 RES OK\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_state, (int)MASK_LIN_STATE.STATE_LIN7_RES_OK))
                s_lin_ext_pool += "STATE LIN7 RES OK\r\n";

            if (Functions.IsBitSetTo1(Globals.GetTheInstance().Image_sas360con_lin_pooling.Lin_pooling_state, (int)MASK_LIN_STATE.STATE_LIN8_RES_OK))
                s_lin_ext_pool += "STATE LIN8 RES OK\r\n";

            if (s_lin_ext_pool != string.Empty)
                s_lin_ext_pool = s_lin_ext_pool[..^2];

            return s_lin_ext_pool;
        }


        public static string SAS360TAG_codif_state(ushort value)
        {

            string s_tag_codif_state = string.Empty;

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_CODIF_STATE.TAG_OK))
                s_tag_codif_state += "TAG OK\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_CODIF_STATE.INTENAL_ERROR))
                s_tag_codif_state += "INTERNAL ERROR\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_CODIF_STATE.BATTERY_OK))
                s_tag_codif_state += "BATTERY OK\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_CODIF_STATE.BATTERY_CHARGING))
                s_tag_codif_state += "VIBRATIONG\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_CODIF_STATE.VIBRATING))
                s_tag_codif_state += "MAINTENANCE MODE ACTIVE\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_CODIF_STATE.MAINTENANCE_MODE_ACTIVE))
                s_tag_codif_state += "ALARMA AREA A\r\n";


            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_CODIF_STATE.ALARMA_AREA_A))
                s_tag_codif_state += "ALARMA AREA N\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_CODIF_STATE.ALARMA_AREA_N))
                s_tag_codif_state += "ALARMA AREA R\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_CODIF_STATE.ALARMA_AREA_R))
                s_tag_codif_state += "ALARMA CONSOLA NOT OK\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_CODIF_STATE.ALARMA_CONSOLA_NOT_OK))
                s_tag_codif_state += "TBD\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_CODIF_STATE.TBD))
                s_tag_codif_state += "TAG_OK\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_CODIF_STATE.AREA_DETECCION))
                s_tag_codif_state += "AREA DETECCION\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_CODIF_STATE.ERROR_CODE_BIT0))
                s_tag_codif_state += "ERROR CODE BIT 0\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_CODIF_STATE.ERROR_CODE_BIT1))
                s_tag_codif_state += "ERROR CODE BIT 1\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_CODIF_STATE.ERROR_CODE_BIT2))
                s_tag_codif_state += "ERROR CODE BIT 2\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_CODIF_STATE.ERROR_CODE_BIT3))
                s_tag_codif_state += "ERROR CODE BIT 3\r\n";


            if (s_tag_codif_state != string.Empty)
                s_tag_codif_state = s_tag_codif_state[..^2];

            return s_tag_codif_state;
        }

        public static string SAS360TAG_uwb_command_codif(ushort value)
        {

            string s_uwb_command_codif = string.Empty;

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_UWB_COMMAND_CODIF.CMD_UWBx_ORIGING_BIT0))
                s_uwb_command_codif += "CMD UWBx ORIGIN BIT 0\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_UWB_COMMAND_CODIF.CMD_UWBx_ORIGING_BIT1))
                s_uwb_command_codif += "CMD UWBx ORIGIN BIT 1\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_UWB_COMMAND_CODIF.CMD_UWBx_ORIGING_BIT2))
                s_uwb_command_codif += "CMD UWBx ORIGIN BIT 2\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_UWB_COMMAND_CODIF.ALARMA_AREA_A_ACTIVE))
                s_uwb_command_codif += "ALARMA AREA A ACTIVE\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_UWB_COMMAND_CODIF.ALARMA_AREA_N_ACTIVE))
                s_uwb_command_codif += "ALARMA AREA N ACTIVE\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_UWB_COMMAND_CODIF.ALARMA_AREA_R_ACTIVE))
                s_uwb_command_codif += "ALARMA AREA R ACTIVE\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_UWB_COMMAND_CODIF.TBD0))
                s_uwb_command_codif += "TBD\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_UWB_COMMAND_CODIF.TBD1))
                s_uwb_command_codif += "TBD\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_UWB_COMMAND_CODIF.TBD2))
                s_uwb_command_codif += "TBD\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_UWB_COMMAND_CODIF.TBD3))
                s_uwb_command_codif += "TBD\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_UWB_COMMAND_CODIF.TBD4))
                s_uwb_command_codif += "TBD\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_UWB_COMMAND_CODIF.REPORTED_REGISTER_BIT0))
                s_uwb_command_codif += "REPORTED REGISTER BIT 0\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_UWB_COMMAND_CODIF.REPORTED_REGISTER_BIT1))
                s_uwb_command_codif += "REPORTED REGISTER BIT 1\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_UWB_COMMAND_CODIF.REPORTED_REGISTER_BIT2))
                s_uwb_command_codif += "REPORTED REGISTER BIT 2\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360TAG_UWB_COMMAND_CODIF.REPORTED_REGISTER_BIT3))
                s_uwb_command_codif += "REPORTED REGISTER BIT 3\r\n";


            if (s_uwb_command_codif != string.Empty)
                s_uwb_command_codif = s_uwb_command_codif[..^2];

            return s_uwb_command_codif;
        }


        public static string SAS360ZONE_codif_state(ushort value)
        {

            string s_tag_codif_state = string.Empty;

            if (Functions.IsBitSetTo1(value, (int)SAS360ZONE_CODIF_STATE.ZONE_OK))
                s_tag_codif_state += "ZONE OK\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360ZONE_CODIF_STATE.ZONE_INTERNAL_ERROR))
                s_tag_codif_state += "INTERNAL ERROR\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360ZONE_CODIF_STATE.TBD_1))
                s_tag_codif_state += "";

            if (Functions.IsBitSetTo1(value, (int)SAS360ZONE_CODIF_STATE.TBD_2))
                s_tag_codif_state += "";

            if (Functions.IsBitSetTo1(value, (int)SAS360ZONE_CODIF_STATE.TBD_3))
                s_tag_codif_state += "";

            if (Functions.IsBitSetTo1(value, (int)SAS360ZONE_CODIF_STATE.TBD_4))
                s_tag_codif_state += "";


            if (Functions.IsBitSetTo1(value, (int)SAS360ZONE_CODIF_STATE.TBD_5))
                s_tag_codif_state += "";

            if (Functions.IsBitSetTo1(value, (int)SAS360ZONE_CODIF_STATE.TBD_5))
                s_tag_codif_state += "";

            if (Functions.IsBitSetTo1(value, (int)SAS360ZONE_CODIF_STATE.REPORTED_REGISTER_BIT0))
                s_tag_codif_state += "REPORTED REGISTER BIT0\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360ZONE_CODIF_STATE.REPORTED_REGISTER_BIT1))
                s_tag_codif_state += "REPORTED REGISTER BIT1\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360ZONE_CODIF_STATE.REPORTED_REGISTER_BIT2))
                s_tag_codif_state += "REPORTED REGISTER BIT2\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360ZONE_CODIF_STATE.REPORTED_REGISTER_BIT3))
                s_tag_codif_state += "REPORTED REGISTER BIT3\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360ZONE_CODIF_STATE.ERROR_CODE_BIT0))
                s_tag_codif_state += "ERROR CODE BIT 0\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360ZONE_CODIF_STATE.ERROR_CODE_BIT1))
                s_tag_codif_state += "ERROR CODE BIT 1\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360ZONE_CODIF_STATE.ERROR_CODE_BIT2))
                s_tag_codif_state += "ERROR CODE BIT 2\r\n";

            if (Functions.IsBitSetTo1(value, (int)SAS360ZONE_CODIF_STATE.ERROR_CODE_BIT3))
                s_tag_codif_state += "ERROR CODE BIT 3\r\n";


            if (s_tag_codif_state != string.Empty)
                s_tag_codif_state = s_tag_codif_state[..^2];

            return s_tag_codif_state;
        }
    }
}


