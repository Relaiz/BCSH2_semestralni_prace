   M S B A M L   `   `   `  ����O  KBCSH2-BDAS2-SemPrace, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:BCSH2_BDAS2_SemPrace.ViewBCSH2_BDAS2_SemPrace.View  5   	   ,    &BCSH2_BDAS2_SemPrace.View.KlientWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:BCSH2_BDAS2_SemPrace.View     5   	     � Title$  KlientWindow��5   	   $	��500��5	   	   $	��850��5
   	    � 
ResizeMode$ NoResize=�5   	    � 	Resources 5         ButtonStyle �� 
TargetType �� Property �� Value  
LabelStyle  ListBoxStyle��%�  &	        &	 M    &	 �    �� 5   
   " ��5      .�5      ��  � ��$ 0,10,0,0q�5      �� 5       � ��$ 10q�5      �� 5       � ��
 ��ح�5      �� 5       � ��
 �����5      �� 5       � ��$
 Bold"�5      �� 5       � ��
 ��  �5      �� 5       � ��$ 2q�5      5      �� 5   
   " ��5      .�5       ��  � �� �	#FFE2DDDD5!      5"      �� 5#   
   " ��5$      5&      5'      .��5)      �  � RowDefinitions 5*   
   �� 5+      $
��Auto �6   �� 5,      $
��Auto �6   $��337.96��5-      �� 5.      $
��Auto �6   �� 5/      $
��Auto �6    � ColumnDefinitions 51   
   �� 52      �� 53      $
��Auto �6   $��532.037��54      �� 55      $
��Auto �6   $��0��56      .+�5:   
   �� -   5         ��Name$ NameTextBox��5:      $��10,10,165,338q�5<      $��0��5=      $��0��5>      $��2��5?      ��5;      �� *Name+	 �� UpdateSourceTrigger$	 PropertyChanged=�5?      �� 5@   
   -   5        $ SurnameTextBox��5@      $��175,10,0,338q�5B      $��0��5C      $��0��5D      $��2��5E      ��5A      �� *	Surname+$	 PropertyChanged=�5E      �� 5F   
   -   5        
 ��Name$
 klientEmail��5F      #��[ 5H      $��1��5I      $��0��5J      $��10,77,0,270q�5K      ��5G      �� *Email+5K      �� 5L   
   -   5        $
 klientZamestnanec��5L      #��[ 5N      $��1��5O      $��0��5P      $��10,109,0,240q�5Q      ��5M      �� *Zamestnanec+5Q      �� 5R   
   -   5        $ 	klientTel��5R      $��1��5T      $��0��5U      $��10,42,0,302q�5V      ��5S      �� *	Telephone+$	 PropertyChanged=�5V      �� 5W   
   -   5        $
 klientZustatek��5W      #��[ 5Y      $��1��5Z      $��0��5[      $��179,146,0,202q�5\      $��18(�5]      $��SimSun)�5^      $
��Bold"�5_      ��5X      �� *
Zustatek+5_      �� 5`   
   -   5        $
 klientZustatekBlock��5`      #��[ 5b      $��1��5c      $��0��5d      $��179,190,0,158q�5e      $��18(�5f      $��SimSun)�5g      $
��Bold"�5h      ��5a      �� *ZustatekBlocked+5h      �� 5i   
   $��Mesicni platby��6   $
��Left=�5j      $��
18,227,0,0q�5k      $��1��5l      $	��Top=�5m      $��Sitka Banner)�5n      $��16(�5o      �� 5p   
   -   5        $
 klientMesicniPlatby��5p      #��[ 5r      $��1��5s      $��0��5t      $��179,231,0,117q�5u      $��18(�5v      $��SimSun)�5w      $
��Bold"�5x      ��5q      �� *MesicniPlatby+5x      �� 5{   
   -	   5         ��Name$ accountsList��5{      #��[ 5~      $��10,42,10,10q�5      $��0��5�      $��1��5�      $��2��5�      ��5|      �� *ListOfKlientUcty+ �� SelectedItem 5}      �� *SelectedUcet+ �� View 5�      �� 5�      .P�5�      �� $
��Ucet��6%    �� Width$	 150��5�   %    �� DisplayMemberBinding 5�   %   �� *	CisloUctu+�� 5�      $��Nazev��6%   $	 100��5�   %    5�   %   �� *Nazev+�� 5�      $��Status��6%   $	 100��5�   %    5�   %   �� *PopisStatus+5�      5�      �� 5�   
   $��Tranzakce Z Uctu��6   #��[  5�      $��10,10,410,10q�5�      $��2��5�      $��1��5�      ��5�      �� *TranzakceZUctuCommand+5�      �� 5�   
   $��Zalozit Ucet��6   #��[  5�      $��380,10,42,10q�5�      $��2��5�      $��1��5�      ��5�      �� *ZalozitNovyCommand+5�      �� 5�   
   $��Detail Uctu��6   #��[  5�       J� RenderTransformOrigin$ -0.251,0.057&�5�      $��2��5�      $��1��5�      $��194,10,228,10q�5�      ��5�      �� *DetailUctuCommand+5�      �� 5�   
   $��Ulozit Data��6   #��[  5�      $ -0.251,0.057&�5�      $��1��5�      $��183,275,10,59q�5�      ��5�      �� *SaveDataCommand+5�      �� 5�   
   -
   5         ��Name$ btnExit��5�      #��[  5�      $
��Exit��5�      $��
10,10,0,10q�5�      $��2��5�      $
��Left=�5�      $	��108��5�      ��5�      �� *ExitKlientCommand+5�      �� 5�   
   -   5        $
 klientAccNumsTitle��5�      $��Vaše účty:��5�      #��[ 5�      $��10,10,10,338q�5�      $��1��5�      $��1��5�      �� 5�   
   $��Zustatek��6   $
��Left=�5�      $��105,144,0,0q�5�      $��1��5�      $	��Top=�5�      $��Sitka Banner)�5�      $��16(�5�      �� 5�   
   $��Blokovane Prostredky��6   $
��Left=�5�      $��
18,188,0,0q�5�      $��1��5�      $	��Top=�5�      $��Sitka Banner)�5�      $��16(�5�      �� 5�   
   -   5        $ 	btnPlatba��5�      #��[  5�      $��Nova Platba��5�      $��179,10,0,10q�5�      $��2��5�      $
��Left=�5�      $	��108��5�      ��5�      �� *VytvoritPlatbuCommand+5�      5�      5�      