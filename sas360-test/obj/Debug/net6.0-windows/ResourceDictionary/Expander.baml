   M S B A M L   `   `   `  ����W  SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ExpanderRightHeaderStyle  �� 
TargetType �� Property �� Value �� 
TargetType �� Padding J� SnapsToDevicePixels � LayoutTransform �� Angle ��Name + 'Expander.Static.Gradient.Rectangle.Fill	 �� RadiusX
 �� RadiusY $  Expander.Static.Rectangle.Stroke F�Name   Expander.Static.Arrow.Stroke �� Triggers P� Property P� Value �� 
TargetName ' #Expander.MouseOver.Rectangle.Stroke . *Expander.MouseOver.Gradient.Rectangle.Fill # Expander.MouseOver.Arrow.Stroke % !Expander.Pressed.Rectangle.Stroke , (Expander.Pressed.Gradient.Rectangle.Fill !	 Expander.Pressed.Arrow.Stroke &
 "Expander.Disabled.Rectangle.Stroke $  Expander.Disabled.Rectangle.Fill " Expander.Disabled.Arrow.Stroke  Expander_style d�Name � Cursor d� 	IsChecked �� Mode �� RelativeSource ��Name :� RoutedEvent W� From	 W� To �� TargetProperty g� Duration�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml      5      %�
  &	        7  7  7  7  7  7  7  7  7 	 7 
 7  7  &	 W    7   �� 5      "  d�6-   .�5   
   ��  � �� 5      �� 5      " d�6"   .R�5	      �� # z��6   ._�5
      � 
������ $ False��68   .+�5      �  5   "   V� 5   &   ~�5   .   X� �� 5   2   $	 -90Q�6B   5   /   5   '   .+�5   "   �� $ 	rectangle��6,   8��  $��Center=�5   ,   $��Center=�6I   $	��110��5   ,   $��20��69   $	 3��5   ,   $
 3��68   8�� $��1��5   g   F� 5   "   $ arrow��6'   "����@B `� � �D @KL � z `� 	$��Center=�5!   '   $��Center=�6D   $ false��5"   '   8�� $��3��5#   ^   5$      �� 5&      $��0,4,0,0q�5'   !   $��Center=�5(   !   $	��Top=�6>   $
��True��5)   !   $
 True��6<   5*      5+       5-      P� 5/       �@d�	IsChecked$
 trueY�67   . �50      �� $ arrow��65    � ��" ��@B  �D � �D @B � z  �D 	6H   51      P� 53       �@J�IsMouseOver$
 true��69   . �54      �� $ 	rectangle��67   8   � ��6N   �� 55      $ 	rectangle��65   8   � ��6M   �� 56      $ arrow��67   8   � ��6K   57      P� 59       � ��$
 true��67   . �5:      �� $ 	rectangle��67   8   � ��6N   �� 5;      $ 	rectangle��6@    � ��$	 1.5��6W   �� 5<      $ 	rectangle��65   8   � ��6L   �� 5=      $ arrow��67   8   � ��6J   �� 5>       � ��$ 3q�6@   5?      P� 5A       � {�$ false��67   . �5B      �� $ 	rectangle��67   8 	  � ��6N   �� 5C      $ 	rectangle��65   8 
  � ��6M   �� 5D      $ arrow��67   8   � ��6J   5E      5H      5J      5K      �� 5N      "  9�6#   .�5O   
   ��  � �� 5P      �� 5Q      " 9�6"   .R�5S      �� #��z��5T      #��z��5U      #��z��5V      $
 true��5W      ._�5Y      ]� .+�5Z      d� $ 
HeaderSite��6+   $
��Left=�5[   +   $	��Top=�5\   +   $
 Hand��5]   +    5^   +   �� *
IsExpanded+$ TwoWay=� �� *TemplatedParent+�� 5`      $ 
ExpandSite��6/   $|�false��5a   /   #��z��5b   /   #��z��5c   /   $y�Visible=�6R   #��z��5d   /   5e      5f       5h      :� 5i      $ Expander.Expanded��6'   [�5j      �� 5k   "   .��5l   &   �� .��5m   *   W�  206:    5206D   $ Width�6M   $ 	0:00:00.7D�6o   5n   '   5o   #   5q      :� 5s      $ Expander.Collapsed��6'   [�5t      �� 5u   "   .��5v   &   �� .��5w   *   W�  5206:    206E   $ Width�6N   $ 	0:00:00.7D�6q   5x   '   5y   #   5{      P� 5}       �@9�ExpandDirection$ Right=�6=   . �5~      �� $ 
HeaderSite��66   8    � ��6N   5      5�      5�      5�      5�      