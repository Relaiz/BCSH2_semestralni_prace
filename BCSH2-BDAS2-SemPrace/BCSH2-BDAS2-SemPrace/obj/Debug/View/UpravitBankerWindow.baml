   M S B A M L   `   `   `  ����O  KBCSH2-BDAS2-SemPrace, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:BCSH2_BDAS2_SemPrace.ViewBCSH2_BDAS2_SemPrace.View  5   	   3    -BCSH2_BDAS2_SemPrace.View.UpravitBankerWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:BCSH2_BDAS2_SemPrace.View     5   	     � Title$  Upravit Bankere��5   	   $	��350��5	   	   $	��350��5
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
��Auto �6   �� 5'      $
��Auto �6   .+�5*   
   �� $��0��6    �� Orientation$ 
Horizontal=�5+      $��10q�5,      .+�5-      �� $��Jméno:��6   $	��100��5.      $��Center=�5/      �� 50      $	��200��51      ��50      �� *Jmeno+ �� UpdateSourceTrigger$ PropertyChanged=�51      52      �� 54   
   $��1��6   $ 
Horizontal=�55      $��10q�56      .+�57      �� $��Příjmení:��6   $	��100��58      $��Center=�59      �� 5:      $	��200��5;      ��5:      �� *
Prijmeni+$ PropertyChanged=�5;      5<      �� 5@   
   $��2��6   $ 
Horizontal=�5A      $��10q�5B      .+�5C      �� $��E-mail:��6   $	��100��5D      $��Center=�5E      �� 5F      $	��200��5G      ��5F      �� *Email+$ PropertyChanged=�5G      5H      �� 5J   
   $��3��6   $ 
Horizontal=�5K      $��10q�5L      .+�5M      �� $��Telefonní číslo:��6   $	��100��5N      $��Center=�5O      �� 5P      $	��200��5Q      ��5P      �� *
TelCislo+$ PropertyChanged=�5Q      5R      �� 5S   
   $��4��6   $ 
Horizontal=�5T      $��10q�5U      .+�5V      �� $��Status:��6   $	��100��5W      $��Center=�5X      �� 5Y      	 �� DisplayMemberPath$	 Popis��5Z      
 �� SelectedValuePath$
 IdStatus��5[      $	��200��5]      ��5Y      �� *	Statusy+ �� SelectedValue 5\      �� *SelectedStatusId+5]      5^      �� 5_   
   $��5��6   $ 
Horizontal=�5`      $��10q�5a      .+�5b      �� $��	Pobočka:��6   $	��100��5c      $��Center=�5d      �� 5e      $	 Nazev��5f      $
 	IdPobocka��5g      $	��200��5i      ��5e      �� *	Pobocky+ 5h      �� *SelectedPobockaId+5i      5j      �� 5k   
   $��6��6   $ 
Horizontal=�5l      $��10q�5m      $��Center=�5n      .+�5o      �� $��Upravit Bankere��6   #��[  5q      $	��150��5r      ��5p      �� *UpravitCommand+5r      5s      5t      5u      