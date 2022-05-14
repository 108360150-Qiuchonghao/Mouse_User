using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Dtos
{
    public class MouseUserDto :BaseDto
    {
        private string mouse_id;

        public string Mouse_ID
        {
            get { return mouse_id; }
            set { mouse_id = value; }
        }

        private string User_id;

        public string User_ID
        {
            get { return User_id; }
            set { User_id = value; }
        }

        private string a_account;

        public string A_Account
        {
            get { return a_account; } 
            set { a_account = value; }
        }

        private bool status;

        public bool Status
        {
            get { return status; }
            set { status = value; }
        }

        private int spo2;

        public int SPO2
        {
            get { return spo2; }
            set { spo2 = value; }
        }

        private int hr;

        public int HR
        {
            get { return hr; }
            set { hr = value; }
        }

        private int temperature;

        public int Temperature
        {
            get { return temperature; }
            set { temperature = value; }
        }

        private List<HistoryDto> histories;

        public List<HistoryDto> Histories
        {
            get { return histories; }
            set { histories = value; }
        }





    }
}
