   M S B A M L   `   `   `  ����O  KBCSH2-BDAS2-SemPrace, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:BCSH2_BDAS2_SemPrace.ViewBCSH2_BDAS2_SemPrace.View  5   	   8    2BCSH2_BDAS2_SemPrace.View.UpravitZamestnanecWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:BCSH2_BDAS2_SemPrace.View     5   	     � Title$  Upravit Zamestnanec��5   	   $	��300��5	   	   $	��350��5
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
Prijmeni+$ PropertyChanged=�5:      5;      �� 5?   
   $��2��6   $ 
Horizontal=�5@      $��10q�5A      .+�5B      �� $��E-mail:��6   $	��100��5C      $��Center=�5D      �� 5E      $	��200��5F      ��5E      �� *Email+$ PropertyChanged=�5F      5G      �� 5I   
   $��3��6   $ 
Horizontal=�5J      $��10q�5K      .+�5L      �� $��Telefonní číslo:��6   $	��100��5M      $��Center=�5N      �� 5O      $	��200��5P      ��5O      �� *
TelCislo+$ PropertyChanged=�5P      5Q      �� 5S   
   $��4��6   $ 
Horizontal=�5T      $��10q�5U      $��Center=�5V      .+�5W      �� $��Upravit Zamestnance��6   #��[  5Y      $	��150��5Z      ��5X      �� *UpravitZamCommand+5Z      5[      5\      5]      