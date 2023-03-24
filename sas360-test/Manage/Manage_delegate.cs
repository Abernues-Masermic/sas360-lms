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

        public event RTU_handler RTU_handler_event;

        public void Manage_rtu_to_main(RTU_ACTION rtu_action, List<int> list_data)
        {
            RTU_handler_args myArgs = new(rtu_action, list_data);
            RTU_handler_event(this, myArgs);
        }
    }

    public class RTU_handler_args : EventArgs
    {
        public RTU_handler_args(RTU_ACTION rtu_action, List<int> list_data)
        {
            RTU_action = rtu_action;
            List_data = list_data;
        }


        public RTU_ACTION RTU_action { get; set; }

        public List<int> List_data { get; set; }
    }
}
