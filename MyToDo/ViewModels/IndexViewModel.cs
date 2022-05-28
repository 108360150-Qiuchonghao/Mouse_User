using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Microsoft.Win32;
using MyToDo.Common;
using MyToDo.Common.Models;
using MyToDo.Extensions;
using MyToDo.Shared.Dtos;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Threading;
using MyToDo.Properties;
using static System.Net.Mime.MediaTypeNames;
using MyToDo.Service.PushData;
using MyToDo.Service.GetData;  
using Prism.Events;
using CsvHelper;
using System.IO;
using CsvHelper.Configuration;
using System.Globalization;
using System.Diagnostics;
using System.Threading;

namespace MyToDo.ViewModels
{
    public class IndexViewModel : NavigationViewModel
    {
        
        private readonly IRegionManager regionManager;
        public IndexViewModel(IContainerProvider provider, IDialogHostService dialog, IPushDataService pushDataService, IConnectService connectService,IEventAggregator aggregator) : base(provider)
        {
            UpdateLoading(true);
            _serialPort = new SerialPort();
            TaskBars = new ObservableCollection<TaskBar>();
            MouseUsers = new ObservableCollection<MouseUserDto>();
            LastHourSeries = new SeriesCollection
            {
                new LineSeries
                {
                    AreaLimit = -10,
                    Values = new ChartValues<ObservableValue>
                    {
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                                new ObservableValue(0),
                    }
                }
            };
            InitTextBar();
            this.regionManager = provider.Resolve<IRegionManager>();
            this.pushDataService = pushDataService;
            this.connectService = connectService;
            this.dialog = dialog;
            this.aggregator = aggregator;
            NavigateCommand = new DelegateCommand<TaskBar>(Navigate);
            ShowAddCommand = new DelegateCommand(ReConnCommand);
            InitIcon();
            CreateTaskBars();

            //新線程相關
            Thread thread = new Thread(CroseTime);
            thread.IsBackground = true;
            thread.Start();
            //timer.Tick += new EventHandler(Timer_Tick);
            //timer.Interval = TimeSpan.FromMilliseconds(4000);
            //timer.Start();
            UpdateLoading(false);
        }
        
         private void CroseTime()
         {
            while (true)
            {
                Timer_Tick();
                Thread.Sleep(2000);
            }
        }

        private void startTick()
        {
            //timer.Tick += new EventHandler(Timer_Tick);
            //timer.Interval = TimeSpan.FromMilliseconds(4000);
            //timer.Start();
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            Title = $"您好，{AppSession.UserName}";
        }

        public DelegateCommand<TaskBar> NavigateCommand { get; private set; }
        public DelegateCommand ShowAddCommand { get; private set; }

        private readonly IDialogHostService dialog;

        public ObservableCollection<TaskBar> taskBars;
        public ObservableCollection<TaskBar> TaskBars
        {
            get{ return taskBars; }
            set { taskBars = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<MouseUserDto> mouseusers;
        public ObservableCollection<MouseUserDto> MouseUsers
        {
            get { return mouseusers; }
            set { mouseusers = value; RaisePropertyChanged(); }
        }
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(); }
        }

        private string date;

        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        private string whichfactor;

        public string  WhichFactor
        {
            get { return whichfactor; }
            set { whichfactor = value; RaisePropertyChanged(); }
        }
   


        //如果系統未連接，則嘗試重連，并獲取新的token
        //如果系統連接，提示已經連接
        async void Connect() 
        {
            string DeviceID = Properties.Settings.Default["DeviceID"].ToString();
            var loginResult = await connectService.Connect(new ConnectionDto()
            {
                MouseId = DeviceID
            });
            if(loginResult.Status==true)
            {
                AppSession.IsConnected = true;
                AppSession.UserName = loginResult.Result.UserName;
                Title = $"您好，{AppSession.UserName}";
            }
            else
            {
                AppSession.IsConnected=false;
                AppSession.UserName = "現在是離線模式";
                Title = $"您好，{AppSession.UserName}";
            }
            InitIcon();
        }

