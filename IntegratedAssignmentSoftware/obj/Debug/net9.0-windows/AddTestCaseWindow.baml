   M S B A M L   `   `   `  ����W  SIntegratedAssignmentSoftware, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:IntegratedAssignmentSoftwareIntegratedAssignmentSoftware  5   	   4    .IntegratedAssignmentSoftware.AddTestCaseWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=4.7.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   6local*clr-namespace:IntegratedAssignmentSoftware  5   	     � Title$  AddTestCaseWindow��5	   	   ���#101010$	��500��68   $	��375��6E   .��5
      �  � ColumnDefinitions 5   
   �� 5      $��20 �6   6,   �� 5      $
��auto �6   6.   �� 5      $��* �6   6+   �� 5      $��20 �6   6,    � RowDefinitions 5   
   �� 5      $��20 �6   6*   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $��* �6   6)   �� 5      $��20 �6   6*   .+�5   
   �� 
�������$��10q�6'   $��1��63   $��1��6@   $-��'Type in the input or choose a txt file:��6P   6�   �� 5    
   -   5         ��Name$ ChooseInputFileButton��5       $��10q�6T   $��Choose file��6`   $��1��6v   $��2��6�   6�   �� 5!   
   -   5         ��Name$ InputTextBox��5!      $��10q�6(   $��2��64   $��1��6A   $��2��6Q   6g   �� 5"   
   
�������$��10q�6'   $.��(Type in the output or choose a txt file:��63   $��3��6c   $��1��6p   6�   �� 5#   
   -   5        $��ChooseOutputFileButton��5#      $��10q�6T   $��Choose file��6`   $��3��6v   $��2��6�   6�   �� 5$   
   -   5        $ OutputTextBox��5$      $��10q�6)   $��4��65   $��1��6B   $��2��6R   6h   �� 5%   
   
�������$��Test Case Name:��6'   $��10q�6>   $��5��6J   $��1��6W   6i   �� 5&   
   -   5        $ TestCaseNameTextBox��5&      $��10q�6/   $��6��6;   $��1��6H   $��2��6X   6n   �� 5'   
   
�������$��Points:��6'   $��10q�66   $��7��6B   $��1��6O   6a   �� 5(   
   -   5        $ SaveTestCaseButton��5(      $��110, 10q�6N   $��Save Test Case��6_   $��9��6x   $��1��6�   $��2��6�   6�   %  Xceed.Wpf.Toolkit.IntegerUpDown  5)   
   -   5          Name$ PointsUpDown��5)      $��10q�63   $��8��6?   $��1��6L   $
��Left=�6\   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, System.Runtime, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]], System.Runtime, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]]   Value 06w      Minimum 05*         Maximum 1005+      �  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Int32, System.Runtime, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]], System.Runtime, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]]	   	Increment	 15,      $	��100��5-      5/      51      52      