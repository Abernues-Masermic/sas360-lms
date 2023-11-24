using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;

namespace sas360_test
{
    internal class Manage_comm
    {
        private delegate void SetTextDeleg(byte[] array_data);

        private System.Timers.Timer m_timer_com_timeout;
        private DateTime m_date_last_received;

        private SerialPort? m_port;
        private byte m_unit_id;

        private MEMORY_CONFIG_TYPE m_memory_config_type;

        private const byte READ_HOLDING_REGISTER = 0x03;
        private const byte WRITE_MULTIPLE_REGISTER = 0x10;

        private int m_num_error_comm;


        #region Constructor
        public Manage_comm()
        {
            m_timer_com_timeout = new System.Timers.Timer();
            m_timer_com_timeout.Elapsed += Timer_com_timeout_Tick!;
            m_timer_com_timeout.Interval = 1000;
            m_timer_com_timeout.Stop();
        }

        #endregion



        #region Connect

        public bool Connect(string com_port, int baud_rate, byte unit_id)
        {
            bool is_connected = false;
            m_unit_id = unit_id;

            try
            {
                m_port = new(com_port)
                {
                    BaudRate = baud_rate,
                    DataBits = 8,
                    Parity = Parity.None,
                    StopBits = StopBits.One
                };

                if (Globals.GetTheInstance().Simulator_mode == BIT_STATE.OFF)
                {
                    m_port.Open();
                    m_port.DataReceived += new SerialDataReceivedEventHandler(Serial_datareceived);
                }

                m_num_error_comm = 0;
                is_connected = true;
                m_timer_com_timeout.Start();
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{GetType().Name} ->  {nameof(Connect)} -> {ex.Message}");
                Globals.GetTheInstance().Manage_delegate.Manage_rtu_to_main(RTU_ACTION.ERROR_CONNECT, new List<ushort>(), MEMORY_CONFIG_TYPE.NONE);
            }

            return is_connected;
        }

        #endregion

        #region Disconnect

        public bool Disconnect()
        {
            bool disconnect_ok = false;
            try
            {
                if (Globals.GetTheInstance().Simulator_mode == BIT_STATE.OFF)
                {
                    m_port!.DataReceived -= new SerialDataReceivedEventHandler(Serial_datareceived);
                    m_port.Close();
                    Manage_logs.SaveLogValue($"{m_port!.PortName} -> DISCONNECTED");
                }

                m_timer_com_timeout.Stop();
                disconnect_ok = true;
                Globals.GetTheInstance().Manage_delegate.Manage_rtu_to_main(RTU_ACTION.DISCONNECT, new List<ushort>(), MEMORY_CONFIG_TYPE.NONE);
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{GetType().Name} ->  {nameof(Disconnect)} -> {ex.Message}");
            }

            return disconnect_ok;
        }

        #endregion





        #region Read - write

        private bool b_manage_data = new bool();
        private List<byte> m_list_manage_data = new();

        private byte m_function;
        private ushort m_start_address;
        private ushort m_number_of_register;

        public bool Read_holding_registers_int32(ushort start_address, ushort number_off_registers, MEMORY_CONFIG_TYPE memory_config_type)
        {
            m_memory_config_type = memory_config_type;

            bool read_ok = false;
            try
            {
                List<byte> list_data = new()
                {
                    m_unit_id,
                    READ_HOLDING_REGISTER
                };

                byte[] byte_address = BitConverter.GetBytes(start_address);
                byte_address = byte_address.Reverse().ToArray();
                list_data.AddRange(byte_address);

                byte[] byte_num_reg = BitConverter.GetBytes(number_off_registers);
                byte_num_reg = byte_num_reg.Reverse().ToArray();
                list_data.AddRange(byte_num_reg);

                byte[] byte_crc = CRC.CRC16_array(list_data.ToArray(), list_data.Count);
                list_data.AddRange(byte_crc);

                m_port!.DiscardInBuffer();
                m_port.DiscardOutBuffer();
                m_port.Write(list_data.ToArray(), 0, list_data.Count);

                m_list_manage_data.Clear();

                m_function = READ_HOLDING_REGISTER;
                m_start_address = start_address;
                m_number_of_register = number_off_registers;

                string s_data = string.Empty;
                list_data.ForEach(data => s_data += $"0X{data:X2} ");

                Manage_logs.SaveDataValue("---------------------------------------------------------------------------------");
                Manage_logs.SaveDataValue($"SEND ({m_memory_config_type}) -> {s_data}");

                m_num_error_comm = 0;
                read_ok = true;
            }
            catch (IOException ex)
            {
                Globals.GetTheInstance().Manage_delegate.Manage_rtu_to_main(RTU_ACTION.DISCONNECT_FROM_READ_WRITE, new List<ushort>(), m_memory_config_type);
                Manage_logs.SaveModbusValue($"{GetType().Name} ->  {nameof(Read_holding_registers_int32)} -> ({m_memory_config_type}) -> {ex.Message}");
            }
            catch (Exception ex)
            {
                m_num_error_comm++;

                Manage_logs.SaveModbusValue($"{GetType().Name} ->  {nameof(Read_holding_registers_int32)} -> ({m_memory_config_type}) -> Errores: {m_num_error_comm} -> {ex.Message}");

                if (m_num_error_comm >= 3)
                {
                    Globals.GetTheInstance().Manage_delegate.Manage_rtu_to_main(RTU_ACTION.DISCONNECT_FROM_READ_WRITE, new List<ushort>(), m_memory_config_type);
                }
            }

            return read_ok;
        }


