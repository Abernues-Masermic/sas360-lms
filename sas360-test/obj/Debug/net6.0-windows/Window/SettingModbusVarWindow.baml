   M S B A M L   `   `   `  ����E  Asas360-lms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:sas360_testsas360_test  5   	   (    "sas360_test.SettingModbusVarWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:sas360_test     5   	   ] YXceed.Wpf.Toolkit.NET5, Version=4.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   -   5          � Title$  
MainWindow��5	   	   $	��200��5
   	   $	��350��6    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	    � AllowsTransparency$
 True��5   	   
������ .��5      ��    Border_background_separator#��[  6   ._�5   
   ��   Border_background#��[ 6   ._�5      � .+�5      ��  �� Orientation$ Vertical=�6    � Cursor$  ��64   .+�5      ]� .+�5      ]� $
��Left=�6$   $ SizeAll��6:   .+�5      �� -   5        
���   � �� CornerRadius$ 5��5   9   
�������$��1q�6^   $��5q�6r   $	��180��6}   $
��Left=�6�   ._�5   "   �� -   5         ��Name$ Label_standard��5   (     Label_title#��[ 6B   $��VAR SETTTING��6g   5      5      � 5      $��Right=�6$   $��Right=�6;   .+�5      �� -   5         ��Name$ Button_save��5   %     Button_rounded#��[ 6:   $��30��6b   $��30��6n   $��0,0,5,0q�6y   $��Right=�6�   .��5    "   �� $��/Images/Save.png��6(   $��22��6E   $��22��6Q   6^   5!      �� 5#      -   5        $ Button_exit��5#   %   #��[ 6:   $��32��6c   $��32��6o   $��5q�6z   $��Right=�6�   .��5$   "   �� $��22��6(   $��22��64   $��/Images/exit.png��6?   5%      5&      5'      �� 5*      -   5        	 ��Name$	 Border_paths��5*      $	��Top=�63     Border_shadow#��[ 6H   $��5q�6o   ._�5,      �� $ Vertical=�6%   $��2q�6<   .+�5-      � 
 � Orientation$
 
Horizontal=�6(   $��2q�6A   .+�5.   "   ��   Label_standard_bold#��[ 6)   $	��100��6V   .��6b   ADDR6h   %  Xceed.Wpf.Toolkit.DecimalUpDown  5/   "   -   5          Name$ Decimal_addr��5/   5   $	��100��6K     DecimalUpDownStyle#��[ 6W   50      � 52      $
 
Horizontal=�6(   $��2q�6A   .+�53   "   �� #��[ 6)   $	��100��6V   .��6b   NAME6h   �� 54   "   -   5         ��Name$ Textbox_name��54   *     Textbox_setting#��[ 6@   $	��200��6j   55      � 57      $
 
Horizontal=�6(   $��2q�6A   .+�58   "   �� #��[ 6)   $	��100��6V   .��6b   UNIT6h   �� 59   "   -	   5        $ Textbox_unit��59   *   #��[ 6@   $	��100��6j   5:      � 5<      $
 
Horizontal=�6(   $��2q�6A   .+�5=   "   �� #��[ 6)   $	��100��6V   .��6b   FORMAT6j     5>   "   -
   5        $ Decimal_format��5>   5   $	��100��6M   #��[ 6Y   5?      � 5A      $
 
Horizontal=�6(   $��2q�6A   .+�5B   "   �� #��[ 6)   $	��100��6V   .��6b   TYPE6h   �� 5C   "   -   5         ��Name$ Combobox_type��5C   +   $	��100��6B   $��22��6N     Combobox_style#��[ 6Z   5D      5F      5I      5L      5M      5N      5O      5P      