   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089X TXceed.Wpf.Toolkit, Version=3.7.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4   CustomTextbox  �� 
TargetType(   "Xceed.Wpf.Toolkit.WatermarkTextBox �� Property  Gray �� Value  
TextColour  RobotoMono Thin �� Orientation �� 
TargetType ��Name �� CornerRadius ��Name J� SnapsToDevicePixels	 ��Name
    	Watermark J� IsHitTestVisible �� Path �� RelativeSource �� TargetNullValue J� Opacity �� EndPoint �� 
StartPoint � Color � Offset �� Triggers ]� 
Conditions �� Property �� Value �� 
TargetName P� Property P� Value �� Triggers  SearchBoxTitle�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      5      4xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      %�
  &	        7  7  7  7  7  &	 �	    7  �� 5      "    6"   .�5   
   ��  � ��8   6'   �� 5   
    �@��TextAlignment$
 Left=�6*   �� 5   
    � ��$ Center=�65   �� 5	   
    � ��$ Bottom=�6.   �� 5
   
    � ��8  6'   �� 5   
    � �� 6(   Z� �� 5   
    � ��8  63   �� 5   
    �@  	Watermark 5      �� 5      $ 
Horizontal=�6   .+�5      �� $��10��6   $��10��6(   $-��'pack://application:,,,/Icons/Search.png��63   �� 5      $��Search��6    8�� 8�� $��4,0,0,0q�5   I   5      5      �� 5   
    � �� 5      �� 5      "   6"   .R�5      � .+�5      �� $ Border��6!   #��z��61   #��z��6]   #��z��6�   $ 10��6�   ._�5      � .+�5   "   �� $ PART_ContentHost��6/   # z 6I   �� 5   "   $	 PART_WatermarkHost��63   #��z
 6O   #��z��6u    .  #��z��6�   $y�	Collapsed=�6�   #��z��6  ��5   &   �� 5   *   $ WatermarkTemplate�62    6K   �� *TemplatedParent+ 5    .   �� 5!   2   .R�5"   6   �� #��� ��6Y   |�.  ��6E   �� 6�   5#   3   5%   +   5'   #   5(      5)      �� 5*      $ MouseOverVisual��6!   #��z��6:   $ 10��6n   $ 0Q�6�   ��5+      �� 5,   "   $	 0,1&�66   $	 0,0&�6E   .��5-   &   � $ 	#FF5794BF��63   $
 0.05Q�6E   � 5.   &   $ 	#FFB7D5EA��63   $
 0.07Q�6E   � 5/   &   $ 	#FFC7E2F1��63   $ 1Q�6E   50   #   52      �� 53      $ FocusVisual��6!   #��z��66   $ 10��6j   $ 0Q�6|   ��54      �� 55   "   $	 0,1&�66   $	 0,0&�6E   .��56   &   � $ 	#FF3D7BAD��63   $
 0.05Q�6E   � 57   &   $ 	#FFA4C9E3��63   $
 0.07Q�6E   � 58   &   $ 	#FFB7D9ED��63   $ 1Q�6E   59   #   5;      5<       5=      ]� 5>       5?      �� 5@   "    �@  KeepWatermarkOnGotFocus .  6O   �� 5A   "    �@J�	IsFocused .  6A   �� 5B   "    � ��$  ��6<   ./�5D      �� $ PART_WatermarkHost��6;    � y�$ Visible=�6[   5E      ]� 5F       5G      �� 5H   "    �@  KeepWatermarkOnGotFocus . 6O   �� 5I   "    � ��$  ��6<   ./�5K      �� $ PART_WatermarkHost��6;    � y�$ Visible=�6[   5L      P� 5M       � {� .  . �5N      �� # � ��6;    � ��5O      5Q      5S       5U   
   P� 5V       �@J�IsMouseOver . . �5W      ��  � ��$ 0q�64   5X      P� 5Y       � {� . . �5Z      ��  � ��$ 0q�64   5[      5]      �� 5_      "  ��6#   .�5`   
   ��  � ��8   6'   �� 5a   
    � ��$ Center=�60   �� 5b   
    � ��$ Bottom=�6.   �� 5c   
    � ��$ 0,0,0,10q�6#   5d      5f      