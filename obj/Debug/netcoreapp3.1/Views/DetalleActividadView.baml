   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   fCclr-namespace:MahApps.Metro.Controls.Dialogs;assembly=MahApps.MetroMahApps.Metro.Controls.Dialogs  5   	   D @kalum2021, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:kalum2021	kalum2021 5	   	   *   $kalum2021.Views.DetalleActividadView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Kmah;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   VDialogCclr-namespace:MahApps.Metro.Controls.Dialogs;assembly=MahApps.Metro     5   	   )localclr-namespace:kalum2021     5	   	   -   5        $��FrmActividad��5   `   $	��350��6"   $	��450��6/     � WindowStartupLocation$  CenterScreen=�6;   8   2MahApps.Metro.Controls.Dialogs.DialogParticipation   Register 5   	   ��  � Title 5   	   �� *Titulo+.��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��60 �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��Nombre de Actividad��6   $��0��6.   $��0��6;   �� 5   
   $��0,5,5,0q�6   $��0��6#   $��1��61    �� 
IsReadOnly$ False��6g   ��6A   �� *ValorNombreActividad+6g   �� 5   
   $
��Nota��6   $��1��6   $��0��6,   �� 5   
   $��0,5,5,0q�6   $��1��6#   $��1��61   $ False��6e   ��6A   �� *ValorNotaActividad+6e   �� 5    
   $��Fecha de Entrega��6   $��3��6+   $��0��68   (  "System.Windows.Controls.DatePicker  5!   
   $��3��6   $��1��6"   $��0,5,5,0q�62      IsTodayHighlighted$
 true��6m      DisplayDateStart 1/7/20216�      DisplayDate 6C   �� *ValorFechaEntrega+6�   �� 5"   
   $��Fecha de Postergación��6   $��4��60   $��0��6=     5#   
   $��4��6   $��1��6"   $��0,5,5,0q�62    1/7/20216r   $
 true��6�    6C   �� *ValorFechaPostergacion+6�   �� 5$   
   $��Módulo��6   $��5��6!   $��0��6.   �� 5%   
   	 �� 
IsEditable$
	 true��6   
 �� 
IsReadOnly$

 true��6%   $��0,5,5,0q�67   $��5��6H   $��1��6V   ��6f   �� *
Seminarios+ �� Text 6�   �� *SeminarioDefinido+ �� SelectedItem 6�   �� *SeminarioSeleccionado+�� 5&   
   $��Estado��6   $��6��6!   $��0��6.   �� 5'   
   $��0,5,5,0q�6   $��6��6#   $��1��61   $ False��6^   ��6A   �� *ValorEstado+6^   ]� 5(   
   $��15q�6   $��8��6    $��2��6/   .+�5)      �� $	��125��6   $��Center=�6!   $��Guardar��6>   ��6Q   �� *	Instancia+��6p   ��  �� ElementName$ FrmActividad��5*      5+      5,      