   M S B A M L   `   `   `  ����E  AMilionario, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Milionario
Milionario  5   	       Milionario.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Milionario     5   	   -   5          �Name$  window��5   	    � Title$! Chi Vuol Essere Milionario?��5   	   $	��450��6-   $	��800��6:   ��5	      �� 5
   
    �� ImageSource$ .\img\logo.jpg��6   .��5      � .+�5   
      Milionario.CTimer  -   5          Name$ Timer��5      $��Center=�6&   $��Bottom=�6C   $��0,0,0,43q�6^   $y�Hidden=�6p   r� 5   
   -   5        $��mediaPlayer��5       r� Stretch$
 Fill=�6N    r� UnloadedBehavior$ Manual=�6]    r� ScrubbingEnabled$
 True��6w   �� 5   
   -   5         ��Name$ lblNome��5      $��Nome Giocatore:��6!   $	��Top=�6;   
�������$��20(�6f   $
��Bold"�6t   $��Right=�6�   $��
0,151,30,0q�6�   $y�Hidden=�6�   �� 5   
   -   5         ��Name$ txtNome��5      $��Right=�6#   $��
0,199,55,0q�6?   $	��Top=�6S   $	��120��6k   $y�Hidden=�6w   �� 5   
   -   5        	 ��Name$	 btnGioca��5      $��Gioca��6#   $��Right=�63   $��
0,230,91,0q�6O   $	��Top=�6c   $��16(�6{   $
��Bold"�6�   $��Italic$�6�   ���	#FFD3B054$y�Hidden=�6�   �� 5   
   -   5        $ 
txtDomanda��5      $
��Ciao��6$   $��	0,0,0,120q�63   $��Bottom=�6F   $��Center=�6a   $��Center=�6�   
�������$��14(�6�   $��Italic$�6�   $��50��6  $y�Hidden=�6  ��6�   �� *Width+
 �� ElementName$
 window����5      �� 5      $ .\img\domanda.png��6   5      �� 5   
   -   5        $	 btnRA��5      $��Button��6    $
��Left=�61   $��	50,0,0,70q�6L   $��Bottom=�6_   $��40��6z   $	��350��6�   
������ $��Center=�6�   $��Center=�6�   
�������$y�Hidden=�6  ��5      �� 5      $ .\img\risposta.png��6   5      �� 5   
   -	   5        $	 btnRC��5      $��Button��6    $
��Left=�61   $��	50,0,0,15q�6L   $��Bottom=�6_   $��40��6z   $	��350��6�   
������ $��Center=�6�   $��Center=�6�   
�������$y�Hidden=�6  ��5      �� 5      $ .\img\risposta.png��6   5       �� 5!   
   -
   5        $	 btnRB��5!      $��Button��6    $��Right=�61   $��	0,0,50,70q�6M   $��Bottom=�6`   $��40��6{   $	��350��6�   
������ $��Center=�6�   $��Center=�6�   
�������$y�Hidden=�6  ��5"      �� 5#      $ .\img\risposta.png��6   5%      �� 5&   
   -   5        $	 btnRD��5&      $��Button��6    $��Right=�61   $��	0,0,50,15q�6M   $��Bottom=�6`   $��40��6{   $	��350��6�   
������ $��Center=�6�   $��Center=�6�   
�������$y�Hidden=�6  ��5'      �� 5(      $ .\img\risposta.png��6   5*      �� 5+   
   -   5        $	 btnSalta��5+      $��Salta��6#   $��Right=�63   $��Bottom=�6O   ���	#FFD3B054 K� ZIndex$ 1��6�   �� 5,   
   -   5         ��Name$ lstDiff��5,      $
��Left=�6#   $	��Top=�6>   $��	0,0,0,170q�6V   ���	#FFD3B054
������ $��Center=�6�   
������ $	��180��6�   �� 5-   
   -   5        $ txtPunteggio��5-      $��Right=�6(   $��0,0,0,0q�6D    �� TextWrapping$
 Wrap=�6U   $��0��6i   $	��Top=�6r   $	��120��6�    �� TextAlignment$ Center=�6�   $y�Hidden=�6�   �� 5.   
   -   5         ��Name$ imagePlayer��5.      $
��Left=�6%   $��0,0,0,0q�6@   $	��Top=�6Q   $y�Hidden=�6i   $ -1��6}   $
��Fill=�6�   5/      50      