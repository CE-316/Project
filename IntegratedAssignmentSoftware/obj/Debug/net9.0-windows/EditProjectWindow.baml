   M S B A M L   `   `   `  ����W  SIntegratedAssignmentSoftware, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:IntegratedAssignmentSoftwareIntegratedAssignmentSoftware  5   	   4    .IntegratedAssignmentSoftware.EditProjectWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:IntegratedAssignmentSoftware     5   	     � Title$  EditProjectWindow��5   	   ���#101010$	��800��68   $
��1200��6E   .��5	      �  � RowDefinitions 5
   
   �� 5      $��20 �6   6*   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $��* �6   6)   �� 5      $��20 �6   6*    � ColumnDefinitions 5   
   �� 5      $��20 �6   6,   �� 5      $
��auto �6   6.   �� 5      $
��auto �6   6.   �� 5      $��* �6   6+   �� 5      $��20 �6   6,   .+�5   
   �� $��Project Name��5   	   $��Center=�5    	   $��1��5!   	   $��1��5"   	   $��3��5#   	   $��10q�5$   	   
�������5&      �� 5'   
   -   5         ��Name$ NameTextBox��5*   	   $��1��5(   	   $��3��5)   	   $��2��5+   	   $��10q�5,   	   5-      �� 5.   
   $��1��5/   	   $��3��50   	   $d��^Assignment Overview (This section presents the assignment prompt and any rules or limitations)��51   	   $��3��52   	   $��Center=�53   	   $��10q�54   	   
�������56      �� 57   
   -   5         ��Name$ DescriptionTextBox��5:   	   $��1��58   	   $��3��59   	   $��4��5;   	   $��10q�5<   	   " �� VerticalScrollBarVisibility$
 Auto=�5=   	   $	��200��5>   	   5?      �� 5@   
   
�������$��10q�6'   $��Choose Configuration��63   $��Center=�6O   $��5��6l   $��1��6y   6�   �� 5A   
   -   5         ��Name$ ConfigurationListBox��5A   �   $	��Top=�6   $��2��6*   $��1��6;   $��Visible=�6K   $��	10,0,10,0q�6~   $��6��6�   $	��200��6�   $	��450��6�   ��5B      �� 5C      .R�5D      �� $��Center=�5E      $	��350��5F       �� Padding$ 5q�5G      ��5D       �� *
Language+5G      5H      5J      �� 5K   
   
�������$��10q�6'   $��
Test Cases��63   $��Center=�6E   $��5��6b   $��2��6o   6�   �� 5L   
   -   5        $ TestCaseListBox��5L   �   $	��Top=�6F   $��2��6^   $��2��6o   $��Visible=�6   $��	10,0,10,0q�6�   $��6��6�   $	��200��6�   $	��450��6�   ��5M      �� 5N      .R�5O      ��  �� Orientation$ 
Horizontal=�6!   $	��0,2q�6:   .+�5P      �� $��Center=�5Q      $	��200��5R      ��5P   $   �� *Name+5R      �� 5S      -   5        $
��Edit��5S   !   
	 � Tag	 5U      �� �� 5W      -   5        $��Delete��5W   !   $��5,0,0,0q�5Z      	 5Y      �� 5Z      5[      5\      5^      �� 5_   
   
�������$��10q�6'   $��Center=�63   $��5��6P   $��3��6]   $'��!Preview of the Selected Test Case��6m   6�   �� 5`   
   -   5        
 ��Name$
 TestCasePreviewTextBlock��5a      $F��@Placeholder Test Case
Input: 3, 4, 1, 2, 5
Output: 1, 2, 3, 4, 5��5b      $��WrapWithOverflow=�5c      $��6��5d      $��3��5e      $	��150��5f      $��10q�5g      
�������5i      �� 5j   
   -   5         ��Name$ AddTestCaseButton��5j      $��Add Test Case��5k      $��7��5l      $��3��5m      $��30��5n      $	��100��5o      $	��Top=�5p      5q      �� 5r   
   -	   5        $ SaveProjectButton��5u   	   $��1��5s   	   $��3��5t   	   $��Save Project Changes��5w   	   $��Center=�5x   	   $��10q�5y   	   $��8��5z   	   5{      5|      5}      