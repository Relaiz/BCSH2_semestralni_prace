   M S B A M L   `   `   `  ����O  KBCSH2-BDAS2-SemPrace, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:BCSH2_BDAS2_SemPrace.ViewModelBCSH2_BDAS2_SemPrace.ViewModel  5   	   5    /BCSH2_BDAS2_SemPrace.View.UpravitBankomatWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   >local,clr-namespace:BCSH2_BDAS2_SemPrace.ViewModel     5   	     � Title$  UpravitBankomatWindow��5   	   $	��204��5	   	   $	��300��5
   	    � 
ResizeMode$ NoResize=�5   	    � 	Resources 5         ButtonStyle �� 
TargetType �� Property �� Value��%W  &	        �� 5   
   " ��5      .�5      ��  � ��$ 0,10,0,0q�5      �� 5       � ��$ 10q�5      �� 5       � ��
 ��ح�5      �� 5       � ��
 �����5      �� 5       � ��$
 Bold"�5      �� 5       � ��
 ��  �5      �� 5       � ��$ 2q�5      5      5      .��5      � $��0,0,0,25q�6    � RowDefinitions 5    
   �� 5!      $
��Auto �6   �� 5"      $
��Auto �6   �� 5#      $
��Auto �6   .+�5'   
   �� $��0��6    �� Orientation$ 
Horizontal=�5(      $��10q�5)      .+�5*      �� $��Nazev:��6   $	��100��5+      $��Center=�5,      �� 5-      $	��180��5.      ��5-      �� *Nazev+ �� UpdateSourceTrigger$ PropertyChanged=�5.      5/      �� 50   
   $��1��6   $ 
Horizontal=�51      $��10q�52      .+�53      �� $��Status:��6   $	��100��54      $��Center=�55      �� 56      	 �� DisplayMemberPath$	 Popis��57      
 �� SelectedValuePath$
 IdStatus��58      $	��180��5:      ��56      �� *	Statusy+ �� SelectedValue 59      �� *SelectedStatusId+5:      5;      �� 5=   
   $��2��6   $ 
Horizontal=�5>      $��10q�5?      $��Center=�5@      .+�5A      �� $��Upravit Bankomat��6   #��[  5C      $	��150��5D      ��5B      �� *UpravitCommand+5D      5E      5F      5G      