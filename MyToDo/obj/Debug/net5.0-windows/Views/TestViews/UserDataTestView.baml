   M S B A M L   `   `   `  ����A  =MyToDo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:MyToDo.Views.TestViewsMyToDo.Views.TestViews  5      U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5	      G4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5
      S.clr-namespace:MyToDo.ViewModels.TestViewModels MyToDo.ViewModels.TestViewModels  5      -    'MyToDo.Views.TestViews.UserDataTestView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Iex:http://schemas.abtsoftware.co.uk/scichart/exampleExternals     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      0local$clr-namespace:MyToDo.Views.TestViews  5      =lv4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf  5	      >lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf  5
      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      _	 [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes 	 5      \
 XSciChart.Charting, Version=6.3.0.13476, Culture=neutral, PublicKeyToken=b55dd9efe817e823X TSciChart.Core, Version=6.3.0.13476, Culture=neutral, PublicKeyToken=b55dd9efe817e823X TSciChart.Data, Version=6.3.0.13476, Culture=neutral, PublicKeyToken=b55dd9efe817e823[ WSciChart.Drawing, Version=6.3.0.13476, Culture=neutral, PublicKeyToken=b55dd9efe817e8237s)http://schemas.abtsoftware.co.uk/scichart 
    5      E
viewmodels.clr-namespace:MyToDo.ViewModels.TestViewModels 
    5      .��5      ) 	 #MaterialDesignThemes.Wpf.DialogHost  .��5   
   ) 	 #MaterialDesignThemes.Wpf.DrawerHost      IsRightDrawerOpen  6   �� *IsRightDrawerOpen+   RightDrawerContent 5      � 5       � RowDefinitions 5      �� 5      $
��auto �6(   �� 5      .+�5      �� $��0��5      $��10q�5      $��20(�5      $
��Bold"�5      $��修改用戶信息��5       �� 5!      $��1��5"      $��10q�5#      $	��Top=�5$       �� Orientation$ Vertical=�5%      .+�5&      �� $��10q�5'      $��20(�5(      ��5)      �� *
UserName+�� 5*      $��10q�5+      ) 	 #MaterialDesignThemes.Wpf.HintAssist   Hint$ 請輸入新名稱��5,       � Cursor$ IBeam��5-      $��20(�5.      ��5/      �� *NewUserName+�� 50      $
��0,20q�6%   $ 
Horizontal=�63   .+�51      �� $��3q�52   !   $��確認��54   !   $
 Hand��55   !   $��13(�56   !    "  MaterialDesignPaperLightButton#��[  57   !   ��53   !   �� *
ModifyTrue+57   !   �� 58      $��3q�59   !   $��取消��5;   !   $
 Hand��5<   !   $��13(�5=   !   #��[  5>   !   ��5:   !   �� *Modifycancel+5>   !   5?      5@      5A      .��5C      �  5D      �� 5E      $
��auto �6$   �� 5F      $
��auto �6$   �� 5G      $
��auto �6$   �� 5H      .+�5J      �� $��0��5K      $��20,0,0,0q�5L      $ 
Horizontal=�5M      .+�5N      �� .+�5O      �� $��10q�5P      $��Center=�5Q      $��Center=�5R      $��28(�5S      ��5T      �� *
UserName+5U      �� 5V      $��10q�5W      $��28(�5X      $��日期:��5Y      �� 5Z      -   5         ��Name$ timeBar��5[      $��10q�5\      $��Center=�5]      $��Center=�5^      $
 Hand��5_      $��20(�5`        MyComboBoxStyle#��[ 5a      ��5b      �� *
TimeBars+ �� SelectedIndex 5c      �� *TimeBarIndex+ �� SelectedItem 5d      �� *TimeSelectedItem+*  $Microsoft.Xaml.Behaviors.Interaction	   Triggers	 5e      +  %Microsoft.Xaml.Behaviors.EventTrigger  5f      
   	EventName$
 SelectionChanged��6-   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5g   "   2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 68   �� *Timelistselect+   CommandParameter 6[   ��  �� ElementName$ timeBar�� �� Path$ SelectedItem�5h      ��5j      �� 5k      .R�5l   "   �� $��5q�6,   ��67   �� *Date+5m      5o      �� 5q      -   5        $��0��5r      $��	20,0,15,0q�5s      $��Right=�5t      $��Center=�5u      $��Reset��5x      $
 Hand��5y      #��[  5z      ��5w      �� 5z      5}      �� 5~      $��0��5      $��0,0,15,0q�5�      $��Right=�5�      $��修改用戶信息��5�      $
 Hand��5�      #��[  5�      ��5�      �� *modifycommand+5�      �� 5�      $��2��5�      $��0,2,0,0q�5�      ���#DDDDDD$��0,0,0,2q�5�      �� 5�      $��3��6   .��5�      �  5�      �� 5�      $
��auto �6,   �� 5�      $
��auto �6,   �� 5�      $
��auto �6,   �� 5�      .+�5�      #  LiveCharts.Wpf.CartesianChart  -   5          Name$ HrChart��5�      $��0��5�      $	��250��5�      $��20q�5�      
�������&	   LiveCharts.Wpf.Charts.Base.Chart 	  Series 5�      �� *SeriesCollection1+ 	  Zoom 5�      �� *ZoomingMode+ 	  AxisX 5�   "   
  LiveCharts.Wpf.Axis
  -   5         
 Name$ X1��5�   %    
  Title$
 Time��5�   %      LiveCharts.Wpf.DefaultAxes   CleanSeparator 
  	Separator# Z 5�   %    
  LabelFormatter 5�   %   �� *XFormatter1+5�   %    	  AxisY 5�   "   
  -   5        $ Y1��5�   %   $ 
Heart Rate��5�   %    5�   %   �� *YFormatter1+ 	  DataTooltip 5�      #  LiveCharts.Wpf.DefaultTooltip  5�   "      
BulletSize$ 20Q�65      	IsWrapped$ false��6E   5�      �� 5�      $��0��5�      $��20q�5�      $��Center=�5�      $��30(�5�      $��Black"�5�      $��
Heart Rate��5�        5�      -   5        $ 	SPO2Chart��5�      $��1��5�      $	��250��5�      $��20q�5�      
������� 5�      �� *SeriesCollection+ 5�      �� *ZoomingMode+ 5�   "   
  -   5        $ X��5�   %   $
 Time��5�   %   # Z 5�   %    5�   %   �� *
XFormatter+5�   %    5�   "   
  -   5        $ Y��5�   %   $
 SPO2��5�   %    5�   %   �� *
YFormatter+5�      �� 5�      $��1��5�      $��20q�5�      $��Center=�5�      $��30(�5�      $��Black"�5�      $
��SPO2��5�        5�      -	   5        $ 	TempChart��5�      $��2��5�      $	��250��5�      $��20q�5�      
������� 5�      �� *SeriesCollection3+ 5�      �� *ZoomingMode+ 5�   "   
  -
   5        $ X3��5�   %   $
 Time��5�   %   # Z 5�   %    5�   %   �� *
XFormatter+5�   %    5�   "   
  -   5        $ Y3��5�   %   $ Temperature��5�   %    5�   %   �� *
YFormatter+ 5�        5�   "   $ 20Q�65   $ false��6E   5�      �� 5�      $��2��5�      $��20q�5�      $��Center=�5�      $��30(�5�      $��Black"�5�      $��Temperature��5�      5     5     5     5     5     5     