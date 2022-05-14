using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Dtos
{
    public class ModifyNameDto: MouseLoginDto
    {
        private string newusername;

        public string NewUserName
        {
            get { return newusername; }
            set { newusername = value; }
        }

    }
}
