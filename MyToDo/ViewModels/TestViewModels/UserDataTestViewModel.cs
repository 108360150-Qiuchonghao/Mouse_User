using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using MyToDo.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using Prism.Mvvm;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Prism.Commands;
using MyToDo.Shared.Dtos;
using MyToDo.Common;
using Prism.Regions;
using Prism.Ioc;
using LiveCharts;
using MyToDo.Common.TestData;
using System.Windows;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Windows.Media;
using System.IO.Ports;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using MyToDo.Extensions;
using LiveCharts.Configurations;
using MyToDo.Service.Python;
using System.Globalization;
using System.Threading;
using MyToDo.Service.PushData;
using MyToDo.Service;

namespace MyToDo.ViewModels.TestViewModels
{
    public class UserDataTestViewModel : NavigationViewModel
    {
        public UserDataTestViewModel(IContainerProvider provider, IModifyNameService modifyNameService, IPullDataService pullDataService) : base(provider)
        {
            UpdateLoading(true);
            
            TaskBars = new ObservableCollection<TaskBar>();
            TimeBars = new ObservableCollection<TimeBar>();
            UserName = AppSession.UserName;
            

            TimeSelectedItem = TimeBars.FirstOrDefault();
            //右侧弹窗的指令
            modifycommand = new DelegateCommand(Modify);
            ModifyTrue = new DelegateCommand(modifytrue);
            Modifycancel = new DelegateCommand(modifycancel);
            Timelistselect = new DelegateCommand<TimeBar>(timelistselect);
            ResetChartsCommand = new DelegateCommand(resetchart);

            //
            this.modifyNameService = modifyNameService;
            this.pullDataService = pullDataService;
            //圖表初始化
            
            InitAllCharts();
           
            ZoomingMode = ZoomingOptions.X;
            UpdateLoading(false);
        }


        private readonly IModifyNameService modifyNameService;
        private readonly IPullDataService pullDataService;

        //重寫導航事件
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            CreateTimeBarsAsync();
        }



        //TimeBar切換功能 5/8日
        private void timelistselect(TimeBar obj)
        {

            if(obj == null)
                    return;
            //要傳給服務器的資料,還要傳給姓名，待修改
            //DateTime dateTime = DateTime.ParseExact(obj.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            //表格記錄，每個表格都需要一個
            //TimeBar切換功能 5/8日
            //1.默認是選項第一個，所以當初始化的時候會向資料庫要一次資料（只是默認第一個日期的資料）
            //2.每次切換日期的時候，會向資料庫要一次資料，日期是選項的日期.
            if (SeriesCollection1!=null)
            {
                SeriesCollection1.Clear();
            }
            if (SeriesCollection != null)
            {
                SeriesCollection.Clear();
            }
            if (SeriesCollection3 != null)
            {
                SeriesCollection3.Clear();
            }
            GetHistory(obj.Date);
        }

        //初始化所有表格

