using System;
using System.Threading;


namespace sas360_test
{
    internal class Manage_comm_thread
    {
        private ThreadStart? m_thread_start;
        private Thread? m_thread;


        public byte Unit_id { get; set; }
        public string Comm_port { get; set; }
        public int Baud_rate { get; set; }

        public bool  Is_connected { get; set; }

        private Manage_comm m_manage_comm;


        public Manage_comm_thread() {
            m_manage_comm = new Manage_comm(); ;
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
            Is_connected = m_manage_comm.Connect(Comm_port, Baud_rate, Unit_id);
            if (Is_connected)
                Manage_logs.SaveLogValue($"CONNECT RTU-> {Comm_port} / {Baud_rate} / {Unit_id}");
        }

        public void Disconnect()
        {
            m_manage_comm.Disconnect();
            Is_connected = false;
            Manage_logs.SaveLogValue($"DISCONNECT RTU -> {Comm_port}");
        }


        public Tuple<bool, int[]> Read_holding_registers_int32(int start_address, int number_off_registers)
        {
            return m_manage_comm.Read_holding_registers_int32(start_address, number_off_registers);
        }

        public bool Write_single_register(int start_address, int value)
        {
            return m_manage_comm.Write_single_register(start_address, value);
        }

        public bool Write_multiple_registers(int start_address, int[] values)
        {
            return m_manage_comm.Write_multiple_registers(start_address, values);
        }
    }
}