        public bool Write_multiple_registers(ushort start_address, int[] array_values, MEMORY_CONFIG_TYPE memory_config_type)
        {
            m_memory_config_type = memory_config_type;
            bool write_ok = false;
            try
            {
                string s_data = string.Empty;
                array_values.ToList().ForEach(data => s_data += $"0X{data:X2} ");
                Manage_logs.SaveLogValue($"WRITE MULTIPLE REGISTERS - ({m_memory_config_type}) -> INI: {start_address} / VALUES: {s_data}");

                List<byte> list_data = new()
                {
                    m_unit_id,
                    WRITE_MULTIPLE_REGISTER
                };

                byte[] byte_address = BitConverter.GetBytes(start_address);
                byte_address = byte_address.Reverse().ToArray();
                list_data.AddRange(byte_address);

                byte[] byte_num_reg = BitConverter.GetBytes((ushort)array_values.Length);
                byte_num_reg = byte_num_reg.Reverse().ToArray();
                list_data.AddRange(byte_num_reg);

                byte byte_count = (byte)(array_values.Length * 2);
                list_data.Add(byte_count);

                //Coger la parte baja del valor y convertirlo a ushort
                for (int index = 0; index < array_values.Length; index++)
                {
                    byte[] byte_value = BitConverter.GetBytes(array_values[index]);
                    byte_value = new byte[2] { byte_value[1], byte_value[0] };
                    list_data.AddRange(byte_value.ToList());
                }

                byte[] byte_crc = CRC.CRC16_array(list_data.ToArray(), list_data.Count);
                list_data.AddRange(byte_crc);


                m_port!.DiscardInBuffer();
                m_port!.DiscardOutBuffer();
                m_port!.Write(list_data.ToArray(), 0, list_data.Count);

                b_manage_data = false;
                m_list_manage_data.Clear();

                m_function = WRITE_MULTIPLE_REGISTER;
                m_start_address = start_address;
                m_number_of_register = (ushort)array_values.Length;

                s_data = string.Empty;
                list_data.ForEach(data => s_data += $"0X{data:X2} ");

                Manage_logs.SaveDataValue("---------------------------------------------------------------------------------");
                Manage_logs.SaveDataValue($"SEND ({m_memory_config_type}) -> {s_data}");

                m_num_error_comm = 0;
                write_ok = true;
            }
            catch (IOException ex)
            {
                Globals.GetTheInstance().Manage_delegate.Manage_rtu_to_main(RTU_ACTION.DISCONNECT_FROM_READ_WRITE, new List<ushort>(), m_memory_config_type);
                Manage_logs.SaveModbusValue($"{GetType().Name} ->  {nameof(Write_multiple_registers)} -> ({m_memory_config_type}) -> {ex.Message}");
            }
            catch (Exception ex)
            {
                m_num_error_comm++;

                Manage_logs.SaveModbusValue($"{GetType().Name} ->  {nameof(Write_multiple_registers)} -> ({m_memory_config_type}) -> Errores: {m_num_error_comm} -> {ex.Message}");

                if (m_num_error_comm >= 3)
                {
                    Globals.GetTheInstance().Manage_delegate.Manage_rtu_to_main(RTU_ACTION.DISCONNECT_FROM_READ_WRITE, new List<ushort>(), m_memory_config_type);
                }
            }

            return write_ok;

        }

        #endregion



        #region Serial data received

        private void Serial_datareceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                m_date_last_received = DateTime.Now;

                int bytes_read = m_port!.BytesToRead;
                byte[] array_receive = new byte[bytes_read];
                m_port.Read(array_receive, 0, bytes_read);

