   M S B A M L   `   `   `  ����O  KBCSH2-BDAS2-SemPrace, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:BCSH2_BDAS2_SemPrace.ViewBCSH2_BDAS2_SemPrace.View  5   	   0    *BCSH2_BDAS2_SemPrace.View.ShowKlientWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:BCSH2_BDAS2_SemPrace.View     5   	     � Title$  Klient Info��5   	   $	��400��5	   	   $	��500��5
   	    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$ SingleBorderWindow=�5   	   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��10q�5       �� Orientation$ 
Horizontal=�5      .+�5      �� $��0,0,10,0q�6   .+�5      �� $��Jméno:��6   $
��Bold"�5      �� 5      $��Příjmení:��6   $
��Bold"�5      �� 5      $��Číslo Průkazu:��6   $
��Bold"�5      �� 5      $��Klient Email:��6   $
��Bold"�5       �� 5!      $��Klient Tel:��6   $
��Bold"�5"      �� 5#      $��Adresa:��6   $
��Bold"�5$      5%      �� 5&      .+�5'      �� ��6   �� *Jmeno+�� 5(      ��6   �� *
Prijmeni+�� 5)      ��6   �� *CisloPrukazu+�� 5*      ��6   �� *KlientEmail+�� 5+      ��6   �� *TelefoniCislo+�� 5,      ��6   �� *Adresa+5-      5.      �� 5/   
   $��1��6   $��10q�50      .+�51      �� $��Seznam Účtů:��6   $
��Bold"�52      �� 53      ��6   �� *Ucty+��54      �� 55      .R�56      �� .+�57      �� ��6(   �� *	CisloUctu+�� 58      ��6(   �� *Nazev+�� 59      ��6(   �� *PopisStatus+5:      5;      5=      5>      5?      5@      