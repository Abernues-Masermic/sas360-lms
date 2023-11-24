using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows;
using CsvHelper.Configuration;
using CsvHelper;


namespace sas360_test
{
    public class Manage_memory
    {

        #region Load / save

        public static List<Modbus_var> Load_memory_config(MEMORY_CONFIG_TYPE memory_config_type)
        {
            List<Modbus_var> list_modbus_var = new();

            string file_path =
                memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_INTERNAL_CFG ? Globals.GetTheInstance().Path_sas360con_internal_cfg :
                memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_CFG ? Globals.GetTheInstance().Path_sas360con_cfg :
                memory_config_type == MEMORY_CONFIG_TYPE.IOT_CFG ? Globals.GetTheInstance().Path_iot_cfg :
                memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_IMAGE ? Globals.GetTheInstance().Path_sas360con_image :
                memory_config_type == MEMORY_CONFIG_TYPE.IOT_IMAGE ? Globals.GetTheInstance().Path_iot_image :
                memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_MAINTENNANCE ? Globals.GetTheInstance().Path_sas360con_maintennance :

                memory_config_type == MEMORY_CONFIG_TYPE.UWB_INTERNAL_CFG ? Globals.GetTheInstance().Path_uwb_internal_cfg :
                memory_config_type == MEMORY_CONFIG_TYPE.UWB_IMAGE ? Globals.GetTheInstance().Path_uwb_image :

                memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE_1 ? Globals.GetTheInstance().Path_console_closest_tags_base :
                memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_EXTENDED ? Globals.GetTheInstance().Path_console_closest_tags_extended :
                memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_BASE_1 ? Globals.GetTheInstance().Path_console_closest_zone_base :
                memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_EXTENDED ? Globals.GetTheInstance().Path_console_closest_zone_extended :

                memory_config_type == MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_BASE ? Globals.GetTheInstance().Path_uwb_closest_tags_base :
                memory_config_type == MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_EXTENDED ? Globals.GetTheInstance().Path_uwb_closest_tags_extended :
                memory_config_type == MEMORY_CONFIG_TYPE.UWB_CLOSEST_ZONE_BASE ? Globals.GetTheInstance().Path_uwb_closest_zone_base :
                memory_config_type == MEMORY_CONFIG_TYPE.UWB_CLOSEST_ZONE_EXTENDED ? Globals.GetTheInstance().Path_uwb_closest_zone_extended :

                memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_NVREG ? Globals.GetTheInstance().Path_sas360con_nvreg :

                string.Empty;

            try
            {
                if (File.Exists(file_path))
                {
                    CultureInfo culture_info = new("es-ES"); //El separador decimal en el fichero es la coma
                    var config = new CsvConfiguration(culture_info) { Delimiter = ";", Encoding = Encoding.UTF8, HasHeaderRecord = true, MissingFieldFound = null };
                    var classMap = new Modbus_var_map(false);
                    using TextReader reader = new StreamReader(file_path);
                    using var csv_reader = new CsvReader(reader, config);

                    csv_reader.Context.RegisterClassMap(classMap);
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
                if (File.Exists(Globals.GetTheInstance().Path_sas360con_commands))
                {
                    using TextReader reader = new StreamReader(Globals.GetTheInstance().Path_sas360con_commands);
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


        public static bool Save_modbus_var(MEMORY_CONFIG_TYPE memory_config_type)
        {
            bool save_ok = true;

            try
            {
                string file_path =
                    memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_INTERNAL_CFG ? Globals.GetTheInstance().Path_sas360con_internal_cfg :
                    memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_CFG ? Globals.GetTheInstance().Path_sas360con_cfg :
                    memory_config_type == MEMORY_CONFIG_TYPE.IOT_CFG ? Globals.GetTheInstance().Path_iot_cfg :
                    memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_IMAGE ? Globals.GetTheInstance().Path_sas360con_image :
                    memory_config_type == MEMORY_CONFIG_TYPE.IOT_IMAGE ? Globals.GetTheInstance().Path_iot_image :
                    memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_MAINTENNANCE ? Globals.GetTheInstance().Path_sas360con_maintennance :

                    memory_config_type == MEMORY_CONFIG_TYPE.UWB_INTERNAL_CFG ? Globals.GetTheInstance().Path_uwb_internal_cfg :
                    memory_config_type == MEMORY_CONFIG_TYPE.UWB_IMAGE ? Globals.GetTheInstance().Path_uwb_image :

                    memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE_1 ? Globals.GetTheInstance().Path_console_closest_tags_base :
                    memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_EXTENDED ? Globals.GetTheInstance().Path_console_closest_tags_extended :
                    memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_BASE_1 ? Globals.GetTheInstance().Path_console_closest_zone_base :
                    memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_EXTENDED ? Globals.GetTheInstance().Path_console_closest_zone_extended :

                    memory_config_type == MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_BASE ? Globals.GetTheInstance().Path_uwb_closest_tags_base :
                    memory_config_type == MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_EXTENDED ? Globals.GetTheInstance().Path_uwb_closest_tags_extended :

                    memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_NVREG ? Globals.GetTheInstance().Path_sas360con_nvreg :

                    string.Empty;


                List<Modbus_var> list_modbus_var =
                    memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_INTERNAL_CFG ? Globals.GetTheInstance().List_sas360con_internal_cfg :
                    memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_CFG ? Globals.GetTheInstance().List_sas360con_cfg :
                    memory_config_type == MEMORY_CONFIG_TYPE.IOT_CFG ? Globals.GetTheInstance().List_iot_cfg :
                    memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_IMAGE ? Globals.GetTheInstance().List_sas360con_image :
                    memory_config_type == MEMORY_CONFIG_TYPE.IOT_IMAGE ? Globals.GetTheInstance().List_iot_image :
                    memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_MAINTENNANCE ? Globals.GetTheInstance().List_sas360con_maintennance :

                    memory_config_type == MEMORY_CONFIG_TYPE.UWB_INTERNAL_CFG ? Globals.GetTheInstance().List_uwb_internal_cfg :
                    memory_config_type == MEMORY_CONFIG_TYPE.UWB_IMAGE ? Globals.GetTheInstance().List_uwb_image :

                    memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE_1 ? Globals.GetTheInstance().List_console_closest_tags_base :
                    memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_EXTENDED ? Globals.GetTheInstance().List_console_closest_tags_extended :
                    memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_BASE_1 ? Globals.GetTheInstance().List_console_closest_zone_base :
                    memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_EXTENDED ? Globals.GetTheInstance().List_console_closest_zone_extended :

                    memory_config_type == MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_BASE ? Globals.GetTheInstance().List_uwb_closest_tags_base :
                    memory_config_type == MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_EXTENDED ? Globals.GetTheInstance().List_uwb_closest_tags_extended :

                    memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_NVREG ? Globals.GetTheInstance().List_sas360con_nvreg :

                    new List<Modbus_var>();



                CultureInfo culture_info = new("es-ES"); //El separador decimal en el fichero es la coma
                var config = new CsvConfiguration(culture_info) { Delimiter = ";", Encoding = Encoding.UTF8, HasHeaderRecord = true };
                var classMap = new Modbus_var_map(false);
                using TextWriter modbus_var_writer = new StreamWriter(file_path, false);
                using var modbus_var_csv_writer = new CsvWriter(modbus_var_writer, config);
                modbus_var_csv_writer.Context.RegisterClassMap(classMap);
                modbus_var_csv_writer.WriteRecords(list_modbus_var);
            }
            catch (Exception ex)
            {
                save_ok = false;
                Manage_logs.SaveErrorValue($"{typeof(Manage_file).Name} -> {nameof(Save_modbus_var)} -> {ex.Message}");
            }

            return save_ok;
        }

        #endregion


        #region CSV

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


        public static List<Modbus_var> Create_structure_array(string textbox_data, int num_array, int num_uwb, RADIO_CSV_TAG_ZONE_TYPE tag_zone_type, RADIO_CSV_MEMORY_TYPE memory_type)
        {
            List<Modbus_var> list_modbus_var_generate = new();

            try
            {
                string[] sep = new string[] { "\r\n" };
                string[] memory_lines = textbox_data.Split(sep, StringSplitOptions.RemoveEmptyEntries);

                List<Modbus_var> list_modbus_var = new();

                memory_lines.ToList()
                    .ForEach(line =>
                    {
                        string[] fields = line.Split(";");
                        list_modbus_var.Add(new()
                        {
                            Addr = double.Parse(fields[(int)MEMORY_FIELD_POS_CSV.ADDR]),
                            Name = fields[(int)MEMORY_FIELD_POS_CSV.NAME],
                            TypeName = fields[(int)MEMORY_FIELD_POS_CSV.TYPE_NAME],
                            Unit = fields[(int)MEMORY_FIELD_POS_CSV.UNIT],
                            Format = double.Parse(fields[(int)MEMORY_FIELD_POS_CSV.FORMAT]),
                        });
                    });

                double next_reg = list_modbus_var[^1].Addr % 1 == 0 ? 1 : 0.5;
                double reg_pos_diff = list_modbus_var[^1].Addr - list_modbus_var[0].Addr + next_reg;

                string s_tag_zone =
                    tag_zone_type == RADIO_CSV_TAG_ZONE_TYPE.TAG ? "T" :
                    tag_zone_type == RADIO_CSV_TAG_ZONE_TYPE.ZONE ? "Z" : "";

                if (num_uwb == 0)
                {
                    for (int index_array = 0; index_array < num_array; index_array++)
                    {
                        list_modbus_var.ForEach(modbus_var_base =>
                            {
                                list_modbus_var_generate.Add(new Modbus_var()
                                {
                                    Addr = modbus_var_base.Addr + (reg_pos_diff * index_array),
                                    Name = $"{modbus_var_base.Name}_{s_tag_zone}{index_array + 1}C",
                                    TypeName = modbus_var_base.TypeName,
                                    Unit = modbus_var_base.Unit,
                                    Format = modbus_var_base.Format
                                });
                            });
                    }
                }

                else
                {

                    for (int index_uwb = 0; index_uwb < num_uwb; index_uwb++)
                    {
                        double uwb_fist_pos = list_modbus_var[0].Addr;
                        if (memory_type == RADIO_CSV_MEMORY_TYPE.BASE)
                            uwb_fist_pos = Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.UWB_CLOSEST_TAGS_BASE] + (Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.UWB_CLOSEST_TAGS_BASE] * index_uwb);

                        else
                            uwb_fist_pos = Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.UWB_CLOSEST_TAGS_EXTENDED] + (Globals.GetTheInstance().Memory_map_size[(int)ENABLE_READ_MEMORY_BIT.UWB_CLOSEST_TAGS_EXTENDED] * index_uwb);


                        double addr_diff = uwb_fist_pos - list_modbus_var[0].Addr;

                        for (int index_array = 0; index_array < num_array; index_array++)
                        {
                            list_modbus_var.ForEach(modbus_var_base =>
                            {
                                list_modbus_var_generate.Add(new Modbus_var()
                                {
                                    Addr = modbus_var_base.Addr + addr_diff + (reg_pos_diff * index_array),
                                    Name = $"{modbus_var_base.Name}_UWB{index_uwb + 1}_T{index_array + 1}C",
                                    TypeName = modbus_var_base.TypeName,
                                    Unit = modbus_var_base.Unit,
                                    Format = modbus_var_base.Format
                                });
                            });
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Create_structure_array)} -> {ex.Message}");
            }

            return list_modbus_var_generate;
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
                            CultureInfo culture_info = new("es-ES"); //El separador decimal en el fichero es la coma
                            var config = new CsvConfiguration(culture_info) { Delimiter = ";", Encoding = Encoding.UTF8, HasHeaderRecord = true };
                            var classMap = new Modbus_var_map(false);
                            using TextWriter writer = new StreamWriter(fileDialog.FileName, false);
                            using var csv_writer = new CsvWriter(writer, config);

                            csv_writer.Context.RegisterClassMap(classMap);
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


        public static bool Save_memory_list_to_csv(List<Modbus_var> list_modbus_var, string file_path)
        {
            bool save = false;

            try
            {
                using TextWriter writer = new StreamWriter(file_path);
                CultureInfo culture_info = new("es-ES"); //El separador decimal en el fichero es la coma
                var config = new CsvConfiguration(culture_info) { Delimiter = ";", Encoding = Encoding.UTF8, HasHeaderRecord = true, MissingFieldFound = null };
                using var csv_writer = new CsvWriter(writer, config);

                var classMap = new Modbus_var_map(true);
                csv_writer.Context.RegisterClassMap(classMap);
                csv_writer.WriteRecords(list_modbus_var);

                save = true;
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Save_memory_list_to_csv)} -> {ex.Message}");
            }

            return save;
        }


        public static List<Modbus_var> Load_memory_list_from_csv(string file_path)
        {
            List<Modbus_var> list_modbus_var = new();

            try
            {
                CultureInfo culture_info = new("es-ES"); //El separador decimal en el fichero es la coma
                var config = new CsvConfiguration(culture_info) { Delimiter = ";", Encoding = Encoding.UTF8, HasHeaderRecord = true, MissingFieldFound = null };
                using TextReader reader = new StreamReader(file_path);
                using var csv_reader = new CsvReader(reader, config);
                var classMap = new Modbus_var_map(true);

                csv_reader.Context.RegisterClassMap(classMap);
                list_modbus_var = csv_reader.GetRecords<Modbus_var>().ToList();
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Load_memory_list_from_csv)} -> {ex.Message}");
            }

            return list_modbus_var;
        }


        #endregion


        #region Simulator data

