   M S B A M L   `   `   `  ����O  KBCSH2-BDAS2-SemPrace, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:BCSH2_BDAS2_SemPrace.ViewBCSH2_BDAS2_SemPrace.View  5   	   +    %BCSH2_BDAS2_SemPrace.View.AdminWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:BCSH2_BDAS2_SemPrace.View     5   	     � Title$  Admin Panel��5   	   $	��600��5	   	   $	��800��5
   	    � 	Resources 5         ButtonStyle �� 
TargetType �� Property �� Value��%W  &	        �� 5   
   " ��5      .�5      ��  � ��$ 0,10,0,0q�5      �� 5       � ��$ 10q�5      �� 5       � ��
 ��ح�5      �� 5       � ��
 �����5      �� 5       � ��$
 Bold"�5      �� 5       � ��
 ��  �5      �� 5       � ��$ 2q�5      5      5      .��5      �  � ColumnDefinitions 5   
   �� 5       $	��200 �6   �� 5!      $��* �6   �� 5"      $��* �6    � RowDefinitions 5%   
   �� 5&      $
��Auto �6   �� 5'      $��* �6   .+�5+   
   �� $��0��6   $��0��5,      $��2��5-       �� Orientation$ Vertical=�5.      $��5q�5/      .+�50      �� $��Jako Klient��6   #��[  52      $��5q�53      ��51      �� *OpenKlientWindowCommand+53      �� 54      $��Jako Zamestnanec��6   #��[  55      $��5q�57      ��56      �� *OpenZamestnanecWindowCommand+57      �� 58      $��	Bankomaty��6   #��[  5:      $��5q�5;      ��59      �� *AddBankomatCommand+5;      �� 5<      $��Pobocky��6   #��[  5>      $��5q�5?      ��5=      �� *AddPobockaCommand+5?      �� 5@      $
��Exit��6   #��[  5B      $��5q�5C      ��5A      �� *ExitCommand+5C      5D      �� 5E   
   $��1��6   $��Klienti��5F      $��Center=�5G      $
��Bold"�5H      $��16(�5I      $��0,10,0,6q�5J      �� 5K   
   $��2��6   $��Zamestnanci��5L      $��Center=�5M      $
��Bold"�5N      $��16(�5O      $��	0,10,0,10q�5P      �� 5R   
   -   5         ��Name$ 
klientList��5T      $��1��5R      $��1��5S      ��5U      �� *	Klienti+	 �� SelectedItem	 5V      �� *SelectedKlient+��5W      �� 5X      .R�5Y      �� $ 
Horizontal=�6!   .+�5[      �� $��5q�5\   $   ��5[   $   �� *Jmeno+5\   $   �� 5]      $��5q�5^   $   ��5]   $   �� *
Prijmeni+5^   $   5`      5a      5c      �� 5f   
   -   5        $ zamList��5h      $��2��5f      $��1��5g      ��5i      �� *Zamestnanci+	 5j      �� *SelectedZamestnanec+��5k      �� 5l      .R�5m      �� $ 
Horizontal=�6!   .+�5o      �� $��5q�5p   $   ��5o   $   �� *Jmeno+5p   $   �� 5q      $��5q�5r   $   ��5q   $   �� *
Prijmeni+5r   $   5t      5u      5x      5y      5z      