                App.Current.Dispatcher.Invoke(new SetTextDeleg(SI_DataReceived), new object[] { array_receive });
            }
            catch (NullReferenceException ex)
            {
                Manage_logs.SaveErrorValue($"{GetType().Name} ->  {nameof(Serial_datareceived)} (NullReferenceException) -> {ex.Message}");
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{GetType().Name} ->  {nameof(Serial_datareceived)} -> {ex.Message}");
            }
        }

        private void SI_DataReceived(byte[] array_data)
        {
            try
            {
                string s_data = string.Empty;
                array_data.ToList().ForEach(data => s_data += $"0X{data:X2} ");
                Manage_logs.SaveDataValue($"RECEIVE ({m_memory_config_type}) -> {s_data}");

                array_data.ToList().ForEach(data =>
                {
                    m_list_manage_data.Add(data);

                    bool calculate_crc = false;
                    List<ushort> data_int_list = new();

                    switch (m_function)
                    {
                        case READ_HOLDING_REGISTER:
                            {
                                // UID + FUNCTION + NUM_OF_REG + DATA + CRC
                                if (m_list_manage_data.Count == 3 + (m_number_of_register * 2) + 2)
                                {
                                    if (
                                    m_list_manage_data[0] == m_unit_id &&
                                    m_list_manage_data[1] == m_function &&
                                    m_list_manage_data[2] == m_number_of_register * 2)
                                    {
                                        #region DATA

                                        List<byte> data_byte_list = m_list_manage_data.GetRange(3, m_number_of_register * 2).ToList();
                                        string s_manage_byte_data = string.Empty;
                                        data_byte_list.ForEach(data => s_manage_byte_data += $"0X{data:X2} ");
                                        Manage_logs.SaveDataValue($" ************* MANAGE DATA - BYTE ARRAY ({m_memory_config_type}) -> {s_manage_byte_data}");

                                        for (int n = 0; n < data_byte_list.Count; n += 2)
                                        {
                                            byte[] data_byte = data_byte_list.ToArray().Skip(n).Take(2).Reverse().ToArray();
                                            ushort value = BitConverter.ToUInt16(data_byte);
                                            data_int_list.Add(value);
                                        }
                                        string s_manage_ushort_data = string.Empty;
                                        data_int_list.ForEach(data => s_manage_ushort_data += $"0X{data:X4} ");
                                        Manage_logs.SaveDataValue($" ************* MANAGE DATA - USHORT ARRAY ({m_memory_config_type}) -> {s_manage_ushort_data}");

                                        #endregion

                                        calculate_crc = true;
                                    }
                                }

                                break;
                            }

                        case WRITE_MULTIPLE_REGISTER:
                            {
                                // UID + FUNCTION + START ADDRESS + NUMBER_OF_REG + CRC
                                if (m_list_manage_data.Count == 8)
                                {
                                    ushort start_address = BitConverter.ToUInt16(new byte[] { m_list_manage_data[3], m_list_manage_data[2] });
                                    ushort number_off_register = BitConverter.ToUInt16(new byte[] { m_list_manage_data[5], m_list_manage_data[4] });

                                    if (
                                    m_list_manage_data[0] == m_unit_id &&
                                    m_list_manage_data[1] == m_function &&
                                    start_address == m_start_address &&
                                    number_off_register == m_number_of_register)
                                    {
                                        calculate_crc = true;
                                    }
                                }

                                break;
                            }
                    }


                    if (calculate_crc)
                    {
                        #region CRC

                        List<byte> crc_receive = m_list_manage_data.GetRange(m_list_manage_data.Count - 2, 2).ToList();
                        string s_crc_data_receive = string.Empty;
                        crc_receive.ForEach(data => s_crc_data_receive += $"0X{data:X2} ");

                        List<byte> crc_calculate_list = m_list_manage_data.GetRange(0, m_list_manage_data.Count - 2).ToList();
                        byte[] crc_calculate = CRC.CRC16_array(crc_calculate_list.ToArray(), crc_calculate_list.Count);
                        string s_crc_data_calculate = string.Empty;
                        crc_calculate.ToList().ForEach(data => s_crc_data_calculate += $"0X{data:X2} ");

                        #endregion

                        Manage_logs.SaveDataValue($" ************* CHECK CRC ({m_memory_config_type}) - RECEIVE -> {s_crc_data_receive} / CALCUTATE -> {s_crc_data_calculate}");
                        if (s_crc_data_receive.Equals(s_crc_data_calculate))
                        {
                            RTU_ACTION rtu_action = m_function == READ_HOLDING_REGISTER ? RTU_ACTION.READ : RTU_ACTION.WRITE;
                            Globals.GetTheInstance().Manage_delegate.Manage_rtu_to_main(rtu_action, data_int_list, m_memory_config_type);
                        }

                        calculate_crc = false;
                    }

                });
            }
            catch (Exception ex)
            {
                m_num_error_comm++;

                Manage_logs.SaveModbusValue($"{GetType().Name} ->  {nameof(SI_DataReceived)} -> ({m_memory_config_type}) -> Errores: {m_num_error_comm} -> {ex.Message}");

                if (m_num_error_comm >= 3)
                {
                    Globals.GetTheInstance().Manage_delegate.Manage_rtu_to_main(RTU_ACTION.DISCONNECT_FROM_READ_WRITE, new List<ushort>(), MEMORY_CONFIG_TYPE.NONE);
                }
            }
        }

        #endregion



        #region Timer comm timeout

        private void Timer_com_timeout_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Subtract(m_date_last_received) > TimeSpan.FromSeconds(3))
            {
                Globals.GetTheInstance().Manage_delegate.Manage_rtu_to_main(RTU_ACTION.DISCONNECT_FROM_READ_WRITE, new List<ushort>(), m_memory_config_type);
                Manage_logs.SaveModbusValue($"{GetType().Name} ->  {nameof(Timer_com_timeout_Tick)} -> Communications timeout");
            }
        }

        #endregion
    }
}
