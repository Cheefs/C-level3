   M S B A M L   `   `   `  ����  MailSenderWPF&     MailSenderWPF.Viev.EmailInfoView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      b ^GalaSoft.MvvmLight.Platform, Version=5.4.1.0, Culture=neutral, PublicKeyToken=5f873c45e98af8a1+mvvm http://www.galasoft.ch/mvvmlight  5      "ignorehttp://www.ignore.com  5      -   5          >�Name$  usContEmailinfo��5      $
��Auto��5
   L   $
��Auto��6Z    � DataContext 6   �� *Main+   Locator �� Source# [  .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .  (System.Windows.Interactivity.Interaction   Triggers 5   
   /  )System.Windows.Interactivity.EventTrigger  5         	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      /  )GalaSoft.MvvmLight.Command.EventToCommand     Command 6&   �� *ReadAllCommand+5      .+�5   
   �� $
��Wrap=�6    �� TextAlignment$ Center=�5      $-��'Получатели сообщений��6+   $
��Bold"�6G   �� 5   
   -   5        	 ��Name$	 btnloadallemails��5      $'��!Отобразить данные��6+   $��1��5      $
��Bold"�5      $��22��5   Z   $��
152,10,0,3q�6f   $
��Left=�6z   $	��180��6�   ��6   �� *ReadAllCommand+��65   �� *	GetName+6�   &   System.Windows.Controls.DataGrid  5   
   -   5        
  Name$
 dgEmails��5      $��2��6%      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 5      $��	0,10,0,28q�6#    J� RenderTransformOrigin$ 0.5,0.5&�66      
IsReadOnly . ��5   2   �� *Emails+5!      �� 5"   
   $
��Left=�6   $��1��6/   $
��Wrap=�5#      $��Search By Name:��6%   $	��Top=�6<   $	��136��5$      $��41��6   $��
AR DESTINE)�6)   $��2��6A   $��16(�6R   $��	10,10,0,0q�6`   $
��Bold"�6s   
���   ���5%      �� 5&       j� TileMode$ FlipX=�6   5(      �� 5)   
   $
��Left=�6   $
��Auto��6-   $��	337,7,0,0q�6;   $��1��5*       �� TextWrapping$
 Wrap=�6"   $	��Top=�6q   $	��235��6�   
���   ���66   �� *Name+ �� UpdateSourceTrigger$ PropertyChanged=�.��6�   ��5+      �� 5,       �� EndPoint$	 0,1&�6&    �� 
StartPoint$	 0,0&�65   .��5-      �  � Color$ 	#FF878585��6#    � Offset$ 0Q�65   � 5.      $ White��6#   $ 1Q�61   5/      .��50   "   51      53      54      