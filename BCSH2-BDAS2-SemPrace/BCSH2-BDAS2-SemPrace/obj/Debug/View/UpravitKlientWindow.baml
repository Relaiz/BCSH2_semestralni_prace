   M S B A M L   `   `   `  ����O  KBCSH2-BDAS2-SemPrace, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:BCSH2_BDAS2_SemPrace.ViewBCSH2_BDAS2_SemPrace.View  5   	   3    -BCSH2_BDAS2_SemPrace.View.UpravitKlientWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:BCSH2_BDAS2_SemPrace.View     5   	     � Title$  Upravit Klienta��5   	   $	��300��5	   	   $	��350��5
   	    � 
ResizeMode$ NoResize=�5   	    � 	Resources 5         ButtonStyle �� 
TargetType �� Property �� Value��%W  &	        �� 5   
   " ��5      .�5      ��  � ��$ 0,10,0,0q�5      �� 5       � ��$ 10q�5      �� 5       � ��
 ��ح�5      �� 5       � ��
 �����5      �� 5       � ��$
 Bold"�5      �� 5       � ��
 ��  �5      �� 5       � ��$ 2q�5      5      5      .��5      �  � RowDefinitions 5    
   �� 5!      $
��Auto �6   �� 5"      $
��Auto �6   �� 5#      $
��Auto �6   �� 5$      $
��Auto �6   �� 5%      $
��Auto �6   �� 5&      $
��Auto �6   .+�5)   
   �� $��0��6    �� Orientation$ 
Horizontal=�5*      $��10q�5+      .+�5,      �� $��Jméno:��6   $	��100��5-      $��Center=�5.      �� 5/      $	��200��50      ��5/      �� *Jmeno+ �� UpdateSourceTrigger$ PropertyChanged=�50      51      �� 53   
   $��1��6   $ 
Horizontal=�54      $��10q�55      .+�56      �� $��Příjmení:��6   $	��100��57      $��Center=�58      �� 59      $	��200��5:      ��59      �� *
Prijmeni+$ PropertyChanged=�5:      5;      �� 5=   
   $��2��6   $ 
Horizontal=�5>      $��10q�5?      .+�5@      �� $��Číslo průkazu:��6   $	��100��5A      $��Center=�5B      �� 5C      $	��200��5D      ��5C      �� *CisloPrukazu+$ PropertyChanged=�5D      5E      �� 5G   
   $��3��6   $ 
Horizontal=�5H      $��10q�5I      .+�5J      �� $��E-mail klienta:��6   $	��100��5K      $��Center=�5L      �� 5M      $	��200��5N      ��5M      �� *Email+$ PropertyChanged=�5N      5O      �� 5Q   
   $��4��6   $ 
Horizontal=�5R      $��10q�5S      .+�5T      �� $��Telefonní číslo:��6   $	��100��5U      $��Center=�5V      �� 5W      $	��200��5X      ��5W      �� *
TelCislo+$ PropertyChanged=�5X      5Y      �� 5[   
   $��5��6   $ 
Horizontal=�5\      $��10q�5]      $��Center=�5^      .+�5_      �� $��Upravit Klienta��6   #��[  5a      $	��150��5b      ��5`      �� *UpravitCommand+5b      5c      5d      5e      