        public static bool Simulator_data(MEMORY_CONFIG_TYPE memory_config_type)
        {
            Random random = new();

            bool read_memory_ok =
                memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_INTERNAL_CFG && Globals.GetTheInstance().List_sas360con_internal_cfg.Count > 0 ||
                memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_CFG && Globals.GetTheInstance().List_sas360con_cfg.Count > 0 ||
                memory_config_type == MEMORY_CONFIG_TYPE.IOT_CFG ||
                memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_IMAGE && Globals.GetTheInstance().List_sas360con_image.Count > 0 ||
                memory_config_type == MEMORY_CONFIG_TYPE.IOT_IMAGE ||
                memory_config_type == MEMORY_CONFIG_TYPE.UWB_INTERNAL_CFG && Globals.GetTheInstance().List_uwb_internal_cfg.Count > 0 ||
                memory_config_type == MEMORY_CONFIG_TYPE.UWB_IMAGE && Globals.GetTheInstance().List_uwb_image.Count > 0 ||
                memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_MAINTENNANCE && Globals.GetTheInstance().List_sas360con_maintennance.Count > 0 ||
                memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE_3 && Globals.GetTheInstance().List_console_closest_tags_base.Count > 0 ||
                memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_EXTENDED ||
                memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_BASE_2 && Globals.GetTheInstance().List_console_closest_zone_base.Count > 0 ||
                memory_config_type == MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_EXTENDED ||
                memory_config_type == MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_BASE ||
                memory_config_type == MEMORY_CONFIG_TYPE.UWB_CLOSEST_TAGS_EXTENDED ||
                memory_config_type == MEMORY_CONFIG_TYPE.SAS360CON_NVREG && Globals.GetTheInstance().List_sas360con_nvreg.Count > 0;

            try
            {
                if (read_memory_ok)
                {
                    switch (memory_config_type)
                    {
                        case MEMORY_CONFIG_TYPE.SAS360CON_INTERNAL_CFG:
                            {
                                int index_internal_config = Globals.GetTheInstance().List_sas360con_internal_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_INTERNAL_CFG]);

                                #region serial number

                                ushort[] serial_number = new ushort[3] { (ushort)random.Next(ushort.MinValue, 9999), (ushort)random.Next(ushort.MinValue, 9999), (ushort)random.Next(ushort.MinValue, 9999) };
                                Globals.GetTheInstance().List_sas360con_internal_cfg[index_internal_config].Value = serial_number[0];

                                index_internal_config++;

                                Globals.GetTheInstance().List_sas360con_internal_cfg[index_internal_config].Value = serial_number[1];

                                index_internal_config++;

                                Globals.GetTheInstance().List_sas360con_internal_cfg[index_internal_config].Value = serial_number[2];

                                index_internal_config++;

                                Globals.GetTheInstance().SAS360CON_internal_cfg.Serial_number = $"{serial_number[0]:D4}.{serial_number[1]:D4}.{serial_number[2]:D4}";

                                #endregion

                                ushort tag_ID_2LSB = (ushort)random.Next(1, 9912);
                                Globals.GetTheInstance().List_sas360con_internal_cfg[index_internal_config].Value = tag_ID_2LSB;
                                Globals.GetTheInstance().SAS360CON_internal_cfg.ID_manufacturing = tag_ID_2LSB.ToString();

                                index_internal_config++;

                                int tag_ID_3er_byte = random.Next(0, 4);
                                int tag_type = tag_ID_3er_byte;
                                Globals.GetTheInstance().List_sas360con_internal_cfg[index_internal_config].Value = tag_ID_3er_byte;
                                Globals.GetTheInstance().SAS360CON_internal_cfg.Tag_type = Enum.IsDefined(typeof(MASK_TAG_ZONE_TYPE), tag_type) ? (MASK_TAG_ZONE_TYPE)tag_type : MASK_TAG_ZONE_TYPE.UNKNOWN;

                                index_internal_config++;

                                #region HW ver

                                string s_ver_hw = random.Next(100, 200).ToString();
                                s_ver_hw = $"{s_ver_hw[..^2]}.{s_ver_hw.Substring(s_ver_hw.Length - 2, 2)}";

                                Globals.GetTheInstance().List_sas360con_internal_cfg[index_internal_config].Value = s_ver_hw;
                                Globals.GetTheInstance().SAS360CON_internal_cfg.Version_hw = s_ver_hw;

                                index_internal_config++;

                                #endregion

                                #region FW ver

                                string s_ver_fw = random.Next(100, 200).ToString();
                                s_ver_fw = $"{s_ver_fw[..^2]}.{s_ver_fw.Substring(s_ver_fw.Length - 2, 2)}";
                                Globals.GetTheInstance().List_sas360con_internal_cfg[index_internal_config].Value = s_ver_fw;
                                Globals.GetTheInstance().SAS360CON_internal_cfg.Version_fw = s_ver_fw;

                                index_internal_config++;

                                #endregion

                                #region BOOT ver

                                string s_ver_boot = random.Next(100, 200).ToString();
                                s_ver_boot = $"{s_ver_boot[..^2]}.{s_ver_boot.Substring(s_ver_boot.Length - 2, 2)}";
                                Globals.GetTheInstance().List_sas360con_internal_cfg[index_internal_config].Value = s_ver_boot;
                                Globals.GetTheInstance().SAS360CON_internal_cfg.Version_boot = s_ver_boot;

                                index_internal_config++;

                                #endregion

                                ushort slave_speed = 7;
                                Globals.GetTheInstance().List_sas360con_internal_cfg[index_internal_config].Value = slave_speed;
                                Globals.GetTheInstance().SAS360CON_internal_cfg.RTU_slave_speed = SAS360CON_MODBUS_SPEED(slave_speed);

                                index_internal_config++;

                                ushort slave_num = (ushort)random.Next(0, 255);
                                Globals.GetTheInstance().List_sas360con_internal_cfg[index_internal_config].Value = slave_num;
                                Globals.GetTheInstance().SAS360CON_internal_cfg.RTU_slave_num = slave_num;

                                index_internal_config++;

                                ushort master_speed = 7;
                                Globals.GetTheInstance().List_sas360con_internal_cfg[index_internal_config].Value = master_speed;
                                Globals.GetTheInstance().SAS360CON_internal_cfg.Lin_master_speed = SAS360CON_MODBUS_SPEED(master_speed);

                                index_internal_config++;

                                ushort consola_id_installation = 1;
                                Globals.GetTheInstance().List_sas360con_internal_cfg[index_internal_config].Value = consola_id_installation;
                                Globals.GetTheInstance().SAS360CON_internal_cfg.Consola_id = consola_id_installation;

                                index_internal_config++;

                                index_internal_config += 2; //Reserved

                                #region RTC

                                uint u32_rtc_fw = (uint)DateTime.Now.Subtract(Constants.date_ref).TotalSeconds;
                                DateTime date_rtc_fw = Constants.date_ref.Date.AddSeconds(u32_rtc_fw);
                                Globals.GetTheInstance().List_sas360con_internal_cfg[index_internal_config].Value = u32_rtc_fw;
                                Globals.GetTheInstance().SAS360CON_internal_cfg.RTC_fw_update = date_rtc_fw.ToString(Globals.GetTheInstance().DateFormat, new CultureInfo(Globals.GetTheInstance().DateProvider));

                                index_internal_config++;

                                uint u32_rtc_cfg = (uint)DateTime.Now.Subtract(Constants.date_ref).TotalSeconds;
                                DateTime date_rtc_cfg = Constants.date_ref.Date.AddSeconds(u32_rtc_cfg);
                                Globals.GetTheInstance().List_sas360con_internal_cfg[index_internal_config].Value = u32_rtc_cfg;
                                Globals.GetTheInstance().SAS360CON_internal_cfg.RTC_config_update = date_rtc_cfg.ToString(Globals.GetTheInstance().DateFormat, new CultureInfo(Globals.GetTheInstance().DateProvider));

                                index_internal_config++;

                                #endregion

                                ushort cfg_change_counter = (ushort)random.Next(0, ushort.MaxValue);
                                Globals.GetTheInstance().List_sas360con_internal_cfg[index_internal_config].Value = cfg_change_counter;
                                Globals.GetTheInstance().SAS360CON_internal_cfg.Change_counter = cfg_change_counter;

                                index_internal_config++;

                                ushort crc_config = (ushort)random.Next(0, ushort.MaxValue);
                                Globals.GetTheInstance().List_sas360con_internal_cfg[index_internal_config].Value = crc_config;
                                Globals.GetTheInstance().SAS360CON_internal_cfg.CRC_config = crc_config;

                                index_internal_config++;

                                break;
                            }

                        case MEMORY_CONFIG_TYPE.SAS360CON_CFG:
                            {
                                #region Installation client definition

                                try
                                {
                                    int index_installation_client = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + 2);

                                    ushort client_id = (ushort)random.Next(ushort.MinValue, ushort.MaxValue);
                                    Globals.GetTheInstance().SAS360CON_cfg_installation_client.Client = client_id;
                                    Globals.GetTheInstance().List_sas360con_cfg[index_installation_client].Value = client_id;

                                    index_installation_client++;

                                    ushort installation_id = (ushort)random.Next(ushort.MinValue, ushort.MaxValue);
                                    Globals.GetTheInstance().SAS360CON_cfg_installation_client.Installation = installation_id;
                                    Globals.GetTheInstance().List_sas360con_cfg[index_installation_client].Value = installation_id;

                                    index_installation_client++;

                                    ushort vehicle_type_id = (ushort)random.Next(ushort.MinValue, ushort.MaxValue);
                                    Globals.GetTheInstance().SAS360CON_cfg_installation_client.Vehicle_type = vehicle_type_id;
                                    Globals.GetTheInstance().List_sas360con_cfg[index_installation_client].Value = vehicle_type_id;

                                    index_installation_client++;

                                    ushort audio_language = (ushort)random.Next(ushort.MinValue, ushort.MaxValue);
                                    Globals.GetTheInstance().SAS360CON_cfg_installation_client.Audio_language = audio_language;
                                    Globals.GetTheInstance().List_sas360con_cfg[index_installation_client].Value = audio_language;

                                    index_installation_client++;

                                    ushort audio_volume = (ushort)random.Next(ushort.MinValue, ushort.MaxValue);
                                    Globals.GetTheInstance().SAS360CON_cfg_installation_client.Audio_volume = audio_volume;
                                    Globals.GetTheInstance().List_sas360con_cfg[index_installation_client].Value = audio_volume;

                                    index_installation_client++;

                                    index_installation_client++; //Reserved
                                }
                                catch { }

                                #endregion

                                #region Vehicle configuration

                                try
                                {
                                    int index_vehicle_config = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + 8);

                                    #region Vehicle dim

                                    ushort vehicle_width = 300;
                                    Globals.GetTheInstance().List_sas360con_cfg[index_vehicle_config].Value = vehicle_width;

                                    index_vehicle_config++;

                                    ushort vehicle_height = 600;
                                    Globals.GetTheInstance().List_sas360con_cfg[index_vehicle_config].Value = vehicle_height;

                                    index_vehicle_config++;

                                    Globals.GetTheInstance().SAS360CON_cfg_vehicle_cfg.Vehicle_dim_xy_cm = new ushort[2] { vehicle_width, vehicle_height };

                                    #endregion

                                    #region Antenna pos

                                    short[] array_antenna_pos_x_cm = new short[] { 100, 100, -150 };
                                    short[] array_antenna_pos_y_cm = new short[] { -75, 75, 0 };

                                    Globals.GetTheInstance().List_sas360con_cfg[index_vehicle_config].Value = array_antenna_pos_x_cm[0];

                                    index_vehicle_config++;

                                    Globals.GetTheInstance().List_sas360con_cfg[index_vehicle_config].Value = array_antenna_pos_y_cm[0];

                                    index_vehicle_config++;

                                    Globals.GetTheInstance().List_sas360con_cfg[index_vehicle_config].Value = array_antenna_pos_x_cm[1];

                                    index_vehicle_config++;

                                    Globals.GetTheInstance().List_sas360con_cfg[index_vehicle_config].Value = array_antenna_pos_y_cm[1];

                                    index_vehicle_config++;

                                    Globals.GetTheInstance().List_sas360con_cfg[index_vehicle_config].Value = array_antenna_pos_x_cm[2];

                                    index_vehicle_config++;

                                    Globals.GetTheInstance().List_sas360con_cfg[index_vehicle_config].Value = array_antenna_pos_y_cm[2];

                                    index_vehicle_config++;

                                    Globals.GetTheInstance().SAS360CON_cfg_vehicle_cfg.Antenna_xy_cm = new short[Constants.ANTENNA_COUNT, 2];
                                    for (int index_lin = 0; index_lin < Constants.ANTENNA_COUNT; index_lin++)
                                    {
                                        Globals.GetTheInstance().SAS360CON_cfg_vehicle_cfg.Antenna_xy_cm[index_lin, 0] = array_antenna_pos_x_cm[index_lin];
                                        Globals.GetTheInstance().SAS360CON_cfg_vehicle_cfg.Antenna_xy_cm[index_lin, 1] = array_antenna_pos_y_cm[index_lin];
                                    }

                                    #endregion

                                    index_vehicle_config += 4; //Reserved
                                }
                                catch { }

                                #endregion

                                #region Detection area

                                try
                                {
                                    int index_detection_area = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + 20);

                                    #region FRONT

                                    ushort[] area_front_anri_dist_cm = new ushort[Constants.DETECTION_AREA_COUNT];

                                    area_front_anri_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW] = 1250;
                                    area_front_anri_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE] = 1000;
                                    area_front_anri_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.RED] = 750;
                                    area_front_anri_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.INTERIOR] = 500;

                                    Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_FRONT_ANRI_dist_cm = area_front_anri_dist_cm;
                                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                                    {
                                        Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Value = area_front_anri_dist_cm[index];

                                        index_detection_area++;
                                    }

                                    #endregion

                                    #region RIGHT

                                    ushort[] area_right_anri_dist_cm = new ushort[Constants.DETECTION_AREA_COUNT];

                                    area_right_anri_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW] = 1000;
                                    area_right_anri_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE] = 750;
                                    area_right_anri_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.RED] = 500;
                                    area_right_anri_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.INTERIOR] = 250;

                                    Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_RIGHT_ANRI_dist_cm = area_right_anri_dist_cm;
                                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                                    {
                                        Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Value = area_right_anri_dist_cm[index];
                                        index_detection_area++;
                                    }

                                    #endregion

                                    #region BACK

                                    ushort[] area_back_anri_dist_cm = new ushort[Constants.DETECTION_AREA_COUNT];

                                    area_back_anri_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW] = 1250;
                                    area_back_anri_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE] = 1000;
                                    area_back_anri_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.RED] = 750;
                                    area_back_anri_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.INTERIOR] = 500;

                                    Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_BACK_ANRI_dist_cm = area_back_anri_dist_cm;
                                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                                    {
                                        Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Value = area_back_anri_dist_cm[index];
                                        index_detection_area++;
                                    }

                                    #endregion

                                    #region LEFT

                                    ushort[] area_left_anri_dist_cm = new ushort[Constants.DETECTION_AREA_COUNT];

                                    area_left_anri_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW] = 1000;
                                    area_left_anri_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE] = 750;
                                    area_left_anri_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.RED] = 500;
                                    area_left_anri_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.INTERIOR] = 250;

                                    Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_LEFT_ANRI_dist_cm = area_left_anri_dist_cm;
                                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                                    {
                                        Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Value = area_left_anri_dist_cm[index];
                                        index_detection_area++;
                                    }

                                    #endregion

                                    ushort area_detection_distance_cm = 10000;
                                    Globals.GetTheInstance().SAS360CON_cfg_detection_area.Area_detection_distance_cm = area_detection_distance_cm;
                                    Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Value = area_detection_distance_cm;

                                    index_detection_area++;

                                    ushort area_change_hysteresys_cent_pct = 500;
                                    Globals.GetTheInstance().SAS360CON_cfg_detection_area.Area_change_hysteresis_cent_pct = area_change_hysteresys_cent_pct;
                                    Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Value = area_change_hysteresys_cent_pct;

                                    index_detection_area++;

                                    ushort sector_change_hysteresis_cent_pct = 500;
                                    Globals.GetTheInstance().SAS360CON_cfg_detection_area.Sector_change_hysteresis_cent_pct = sector_change_hysteresis_cent_pct;
                                    Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Value = sector_change_hysteresis_cent_pct;

                                    index_detection_area++;

                                    ushort trilat_calc_enabled = 1;
                                    Globals.GetTheInstance().SAS360CON_cfg_detection_area.Trilat_calc_enabled = trilat_calc_enabled;
                                    Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Value = trilat_calc_enabled;

                                    index_detection_area++;

                                    #region DIST ANTENA

                                    ushort[] area_dist_antena_anri_dist_cm = new ushort[Constants.DETECTION_AREA_COUNT];

                                    area_dist_antena_anri_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.YELLOW] = 1000;
                                    area_dist_antena_anri_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.ORANGE] = 750;
                                    area_dist_antena_anri_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.RED] = 500;
                                    area_dist_antena_anri_dist_cm[(int)DETECTION_AREA_POS_IN_ARRAY.INTERIOR] = 250;

                                    Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_DIST_ANTENA_ANRI_dist_cm = area_dist_antena_anri_dist_cm;
                                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                                    {
                                        Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Value = area_dist_antena_anri_dist_cm[index];
                                        index_detection_area++;
                                    }

                                    #endregion

                                    ushort gestion_avanzada_pos = 1;
                                    Globals.GetTheInstance().SAS360CON_cfg_detection_area.Gestion_avanzada_position_enable = gestion_avanzada_pos;
                                    Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Value = gestion_avanzada_pos;

                                    index_detection_area += 5; //Reserved
                                }
                                catch { }

                                #endregion

                                #region Actuaciones E / S

                                try
                                {
                                    int index_actuaciones_e_a = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + 50);

                                    for (int index = 0; index < Enum.GetNames(typeof(ACTUACIONES_SALIDAS_POS_IN_ARRAY)).Length; index++)
                                    {
                                        Globals.GetTheInstance().SAS360CON_cfg_general.Array_actuaciones_salidas[index] = (ushort)random.Next(0, ushort.MaxValue);

                                        Globals.GetTheInstance().List_sas360con_cfg[index_actuaciones_e_a].Value = Globals.GetTheInstance().SAS360CON_cfg_general.Array_actuaciones_salidas[index];
                                        index_actuaciones_e_a++;
                                    }

                                    index_actuaciones_e_a += 4; //Reserved
                                }
                                catch { }

                                #endregion

                                #region Temporizadores y filtros

                                try
                                {
                                    int index_temp = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + 60);

                                    ushort deactivation_delay = 5;
                                    Globals.GetTheInstance().List_sas360con_cfg[index_temp].Value = deactivation_delay;
                                    Globals.GetTheInstance().SAS360CON_cfg_general.Output_deactivation_delay_sec = deactivation_delay;

                                    index_temp++;

                                    ushort area_zone_dist = 2;
                                    Globals.GetTheInstance().List_sas360con_cfg[index_temp].Value = area_zone_dist;
                                    Globals.GetTheInstance().SAS360CON_cfg_general.Area_zone_dist_cm = area_zone_dist;

                                    index_temp++;

                                    ushort red_zone_alert_audio_repeat_sec = 10;
                                    Globals.GetTheInstance().List_sas360con_cfg[index_temp].Value = red_zone_alert_audio_repeat_sec;
                                    Globals.GetTheInstance().SAS360CON_cfg_general.Red_zone_alert_audio_repeat_sec = red_zone_alert_audio_repeat_sec;

                                    index_temp += 7; // Reserved
                                }
                                catch { }

                                #endregion

                                #region UWB com config reserved

                                try
                                {
                                    int index_uwb_com_config = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + 70);

                                    for (int index_lin = 0; index_lin < Constants.UWB_TOTAL_COUNT; index_lin++)
                                    {
                                        Globals.GetTheInstance().SAS360CON_cfg_general.Array_lin_used[index_lin] = (byte)index_lin;
                                        Globals.GetTheInstance().SAS360CON_cfg_general.Array_lin_modbus_slave[index_lin] = (byte)random.Next(0, 255);
                                    }

                                    for (int index_uwb = 0; index_uwb < Constants.UWB_TOTAL_COUNT; index_uwb++)
                                    {
                                        Globals.GetTheInstance().List_sas360con_cfg[index_uwb_com_config].Value = Globals.GetTheInstance().SAS360CON_cfg_general.Array_lin_used[index_uwb];
                                        Globals.GetTheInstance().Array_SAS360CON_UWB[index_uwb].Lin = Globals.GetTheInstance().SAS360CON_cfg_general.Array_lin_used[index_uwb];

                                        index_uwb_com_config++;
                                    }

                                    for (int index_uwb = 0; index_uwb < Constants.UWB_TOTAL_COUNT; index_uwb++)
                                    {
                                        Globals.GetTheInstance().List_sas360con_cfg[index_uwb_com_config].Value = Globals.GetTheInstance().SAS360CON_cfg_general.Array_lin_modbus_slave[index_uwb];
                                        Globals.GetTheInstance().Array_SAS360CON_UWB[index_uwb].Slave = Globals.GetTheInstance().SAS360CON_cfg_general.Array_lin_modbus_slave[index_uwb];

                                        index_uwb_com_config++;
                                    }

                                    index_uwb_com_config += 6; // Reserved
                                }
                                catch { }

                                #endregion

                                #region UWB tag config reserved

                                try
                                {
                                    int index_uwb_tag_config = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + 80);

                                    ushort clear_undetected_tag_decsec = 1;
                                    Globals.GetTheInstance().List_sas360con_cfg[index_uwb_tag_config].Value = clear_undetected_tag_decsec;
                                    Globals.GetTheInstance().SAS360CON_cfg_general.Clear_undetected_tag_decseg = clear_undetected_tag_decsec;

                                    index_uwb_tag_config++;

                                    index_uwb_tag_config += 9; // Reserved
                                }
                                catch { }

                                #endregion

                                #region Recording


                                #endregion

                                #region Calculadas config

                                try
                                {
                                    int index_calculadas = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + 116);

                                    uint u32_rtc_last_config_change = (uint)DateTime.Now.Subtract(Constants.date_ref).TotalSeconds;
                                    DateTime date_rtc_last_config_change = Constants.date_ref.Date.AddSeconds(u32_rtc_last_config_change);
                                    Globals.GetTheInstance().List_sas360con_cfg[index_calculadas].Value = u32_rtc_last_config_change;
                                    Globals.GetTheInstance().SAS360CON_cfg_general.RTC_last_config_change = date_rtc_last_config_change.ToString(Globals.GetTheInstance().DateFormat, new CultureInfo(Globals.GetTheInstance().DateProvider));

                                    index_calculadas++;

                                    Globals.GetTheInstance().SAS360CON_cfg_general.Change_counter = (ushort)random.Next(1, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_cfg[index_calculadas].Value = Globals.GetTheInstance().SAS360CON_cfg_general.Change_counter;

                                    index_calculadas++;

                                    Globals.GetTheInstance().SAS360CON_cfg_general.CRC_config = (ushort)random.Next(1, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_cfg[index_calculadas].Value = Globals.GetTheInstance().SAS360CON_cfg_general.CRC_config;
                                }
                                catch { }

                                #endregion

                                break;
                            }

                        case MEMORY_CONFIG_TYPE.IOT_CFG:
                            {
                                break;
                            }

                        case MEMORY_CONFIG_TYPE.SAS360CON_IMAGE:
                            {
                                #region Estados booleanos

                                try
                                {
                                    int index_estados_booleanos = Globals.GetTheInstance().List_sas360con_image.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE]);

                                    uint u32_rtc_utc = (uint)DateTime.Now.Subtract(Constants.date_ref).TotalSeconds;
                                    Globals.GetTheInstance().List_sas360con_image[index_estados_booleanos].Value = u32_rtc_utc;
                                    Globals.GetTheInstance().SAS360CON_image_general.RTC_UTC_seconds = u32_rtc_utc;

                                    index_estados_booleanos++;

                                    ushort milliseconds = 1274;
                                    Globals.GetTheInstance().SAS360CON_image_general.Milliseconds = milliseconds;
                                    Globals.GetTheInstance().List_sas360con_image[index_estados_booleanos].Value = milliseconds;

                                    index_estados_booleanos++;

                                    ushort watchdog = (ushort)random.Next(ushort.MinValue, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_estados_booleanos].Value = watchdog;
                                    Globals.GetTheInstance().SAS360CON_image_general.Watchdog_inc = watchdog;

                                    index_estados_booleanos++;

                                    #region State - substate

                                    ushort sas360_state = (ushort)random.Next(1, 7);
                                    ushort sas360_substate = (ushort)random.Next(1, 3);

                                    Globals.GetTheInstance().List_sas360con_image[index_estados_booleanos].Value = sas360_state;
                                    Globals.GetTheInstance().SAS360CON_image_general.Sas360_state = Enum.IsDefined(typeof(MASK_SAS360CON_STATE), sas360_state) ? (MASK_SAS360CON_STATE)sas360_state : MASK_SAS360CON_STATE.NOT_DEFINED;

                                    index_estados_booleanos++;

                                    Globals.GetTheInstance().List_sas360con_image[index_estados_booleanos].Value = sas360_state;
                                    Globals.GetTheInstance().SAS360CON_image_general.Sas360_substate = Enum.IsDefined(typeof(MASK_SAS360CON_SUBSTATE), sas360_substate) ? (MASK_SAS360CON_SUBSTATE)sas360_substate : MASK_SAS360CON_SUBSTATE.NOT_DEFINED;

                                    index_estados_booleanos++;

                                    #endregion

                                    #region Codif bits

                                    Globals.GetTheInstance().SAS360CON_image_general.Array_codif_bits = new ushort[2];

                                    ushort[] codif_bits = new ushort[2] { (ushort)random.Next(ushort.MinValue, ushort.MaxValue), (ushort)random.Next(ushort.MinValue, ushort.MaxValue) };
                                    Globals.GetTheInstance().List_sas360con_image[index_estados_booleanos].Value = codif_bits[0];
                                    Globals.GetTheInstance().SAS360CON_image_general.Array_codif_bits[0] = codif_bits[0];

                                    index_estados_booleanos++;

                                    Globals.GetTheInstance().List_sas360con_image[index_estados_booleanos].Value = codif_bits[1];
                                    Globals.GetTheInstance().SAS360CON_image_general.Array_codif_bits[1] = codif_bits[1];

                                    index_estados_booleanos++;

                                    #endregion

                                    #region Codif management

                                    Globals.GetTheInstance().SAS360CON_image_general.Array_codif_management = new ushort[2];

                                    ushort[] codif_management = new ushort[2] { (ushort)random.Next(ushort.MinValue, ushort.MaxValue), (ushort)random.Next(ushort.MinValue, ushort.MaxValue) };
                                    Globals.GetTheInstance().List_sas360con_image[index_estados_booleanos].Value = codif_management[0];
                                    Globals.GetTheInstance().SAS360CON_image_general.Array_codif_management[0] = codif_management[0];

                                    index_estados_booleanos++;

                                    Globals.GetTheInstance().List_sas360con_image[index_estados_booleanos].Value = codif_management[1];
                                    Globals.GetTheInstance().SAS360CON_image_general.Array_codif_management[1] = codif_management[1];

                                    index_estados_booleanos++;

                                    #endregion

                                    #region Digital states

                                    int digital_states_size = Enum.GetNames(typeof(DIGITAL_STATES_IN_LIST)).Length;
                                    for (int index = 0; index < digital_states_size; index++)
                                    {
                                        ushort digital_state_value = (ushort)random.Next(0, ushort.MaxValue);
                                        Globals.GetTheInstance().List_sas360con_image[index_estados_booleanos].Value = digital_state_value;
                                        Globals.GetTheInstance().SAS360CON_image_general.Array_digital_states[index] = digital_state_value;

                                        index_estados_booleanos++;
                                    }

                                    #endregion

                                    uint global_dmsec_counter = (uint)random.Next(0, int.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_estados_booleanos].Value = global_dmsec_counter;

                                    index_estados_booleanos++;
                                }
                                catch { }

                                #endregion

                                #region Entradas analógicas / sensores

                                try
                                {
                                    int index_ea_sensores = Globals.GetTheInstance().List_sas360con_image.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE] + 20);

                                    ushort ea_4v1_power = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_ea_sensores].Value = ea_4v1_power;
                                    Globals.GetTheInstance().SAS360CON_image_general.EA_4v1_power_mv = ea_4v1_power;

                                    index_ea_sensores++;

                                    ushort ea_shunt_leds = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_ea_sensores].Value = ea_shunt_leds;
                                    Globals.GetTheInstance().SAS360CON_image_general.EA_shunt_leds_ma = ea_shunt_leds;

                                    index_ea_sensores++;

                                    ushort voltage_adcref_3v3_mv = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_ea_sensores].Value = voltage_adcref_3v3_mv;
                                    Globals.GetTheInstance().SAS360CON_image_general.Voltage_adcref_3v3_mv = voltage_adcref_3v3_mv;

                                    index_ea_sensores++;

                                    index_ea_sensores += 7; //Reserved
                                }
                                catch { }

                                #endregion


                                #region Tiempo procesado / temporizadores

                                try
                                {
                                    int index_processing_tak = Globals.GetTheInstance().List_sas360con_image.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE] + 30);

                                    Globals.GetTheInstance().List_sas360con_image[index_processing_tak].Value = (ushort)random.Next(ushort.MinValue, ushort.MaxValue);
                                    Globals.GetTheInstance().SAS360CON_image_procesado_contadores.Total_polling_cycle_counter = (ushort)random.Next(ushort.MinValue, ushort.MaxValue);

                                    index_processing_tak++;

                                    index_processing_tak++; //Reserved

                                    Globals.GetTheInstance().List_sas360con_image[index_processing_tak].Value = (ushort)random.Next(ushort.MinValue, ushort.MaxValue);
                                    Globals.GetTheInstance().SAS360CON_image_procesado_contadores.Polling_cycle_execution_time_dmsec = (ushort)random.Next(ushort.MinValue, ushort.MaxValue);

                                    index_processing_tak++;

                                    Globals.GetTheInstance().List_sas360con_image[index_processing_tak].Value = (ushort)random.Next(ushort.MinValue, ushort.MaxValue);
                                    Globals.GetTheInstance().SAS360CON_image_procesado_contadores.Time_processing_int_1msec_actions_dmseg = (ushort)random.Next(ushort.MinValue, ushort.MaxValue);

                                    index_processing_tak++;

                                    Globals.GetTheInstance().List_sas360con_image[index_processing_tak].Value = (ushort)random.Next(ushort.MinValue, ushort.MaxValue);
                                    Globals.GetTheInstance().SAS360CON_image_procesado_contadores.Time_processing_main_10msec_actions_dmseg = (ushort)random.Next(ushort.MinValue, ushort.MaxValue);

                                    index_processing_tak++;

                                    Globals.GetTheInstance().List_sas360con_image[index_processing_tak].Value = (ushort)random.Next(ushort.MinValue, ushort.MaxValue);
                                    Globals.GetTheInstance().SAS360CON_image_procesado_contadores.Time_processing_main_100msec_actions_dmseg = (ushort)random.Next(ushort.MinValue, ushort.MaxValue);

                                    index_processing_tak++;

                                    Globals.GetTheInstance().List_sas360con_image[index_processing_tak].Value = (ushort)random.Next(ushort.MinValue, ushort.MaxValue);
                                    Globals.GetTheInstance().SAS360CON_image_procesado_contadores.Time_processing_main_1sec_actions_dmseg = (ushort)random.Next(ushort.MinValue, ushort.MaxValue);

                                    index_processing_tak++;

                                    Globals.GetTheInstance().List_sas360con_image[index_processing_tak].Value = (ushort)random.Next(ushort.MinValue, ushort.MaxValue);
                                    Globals.GetTheInstance().SAS360CON_image_procesado_contadores.Time_processing_int_1msec_MAX_actions_dmseg = (ushort)random.Next(ushort.MinValue, ushort.MaxValue);

                                    index_processing_tak++;

                                    Globals.GetTheInstance().List_sas360con_image[index_processing_tak].Value = (ushort)random.Next(ushort.MinValue, ushort.MaxValue);
                                    Globals.GetTheInstance().SAS360CON_image_procesado_contadores.Time_processing_int_10msec_MAX_actions_dmseg = (ushort)random.Next(ushort.MinValue, ushort.MaxValue);

                                    index_processing_tak++;
                                }
                                catch { }

                                #endregion


                                #region NVREG management

                                try
                                {
                                    int index_nvreg_management = Globals.GetTheInstance().List_sas360con_image.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE] + 40);

                                    ushort internal_change_counter = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_nvreg_management].Value = internal_change_counter;
                                    Globals.GetTheInstance().SAS360CON_image_nvreg_management.Internal_change_counter = internal_change_counter;

                                    index_nvreg_management++;

                                    ushort config_con_change_counter = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_nvreg_management].Value = config_con_change_counter;
                                    Globals.GetTheInstance().SAS360CON_image_nvreg_management.Config_con_change_counter = config_con_change_counter;

                                    index_nvreg_management++;

                                    ushort config_iot_change_counter = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_nvreg_management].Value = config_iot_change_counter;
                                    Globals.GetTheInstance().SAS360CON_image_nvreg_management.Config_iot_change_counter = config_iot_change_counter;

                                    index_nvreg_management++;

                                    ushort nvreg_change_counter = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_nvreg_management].Value = nvreg_change_counter;
                                    Globals.GetTheInstance().SAS360CON_image_nvreg_management.Nvreg_change_counter = nvreg_change_counter;

                                    index_nvreg_management++;

                                    ushort last_recorded_event_id = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_nvreg_management].Value = last_recorded_event_id;
                                    Globals.GetTheInstance().SAS360CON_image_nvreg_management.Last_recorded_event_absolute_index_copy = last_recorded_event_id;

                                    index_nvreg_management++;

                                    ushort num_recorded_events = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_nvreg_management].Value = num_recorded_events;
                                    Globals.GetTheInstance().SAS360CON_image_nvreg_management.Num_recorded_events_copy = num_recorded_events;

                                    index_nvreg_management++;

                                    ushort last_recorded_event_pos = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_nvreg_management].Value = last_recorded_event_pos;
                                    Globals.GetTheInstance().SAS360CON_image_nvreg_management.Last_recorded_event_array_position_copy = last_recorded_event_pos;

                                    index_nvreg_management++;
                                }
                                catch { }

                                #endregion


                                #region Main management

                                try
                                {
                                    int index_main_management = Globals.GetTheInstance().List_sas360con_image.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE] + 50);

                                    ushort internal_error_codif = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_main_management].Value = internal_error_codif;
                                    Globals.GetTheInstance().SAS360CON_image_main_management.Internal_error = internal_error_codif;

                                    index_main_management++;

                                    ushort error_code_detail = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_main_management].Value = error_code_detail;
                                    Globals.GetTheInstance().SAS360CON_image_main_management.Error_code_detail = error_code_detail;

                                    index_main_management++;

                                    ushort active_warning_id = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_main_management].Value = active_warning_id;
                                    Globals.GetTheInstance().SAS360CON_image_main_management.Active_warning_id = active_warning_id;

                                    index_main_management++;

                                    ushort warning_exceded_T15C_tag_number = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_main_management].Value = warning_exceded_T15C_tag_number;
                                    Globals.GetTheInstance().SAS360CON_image_main_management.Warning_exceded_T15C_tag_number = warning_exceded_T15C_tag_number;

                                    index_main_management++;

                                    ushort last_event_log_rtc = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_main_management].Value = last_event_log_rtc;
                                    Globals.GetTheInstance().SAS360CON_image_main_management.Last_event_log_rtc = last_event_log_rtc;

                                    index_main_management++;

                                    ushort last_event_log_milisec = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_main_management].Value = last_event_log_milisec;
                                    Globals.GetTheInstance().SAS360CON_image_main_management.Last_event_log_msec = last_event_log_milisec;

                                    index_main_management++;

                                    ushort last_event_log_id = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_main_management].Value = last_event_log_id;
                                    Globals.GetTheInstance().SAS360CON_image_main_management.Last_event_log_id = last_event_log_id;

                                    index_main_management++;

                                    Globals.GetTheInstance().SAS360CON_image_main_management.Last_event_log_value = new ushort[2];

                                    ushort[] last_event_log_value = new ushort[2] { (ushort)random.Next(0, ushort.MaxValue), (ushort)random.Next(0, ushort.MaxValue) };

                                    Globals.GetTheInstance().List_sas360con_image[index_main_management].Value = last_event_log_value[0];

                                    index_main_management++;

                                    Globals.GetTheInstance().List_sas360con_image[index_main_management].Value = last_event_log_value[1];
                                    Globals.GetTheInstance().SAS360CON_image_main_management.Last_event_log_value = last_event_log_value;

                                    index_main_management++;
                                }
                                catch { }

                                #endregion


                                #region Lin pooling management

                                try
                                {
                                    int index_lin_pooling = Globals.GetTheInstance().List_sas360con_image.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE] + 60);

                                    Globals.GetTheInstance().SAS360CON_image_lin_pooling.Array_lin_pooling_read_uwb = new ushort[Constants.UWB_TOTAL_COUNT];
                                    Globals.GetTheInstance().SAS360CON_image_lin_pooling.Array_lin_pooling_write_uwb = new ushort[Constants.UWB_TOTAL_COUNT];
                                    Globals.GetTheInstance().SAS360CON_image_lin_pooling.Array_lin_com_total_counter = new ushort[Constants.UWB_TOTAL_COUNT];
                                    Globals.GetTheInstance().SAS360CON_image_lin_pooling.Array_lin_com_error_counter = new ushort[Constants.UWB_TOTAL_COUNT];
                                    Globals.GetTheInstance().SAS360CON_image_lin_pooling.Array_lin_total_last_cycle_time = new ushort[Constants.UWB_TOTAL_COUNT];

                                    for (int index = 0; index < Constants.UWB_TOTAL_COUNT; index++)
                                    {
                                        ushort lin_pooling_read = 1;
                                        Globals.GetTheInstance().List_sas360con_image[index_lin_pooling].Value = lin_pooling_read;
                                        Globals.GetTheInstance().SAS360CON_image_lin_pooling.Array_lin_pooling_read_uwb[index] = lin_pooling_read;
                                        Globals.GetTheInstance().Array_SAS360CON_UWB[index].Pool_read = lin_pooling_read;

                                        index_lin_pooling++;
                                    }

                                    for (int index = 0; index < Constants.UWB_TOTAL_COUNT; index++)
                                    {
                                        ushort lin_pooling_write = 2;
                                        Globals.GetTheInstance().List_sas360con_image[index_lin_pooling].Value = lin_pooling_write;
                                        Globals.GetTheInstance().SAS360CON_image_lin_pooling.Array_lin_pooling_write_uwb[index] = lin_pooling_write;
                                        Globals.GetTheInstance().Array_SAS360CON_UWB[index].Pool_write = lin_pooling_write;

                                        index_lin_pooling++;
                                    }

                                    ushort lin_pooling_write_broadcast = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_lin_pooling].Value = lin_pooling_write_broadcast;
                                    Globals.GetTheInstance().SAS360CON_image_lin_pooling.Lin_pooling_write_broadcast = lin_pooling_write_broadcast;

                                    index_lin_pooling++;

                                    ushort lin_pooling_state = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_lin_pooling].Value = lin_pooling_state;
                                    Globals.GetTheInstance().SAS360CON_image_lin_pooling.Lin_pooling_state = lin_pooling_state;

                                    index_lin_pooling++;

                                    ushort actual_pooled_uwb = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_lin_pooling].Value = actual_pooled_uwb;
                                    Globals.GetTheInstance().SAS360CON_image_lin_pooling.Actual_pooled_uwb = actual_pooled_uwb;

                                    index_lin_pooling++;

                                    ushort actual_pooling_request_group = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_lin_pooling].Value = actual_pooling_request_group;
                                    Globals.GetTheInstance().SAS360CON_image_lin_pooling.Actual_pooling_request_group = actual_pooling_request_group;

                                    index_lin_pooling++;

                                    ushort actual_pooling_request_index = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_lin_pooling].Value = actual_pooling_request_index;
                                    Globals.GetTheInstance().SAS360CON_image_lin_pooling.Actual_pooling_request_index = actual_pooling_request_index;

                                    index_lin_pooling++;

                                    for (int index = 0; index < Constants.UWB_TOTAL_COUNT; index++)
                                    {
                                        ushort lin_com_total_counter = 3;
                                        Globals.GetTheInstance().List_sas360con_image[index_lin_pooling].Value = lin_com_total_counter;
                                        Globals.GetTheInstance().SAS360CON_image_lin_pooling.Array_lin_com_total_counter[index] = lin_com_total_counter;
                                        Globals.GetTheInstance().Array_SAS360CON_UWB[index].Comm_total = lin_com_total_counter;

                                        index_lin_pooling++;
                                    }

                                    for (int index = 0; index < Constants.UWB_TOTAL_COUNT; index++)
                                    {
                                        ushort lin_com_error_counter = 4;
                                        Globals.GetTheInstance().List_sas360con_image[index_lin_pooling].Value = lin_com_error_counter;
                                        Globals.GetTheInstance().SAS360CON_image_lin_pooling.Array_lin_com_error_counter[index] = lin_com_error_counter;
                                        Globals.GetTheInstance().Array_SAS360CON_UWB[index].Com_error = lin_com_error_counter;

                                        index_lin_pooling++;
                                    }

                                    for (int index = 0; index < Constants.UWB_TOTAL_COUNT; index++)
                                    {
                                        ushort lin_total_last_cycle = 5;
                                        Globals.GetTheInstance().List_sas360con_image[index_lin_pooling].Value = lin_total_last_cycle;
                                        Globals.GetTheInstance().SAS360CON_image_lin_pooling.Array_lin_total_last_cycle_time[index] = lin_total_last_cycle;
                                        Globals.GetTheInstance().Array_SAS360CON_UWB[index].Cycle_time = lin_total_last_cycle;

                                        index_lin_pooling++;
                                    }
                                }
                                catch { }

                                #endregion


                                #region CON Processed TAGS

                                try
                                {
                                    int index_con_processed_tags = Globals.GetTheInstance().List_sas360con_image.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE] + 84);

                                    for (int index = 0; index < 2; index++)
                                    {
                                        byte contag_id = (byte)index;
                                        Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value = contag_id;
                                        Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_assigned_self_contag_id[index] = contag_id;

                                        index_con_processed_tags++;
                                    }

                                    for (int index = 0; index < 2; index++)
                                    {
                                        byte drvtag_id = (byte)index;
                                        Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value = drvtag_id;
                                        Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_assigned_self_drvtag_id[index] = drvtag_id;

                                        index_con_processed_tags++;
                                    }

                                    index_con_processed_tags+=4;

                                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                                    {
                                        byte total_tags = (byte)random.Next(0, 4);
                                        Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value = total_tags;
                                        Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_number_total_TAGS_in_area_DANR[index] = total_tags;

                                        index_con_processed_tags++;
                                    }
                                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                                    {
                                        byte total_ped = (byte)random.Next(0, 4);
                                        Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value = total_ped;
                                        Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_number_total_PED_in_area_DANR[index] = total_ped;

                                        index_con_processed_tags++;
                                    }
                                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                                    {
                                        byte total_drv = (byte)random.Next(0, 4);
                                        Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value = total_drv;
                                        Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_number_total_DRV_in_area_DANR[index] = total_drv;

                                        index_con_processed_tags++;
                                    }
                                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                                    {
                                        byte total_lv = (byte)random.Next(0, 4);
                                        Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value = total_lv;
                                        Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_number_total_LV_in_area_DANR[index] = total_lv;

                                        index_con_processed_tags++;
                                    }
                                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                                    {
                                        byte total_hv = (byte)random.Next(0, 4);
                                        Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value = total_hv;
                                        Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_number_total_HV_in_area_DANR[index] = total_hv;

                                        index_con_processed_tags++;
                                    }
                                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                                    {
                                        byte total_zones = (byte)random.Next(0, 4);
                                        Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value = total_zones;
                                        Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_number_total_ZONES_in_area_DANR[index] = total_zones;

                                        index_con_processed_tags++;
                                    }

                                    byte number_zones_slow_range = (byte)random.Next(0, 10);
                                    Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value = number_zones_slow_range;
                                    Globals.GetTheInstance().SAS360CON_image_processed_tag.Number_zones_slow_range = number_zones_slow_range;

                                    index_con_processed_tags++;

                                    index_con_processed_tags++; //Reserved

                                    byte reported_register_uwb_index = (byte)random.Next(0, 10);
                                    Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value = reported_register_uwb_index;
                                    Globals.GetTheInstance().SAS360CON_image_processed_tag.Reported_register_uwb_index = reported_register_uwb_index;

                                    index_con_processed_tags++;

                                    byte reported_register_tag_index = (byte)random.Next(0, 10);
                                    Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value = reported_register_tag_index;
                                    Globals.GetTheInstance().SAS360CON_image_processed_tag.Reported_register_tag_index = reported_register_tag_index;

                                    index_con_processed_tags++;

                                    ushort estado_leds_amarillo = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value = estado_leds_amarillo;
                                    Globals.GetTheInstance().SAS360CON_image_processed_tag.Estado_leds_amarillo = estado_leds_amarillo;

                                    index_con_processed_tags++;

                                    ushort estado_leds_naranja = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value = estado_leds_naranja;
                                    Globals.GetTheInstance().SAS360CON_image_processed_tag.Estado_leds_amarillo = estado_leds_naranja;

                                    index_con_processed_tags++;

                                    ushort estado_leds_rojo = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value = estado_leds_rojo;
                                    Globals.GetTheInstance().SAS360CON_image_processed_tag.Estado_leds_amarillo = estado_leds_rojo;

                                    index_con_processed_tags++;

                                    ushort closest_drvtag_tagID_2lsb = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value = closest_drvtag_tagID_2lsb;
                                    Globals.GetTheInstance().SAS360CON_image_processed_tag.Closest_DRVTAG_tagID_2LSB = closest_drvtag_tagID_2lsb;

                                    index_con_processed_tags++;
                                }
                                catch { }

                                #endregion


                                #region SAS360CON field position

                                try
                                {
                                    int index_field_position = Globals.GetTheInstance().List_sas360con_image.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE] + 110);

                                    int pos_x = random.Next(0, int.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_field_position].Value = pos_x;
                                    Globals.GetTheInstance().SAS360CON_image_field_position.Installation_pos_x_cm = pos_x;

                                    index_field_position++;

                                    int pos_y = random.Next(0, int.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_field_position].Value = pos_y;
                                    Globals.GetTheInstance().SAS360CON_image_field_position.Installation_pos_y_cm = pos_y;

                                    index_field_position++;

                                    int latitud = random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_field_position].Value = latitud;
                                    Globals.GetTheInstance().SAS360CON_image_field_position.Latitud = latitud;

                                    index_field_position++;

                                    int longitud = random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_sas360con_image[index_field_position].Value = longitud;
                                    Globals.GetTheInstance().SAS360CON_image_field_position.Longitud = longitud;

                                    index_field_position++;

                                    index_field_position += 2; //Reserved
                                }
                                catch { }

                                #endregion


                                break;
                            }

                        case MEMORY_CONFIG_TYPE.IOT_IMAGE:
                            {
                                break;
                            }

                        case MEMORY_CONFIG_TYPE.SAS360CON_MAINTENNANCE:
                            {
                                break;
                            }

                        case MEMORY_CONFIG_TYPE.UWB_INTERNAL_CFG:
                            {
                                int index_uwb_internal_cfg = Globals.GetTheInstance().List_uwb_internal_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.UWB_INTERNAL_CFG]);

                                for (int index = 0; index < Constants.UWB_TOTAL_COUNT; index++)
                                {
                                    ushort[] num_serie = new ushort[] { 11, 22, 33, };
                                    Globals.GetTheInstance().List_uwb_internal_cfg[index_uwb_internal_cfg].Value = num_serie[0];

                                    index_uwb_internal_cfg++;

                                    Globals.GetTheInstance().List_uwb_internal_cfg[index_uwb_internal_cfg].Value = num_serie[1];

                                    index_uwb_internal_cfg++;

                                    Globals.GetTheInstance().List_uwb_internal_cfg[index_uwb_internal_cfg].Value = num_serie[2];

                                    index_uwb_internal_cfg++;

                                    index_uwb_internal_cfg += 2; //Reserved

                                    ushort ver_hw = 123;
                                    Globals.GetTheInstance().List_uwb_internal_cfg[index_uwb_internal_cfg].Value = ver_hw;

                                    index_uwb_internal_cfg++;

                                    ushort ver_fw = 456;
                                    Globals.GetTheInstance().List_uwb_internal_cfg[index_uwb_internal_cfg].Value = ver_fw;

                                    index_uwb_internal_cfg++;

                                    ushort ver_boot = 789;
                                    Globals.GetTheInstance().List_uwb_internal_cfg[index_uwb_internal_cfg].Value = ver_boot;

                                    index_uwb_internal_cfg++;

                                    ushort modbus_speed = 5;
                                    Globals.GetTheInstance().List_uwb_internal_cfg[index_uwb_internal_cfg].Value = modbus_speed;

                                    index_uwb_internal_cfg++;

                                    ushort modbus_slave = 1;
                                    Globals.GetTheInstance().List_uwb_internal_cfg[index_uwb_internal_cfg].Value = modbus_slave;

                                    index_uwb_internal_cfg++;

                                    index_uwb_internal_cfg += 4;

                                    int seconds = (int)DateTime.Now.Subtract(Constants.date_ref).TotalSeconds;

                                    Globals.GetTheInstance().List_uwb_internal_cfg[index_uwb_internal_cfg].Value = seconds;

                                    index_uwb_internal_cfg++;

                                    Globals.GetTheInstance().List_uwb_internal_cfg[index_uwb_internal_cfg].Value = seconds;

                                    index_uwb_internal_cfg++;

                                    ushort counter = 100;
                                    Globals.GetTheInstance().List_uwb_internal_cfg[index_uwb_internal_cfg].Value = counter;

                                    index_uwb_internal_cfg++;

                                    ushort crc_interna = 999;
                                    Globals.GetTheInstance().List_uwb_internal_cfg[index_uwb_internal_cfg].Value = crc_interna;

                                    index_uwb_internal_cfg++;
                                }

                                break;
                            }

                        case MEMORY_CONFIG_TYPE.UWB_IMAGE:
                            {
                                int index_uwb_image = Globals.GetTheInstance().List_uwb_image.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.UWB_IMAGE]);

                                for (int index = 0; index < Constants.UWB_TOTAL_COUNT; index++)
                                {
                                    uint u32_rtc_utc = (uint)DateTime.Now.Subtract(Constants.date_ref).TotalSeconds;
                                    DateTime date_rtc_utc = Constants.date_ref.Date.AddSeconds(u32_rtc_utc);

                                    Globals.GetTheInstance().List_uwb_image[index_uwb_image].Value = u32_rtc_utc;
                                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].RTC_UTC_value = u32_rtc_utc;
                                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].RTC_UTC_DATE = date_rtc_utc.ToString(Globals.GetTheInstance().DateFormat, new CultureInfo(Globals.GetTheInstance().DateProvider)); ;

                                    index_uwb_image++;

                                    ushort milliseconds = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_uwb_image[index_uwb_image].Value = milliseconds;
                                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].RTC_millisecs = milliseconds;

                                    index_uwb_image++;

                                    ushort master_watchdog = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_uwb_image[index_uwb_image].Value = master_watchdog;
                                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].Watchdog_inc = master_watchdog;

                                    index_uwb_image++;

                                    ushort codif_state = (ushort)random.Next(0, 15);
                                    Globals.GetTheInstance().List_uwb_image[index_uwb_image].Value = codif_state;
                                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].Codif_state = codif_state;

                                    index_uwb_image++;

                                    ushort number_of_tags_detected = (ushort)random.Next(0, 15);
                                    Globals.GetTheInstance().List_uwb_image[index_uwb_image].Value = number_of_tags_detected;
                                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].Num_tags = number_of_tags_detected;

                                    index_uwb_image++;

                                    ushort number_of_zones_detected = (ushort)random.Next(0, 15);
                                    Globals.GetTheInstance().List_uwb_image[index_uwb_image].Value = number_of_zones_detected;
                                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].Num_zones = number_of_zones_detected;

                                    index_uwb_image++;

                                    ushort delay_in_image_pool = (ushort)random.Next(0, 15);
                                    Globals.GetTheInstance().List_uwb_image[index_uwb_image].Value = delay_in_image_pool;
                                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].Delay_in_image_pool = delay_in_image_pool;

                                    index_uwb_image++;

                                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].Array_contag_id = new byte[Constants.ASSIGNED_ID_SIZE];
                                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].Array_drvtag_id = new byte[Constants.ASSIGNED_ID_SIZE];
                                    for (int index_id = 0; index_id < Constants.ASSIGNED_ID_SIZE; index_id++)
                                    {
                                        byte contag_id_value = (byte)index_id;
                                        Globals.GetTheInstance().List_uwb_image[index_uwb_image].Value = contag_id_value;
                                        Globals.GetTheInstance().Array_SAS360CON_UWB[index].Array_contag_id[index_id] = contag_id_value;

                                        index_uwb_image++;
                                    }

                                    for (int index_id = 0; index_id < Constants.ASSIGNED_ID_SIZE; index_id++)
                                    {
                                        byte drvtag_id_value = (byte)index_id;
                                        Globals.GetTheInstance().List_uwb_image[index_uwb_image].Value = drvtag_id_value;
                                        Globals.GetTheInstance().Array_SAS360CON_UWB[index].Array_drvtag_id[index_id] = drvtag_id_value;

                                        index_uwb_image++;
                                    }

                                    ushort antenna_number_in_CON = (ushort)random.Next(1, 4);
                                    Globals.GetTheInstance().List_uwb_image[index_uwb_image].Value = antenna_number_in_CON;
                                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].Antenna_number_in_CON = antenna_number_in_CON;

                                    index_uwb_image++;

                                    ushort installation_ID = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_uwb_image[index_uwb_image].Value = installation_ID;
                                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].Installation_ID = installation_ID;

                                    index_uwb_image++;

                                    index_uwb_image += 4; //Reserved

                                    ushort reported_register = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_uwb_image[index_uwb_image].Value = reported_register;
                                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].Reported_register = reported_register;

                                    index_uwb_image++;

                                    ushort warning_id = (ushort)random.Next(0, ushort.MaxValue);
                                    Globals.GetTheInstance().List_uwb_image[index_uwb_image].Value = warning_id;
                                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].War_error_id = warning_id;

                                    index_uwb_image++;
                                }

                                break;
                            }

                        case MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE_3:
                            {
                                int list_pos = Globals.GetTheInstance().List_console_closest_tags_base.FindIndex(memory_tags => memory_tags.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.CONSOLE_CLOSEST_TAGS_BASE]);
                                for (int index_tag = 0; index_tag < Globals.GetTheInstance().Total_closest_tags; index_tag++)
                                {
                                    #region Values

                                    ushort tagID_2LSB = (ushort)(Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.CONSOLE_CLOSEST_TAGS_BASE] + (index_tag * Constants.TAGS_BASE_CON_STRUCT_NUM_REG));

                                    int identifier_zone = random.Next(0x00, 0x04);
                                    int tag_type = random.Next(0, 4);
                                    int tagID_byte_3 = (identifier_zone << 4) ^ tag_type;
                                    byte tag_fw_version = (byte)random.Next(0, byte.MaxValue);
                                    byte[] array_values_1 = new byte[] { (byte)tagID_byte_3, tag_fw_version };

                                    ushort tag_estado_codificado = (ushort)random.Next(0, ushort.MaxValue);

                                    short con_calc_tag_position_abs_X_cm = (short)random.Next(-800, 800);
                                    short con_calc_tag_position_abs_Y_cm = (short)random.Next(-800, 800);

                                    ushort tag_received_command = (ushort)random.Next(0, ushort.MaxValue);

                                    int tag_bat_level_pct_u8zone_cfg_dist_m = random.Next(0, 255);
                                    byte tag_consola_code_alarma = (byte)random.Next(0, byte.MaxValue);
                                    byte[] array_values_2 = new byte[] { (byte)tag_bat_level_pct_u8zone_cfg_dist_m, tag_consola_code_alarma };

                                    ushort tag_reported_register = (ushort)random.Next(0, ushort.MaxValue);

                                    byte con_calc_uwb_detection = (byte)random.Next(0, byte.MaxValue);
                                    byte con_worst_time_last_success_decseg = (byte)random.Next(0, byte.MaxValue);

                                    byte con_calc_det_area = (byte)random.Next(0, byte.MaxValue);
                                    byte con_calc_direcion = (byte)random.Next(0, byte.MaxValue);

                                    byte con_calc_led_sector = (byte)random.Next(0, byte.MaxValue);
                                    byte con_calc_num_det_ant = (byte)random.Next(0, byte.MaxValue);

                                    ushort con_dist_closest_cm = (ushort)random.Next(0, ushort.MaxValue);

                                    ushort[] dist_from_antenna = new ushort[4] { (ushort)random.Next(0, ushort.MaxValue), (ushort)random.Next(0, ushort.MaxValue), (ushort)random.Next(0, ushort.MaxValue), (ushort)random.Next(0, ushort.MaxValue) };
                                    byte[] tag_time_last_success_decseg = new byte[4] { (byte)random.Next(0, byte.MaxValue), (byte)random.Next(0, byte.MaxValue), (byte)random.Next(0, byte.MaxValue), (byte)random.Next(0, byte.MaxValue) };

                                    #endregion

                                    #region Save into the list

                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = tagID_2LSB;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = tagID_byte_3;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = tag_fw_version;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = tag_estado_codificado;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = con_calc_tag_position_abs_X_cm;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = con_calc_tag_position_abs_Y_cm;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = tag_received_command;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = tag_bat_level_pct_u8zone_cfg_dist_m;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = tag_consola_code_alarma;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = tag_reported_register;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = con_calc_uwb_detection;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = con_worst_time_last_success_decseg;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = con_calc_det_area;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = con_calc_direcion;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = con_calc_led_sector;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = con_calc_num_det_ant;
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = con_dist_closest_cm;

                                    for (int index = 0; index < Constants.UWB_TOTAL_COUNT; index++)
                                        Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = dist_from_antenna[index];

                                    for (int index = 0; index < Constants.UWB_TOTAL_COUNT; index++)
                                        Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = tag_time_last_success_decseg[index];

                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = byte.MaxValue; //Reserva 
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = byte.MaxValue; //Reserva 
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = byte.MaxValue; //Reserva 
                                    Globals.GetTheInstance().List_console_closest_tags_base[list_pos++].Value = byte.MaxValue; //Reserva 

                                    #endregion

                                    #region Save into the class

                                    SAS360CON_TAG sas360_tag = Globals.GetTheInstance().Array_SAS360CON_TAG[index_tag];
                                    sas360_tag.ID_2LSB = tagID_2LSB;

                                    sas360_tag.Tag_type_value = (byte)tag_type;
                                    sas360_tag.Tag_type_id_grid = $"{tag_type:X}-{tagID_2LSB}";
                                    sas360_tag.Tag_type = Enum.IsDefined(typeof(MASK_TAG_ZONE_TYPE), tag_type) ? (MASK_TAG_ZONE_TYPE)tag_type : MASK_TAG_ZONE_TYPE.UNKNOWN;

                                    #region FW version

                                    sas360_tag.FW_version_value = tag_fw_version;
                                    string s_fw_ver = $"{tag_fw_version:D3}";
                                    string s_fw_ver_dot = string.Empty;
                                    int index_ver = 0;
                                    do
                                        s_fw_ver_dot += $"{s_fw_ver[index_ver]}.";
                                    while (index_ver++ < s_fw_ver.Length - 2);
                                    s_fw_ver_dot += s_fw_ver[index_ver];
                                    sas360_tag.FW_version = s_fw_ver_dot;

                                    #endregion

                                    sas360_tag.Estado_codificado = tag_estado_codificado;
                                    sas360_tag.Estado_ec = (byte)(tag_estado_codificado & 0xFF);
                                    sas360_tag.Estado_rr = (byte)(tag_estado_codificado >> 8);

                                    sas360_tag.Calc_tag_position_abs_X_cm = con_calc_tag_position_abs_X_cm;
                                    sas360_tag.Calc_tag_position_abs_Y_cm = con_calc_tag_position_abs_Y_cm;

                                    sas360_tag.Received_command = tag_received_command;
                                    sas360_tag.Received_command_ec = (byte)(tag_received_command & 0xFF);
                                    sas360_tag.Received_command_rr = (byte)(tag_received_command >> 8);

                                    sas360_tag.Battery_level_pct = (byte)(tag_bat_level_pct_u8zone_cfg_dist_m >> 4);
                                    sas360_tag.Zone_cfg_dist_m = (byte)(tag_bat_level_pct_u8zone_cfg_dist_m & 0X0F);

                                    sas360_tag.Consola_code_alarma = tag_consola_code_alarma;
                                    sas360_tag.Reported_register = tag_reported_register;

                                    sas360_tag.Calc_uwb_detection = con_calc_uwb_detection;
                                    sas360_tag.Worst_time_last_success_decseg = con_worst_time_last_success_decseg;
                                    sas360_tag.Calc_det_area = con_calc_det_area;
                                    sas360_tag.Calc_direccion = con_calc_direcion;
                                    sas360_tag.Calc_led_sector = con_calc_led_sector;
                                    sas360_tag.Calc_num_det_ant = con_calc_num_det_ant;

                                    sas360_tag.Dist_closest_cm = con_dist_closest_cm;

                                    sas360_tag.Dist_from_antenna_cm = new ushort[Constants.UWB_TOTAL_COUNT];
                                    sas360_tag.Dist_from_antenna_grid = string.Empty;
                                    for (int index_uwb = 0; index_uwb < Constants.UWB_TOTAL_COUNT; index_uwb++)
                                    {
                                        sas360_tag.Dist_from_antenna_cm[index_uwb] = dist_from_antenna[index_uwb];
                                        sas360_tag.Dist_from_antenna_grid += $"{dist_from_antenna[index_uwb]} / ";
                                    }

                                    sas360_tag.Tag_time_last_success_decsec = new byte[Constants.UWB_TOTAL_COUNT];
                                    sas360_tag.Time_last_success_grid = string.Empty;
                                    for (int index_uwb = 0; index_uwb < Constants.UWB_TOTAL_COUNT; index_uwb++)
                                    {
                                        sas360_tag.Tag_time_last_success_decsec[index_uwb] = tag_time_last_success_decseg[index_uwb];
                                        sas360_tag.Time_last_success_grid += $"{tag_time_last_success_decseg[index_uwb]} / ";
                                    }

                                    #endregion
                                }

                                break;
                            }

                        case MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_EXTENDED:
                            {
                                break;
                            }

                        case MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_BASE_2:
                            {
                                int list_pos = Globals.GetTheInstance().List_console_closest_zone_base.FindIndex(memory_tags => memory_tags.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.CONSOLE_CLOSEST_ZONE_BASE]);
                                for (int index_zone = 0; index_zone < Globals.GetTheInstance().Total_closest_zone; index_zone++)
                                {
                                    #region Values

                                    ushort zoneID_2LSB = (ushort)(Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.CONSOLE_CLOSEST_ZONE_BASE] + (index_zone * Constants.ZONE_BASE_CON_STRUCT_NUM_REG));

                                    int identifier_zone = random.Next(0, 7);
                                    int zone_type = random.Next(0x05, 0x0A);
                                    int zoneID_byte_3 = (identifier_zone << 4) ^ zone_type;

                                    byte fw_ver = (byte)random.Next(0, byte.MaxValue);
                                    byte[] array_values_1 = new byte[] { (byte)zoneID_byte_3, fw_ver };

                                    ushort zone_estado_codificado = (ushort)random.Next(0, ushort.MaxValue);

                                    short con_calc_tag_position_abs_X_cm = (short)random.Next(-800, 800);
                                    short con_calc_tag_position_abs_Y_cm = (short)random.Next(-800, 800);

                                    ushort received_command = (ushort)random.Next(0, ushort.MaxValue);
                                    ushort radio_action_zona_cm = (ushort)random.Next(40, 100);
                                    ushort reported_reg = (ushort)random.Next(0, ushort.MaxValue);
                                    ushort dist_closest_cm = (ushort)random.Next(0, ushort.MaxValue);

                                    ushort[] dist_from_antenna = new ushort[4] { (ushort)random.Next(0, ushort.MaxValue), (ushort)random.Next(0, ushort.MaxValue), (ushort)random.Next(0, ushort.MaxValue), (ushort)random.Next(0, ushort.MaxValue) };

                                    #endregion

                                    #region Save into the list

                                    Globals.GetTheInstance().List_console_closest_zone_base[list_pos++].Value = zoneID_2LSB;
                                    Globals.GetTheInstance().List_console_closest_zone_base[list_pos++].Value = zoneID_byte_3;
                                    Globals.GetTheInstance().List_console_closest_zone_base[list_pos++].Value = fw_ver;
                                    Globals.GetTheInstance().List_console_closest_zone_base[list_pos++].Value = zone_estado_codificado;
                                    Globals.GetTheInstance().List_console_closest_zone_base[list_pos++].Value = con_calc_tag_position_abs_X_cm;
                                    Globals.GetTheInstance().List_console_closest_zone_base[list_pos++].Value = con_calc_tag_position_abs_Y_cm;
                                    Globals.GetTheInstance().List_console_closest_zone_base[list_pos++].Value = received_command;
                                    Globals.GetTheInstance().List_console_closest_zone_base[list_pos++].Value = radio_action_zona_cm;
                                    Globals.GetTheInstance().List_console_closest_zone_base[list_pos++].Value = reported_reg;

                                    list_pos += 3;

                                    Globals.GetTheInstance().List_console_closest_zone_base[list_pos++].Value = dist_closest_cm;

                                    for (int index = 0; index < 4; index++)
                                        Globals.GetTheInstance().List_console_closest_zone_base[list_pos++].Value = dist_from_antenna[index];

                                    #endregion

                                    #region Save into the class

                                    SAS360CON_ZONE sas360_zone = Globals.GetTheInstance().Array_SAS360CON_ZONE[index_zone];
                                    sas360_zone.ID_2LSB = zoneID_2LSB;

                                    sas360_zone.Zone_type_value = (byte)zone_type;
                                    sas360_zone.Zone_type_id_grid = $"{zone_type}-{zoneID_2LSB}";
                                    sas360_zone.Zone_type = Enum.IsDefined(typeof(MASK_TAG_ZONE_TYPE), zone_type) ? (MASK_TAG_ZONE_TYPE)zone_type : MASK_TAG_ZONE_TYPE.UNKNOWN;

                                    #region FW ver

                                    sas360_zone.FW_version_value = fw_ver;
                                    string s_fw_ver = $"{fw_ver:D3}";
                                    string s_fw_ver_dot = string.Empty;
                                    int index_ver = 0;
                                    do
                                        s_fw_ver_dot += $"{s_fw_ver[index_ver]}.";
                                    while (index_ver++ < s_fw_ver.Length - 2);
                                    s_fw_ver_dot += s_fw_ver[index_ver];
                                    sas360_zone.FW_version = s_fw_ver_dot;

                                    #endregion


                                    sas360_zone.Estado_codificado = zone_estado_codificado;
                                    sas360_zone.Estado_ec = (byte)(zone_estado_codificado & 0xFF);
                                    sas360_zone.Estado_rr = (byte)(zone_estado_codificado >> 8);

                                    sas360_zone.Calc_zone_position_abs_X_cm = con_calc_tag_position_abs_X_cm;
                                    sas360_zone.Calc_zone_position_abs_Y_cm = con_calc_tag_position_abs_Y_cm;

                                    sas360_zone.Received_command = received_command;
                                    sas360_zone.Received_command_ec = (byte)(received_command & 0xFF);
                                    sas360_zone.Received_command_rr = (byte)(received_command >> 8);

                                    sas360_zone.Radio_action = radio_action_zona_cm;
                                    sas360_zone.Reported_register = reported_reg;
                                    sas360_zone.Dist_closest_cm = dist_closest_cm;

                                    sas360_zone.Dist_from_antenna_cm = new ushort[Constants.UWB_TOTAL_COUNT];
                                    sas360_zone.Dist_from_antenna_grid = string.Empty;
                                    for (int index_uwb = 0; index_uwb < Constants.UWB_TOTAL_COUNT; index_uwb++)
                                    {
                                        sas360_zone.Dist_from_antenna_cm[index_uwb] = dist_from_antenna[index_uwb];
                                        sas360_zone.Dist_from_antenna_grid += $"{dist_from_antenna[index_uwb]} / ";
                                    }

                                    #endregion
                                }

                                break;
                            }

                        case MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_EXTENDED:
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

                        case MEMORY_CONFIG_TYPE.SAS360CON_NVREG:
                            {
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_file).Name} -> {nameof(Simulator_data)} -> {memory_config_type} -> {ex.Message}");
            }

            return read_memory_ok;
        }

        #endregion


        #region Load modbus data

        public static void Load_modbus_data(List<ushort> list_data, MEMORY_CONFIG_TYPE memory_config_type)
        {
            try
            {
                if (Globals.GetTheInstance().Depur_mode == BIT_STATE.ON)
                {
                    string s_data = string.Empty;
                    list_data.ForEach(data => s_data += $"0X{data:X4} - ");

                    Manage_logs.SaveModbusValue($"RECEIVED HOLDING REGISTER ({memory_config_type}) -> DATA: {s_data}");
                    Manage_logs.SaveModbusValue("---------------------------------------------------------------------------------");
                    Manage_logs.SaveDataValue("---------------------------------------------------------------------------------");
                }


                switch (memory_config_type)
                {
                    case MEMORY_CONFIG_TYPE.SAS360CON_INTERNAL_CFG:
                        {
                            Convert_SAS360CON_internal_cfg_modbus_data(list_data.ToArray());
                            Load_SAS360CON_internal_cfg_modbus_data();

                            break;
                        }

                    case MEMORY_CONFIG_TYPE.SAS360CON_CFG:
                        {
                            Convert_SAS360CON_cfg_modbus_data(list_data.ToArray());
                            Load_SAS360CON_cfg_modbus_data();

                            break;
                        }

                    case MEMORY_CONFIG_TYPE.IOT_CFG:
                        {
                            break;
                        }

                    case MEMORY_CONFIG_TYPE.SAS360CON_IMAGE:
                        {
                            Convert_SAS360CON_image_modbus_data(list_data.ToArray());
                            Load_SAS360CON_image_modbus_data();

                            break;
                        }

                    case MEMORY_CONFIG_TYPE.IOT_IMAGE:
                        {
                            break;
                        }

                    case MEMORY_CONFIG_TYPE.SAS360CON_MAINTENNANCE:
                        {
                            Convert_SAS360CON_maintennance_modbus_data(list_data.ToArray());
                            Load_SAS360CON_maintennance_modbus_data();

                            break;
                        }


                    case MEMORY_CONFIG_TYPE.UWB_INTERNAL_CFG:
                        {
                            Convert_UWB_internal_cfg_modbus_data(list_data.ToArray());
                            Load_UWB_internal_cfg_modbus_data();

                            break;
                        }

                    case MEMORY_CONFIG_TYPE.UWB_IMAGE:
                        {
                            Convert_UWB_image_modbus_data(list_data.ToArray());
                            Load_UWB_image_modbus_data();

                            break;
                        }


                    case MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE_1:
                        {
                            Globals.GetTheInstance().Array_data_closest_tag_base_1 = list_data.ToArray();

                            break;
                        }

                    case MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE_2:
                        {
                            Globals.GetTheInstance().Array_data_closest_tag_base_2 = list_data.ToArray();

                            break;
                        }

                    case MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_BASE_3:
                        {
                            Globals.GetTheInstance().Array_data_closest_tag_base_3 = list_data.ToArray();
                            Globals.GetTheInstance().Array_data_closest_tag_base_all = Globals.GetTheInstance().Array_data_closest_tag_base_1.Concat(Globals.GetTheInstance().Array_data_closest_tag_base_2).Concat(Globals.GetTheInstance().Array_data_closest_tag_base_3).ToArray();

                            Convert_CON_TAG_BASE_modbus_data(Globals.GetTheInstance().Array_data_closest_tag_base_all);

                            if (Globals.GetTheInstance().Depur_mode == BIT_STATE.ON)
                            {
                                string s_data_tags = string.Empty;
                                Globals.GetTheInstance().List_console_closest_tags_base.ForEach(modbus_var => s_data_tags += $"0X{modbus_var.Value:X4} - ");

                                Manage_logs.SaveLogValue($"CLOSEST TAGS BASE CONCAT DATA -> {s_data_tags}");
                                Manage_logs.SaveLogValue($"-------------------------------------------------------------");
                            }

                            Load_CON_TAG_base_modbus_data();
     
                            break;
                        }

                    case MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_TAGS_EXTENDED:
                        {
                            break;
                        }

                    case MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_BASE_1:
                        {
                            Globals.GetTheInstance().Array_data_closest_zone_base_1 = list_data.ToArray();
                            break;
                        }

                    case MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_BASE_2:
                        {
                            Globals.GetTheInstance().Array_data_closest_zone_base_2 = list_data.ToArray();

                            Globals.GetTheInstance().Array_data_closest_zone_base_all = Globals.GetTheInstance().Array_data_closest_zone_base_1.Concat(Globals.GetTheInstance().Array_data_closest_zone_base_2).ToArray();

                            Convert_CON_ZONE_BASE_modbus_data(Globals.GetTheInstance().Array_data_closest_zone_base_all);
                            Load_CON_ZONE_base_modbus_data();

                            break;
                        }

                    case MEMORY_CONFIG_TYPE.CONSOLE_CLOSEST_ZONE_EXTENDED:
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

                    case MEMORY_CONFIG_TYPE.SAS360CON_NVREG:
                        {
                            Convert_SAS360CON_nvreg_modbus_data(list_data.ToArray());
                            Load_SAS360CON_nvreg_modbus_data();

                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Load_modbus_data)} -> {memory_config_type} -> {ex.Message}");
            }
        }

        #endregion


        #region Convert modbus register to type value

        public static void Convert_SAS360CON_internal_cfg_modbus_data(ushort[] array_data)
        {
            try
            {
                int sas360con_internal_cfg_to_list_pos = 0;
                for (int index = 0; index < array_data.Length; index++)
                {
                    if (Globals.GetTheInstance().List_sas360con_internal_cfg_u32_pos.Contains(index))
                    {
                        ushort[] array_ushort_values = new ushort[] { array_data[index], array_data[index + 1] };
                        byte[] array_byte_values_1 = BitConverter.GetBytes(array_ushort_values[0]);
                        byte[] array_byte_values_2 = BitConverter.GetBytes(array_ushort_values[1]);
                        index++;

                        byte[] array_byte_values_concat = array_byte_values_1.Concat(array_byte_values_2).ToArray();
                        uint uint_value = BitConverter.ToUInt32(array_byte_values_concat);

                        Globals.GetTheInstance().List_sas360con_internal_cfg[sas360con_internal_cfg_to_list_pos].Value = uint_value;
                    }
                    else
                    {
                        Globals.GetTheInstance().List_sas360con_internal_cfg[sas360con_internal_cfg_to_list_pos].Value = array_data[index];
                    }

                    sas360con_internal_cfg_to_list_pos++;
                }
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Convert_SAS360CON_internal_cfg_modbus_data)} -> {ex.Message}");
            }
        }

        public static void Convert_SAS360CON_cfg_modbus_data(ushort[] array_data)
        {
            try
            {
                int sas360con_cfg_to_list_pos = 0;
                for (int index = 0; index < array_data.Length; index++)
                {
                    if (Globals.GetTheInstance().List_sas360con_cfg_u32_pos.Contains(index))
                    {
                        ushort[] array_ushort_values = new ushort[] { array_data[index], array_data[index + 1] };
                        byte[] array_byte_values_1 = BitConverter.GetBytes(array_ushort_values[0]);
                        byte[] array_byte_values_2 = BitConverter.GetBytes(array_ushort_values[1]);
                        index++;

                        byte[] array_byte_values_concat = array_byte_values_1.Concat(array_byte_values_2).ToArray();
                        uint uint_value = BitConverter.ToUInt32(array_byte_values_concat);
                        Globals.GetTheInstance().List_sas360con_cfg[sas360con_cfg_to_list_pos].Value = uint_value;
                    }
                    else if (Globals.GetTheInstance().List_sas360con_cfg_byte_pos.Contains(index))
                    {
                        byte[] array_values = BitConverter.GetBytes(array_data[index]);
                        Globals.GetTheInstance().List_sas360con_cfg[sas360con_cfg_to_list_pos].Value = array_values[0];
                        Globals.GetTheInstance().List_sas360con_cfg[++sas360con_cfg_to_list_pos].Value = array_values[1];
                    }
                    else if (Globals.GetTheInstance().List_sas360con_cfg_int16_pos.Contains(index))
                    {
                        Globals.GetTheInstance().List_sas360con_cfg[sas360con_cfg_to_list_pos].Value = (short)array_data[index];
                    }
                    else
                    {
                        Globals.GetTheInstance().List_sas360con_cfg[sas360con_cfg_to_list_pos].Value = (ushort)array_data[index];
                    }

                    sas360con_cfg_to_list_pos++;
                }
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Convert_SAS360CON_cfg_modbus_data)} -> {ex.Message}");
            }
        }

        public static void Convert_SAS360CON_image_modbus_data(ushort[] array_data)
        {
            try
            {
                int sas360con_image_pos = 0;
                for (int index = 0; index < array_data.Length; index++)
                {
                    if (Globals.GetTheInstance().List_sas360con_image_u32_pos.Contains(index))
                    {
                        ushort[] array_ushort_values = new ushort[] { array_data[index], array_data[index + 1] };
                        byte[] array_byte_values_1 = BitConverter.GetBytes(array_ushort_values[0]);
                        byte[] array_byte_values_2 = BitConverter.GetBytes(array_ushort_values[1]);
                        index++;

                        byte[] array_byte_values_concat = array_byte_values_1.Concat(array_byte_values_2).ToArray();
                        uint uint_value = BitConverter.ToUInt32(array_byte_values_concat);
                        Globals.GetTheInstance().List_sas360con_image[sas360con_image_pos].Value = uint_value;
                    }
                    else if (Globals.GetTheInstance().List_sas360con_image_s32_pos.Contains(index))
                    {
                        ushort[] array_ushort_values = new ushort[] { array_data[index], array_data[index + 1] };
                        byte[] array_byte_values_1 = BitConverter.GetBytes(array_ushort_values[0]);
                        byte[] array_byte_values_2 = BitConverter.GetBytes(array_ushort_values[1]);
                        index++;

                        byte[] array_byte_values_concat = array_byte_values_1.Concat(array_byte_values_2).ToArray();
                        int int_value = BitConverter.ToInt32(array_byte_values_concat);
                        Globals.GetTheInstance().List_sas360con_image[sas360con_image_pos].Value = int_value;
                    }
                    else if (Globals.GetTheInstance().List_sas360con_image_float_pos.Contains(index))
                    {
                        ushort[] array_ushort_values = new ushort[] { array_data[index], array_data[index + 1] };
                        byte[] array_byte_values_1 = BitConverter.GetBytes(array_ushort_values[0]);
                        byte[] array_byte_values_2 = BitConverter.GetBytes(array_ushort_values[1]);
                        index++;

                        byte[] array_byte_values_concat = array_byte_values_1.Concat(array_byte_values_2).ToArray();
                        float float_value = BitConverter.ToSingle(array_byte_values_concat);

                        Globals.GetTheInstance().List_sas360con_image[sas360con_image_pos].Value = float_value;
                    }
                    else if (Globals.GetTheInstance().List_sas360con_image_byte_pos.Contains(index))
                    {
                        byte[] array_values = BitConverter.GetBytes(array_data[index]);
                        Globals.GetTheInstance().List_sas360con_image[sas360con_image_pos].Value = array_values[0];
                        Globals.GetTheInstance().List_sas360con_image[sas360con_image_pos + 1].Value = array_values[1];
                        sas360con_image_pos++;
                    }
                    else
                    {
                        Globals.GetTheInstance().List_sas360con_image[sas360con_image_pos].Value = array_data[index];
                    }

                    sas360con_image_pos++;
                }
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Convert_SAS360CON_image_modbus_data)} -> {ex.Message}");
            }
        }


        public static void Convert_SAS360CON_maintennance_modbus_data(ushort[] array_data)
        {
            try
            {
                int sas360con_maintennance_pos = 0;
                for (int index = 0; index < array_data.Length; index++)
                {
                    if (Globals.GetTheInstance().List_sas360con_maintennance_u32_pos.Contains(index))
                    {
                        ushort[] array_ushort_values = new ushort[] { array_data[index], array_data[index + 1] };
                        byte[] array_byte_values_1 = BitConverter.GetBytes(array_ushort_values[0]);
                        byte[] array_byte_values_2 = BitConverter.GetBytes(array_ushort_values[1]);
                        index++;

                        byte[] array_byte_values_concat = array_byte_values_1.Concat(array_byte_values_2).ToArray();
                        uint uint_value = BitConverter.ToUInt32(array_byte_values_concat);
                        Globals.GetTheInstance().List_sas360con_maintennance[sas360con_maintennance_pos].Value = uint_value;
                    }
                    else if (Globals.GetTheInstance().List_sas360con_maintennance_byte_pos.Contains(index))
                    {
                        byte[] array_values = BitConverter.GetBytes(array_data[index]);
                        Globals.GetTheInstance().List_sas360con_maintennance[sas360con_maintennance_pos].Value = array_values[0];
                        Globals.GetTheInstance().List_sas360con_maintennance[sas360con_maintennance_pos + 1].Value = array_values[1];
                        sas360con_maintennance_pos++;
                    }
                    else
                    {
                        Globals.GetTheInstance().List_sas360con_maintennance[sas360con_maintennance_pos].Value = array_data[index];
                    }

                    sas360con_maintennance_pos++;
                }
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Convert_SAS360CON_maintennance_modbus_data)} -> {ex.Message}");
            }
        }



        public static void Convert_UWB_internal_cfg_modbus_data(ushort[] array_data)
        {
            try
            {
                int uwb_image_to_list_pos = 0;
                for (int index = 0; index < array_data.Length; index++)
                {
                    if (Globals.GetTheInstance().List_uwb_internal_cfg_u32_pos.Contains(index))
                    {
                        ushort[] array_ushort_values = new ushort[] { array_data[index], array_data[index + 1] };
                        byte[] array_byte_values_1 = BitConverter.GetBytes(array_ushort_values[0]);
                        byte[] array_byte_values_2 = BitConverter.GetBytes(array_ushort_values[1]);
                        index++;

                        byte[] array_byte_values_concat = array_byte_values_1.Concat(array_byte_values_2).ToArray();
                        uint uint_value = BitConverter.ToUInt32(array_byte_values_concat);
                        Globals.GetTheInstance().List_uwb_internal_cfg[uwb_image_to_list_pos].Value = uint_value;
                    }
                    else
                    {
                        Globals.GetTheInstance().List_uwb_internal_cfg[uwb_image_to_list_pos].Value = array_data[index];
                    }

                    uwb_image_to_list_pos++;
                }
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Convert_UWB_internal_cfg_modbus_data)} -> {ex.Message}");
            }
        }

        public static void Convert_UWB_image_modbus_data(ushort[] array_data)
        {
            try
            {
                int uwb_image_to_list_pos = 0;
                for (int index = 0; index < array_data.Length; index++)
                {
                    if (Globals.GetTheInstance().List_uwb_image_u32_pos.Contains(index))
                    {
                        ushort[] array_ushort_values = new ushort[] { array_data[index], array_data[index + 1] };
                        byte[] array_byte_values_1 = BitConverter.GetBytes(array_ushort_values[0]);
                        byte[] array_byte_values_2 = BitConverter.GetBytes(array_ushort_values[1]);
                        index++;

                        byte[] array_byte_values_concat = array_byte_values_1.Concat(array_byte_values_2).ToArray();
                        uint uint_value = BitConverter.ToUInt32(array_byte_values_concat);
                        Globals.GetTheInstance().List_uwb_image[uwb_image_to_list_pos].Value = uint_value;
                    }
                    else if (Globals.GetTheInstance().List_uwb_image_byte_pos.Contains(index))
                    {
                        byte[] array_values = BitConverter.GetBytes(array_data[index]);
                        Globals.GetTheInstance().List_uwb_image[uwb_image_to_list_pos].Value = array_values[0];
                        Globals.GetTheInstance().List_uwb_image[++uwb_image_to_list_pos].Value = array_values[1];
                    }
                    else
                    {
                        Globals.GetTheInstance().List_uwb_image[uwb_image_to_list_pos].Value = array_data[index];
                    }

                    uwb_image_to_list_pos++;
                }
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Convert_UWB_image_modbus_data)} -> {ex.Message}");
            }
        }


        public static void Convert_CON_TAG_BASE_modbus_data(ushort[] array_data)
        {
            try
            {
                int console_tags_base_pos = 0;
                for (int index = 0; index < Globals.GetTheInstance().Array_data_closest_tag_base_all.Length; index++)
                {
                    if (Globals.GetTheInstance().List_console_closest_tags_base_byte_pos.Contains(index))
                    {
                        byte[] array_values = BitConverter.GetBytes(Globals.GetTheInstance().Array_data_closest_tag_base_all[index]);
                        Globals.GetTheInstance().List_console_closest_tags_base[console_tags_base_pos].Value = array_values[0];
                        Globals.GetTheInstance().List_console_closest_tags_base[++console_tags_base_pos].Value = array_values[1];
                    }
                    else if (Globals.GetTheInstance().List_console_closest_tags_base_int16_pos.Contains(index))
                    {
                        Globals.GetTheInstance().List_console_closest_tags_base[console_tags_base_pos].Value = (short)array_data[index];
                    }
                    else
                    {
                        Globals.GetTheInstance().List_console_closest_tags_base[console_tags_base_pos].Value = array_data[index];
                    }

                    console_tags_base_pos++;
                }
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Convert_CON_TAG_BASE_modbus_data)} -> {ex.Message}");
            }
        }

        public static void Convert_CON_ZONE_BASE_modbus_data(ushort[] array_data)
        {
            try
            {
                int console_zone_base_pos = 0;
                for (int index = 0; index < array_data.Length; index++)
                {
                    if (Globals.GetTheInstance().List_console_closest_zone_base_byte_pos.Contains(index))
                    {
                        byte[] array_values = BitConverter.GetBytes(Globals.GetTheInstance().Array_data_closest_zone_base_all[index]);
                        Globals.GetTheInstance().List_console_closest_zone_base[console_zone_base_pos].Value = array_values[0];
                        Globals.GetTheInstance().List_console_closest_zone_base[++console_zone_base_pos].Value = array_values[1];
                    }
                    else if (Globals.GetTheInstance().List_console_closest_zone_base_int16_pos.Contains(index))
                    {
                        Globals.GetTheInstance().List_console_closest_zone_base[console_zone_base_pos].Value = (short)array_data[index];
                    }
                    else
                    {
                        Globals.GetTheInstance().List_console_closest_zone_base[console_zone_base_pos].Value = (ushort)array_data[index];
                    }

                    console_zone_base_pos++;
                }
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Convert_CON_ZONE_BASE_modbus_data)} -> {ex.Message}");
            }
        }



        public static void Convert_SAS360CON_nvreg_modbus_data(ushort[] array_data)
        {
            try
            {
                int sas360con_nvreg_pos = 0;
                for (int index = 0; index < array_data.Length; index++)
                {
                    if (Globals.GetTheInstance().List_sas360con_nvreg_u32_pos.Contains(index))
                    {
                        ushort[] array_ushort_values = new ushort[] { array_data[index], array_data[index + 1] };
                        byte[] array_byte_values_1 = BitConverter.GetBytes(array_ushort_values[0]);
                        byte[] array_byte_values_2 = BitConverter.GetBytes(array_ushort_values[1]);
                        index++;

                        byte[] array_byte_values_concat = array_byte_values_1.Concat(array_byte_values_2).ToArray();
                        uint uint_value = BitConverter.ToUInt32(array_byte_values_concat);
                        Globals.GetTheInstance().List_sas360con_nvreg[sas360con_nvreg_pos].Value = uint_value;
                    }
                    else if (Globals.GetTheInstance().List_sas360con_nvreg_byte_pos.Contains(index))
                    {
                        byte[] array_values = BitConverter.GetBytes(array_data[index]);
                        Globals.GetTheInstance().List_sas360con_nvreg[sas360con_nvreg_pos].Value = array_values[0];
                        Globals.GetTheInstance().List_sas360con_nvreg[sas360con_nvreg_pos + 1].Value = array_values[1];
                        sas360con_nvreg_pos++;
                    }
                    else
                    {
                        Globals.GetTheInstance().List_sas360con_nvreg[sas360con_nvreg_pos].Value = array_data[index];
                    }

                    sas360con_nvreg_pos++;
                }
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Convert_SAS360CON_nvreg_modbus_data)} -> {ex.Message}");
            }
        }

        #endregion

        #region Load modbus memory type

        #region Load SAS360CON internal cfg memory data

        public static void Load_SAS360CON_internal_cfg_modbus_data()
        {
            try
            {
                int sas360con_internal_cfg_from_list_pos = 0;

                #region Serial number

                Globals.GetTheInstance().SAS360CON_internal_cfg.Serial_number =
                    $"{Globals.GetTheInstance().List_sas360con_internal_cfg[sas360con_internal_cfg_from_list_pos].Value:D4}." +
                    $"{Globals.GetTheInstance().List_sas360con_internal_cfg[sas360con_internal_cfg_from_list_pos + 1].Value:D4}." +
                    $"{Globals.GetTheInstance().List_sas360con_internal_cfg[sas360con_internal_cfg_from_list_pos + 2].Value:D4}";

                #endregion

                sas360con_internal_cfg_from_list_pos += 3;

                Globals.GetTheInstance().SAS360CON_internal_cfg.ID_manufacturing = Globals.GetTheInstance().List_sas360con_internal_cfg[sas360con_internal_cfg_from_list_pos].Value!.ToString()!;

                sas360con_internal_cfg_from_list_pos++;

                int tag_type = (ushort)Globals.GetTheInstance().List_sas360con_internal_cfg[sas360con_internal_cfg_from_list_pos].Value & 0X0F;
                Globals.GetTheInstance().SAS360CON_internal_cfg.Tag_type = Enum.IsDefined(typeof(MASK_TAG_ZONE_TYPE), tag_type) ? (MASK_TAG_ZONE_TYPE)tag_type : MASK_TAG_ZONE_TYPE.UNKNOWN;

                sas360con_internal_cfg_from_list_pos++;

                #region Versiones

                Globals.GetTheInstance().SAS360CON_internal_cfg.Version_hw = string.Empty;
                string s_ver_hw = $"{Globals.GetTheInstance().List_sas360con_internal_cfg[sas360con_internal_cfg_from_list_pos].Value:D3}";
                s_ver_hw = s_ver_hw.Length >= 2 ? $"{s_ver_hw[..^2]}.{s_ver_hw.Substring(s_ver_hw.Length - 2, 2)}" : "";
                Globals.GetTheInstance().SAS360CON_internal_cfg.Version_hw = s_ver_hw;

                sas360con_internal_cfg_from_list_pos++;


                Globals.GetTheInstance().SAS360CON_internal_cfg.Version_fw = string.Empty;
                string s_ver_fw = $"{Globals.GetTheInstance().List_sas360con_internal_cfg[sas360con_internal_cfg_from_list_pos].Value:D3}";
                s_ver_fw = s_ver_fw.Length >= 2 ? $"{s_ver_fw[..^2]}.{s_ver_fw.Substring(s_ver_fw.Length - 2, 2)}" : "";
                Globals.GetTheInstance().SAS360CON_internal_cfg.Version_fw = s_ver_fw;

                sas360con_internal_cfg_from_list_pos++;


                Globals.GetTheInstance().SAS360CON_internal_cfg.Version_boot = string.Empty;
                string s_ver_boot = $"{Globals.GetTheInstance().List_sas360con_internal_cfg[sas360con_internal_cfg_from_list_pos].Value:D3}";
                s_ver_boot = s_ver_boot.Length >= 2 ? $"{s_ver_boot[..^2]}.{s_ver_boot.Substring(s_ver_boot.Length - 2, 2)}" : "";
                Globals.GetTheInstance().SAS360CON_internal_cfg.Version_boot = s_ver_boot;

                sas360con_internal_cfg_from_list_pos++;

                #endregion

                Globals.GetTheInstance().SAS360CON_internal_cfg.RTU_slave_speed = SAS360CON_MODBUS_SPEED((ushort)Globals.GetTheInstance().List_sas360con_internal_cfg[sas360con_internal_cfg_from_list_pos].Value);

                sas360con_internal_cfg_from_list_pos++;

                Globals.GetTheInstance().SAS360CON_internal_cfg.RTU_slave_num = (ushort)Globals.GetTheInstance().List_sas360con_internal_cfg[sas360con_internal_cfg_from_list_pos].Value;

                sas360con_internal_cfg_from_list_pos++;

                Globals.GetTheInstance().SAS360CON_internal_cfg.Lin_master_speed = SAS360CON_MODBUS_SPEED((ushort)Globals.GetTheInstance().List_sas360con_internal_cfg[sas360con_internal_cfg_from_list_pos].Value);

                sas360con_internal_cfg_from_list_pos++;

                Globals.GetTheInstance().SAS360CON_internal_cfg.Consola_id = Globals.GetTheInstance().List_sas360con_internal_cfg[sas360con_internal_cfg_from_list_pos].Value;

                sas360con_internal_cfg_from_list_pos++;

                sas360con_internal_cfg_from_list_pos += 2; //Reserved

                uint u32_rtc_fw = Globals.GetTheInstance().List_sas360con_internal_cfg[sas360con_internal_cfg_from_list_pos].Value;
                DateTime date_rtc_fw = Constants.date_ref.Date.AddSeconds(u32_rtc_fw);
                Globals.GetTheInstance().SAS360CON_internal_cfg.RTC_fw_update = date_rtc_fw.ToString(Globals.GetTheInstance().DateFormat, new CultureInfo(Globals.GetTheInstance().DateProvider));

                sas360con_internal_cfg_from_list_pos++;

                uint u32_rtc_config = Globals.GetTheInstance().List_sas360con_internal_cfg[sas360con_internal_cfg_from_list_pos].Value;
                DateTime date_rtc_config = Constants.date_ref.Date.AddSeconds(u32_rtc_config);
                Globals.GetTheInstance().SAS360CON_internal_cfg.RTC_config_update = date_rtc_config.ToString(Globals.GetTheInstance().DateFormat, new CultureInfo(Globals.GetTheInstance().DateProvider));

                sas360con_internal_cfg_from_list_pos++;

                Globals.GetTheInstance().SAS360CON_internal_cfg.Change_counter = Globals.GetTheInstance().List_sas360con_internal_cfg[sas360con_internal_cfg_from_list_pos].Value;

                sas360con_internal_cfg_from_list_pos++;

                Globals.GetTheInstance().SAS360CON_internal_cfg.CRC_config = Globals.GetTheInstance().List_sas360con_internal_cfg[sas360con_internal_cfg_from_list_pos].Value;

            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Load_SAS360CON_internal_cfg_modbus_data)} -> {ex.Message}");
            }
        }

        #endregion

        #region Load SAS360CON cfg memory data

        public static void Load_SAS360CON_cfg_modbus_data()
        {
            try
            {
                #region Installation client definition

                try
                {
                    int index_installation_client = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + 2);

                    Globals.GetTheInstance().SAS360CON_cfg_installation_client.Client = Globals.GetTheInstance().List_sas360con_cfg[index_installation_client].Value;

                    index_installation_client++;

                    Globals.GetTheInstance().SAS360CON_cfg_installation_client.Installation = Globals.GetTheInstance().List_sas360con_cfg[index_installation_client].Value;

                    index_installation_client++;

                    Globals.GetTheInstance().SAS360CON_cfg_installation_client.Vehicle_type = Globals.GetTheInstance().List_sas360con_cfg[index_installation_client].Value;

                    index_installation_client++;

                    Globals.GetTheInstance().SAS360CON_cfg_installation_client.Audio_language = Globals.GetTheInstance().List_sas360con_cfg[index_installation_client].Value;

                    index_installation_client++;

                    Globals.GetTheInstance().SAS360CON_cfg_installation_client.Audio_volume = Globals.GetTheInstance().List_sas360con_cfg[index_installation_client].Value;

                    index_installation_client++;

                    index_installation_client++; //Reserved
                }
                catch { }

                #endregion

                #region Vehicle configuration

                try
                {
                    int index_vehicle_config = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + 8);

                    Globals.GetTheInstance().SAS360CON_cfg_vehicle_cfg.Vehicle_dim_xy_cm = new ushort[2] { Globals.GetTheInstance().List_sas360con_cfg[index_vehicle_config].Value, Globals.GetTheInstance().List_sas360con_cfg[index_vehicle_config + 1].Value };

                    index_vehicle_config += 2;

                    Globals.GetTheInstance().SAS360CON_cfg_vehicle_cfg.Antenna_xy_cm = new short[Constants.ANTENNA_COUNT, 2];
                    for (int index_lin = 0; index_lin < Constants.ANTENNA_COUNT; index_lin++)
                    {
                        Globals.GetTheInstance().SAS360CON_cfg_vehicle_cfg.Antenna_xy_cm[index_lin, 0] = Globals.GetTheInstance().List_sas360con_cfg[index_vehicle_config].Value;
                        Globals.GetTheInstance().SAS360CON_cfg_vehicle_cfg.Antenna_xy_cm[index_lin, 1] = Globals.GetTheInstance().List_sas360con_cfg[index_vehicle_config + 1].Value;

                        index_vehicle_config += 2;
                    }

                    index_vehicle_config += 4; //Reserved
                }
                catch { }

                #endregion

                #region Detection area definition

                try
                {
                    int index_detection_area = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + 20);

                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                    {
                        Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_FRONT_ANRI_dist_cm[index] = Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Value;
                        index_detection_area++;
                    }
                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                    {
                        Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_RIGHT_ANRI_dist_cm[index] = Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Value;
                        index_detection_area++;
                    }
                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                    {
                        Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_BACK_ANRI_dist_cm[index] = Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Value;
                        index_detection_area++;
                    }
                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                    {
                        Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_LEFT_ANRI_dist_cm[index] = Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Value;
                        index_detection_area++;
                    }

                    Globals.GetTheInstance().SAS360CON_cfg_detection_area.Area_detection_distance_cm = Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Value;

                    index_detection_area++;

                    Globals.GetTheInstance().SAS360CON_cfg_detection_area.Area_change_hysteresis_cent_pct = Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Value;

                    index_detection_area++;

                    Globals.GetTheInstance().SAS360CON_cfg_detection_area.Sector_change_hysteresis_cent_pct = Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Value;

                    index_detection_area++;

                    Globals.GetTheInstance().SAS360CON_cfg_detection_area.Trilat_calc_enabled = Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Value;

                    index_detection_area++;

                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                    {
                        Globals.GetTheInstance().SAS360CON_cfg_detection_area.Array_area_DIST_ANTENA_ANRI_dist_cm[index] = Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Value;
                        index_detection_area++;
                    }


                    Globals.GetTheInstance().SAS360CON_cfg_detection_area.Gestion_avanzada_position_enable= Globals.GetTheInstance().List_sas360con_cfg[index_detection_area].Value;

                    index_detection_area++;

                    index_detection_area += 5; //Reserved
                }
                catch { }

                #endregion

                #region Actuaciones E / S

                try
                {
                    int index_actuaciones_e_a = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + 50);

                    Globals.GetTheInstance().SAS360CON_cfg_general.Array_actuaciones_salidas = new ushort[Enum.GetNames(typeof(ACTUACIONES_SALIDAS_POS_IN_ARRAY)).Length];
                    for (int index = 0; index < Enum.GetNames(typeof(ACTUACIONES_SALIDAS_POS_IN_ARRAY)).Length; index++)
                    {
                        Globals.GetTheInstance().SAS360CON_cfg_general.Array_actuaciones_salidas[index] = Globals.GetTheInstance().List_sas360con_cfg[index_actuaciones_e_a].Value;
                        index_actuaciones_e_a++;
                    }

                    index_actuaciones_e_a += 4; //Reserved
                }
                catch { }

                #endregion

                #region Temporizadores y filtros

                try
                {
                    int index_temp = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + 60);

                    Globals.GetTheInstance().SAS360CON_cfg_general.Output_deactivation_delay_sec = Globals.GetTheInstance().List_sas360con_cfg[index_temp].Value;

                    index_temp++;

                    Globals.GetTheInstance().SAS360CON_cfg_general.Area_zone_dist_cm = Globals.GetTheInstance().List_sas360con_cfg[index_temp].Value;

                    index_temp++;

                    Globals.GetTheInstance().SAS360CON_cfg_general.Red_zone_alert_audio_repeat_sec = Globals.GetTheInstance().List_sas360con_cfg[index_temp].Value;

                    index_temp++;

                    index_temp += 7; //Reserved
                }
                catch { }

                #endregion

                #region UWB COM config reserved

                try
                {
                    int index_uwb_com_config = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + 70);

                    for (int index_uwb = 0; index_uwb < Constants.UWB_TOTAL_COUNT; index_uwb++)
                    {
                        Globals.GetTheInstance().SAS360CON_cfg_general.Array_lin_used[index_uwb] = Globals.GetTheInstance().List_sas360con_cfg[index_uwb_com_config].Value;
                        Globals.GetTheInstance().Array_SAS360CON_UWB[index_uwb].Lin = Globals.GetTheInstance().List_sas360con_cfg[index_uwb_com_config].Value;

                        index_uwb_com_config++;
                    }
                    for (int index_uwb = 0; index_uwb < Constants.UWB_TOTAL_COUNT; index_uwb++)
                    {
                        Globals.GetTheInstance().SAS360CON_cfg_general.Array_lin_modbus_slave[index_uwb] = Globals.GetTheInstance().List_sas360con_cfg[index_uwb_com_config].Value;
                        Globals.GetTheInstance().Array_SAS360CON_UWB[index_uwb].Slave = Globals.GetTheInstance().List_sas360con_cfg[index_uwb_com_config].Value;

                        index_uwb_com_config++;
                    }

                    index_uwb_com_config += 6; //Reserved
                }
                catch { }

                #endregion

                #region UWB TAG config reserved

                try
                {
                    int index_uwb_tag_config = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + 80);

                    Globals.GetTheInstance().SAS360CON_cfg_general.Clear_undetected_tag_decseg = Globals.GetTheInstance().List_sas360con_cfg[index_uwb_tag_config].Value;

                    index_uwb_tag_config++;

                    index_uwb_tag_config += 9; //Reserved
                }
                catch { }

                #endregion

                #region RECORDING

                try
                {
                    int index_recording = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + 90);

                    Globals.GetTheInstance().SAS360CON_cfg_recording.Array_recorded_register_index = new byte[Constants.RECORDING_REG_SAS360CON_ARRAY];
                    Globals.GetTheInstance().SAS360CON_cfg_recording.Array_recorded_register_unit_codif = new byte[Constants.RECORDING_REG_SAS360CON_ARRAY];
                    Globals.GetTheInstance().SAS360CON_cfg_recording.Array_recorded_register_period_secs = new ushort[Constants.RECORDING_REG_SAS360CON_ARRAY];

                    for (int index = 0; index < Constants.RECORDING_REG_SAS360CON_ARRAY; index++)
                    {
                        Globals.GetTheInstance().SAS360CON_cfg_recording.Array_recorded_register_index[index] = Globals.GetTheInstance().List_sas360con_cfg[index_recording].Value;

                        index_recording++;
                    }

                    for (int index = 0; index < Constants.RECORDING_REG_SAS360CON_ARRAY; index++)
                    {
                        Globals.GetTheInstance().SAS360CON_cfg_recording.Array_recorded_register_unit_codif[index] = Globals.GetTheInstance().List_sas360con_cfg[index_recording].Value;

                        index_recording++;
                    }

                    for (int index = 0; index < Constants.RECORDING_REG_SAS360CON_ARRAY; index++)
                    {
                        Globals.GetTheInstance().SAS360CON_cfg_recording.Array_recorded_register_period_secs[index] = Globals.GetTheInstance().List_sas360con_cfg[index_recording].Value;

                        index_recording++;
                    }
                }
                catch { }

                #endregion

                #region CALCULADAS CONFIG

                try
                {
                    int index_calculadas = Globals.GetTheInstance().List_sas360con_cfg.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG] + 116);

                    uint u32_rtc_last_config = Globals.GetTheInstance().List_sas360con_cfg[index_calculadas].Value;
                    DateTime date_rtc_last_config = Constants.date_ref.Date.AddSeconds(u32_rtc_last_config);
                    Globals.GetTheInstance().SAS360CON_cfg_general.RTC_last_config_change = date_rtc_last_config.ToString(Globals.GetTheInstance().DateFormat, new CultureInfo(Globals.GetTheInstance().DateProvider)); ;

                    index_calculadas++;

                    Globals.GetTheInstance().SAS360CON_cfg_general.Change_counter = Globals.GetTheInstance().List_sas360con_cfg[index_calculadas].Value;

                    index_calculadas++;

                    Globals.GetTheInstance().SAS360CON_cfg_general.CRC_config = Globals.GetTheInstance().List_sas360con_cfg[index_calculadas].Value;

                    index_calculadas++;
                }
                catch { }

                #endregion

            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Load_SAS360CON_cfg_modbus_data)} -> {ex.Message}");
            }
        }

        #endregion

        #region Load SAS360CON image memory data

        public static void Load_SAS360CON_image_modbus_data()
        {
            try
            {
                #region Estados booleanos
                try
                {
                    int index_estados_booleanos = Globals.GetTheInstance().List_sas360con_image.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE]);

                    #region RTC UTC

                    uint u32_rtc_utc = Globals.GetTheInstance().List_sas360con_image[index_estados_booleanos].Value;
                    Globals.GetTheInstance().SAS360CON_image_general.RTC_UTC_seconds = u32_rtc_utc;

                    index_estados_booleanos++;

                    Globals.GetTheInstance().SAS360CON_image_general.Milliseconds = Globals.GetTheInstance().List_sas360con_image[index_estados_booleanos].Value;

                    index_estados_booleanos++;

                    #endregion

                    Globals.GetTheInstance().SAS360CON_image_general.Watchdog_inc = Globals.GetTheInstance().List_sas360con_image[index_estados_booleanos].Value;

                    index_estados_booleanos++;

                    #region State

                    if (Enum.IsDefined(typeof(MASK_SAS360CON_STATE), Globals.GetTheInstance().List_sas360con_image[index_estados_booleanos].Value))
                        Globals.GetTheInstance().SAS360CON_image_general.Sas360_state = (MASK_SAS360CON_STATE)Globals.GetTheInstance().List_sas360con_image[index_estados_booleanos].Value;

                    index_estados_booleanos++;

                    if (Enum.IsDefined(typeof(MASK_SAS360CON_SUBSTATE), Globals.GetTheInstance().List_sas360con_image[index_estados_booleanos].Value))
                        Globals.GetTheInstance().SAS360CON_image_general.Sas360_substate = (MASK_SAS360CON_SUBSTATE)Globals.GetTheInstance().List_sas360con_image[index_estados_booleanos].Value;

                    index_estados_booleanos++;

                    #endregion

                    Globals.GetTheInstance().SAS360CON_image_general.Array_codif_bits[0] = Globals.GetTheInstance().List_sas360con_image[index_estados_booleanos].Value;

                    index_estados_booleanos++;

                    Globals.GetTheInstance().SAS360CON_image_general.Array_codif_bits[1] = Globals.GetTheInstance().List_sas360con_image[index_estados_booleanos].Value;

                    index_estados_booleanos++;

                    Globals.GetTheInstance().SAS360CON_image_general.Array_codif_management[0] = Globals.GetTheInstance().List_sas360con_image[index_estados_booleanos].Value;

                    index_estados_booleanos++;

                    Globals.GetTheInstance().SAS360CON_image_general.Array_codif_management[1] = Globals.GetTheInstance().List_sas360con_image[index_estados_booleanos].Value;

                    index_estados_booleanos++;

                    #region Digital states

                    for (int index = 0; index < Enum.GetNames(typeof(DIGITAL_STATES_IN_LIST)).Length; index++)
                    {
                        Globals.GetTheInstance().SAS360CON_image_general.Array_digital_states[index] = Globals.GetTheInstance().List_sas360con_image[index_estados_booleanos].Value;

                        index_estados_booleanos++;
                    }

                    #endregion

                    Globals.GetTheInstance().SAS360CON_image_general.Global_dmsec_counter = Globals.GetTheInstance().List_sas360con_image[index_estados_booleanos].Value;

                    index_estados_booleanos++;
                }
                catch (Exception ex)
                {
                    Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Load_SAS360CON_image_modbus_data)} (Estados booleanos)-> {ex.Message}");
                }


                #endregion

                #region Entradas analógicas

                try
                {
                    int index_ea = Globals.GetTheInstance().List_sas360con_image.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE] + 20);

                    Globals.GetTheInstance().SAS360CON_image_general.EA_4v1_power_mv = Globals.GetTheInstance().List_sas360con_image[index_ea].Value;

                    index_ea++;

                    Globals.GetTheInstance().SAS360CON_image_general.EA_shunt_leds_ma = Globals.GetTheInstance().List_sas360con_image[index_ea].Value;

                    index_ea++;

                    Globals.GetTheInstance().SAS360CON_image_general.Voltage_adcref_3v3_mv = Globals.GetTheInstance().List_sas360con_image[index_ea].Value;

                    index_ea++;

                    index_ea += 7; //reserved
                }
                catch (Exception ex)
                {
                    Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Load_SAS360CON_image_modbus_data)} (Entradas analogicas)-> {ex.Message}");
                }

                #endregion

                #region Tiempo procesado y temporizaciones

                try
                {
                    int index_tiempo_procesado = Globals.GetTheInstance().List_sas360con_image.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE] + 30);

                    Globals.GetTheInstance().SAS360CON_image_procesado_contadores.Total_polling_cycle_counter = Globals.GetTheInstance().List_sas360con_image[index_tiempo_procesado].Value;

                    index_tiempo_procesado++;

                    index_tiempo_procesado++; //Reserved

                    Globals.GetTheInstance().SAS360CON_image_procesado_contadores.Polling_cycle_execution_time_dmsec = Globals.GetTheInstance().List_sas360con_image[index_tiempo_procesado].Value;

                    index_tiempo_procesado++;

                    Globals.GetTheInstance().SAS360CON_image_procesado_contadores.Time_processing_int_1msec_actions_dmseg = Globals.GetTheInstance().List_sas360con_image[index_tiempo_procesado].Value;

                    index_tiempo_procesado++;

                    Globals.GetTheInstance().SAS360CON_image_procesado_contadores.Time_processing_main_10msec_actions_dmseg = Globals.GetTheInstance().List_sas360con_image[index_tiempo_procesado].Value;

                    index_tiempo_procesado++;

                    Globals.GetTheInstance().SAS360CON_image_procesado_contadores.Time_processing_main_100msec_actions_dmseg = Globals.GetTheInstance().List_sas360con_image[index_tiempo_procesado].Value;

                    index_tiempo_procesado++;

                    Globals.GetTheInstance().SAS360CON_image_procesado_contadores.Time_processing_main_1sec_actions_dmseg = Globals.GetTheInstance().List_sas360con_image[index_tiempo_procesado].Value;

                    index_tiempo_procesado++;

                    Globals.GetTheInstance().SAS360CON_image_procesado_contadores.Time_processing_int_1msec_MAX_actions_dmseg = Globals.GetTheInstance().List_sas360con_image[index_tiempo_procesado].Value;

                    index_tiempo_procesado++;

                    Globals.GetTheInstance().SAS360CON_image_procesado_contadores.Time_processing_int_10msec_MAX_actions_dmseg = Globals.GetTheInstance().List_sas360con_image[index_tiempo_procesado].Value;

                    index_tiempo_procesado++;
                }
                catch (Exception ex)
                {
                    Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Load_SAS360CON_image_modbus_data)} (Tiempo procesado)-> {ex.Message}");
                }

                #endregion

                #region NVREG management

                try
                {
                    int index_nvreg = Globals.GetTheInstance().List_sas360con_image.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE] + 40);

                    Globals.GetTheInstance().SAS360CON_image_nvreg_management.Internal_change_counter = Globals.GetTheInstance().List_sas360con_image[index_nvreg].Value;

                    if (Globals.GetTheInstance().SAS360CON_image_nvreg_management.Internal_change_counter != Globals.GetTheInstance().SAS360CON_internal_cfg.Change_counter)
                    {
                        Manage_logs.SaveLogValue($"SAS360CON internal config counter change / LAST VALUE: {Globals.GetTheInstance().SAS360CON_internal_cfg.Change_counter} / CURRENT VALUE {Globals.GetTheInstance().SAS360CON_image_nvreg_management.Internal_change_counter}");
                        Globals.GetTheInstance().SAS360con_internal_config_read = false;
                    }

                    index_nvreg++;

                    Globals.GetTheInstance().SAS360CON_image_nvreg_management.Config_con_change_counter = Globals.GetTheInstance().List_sas360con_image[index_nvreg].Value;
                    if (Globals.GetTheInstance().SAS360CON_image_nvreg_management.Config_con_change_counter != Globals.GetTheInstance().SAS360CON_cfg_general.Change_counter)
                    {
                        Manage_logs.SaveLogValue($"SAS360CON config counter change / LAST VALUE: {Globals.GetTheInstance().SAS360CON_cfg_general.Change_counter} / CURRENT VALUE {Globals.GetTheInstance().SAS360CON_image_nvreg_management.Config_con_change_counter}");
                        Globals.GetTheInstance().SAS360con_config_read = false;
                    }

                    index_nvreg++;

                    Globals.GetTheInstance().SAS360CON_image_nvreg_management.Config_iot_change_counter = Globals.GetTheInstance().List_sas360con_image[index_nvreg].Value;

                    index_nvreg++;

                    Globals.GetTheInstance().SAS360CON_image_nvreg_management.Nvreg_change_counter = Globals.GetTheInstance().List_sas360con_image[index_nvreg].Value;

                    index_nvreg++;

                    Globals.GetTheInstance().SAS360CON_image_nvreg_management.Last_recorded_event_absolute_index_copy = Globals.GetTheInstance().List_sas360con_image[index_nvreg].Value;

                    index_nvreg++;

                    Globals.GetTheInstance().SAS360CON_image_nvreg_management.Num_recorded_events_copy = Globals.GetTheInstance().List_sas360con_image[index_nvreg].Value;

                    index_nvreg++;

                    Globals.GetTheInstance().SAS360CON_image_nvreg_management.Last_recorded_event_array_position_copy = Globals.GetTheInstance().List_sas360con_image[index_nvreg].Value;

                    index_nvreg++;
                }
                catch (Exception ex)
                {
                    Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Load_SAS360CON_image_modbus_data)} (NVREG)-> {ex.Message}");
                }

                #endregion

                #region Main management

                try
                {
                    int index_main_management = Globals.GetTheInstance().List_sas360con_image.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE] + 50);

                    Globals.GetTheInstance().SAS360CON_image_main_management.Internal_error = Globals.GetTheInstance().List_sas360con_image[index_main_management].Value;

                    index_main_management++;

                    Globals.GetTheInstance().SAS360CON_image_main_management.Error_code_detail = Globals.GetTheInstance().List_sas360con_image[index_main_management].Value;

                    index_main_management++;

                    Globals.GetTheInstance().SAS360CON_image_main_management.Active_warning_id = Globals.GetTheInstance().List_sas360con_image[index_main_management].Value;

                    index_main_management++;

                    Globals.GetTheInstance().SAS360CON_image_main_management.Warning_exceded_T15C_tag_number = Globals.GetTheInstance().List_sas360con_image[index_main_management].Value;

                    index_main_management++;

                    Globals.GetTheInstance().SAS360CON_image_main_management.Last_event_log_rtc = Globals.GetTheInstance().List_sas360con_image[index_main_management].Value;

                    index_main_management++;

                    Globals.GetTheInstance().SAS360CON_image_main_management.Last_event_log_msec = Globals.GetTheInstance().List_sas360con_image[index_main_management].Value;

                    index_main_management++;

                    Globals.GetTheInstance().SAS360CON_image_main_management.Last_event_log_id = Globals.GetTheInstance().List_sas360con_image[index_main_management].Value;

                    index_main_management++;

                    Globals.GetTheInstance().SAS360CON_image_main_management.Last_event_log_value[0] = Globals.GetTheInstance().List_sas360con_image[index_main_management].Value;

                    index_main_management++;

                    Globals.GetTheInstance().SAS360CON_image_main_management.Last_event_log_value[1] = Globals.GetTheInstance().List_sas360con_image[index_main_management].Value;

                    index_main_management++;
                }
                catch (Exception ex)
                {
                    Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Load_SAS360CON_image_modbus_data)} (Main management) -> {ex.Message}");
                }
                #endregion

                #region Lin pooling management

                try
                {
                    int index_lin_pooling_management = Globals.GetTheInstance().List_sas360con_image.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE] + 60);

                    Globals.GetTheInstance().SAS360CON_image_lin_pooling.Array_lin_pooling_read_uwb = new ushort[Constants.UWB_TOTAL_COUNT];
                    Globals.GetTheInstance().SAS360CON_image_lin_pooling.Array_lin_pooling_write_uwb = new ushort[Constants.UWB_TOTAL_COUNT];
                    Globals.GetTheInstance().SAS360CON_image_lin_pooling.Array_lin_com_total_counter = new ushort[Constants.UWB_TOTAL_COUNT];
                    Globals.GetTheInstance().SAS360CON_image_lin_pooling.Array_lin_com_error_counter = new ushort[Constants.UWB_TOTAL_COUNT];
                    Globals.GetTheInstance().SAS360CON_image_lin_pooling.Array_lin_total_last_cycle_time = new ushort[Constants.UWB_TOTAL_COUNT];

                    for (int index = 0; index < Constants.UWB_TOTAL_COUNT; index++)
                    {
                        Globals.GetTheInstance().SAS360CON_image_lin_pooling.Array_lin_pooling_read_uwb[index] = Globals.GetTheInstance().List_sas360con_image[index_lin_pooling_management].Value;
                        Globals.GetTheInstance().Array_SAS360CON_UWB[index].Pool_read = Globals.GetTheInstance().List_sas360con_image[index_lin_pooling_management].Value;

                        index_lin_pooling_management++;
                    }

                    for (int index = 0; index < Constants.UWB_TOTAL_COUNT; index++)
                    {
                        Globals.GetTheInstance().SAS360CON_image_lin_pooling.Array_lin_pooling_write_uwb[index] = Globals.GetTheInstance().List_sas360con_image[index_lin_pooling_management].Value;
                        Globals.GetTheInstance().Array_SAS360CON_UWB[index].Pool_write = Globals.GetTheInstance().List_sas360con_image[index_lin_pooling_management].Value;

                        index_lin_pooling_management++;
                    }

                    Globals.GetTheInstance().SAS360CON_image_lin_pooling.Lin_pooling_write_broadcast = Globals.GetTheInstance().List_sas360con_image[index_lin_pooling_management].Value;

                    index_lin_pooling_management++;

                    Globals.GetTheInstance().SAS360CON_image_lin_pooling.Lin_pooling_state = Globals.GetTheInstance().List_sas360con_image[index_lin_pooling_management].Value;

                    index_lin_pooling_management++;

                    Globals.GetTheInstance().SAS360CON_image_lin_pooling.Actual_pooled_uwb = Globals.GetTheInstance().List_sas360con_image[index_lin_pooling_management].Value;

                    index_lin_pooling_management++;

                    Globals.GetTheInstance().SAS360CON_image_lin_pooling.Actual_pooling_request_group = Globals.GetTheInstance().List_sas360con_image[index_lin_pooling_management].Value;

                    index_lin_pooling_management++;

                    Globals.GetTheInstance().SAS360CON_image_lin_pooling.Actual_pooling_request_index = Globals.GetTheInstance().List_sas360con_image[index_lin_pooling_management].Value;

                    index_lin_pooling_management++;


                    for (int index = 0; index < Constants.UWB_TOTAL_COUNT; index++)
                    {
                        Globals.GetTheInstance().SAS360CON_image_lin_pooling.Array_lin_com_total_counter[index] = Globals.GetTheInstance().List_sas360con_image[index_lin_pooling_management].Value;
                        Globals.GetTheInstance().Array_SAS360CON_UWB[index].Comm_total = Globals.GetTheInstance().List_sas360con_image[index_lin_pooling_management].Value;

                        index_lin_pooling_management++;
                    }

                    for (int index = 0; index < Constants.UWB_TOTAL_COUNT; index++)
                    {
                        Globals.GetTheInstance().SAS360CON_image_lin_pooling.Array_lin_com_error_counter[index] = Globals.GetTheInstance().List_sas360con_image[index_lin_pooling_management].Value; ;
                        Globals.GetTheInstance().Array_SAS360CON_UWB[index].Com_error = Globals.GetTheInstance().List_sas360con_image[index_lin_pooling_management].Value; ;

                        index_lin_pooling_management++;
                    }

                    for (int index = 0; index < Constants.UWB_TOTAL_COUNT; index++)
                    {
                        Globals.GetTheInstance().SAS360CON_image_lin_pooling.Array_lin_total_last_cycle_time[index] = Globals.GetTheInstance().List_sas360con_image[index_lin_pooling_management].Value; ;
                        Globals.GetTheInstance().Array_SAS360CON_UWB[index].Cycle_time = Globals.GetTheInstance().List_sas360con_image[index_lin_pooling_management].Value; ;

                        index_lin_pooling_management++;
                    }
                }
                catch (Exception ex)
                {
                    Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Load_SAS360CON_image_modbus_data)} (LIN polling) -> {ex.Message}");
                }

                #endregion

                #region Con processed tag

                try
                {
                    int index_con_processed_tags = Globals.GetTheInstance().List_sas360con_image.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE] + 84);

                    for (int index = 0; index < 2; index++)
                    {
                        Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_assigned_self_contag_id[index] = Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value;

                        index_con_processed_tags++;
                    }

                    for (int index = 0; index < 2; index++)
                    {
                        Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_assigned_self_drvtag_id[index] = Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value;

                        index_con_processed_tags++;
                    }

                    index_con_processed_tags += 4; //Reserved

                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                    {
                        Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_number_total_TAGS_in_area_DANR[index] = Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value;

                        index_con_processed_tags++;
                    }
                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                    {
                        Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_number_total_PED_in_area_DANR[index] = Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value;

                        index_con_processed_tags++;
                    }
                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                    {
                        Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_number_total_DRV_in_area_DANR[index] = Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value;

                        index_con_processed_tags++;
                    }
                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                    {
                        Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_number_total_LV_in_area_DANR[index] = Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value;

                        index_con_processed_tags++;
                    }
                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                    {
                        Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_number_total_HV_in_area_DANR[index] = Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value;

                        index_con_processed_tags++;
                    }
                    for (int index = 0; index < Constants.DETECTION_AREA_COUNT; index++)
                    {
                        Globals.GetTheInstance().SAS360CON_image_processed_tag.Array_number_total_ZONES_in_area_DANR[index] = Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value;

                        index_con_processed_tags++;
                    }

                    Globals.GetTheInstance().SAS360CON_image_processed_tag.Number_zones_slow_range = Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value;

                    index_con_processed_tags++;

                    index_con_processed_tags++; //Reserved

                    Globals.GetTheInstance().SAS360CON_image_processed_tag.Reported_register_uwb_index = Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value;

                    index_con_processed_tags++;

                    Globals.GetTheInstance().SAS360CON_image_processed_tag.Reported_register_tag_index = Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value;

                    index_con_processed_tags++;

                    Globals.GetTheInstance().SAS360CON_image_processed_tag.Estado_leds_amarillo = Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value;

                    index_con_processed_tags++;

                    Globals.GetTheInstance().SAS360CON_image_processed_tag.Estado_leds_naranja = Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value;

                    index_con_processed_tags++;

                    Globals.GetTheInstance().SAS360CON_image_processed_tag.Estado_leds_rojo = Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value;

                    index_con_processed_tags++;

                    Globals.GetTheInstance().SAS360CON_image_processed_tag.Closest_DRVTAG_tagID_2LSB = Globals.GetTheInstance().List_sas360con_image[index_con_processed_tags].Value;

                    index_con_processed_tags++;
                }
                catch (Exception ex)
                {
                    Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Load_SAS360CON_image_modbus_data)} (CON processed TAGS) -> {ex.Message}");
                }

                #endregion

                #region SAS360CON field position

                try
                {
                    int index_field_position = Globals.GetTheInstance().List_sas360con_image.FindIndex(config => config.Addr == Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE] + 110);

                    Globals.GetTheInstance().SAS360CON_image_field_position.Installation_pos_x_cm = Globals.GetTheInstance().List_sas360con_image[index_field_position].Value;

                    index_field_position++;

                    Globals.GetTheInstance().SAS360CON_image_field_position.Installation_pos_y_cm = Globals.GetTheInstance().List_sas360con_image[index_field_position].Value;

                    index_field_position++;

                    Globals.GetTheInstance().SAS360CON_image_field_position.Latitud = Globals.GetTheInstance().List_sas360con_image[index_field_position].Value;

                    index_field_position++;

                    Globals.GetTheInstance().SAS360CON_image_field_position.Longitud = Globals.GetTheInstance().List_sas360con_image[index_field_position].Value;

                    index_field_position++;

                    Globals.GetTheInstance().SAS360CON_image_field_position.Contador_alerta_entrada_en_area_roja = Globals.GetTheInstance().List_sas360con_image[index_field_position].Value;

                    index_field_position++;

                    Globals.GetTheInstance().SAS360CON_image_field_position.Contador_alerta_entrada_en_area_naranja = Globals.GetTheInstance().List_sas360con_image[index_field_position].Value;

                    index_field_position++;
                }
                catch (Exception ex)
                {
                    Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Load_SAS360CON_image_modbus_data)} (CON field pos) -> {ex.Message}");
                }

                #endregion

            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Load_SAS360CON_image_modbus_data)} -> {ex.Message}");
            }
        }

        #endregion

        #region Load SAS360CON maintennance memory data

        public static void Load_SAS360CON_maintennance_modbus_data()
        {
            try
            {
                int sas360con_maintennance_from_list_pos = 0;
                Globals.GetTheInstance().SAS360CON_maintennance.Autotest_bit_check_codif = Globals.GetTheInstance().List_sas360con_maintennance[sas360con_maintennance_from_list_pos].Value;

                sas360con_maintennance_from_list_pos++;

                Globals.GetTheInstance().SAS360CON_maintennance.Autotest_ea_4V1_POWER_value_mV = Globals.GetTheInstance().List_sas360con_maintennance[sas360con_maintennance_from_list_pos].Value;

                sas360con_maintennance_from_list_pos++;

                Globals.GetTheInstance().SAS360CON_maintennance.Autotest_reset_cause_codif = Globals.GetTheInstance().List_sas360con_maintennance[sas360con_maintennance_from_list_pos].Value;

                sas360con_maintennance_from_list_pos++;

                Globals.GetTheInstance().SAS360CON_maintennance.Autotest_manteni_type_codif = Globals.GetTheInstance().List_sas360con_maintennance[sas360con_maintennance_from_list_pos].Value;

                sas360con_maintennance_from_list_pos++;

                for (int index = 0; index < Constants.AUTOTEST_EA_LEDS_COUNT; index++)
                {
                    Globals.GetTheInstance().SAS360CON_maintennance.Autotest_ea_consumo_grupo_LEDS_ma[index] = Globals.GetTheInstance().List_sas360con_maintennance[sas360con_maintennance_from_list_pos].Value;

                    sas360con_maintennance_from_list_pos++;
                }

                sas360con_maintennance_from_list_pos+=10; //Reserved

                Globals.GetTheInstance().SAS360CON_maintennance.Forced_mask_DO1_INT = Globals.GetTheInstance().List_sas360con_maintennance[sas360con_maintennance_from_list_pos].Value;

                sas360con_maintennance_from_list_pos++;

                Globals.GetTheInstance().SAS360CON_maintennance.Forced_mask_DO2_EXT = Globals.GetTheInstance().List_sas360con_maintennance[sas360con_maintennance_from_list_pos].Value;

                sas360con_maintennance_from_list_pos++;

                Globals.GetTheInstance().SAS360CON_maintennance.Forced_mask_DO3_LED = Globals.GetTheInstance().List_sas360con_maintennance[sas360con_maintennance_from_list_pos].Value;

                sas360con_maintennance_from_list_pos++;

                Globals.GetTheInstance().SAS360CON_maintennance.Forced_mask_codif_LED1 = Globals.GetTheInstance().List_sas360con_maintennance[sas360con_maintennance_from_list_pos].Value;

                sas360con_maintennance_from_list_pos++;

                Globals.GetTheInstance().SAS360CON_maintennance.Forced_mask_codif_LED2 = Globals.GetTheInstance().List_sas360con_maintennance[sas360con_maintennance_from_list_pos].Value;

                sas360con_maintennance_from_list_pos++;

                Globals.GetTheInstance().SAS360CON_maintennance.Forced_mask_AUDIO_1_to_play = Globals.GetTheInstance().List_sas360con_maintennance[sas360con_maintennance_from_list_pos].Value;

                sas360con_maintennance_from_list_pos++;

                Globals.GetTheInstance().SAS360CON_maintennance.Forced_mask_AUDIO_2_to_play = Globals.GetTheInstance().List_sas360con_maintennance[sas360con_maintennance_from_list_pos].Value;

                sas360con_maintennance_from_list_pos+=3;
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Load_SAS360CON_maintennance_modbus_data)} -> {ex.Message}");
            }
        }

        #endregion

        #region Load UWB internal cfg memory data

        public static void Load_UWB_internal_cfg_modbus_data()
        {
            try
            {
                //int uwb_internal_cfg_from_list_pos = 0;

                for (int index = 0; index < Constants.UWB_TOTAL_COUNT; index++)
                {
                }

            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Load_UWB_internal_cfg_modbus_data)} -> {ex.Message}");
            }
        }

        #endregion

        #region Load UWB image memory data

        public static void Load_UWB_image_modbus_data()
        {
            try
            {
                int uwb_image_from_list_pos = 0;

                for (int index = 0; index < Constants.UWB_TOTAL_COUNT; index++)
                {
                    uint u32_rtc_utc = Globals.GetTheInstance().List_uwb_image[uwb_image_from_list_pos].Value;
                    DateTime date_rtc_utc = Constants.date_ref.Date.AddSeconds(u32_rtc_utc);
                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].RTC_UTC_value = u32_rtc_utc;
                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].RTC_UTC_DATE = date_rtc_utc.ToString(Globals.GetTheInstance().DateFormat, new CultureInfo(Globals.GetTheInstance().DateProvider)); ;

                    uwb_image_from_list_pos++;

                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].RTC_millisecs = Globals.GetTheInstance().List_uwb_image[uwb_image_from_list_pos].Value;

                    uwb_image_from_list_pos++;

                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].Watchdog_inc = Globals.GetTheInstance().List_uwb_image[uwb_image_from_list_pos].Value;

                    uwb_image_from_list_pos++;

                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].Codif_state = Globals.GetTheInstance().List_uwb_image[uwb_image_from_list_pos].Value;

                    uwb_image_from_list_pos++;

                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].Num_tags = Globals.GetTheInstance().List_uwb_image[uwb_image_from_list_pos].Value;

                    uwb_image_from_list_pos++;

                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].Num_zones = Globals.GetTheInstance().List_uwb_image[uwb_image_from_list_pos].Value;

                    uwb_image_from_list_pos++;

                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].Delay_in_image_pool = Globals.GetTheInstance().List_uwb_image[uwb_image_from_list_pos].Value;

                    uwb_image_from_list_pos++;

                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].Array_contag_id = new byte[Constants.ASSIGNED_ID_SIZE];
                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].Array_drvtag_id = new byte[Constants.ASSIGNED_ID_SIZE];
                    for (int index_id = 0; index_id < Constants.ASSIGNED_ID_SIZE; index_id++)
                    {
                        Globals.GetTheInstance().Array_SAS360CON_UWB[index].Array_contag_id[index_id] = Globals.GetTheInstance().List_uwb_image[uwb_image_from_list_pos].Value;

                        uwb_image_from_list_pos++;
                    }

                    for (int index_id = 0; index_id < Constants.ASSIGNED_ID_SIZE; index_id++)
                    {
                        Globals.GetTheInstance().Array_SAS360CON_UWB[index].Array_drvtag_id[index_id] = Globals.GetTheInstance().List_uwb_image[uwb_image_from_list_pos].Value;

                        uwb_image_from_list_pos++;
                    }


                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].Antenna_number_in_CON = Globals.GetTheInstance().List_uwb_image[uwb_image_from_list_pos].Value;

                    uwb_image_from_list_pos++;

                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].Installation_ID = Globals.GetTheInstance().List_uwb_image[uwb_image_from_list_pos].Value;

                    uwb_image_from_list_pos++;

                    uwb_image_from_list_pos += 4; //Reserved

                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].Reported_register = Globals.GetTheInstance().List_uwb_image[uwb_image_from_list_pos].Value;

                    uwb_image_from_list_pos++;

                    Globals.GetTheInstance().Array_SAS360CON_UWB[index].War_error_id = Globals.GetTheInstance().List_uwb_image[uwb_image_from_list_pos].Value;

                    uwb_image_from_list_pos++;
                }

            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Load_UWB_image_modbus_data)} -> {ex.Message}");
            }
        }

        #endregion


        #region Load CON TAG base memory data

        public static void Load_CON_TAG_base_modbus_data()
        {
            try
            {
                int tags_base_from_list_pos = 0;

                for (int index_tag = 0; index_tag < Globals.GetTheInstance().Total_closest_tags; index_tag++)
                {
                    SAS360CON_TAG sas360_tag = Globals.GetTheInstance().Array_SAS360CON_TAG[index_tag];

                    ushort tagID_2LSB = Globals.GetTheInstance().List_console_closest_tags_base[tags_base_from_list_pos].Value;
                    sas360_tag.ID_2LSB = tagID_2LSB;

                    tags_base_from_list_pos++;

                    byte tagID_byte_3 = Globals.GetTheInstance().List_console_closest_tags_base[tags_base_from_list_pos].Value;
                    int identifier_zone = tagID_byte_3 >> 4;

                    int tag_type = tagID_byte_3 & 0x0F;
                    sas360_tag.Tag_type_value = (byte)tag_type;
                    sas360_tag.Tag_type = Enum.IsDefined(typeof(MASK_TAG_ZONE_TYPE), tag_type) ? (MASK_TAG_ZONE_TYPE)tag_type : MASK_TAG_ZONE_TYPE.UNKNOWN;

                    sas360_tag.Tag_type_id_grid = $"{tag_type}-{tagID_2LSB}";

                    tags_base_from_list_pos++;

                    #region FW version

                    byte tag_fw_version = Globals.GetTheInstance().List_console_closest_tags_base[tags_base_from_list_pos].Value;

                    sas360_tag.FW_version_value = tag_fw_version;
                    string s_fw_ver = $"{tag_fw_version:D3}";
                    string s_fw_ver_dot = string.Empty;
                    int index_ver = 0;
                    do
                        s_fw_ver_dot += $"{s_fw_ver[index_ver]}.";
                    while (index_ver++ < s_fw_ver.Length - 2);
                    s_fw_ver_dot += s_fw_ver[index_ver];
                    sas360_tag.FW_version = s_fw_ver_dot;

                    #endregion


                    tags_base_from_list_pos++;

                    ushort tag_estado_codificado = Globals.GetTheInstance().List_console_closest_tags_base[tags_base_from_list_pos].Value;
                    sas360_tag.Estado_codificado = tag_estado_codificado;
                    sas360_tag.Estado_ec = (byte)(tag_estado_codificado & 0xFF);
                    sas360_tag.Estado_rr = (byte)(tag_estado_codificado >> 8);

                    tags_base_from_list_pos++;

                    short con_calc_tag_position_abs_X_cm = Globals.GetTheInstance().List_console_closest_tags_base[tags_base_from_list_pos].Value;
                    sas360_tag.Calc_tag_position_abs_X_cm = con_calc_tag_position_abs_X_cm;

                    tags_base_from_list_pos++;

                    short con_calc_tag_position_abs_Y_cm = Globals.GetTheInstance().List_console_closest_tags_base[tags_base_from_list_pos].Value;
                    sas360_tag.Calc_tag_position_abs_Y_cm = con_calc_tag_position_abs_Y_cm;

                    tags_base_from_list_pos++;

                    ushort tag_received_command = Globals.GetTheInstance().List_console_closest_tags_base[tags_base_from_list_pos].Value;
                    sas360_tag.Received_command = tag_received_command;
                    sas360_tag.Received_command_ec = (byte)(tag_received_command & 0xFF);
                    sas360_tag.Received_command_rr = (byte)(tag_received_command >> 8);

                    tags_base_from_list_pos++;

                    byte tag_battery_level_pct = Globals.GetTheInstance().List_console_closest_tags_base[tags_base_from_list_pos].Value;
                    sas360_tag.Battery_level_pct = (byte)(tag_battery_level_pct >> 4);
                    sas360_tag.Zone_cfg_dist_m = (byte)(tag_battery_level_pct & 0X0F);

                    tags_base_from_list_pos++;

                    byte tag_consola_code_alarma = Globals.GetTheInstance().List_console_closest_tags_base[tags_base_from_list_pos].Value;
                    sas360_tag.Consola_code_alarma = tag_consola_code_alarma;

                    tags_base_from_list_pos++;

                    ushort tag_reported_register = Globals.GetTheInstance().List_console_closest_tags_base[tags_base_from_list_pos].Value;
                    sas360_tag.Reported_register = tag_reported_register;

                    tags_base_from_list_pos++;

                    byte con_calc_uwb_detection = Globals.GetTheInstance().List_console_closest_tags_base[tags_base_from_list_pos].Value;
                    sas360_tag.Calc_uwb_detection = con_calc_uwb_detection;

                    tags_base_from_list_pos++;

                    byte worst_time_last_success_decseg = Globals.GetTheInstance().List_console_closest_tags_base[tags_base_from_list_pos].Value;
                    sas360_tag.Worst_time_last_success_decseg = worst_time_last_success_decseg;

                    tags_base_from_list_pos++;

                    byte con_calc_det_area = Globals.GetTheInstance().List_console_closest_tags_base[tags_base_from_list_pos].Value;
                    sas360_tag.Calc_det_area = con_calc_det_area;

                    tags_base_from_list_pos++;

                    byte con_calc_direccion = Globals.GetTheInstance().List_console_closest_tags_base[tags_base_from_list_pos].Value;
                    sas360_tag.Calc_direccion = con_calc_direccion;

                    tags_base_from_list_pos++;

                    byte con_calc_led_sector = Globals.GetTheInstance().List_console_closest_tags_base[tags_base_from_list_pos].Value;
                    sas360_tag.Calc_led_sector = con_calc_led_sector;

                    tags_base_from_list_pos++;

                    byte con_calc_num_det_ant = Globals.GetTheInstance().List_console_closest_tags_base[tags_base_from_list_pos].Value;
                    sas360_tag.Calc_num_det_ant = con_calc_num_det_ant;

                    tags_base_from_list_pos++;

                    ushort con_dist_closest_cm = Globals.GetTheInstance().List_console_closest_tags_base[tags_base_from_list_pos].Value;
                    sas360_tag.Dist_closest_cm = con_dist_closest_cm;

                    tags_base_from_list_pos++;


                    sas360_tag.Dist_from_antenna_grid = string.Empty;
                    for (int index_uwb = 0; index_uwb < Constants.UWB_TOTAL_COUNT; index_uwb++)
                    {
                        ushort dist_from_antenna = Globals.GetTheInstance().List_console_closest_tags_base[tags_base_from_list_pos].Value;
                        sas360_tag.Dist_from_antenna_cm[index_uwb] = dist_from_antenna;

                        //No visualizar último uwb
                        if (index_uwb < Constants.UWB_TOTAL_COUNT - 1)
                        {
                            sas360_tag.Dist_from_antenna_grid += $"{dist_from_antenna}";
                            if (index_uwb < Constants.UWB_TOTAL_COUNT - 2)
                                sas360_tag.Dist_from_antenna_grid += $" / ";
                        }

                        tags_base_from_list_pos++;
                    }

                    sas360_tag.Time_last_success_grid = string.Empty;
                    for (int index_uwb = 0; index_uwb < Constants.UWB_TOTAL_COUNT; index_uwb++)
                    {
                        byte time_last_success = Globals.GetTheInstance().List_console_closest_tags_base[tags_base_from_list_pos].Value;
                        sas360_tag.Tag_time_last_success_decsec[index_uwb] = time_last_success;

                        //No visualizar último uwb
                        if (index_uwb < Constants.UWB_TOTAL_COUNT - 1)
                        {
                            sas360_tag.Time_last_success_grid += $"{time_last_success}";
                            if (index_uwb < Constants.UWB_TOTAL_COUNT - 2)
                                sas360_tag.Time_last_success_grid += $" / ";
                        }

                        tags_base_from_list_pos++;
                    }

                    tags_base_from_list_pos += 4; //Reserved
                }
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Load_CON_TAG_base_modbus_data)} -> {ex.Message}");
            }
        }

        #endregion

        #region Load CON ZONE base memory data

        public static void Load_CON_ZONE_base_modbus_data()
        {
            try
            {
                int zone_base_from_list_pos = 0;

                for (int index_zone = 0; index_zone < Globals.GetTheInstance().Total_closest_zone; index_zone++)
                {
                    SAS360CON_ZONE sas360_zone = Globals.GetTheInstance().Array_SAS360CON_ZONE[index_zone];

                    ushort zoneID_2LSB = Globals.GetTheInstance().List_console_closest_zone_base[zone_base_from_list_pos].Value;
                    sas360_zone.ID_2LSB = zoneID_2LSB;

                    zone_base_from_list_pos++;

                    byte zoneID_byte_3 = Globals.GetTheInstance().List_console_closest_zone_base[zone_base_from_list_pos].Value;
                    int tag_type = zoneID_byte_3 & 0x0F;
                    sas360_zone.Zone_type_value = (byte)tag_type;
                    sas360_zone.Zone_type_id_grid = $"{tag_type:X}-{zoneID_2LSB}";
                    sas360_zone.Zone_type = Enum.IsDefined(typeof(MASK_TAG_ZONE_TYPE), tag_type) ? (MASK_TAG_ZONE_TYPE)tag_type : MASK_TAG_ZONE_TYPE.UNKNOWN;

                    zone_base_from_list_pos++;

                    #region FW version

                    byte zone_fw_version = Globals.GetTheInstance().List_console_closest_zone_base[zone_base_from_list_pos].Value;

                    sas360_zone.FW_version_value = zone_fw_version;
                    string s_fw_ver = $"{zone_fw_version:D3}";
                    string s_fw_ver_dot = string.Empty;
                    int index_ver = 0;
                    do
                        s_fw_ver_dot += $"{s_fw_ver[index_ver]}.";
                    while (index_ver++ < s_fw_ver.Length - 2);
                    s_fw_ver_dot += s_fw_ver[index_ver];
                    sas360_zone.FW_version = s_fw_ver_dot;

                    #endregion

                    ushort zone_estado_codificado = Globals.GetTheInstance().List_console_closest_zone_base[zone_base_from_list_pos].Value;
                    sas360_zone.Estado_codificado = zone_estado_codificado;
                    sas360_zone.Estado_ec = (byte)(zone_estado_codificado & 0xFF);
                    sas360_zone.Estado_rr = (byte)(zone_estado_codificado >> 8);

                    zone_base_from_list_pos++;

                    short calc_zone_position_abs_X_cm = Globals.GetTheInstance().List_console_closest_zone_base[zone_base_from_list_pos].Value;
                    sas360_zone.Calc_zone_position_abs_X_cm = calc_zone_position_abs_X_cm;

                    zone_base_from_list_pos++;

                    short calc_zone_position_abs_Y_cm = Globals.GetTheInstance().List_console_closest_zone_base[zone_base_from_list_pos].Value;
                    sas360_zone.Calc_zone_position_abs_Y_cm = calc_zone_position_abs_Y_cm;

                    zone_base_from_list_pos++;

                    ushort received_command = Globals.GetTheInstance().List_console_closest_zone_base[zone_base_from_list_pos].Value;
                    sas360_zone.Received_command = received_command;
                    sas360_zone.Received_command_ec = (byte)(received_command & 0xFF);
                    sas360_zone.Received_command_rr = (byte)(received_command >> 8);

                    zone_base_from_list_pos++;

                    ushort radio_action = Globals.GetTheInstance().List_console_closest_zone_base[zone_base_from_list_pos].Value;
                    sas360_zone.Radio_action = radio_action;

                    zone_base_from_list_pos++;

                    ushort reported_reg = Globals.GetTheInstance().List_console_closest_zone_base[zone_base_from_list_pos].Value;
                    sas360_zone.Reported_register = reported_reg;

                    zone_base_from_list_pos++;

                    ushort dist_closest_cm = Globals.GetTheInstance().List_console_closest_zone_base[zone_base_from_list_pos].Value;
                    sas360_zone.Dist_closest_cm = dist_closest_cm;

                    zone_base_from_list_pos++;

                    sas360_zone.Dist_from_antenna_grid = string.Empty;
                    for (int index_uwb = 0; index_uwb < Constants.UWB_TOTAL_COUNT; index_uwb++)
                    {
                        byte dist_from_antenna = Globals.GetTheInstance().List_console_closest_tags_base[zone_base_from_list_pos].Value;
                        sas360_zone.Dist_from_antenna_cm[index_uwb] = dist_from_antenna;
                        sas360_zone.Dist_from_antenna_grid += $"{dist_from_antenna} / ";
                        zone_base_from_list_pos++;
                    }
                }
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Load_CON_ZONE_base_modbus_data)} -> {ex.Message}");
            }
        }

        #endregion


        #region Load SAS360CON nvreg memory data

        public static void Load_SAS360CON_nvreg_modbus_data()
        {
            try
            {
                int sas360con_nvreg_from_list_pos = 0;

                Globals.GetTheInstance().SAS360CON_nvreg.Tam_bytes = Globals.GetTheInstance().List_sas360con_nvreg[sas360con_nvreg_from_list_pos].Value!;

                sas360con_nvreg_from_list_pos++;

                Globals.GetTheInstance().SAS360CON_nvreg.Nvreg_version = Globals.GetTheInstance().List_sas360con_nvreg[sas360con_nvreg_from_list_pos].Value!;

                sas360con_nvreg_from_list_pos++;

                Globals.GetTheInstance().SAS360CON_nvreg.Events_last_event_offset_flash = Globals.GetTheInstance().List_sas360con_nvreg[sas360con_nvreg_from_list_pos].Value!;

                sas360con_nvreg_from_list_pos++;

                Globals.GetTheInstance().SAS360CON_nvreg.Events_total_events_flash = Globals.GetTheInstance().List_sas360con_nvreg[sas360con_nvreg_from_list_pos].Value!;

                sas360con_nvreg_from_list_pos++;

                Globals.GetTheInstance().SAS360CON_nvreg.Events_last_page_used_flash = Globals.GetTheInstance().List_sas360con_nvreg[sas360con_nvreg_from_list_pos].Value!;

                sas360con_nvreg_from_list_pos++;

                Globals.GetTheInstance().SAS360CON_nvreg.Events_total_events = Globals.GetTheInstance().List_sas360con_nvreg[sas360con_nvreg_from_list_pos].Value!;

                sas360con_nvreg_from_list_pos++;

                Globals.GetTheInstance().SAS360CON_nvreg.Ram_buffer_last_event_index = Globals.GetTheInstance().List_sas360con_nvreg[sas360con_nvreg_from_list_pos].Value!;

                sas360con_nvreg_from_list_pos++;

                Globals.GetTheInstance().SAS360CON_nvreg.Historics_last_historic_offset_flash = Globals.GetTheInstance().List_sas360con_nvreg[sas360con_nvreg_from_list_pos].Value!;

                sas360con_nvreg_from_list_pos++;

                Globals.GetTheInstance().SAS360CON_nvreg.Historics_total_historics_flash = Globals.GetTheInstance().List_sas360con_nvreg[sas360con_nvreg_from_list_pos].Value!;

                sas360con_nvreg_from_list_pos++;

                Globals.GetTheInstance().SAS360CON_nvreg.Historics_last_page_used_flash = Globals.GetTheInstance().List_sas360con_nvreg[sas360con_nvreg_from_list_pos].Value!;

                sas360con_nvreg_from_list_pos++;

                sas360con_nvreg_from_list_pos+=2; //Reserved

                Globals.GetTheInstance().SAS360CON_nvreg.Nvreg_change_counter = Globals.GetTheInstance().List_sas360con_nvreg[sas360con_nvreg_from_list_pos].Value!;

                sas360con_nvreg_from_list_pos++;

                sas360con_nvreg_from_list_pos++; //Reserved

                Globals.GetTheInstance().SAS360CON_nvreg.CRC_eeprom = Globals.GetTheInstance().List_sas360con_nvreg[sas360con_nvreg_from_list_pos].Value!;

                sas360con_nvreg_from_list_pos++;
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_memory).Name} -> {nameof(Load_SAS360CON_nvreg_modbus_data)} -> {ex.Message}");
            }
        }

        #endregion

        #endregion



        #region List memory field type pos

        public static void List_memory_fields_type_pos()
        {
            Globals.GetTheInstance().List_sas360con_internal_cfg_byte_pos = new List<int>();
            Globals.GetTheInstance().List_sas360con_internal_cfg_u32_pos = new List<int>();
            Globals.GetTheInstance().List_sas360con_internal_cfg
                .ForEach(internal_config =>
                {
                    if (internal_config.TypeName.Equals("Byte"))
                    {
                        int address = 0;
                        if (internal_config.Addr % 1 == 0)
                        {
                            address = (int)internal_config.Addr;

                            Globals.GetTheInstance().List_sas360con_internal_cfg_byte_pos.Add(address - Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_INTERNAL_CFG]);
                        }
                    }
                    else if (internal_config.TypeName.Equals("UInt32") || internal_config.TypeName.Equals("UTC"))
                    {
                        int address = (int)internal_config.Addr;
                        Globals.GetTheInstance().List_sas360con_internal_cfg_u32_pos.Add(address - Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_INTERNAL_CFG]);
                    }
                });

            Globals.GetTheInstance().List_sas360con_cfg_byte_pos = new List<int>();
            Globals.GetTheInstance().List_sas360con_cfg_int16_pos = new List<int>();
            Globals.GetTheInstance().List_sas360con_cfg_u32_pos = new List<int>();
            Globals.GetTheInstance().List_sas360con_cfg
                .ForEach(config_sas360con =>
                {
                    if (config_sas360con.TypeName.Equals("Byte"))
                    {
                        int address = 0;
                        if (config_sas360con.Addr % 1 == 0)
                        {
                            address = (int)config_sas360con.Addr;

                            Globals.GetTheInstance().List_sas360con_cfg_byte_pos.Add(address - Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG]);
                        }
                    }

                    else if (config_sas360con.TypeName.Equals("Int16"))
                    {
                        int address = (int)config_sas360con.Addr;
                        Globals.GetTheInstance().List_sas360con_cfg_int16_pos.Add(address - Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG]);
                    }

                    else if (config_sas360con.TypeName.Equals("UInt32") || config_sas360con.TypeName.Equals("UTC"))
                    {
                        int address = (int)config_sas360con.Addr;
                        Globals.GetTheInstance().List_sas360con_cfg_u32_pos.Add(address - Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_CFG]);
                    }
                });

            Globals.GetTheInstance().List_sas360con_image_byte_pos = new List<int>();
            Globals.GetTheInstance().List_sas360con_image_u32_pos = new List<int>();
            Globals.GetTheInstance().List_sas360con_image_s32_pos = new List<int>();
            Globals.GetTheInstance().List_sas360con_image_float_pos = new List<int>();
            Globals.GetTheInstance().List_sas360con_image
                .ForEach(image_sas360con =>
                {
                    if (image_sas360con.TypeName.Equals("Byte"))
                    {
                        int address = 0;
                        if (image_sas360con.Addr % 1 == 0)
                        {
                            address = (int)image_sas360con.Addr;

                            Globals.GetTheInstance().List_sas360con_image_byte_pos.Add(address - Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE]);
                        }
                    }
                    else if (image_sas360con.TypeName.Equals("UInt32") || image_sas360con.TypeName.Equals("UTC"))
                    {
                        int address = (int)image_sas360con.Addr;
                        Globals.GetTheInstance().List_sas360con_image_u32_pos.Add(address - Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE]);
                    }
                    else if (image_sas360con.TypeName.Equals("Int32"))
                    {
                        int address = (int)image_sas360con.Addr;
                        Globals.GetTheInstance().List_sas360con_image_s32_pos.Add(address - Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE]);
                    }
                    else if (image_sas360con.TypeName.Equals("Single"))
                    {
                        int address = (int)image_sas360con.Addr;
                        Globals.GetTheInstance().List_sas360con_image_float_pos.Add(address - Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_IMAGE]);
                    }
                });


            Globals.GetTheInstance().List_sas360con_maintennance_byte_pos = new List<int>();
            Globals.GetTheInstance().List_sas360con_maintennance_u32_pos = new List<int>();
            Globals.GetTheInstance().List_sas360con_maintennance
                .ForEach(maintennance =>
                {
                    if (maintennance.TypeName.Equals("Byte"))
                    {
                        int address = 0;
                        if (maintennance.Addr % 1 == 0)
                        {
                            address = (int)maintennance.Addr;

                            Globals.GetTheInstance().List_sas360con_maintennance_byte_pos.Add(address - Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_MAINTENNANCE]);
                        }
                    }
                    else if (maintennance.TypeName.Equals("UInt32") || maintennance.TypeName.Equals("UTC"))
                    {
                        int address = (int)maintennance.Addr;
                        Globals.GetTheInstance().List_sas360con_maintennance_u32_pos.Add(address - Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_MAINTENNANCE]);
                    }
                });


            Globals.GetTheInstance().List_uwb_image_byte_pos = new List<int>();
            Globals.GetTheInstance().List_uwb_image_u32_pos = new List<int>();
            Globals.GetTheInstance().List_uwb_image
                .ForEach(uwb_cfg =>
                {
                    if (uwb_cfg.TypeName.Equals("Byte"))
                    {
                        int address = 0;
                        if (uwb_cfg.Addr % 1 == 0)
                        {
                            address = (int)uwb_cfg.Addr;

                            Globals.GetTheInstance().List_uwb_image_byte_pos.Add(address - Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.UWB_IMAGE]);
                        }
                    }
                    else if (uwb_cfg.TypeName.Equals("UInt32") || uwb_cfg.TypeName.Equals("UTC"))
                    {
                        int address = (int)uwb_cfg.Addr;
                        Globals.GetTheInstance().List_uwb_image_u32_pos.Add(address - Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.UWB_IMAGE]);
                    }
                });

            Globals.GetTheInstance().List_uwb_internal_cfg_u32_pos = new List<int>();
            Globals.GetTheInstance().List_uwb_internal_cfg
                .ForEach(uwb_internal_cfg =>
                {
                    if (uwb_internal_cfg.TypeName.Equals("UInt32") || uwb_internal_cfg.TypeName.Equals("UTC"))
                    {
                        int address = (int)uwb_internal_cfg.Addr;
                        Globals.GetTheInstance().List_uwb_internal_cfg_u32_pos.Add(address - Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.UWB_INTERNAL_CFG]);
                    }
                });

            Globals.GetTheInstance().List_console_closest_tags_base_byte_pos = new List<int>();
            Globals.GetTheInstance().List_console_closest_tags_base_int16_pos = new List<int>();
            Globals.GetTheInstance().List_console_closest_tags_base_u32_pos = new List<int>();
            Globals.GetTheInstance().List_console_closest_tags_base
                .ForEach(console_closest_tags_base =>
                {
                    if (console_closest_tags_base.TypeName.Equals("Byte"))
                    {
                        int address = 0;
                        if (console_closest_tags_base.Addr % 1 == 0)
                        {
                            address = (int)console_closest_tags_base.Addr;

                            Globals.GetTheInstance().List_console_closest_tags_base_byte_pos.Add(address - Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.CONSOLE_CLOSEST_TAGS_BASE]);
                        }
                    }
                    else if (console_closest_tags_base.TypeName.Equals("Int16"))
                    {
                        int address = (int)console_closest_tags_base.Addr;
                        Globals.GetTheInstance().List_console_closest_tags_base_int16_pos.Add(address - Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.CONSOLE_CLOSEST_TAGS_BASE]);
                    }

                    else if (console_closest_tags_base.TypeName.Equals("UInt32") ||   console_closest_tags_base.TypeName.Equals("UTC"))
                    {
                        int address = (int)console_closest_tags_base.Addr;
                        Globals.GetTheInstance().List_console_closest_tags_base_u32_pos.Add(address - Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.CONSOLE_CLOSEST_TAGS_BASE]);
                    }
                });

            Globals.GetTheInstance().List_console_closest_zone_base_byte_pos = new List<int>();
            Globals.GetTheInstance().List_console_closest_zone_base_int16_pos = new List<int>();
            Globals.GetTheInstance().List_console_closest_zone_base_u32_pos = new List<int>();
            Globals.GetTheInstance().List_console_closest_zone_base
                .ForEach(console_closest_zone_base =>
                {
                    if (console_closest_zone_base.TypeName.Equals("Byte"))
                    {
                        int address = 0;
                        if (console_closest_zone_base.Addr % 1 == 0)
                        {
                            address = (int)console_closest_zone_base.Addr;

                            Globals.GetTheInstance().List_console_closest_zone_base_byte_pos.Add(address - Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.CONSOLE_CLOSEST_ZONE_BASE]);
                        }
                    }
                    else if (console_closest_zone_base.TypeName.Equals("Int16"))
                    {
                        int address = (int)console_closest_zone_base.Addr;
                        Globals.GetTheInstance().List_console_closest_zone_base_int16_pos.Add(address - Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.CONSOLE_CLOSEST_ZONE_BASE]);
                    }

                    else if (console_closest_zone_base.TypeName.Equals("UInt32") || console_closest_zone_base.TypeName.Equals("UTC"))
                    {
                        int address = (int)console_closest_zone_base.Addr;
                        Globals.GetTheInstance().List_console_closest_zone_base_u32_pos.Add(address - Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.CONSOLE_CLOSEST_ZONE_BASE]);
                    }
                });


            Globals.GetTheInstance().List_sas360con_nvreg_byte_pos = new List<int>();
            Globals.GetTheInstance().List_sas360con_nvreg_u32_pos = new List<int>();
            Globals.GetTheInstance().List_sas360con_nvreg
                .ForEach(nvreg =>
                {
                    if (nvreg.TypeName.Equals("Byte"))
                    {
                        int address = 0;
                        if (nvreg.Addr % 1 == 0)
                        {
                            address = (int)nvreg.Addr;

                            Globals.GetTheInstance().List_sas360con_nvreg_byte_pos.Add(address - Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_NVREG]);
                        }
                    }
                    else if (nvreg.TypeName.Equals("UInt32") || nvreg.TypeName.Equals("UTC"))
                    {
                        int address = (int)nvreg.Addr;
                        Globals.GetTheInstance().List_sas360con_nvreg_u32_pos.Add(address - Globals.GetTheInstance().Memory_map_ini[(int)ENABLE_READ_MEMORY_BIT.SAS360CON_NVREG]);
                    }
                });
        }

        #endregion

        #region Memory convert functions

        public static string SAS360CON_MODBUS_SPEED(ushort value)
        {
            string s_modbus =
                value == (ushort)MASTER_SPEED._9600 ? "9600" :
                value == (ushort)MASTER_SPEED._19200 ? "19200" :
                value == (ushort)MASTER_SPEED._38400 ? "38400" :
                value == (ushort)MASTER_SPEED._57600 ? "57600" :
                value == (ushort)MASTER_SPEED._11520 ? "115200" : "9600";

            return s_modbus;
        }

        public static string SAS360CON_DI1()
        {
            string s_di1 = string.Empty;
            for (int index = 0; index < Enum.GetNames(typeof(MASK_CODIF_DI1)).Length; index++)
            {
                if (Functions.IsBitSetTo1(Globals.GetTheInstance().SAS360CON_image_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.INPUT], index))
                {
                    s_di1 += Enum.GetName(typeof(MASK_CODIF_DI1), index) + "\r\n";
                }
            }

            return s_di1;
        }
        public static string SAS360CON_DO1()
        {
            string s_do1 = string.Empty;
            for (int index = 0; index < Enum.GetNames(typeof(FORCE_MASK_DO1)).Length; index++)
            {
                if (Functions.IsBitSetTo1(Globals.GetTheInstance().SAS360CON_image_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.OUTPUT_1_INT], index))
                {
                    s_do1 += Enum.GetName(typeof(FORCE_MASK_DO1), index) + "\r\n";
                }
            }

            return s_do1;
        }
        public static string SAS360CON_DO2()
        {
            string s_do2 = string.Empty;
            for (int index = 0; index < Enum.GetNames(typeof(FORCE_MASK_DO2)).Length; index++)
            {
                if (Functions.IsBitSetTo1(Globals.GetTheInstance().SAS360CON_image_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.OUTPUT_2_EXT], index))
                {
                    s_do2 += Enum.GetName(typeof(FORCE_MASK_DO2), index) + "\r\n";
                }
            }

            return s_do2;
        }
        public static string SAS360CON_DO3()
        {
            string s_do3 = string.Empty;
            for (int index = 0; index < Enum.GetNames(typeof(FORCE_MASK_DO3)).Length; index++)
            {
                if (Functions.IsBitSetTo1(Globals.GetTheInstance().SAS360CON_image_general.Array_digital_states[(int)DIGITAL_STATES_IN_LIST.OUTPUT_3_LED], index))
                {
                    s_do3 += Enum.GetName(typeof(FORCE_MASK_DO3), index) + "\r\n";
                }
            }

            return s_do3;
        }
        public static string SAS360CON_internal_error()
        {
            string s_internal_error = string.Empty;
            for (int index = 0; index < Enum.GetNames(typeof(MASK_ERROR)).Length; index++)
            {
                if (Functions.IsBitSetTo1(Globals.GetTheInstance().SAS360CON_image_main_management.Internal_error, index))
                {
                    s_internal_error += Enum.GetName(typeof(MASK_ERROR), index) + "\r\n";
                }
            }

            return s_internal_error;
        }
        public static string SAS360CON_lin_read_pool()
        {
            string s_lin_read_pool = string.Empty;
            for (int index = 0; index < Enum.GetNames(typeof(MASK_LIN_READ_POOL)).Length; index++)
            {
                if (Functions.IsBitSetTo1(Globals.GetTheInstance().SAS360CON_image_lin_pooling.Array_lin_pooling_read_uwb[0], index))
                {
                    s_lin_read_pool += Enum.GetName(typeof(MASK_LIN_READ_POOL), index) + "\r\n";
                }
            }

            return s_lin_read_pool;
        }
        public static string SAS360CON_lin_write_pool()
        {
            string s_lin_write_pool = string.Empty;
            for (int index = 0; index < Enum.GetNames(typeof(MASK_LIN_WRITE_POOL)).Length; index++)
            {
                if (Functions.IsBitSetTo1(Globals.GetTheInstance().SAS360CON_image_lin_pooling.Array_lin_pooling_write_uwb[0], index))
                {
                    s_lin_write_pool += Enum.GetName(typeof(MASK_LIN_WRITE_POOL), index) + "\r\n";
                }
            }

            return s_lin_write_pool;
        }
        public static string SAS360CON_lin_write_pool_broadcast()
        {
            string s_lin_write_pool_broadcast = string.Empty;
            for (int index = 0; index < Enum.GetNames(typeof(MASK_LIN_WRITE_POOL_BROADCAST)).Length; index++)
            {
                if (Functions.IsBitSetTo1(Globals.GetTheInstance().SAS360CON_image_lin_pooling.Lin_pooling_write_broadcast, index))
                {
                    s_lin_write_pool_broadcast += Enum.GetName(typeof(MASK_LIN_WRITE_POOL_BROADCAST), index) + "\r\n";
                }
            }

            return s_lin_write_pool_broadcast;
        }
        public static string SAS360CON_lin_pool_state()
        {
            string s_lin_pooling = Globals.GetTheInstance().SAS360CON_image_lin_pooling.Lin_pooling_state.ToString();
            if (Enum.IsDefined(typeof(MASK_LIN_STATE), Globals.GetTheInstance().SAS360CON_image_lin_pooling.Lin_pooling_state))
            {
                s_lin_pooling = Enum.GetName(typeof(MASK_LIN_STATE), Globals.GetTheInstance().SAS360CON_image_lin_pooling.Lin_pooling_state)!;
            }

            return s_lin_pooling;
        }
        public static string SAS360TAG_ZONE_TYPE(MASK_TAG_ZONE_TYPE tag_type)
        {
            string s_type =
               tag_type == MASK_TAG_ZONE_TYPE.SAS360TAG_PED ? "PED" :
               tag_type == MASK_TAG_ZONE_TYPE.SAS360TAG_DRV ? "DRV" :
               tag_type == MASK_TAG_ZONE_TYPE.SAS360CON_TAG_LV ? "LV" :
               tag_type == MASK_TAG_ZONE_TYPE.SAS360CON_TAG_HV ? "HV" :
               tag_type == MASK_TAG_ZONE_TYPE.SAS360ZONE_CIRC_R_SLOW ? "R SLOW" :
               tag_type == MASK_TAG_ZONE_TYPE.SAS360ZONE_REC_P1_SLOW ? "P1 SLOW" :
               tag_type == MASK_TAG_ZONE_TYPE.SAS360ZONE_REC_P2_SLOW ? "P2 SLOW" :
               tag_type == MASK_TAG_ZONE_TYPE.SAS360ZONE_REC_P3_SLOW ? "P3 SLOW" :
               tag_type == MASK_TAG_ZONE_TYPE.SAS360ZONE_REC_P4_SLOW ? "P4 SLOW" :
               tag_type == MASK_TAG_ZONE_TYPE.SAS360ZONE_INHIBIT_RAD ? "INHIBIT RAD" :
               "UNKNOWN";

            return s_type;
        }
        public static string SAS360TAG_STATUS(ushort value)
        {
            string s_tag_status = string.Empty;
            for (int index = 0; index < Enum.GetNames(typeof(MASK_TAG_STATUS)).Length; index++)
            {
                if (Functions.IsBitSetTo1(value, index))
                {
                    s_tag_status += Enum.GetName(typeof(MASK_TAG_STATUS), index) + "\r\n";
                }
            }

            return s_tag_status;
        }
        public static string SAS360ZONE_STATUS(ushort value)
        {

            string s_zone_status = string.Empty;
            for (int index = 0; index < Enum.GetNames(typeof(MASK_ZONE_STATUS)).Length; index++)
            {
                if (Functions.IsBitSetTo1(value, index))
                {
                    s_zone_status += Enum.GetName(typeof(MASK_ZONE_STATUS), index) + "\r\n";
                }
            }

            return s_zone_status;
        }
        public static string SAS360TAG_UWB_COMMAND(ushort value)
        {
            string s_uwb_command = string.Empty;
            for (int index = 0; index < Enum.GetNames(typeof(MASK_SAS360TAG_UWB_COMMAND)).Length; index++)
            {
                if (Functions.IsBitSetTo1(value, index))
                {
                    s_uwb_command += Enum.GetName(typeof(MASK_SAS360TAG_UWB_COMMAND), index) + "\r\n";
                }
            }

            return s_uwb_command;
        }

        #endregion
    }
}


