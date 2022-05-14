using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Dtos
{
    public class PullAllDataDto:MouseLoginDto
    {
        private string history;

        public string History
        {
            get { return history; }
            set { history = value; }
        }

        private string date;

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

    }
}
