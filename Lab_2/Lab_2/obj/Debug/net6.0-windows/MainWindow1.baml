   M S B A M L   `   `   `  ����@  <Lab_2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:Lab_2Lab_2  5   	       Lab_2.MainWindow1   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Lab_2     5   	     � Title$  Infix evaluation��5   	    � SizeToContent$ WidthAndHeight=�6"   .��5	      �  J� RenderTransformOrigin$ 0.5,0.5&�6   $	��250��6+   $	��400��67   z�5
   
   V� 5      .~�5      �� �� 5      �� 5      T� 5       T� X$ 1Q�6%   5      .+�5   
   �� -   5         ��Name$ Button1��5      $��Button��6"   $��Center=�63   $��20,270,20,0q�6P   $	��Top=�6e   $	��210��6�   �� 5   
   -   5         ��Name$ TextBoxEval��5      $��Right=�6'   $��
0,354,20,0q�6C    �� TextWrapping$
 Wrap=�6W   $	��Top=�6k   $	��160��6�    �� 
IsReadOnly$
 True��6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 	DataGrid1��5      $��20,60,20,148q�6&   	 J� IsManipulationEnabled$
	 True��6<   $	��180��6Y   �� 5   
   -   5        $ TextBoxInfix��5      $��Right=�6(   $��
0,314,20,0q�6D   $
 Wrap=�6X   $	��Top=�6l   $	��160��6�   $
 True��6�   �� 5   
   -   5        $ TextBoxPrefix��5      $��Right=�6)   $��	0,24,20,0q�6E   $
 Wrap=�6X   $��	neg - x 1��6l   $	��Top=�6}   $	��160��6�   �� 5   
   $��Prefix��6   $
��Left=�6!   $��	20,20,0,0q�6<   $	��Top=�6O   �� 5   
   $��Infix��6   $
��Left=�6    $��
20,310,0,0q�6;   $	��Top=�6O   �� 5   
   $
��Eval��6   $
��Left=�6   $��
20,350,0,0q�6:   $	��Top=�6N   5      5      