   M S B A M L   `   `   `  ����O  KBCSH2-BDAS2-SemPrace, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:BCSH2_BDAS2_SemPrace.ViewBCSH2_BDAS2_SemPrace.View  5   	   -    'BCSH2_BDAS2_SemPrace.View.PobockyWindov   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:BCSH2_BDAS2_SemPrace.View     5   	     � Title$  Pobocky��5   	   $	��450��5	   	   $	��800��5
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
��Left=�5&      $��10q�5'      .+�5(      �� $��Pridat Pobocku��6   #��[  5)      $��5q�5+      ��5*      �� *PridatPobockuCommand+5+      �� 5,      $��Odebrat Pobocku��6   #��[  5-      $��5q�5/      ��5.      �� *OdebratPobockuCommand+5/      �� 50      $��Upravit Pobocku��6   #��[  52      $��5q�53      ��51      �� *UpravitPobockuCommand+53      54      �� 55   
   -   5         ��Name$ pobockyList��55      $��1��58      $��10q�59      ��56      �� *	Pobocky+ �� SelectedItem 57      �� *AktualniPobocka+	 �� View	 5:      �� 5;      .P�5<      �� $��ID��6%   
 �� Width$
 50��5>   %    �� DisplayMemberBinding 5=   %   �� *	IdPobocka+5>   %   �� 5?      $��Nazev��6%   $	
 150��5A   %    5@   %   �� *Nazev+5A   %   �� 5B      $��Telefonni Cislo��6%   $	
 150��5D   %    5C   %   �� *TelefoniCislo+5D   %   �� 5E      $��Adresa��6%   $	
 200��5G   %    5F   %   �� *Adresa+5G   %   �� 5H      $��Status��6%   $	
 100��5J   %    5I   %   �� *StatusPopis+5J   %   5K      5M      �� 5N   
   $��Zavrit��6   #��[  5P      $��422,15,251,15q�5Q      ��5O      �� *ZavritCommand+5Q      5R      5S      