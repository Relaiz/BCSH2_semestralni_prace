   M S B A M L   `   `   `  ����O  KBCSH2-BDAS2-SemPrace, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:BCSH2_BDAS2_SemPrace.ViewBCSH2_BDAS2_SemPrace.View  5   	   /    )BCSH2_BDAS2_SemPrace.View.BankomatyWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:BCSH2_BDAS2_SemPrace.View     5   	     � Title$  	Bankomaty��5   	   $	��450��5	   	   $	��800��5
   	    � 	Resources 5         ButtonStyle �� 
TargetType �� Property �� Value��%W  &	        �� 5   
   " ��5      .�5      ��  � ��$ 0,10,0,0q�5      �� 5       � ��$ 10q�5      �� 5       � ��
 ��ح�5      �� 5       � ��
 �����5      �� 5       � ��$
 Bold"�5      �� 5       � ��
 ��  �5      �� 5       � ��$ 2q�5      5      5      .��5      �  � RowDefinitions 5   
   �� 5       $
��Auto �6   �� 5!      $��* �6   .+�5$   
   �� $��0��6    �� Orientation$ 
Horizontal=�5%      $
��Left=�5&      $��10q�5'      .+�5(      �� $��Pridat Bankomat��6   #��[  5)      $��5q�5+      ��5*      �� *PridatBankomatCommand+5+      �� 5,      $��Odebrat Bankomat��6   #��[  5-      $��5q�5/      ��5.      �� *OdebratBankomaCommand+5/      �� 50      $��Upravit Bankomat��6   #��[  52      $��5q�53      ��51      �� *UpravitBankomaCommand+53      54      �� 55   
   -   5         ��Name$ bankomatyList��55      $��1��58      $��10q�59      ��56      �� *listBankomatu+ �� SelectedItem 57      �� *AktualniBankomat+	 �� View	 5:      �� 5;      .P�5<      �� $��ID��6%   
 �� Width$
 50��5>   %    �� DisplayMemberBinding 5=   %   �� *
IdBamkomat+5>   %   �� 5?      $��Nazev��6%   $	
 150��5A   %    5@   %   �� *Nazev+5A   %   �� 5B      $��Adresa��6%   $	
 200��5D   %    5C   %   �� *Adresa+5D   %   �� 5E      $��Status��6%   $	
 100��5G   %    5F   %   �� *StatusPopis+5G   %   5H      5J      �� 5K   
   $��Zavrit��6   #��[  5M      $��422,15,251,15q�5N      ��5L      �� *ZavritCommand+5N      5O      5P      