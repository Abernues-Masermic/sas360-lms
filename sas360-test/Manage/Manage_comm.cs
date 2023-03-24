using EasyModbus;
using System;
using System.Collections.Generic;

namespace sas360_test
{
    internal class Manage_comm
    {
        private ModbusClient? m_modbus_client;

        #region Connect

        public bool Connect(string com_port, int baud_rate, byte unit_id)
        {
            bool is_connected = false;
            try
            {
                m_modbus_client = new ModbusClient(com_port)
                {
                    UnitIdentifier = unit_id,
                    Baudrate = baud_rate,
                    ConnectionTimeout = 2000,
                    Parity = System.IO.Ports.Parity.None,
                    StopBits = System.IO.Ports.StopBits.One
                };

                //m_modbus_client.Connect();

                is_connected = true;
                Globals.GetTheInstance().Manage_delegate.Manage_rtu_to_main(RTU_ACTION.CONNECT, new List<int>());
            }
            catch(Exception ex)
            {
                Manage_logs.SaveErrorValue($"{GetType().Name} ->  {nameof(Connect)} -> {ex.Message}");
                Globals.GetTheInstance().Manage_delegate.Manage_rtu_to_main(RTU_ACTION.ERROR_CONNECT, new List<int>());
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
                //m_modbus_client.Disconnect();
                disconnect_ok = true;
                Globals.GetTheInstance().Manage_delegate.Manage_rtu_to_main(RTU_ACTION.DISCONNECT, new List<int>());
            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{GetType().Name} ->  {nameof(Disconnect)} -> {ex.Message}");
            }

            return disconnect_ok;
        }

        #endregion

        #region Read holding registers i32

        public Tuple<bool, int[]> Read_holding_registers_int32(int start_address, int number_off_registers)
        {
            bool read_ok = false;
            int[] received_data = null;

            try
            {
                received_data = m_modbus_client!.ReadHoldingRegisters(start_address, number_off_registers);

            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{GetType().Name} ->  {nameof(Read_holding_registers_int32)} -> {ex.Message}");
            }

            Tuple<bool, int[]> tuple_read = new(read_ok, received_data!);

            return tuple_read;
        }


        #endregion

        #region Write single registers i32

        public bool Write_single_register(int start_address, int value)
        {
            bool write_ok = false;
            try
            {
                m_modbus_client!.WriteSingleRegister(start_address, value);
                write_ok = true;

            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{GetType().Name} ->  {nameof(Write_single_register)} -> {ex.Message}");
            }

            return write_ok;
        }

        #endregion


        #region Write multiple registers i32

        public bool Write_multiple_registers(int start_address, int[] values)
        {
            bool write_ok = false;
            try
            {
                m_modbus_client!.WriteMultipleRegisters(start_address, values);
                write_ok = true;

            }
            catch (Exception ex)
            {
                Manage_logs.SaveErrorValue($"{GetType().Name} ->  {nameof(Write_multiple_registers)} -> {ex.Message}");
            }

            return write_ok;
        }

        #endregion
    }
}
