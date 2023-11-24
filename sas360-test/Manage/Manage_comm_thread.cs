using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace sas360_test
{
    internal class Manage_comm_thread
    {
        private ThreadStart? m_thread_start;
        private Thread? m_thread;


        public byte Unit_id { get; set; } = new byte();
        public string Comm_port { get; set; } = string.Empty;
        public int Baud_rate { get; set; } = new int();

        public bool  Is_connected { get; set; } = new bool();

        private Manage_comm m_manage_comm = new();


        public Manage_comm_thread() {
            m_manage_comm = new Manage_comm();
        }


        public void Connect()
        {
            m_thread_start = new ThreadStart(Connect_thread);
            m_thread = new Thread(m_thread_start);
            m_thread.SetApartmentState(ApartmentState.STA);
            m_thread.Start();
        }

        private void Connect_thread()
        {
            bool is_connected = m_manage_comm.Connect(Comm_port, Baud_rate, Unit_id);
            if (is_connected)
            {
                Globals.GetTheInstance().Manage_delegate.Manage_rtu_to_main(RTU_ACTION.CONNECT, new List<ushort>(), MEMORY_CONFIG_TYPE.NONE);
                Manage_logs.SaveLogValue($"CONNECT RTU -> {Comm_port} / {Baud_rate} / {Unit_id}");
            }
        }

        public void Disconnect()
        {
            m_manage_comm.Disconnect();
            Is_connected = false;
            Manage_logs.SaveLogValue($"DISCONNECT RTU -> {Comm_port}");
        }


        public bool Read_holding_registers_int32(ushort start_address, ushort number_off_registers, MEMORY_CONFIG_TYPE memory_config_type)
        {
            return m_manage_comm.Read_holding_registers_int32(start_address, number_off_registers, memory_config_type);
        }



        public bool Write_multiple_registers(ushort start_address, int[] array_values, MEMORY_CONFIG_TYPE memory_config_type)
        {
            return m_manage_comm.Write_multiple_registers(start_address, array_values, memory_config_type);
        }


    }
}
