using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sas360_test
{
    public class Manage_delegate
    {
        public delegate void RTU_handler(object myObject, RTU_handler_args myArgs);

        public event RTU_handler? RTU_handler_event;

        public void Manage_rtu_to_main(RTU_ACTION rtu_action, List<ushort> list_data, MEMORY_CONFIG_TYPE memory_config_type)
        {
            RTU_handler_args myArgs = new(rtu_action, list_data, memory_config_type);
            RTU_handler_event!(this, myArgs);
        }
    }

    public class RTU_handler_args : EventArgs
    {
        public RTU_handler_args(RTU_ACTION rtu_action, List<ushort> list_data, MEMORY_CONFIG_TYPE memory_config_type)
        {
            RTU_action = rtu_action;
            List_data = list_data;
            Memory_config_type = memory_config_type;
        }


        public RTU_ACTION RTU_action { get; set; }

        public List<ushort> List_data { get; set; }
        public MEMORY_CONFIG_TYPE Memory_config_type { get; set; }
    }
}
