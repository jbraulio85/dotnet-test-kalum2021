   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   fCclr-namespace:MahApps.Metro.Controls.Dialogs;assembly=MahApps.MetroMahApps.Metro.Controls.Dialogs  5   	   D @kalum2021, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:kalum2021	kalum2021 5	   	      kalum2021.Views.ClaseView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Kmah;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   VDialogCclr-namespace:MahApps.Metro.Controls.Dialogs;assembly=MahApps.Metro     5   	   )localclr-namespace:kalum2021     5	   	   -   5        $��FrmClase��5   `   $	��350��6"   $	��450��6/     � WindowStartupLocation$  CenterScreen=�6;   8   2MahApps.Metro.Controls.Dialogs.DialogParticipation   Register 5   	   ��  � Title 5   	   �� *Titulo+.��5      �  � RowDefinitions 5   
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
   �� $��Descripción��6   $��0��6&   $��0��63   �� 5   
   $��0,5,5,0q�6   $��0��6#   $��1��61    �� 
IsReadOnly$ False��6c   ��6A   �� *ValorDescripcion+6c   �� 5   
   $��Ciclo��6   $��1��6    $��0��6-   �� 5   
    �� 
IsEditable$
 true��6   $��0,5,5,0q�6%   $��1��66   $��1��6D   ��6T   �� *Ciclos+ �� SelectedValue 6s   �� *
ValorCiclo+ �� Text 6�   �� *
ValorCiclo+�� 5    
   $��Cupo Mínimo��6   $��2��6&   $��0��63   �� 5!   
   $
 true��6   $��0,5,5,0q�6%   $��2��66   $��1��6D   ��6T   �� *
CupoMinimo+ 6w   �� *ValorCupoMinimo+ 6�   �� *ValorCupoMinimo+�� 5"   
   $��Cupo Máximo��6   $��3��6&   $��0��63   �� 5#   
   $
 true��6   $��0,5,5,0q�6%   $��3��66   $��1��6D   ��6T   �� *
CupoMaximo+ 6w   �� *ValorCupoMaximo+ 6�   �� *ValorCupoMaximo+�� 5$   
   $��Carrera��6   $��4��6"   $��0��6/   �� 5%   
   $
 true��6   	 �� 
IsReadOnly$
	 true��6%   $��0,5,5,0q�67   $��4��6H   $��1��6V   ��6f   �� *CarrerasTecnicas+ 6�   �� *CarreraTecnicaDefinida+
 �� SelectedItem
 6�   �� *CarreraTecnicaSeleccionada+�� 5&   
   $��
Instructor��6   $��5��6%   $��0��62   �� 5'   
   $
 true��6   $
	 true��6%   $��0,5,5,0q�67   $��5��6H   $��1��6V   ��6f   �� *Instructores+ 6�   �� *InstructorDefinido+
 6�   �� *InstructorSeleccionado+�� 5(   
   $��Salón��6   $��6��6    $��0��6-   �� 5)   
   $
 true��6   $
	 true��6&   $��0,5,5,0q�68   $��6��6I   $��1��6W   ��6g   �� *	Salones+ 6�   �� *SalonDefinido+
 6�   �� *SalonSeleccionado+�� 5*   
   $��Horario��6   $��7��6"   $��0��6/   �� 5+   
   $
 true��6   $
	 true��6%   $��0,5,5,0q�67   $��7��6H   $��1��6V   ��6f   �� *
Horarios+ 6�   �� *HorarioDefinido+
 6�   �� *HorarioSeleccionado+]� 5,   
   $��15q�6   $��8��6    $��2��6/   .+�5-      �� $	��125��6   $��Center=�6!   $��Guardar��6>   ��6Q   �� *	Instancia+��6p   ��  �� ElementName$ FrmClase��5.      5/      50      