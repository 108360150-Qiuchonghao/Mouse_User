   M S B A M L   `   `   `  ����A  =MyToDo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:MyToDo.ViewsMyToDo.Views  5      U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5      5clr-namespace:MyToDo.ViewModelsMyToDo.ViewModels  5          MyToDo.Views.IndexView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:MyToDo.Views     5      Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5      _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5	      [	 WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T
 PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/ 	 
 5
      2
viewmodelsclr-namespace:MyToDo.ViewModels 	 
 5      ��5      �� 5   
   .��5      �   � RowDefinitions  5   
   �� 5      $
��auto �6   �� 5      .+�5   
   � .+�5      �� $��0��5      $��0��5      $
��auto��5      $��10,10q�5      $��25(�5      $
��bold"�5       ��5!      �� *Title+�� 5"      $��0��5#      $��1��5$      $	��100��5%      $��35��5&      $��14q�5'      $��Right=�5(      $��Center=�5)      $��重新連接��5+       � Cursor$
 Hand��5,       #  MaterialDesignRaisedLightButton#��[  5-      ��5*      �� *ShowAddCommand+5-      5/      �� 50   
   ���#DDDDDD$��0,0,0,2q�6'   � 51   
   $��1��6    � ColumnDefinitions 52      �� 53      $��4* �6#   �� 54      $��6* �6#   .+�57      � $��0��6     58      �� 59      $
��auto �6$   �� 5:      $
��auto �6$   �� 5;      .+�5=      � $��0��6   .+�5>      ��  �� Orientation$ 
Horizontal=�6!   .+�5?      '  !MaterialDesignThemes.Wpf.PackIcon  $��15,10,10,10q�5@      $
��Left=�5A      $��Center=�5B         Kind$ 
Connection=�5D      ��5C      �� *
Icon1Color+5D      �� 5E      $
��Left=�5F      $��Center=�5G      $��15(�5H      ��5I      �� *MouseStatus+5J      5L      � 5M      $��1��6   .+�5N      �� $��1��5O      $��2��5P      $��5,0,5,5q�5Q      ��5R      �� *
TaskBars+��5S      �� 5T      .R�5U   "   A�  A� Columns$ 1��6.   5V      ��5X      �� 5Y      .R�5Z   "   �� $��10q�5[   %    �� CornerRadius$ 4��5]   %   ��5\   %   �� *Color+ J� InputBindings 5^   &   d� 5_   *    d� MouseAction$ 	LeftClicke�5b   -   	 �� Command	 5`   -   �� *DataContext.NavigateCommand+
 �� RelativeSource
 ��  �� Mode$ FindAncestor=� �� AncestorType ItemsControl �� CommandParameter 5a   -   �� 5b   -   ��5d   &   �� 5e   *    �� 
TargetType" ��60    �� Triggers 5f   .   P� 5g   2    P� Property �@J�IsMouseOver P� Value$
 True��6Q   . �5h   6   ��  �� Property �@J�Effect �� Value 5i   :   3  -System.Windows.Media.Effects.DropShadowEffect  5j   >      
BlurRadius$ 10Q�5k   A      ShadowDepth$ 1Q�5l   A      Color$ #DDDDDD��5m   A   5o   7   5p   3   .�5r   .   ��  �@�Cursor$
 Hand��6G   5s   +   ._�5u   &   �   5v   *   �� 5w   .   $
��auto �6<   �� 5x   .   .+�5z   *   � $��0��6/   .+�5{   .   �� $ 
Horizontal=�69   .+�5|   2     $��0��5}   5   $��20q�5~   5   $
��Left=�5   5   $��Center=�5�   5    5�   5   �� *Icon+5�   /   5�   +   � 5�   *   $��1��6/    5�   .   �� 5�   2   $
��auto �6C   �� 5�   2   .+�5�   .   �� $��0��69   $ 
Horizontal=�6I   .+�5�   2   �� $��10q�5�   5   $��37(�5�   5   ��5�   5   �� *Title+5�   /   �� 5�   .   $��1��69   $ 
Horizontal=�6I   .+�5�   2   �� $��10,12,10,10q�5�   5   $��37(�5�   5   $��Heavy"�5�   5   ��5�   5   �� *	Content+�� 5�   2   $��10,12,10,10q�5�   5   $��37(�5�   5   $��Heavy"�5�   5   ��5�   5   �� *Target+5�   /   5�   +   5�   '   5�   #   5�      5�      5�      5�      �� 5�      ���#DDDDDD$��0,0,1.5,1.5q�6+   � 5�      $��1��6     5�      �� 5�      $
��auto �6$   �� 5�      .+�5�      �� $ 
Horizontal=�6   .+�5�        $��20,10,10,10q�5�      $
��Left=�5�      $��Center=�5�       5�      �� *	Icon2Type+�� 5�      $
��Left=�5�      $��Center=�5�      $��15(�5�      ��5�      �� *ConnectionStatus+5�      � 5�      $��1��5�      $��1��5�      $��
15,0,15,15q�5�       J� Effect 5�        5�      $ 15Q�5�         	Direction$	 -90Q�5�         Opacity$ .2Q�5�         RenderingBias$ Quality=�5�      $ 1Q�5�       J� OpacityMask 5�      !� 5�       !� Visual 6&   ��  �� ElementName$ Border1�� � 	Resources 5�        LiveCharts.Wpf.LineSeries  LiveCharts.Wpf.Series  LiveCharts.Wpf.Axis��%�  '        '      �� 5�      "  6    .�5�      ��  �@ StrokeThickness$ 3Q�6@   �� 5�       �@ Stroke
 �����67   �� 5�       �@ Fill �	#4EFFFFFF65   �� 5�       �@ PointGeometrySize$ 0Q�6B   �� 5�       �@ LineSmoothness$ 0Q�6?   5�      �� 5�      "  6    .�5�      ��  �@ 
ShowLabels$ False��6;   �� 5�       � {�$ False��6:   5�      5�        5�      �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��.50* �6(   �� 5�      $	��.5* �6(   .+�5�      �� -   5         ��Name$ Border1��5�      $��0��5�      $��4��5�      
�������$ 5��5�      �� 5�      $��0��5�      $��3��5�      ���#3333335�      �� 5�      $��0��5�        �� Padding$  	10,10,0,5q�5�      $��18(�5�      
�������! �� TextAlignment$! Center=�5�      The Data Chart5�      �� 5�      $��1��5�      $  0,0,0,20q�5�      ���	#59FFFFFF$! Center=�5�      ��5�      �� *Date+5�      #  LiveCharts.Wpf.CartesianChart  5�      $��2��5�      $��0,0,0,0q�5�      &   LiveCharts.Wpf.Charts.Base.Chart"   	Hoverable$" False��5�      #   DataTooltip# 5�      Z� $   Series$ 5�      �� *LastHourSeries+%   AxisX% 5�        &   MinValue$& 2Q�6'   5�      �� 5�      $��3��5�      $
��25,0q�5�      $��Center=�5�      .+�5�      �� $��13(�6$   $��10q�62   $
��Wrap=�6>   ��6R   �� *Introducation+�� 5      $ 
Horizontal=�6%   .+�5     �� $��40(�5     ���#303030��5     �� *_trend+' �� StringFormat$' {0:N1}���� 5     $	��8,6q�5     $��Bottom=�5     $��18(�5     ���#303030��5
     �� *WhichFactor+5     5     5     5     5     5     5     