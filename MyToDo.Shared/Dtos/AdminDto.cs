using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Dtos
{
    public class AdminDto : BaseDto
    {
        private string Account;

        public string account
        {
            get { return Account; }
            set { Account = value; }
        }

        private string Password;

        public string password
        {
            get { return Password; }
            set { Password = value; }
        }

    }
}
