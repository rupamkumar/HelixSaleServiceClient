   M S B A M L   `   `   `  ����  HelixSaleServiceClient'    !HelixSaleServiceClient.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6F     � Title$  
MainWindow��5   	   $	��790��6   .��5      �  � RowDefinitions 5   
   �� 5      $	��350 �6   �� 5	      $��3 �6   �� 5
      $	��350 �6   .+�5   
   �� $��1��6   $��1��6%   $	��100��65   $��Stretch=�6A   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 	SalesGrid��5   T   $
��Left=�6   $��0��6.   $	��Top=�6<   6T   � 5   
   $��0,0,0,0q�6   $��2��6    $	��Top=�6-   .+�5      �� $��Location Name��6   $
��Left=�6,   $	��Top=�6G   $��	31,40,0,0q�6_   �� 5      -   5         ��Name$ txtLocationName��5   }   $
��Left=�6    �� TextWrapping$
 Wrap=�61   $	��Top=�6E   $	��120��6]   $��
118,40,0,0q�6i    J� RenderTransformOrigin$ 0.515,1.205&�6�   �� 5      $��Sales Person��6   $
��Left=�6+   $	��Top=�6F   $��	31,70,0,0q�6_   �� 5      -   5        $ txtSalesPerson��5   }   $
��Left=�6   $
 Wrap=�61   $	��Top=�6E   $	��120��6]   $��
118,70,0,0q�6i   6}   �� 5      $��Total Sales��6   $
��Left=�6*   $	��Top=�6E   $��
31,100,0,0q�6]   �� 5      -   5        $ txtTotalSales��5   ~   $
��Left=�6   $
 Wrap=�61   $	��Top=�6E   $	��120��6]   $��118,100,0,0q�6i   6~   �� 5      $��Product��6   $
��Left=�6&   $	��Top=�6A   $��
400,40,0,0q�6Z   $��82��6n   �� 5      -   5         ��Name$ 
cmbProduct��5       �� DisplayMemberPath$
 Name��5      $
��Left=�60   $��
480,40,0,0q�6K   $	��Top=�6_   $	��120��6w   ��5   ,   ��  �� Path$ Products�	 �� SelectedValue	 6R   �� $ _Product�5   w   �� 5      $��Currency��6   $
��Left=�6'   $	��Top=�6B   $��
400,70,0,0q�6[   $��82��6o   �� 5      -   5        $ cmbCurrency��5      $ currency��5       $
��Left=�64   $��
480,70,0,0q�6O   $	��Top=�6c   $	��120��6{   ��5   -   �� $ Currency�	 6S   �� $ 	_Currency�5    {   �� 5"      -   5        
 ��Name$
 btnAdd��5"   k   $��Add New Sale��6   $
��Left=�6,   $	��Top=�6G   $	��120��6_   $��567,130,0,0q�6�   $
��Auto��6�   �� 5#      $��Add Sale��6   $
��Left=�6)   $��	10,20,0,0q�6D   $	��Top=�6W   $	��150��6o   $	��688��6|   5&      5(      5)      