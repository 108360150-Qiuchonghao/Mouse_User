using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Dtos
{
    public class PushDataDto : PullDatesDto
    {
        private string Time;

        public string time
        {
            get { return Time; }
            set { Time = value; }
        }

        private string mouse_id;

        public string u_mouse_id
        {
            get { return mouse_id; }
            set { mouse_id = value; }
        }


        private float Temp;
        public float temp
        {
            get { return Temp; }
            set { Temp = value; }
        }

        private float hr;
        public float HR
        {
            get { return hr; }
            set { hr = value; }
        }

        private float spo2;
        public float SPO2
        {
            get { return spo2; }
            set { spo2 = value; }
        }

        private bool status;

        public bool Status
        {
            get { return status; }
            set { status = value; }
        }


    }
}
