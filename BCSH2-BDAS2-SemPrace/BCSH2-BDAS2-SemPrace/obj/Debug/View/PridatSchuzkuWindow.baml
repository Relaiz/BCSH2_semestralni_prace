   M S B A M L   `   `   `  ����O  KBCSH2-BDAS2-SemPrace, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:BCSH2_BDAS2_SemPrace.ViewBCSH2_BDAS2_SemPrace.View  5   	   3    -BCSH2_BDAS2_SemPrace.View.PridatSchuzkuWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:BCSH2_BDAS2_SemPrace.View     5   	     � Title$  Přidat Schůzku��5   	   $	��300��5	   	   $	��400��5
   	    � 	Resources 5         ButtonStyle �� 
TargetType �� Property �� Value��%W  &	        �� 5   
   " ��5      .�5      ��  � ��$ 0,10,0,0q�5      �� 5       � ��$ 10q�5      �� 5       � ��
 ��ح�5      �� 5       � ��
 �����5      �� 5       � ��$
 Bold"�5      �� 5       � ��
 ��  �5      �� 5       � ��$ 2q�5      5      5      .��5      �  � RowDefinitions 5   
   �� 5       $
��Auto �6   �� 5!      $
��Auto �6   �� 5"      $
��Auto �6   �� 5#      $
��Auto �6   �� 5$      $
��Auto �6   �� 5%      $��40 �6   .+�5(   
   �� $��Datum:��6   $��0��5)      $��Center=�5*      (  "System.Windows.Controls.DatePicker  5+   
   $��0��6   $��1��5,      $��5q�5-         SelectedDate 5.      �� *SelectedDate+�� 51   
   $��Čas:��6   $��1��52      $��Center=�53      �� 54   
   $��1��6   $��1��55      $��5q�56       �� SelectedItem 57      �� *SelectedTime+��58      �� *AvailableTimes+�� 5:   
   $��	Pobočka:��6   $��2��5;      $��Center=�5<      �� 5=   
   $��2��6   $��1��5>      $��5q�5?       �� DisplayMemberPath$ Nazev��5A      ��5@      �� *	Pobocky+ 5B      �� *SelectedPobocka+�� 5D   
   $��	Bankéř:��6   $��3��5E      $��Center=�5F      �� 5G   
   $��3��6   $��1��5H      $��5q�5I      $ FullName��5K      ��5J      �� *	Bankers+ 5L      �� *SelectedBanker+�� 5N   
   $��Přidat��6   $��4��5O      #��[  5P      $��Right=�5R      $��5q�5S      $	��100��5T      ��5Q      �� *AddSchuzkaCommand+5T      5U      5V      