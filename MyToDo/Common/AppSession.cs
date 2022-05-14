using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common
{
    public static class AppSession
    {
        public static string UserName { get; set; }

        public static bool IsConnected { get; set; }

        public static string Token { get; set; }

        public static string APPHR { get; set; }
        public static string APPTemp { get; set; }
        public static string APPSPO2 { get; set; }
        public static string APPMouseStatus { get; set; }

    }
}
