using Microsoft.Win32;
using MyToDo.Common;
using MyToDo.Service.TestService;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MyToDo.ViewModels
{
    public class MiniwindowViewModel : BindableBase, IDialogAware
    {

        public MiniwindowViewModel()
        {

            
            _serialPort = new SerialPort();
            ExecuteCommand = new DelegateCommand(Execute);
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Start();
           
        }
        public string Title { get; set; }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            BackMainView();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
           // throw new NotImplementedException();
        }

        void BackMainView() {
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }

        public DelegateCommand ExecuteCommand { get; private set; }

        void Execute()
        {
            BackMainView();
        }

        #region 更新實時數據

        //根據ubs設備的PID和VID檢測串口
        private static List<string> ComPortNames(String VID, String PID)
        {
            String pattern = String.Format("^VID_{0}.PID_{1}", VID, PID);
            Regex _rx = new Regex(pattern, RegexOptions.IgnoreCase);
            List<string> comports = new List<string>();

            RegistryKey rk1 = Registry.LocalMachine;
            RegistryKey rk2 = rk1.OpenSubKey("SYSTEM\\CurrentControlSet\\Enum");

            foreach (String s3 in rk2.GetSubKeyNames())
            {
                RegistryKey rk3 = rk2.OpenSubKey(s3);
                foreach (String s in rk3.GetSubKeyNames())
                {
                    if (_rx.Match(s).Success)
                    {
                        RegistryKey rk4 = rk3.OpenSubKey(s);
                        foreach (String s2 in rk4.GetSubKeyNames())
                        {
                            RegistryKey rk5 = rk4.OpenSubKey(s2);
                            string location = (string)rk5.GetValue("LocationInformation");
                            RegistryKey rk6 = rk5.OpenSubKey("Device Parameters");
                            string portName = (string)rk6.GetValue("PortName");
                            if (!String.IsNullOrEmpty(portName) && SerialPort.GetPortNames().Contains(portName))
                                comports.Add((string)rk6.GetValue("PortName"));
                        }
                    }
                }
            }
            return comports;
        }
        //根據API獲取所有滑鼠的PID和VID
        private string GetVIDPID()
        {
            string VidPid = "0483;374B";
            return VidPid;
        }
        //檢測滑鼠是否連接
        private Boolean ListenMouse()
        {

            string[] VidPid = GetVIDPID().Split(';');
            portnames.Clear();
            portnames.AddRange(ComPortNames(VidPid[0], VidPid[1]));
            if (portnames.Count == 0)
                return false;
            else
                return true;
        }
        //更新實時數據
        public class person
        {
            //public int id { get; set; }
            //public string name { get; set; }
            public string age { get; set; }
            public string height { get; set; }
            public string weight { get; set; }
            public string spo2 { get; set; }
            public string hand_temp { get; set; }
            public string hand_humidity { get; set; }
            public string hand_pressure { get; set; }
            public string envir_temp { get; set; }
            public string envir_humidity { get; set; }
            public string envir_pressure { get; set; }
            public string hr { get; set; }
            public string ear_temp { get; set; }

            //public double HR { get; set; }
        }
        List<String> portnames = new List<String>();
        SerialPort _serialPort;
        DispatcherTimer timer = new DispatcherTimer();
        Boolean b_SPO2 = false;
        Boolean b_BME = false;
        Boolean b_BME_EM = false;
        Boolean HR = false;
        Boolean Temp = false;
        Boolean MouseIsCon = false;//判断mouse是否正常连接
        private string[] data;

        public string[] Data
        {
            get { return data; }
            set { data = value; RaisePropertyChanged(); }
        }

        private string spo2;

        public string SPO2Data
        {
            get { return spo2; }
            set { spo2 = value; RaisePropertyChanged(); }
        }

        private string hr;

        public string HRData
        {
            get { return hr; }
            set { hr = value; RaisePropertyChanged(); }
        }
        private string temp;

        public string TempData
        {
            get { return temp; }
            set { temp = value; RaisePropertyChanged(); }
        }


        private string mousestatus;

        public string MouseStatus
        {
            get { return mousestatus; }
            set { mousestatus = value; RaisePropertyChanged(); }
        }
        #endregion 更新實時數據
        //
        private void Timer_Tick(object sender, EventArgs e)
        {
            MouseStatus = AppSession.APPMouseStatus;
            HRData = $"{AppSession.APPHR}bmp";
            SPO2Data = $"{AppSession.APPSPO2}%" ;
            TempData = $"{AppSession.APPTemp}℃" ;
            #region 原來的現在沒用
            //    //step1:监测是否连接到滑鼠
            //    MouseIsCon = ListenMouse();
            //    if (MouseIsCon == false)//step2：如果没有连接到，则，显示未连接，请检查等字样
            //    {
            //        MouseStatus = "滑鼠連接有誤，請檢查！";
            //    }
            //    else//step3：如果连接到，则做。。。。
            //    {
            //        try
            //        {
            //            MouseStatus = "滑鼠正常連接";
            //            b_SPO2 = false;
            //            b_BME = false;
            //            b_BME_EM = false;
            //            HR = false;
            //            Temp = false;
            //            person ps = new person();
            //            _serialPort.PortName = portnames[0];
            //            _serialPort.BaudRate = 115200;
            //            _serialPort.DataBits = 8;
            //            _serialPort.Open();
            //            if (_serialPort.IsOpen)
            //            {
            //                while (b_SPO2 == false || b_BME == false || b_BME_EM == false || HR == false || Temp == false)
            //                {
            //                    try
            //                    {
            //                        //1.先读取资料

            //                        String ppgString = _serialPort.ReadLine();
            //                        String[] ppgArray2 = ppgString.Split(':');

            //                        //2.按照开头字段做判断
            //                        /*
            //                          未來的格式：
            //                          SPO2:xx.xxx
            //                          HR:xx.xxxx
            //                          TEMP:xx.xxxx
            //                          BME:xx.xx,xx.xx,xx.xx
            //                          BME_EN:xx.xx,xx.xx,xx.xx*/
            //                        switch (ppgArray2[0])
            //                        {
            //                            case "SPO2":
            //                                ps.spo2 = ppgArray2[1].Split('.')[0];
            //                                b_SPO2 = true;
            //                                break;
            //                            case "HR":
            //                                ps.hr = ppgArray2[1].Split('.')[0];
            //                                HR = true;
            //                                break;
            //                            case "TEMP":
            //                                ps.hand_temp = ppgArray2[1].Split('.')[0];
            //                                Temp = true;
            //                                break;
            //                            case "BME":
            //                                String[] ppgArray3 = ppgArray2[1].Split(',');
            //                                //ps.hand_temp = ppgArray3[0];
            //                                ps.hand_humidity = ppgArray3[1];
            //                                ps.hand_pressure = ppgArray3[2];
            //                                b_BME = true;
            //                                ps.envir_temp = "1";
            //                                ps.envir_humidity = "2";
            //                                ps.envir_pressure = "3";
            //                                b_BME_EM = true;
            //                                break;
            //                            case "BME_EN":

            //                                break;

            //                        }

            //                    }
            //                    catch
            //                    {

            //                    }
            //                }
            //                if (b_SPO2 == true && b_BME == true && b_BME_EM == true && HR == true && Temp == true)
            //                {
            //                    HRData= $"心率：{ps.hr.ToString()}";
            //                    SPO2Data = $"血氧：{ps.spo2.ToString()}";
            //                    TempData = $"體溫：{ps.hand_temp.ToString()}";

            //                }
            //                _serialPort.Close();

            //            }
            //        }
            //        catch (Exception ex)
            //        {

            //        }
            //        finally
            //        {
            //            //MouseIsCon = ListenMouse();
            //        }

            //    }
            #endregion



        }
        
    }
    }
