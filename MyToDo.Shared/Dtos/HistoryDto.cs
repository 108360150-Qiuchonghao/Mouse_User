using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Dtos
{
    public class HistoryDto : BaseDto
    {
        //private DateTime time;
        //public DateTime Time
        //{
        //    get { return time; }
        //    set { time = value; }
        //}

        private float temp;
        public float Temp
        {
            get { return temp; }
            set { temp = value; }
        }

        private int hr;
        public int HR
        {
            get { return hr; }
            set { hr = value; }
        }

        private int spo2;
        public int SPO2
        {
            get { return spo2; }
            set { spo2 = value; }
        }
    }
}
