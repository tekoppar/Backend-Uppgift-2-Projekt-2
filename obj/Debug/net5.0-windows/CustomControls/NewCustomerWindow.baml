   M S B A M L   `   `   `  ����C  ?Projekt2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:Projekt2FuckAll.CustomControlsProjekt2FuckAll.CustomControls  5   	   6    0Projekt2FuckAll.CustomControls.NewCustomerWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   >local,clr-namespace:Projekt2FuckAll.CustomControls     5   	     � Title$  NewCustomerWindow��5   	   $��Center=�6#   $��Center=�6@    � SizeToContent$ WidthAndHeight=�6[   $	��480��6z   .��5	      � $��Center=�6   $��Center=�6(    � RowDefinitions 5
   
   �� 5      �� 5      �� 5      $	��225 �6   �� 5       � ColumnDefinitions 5   
   �� 5      $	��125 �6   �� 5      $	��350 �6   .+�5   
   �� -   5         ��Name$ 	textBlock��5      $��Center=�6'   $��0,5,0,5q�6D   $��
First Name��6U   $
��Wrap=�6g   $��Center=�6{   �� 5   
   -   5        $ 
textBlock1��5      $��Center=�6(   $��0,5,0,5q�6E   $��1��6V   $��	Last Name��6c   $
��Wrap=�6t   $��Center=�6�   �� 5   
   -   5         ��Name$ tbFirstName��5      $��1��6'   $��Center=�67    �� TextWrapping$
 Wrap=�6T   $��Center=�6h   $	��230��6�   �� 5   
   -   5        $ 
tbLastName��5      $��1��6&   $��Center=�66   $��1��6S   $
 Wrap=�6`   $��Center=�6t   $	��230��6�   �� 5   
   -   5         ��Name$ btnContactInfo��5      $��Add Contact Info��6)   $��Center=�6D   $��5,5,5,0q�6a   $��2��6r   $	��Top=�6   6�   �� 5   
   -   5         ��Name$ listContactInfo��5      $��2��6,   $	��200��69   $	��200��6H   $��	4,5,10,10q�6X   $��1��6k   $��Stretch=�6{   $��Stretch=�6�   5      � 5   
   $��3��6   $��1��6!   $��Center=�61   $��Center=�6N   $��0,5,0,5q�6i   .+�5      �� -   5        $ 	btnAccept��5      $��Accept��6(   $��Center=�69   $��2,0,2,0q�6V   $��Center=�6g   6�   �� 5      -   5        $ 	btnCancel��5      $��Cancel��6(   $��Center=�69   $��2,0,2,0q�6V   $��Center=�6g   6�   5      5      5       