using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Dtos
{
    public class ConnectionDto: MouseLoginDto
    {
        private string username;

        public string UserName
        {
            get { return username; }
            set { username = value; }
        }

        private bool isConnected;

        public bool IsConnected
        {
            get { return isConnected; }
            set { isConnected = value; }
        }

        private string token;

        public string Token
        {
            get { return token; }
            set { token = value; }
        }

    }
}
