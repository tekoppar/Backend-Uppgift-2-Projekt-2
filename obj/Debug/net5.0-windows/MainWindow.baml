   M S B A M L   `   `   `  ����C  ?Projekt2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Projekt2FuckAllProjekt2FuckAll  5   	        Projekt2FuckAll.MainWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Projekt2FuckAll     5   	     � Title$  
MainWindow��5   	   $��Center=�6   $��Center=�69   $	��200��6T   $	��200��6c   $��Center=�6s   $��Center=�6�    � SizeToContent$ WidthAndHeight=�6�   .��5	      �  � IsSharedSizeScope$
 True��6   $��Center=�6)   $��Center=�6F    � RowDefinitions 5
   
   �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      $	��150 �6   �� 5      $	��500 �6   .+�5   
   �� -   5         ��Name$ btnNewCustomer��5      $��New Customer��6)   $��Center=�6@   $��0,0,0,0q�6]   $��Center=�6n   6�   �� 5   
   $��1��6   $��0��6!   $��	Customers��61   $��Center=�6B   $��Center=�6]   6|   �� 5   
   -   5         ��Name$ listCustomers��5      $��0��6*   $��2��6:   $	��200��6G   $	��200��6V   $��5,5,5,5q�6f   6�   � 5   
   $��3��6   $��0��6!   $��Center=�61   $��Center=�6N   .+�5      �� -   5        $ btnEditCustomer��5      $��Edit Customer��6.   $��Center=�6F   $��2,2,2,2q�6c   $��Center=�6t   6�   5      �� 5   
   -   5        $ btnNewBooking��5      $��New Booking��6(   $��Center=�6>   $��0,0,0,0q�6[   $��Center=�6l   $��1��6�   �� 5   
   $��1��6   $��1��6!   $��Bookings��61   $��Center=�6A   $��Center=�6\   6{   �� 5   
   -   5        $ listCustomerBookings��5      $��1��61   $��2��6A   $	��100��6N   $	��200��6]   $��5,5,5,5q�6m   6�   �� 5    
   -   5        $ btnAddBooking��5       $��Add Booking��6(   $��Center=�6>   $��0,0,0,0q�6[   $��Center=�6l   $��2��6�   �� 5!   
   $��1��6   $��2��6!   $��Bookings��61   $��Center=�6A   $��Center=�6\   6{   �� 5"   
   -   5        $ listBookings��5"      $��2��6)   $��2��69   $	��200��6F   $	��200��6U   $��5,5,5,5q�6e   6x   � 5#   
   $��3��6   $��2��6!   $��Center=�61   $��Center=�6N   .+�5$      �� -   5        $ btnEditBooking��5$      $��Edit Booking��6-   $��Center=�6D   $��2,2,2,2q�6a   $��Center=�6r   6�   5%      5&      5'      