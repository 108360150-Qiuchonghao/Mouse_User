   M S B A M L   `   `   `  ����A  =MyToDo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:MyToDo.ViewModels.DialogMyToDo.ViewModels.Dialog  5      9!clr-namespace:MyToDo.Views.DialogMyToDo.Views.Dialog  5      5clr-namespace:MyToDo.ViewModelsMyToDo.ViewModels  5      (    "MyToDo.Views.Dialog.MiniwindowView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9dialog&clr-namespace:MyToDo.ViewModels.Dialog     5      3local!clr-namespace:MyToDo.Views.Dialog     5      _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5	      [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T	 PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/  	 5
      2
viewmodelsclr-namespace:MyToDo.ViewModels  	 5      # 	 Prism.Services.Dialogs.Dialog    WindowStyle  5      �� 5   
    �� 
TargetType" �6   .�5      ��  �� Property � �� �� Value$	 350��6&   �� 5       � ��$	 400��6'   �� 5       �@�SizeToContent$ WidthAndHeight=�6.   �� 5       �@�
ResizeMode$ NoResize=�6+   �� 5       �@ WindowStartupLocation$ CenterScreen=�6C   �� 5       �@�WindowStyle$
 None=�6,   �� 5       �@�AllowsTransparency$
 True��63   �� 5       � ��
 ���� 6+   �� 5       �@�Topmost$
 True��6(   5      ��5      �� 5   
    �� Opacity$	 0.5Q�6    �� Color$ White��6(   .��5"      � -   5         �Name$ MoveGrid��5"      $��0,0,0,38q�6    � RowDefinitions 5$   
   �� 5%      $
��auto �6   �� 5&      $
��auto �6   .+�5)   
   ]� -   5         ]�Name$ TitleBar��5)      $��0��6&   .+�5+      �� -   5        $��TitleBorder��5,      $	��120��5-      $
��auto��5.      	 J� InputBindings	 5/      d� 50      
 d� Gesture$
 	LeftClicka�53       �� Command 51      �� *DragWindowCommand+ �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType UserControl �� CommandParameter 52      ��  �� $ FindAncestor=�
 Window53      .+�55      �� $��10q�56      $��Right=�57      $	��Top=�58      $��18(�59      $
��Bold"�5:      $��實時生理參數��5;      5<      �� 5=      -   5         ��Name$ backbtn��5>      $��30��5?      $��5q�5@      $��Right=�5A      $	��Top=�5B       � Cursor$
 Hand��5D         MaterialDesignToolButton#��[  5E      ��5C      �� *ExecuteCommand+.��5F      '  !MaterialDesignThemes.Wpf.PackIcon  $��Right=�5G      $	��Top=�5H         Kind$ 	ExitToApp=�5I      5J      5K      �� 5N   
   $��1��5O      $��0,5,0,0q�5P       �� Orientation$ Vertical=�5Q      .+�5S      �� $��5q�5T      $��Center=�5U      ��5V      �� *MouseStatus+�� 5W      $��5q�5X       J� Opacity$	 0.7Q�5Y      ���	#FFD6D6D6 �� CornerRadius$ 7��5[      ��5\      �� 5]      " ��6    �� Triggers 5^      P� 5_       P� Property �@J�IsMouseOver P� Value$
 True��6=   . �5`   "   ��  �@J�Effect 5a   &   3  -System.Windows.Media.Effects.DropShadowEffect  5b   *      
BlurRadius$ 10Q�5c   -      ShadowDepth$ 1Q�5d   -      Color$ #DDDDDD��5e   -   5g   #   5h      .�5j      ��  �@�Cursor$
 Hand��63   5k      ._�5m      �  5n      �� 5o      $
��auto �6(   .+�5q      � $��0��6   .+�5r      �� $ 
Horizontal=�6%   .+�5s        $��5q�5t   !   $
��Left=�5u   !   $��Center=�5v   !   $ 
CardsHeart=�5w   !   �� 5x      $��10q�6(   ��64   �� *HRData+5y      5z      5{      5|      �� 5}      $��5q�5~      $	 0.7Q�5      ���	#FFD6D6D6$ 7��5�      ��5�      �� 5�      " ��6    5�      P� 5�       �@J�IsMouseOver$
 True��6=   . �5�   "   ��  �@J�Effect 5�   &     5�   *   $ 10Q�5�   -   $ 1Q�5�   -   $ #DDDDDD��5�   -   5�   #   5�      .�5�      ��  �@�Cursor$
 Hand��63   5�      ._�5�      �  5�      �� 5�      $
��auto �6(   .+�5�      � $��0��6   .+�5�      �� $ 
Horizontal=�6%   .+�5�        $��5q�5�   !   $
��Left=�5�   !   $��Center=�5�   !   $ 
LiquidSpot=�5�   !   �� 5�      $��10q�6(   ��64   �� *
SPO2Data+5�      5�      5�      5�      �� 5�      $	 0.7Q�5�      $��5q�5�      ���	#FFD6D6D6$ 7��5�      ��5�      �� 5�      " ��6    5�      P� 5�       �@J�IsMouseOver$
 True��6=   . �5�   "   ��  �@J�Effect 5�   &     5�   *   $ 10Q�5�   -   $ 1Q�5�   -   $ #DDDDDD��5�   -   5�   #   5�      .�5�      ��  �@�Cursor$
 Hand��63   5�      ._�5�      �  5�      �� 5�      $
��auto �6(   .+�5�      � $��0��6   .+�5�      �� $ 
Horizontal=�6%   .+�5�        $��5q�5�   !   $
��Left=�5�   !   $��Center=�5�   !   $ Thermometer=�5�   !   �� 5�      $��10q�6(   ��64   �� *
TempData+5�      5�      5�      5�      5�      5�      5�      