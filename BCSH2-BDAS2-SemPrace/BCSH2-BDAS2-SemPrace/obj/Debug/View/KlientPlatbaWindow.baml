   M S B A M L   `   `   `  ����  BCSH2-BDAS2-SemPrace2    ,BCSH2_BDAS2_SemPrace.View.KlientPlatbaWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  KlientPlatbaWindow��5   	   $	��300��6$   $	��400��61    � DataContext 5   	   �� *KlientPlatbaViewModelInstance+.��5	      � .+�5
   
   �� -   5        $��UctyComboBox��5
      $��Stretch=�6'   $	��Top=�6E   $��10q�6]    �� DisplayMemberPath$ Nazev��6�   ��6i   �� *
UctyList+ �� SelectedItem 6�   �� *SelectedUcet+�� 5   
   -   5        $��CisloUctuTextBox��5      $	��Top=�6*   $��200,40,40,0q�6B   $��27��6s   ��6W   �� *
UcetNumber+6s   �� 5   
   -   5        $��CastkaTextBox��5      $	��Top=�6'   $��200,74,40,0q�6?   $��28��6l   ��6T   �� *Castka+6l   �� 5   
   -   5        $��NazevTextBox��5      $	��Top=�6&   $��200,107,40,0q�6>   $��26��6k   ��6T   �� *Nazev+6k   �� 5   
   $��Zaplatit��6   $��Center=�6$   $	��Top=�6A   $��	0,234,0,0q�6Y   $��40��6�   $	��380��6�   ��6l   �� *VytvoritPlatbuCommand+6�   �� 5   
   $��Do uctu:��6   $
��Left=�6#   $��
140,40,0,0q�6>   $	��Top=�6R   $��27��6j   �� 5   
   $��Castka:��6   $
��Left=�6"   $��
144,75,0,0q�6=   $	��Top=�6Q   $��27��6i   �� 5   
   $��Nazev Operace:��6   $
��Left=�6)   $��102,107,0,0q�6D   $	��Top=�6Y   $��27��6q   5      5      