using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service.Python
{
    public class PythonService
    {
        public PythonService()
        {

        }
        public string Getvaluefrompy()
        {
            var psi = new ProcessStartInfo();
            psi.FileName = @"D:\Pycharm_file\Scripts\python.exe";

            var script = @"D:\pythonProject\Hello.py";
            var x = 5;
            var y = 1;
            psi.Arguments = $"\"{script}\" \"{x}\" \"{y}\"";

            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            var error = "";
            var results = "";

            using (var process = Process.Start(psi))
            {
                error = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }
            return results;
        }
    }
}
