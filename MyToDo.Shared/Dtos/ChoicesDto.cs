using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Dtos
{
    public class ChoicesDto : BaseDto
    {

        private bool choice;

        public bool Choice
        {
            get { return choice; }
            set { choice = value; }
        }

    }
}
