   M S B A M L   `   `   `  ����O  KBCSH2-BDAS2-SemPrace, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:BCSH2_BDAS2_SemPrace.ViewModelBCSH2_BDAS2_SemPrace.ViewModel  5   	   4    .BCSH2_BDAS2_SemPrace.View.UpravitPobockuWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   >local,clr-namespace:BCSH2_BDAS2_SemPrace.ViewModel     5   	     � Title$  Upravit Pobocku��5   	   $	��250��5	   	   $	��300��5
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
��Auto �6   �� 5$      $
��Auto �6   .+�5(   
   �� $��0��6    �� Orientation$ 
Horizontal=�5)      $��10q�5*      .+�5+      �� $��Nazev:��6   $	��100��5,      $��Center=�5-      �� 5.      $	��180��5/      ��5.      �� *Nazev+ �� UpdateSourceTrigger$ PropertyChanged=�5/      50      �� 51   
   $��1��6   $ 
Horizontal=�52      $��10q�53      .+�54      �� $��Tel Pobocky:��6   $	��100��55      $��Center=�56      �� 57      $	��180��58      ��57      �� *TelefoniCislo+$ PropertyChanged=�58      59      �� 5:   
   $��2��6   $ 
Horizontal=�5;      $��10q�5<      .+�5=      �� $��Status:��6   $	��100��5>      $��Center=�5?      �� 5@      	 �� DisplayMemberPath$	 Popis��5A      
 �� SelectedValuePath$
 IdStatus��5B      $	��180��5D      ��5@      �� *	Statusy+ �� SelectedValue 5C      �� *SelectedStatusId+5D      5E      �� 5G   
   $��3��6   $ 
Horizontal=�5H      $��10q�5I      $��Center=�5J      .+�5K      �� $��Upravit Pobocku��6   #��[  5M      $	��150��5N      ��5L      �� *UpravitCommand+5N      5O      5P      5Q      