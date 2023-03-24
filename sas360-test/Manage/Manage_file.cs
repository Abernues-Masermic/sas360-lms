using sas360_test;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace sas360_test
{
    public class Manage_file
    {
        #region Create directories

        public static bool Create_directories()
        {
            bool create_ok = true;

            string[] dirs = new string[] { Constants.Setting_dir, Constants.Log_dir, Constants.Memory_map_dir };
            try
            {
                dirs.ToList().ForEach(dir =>
                {
                    if (!Directory.Exists($"{AppDomain.CurrentDomain.BaseDirectory}{dir}"))
                        Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}{dir}");
                });

            }
            catch (Exception ex)
            {
                create_ok = false;
                Manage_logs.SaveErrorValue($"{typeof(Manage_file).Name} -> {nameof(Create_directories)} -> {ex.Message}");
            }

            return create_ok;
        }

        #endregion

        #region Create files

        public static void Create_files()
        {
            List<string> list_file_name_xml = new() { "SettingApp.xml", "SettingComm.xml" };

            try
            {
                list_file_name_xml.ForEach(file =>
                {
                    if (!File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\{Constants.Setting_dir}\\{file}"))
                    {
                        XmlDocument xmlDoc = new();
                        XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                        XmlElement rootNode = xmlDoc.CreateElement("RAIZ");
                        xmlDoc.InsertBefore(xmlDeclaration, xmlDoc.DocumentElement);
                        xmlDoc.AppendChild(rootNode);
                        xmlDoc.Save($"{AppDomain.CurrentDomain.BaseDirectory}\\{Constants.Setting_dir}\\{file}");
                    }
                });
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_file).Name} -> {nameof(Create_files)} -> {ex.Message}");
            }
        }

        #endregion

        #region Load app setting

        public static void Load_app_setting()
        {
            try
            {
                XmlDocument xdoc = new();
                xdoc.Load($"{AppDomain.CurrentDomain.BaseDirectory}\\{Constants.Setting_dir}\\SettingApp.xml");
                XmlNodeList nodelist = xdoc.SelectNodes("/body")!;

                if (nodelist.Count > 0)
                {
                    #region General

                    int i_simulator_mode = nodelist[0]!.SelectSingleNode("Simulator_mode") != null ? int.Parse(nodelist[0]!.SelectSingleNode("Simulator_mode")!.InnerText) : (int)BIT_STATE.OFF;
                    Globals.GetTheInstance().Simulator_mode = (BIT_STATE)i_simulator_mode;

                    Globals.GetTheInstance().Panel_area_cm = nodelist[0]!.SelectSingleNode("Panel_area") != null ? int.Parse(nodelist[0]!.SelectSingleNode("Panel_area")!.InnerText) : 1500;
                    Globals.GetTheInstance().Grid_area_cm = nodelist[0]!.SelectSingleNode("Grid_area") != null ? int.Parse(nodelist[0]!.SelectSingleNode("Grid_area")!.InnerText) : 100;
                    Globals.GetTheInstance().Total_closest_tags = nodelist[0]!.SelectSingleNode("Total_closest_tags") != null ? int.Parse(nodelist[0]!.SelectSingleNode("Total_closest_tags")!.InnerText) : 12;
                    Globals.GetTheInstance().Total_closest_zone = nodelist[0]!.SelectSingleNode("Total_closest_zone") != null ? int.Parse(nodelist[0]!.SelectSingleNode("Total_closest_zone")!.InnerText) : 16;

                    Globals.GetTheInstance().DateFormat = nodelist[0]!.SelectSingleNode("Date_format") != null ? nodelist[0]!.SelectSingleNode("Date_format")!.InnerText : "yyyy/MM/dd HH:mm:ss";
                    Globals.GetTheInstance().DateProvider = nodelist[0]!.SelectSingleNode("Date_provider") != null ? nodelist[0]!.SelectSingleNode("Date_provider")!.InnerText : "es-ES";

                    #endregion

                    #region Read - write memory

                    Globals.GetTheInstance().Enable_read_memory_bits = nodelist[0]!.SelectSingleNode("Enable_read_memory_bits") != null ? int.Parse(nodelist[0]!.SelectSingleNode("Enable_read_memory_bits")!.InnerText) : int.MaxValue;
                    Globals.GetTheInstance().Code_init = nodelist[0]!.SelectSingleNode("Write_code_init") != null ? ushort.Parse(nodelist[0]!.SelectSingleNode("Write_code_init")!.InnerText) : (ushort)170;
                    Globals.GetTheInstance().Code_prod = nodelist[0]!.SelectSingleNode("Write_code_prod") != null ? ushort.Parse(nodelist[0]!.SelectSingleNode("Write_code_prod")!.InnerText) : (ushort)90;
                    Globals.GetTheInstance().Code_depu = nodelist[0]!.SelectSingleNode("Write_code_depu") != null ? ushort.Parse(nodelist[0]!.SelectSingleNode("Write_code_depu")!.InnerText) : (ushort)170;

                    #endregion

                    #region Path

                    Globals.GetTheInstance().Path_internal_config = nodelist[0]!.SelectSingleNode("Path_internal_config") != null ? nodelist[0]!.SelectSingleNode("Path_internal_config")!.InnerText : string.Empty;
                    Globals.GetTheInstance().Path_config_sas360con = nodelist[0]!.SelectSingleNode("Path_config_sas360con") != null ? nodelist[0]!.SelectSingleNode("Path_config_sas360con")!.InnerText : string.Empty;
                    Globals.GetTheInstance().Path_config_iot = nodelist[0]!.SelectSingleNode("Path_config_iot") != null ? nodelist[0]!.SelectSingleNode("Path_config_iot")!.InnerText : string.Empty;
                    Globals.GetTheInstance().Path_image_sas360con = nodelist[0]!.SelectSingleNode("Path_image_sas360con") != null ? nodelist[0]!.SelectSingleNode("Path_image_sas360con")!.InnerText : string.Empty;
                    Globals.GetTheInstance().Path_image_iot = nodelist[0]!.SelectSingleNode("Path_image_iot") != null ? nodelist[0]!.SelectSingleNode("Path_image_iot")!.InnerText : string.Empty;
                    
                    Globals.GetTheInstance().Path_console_closest_tags_base = nodelist[0]!.SelectSingleNode("Path_console_closest_tags_base") != null ? nodelist[0]!.SelectSingleNode("Path_console_closest_tags_base")!.InnerText : string.Empty;
                    Globals.GetTheInstance().Path_console_closest_tags_extended = nodelist[0]!.SelectSingleNode("Path_console_closest_tags_extended") != null ? nodelist[0]!.SelectSingleNode("Path_console_closest_tags_extended")!.InnerText : string.Empty;
                    Globals.GetTheInstance().Path_uwb_closest_tags_base = nodelist[0]!.SelectSingleNode("Path_uwb_closest_tags_base") != null ? nodelist[0]!.SelectSingleNode("Path_uwb_closest_tags_base")!.InnerText : string.Empty;
                    Globals.GetTheInstance().Path_uwb_closest_tags_extended = nodelist[0]!.SelectSingleNode("Path_uwb_closest_tags_extended") != null ? nodelist[0]!.SelectSingleNode("Path_uwb_closest_tags_extended")!.InnerText : string.Empty;

                    Globals.GetTheInstance().Path_console_closest_zone_base = nodelist[0]!.SelectSingleNode("Path_console_closest_zone_base") != null ? nodelist[0]!.SelectSingleNode("Path_console_closest_zone_base")!.InnerText : string.Empty;
                    Globals.GetTheInstance().Path_console_closest_zone_extended = nodelist[0]!.SelectSingleNode("Path_console_closest_zone_extended") != null ? nodelist[0]!.SelectSingleNode("Path_console_closest_zone_extended")!.InnerText : string.Empty;
                    Globals.GetTheInstance().Path_nvreg = nodelist[0]!.SelectSingleNode("Path_nvreg") != null ? nodelist[0]!.SelectSingleNode("Path_nvreg")!.InnerText : string.Empty;

                    Globals.GetTheInstance().Path_commands = nodelist[0]!.SelectSingleNode("Path_commands") != null ? nodelist[0]!.SelectSingleNode("Path_commands")!.InnerText : string.Empty;

                    #endregion
                }
                else 
                {
                    #region General

                    Globals.GetTheInstance().Simulator_mode = BIT_STATE.OFF;

                    Globals.GetTheInstance().Panel_area_cm =1500;
                    Globals.GetTheInstance().Grid_area_cm = 100;
                    Globals.GetTheInstance().Total_closest_tags = 12;
                    Globals.GetTheInstance().Total_closest_zone = 16;
                    Globals.GetTheInstance().DateFormat = "yyyy/MM/dd HH:mm:ss";
                    Globals.GetTheInstance().DateProvider = "es-ES";

                    #endregion

                    #region Read - write memory

                    Globals.GetTheInstance().Enable_read_memory_bits = int.MaxValue;
                    Globals.GetTheInstance().Code_init = 170;
                    Globals.GetTheInstance().Code_prod = 90;
                    Globals.GetTheInstance().Code_depu = 170;

                    #endregion

                    #region Path

                    Globals.GetTheInstance().Path_internal_config = string.Empty;
                    Globals.GetTheInstance().Path_config_sas360con = string.Empty;
                    Globals.GetTheInstance().Path_config_iot = string.Empty;
                    Globals.GetTheInstance().Path_image_sas360con =  string.Empty;

                    Globals.GetTheInstance().Path_console_closest_tags_base = string.Empty;
                    Globals.GetTheInstance().Path_console_closest_tags_extended = string.Empty;
                    Globals.GetTheInstance().Path_uwb_closest_tags_base = string.Empty;
                    Globals.GetTheInstance().Path_uwb_closest_tags_extended = string.Empty;

                    Globals.GetTheInstance().Path_console_closest_zone_base = string.Empty;
                    Globals.GetTheInstance().Path_console_closest_zone_extended = string.Empty;
                   
                    Globals.GetTheInstance().Path_nvreg = string.Empty;

                    Globals.GetTheInstance().Path_commands = string.Empty;

                    #endregion
                }
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_file).Name} -> {nameof(Load_app_setting)} -> {ex.Message}");
            }
        }

        #endregion


        #region Save app setting

        public static bool Save_app_setting()
        {
            bool saved = false;
            try
            {
                XmlDocument doc = new();

                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = doc.DocumentElement!;
                doc.InsertBefore(xmlDeclaration, root);

                XmlElement element_body = doc.CreateElement(string.Empty, "body", string.Empty);
                doc.AppendChild(element_body);

                #region General

                XmlElement element_simulator_mode = doc.CreateElement(string.Empty, "Simulator_mode", string.Empty);
                int i_simulator_mode = (int)Globals.GetTheInstance().Simulator_mode;
                XmlText text_simulator_mode= doc.CreateTextNode(i_simulator_mode.ToString());
                element_simulator_mode.AppendChild(text_simulator_mode);
                element_body.AppendChild(element_simulator_mode);

                XmlElement element_panel_area = doc.CreateElement(string.Empty, "Panel_area", string.Empty);
                XmlText text_panel_area = doc.CreateTextNode(Globals.GetTheInstance().Panel_area_cm.ToString());
                element_panel_area .AppendChild(text_panel_area );
                element_body.AppendChild(element_panel_area );

                XmlElement element_grid_area = doc.CreateElement(string.Empty, "Grid_area", string.Empty);
                XmlText text_grid_area = doc.CreateTextNode(Globals.GetTheInstance().Grid_area_cm.ToString());
                element_grid_area.AppendChild(text_grid_area);
                element_body.AppendChild(element_grid_area);

                XmlElement element_total_closest_tags = doc.CreateElement(string.Empty, "Total_closest_tags", string.Empty);
                XmlText text_total_closest_tags = doc.CreateTextNode(Globals.GetTheInstance().Total_closest_tags.ToString());
                element_total_closest_tags.AppendChild(text_total_closest_tags);
                element_body.AppendChild(element_total_closest_tags);

                XmlElement element_total_closest_zone = doc.CreateElement(string.Empty, "Total_closest_zone", string.Empty);
                XmlText text_total_closest_zone = doc.CreateTextNode(Globals.GetTheInstance().Total_closest_zone.ToString());
                element_total_closest_zone.AppendChild(text_total_closest_zone);
                element_body.AppendChild(element_total_closest_zone);

                XmlElement element_date_format = doc.CreateElement(string.Empty, "Date_format", string.Empty);
                XmlText text_date_format = doc.CreateTextNode(Globals.GetTheInstance().DateFormat);
                element_date_format.AppendChild(text_date_format);
                element_body.AppendChild(element_date_format);

                XmlElement element_date_provider = doc.CreateElement(string.Empty, "Date_provider", string.Empty);
                XmlText text_date_provider = doc.CreateTextNode(Globals.GetTheInstance().DateProvider);
                element_date_provider.AppendChild(text_date_provider);
                element_body.AppendChild(element_date_provider);

                #endregion

                #region Read - write memory

                XmlElement element_enable_read_memory_bits = doc.CreateElement(string.Empty, "Enable_read_memory_bits", string.Empty);
                XmlText text_enable_read_memory_bits = doc.CreateTextNode(Globals.GetTheInstance().Enable_read_memory_bits.ToString());
                element_enable_read_memory_bits.AppendChild(text_enable_read_memory_bits);
                element_body.AppendChild(element_enable_read_memory_bits);

                XmlElement element_write_code_init = doc.CreateElement(string.Empty, "Write_code_init", string.Empty);
                XmlText text_write_code_init = doc.CreateTextNode(Globals.GetTheInstance().Code_init.ToString());
                element_write_code_init.AppendChild(text_write_code_init);
                element_body.AppendChild(element_write_code_init);

                XmlElement element_write_code_prod = doc.CreateElement(string.Empty, "Write_code_prod", string.Empty);
                XmlText text_write_code_prod = doc.CreateTextNode(Globals.GetTheInstance().Code_prod.ToString());
                element_write_code_prod.AppendChild(text_write_code_prod);
                element_body.AppendChild(element_write_code_prod);

                XmlElement element_write_code_depu = doc.CreateElement(string.Empty, "Write_code_depu", string.Empty);
                XmlText text_write_code_depu = doc.CreateTextNode(Globals.GetTheInstance().Code_depu.ToString());
                element_write_code_depu.AppendChild(text_write_code_depu);
                element_body.AppendChild(element_write_code_depu);

                #endregion

                #region Path

                XmlElement element_internal_config = doc.CreateElement(string.Empty, "Path_internal_config", string.Empty);
                XmlText text_internal_config = doc.CreateTextNode(Globals.GetTheInstance().Path_internal_config);
                element_internal_config.AppendChild(text_internal_config);
                element_body.AppendChild(element_internal_config);

                XmlElement element_config_sas360con = doc.CreateElement(string.Empty, "Path_config_sas360con", string.Empty);
                XmlText text_config = doc.CreateTextNode(Globals.GetTheInstance().Path_config_sas360con);
                element_config_sas360con.AppendChild(text_config);
                element_body.AppendChild(element_config_sas360con);

                XmlElement element_config_iot = doc.CreateElement(string.Empty, "Path_config_iot", string.Empty);
                XmlText text_config_iot = doc.CreateTextNode(Globals.GetTheInstance().Path_config_iot);
                element_config_iot.AppendChild(text_config_iot);
                element_body.AppendChild(element_config_iot);

                XmlElement element_image_sas360con= doc.CreateElement(string.Empty, "Path_image_sas360con", string.Empty);
                XmlText text_image_sas360con = doc.CreateTextNode(Globals.GetTheInstance().Path_image_sas360con);
                element_image_sas360con.AppendChild(text_image_sas360con);
                element_body.AppendChild(element_image_sas360con);

                XmlElement element_image_iot = doc.CreateElement(string.Empty, "Path_image_iot", string.Empty);
                XmlText text_image_iot = doc.CreateTextNode(Globals.GetTheInstance().Path_image_iot);
                element_image_iot.AppendChild(text_image_iot);
                element_body.AppendChild(element_image_iot);

                XmlElement element_console_closest_tags_base = doc.CreateElement(string.Empty, "Path_console_closest_tags_base", string.Empty);
                XmlText text_console_closest_tags_base = doc.CreateTextNode(Globals.GetTheInstance().Path_console_closest_tags_base);
                element_console_closest_tags_base.AppendChild(text_console_closest_tags_base);
                element_body.AppendChild(element_console_closest_tags_base);

                XmlElement element_console_closest_tags_extended = doc.CreateElement(string.Empty, "Path_console_closest_tags_extended", string.Empty);
                XmlText text_console_closest_tags_extended = doc.CreateTextNode(Globals.GetTheInstance().Path_console_closest_tags_extended);
                element_console_closest_tags_extended.AppendChild(text_console_closest_tags_extended);
                element_body.AppendChild(element_console_closest_tags_extended);

                XmlElement element_uwb_closest_tags_base = doc.CreateElement(string.Empty, "Path_uwb_closest_tags_base", string.Empty);
                XmlText text_uwb_closest_tags_base = doc.CreateTextNode(Globals.GetTheInstance().Path_uwb_closest_tags_base);
                element_uwb_closest_tags_base.AppendChild(text_uwb_closest_tags_base);
                element_body.AppendChild(element_uwb_closest_tags_base);

                XmlElement element_uwb_closest_tags_extended = doc.CreateElement(string.Empty, "Path_uwb_closest_tags_extended", string.Empty);
                XmlText text_uwb_closest_tags_extended = doc.CreateTextNode(Globals.GetTheInstance().Path_uwb_closest_tags_extended);
                element_uwb_closest_tags_extended.AppendChild(text_uwb_closest_tags_extended);
                element_body.AppendChild(element_uwb_closest_tags_extended);

                XmlElement element_console_closest_zone_base = doc.CreateElement(string.Empty, "Path_console_closest_zone_base", string.Empty);
                XmlText text_console_closest_zone_base = doc.CreateTextNode(Globals.GetTheInstance().Path_console_closest_zone_base);
                element_console_closest_zone_base.AppendChild(text_console_closest_zone_base);
                element_body.AppendChild(element_console_closest_zone_base);

                XmlElement element_console_closest_zone_extended = doc.CreateElement(string.Empty, "Path_console_closest_zone_extended", string.Empty);
                XmlText text_console_closest_zone_extended = doc.CreateTextNode(Globals.GetTheInstance().Path_console_closest_zone_extended);
                element_console_closest_zone_extended.AppendChild(text_console_closest_zone_extended);
                element_body.AppendChild(element_console_closest_zone_extended);

                XmlElement element_nvreg = doc.CreateElement(string.Empty, "Path_nvreg", string.Empty);
                XmlText text_nvreg = doc.CreateTextNode(Globals.GetTheInstance().Path_nvreg);
                element_nvreg.AppendChild(text_nvreg);
                element_body.AppendChild(element_nvreg);


                XmlElement element_commands= doc.CreateElement(string.Empty, "Path_commands", string.Empty);
                XmlText text_commands = doc.CreateTextNode(Globals.GetTheInstance().Path_commands);
                element_commands.AppendChild(text_commands);
                element_body.AppendChild(element_commands);

                #endregion


                doc.Save($"{AppDomain.CurrentDomain.BaseDirectory}\\{Constants.Setting_dir}\\SettingApp.xml");

                saved = true;
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_file).Name} -> {nameof(Save_app_setting)} -> {ex.Message}");
            }

            return saved;
        }

        #endregion


        #region Load comm setting

        public static void Load_comm_setting()
        {
            try
            {
                XmlDocument xdoc = new();
                xdoc.Load($"{AppDomain.CurrentDomain.BaseDirectory}\\{Constants.Setting_dir}\\SettingComm.xml");
                XmlNodeList nodelist = xdoc.SelectNodes("/body")!;

                if (nodelist.Count > 0)
                {
                    Globals.GetTheInstance().Unit_id = nodelist[0]!.SelectSingleNode("unit_id") != null ? byte.Parse(nodelist[0]!.SelectSingleNode("unit_id")!.InnerText) : (byte)0;
                    Globals.GetTheInstance().Comm_port = nodelist[0]!.SelectSingleNode("comm_port") != null ? nodelist[0]!.SelectSingleNode("comm_port")!.InnerText : string.Empty;
                    Globals.GetTheInstance().Baud_rate = nodelist[0]!.SelectSingleNode("baud_rate") != null ? int.Parse(nodelist[0]!.SelectSingleNode("baud_rate")!.InnerText) : 9600;
                    Globals.GetTheInstance().Modbus_connection_timeout = nodelist[0]!.SelectSingleNode("conn_timeout") != null ? int.Parse(nodelist[0]!.SelectSingleNode("conn_timeout")!.InnerText) : 1000;
                    Globals.GetTheInstance().Read_memory_interval = nodelist[0]!.SelectSingleNode("read_memory") != null ? int.Parse(nodelist[0]!.SelectSingleNode("read_memory")!.InnerText) : 5000;
                }
                else {
                    Globals.GetTheInstance().Unit_id = 0;
                    Globals.GetTheInstance().Comm_port = string.Empty;
                    Globals.GetTheInstance().Baud_rate = 9600;
                    Globals.GetTheInstance().Modbus_connection_timeout = 1000;
                    Globals.GetTheInstance().Read_memory_interval = 5000;
                }
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_file).Name} -> {nameof(Load_comm_setting)} -> {ex.Message}");
            }
        }

        #endregion


        #region Save comm setting

        public static bool Save_comm_setting()
        {
            bool saved = false;
            try
            {
                XmlDocument doc = new();

                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = doc.DocumentElement!;
                doc.InsertBefore(xmlDeclaration, root);

                XmlElement element_body = doc.CreateElement(string.Empty, "body", string.Empty);
                doc.AppendChild(element_body);

                XmlElement element_internal_config = doc.CreateElement(string.Empty, "unit_id", string.Empty);
                XmlText text_internal_config = doc.CreateTextNode(Globals.GetTheInstance().Unit_id.ToString());
                element_internal_config.AppendChild(text_internal_config);
                element_body.AppendChild(element_internal_config);

                XmlElement element_config = doc.CreateElement(string.Empty, "comm_port", string.Empty);
                XmlText text_config = doc.CreateTextNode(Globals.GetTheInstance().Comm_port);
                element_config.AppendChild(text_config);
                element_body.AppendChild(element_config);

                XmlElement element_image = doc.CreateElement(string.Empty, "baud_rate", string.Empty);
                XmlText text_image = doc.CreateTextNode(Globals.GetTheInstance().Baud_rate.ToString());
                element_image.AppendChild(text_image);
                element_body.AppendChild(element_image);

                XmlElement element_conn_timeout = doc.CreateElement(string.Empty, "conn_timeout", string.Empty);
                XmlText text_conn_timeout= doc.CreateTextNode(Globals.GetTheInstance().Modbus_connection_timeout.ToString());
                element_conn_timeout.AppendChild(text_conn_timeout);
                element_body.AppendChild(element_conn_timeout);

                XmlElement element_read_interval = doc.CreateElement(string.Empty, "read_memory", string.Empty);
                XmlText text_read_interval = doc.CreateTextNode(Globals.GetTheInstance().Read_memory_interval.ToString());
                element_read_interval.AppendChild(text_read_interval);
                element_body.AppendChild(element_read_interval);

                doc.Save($"{AppDomain.CurrentDomain.BaseDirectory}\\{Constants.Setting_dir}\\SettingComm.xml");

                saved = true;
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{typeof(Manage_file).Name} -> {nameof(Save_comm_setting)} -> {ex.Message}");
            }

            return saved;
        }

        #endregion

    }
}