        void ReConnCommand()
        {
            if(AppSession.IsConnected==true)
            {
               // aggregator.SendMessage("已經連接");
            }
            else if(AppSession.IsConnected==false)
            {
                Connect();
            }
        }
        private void Navigate(TaskBar obj)
        {
            NavigationParameters param = new NavigationParameters();
            // ToName.Name = obj.Title;
            string unit = obj.Title;
            switch(unit)
            {
                case "心率:":
                    _trend = AppSession.APPHR;
                    WhichFactor = "bpm";
                    Introducation = "心率（Heart rate）是指心臟收縮（contractions）跳動的頻率（beats）和每分鐘跳動的次數（bpm），正常人平靜時（安靜心率）每分鐘60到100次（60~100bpm(次/分鐘)），運動時心跳會加速，心肺功能較好的運動員會比正常人的心跳要慢。";
                break;
                case "體溫:":
                    _trend = AppSession.APPTemp;
                    WhichFactor = "℃";
                    Introducation = "體溫指生物的身體溫度。在正常情況下，人類體溫一般為37℃或者98.6℉。經口腔測量的體溫一般為36.8±0.7℃（98.2±1.3℉）。亦即攝氏36.1度至37.5度，或者華氏97.9度至99.5度。體溫反應了機體新陳代謝的結果，也是機體發揮各項正常功能的必備條件之一。";
                    break;
                case "血氧:":
                    _trend = AppSession.APPSPO2;
                    WhichFactor = "%";
                    Introducation = "血氧飽和度(Oxygen saturation)，或稱血氧濃度，是指血中氧飽和血紅蛋白相對於總血紅蛋白（不飽和+飽和）的比例。人體需要並調節血液中氧氣的非常精確和特定的平衡。人體的正常動脈血氧飽和度為95-100％，如果該水平低於90％，則被認為是低氧血症。";
                    break;
            }
            //LastHourSeries[0].Values.Clear();
            CreateChart();
            if (MouseUsers.Count != 0)
            {
                int index = 0;
                foreach (MouseUserDto mouseUserDto in MouseUsers)
                {
                    if (mouseUserDto.User_ID == ToName.Name)
                        ToName.index = index;
                    else
                        index++;
                }
            }
           
        }
        private string introducation;

        public string Introducation
        {
            get { return introducation; }
            set { introducation = value; RaisePropertyChanged(); }
        }


        private void CreateTaskBars() 
        {
            TaskBars.Add(new TaskBar() { Icon = "CardsHeart", Title = "心率:", Content = "--", Color = "#FFD6D6D6", Target = "bpm" });
            TaskBars.Add(new TaskBar() { Icon = "LiquidSpot", Title = "血氧:", Content = "--", Color = "#FFD6D6D6", Target = "%" });
            TaskBars.Add(new TaskBar() { Icon = "Thermometer", Title = "體溫:", Content = "--", Color = "#FFD6D6D6", Target = "℃" });
        }

        #region 两个Icon的类
        //两个Icon
        private string icon2Type;

        public string Icon2Type
        {
            get { return icon2Type; }
            set { icon2Type = value; RaisePropertyChanged(); }
        }

        private string icon1Color;

        public string Icon1Color
        {
            get { return icon1Color; }
            set { icon1Color = value; RaisePropertyChanged(); }
        }

        private string connectionStatus;

        public string ConnectionStatus
        {
            get { return connectionStatus; }
            set { connectionStatus = value; RaisePropertyChanged(); }
        }

        private void InitIcon()
        {
            if (AppSession.IsConnected == true)
            {
                Icon2Type = "Wifi";
                ConnectionStatus = "網路連接成功";
            }
            else if (AppSession.IsConnected == false)
            {
                Icon2Type = "WifiOff";
                ConnectionStatus = "網路連接失敗，請嘗試重新連接";
            }
        }

        #endregion


        private void InitTextBar() 
        {

            Title = $"您好，{AppSession.UserName}";
            Date = $"{DateTime.Now.ToString("yyyy-MM-dd")}";
            WhichFactor = "bpm";
            Introducation = "心率（Heart rate）是指心臟收縮（contractions）跳動的頻率（beats）和每分鐘跳動的次數（bpm），正常人平靜時（安靜心率）每分鐘60到100次（60~100bpm(次/分鐘)），運動時心跳會加速，心肺功能較好的運動員會比正常人的心跳要慢。";

        }

        public SeriesCollection LastHourSeries { get; set; }
        

        private string trend;

