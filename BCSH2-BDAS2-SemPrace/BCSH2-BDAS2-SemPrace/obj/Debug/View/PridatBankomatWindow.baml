   M S B A M L   `   `   `  ����O  KBCSH2-BDAS2-SemPrace, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:BCSH2_BDAS2_SemPrace.ViewBCSH2_BDAS2_SemPrace.View  5   	   4    .BCSH2_BDAS2_SemPrace.View.PridatBankomatWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:BCSH2_BDAS2_SemPrace.View     5   	     � Title$  Pridat Bankomat��5   	   $	��400��6!   $	��400��6.    � 
ResizeMode$ NoResize=�5	   	    � 	Resources 5
         ButtonStyle �� 
TargetType �� Property �� Value��%W  &	        �� 5   
   " ��5      .�5      ��  � ��$ 0,10,0,0q�5      �� 5       � ��$ 10q�5      �� 5       � ��
 ��ح�5      �� 5       � ��
 �����5      �� 5       � ��$
 Bold"�5      �� 5       � ��
 ��  �5      �� 5       � ��$ 2q�5      5      5      .��5       �  � RowDefinitions 5!   
   �� 5"      $
��Auto �6   �� 5#      $
��Auto �6   �� 5$      $
��Auto �6   �� 5%      $
��Auto �6   �� 5&      $
��Auto �6   �� 5'      $
��Auto �6   �� 5(      $
��Auto �6   �� 5)      $
��Auto �6   �� 5*      $
��Auto �6   �� 5+      $
��Auto �6   �� 5,      $
��Auto �6   �� 5-      $
��Auto �6   �� 5.      $
��Auto �6   �� 5/      $
��Auto �6   �� 50      $
��Auto �6   �� 51      $
��Auto �6   .+�54   
   �� $��Nazev:��6   $��0��55      $��10q�56      �� 57   
   -   5         ��Name$ txtJmeno��57      $��0��58      $��
150,0,10,0q�5:      ��59      �� *Nazev+ �� UpdateSourceTrigger$ PropertyChanged=�	 �� ValidatesOnDataErrors	 . 5:      �� 5<   
   $��Adresa - Stat:��6   $��1��5=      $��10q�5>      �� 5?   
   -   5        $ txtStat��5?      $��1��5A      $��
150,0,10,0q�5B      ��5@      �� *Stat+$ PropertyChanged=�	 . 5B      �� 5D   
   $��Adresa - Mesto:��6   $��2��5E      $��10q�5F      �� 5G   
   -   5        $ txtMesto��5G      $��2��5I      $��
150,0,10,0q�5J      ��5H      �� *Mesto+$ PropertyChanged=�	 . 5J      �� 5L   
   $��Adresa - Ulice:��6   $��3��5M      $��10q�5N      �� 5O   
   -   5        $ txtUlice��5O      $��3��5Q      $��
150,0,10,0q�5R      ��5P      �� *Ulice+$ PropertyChanged=�	 . 5R      �� 5T   
   $��Adresa - Cislo Popisne:��6   $��4��5U      $��10q�5V      �� 5W   
   -   5        $ txtCislo_popisne��5W      $��4��5Y      $��
150,0,10,0q�5Z      ��5X      �� *CisloPopisne+$ PropertyChanged=�	 . 5Z      �� 5[   
   $��Adresa - PSC:��6   $��5��5\      $��10q�5]      �� 5^   
   -   5        $ txtPSC��5^      $��5��5`      $��
150,0,10,0q�5a      ��5_      �� *PSC+$ PropertyChanged=�	 . 5a      �� 5c   
   $��Status:��6   $��7��5d      $��10q�5e      �� 5f   
   -   5        
 ��Name$
 comboBoxStatus��5f       �� DisplayMemberPath$ Popis��5h       �� SelectedValuePath$ IdStatus��5i      $��7��5k      $��
150,0,10,0q�5l      ��5g      �� *	Statusy+ �� SelectedValue 5j      �� *SelectedStatusId+5l      �� 5n   
   $��Pridat��6   #��[  5o      $��8��5p      $
��Left=�5q      $��Center=�5r      $��	219,0,0,0q�5s      $	��111��5t      {�5u      �� *IsDataValid+��5v      �� *PridatBankomatCommand+5w      5x      