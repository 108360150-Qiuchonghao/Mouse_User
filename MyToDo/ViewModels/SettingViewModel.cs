using MyToDo.Common.Models;
using MyToDo.Extensions;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
     public class SettingViewModel : NavigationViewModel
    {
        public SettingViewModel(IRegionManager regionManager,IContainerProvider provider) : base(provider)
        {
            MenuBars = new ObservableCollection<MenuBar>();
            CreateMenuBar();
            NavigateCommmand = new DelegateCommand<MenuBar>(Navigate);
            this.regionManager = regionManager;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            regionManager.Regions[PrismManager.SettingsViewRegionName].RequestNavigate("SysSettingView");

        }
        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.NameSpace))
                return;

            regionManager.Regions[PrismManager.SettingsViewRegionName].RequestNavigate(obj.NameSpace);
        }

        //驅動整個導航功能
        public DelegateCommand<MenuBar> NavigateCommmand { get; private set; }
        private readonly IRegionManager regionManager;
       

        public DelegateCommand GoBackCommand { get; private set; }
        public DelegateCommand GoForwardCommand { get; private set; }

        private ObservableCollection<MenuBar> menuBars;

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }



        void CreateMenuBar()
        {
            //MenuBars.Add(new MenuBar() { Icon = "HeartCog", Title = "個性化", NameSpace = "" });
            MenuBars.Add(new MenuBar() { Icon = "Cog", Title = "系統設置", NameSpace = "SysSettingView" });
            MenuBars.Add(new MenuBar() { Icon = "Information", Title = "關於我們", NameSpace = "AboutUsView" });
        }

        public void Configure()
        {
            CreateMenuBar();
            regionManager.Regions[PrismManager.SettingsViewRegionName].RequestNavigate("SysSettingView");
        }
    }
}