        public string _trend
        {
            get { return trend; }
            set { trend = value; RaisePropertyChanged(); }
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
            string VidPid = Settings.Default["DeviceID"].ToString();
            return VidPid;
        }
        //檢測滑鼠是否連接
        private Boolean ListenMouse()
        {

            string[] VidPid = GetVIDPID().Split(';');
            portnames.Clear();
            try
            {
                portnames.AddRange(ComPortNames(VidPid[0], VidPid[1]));
                if (portnames.Count == 0)
                    return false;
                else
                    return true;
            }
            catch(Exception ex)
            {
                return false;
            }
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

        private string mousestatus;

        public string MouseStatus
        {
            get { return mousestatus; }
            set { mousestatus = value; RaisePropertyChanged(); }
        }

        //push歷史記錄相關.5/9

        //存入csv档案的class,For CsvHelper
        public class PPG
        {
            public long channel1 { get; set; }
            public long channel2 { get; set; }
            public long channel3 { get; set; }
        }

        //
        private void Timer_Tick( )
        {
                MouseIsCon = ListenMouse();

                if (MouseIsCon == false)//step2：如果没有连接到，则，显示未连接，请检查等字样
                {
                    MouseStatus = "滑鼠連接有誤，請檢查！";
                    AppSession.APPMouseStatus = MouseStatus;
                    Icon1Color = "Red";
                    TaskBars[0].Content = "--";
                    TaskBars[1].Content = "--";
                    TaskBars[2].Content = "--";
                    AppSession.APPHR = TaskBars[0].Content;
                    AppSession.APPTemp = TaskBars[1].Content;
                    AppSession.APPSPO2 = TaskBars[2].Content;
                }
                else//step3：如果连接到，则做。。。。
                {
                    try
                    {
                        MouseStatus = "滑鼠正常連接";
                        AppSession.APPMouseStatus = MouseStatus;
                        Icon1Color = "Black";
                        b_SPO2 = false;
                        b_BME = false;
                        b_BME_EM = false;
                        HR = false;
                        Temp = false;
                        person ps = new person();
                        _serialPort.PortName = portnames[0];
                        _serialPort.BaudRate = 115200;
                        _serialPort.DataBits = 8;
                        _serialPort.Open();
                        if (_serialPort.IsOpen)
                        {
                            while (b_SPO2 == false || HR == false || Temp == false)
                            {

                                #region 原本直接從serialport讀生理參數
                                //try
                                //{
                                //    //1.先读取资料
                                //    String ppgString = _serialPort.ReadLine();
                                //    String[] ppgArray2 = ppgString.Split(':');
                                //    //2.按照开头字段做判断
                                //    /*
                                //      未來的格式：
                                //      SPO2:xx.xxx
                                //      HR:xx.xxxx
                                //      TEMP:xx.xxxx
                                //      BME:xx.xx,xx.xx,xx.xx
                                //      BME_EN:xx.xx,xx.xx,xx.xx */

                                //    switch (ppgArray2[0])
                                //    {
                                //        case "SPO2":
                                //            ps.spo2 = ppgArray2[1].Split('.')[0];
                                //            b_SPO2 = true;
                                //            break;
                                //        case "HR":
                                //            ps.hr = ppgArray2[1].Split('.')[0];
                                //            HR = true;
                                //            break;
                                //        case "TEMP":
                                //            ps.hand_temp = ppgArray2[1].Split('.')[0];
                                //            Temp = true;
                                //            break;
                                //        case "BME":
                                //            String[] ppgArray3 = ppgArray2[1].Split(',');
                                //            //ps.hand_temp = ppgArray3[0];
                                //            ps.hand_humidity = ppgArray3[1];
                                //            ps.hand_pressure = ppgArray3[2];
                                //            b_BME = true;
                                //            ps.envir_temp = "1";
                                //            ps.envir_humidity = "2";
                                //            ps.envir_pressure = "3";
                                //            b_BME_EM = true;
                                //            break;
                                //        case "BME_EN":
                                //            break;
                                //    }
                                //}
                                //catch
                                //{ }
                                #endregion

                                #region 適配真正的滑鼠，新的讀取方法,第一步：先去算出SPO2和HR
                                try
                                {
                                    var records = new List<PPG>
                                    {

                                    };
                                    for(int i = 0; i< 3200;i++)
                                    {
                                        String IDString = _serialPort.ReadLine();
                                        string[] ppgstring = IDString.Split(':');
                                        if(ppgstring[0] == "ID")
                                        {
                                            string[] Channels = ppgstring[1].Split(',');
                                            string id = Channels[0];
                                            PPG ppg = new PPG();
                                            ppg.channel1 = long.Parse(Channels[1]);
                                            ppg.channel2 = long.Parse(Channels[2]);
                                            ppg.channel3 = long.Parse(Channels[3]);
                                            if (ppg.channel1 > 500000 || ppg.channel2 > 500000 || ppg.channel3 > 500000 || ppg.channel1 < 50000 || ppg.channel2 < 50000 || ppg.channel3 < 50000)
                                            {
                                                continue;
                                            }
                                            records.Add(ppg);
                                        }
                                        else if(ppgstring[0] == "TEMP")
                                        {
           
                                            double Hand_temp = (double.Parse(ppgstring[1]));
                                            if (Hand_temp > 10)
                                            {
                                                Hand_temp = 0.0086 * (Hand_temp - 28.3) + 36.58;
                                                ps.hand_temp = (Hand_temp).ToString("0.0");
                                            }
                                            else
                                                ps.hand_temp = "--";

                                                Temp = true;
                                        }
                                        else if (ppgstring[0] == "HR")
                                        {
                                            //if ((float.Parse(ppgstring[1]) / 10) < 30)
                                            //    ps.hr = "--";
                                            //else
                                            //    ps.hr = (float.Parse(ppgstring[1]) / 10).ToString();
                                            //HR = true;
                                        }
                                        else if (ppgstring[0] == "SPO2")
                                        {
                                            //if ((float.Parse(ppgstring[1]) / 10) > 30)
                                            //{
                                            //    ps.spo2 = (float.Parse(ppgstring[1]) / 10).ToString();
                                            //}
                                            //else
                                            //    ps.spo2 = "--";
                                    
                                            //    b_SPO2 = true;
                                        }


                                }
                                    using (StreamWriter writer = new StreamWriter(".\\Data\\WriteCsv.csv"))
                                    {
                                        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                                        {
                                            csv.WriteRecords(records);
                                        }
                                    }
                                #region Python
                                var psi = new ProcessStartInfo();
                                psi.FileName = @"D:\Develop\Python\venv\Scripts\python.exe";
                                var script = ".\\Data\\main1.py";
                                psi.Arguments = $"\"{script}\"";
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
                                string[] str = results.Split(";");
                                if (str[1] != "")
                                {
                                    ps.spo2 = str[1];
                                    b_SPO2 = true;
                                    ps.hr = str[2];
                                    HR = true;
                                    
                                    //假資料
                                    //double Hand_temp = double.Parse(ps.hand_temp);
                                    //Hand_temp = 0.0086 * (Hand_temp - 28.3) + 36.58;

                                    //ps.spo2 = "97.8";
                                    //ps.hr = "78";
                                    //ps.hand_temp = Hand_temp.ToString();

                                }
                                else
                                    break;

                                #endregion
                            }
                                catch(Exception ex)
                                { 
                                    Console.WriteLine(ex.Message);
                                }
                                #endregion

                                #region 假的板子讀取資料
                                //try
                                //{
                                //    var records = new List<PPG>
                                //    {

                                //    };

                                //    for (int i = 0; i < 800; i++)
                                //    {
                                //        PPG ppg = new PPG();
                                //        string IDString = _serialPort.ReadLine();
                                //        string[] Channels = IDString.Split(',');
                                //        ppg.channel1 = long.Parse(Channels[0]);
                                //        ppg.channel2 = long.Parse(Channels[1]);
                                //        ppg.channel3 = long.Parse(Channels[2]);
                                //        if (ppg.channel1 > 500000 || ppg.channel2 > 500000 || ppg.channel3 > 500000 || ppg.channel1 < 100000 || ppg.channel2 < 100000 || ppg.channel3 < 100000)
                                //        {
                                //            continue;
                                //        }
                                //        records.Add(ppg);
                                //    }
                                //    using (StreamWriter writer = new StreamWriter(".\\Data\\WriteCsv.csv"))
                                //    {
                                //        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                                //        {
                                //            csv.WriteRecords(records);
                                //        }
                                //    }
                                //    #region Python
                                //    var psi = new ProcessStartInfo();
                                //    psi.FileName = @"C:\Users\Administrator\AppData\Local\Programs\Python\Python39\python.exe";
                                //    var script = ".\\Data\\main.py";
                                //    psi.Arguments = $"\"{script}\"";
                                //    psi.UseShellExecute = false;
                                //    psi.CreateNoWindow = true;
                                //    psi.RedirectStandardOutput = true;
                                //    psi.RedirectStandardError = true;
                                //    var error = "";
                                //    var results = "";
                                //    using (var process = Process.Start(psi))
                                //    {
                                //        error = process.StandardError.ReadToEnd();
                                //        results = process.StandardOutput.ReadToEnd();
                                //    }
                                //    string[] str = results.Split(";");
                                //    string Str = results;
                                //    if (str[0] != "")
                                //    {
                                //        ps.spo2 = Str.Split(';')[2];
                                //        b_SPO2 = true;
                                //        ps.hr = Str.Split(';')[1];
                                //        HR = true;
                                //        Temp = true;
                                //        ps.hand_temp = "37";
                                //    }
                                //    else
                                //        break;

                                //    #endregion
                                //}
                                //catch
                                //{ }
                                #endregion

                            }
                            if (b_SPO2 == true && HR == true && Temp == true)
                            {
                                TaskBars[0].Content = ps.hr.ToString();
                                TaskBars[1].Content = ps.spo2.ToString();
                                TaskBars[2].Content = ps.hand_temp.ToString();
                                AppSession.APPHR = TaskBars[0].Content;
                                AppSession.APPSPO2 = TaskBars[1].Content;
                                AppSession.APPTemp = TaskBars[2].Content;
                                //切換圖表顯示哪一個參數
                                switch (WhichFactor)
                                {
                                    case "bpm":
                                        //_trend = double.Parse(TaskBars[0].Content);
                                        _trend = TaskBars[0].Content;
                                        break;
                                    case "%":
                                        //_trend = double.Parse(TaskBars[1].Content);
                                        _trend = TaskBars[1].Content;
                                        break;
                                    case "℃":
                                        //_trend = double.Parse(TaskBars[2].Content);
                                        _trend = TaskBars[2].Content;
                                        break;
                                }

                            //圖表添加數據
                            if (_trend != "--")
                            {
                                LastHourSeries[0].Values.Add(new ObservableValue(double.Parse(_trend)));
                                LastHourSeries[0].Values.RemoveAt(0);
                            }
                            //如果連接中，push資料
                                if (AppSession.IsConnected == true)
                            { 
                                if(ps.hr!="--" && ps.spo2 != "--"&& ps.hand_temp!="--")
                                    PushData(ps.hr, ps.spo2, ps.hand_temp);
                            }
                        }
                            _serialPort.Close();
                        }
                        else
                        {

                        }
                    }
                    catch (Exception ex)
                    {
                         Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        if (_serialPort.IsOpen)
                            _serialPort.Close();
                    }

                }
            // make query here
            //step1:监测是否连接到滑鼠
        }
        #endregion 更新實時數據

        #region pushdata
        private readonly IPushDataService pushDataService;
        private readonly IConnectService connectService ;
        private new readonly IEventAggregator aggregator;
        private async void PushData(string Hr,string Spo2,string Temp)
        {
            
                string DeviceID = Properties.Settings.Default["DeviceID"].ToString();
                PushDataDto pushDataDto = new PushDataDto()
                {
                    time = DateTime.Now.ToString("yyyy-MM-dd H:m:s"),
                    temp = float.Parse(Temp),
                    HR = float.Parse(Hr),
                    SPO2 = float.Parse(Spo2),
                    u_mouse_id = DeviceID
                };
                
                var loginResult = await pushDataService.AddHistory(pushDataDto);
                if(loginResult.Status == true)
                {
                    AppSession.IsConnected = true;
                    Icon2Type = "Wifi";
                    ConnectionStatus = "網路連接成功";
                    Title = $"您好，{AppSession.UserName}";
                }
                else if (loginResult.Status == false)
                {
                    if(loginResult.Message == "PushError")
                    {
                       
                    }
                    else
                    {
                        AppSession.IsConnected = false;
                        Icon2Type = "WifiOff";
                        ConnectionStatus = "網路連接失敗，請嘗試重新連接";
                        AppSession.UserName = "現在是離線模式";
                        Title = $"您好，{AppSession.UserName}";
                }
            }
            
            
        }
        
        
        public void TryConnect()
        {

        }
        #endregion
        #region 表格
        private void CreateChart() 
        {
            //LastHourSeries[0].Values.Clear();
            for (int i = 0; i < LastHourSeries[0].Values.Count; i++)
            {
                LastHourSeries[0].Values.RemoveAt(0);
                LastHourSeries[0].Values.Add(new ObservableValue(0));
            }
        }
        #endregion 表格




    }
}