        ChartValues<DateTimePoint> HRvalues ;
        ChartValues<DateTimePoint> SPO2values;
        ChartValues<DateTimePoint> Tempvalues;
        private void InitAllCharts()
        {
            HRvalues = new ChartValues<DateTimePoint>();
            SPO2values = new ChartValues<DateTimePoint>();
            Tempvalues = new ChartValues<DateTimePoint>();
            XFormatter1 = val => new DateTime((long)val).ToString("t");
            XFormatter = val => new DateTime((long)val).ToString("t");
            XFormatter3 = val => new DateTime((long)val).ToString("t");
            //YFormatter = val => val.ToString("C");
        }
        private async void GetHistory(string InputDate)
        {
            
                UpdateLoading(true);
                //初始化算法变量
                if (HRvalues.Count > 0)
                    HRvalues.Clear();
                if (SPO2values.Count > 0)
                    SPO2values.Clear();
                if (Tempvalues.Count > 0)
                    Tempvalues.Clear();
                string DeviceID = Properties.Settings.Default["DeviceID"].ToString();
                var PullDatasResult = await pullDataService.PullHistoryDatas(new PullDatesDto()
                {
                    MouseId = DeviceID,
                    Date = InputDate,
                });
                if (PullDatasResult.Status == true)
                {
                    string[] History = PullDatasResult.Result.History.Split(";");
                    int step = 10;
                    for (int i = 1; i < History.Count()-1; i += step)
                    {
                        string[] hs = History[i].Split(",");
                        
                            Tempvalues.Add(new DateTimePoint(Convert.ToDateTime(hs[4]), double.Parse(hs[1])));
                            HRvalues.Add(new DateTimePoint(Convert.ToDateTime(hs[4]), double.Parse(hs[2])));
                            SPO2values.Add(new DateTimePoint(Convert.ToDateTime(hs[4]), double.Parse(hs[3])));
                        
                    }
                
                Tempvalues = modifyvalues(Tempvalues);
                HRvalues = modifyvalues(HRvalues);
                SPO2values = modifyvalues(SPO2values);
                //生成数据表
                SeriesCollection1 = new SeriesCollection
            {
                new LineSeries
                {
                    Title ="HR",
                    Values =  HRvalues,
                    //Fill = gradientBrush,
                    StrokeThickness = 1,
                    ToolTip = null,
                    LineSmoothness = 0,
                    PointGeometrySize = 0
                }
            };
                SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title ="SPO2",
                    Values = SPO2values,
                    //Fill = gradientBrush,
                    StrokeThickness = 1,
                    ToolTip = null,
                    LineSmoothness = 0,
                    PointGeometrySize = 0
                }
            };
                SeriesCollection3 = new SeriesCollection
            {
                new LineSeries
                {
                    Title ="Temp",
                    Values = Tempvalues,
                    //Fill = gradientBrush,
                    StrokeThickness = 1,
                    ToolTip = null,
                    LineSmoothness = 0,
                    PointGeometrySize = 0
                }
            };
                }
            
            
                UpdateLoading(false);
            
           
        }
        private ChartValues<DateTimePoint> modifyvalues(ChartValues<DateTimePoint> values)
        {
            DateTime start;
            DateTime stop;
            TimeSpan ts;
            var origionValues = new ChartValues<DateTimePoint>();
            var Values = new ChartValues<DateTimePoint>();
            origionValues = values;
            start = origionValues[0].DateTime;
            stop = origionValues[0].DateTime;
            origionValues.Add(origionValues[0]);
            int k = 1;
            while (k < origionValues.Count - 1)
            {

                DateTime tempdatetime = origionValues[k].DateTime;
                stop = tempdatetime;
                ts = stop - start;
                if (ts.TotalMinutes > 2)
                {
                    for (int i = 0; i < ts.TotalMinutes - 1; i+=20)
                    {
                        Values.Add(new DateTimePoint(start.AddMinutes(1), 0));
                        start = start.AddMinutes(1);
                    }
                }
                else
                {
                    start = origionValues[k].DateTime;
                    Values.Add(new DateTimePoint(values[k].DateTime, origionValues[k].Value));
                    k++;
                }
            }
            return Values;
        }
        //ZoomingMode
        private ZoomingOptions _zoomingMode;
        public ZoomingOptions ZoomingMode
        {
            get { return _zoomingMode; }
            set
            {
                _zoomingMode = value;
               RaisePropertyChanged();
            }
        }
        private double maxvalue;

        public double MAXValue
        {
            get { return maxvalue; }
            set { maxvalue = value;  RaisePropertyChanged(); }
        }
        private double minvalue;

        public double MINValue
        {
            get { return minvalue; }
            set { minvalue = value; RaisePropertyChanged(); }
        }


        private void resetchart()
        {
            string Date = TimeBars[TimeBarIndex].Date;
            //Use the axis MinValue/MaxValue properties to specify the values to display.
            //use double.Nan to clear it.
            if (SeriesCollection1 != null)
            {
                SeriesCollection1.Clear();
            }
            if (SeriesCollection != null)
            {
                SeriesCollection.Clear();
            }
            if (SeriesCollection3 != null)
            {
                SeriesCollection3.Clear();
            }
            GetHistory(Date);
        }

        private void ToogleZoomingMode(object sender, RoutedEventArgs e)
        {
            switch (ZoomingMode)
            {
                case ZoomingOptions.None:
                    ZoomingMode = ZoomingOptions.X;
                    break;
                case ZoomingOptions.X:
                    ZoomingMode = ZoomingOptions.Y;
                    break;
                case ZoomingOptions.Y:
                    ZoomingMode = ZoomingOptions.Xy;
                    break;
                case ZoomingOptions.Xy:
                    ZoomingMode = ZoomingOptions.None;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private int nameindex;

        /// <summary>
        /// 下拉列表选中状态值
        /// </summary>
        public int NameIndex
        {
            get { return nameindex; }
            set { nameindex = value; RaisePropertyChanged(); }
        }



        #region SelectedItem
        private MouseUserDto nameselectedItem;

        /// <summary>
        /// 
        /// </summary>
        public MouseUserDto NameSelectedItem
        {
            get { return nameselectedItem; }
            set { nameselectedItem = value; RaisePropertyChanged(); }
        }

        private TimeBar timeselectedItem;

        /// <summary>
        /// 
        /// </summary>
        public TimeBar TimeSelectedItem
        {
            get { return timeselectedItem; }
            set { timeselectedItem = value; RaisePropertyChanged(); }
        }

        #endregion
        //寻找对应人的时间


        private int timebarindex;

        /// <summary>
        /// 下拉列表选中状态值
        /// </summary>
        public int TimeBarIndex
        {
            get { return timebarindex; }
            set { timebarindex = value; RaisePropertyChanged(); }
        }

        //按下修改按钮后的右侧弹窗是否开启参数
        private bool isRightDrawerOpen;

        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }

        #region 修改用户信息按钮的command
        private void Modify() 
        {
            IsRightDrawerOpen = true;
        }
        private void modifytrue()
        {
            ModifyName();
            IsRightDrawerOpen = false;
        }

        async void ModifyName()
        {
            string DeviceID = Properties.Settings.Default["DeviceID"].ToString();
            if (string.IsNullOrWhiteSpace(NewUserName) !=true)
            {
                var loginResult = await modifyNameService.Modify(new ModifyNameDto()
                {
                    MouseId = DeviceID,
                    NewUserName = NewUserName
                });
                if(loginResult.Status==true)
                {
                    AppSession.UserName = NewUserName;
                    UserName = NewUserName;
                    NewUserName = "";
                }
            }
        }

        private void modifycancel()
        {
            IsRightDrawerOpen = false;
        }

        //修改用户信息按钮的command start
        public DelegateCommand modifycommand { get;  private set;}
        public DelegateCommand ModifyTrue { get; private set; }
        public DelegateCommand Modifycancel { get; private set; }
        //修改用户信息按钮的command end
        #endregion

       
        public DelegateCommand NameListselect{ get; private set; }
        public DelegateCommand<TimeBar> Timelistselect { get; private set; }
        public DelegateCommand ResetChartsCommand { get; private set; }


        public List<string> ConfirmDate(List<History> histories) 
        {
            DateTime tempDate = histories[0].time;
            List<string> backDate = new List<string>();
            foreach (History history in histories)
            {
                backDate.Add(history.time.ToString("yyyy年MM月dd日"));
            }
            return backDate;
        }

        //自定义去重函数
        public class History
        {
            public DateTime time { set; get; }
            public int temp { set; get; }
            public int HR { set; get; }
            public int SPO2 { set; get; }
            public int u_mouse_id { set; get; }
        }

        private ObservableCollection<TimeBar> timeBars;

        public ObservableCollection<TimeBar> TimeBars
        {
            get { return timeBars; }
            set { timeBars = value; RaisePropertyChanged(); }
        }

        //新建一个List，存放所有的日期，按照mouseid或者用户名寻找。


        //要和combobox绑定的class
        private ObservableCollection<TaskBar> taskBars;

        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            set { taskBars = value; RaisePropertyChanged(); }
        }


        private string username;

        public string  UserName
        {
            get { return username; }
            set { username = value; RaisePropertyChanged(); }
        }

        private string newusername;

        public string NewUserName
        {
            get { return newusername; }
            set { newusername = value; RaisePropertyChanged(); }
        }

        //創建Timebar條
        async void CreateTimeBarsAsync()
        {
            if(TimeBars.Count>0)
            {
                TimeBars.Clear();
            }
            //待修改
            if(AppSession.IsConnected == true)
            {
                string[] HistoryStr;
                string DeviceID = Properties.Settings.Default["DeviceID"].ToString();
                var loginResult = await pullDataService.GetAllDates(new PullDatesDto()
                {
                    MouseId = DeviceID
                });
                int i = 0;
                if (loginResult.Status==true)
                {
                    if (loginResult.Result.Dates != "")
                    {
                        HistoryStr = loginResult.Result.Dates.Split(',');
                        foreach (string dateTime in HistoryStr)
                        {
                            TimeBars.Add(new TimeBar() { Date = dateTime.ToString(), Time = "" });
                        }
                    }

                    else
                    {
                        TimeBars.Add(new TimeBar() { Date = "無資料", Time = "" });
                    }

                }
                else if (loginResult.Status==false)
                {
                    TimeBars.Add(new TimeBar() { Date = "連接失败", Time = "" });
                }
            }
            else
            {
                TimeBars.Add(new TimeBar() { Date = "未連接", Time = "" });
            }
            TimeSelectedItem = TimeBars.FirstOrDefault();
        }



        //从APi接口得到所有关于每个人的Date

        #region livecharts圖表



        //第一個圖表
        private SeriesCollection seriesCollection1;

        public SeriesCollection SeriesCollection1
        {
            get { return seriesCollection1; }
            set { seriesCollection1 = value; RaisePropertyChanged(); }
        }

        public Func<double, string> XFormatter1 { get; set; }
        public Func<double, string> YFormatter1 { get; set; }
        public Func<double, string> Formatter1 { get; set; }

        //第二個圖表
        private SeriesCollection seriesCollection;

        public SeriesCollection SeriesCollection
        {
            get { return seriesCollection; }
            set { seriesCollection = value; RaisePropertyChanged(); }
        }
        
        public Func<double, string> XFormatter { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public Func<double, string> Formatter { get; set; }
        //第三個圖表
        private SeriesCollection seriesCollection3;

        public SeriesCollection SeriesCollection3
        {
            get { return seriesCollection3; }
            set { seriesCollection3 = value; RaisePropertyChanged(); }
        }
        public Func<double, string> XFormatter3 { get; set; }
        public Func<double, string> YFormatter3 { get; set; }
        public Func<double, string> Formatter3 { get; set; }
        private ChartValues<DateTimePoint> GetData()
        {
            var r = new Random();
            var trend = 100;
            var values = new ChartValues<DateTimePoint>();
            var Values = new ChartValues<DateTimePoint>();
            DateTime start;
            DateTime stop ;
            TimeSpan ts;
            for (var i = 0; i < 3; i++)
            {
                var seed = r.NextDouble();
                if (seed > .8) trend += seed > .9 ? 50 : -50;
                values.Add(new DateTimePoint(DateTime.Now.AddMinutes(i), trend + r.Next(0, 10)));
            } 
            for (var i = 30; i < 92; i++)
            {
                var seed = r.NextDouble();
                if (seed > .8) trend += seed > .9 ? 50 : -50;
                values.Add(new DateTimePoint(DateTime.Now.AddMinutes(i), trend + r.Next(0, 10)));
            }
            for (var i = 120; i < 200; i++)
            {
                var seed = r.NextDouble();
                if (seed > .8) trend += seed > .9 ? 50 : -50;
                values.Add(new DateTimePoint(DateTime.Now.AddMinutes(i), trend + r.Next(0, 10)));
            }
            start = values[0].DateTime;
            stop = values[0].DateTime;
            values.Add(values[0]); 
            int k = 1;
            while(k < values.Count-1)
            {
                
                DateTime tempdatetime = values[k].DateTime;
                stop = tempdatetime;
                ts = stop - start;
                if(ts.TotalMinutes > 2)
                {
                    for(int i=0;i<ts.TotalMinutes-1;i++)
                    { 
                        Values.Add(new DateTimePoint(start.AddMinutes(1), 0));
                       start = start.AddMinutes(1);
                    }
                }
                else
                {
                    start = values[k].DateTime;
                    Values.Add(new DateTimePoint(values[k].DateTime, values[k].Value));
                    k++;
                }

            }
            return Values;
        }

        //private ChartValues<DateTimePoint> GetData2()
        //{
        //    var r = new Random();
        //    var trend = 200;
        //    var values = new ChartValues<DateTimePoint>();
        //    var Values = new ChartValues<DateTimePoint>();
        //    DateTime start;
        //    DateTime stop;
        //    TimeSpan ts;

        //    for (var i = 0; i < 3; i++)
        //    {
        //        var seed = r.NextDouble();
        //        if (seed > .8) trend += seed > .9 ? 50 : -50;
        //        values.Add(new DateTimePoint(DateTime.Now.AddMinutes(i), trend + r.Next(0, 10)));
        //    }
        //    for (var i = 30; i < 92; i++)
        //    {
        //        var seed = r.NextDouble();
        //        if (seed > .8) trend += seed > .9 ? 50 : -50;
        //        values.Add(new DateTimePoint(DateTime.Now.AddMinutes(i), trend + r.Next(0, 10)));
        //    }
        //    for (var i = 120; i < 200; i++)
        //    {
        //        var seed = r.NextDouble();
        //        if (seed > .8) trend += seed > .9 ? 50 : -50;
        //        values.Add(new DateTimePoint(DateTime.Now.AddMinutes(i), trend + r.Next(0, 10)));
        //    }
        //    start = values[0].DateTime;
        //    stop = values[0].DateTime;
        //    values.Add(values[0]);
        //    int k = 1;
        //    while (k < values.Count - 1)
        //    {

        //        DateTime tempdatetime = values[k].DateTime;
        //        stop = tempdatetime;
        //        ts = stop - start;
        //        if (ts.TotalMinutes > 2)
        //        {
        //            for (int i = 0; i < ts.TotalMinutes - 1; i++)
        //            {
        //                Values.Add(new DateTimePoint(start.AddMinutes(1), 0));
        //                start = start.AddMinutes(1);
        //            }
        //        }
        //        else
        //        {
        //            start = values[k].DateTime;
        //            Values.Add(new DateTimePoint(values[k].DateTime, values[k].Value));
        //            k++;
        //        }

        //    }
        //    return Values;
        //}

        //private  ChartValues<DateTimePoint> GetDataOnline(string Date)
        //{
        //    string DATE = Date;
        //    var values = new ChartValues<DateTimePoint>();
        //    var Values = new ChartValues<DateTimePoint>();
        //    DateTime start;
        //    DateTime stop;
        //    TimeSpan ts;
        //    start = values[0].DateTime;
        //    stop = values[0].DateTime;
        //    values.Add(values[0]);
        //    int k = 1;
        //    while (k < values.Count - 1)
        //    {

        //        DateTime tempdatetime = values[k].DateTime;
        //        stop = tempdatetime;
        //        ts = stop - start;
        //        if (ts.TotalMinutes > 2)
        //        {
        //            for (int i = 0; i < ts.TotalMinutes - 1; i++)
        //            {
        //                Values.Add(new DateTimePoint(start.AddMinutes(1), 0));
        //                start = start.AddMinutes(1);
        //            }
        //        }
        //        else
        //        {
        //            start = values[k].DateTime;
        //            Values.Add(new DateTimePoint(values[k].DateTime, values[k].Value));
        //            k++;
        //        }

        //    }
        //    return Values;
        //}
        #endregion


        //1.线条显示数值: DataLabels="True"
        //2.线条是否弯曲: LineSmoothness="0" 或 "1" 
        //3.线条的颜色: Stroke="Red"  //设置线条的颜色为红色
        //4.线条下方颜色: Fill="Pink" //线条的下方颜色
        //5.线条的每个点: PointGeometrySize="20" //设置数据点大小
        //6.显示数据字体颜色: Foreground="Red" 
        //7.数据点的颜色: PointForeground="#FF6347"
        //8.线条虚线: StrokeDashArray="5" //数值愈大间隔愈大, 如下绿色虚线

        private void CreateFakechartData() 
        {
            SeriesCollection1 = new SeriesCollection
            {
                new LineSeries
                {
                    Title ="HR",
                    Values = GetData(),
                    //Fill = gradientBrush,
                    StrokeThickness = 1,
                    ToolTip = null,
                    PointGeometrySize = 0
                }
            };
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title ="SPO2",
                    Values = GetData(),
                    //Fill = gradientBrush,
                    StrokeThickness = 1,
                    ToolTip = null,
                    PointGeometrySize = 0
                }
            };
            SeriesCollection3 = new SeriesCollection
            {
                new LineSeries
                {
                    Title ="Temp",
                    Values = GetData(),
                    //Fill = gradientBrush,
                    StrokeThickness = 1,
                    ToolTip = null,
                    PointGeometrySize = 0
                }
            };
        }
    }




}
