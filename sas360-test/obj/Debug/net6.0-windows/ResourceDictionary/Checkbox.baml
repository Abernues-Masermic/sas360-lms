   M S B A M L   `   `   `  ����W  SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   Label_checkbox  �� 
TargetType �� Property �� Value  RenderTransform.Checkbox �� ScaleX �� ScaleY �� AngleX �� AngleY �� Angle T� X	 T� Y  CheckBoxSwitchStyle
 �� 
TargetType � 	Resources  Key g� 	BeginTime �� 
TargetName �� TargetProperty O� KeyTime O� Value J� SnapsToDevicePixels �� ContentStringFormat �� ContentStringFormat ��Name  Checkbox.White.Gradient �� CornerRadius � ColumnDefinitions ��Name � Cursor J� Opacity J� RenderTransformOrigin �� Triggers P� Property P� Value N� ExitActions ��Name  
OnChecking  N� EnterActions  OnUnchecking! �� 
TargetName  Checkbox.Red.Gradient  Checkbox.Red.Border 	 Checkbox.Green.Gradient 
 Checkbox.Green.Border  Checkbox_setting" �� Orientation# �� 
Background$ �� Bullet  Checkbox_image_style% ��Name& ��Name' �� RelativeSource�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml      5      %�  &	        &	     &	 �    7  7   7   7  7  7  7  7  7  7 	 7 
 &	 
    &	 	    �� 5      "  ��6#   .�5   
   ��  � ��$ Center=�66   �� 5   
    � ��$
 Bold"�6-   �� 5   
    � ��$ 13(�6+   �� 5   
    � ��
 �����6-   �� 5	   
    � ��$ Verdana)�6-   �� 5
   
    � ��$ 1q�6$   5      V� 5      .~�5   
   �� $ 1Q�6   $ 1Q�6$   �� 5   
   $ 0Q�6   $ 0Q�6#   �� 5   
   $ 0Q�6   T� 5   
   $ 0Q�6   $	 0Q�6#   5      �� 5      "  ��6(   .�5   
   ��  � ��# � ��6'   �� 5   
    � ��# � ��6'   �� 5   
    � �� 5      �� 5      "
 ��6"    5      ���� 5      OnUnchecking .��5      U�  00:00:006<   $ Border_slider��5   <   $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�5    <   .\�5!   "   �� $ 00:00:00.1000000��67   $ 60Q�6R   5"      5#      �� 5%      
OnChecking .��5&      U�  00:00:006<   $ Border_slider��5'   <   $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�5(   <   .\�5)   "   �� $ 00:00:00.1000000��67   $ 0Q�6R   5*      5+      5-      .R�5/      ]� .+�50      �� # z 6+   #��z��51   +   # z 52   +   #��z��53   +   $
��True��54   +   $��Center=�55   +   � 56      .+�57      �� $ BackgroundBorder��6%   ���	#FF9393938��  $��1,1,2,2q�5:   %   $ 3��5;   %   $��25��5<   %   $	��120��5=   %   ._�5>   "   �  5?   &   �� 5@   *   �� 5A   *   .+�5D   &   �� $ 
Label_stop��6,   $
��STOP��6@   $��0��5E   ,   8�� $��Center=�5G   ,   $��Center=�6I   �� 5H   &   $ Label_start��6,   $��START��6A   $��1��5I   ,   8�� $��Center=�5K   ,   $��Center=�6I   5L   #   5M      �� 5O      $ Border_slider��6%   ���	#FF939393$��1q�5Q   %   8�� $
 Hand��5S   %   $	 0.7Q�5T   %   8z� $
��Left=�5V   %   $��60��5W   %   $��25��60   $ 3��5X   %   $ 0.5,0.5&�5Y   %   5Z      5[       5]      P� 5_       �@d�	IsChecked$ FalseY�67    5`      �� 5a   "   $  OnChecking_BeginStoryboard��6[   8��   5d      �� 5e   "   $" OnUnchecking_BeginStoryboard��6]   8�� . �5h      �� $! BackgroundBorder��6%   8   � ��6Y   �� 5i      $! BackgroundBorder��6%   8   � ��6Z   5j      P� 5l       �@d�	IsChecked$
 TrueY�67   . �5m      �� $! BackgroundBorder��6%   8 	  � ��6Y   �� 5n      $! BackgroundBorder��6%   8 
  � ��6Z   5o      P� 5q       � {�$ False��67   5r      5v      5x      5y      �� 5|      "  ��6%   .�5}   
   ��  �@J�SnapsToDevicePixels$
 true��60   �� 5~   
    � ��
 ���� 6'   �� 5   
    � ��
 �   �6*   �� 5�   
    � �� 5�      �� 5�      "
 ��6"   .R�5�      �� $" 
Horizontal=�6!   $��Center=�6:   .+�5�      �� 
# ���� $
 Hand��6C   $ 5�      � 5�   "   $��16��6'   $��16��63   .+�5�   &   �� $��
RadioOuter��6-   
�������
���   �$��2q�6f   $ 2��6z   �� 5�   &   $��	RadioMark��6-   $ 0��6>   $��4q�6O   
��� d �$y�Hidden=�6r   5�   #   ._�5�      �� $��8,0,0,0q�6(   
���   �$
��Bold"�6M   $��Cambria)�6_   $��12(�6t   �� 5�      5�      5�      5�       5�      P� 5�       �@d�	IsChecked$
 trueY�67   . �5�      �� $! 	RadioMark��6%    � y�$ Visible=�6R   5�      P� 5�       �@d�	IsChecked$ falseY�67   . �5�      �� $! 	RadioMark��6%    � y�$ Hidden=�6R   5�      P� 5�       �@J�IsMouseOver$
 true��69   . �5�      �� $! 
RadioOuter��6%    � ��
 �����6T   5�      P� 5�       � ��$
 true��67   . �5�      �� $! 
RadioOuter��6%    � ��
 �   �6T   �� 5�      $! 	RadioMark��6%    � ��
 ��  �6R   5�      5�      5�      5�      �� 5�      "  ��6)   .�5�   
   ��  � �� 5�      �� 5�      "
 ��6"   .R�5�      �� $ Border_checkbox��6   ���#4c6680$��2q�6L   $ 5��6`   ._�5�      � .+�5�      �� $% Rectangle_checkbox��6(   $��18��6D   $��18��6O   ���#9ccdff6[   �� 5�      $& imageCheckBox��6$   $
 Hand��6�   $��20��6�   $��20��6�   $��1q�6�   ��6;   �� *Tag+' �� *TemplatedParent+6�   �� 5�      $��Center=�6/   5�      5�       5�      P� 5�       �@d�	IsChecked$
 TrueY�67   . �5�      �� $! Border_checkbox��6%    � �� �#4c66806Y   �� 5�      $! Rectangle_checkbox��6%    � �� �#9ccdff6U   5�      P� 5�       �@d�	IsChecked$ FalseY�67   . �5�      �� $! Border_checkbox��6%    � ��
 ���� 6Y   �� 5�      $! Rectangle_checkbox��6%    � ��
 ���� 6U   5�      5�      5�      5�      5�      