   M S B A M L   `   `   `  ����A  =MyToDo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:MyToDo.Views.DialogMyToDo.Views.Dialog  5      ,    &MyToDo.Views.Dialog.ChangeDeviceIDView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:MyToDo.Views.Dialog     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  5      [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T	 PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/  	 6B   # 	 Prism.Services.Dialogs.Dialog    WindowStyle  5      �� 5   
    �� 
TargetType" �6   .�5      ��  �� Property � �� �� Value$	 600��6&   �� 5       � ��$	 350��6'   �� 5       �@�SizeToContent$ WidthAndHeight=�6.   �� 5       �@�
ResizeMode$ NoResize=�6+   �� 5       �@�WindowStyle$
 None=�6,   5      .��5      � $��0,0,0,38q�6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      .+�5   
   ]� $��0��6   .+�5      �� $��20,10,20,10q�5      $
��Left=�5       $��20(�5!      $
��Bold"�5"      $��修改設備ID��5#      �� 5$      -   5         ��Name$ btnclose��5%      $��30��5&      $��Right=�5'      $	��Top=�5(      $	��✕��5*       � Cursor$
 Hand��5+      $��Right=�5,      $��20(�5-         MaterialDesignToolButton#��[  5.      ��5)      �� *CancelCommand+5.      5/      � 50   
   $��1��51      $��20q�52      $��Center=�53      .+�54      �� .+�55      �� $
��0,10q�56       �� 
IsReadOnly$
 True��57      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 舊ID��58      
�������$ IBeam��5:      $	��Top=�5;      $��17(�5<      ��5=      �� *	Message+�� 5>      $
��0,10q�5?      $ 請輸入新PID和VID��5@      
�������$ IBeam��5B      $	��Top=�5C      $��17(�5D      ��5E      �� *NewDeviceID+5F      5G      �� 5I   
   $��2��5J      $��	20,0,20,0q�5K      $��Center=�5L      $��Bottom=�5M      	 �� Orientation$	 
Horizontal=�5N      .+�5O      �� $��2��5P      $	��100��5Q      $��10q�5R      $��Center=�5S      $��Bottom=�5T      $��確定��5V      $
 Hand��5W      ��5U      �� *ConfirmCommand+5W      �� 5X      $��2��5Y      $	��100��5Z      $��10q�5[      $��Center=�5\      $��Bottom=�5]      $��取消��5_      $
 Hand��5`       " MaterialDesignPaperLightButton#��[ 5a      ��5^      �� *CancelCommand+5a      5b      �� 5c   
   ���#DDDDDD$��2q�6(   5d      5f      