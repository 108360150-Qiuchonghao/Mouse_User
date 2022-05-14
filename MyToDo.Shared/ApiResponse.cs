using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Shared.Contact
{
    public class ApiResponse
    { 
        public string Message { get; set; }

        public bool Status { get; set; }

        public object Result { get; set; }

        //public string MouseId { get; set; }
        //public string Dates { get; set; }   
        //public bool IsConnected { get; set; }
        //public string UserName { get; set; }

        //public object History { get; set; }

    }

    public class ApiResponse<T>
    {
        public string Message { get; set; }

        public bool Status { get; set; }

        public T Result { get; set; }

        //public string MouseId { get; set; }
        //public string Dates { get; set; }
        //public bool IsConnected { get; set; }
        //public string UserName { get; set; }

        //public object History { get; set; }
    }
}
