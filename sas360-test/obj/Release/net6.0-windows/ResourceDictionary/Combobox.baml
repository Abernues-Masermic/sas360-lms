   M S B A M L   `   `   `  ���� W  SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   	animation  �� 
TargetName �� TargetProperty	 �� To g� Duration g� AutoReverse g� RepeatBehavior �� 
TargetType   ComboBoxToggleButtonTemplate � ColumnDefinitions �� CornerRadius  Combobox_NormalBorderBrush	 �� Triggers
 P� Property P� Value �� 
TargetName �� Property �� Value  ComboBoxTextBoxTemplate �� Padding �� 
TargetType J� SnapsToDevicePixels  Combobox_ItemsBackground  Combobox_style �� 	ClickMode d� 	IsChecked �� Path �� RelativeSource �� Mode �� SelectionBoxItemTemplate �� SelectionBoxItem J� IsHitTestVisible2   ,System.Windows.Markup.XmlAttributeProperties   XmlSpace �� 
IsReadOnly � AllowsTransparency �� IsDropDownOpen � ActualWidth �� MaxDropDownHeight-  'System.Windows.Input.KeyboardNavigation    DirectionalNavigation! P� 
SourceName�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml      %�
  &	        &	 M     7  &	     '�� Z    7  &	 �    7  7  7  �� $  Panel��$ Color� 	#FF8BAEB8$ 0:0:0.25D�$
 True��$ 4���� " d�.R��  �� �� $��16 �.+��� $��Border��
���   �$ 2,2,2,2��$��1,1,1,1q�
���   �$��2���� $��Text_caption��$��1,1,1,1q�8��  $ 2,0,0,2��
�������$��0��F� $��Arrow��$��3,0,3,0q�"����        � 	=  	= � z     	$��Center=�
�������$��Center=�$��8��$��1��	 P� 
 �@J�IsMouseOver$
 True��. ��� $ Border�� � ��
 ������� $ Arrow�� � ��
 �   �P� 
 �@d�	IsChecked$
 TrueY�. ��� $ Border�� � ��
 ������� $ Arrow�� � ��
 �  ��P� 
 � {�$ False��. ��� $ Border�� � �� �	#FFA29D9D�� $ Text_caption�� � �� �	#FFA29D9D�� $ Arrow�� � �� �	#66FFFFFF�� " ��.R��� $��PART_ContentHost��
�������$|�False��$ 2,2,2,2q��� " ��.���  �@J�SnapsToDevicePixels$
 True����  �@�FocusVisualStyle Z� ��  � ��
 �   ���  �@�OverridesDefaultStyle$
 True����  � �� �� " ��.R��� $��Border��$
 True��$ 2,2,2,2q�._��� #��z��#��z��	 P� 
 �@��IsHighlighted$
 True��. ��� $ Border��8    � ���� " ��.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 True����  � ��$
 Auto=���  � ��$
 Auto=���  � ��$
 True����  � ��
 �   ���  � ��$
 Bold"���  � ��$ 13(���  � ��$ Arial)���  � �� �	#FF042A55��  � ��
 �   ���  � ��$
 Left=���  � ��$ Center=���  �@�FocusVisualStyle Z� ��  � �� �� " ��.R�� .+�d� $��ToggleButton��$ Press=�$|�False��$��2��8��   �� $ IsDropDownOpen� �� *TemplatedParent+$ TwoWay=��� $��ContentSite��$��5,0,0,0q�$
��Left=�$��Center=�#��z #��z $ False����  preserve$��PART_EditableTextBox��$��3,3,23,3q�$y�Hidden=�$
��Left=�
������ $��Center=�$ False��$
|�True��8�� ��Z� � $��Popup��$��Bottom=�$|�False��$
 True��#��z $
��Fade=�.��� $��DropDown��#��z #��z $
 True��.+��� $��DropDownBorder��8�� $��1,1,1,1q�
��������� $��4,6,4,6q�$
 True��.���� $  	Contained=�	 P� 
 �@��HasItems$ False��. ��� $ DropDownBorder�� � ��$ 95��P� 
 �@��
IsGrouping$
 True��. ���  � ��$ False��P� $! Popup��
 �@�AllowsTransparency$
 True��. ��� $ DropDownBorder�� �@��CornerRadius$ 4���� $ DropDownBorder�� � ��$ 0,2,0,0q�P� 
 �@��
IsEditable$
 True��. ���  �@ 	IsTabStop$ False���� $ PART_EditableTextBox�� � y�$ Visible=��� $ ContentSite�� � y�$ Hidden=