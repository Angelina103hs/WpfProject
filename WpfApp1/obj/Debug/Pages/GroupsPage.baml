   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WpfApp1.PagesWpfApp1.Pages  5          WpfApp1.Pages.GroupsPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:WpfApp1.Pages     5        N� Title$  
GroupsPage��5	      .��5      � -   5         �Name$ 
GroupsGrid��5       � ColumnDefinitions 5   
   �� 5      $	��250��6   �� 5      -   5         ��Name$ SplitterColumn��5   ,   $
��auto �6   6,   �� 5      -   5        $ ChangeColumn��5   8   $��0 �6   $	��250��6)   68    � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��* �6   .+�5   
   �� $��Группы��6   $��1��6!   $��0��6.   $��3��6>   6T   �� 5   
   $��0��6   $��1��6%   $��3��62    �� Orientation$ 
Horizontal=�6F   .+�5      �� -   5         ��Name$ AddGroup��5      $��Добавить��6=   $��75��6P   $��5,5,0,5q�6[   �� 5      -   5        $ 	CopyGroup��5      $��Копировать��6?   $��75��6T   $��5,5,0,5q�6_   �� 5      -   5        $ 	EditGroup��5      $��Изменить��6?   $��75��6R   $��5,5,0,5q�6]   �� 5      -   5        $ DeleteGroup��5      $��Удалить��6C   $��75��6U   $��5,5,0,5q�6`   5      ]� 5   
   $��2��6   $��0��6!   $��2��61   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ GroupDataGrid��5         AutoGenerateColumns .  	   CanUserAddRows	 .  $��2��6a   
 � ContextMenu
 5      �� 5       5"         Columns 5$      4  .System.Windows.Controls.DataGridTemplateColumn  5&      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6-      Header$# Название группы��5'   g      CellTemplate 5(      �� 5)      .R�5*   "   �� ��6(   �� *
NAME_GROUP+5+      5-      51      52      �� 54   
   $��1��6   $��2��6'   $��3��64   $��Stretch=�6>   �� 56   
   -	   5        $��X��56      $��20��6   $��20��6)   $��2��64   $��1��6D   $��Right=�6Q   $	��Top=�6m   6�   �� 58   
   $��2��6   $��2��6%   $ Vertical=�62   .+�59      �� -
   5         ��Name$ 
GroupLabel��59      $#��Добавить группу��6(   $��16(�6B   $
��Bold"�6P   $��5,0,0,0q�6b   �� 5;      $��Наименование��6   $��5,0,0,0q�6/   �� 5<      -   5         ��Name$ 
TextGroups��5<      $��25��6t   $��5,0,0,0q�6�   ��6*   ��  �� ElementName$ GroupDataGrid�� �� Path$ SelectedItem.NAME_GROUP�6�   �� 5>      $ 
Horizontal=�6   $��Center=�66   $��0,15,0,0q�6S   .+�5?      �� -   5        $ GroupAddCommit��5?      $��Добавить��61   $��30��6D   $��5,5,5,5q�6Q   $��5,5,0,5q�6c   6t   5@      5B      5D      5E      