   M S B A M L   `   `   `  ����C  ?Projekt2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:Projekt2FuckAll.CustomControlsProjekt2FuckAll.CustomControls  5   	   5    /Projekt2FuckAll.CustomControls.NewBookingWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   >local,clr-namespace:Projekt2FuckAll.CustomControls     5   	     � Title$  NewBookingWindow��5   	   $��Center=�6"   $��Center=�6=    � SizeToContent$ WidthAndHeight=�6Z   .��5	      � $��Center=�6   $��Center=�6&   $
��Auto��6C   $
��Auto��6P    � RowDefinitions 5
   
   �� 5      �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� -   5         ��Name$ 
textBlock1��5      $��Center=�6(   $��5,5,5,5q�6E   $��1��6V   $
��Room��6c   $
��Wrap=�6o   $��Center=�6�   �� 5   
   -   5         ��Name$ 	listRooms��5      $��2��6&   $��1��66    �� 
IsEditable$
 True��6C   $	��400��6U   6d   �� 5   
   -   5        $ 
textBlock2��5      $��Center=�6(   $��5,5,5,5q�6E   $��2��6V   $��Payment Method��6c   $
��Wrap=�6y   $��Center=�6�   �� 5   
   -   5        $ paymentMethods��5      $��2��6+   $��2��6;   $
 True��6H   $	��400��6Z   �� 5   
   -   5        $ 
textBlock3��5      $��Center=�6(   $��5,5,5,5q�6E   $��3��6V   $��Dates��6c   $
��Wrap=�6p   $��Center=�6�   � 5   
   $��3��6   $��1��6!    � Orientation$ Vertical=�61   $��0,5,0,5q�6H   .+�5      � .+�5      �� $��Check In��6   $��2,0,2,0q�6,   $��Center=�6=   $��Center=�6X   $��75��6u   6�   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ checkInDate��5      62   5      � 5      .+�5       �� $��	Check Out��6   $��2,0,2,0q�6.   $��Center=�6?   $��Center=�6Z   $��75��6w   6�     5!      -   5        $ checkOutDate��5!      5"      5#      � 5$   
   $��4��6   $��1��6!   $��Center=�61   $��Center=�6L   .+�5%      �� -   5        	 ��Name$	 	btnAccept��5%      $��Accept��6(   $��2,2,2,2q�69   $��Center=�6J   $��Center=�6e   6�   �� 5&      -	   5        $	 	btnCancel��5&      $��Cancel��6(   $��2,2,2,2q�69   $��Center=�6J   $��Center=�6e   6�   5'      5)      5*      