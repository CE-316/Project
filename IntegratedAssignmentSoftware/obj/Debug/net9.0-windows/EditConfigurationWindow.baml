   M S B A M L   `   `   `  ����W  SIntegratedAssignmentSoftware, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:IntegratedAssignmentSoftwareIntegratedAssignmentSoftware  5   	   :    4IntegratedAssignmentSoftware.EditConfigurationWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:IntegratedAssignmentSoftware     5   	     � Title$  EditConfigurationWindow��5   	   ���#101010$	��400��6>   $	��350��6K   .��5	      �  � RowDefinitions 5
   
   �� 5      $��20 �6   6*   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $��20 �6   6*   .+�5   
   �� $"��Language/Configuration Name:��5   	   $��1��5   	   $��Center=�5   	   $��10q�5   	   
�������5      �� 5   
   -   5         ��Name$ LanguageTextBox��5   	   $��2��5   	   $��10q�5   	   5       �� 5!   
   $%��Compile Command With Arguments:��5"   	   $��3��5#   	   $��Center=�5$   	   $��10q�5%   	   
�������5'      �� 5(   
   -   5        $ CompileTextBox��5)   	   $��4��5*   	   $��10q�5+   	   5,      �� 5-   
   $!��Run Command With Arguments:��5.   	   $��5��5/   	   $��Center=�50   	   $��10q�51   	   
�������53      �� 54   
   -   5        $ 
RunTextBox��55   	   $��6��56   	   $��10q�57   	   58      �� 59   
   -   5         ��Name$ SaveConfigurationButton��5:   	   $ ��Save Configuration Changes��5<   	   $��7��5=   	   $��Center=�5>   	   $��Center=�5?   	   $��10q�5@   	   5A      5B      5C      