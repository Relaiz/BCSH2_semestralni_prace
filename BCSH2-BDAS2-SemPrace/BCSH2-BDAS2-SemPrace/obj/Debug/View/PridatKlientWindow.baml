   M S B A M L   `   `   `  ����O  KBCSH2-BDAS2-SemPrace, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:BCSH2_BDAS2_SemPrace.ViewBCSH2_BDAS2_SemPrace.View  5   	   O,clr-namespace:BCSH2_BDAS2_SemPrace.ViewModelBCSH2_BDAS2_SemPrace.ViewModel  5   	   2    ,BCSH2_BDAS2_SemPrace.View.PridatKlientWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:BCSH2_BDAS2_SemPrace.View     5   	   ;vm,clr-namespace:BCSH2_BDAS2_SemPrace.ViewModel     5   	     � Title$  Pridani Noveho Klienta��5	   	   $	��600��5
   	   $	��600��5   	    � 
ResizeMode$ NoResize=�5   	    � DataContext 5      :   4BCSH2_BDAS2_SemPrace.ViewModel.PridatKlientViewModel  5   
    � 	Resources 5         ButtonStyle �� 
TargetType �� Property �� Value��%W  &	        �� 5   
   " ��5      .�5      ��  � ��$ 0,10,0,0q�5      �� 5       � ��$ 10q�5      �� 5       � ��
 ��ح�5      �� 5       � ��
 �����5      �� 5       � ��$
 Bold"�5      �� 5       � ��
 ��  �5      �� 5       � ��$ 2q�5       5!      5$      .��5&      �  � RowDefinitions 5'   
   �� 5(      $
��Auto �6   �� 5)      $
��Auto �6   �� 5*      $
��Auto �6   �� 5+      $
��Auto �6   �� 5,      $
��Auto �6   �� 5-      $
��Auto �6   �� 5.      $
��Auto �6   �� 5/      $
��Auto �6   �� 50      $
��Auto �6   �� 51      $
��Auto �6   �� 52      $
��Auto �6   �� 53      $
��Auto �6   .+�57   
   �� $��Jmeno:��6   $��0��58      $��10q�59      �� 5:   
   -   5         ��Name$ txtFirstName��5:      $��0��5;      $��
150,0,10,0q�5=      ��5<      �� *Name+	 �� UpdateSourceTrigger$	 PropertyChanged=�
 �� ValidatesOnDataErrors
 . 5=      �� 5@   
   $��	Prijmeni:��6   $��1��5A      $��10q�5C      �� 5D   
   -   5        $ txtLastName��5D      $��1��5E      $��
150,0,10,0q�5G      ��5F      �� *
Lastname+$	 PropertyChanged=�
 . 5G      �� 5J   
   $��Cislo Prukazu:��6   $��2��5K      $��10q�5L      �� 5M   
   -   5        $ txtCisloPrukazu��5M      $��2��5N      $��
150,0,10,0q�5P      ��5O      �� *CisloPrukaz+$	 PropertyChanged=�
 . 5P      �� 5T   
   $��Email:��6   $��3��5U      $��10q�5V      �� 5W   
   -   5        $ txtEmail��5W      $��3��5X      $��
150,0,10,0q�5Y      ��5Z      �� *Email+$	 PropertyChanged=�
 . �� 5^   
   $��Telefonni Cislo:��6   $��4��5_      $��10q�5`      �� 5a   
   -   5        $ txtTelCislo��5a      $��4��5c      $��
150,0,10,0q�5d      ��5b      �� *
TelCislo+$	 PropertyChanged=�
 . 5d      �� 5g   
   $��Adresa - Stat:��6   $��5��5h      $��10q�5i      �� 5j   
   -   5        $ txtStat��5j      $��5��5l      $��
150,0,10,0q�5m      ��5k      �� *Stat+$	 PropertyChanged=�
 . 5m      �� 5o   
   $��Adresa - Mesto:��6   $��6��5p      $��10q�5q      �� 5r   
   -   5        $ txtMesto��5r      $��6��5t      $��
150,0,10,0q�5u      ��5s      �� *Mesto+$	 PropertyChanged=�
 . 5u      �� 5w   
   $��Adresa - Ulice:��6   $��7��5x      $��10q�5y      �� 5z   
   -   5        $ txtUlice��5z      $��7��5|      $��
150,0,10,0q�5}      ��5{      �� *Ulice+$	 PropertyChanged=�
 . 5}      �� 5   
   $��Adresa - Cislo Popisne:��6   $��8��5�      $��10q�5�      �� 5�   
   -	   5        $ txtCislo_popisne��5�      $��8��5�      $��
150,0,10,0q�5�      ��5�      �� *CisloPopisne+$	 PropertyChanged=�
 . 5�      �� 5�   
   $��Adresa - PSC:��6   $��9��5�      $��10q�5�      �� 5�   
   -
   5        $ txtPSC��5�      $��9��5�      $��
150,0,10,0q�5�      ��5�      �� *PSC+$	 PropertyChanged=�
 . 5�      �� 5�   
   $��Heslo (abcde - standart):��6   $��10��5�      $��10q�5�      G� 5�   
   -   5         G�Name$ txtPassword��5�       G� Password$ abcde��5�      $��10��5�      $��
150,0,10,0q�5�      �� 5�   
   $��Pridat��6   #��[  5�      $��11��5�      $
��Left=�5�      $��Center=�5�      $��	219,0,0,0q�5�      $	��111��5�      {�5�      �� *IsDataValid+��5�      �� *CreateClientCommand+5�      5�      