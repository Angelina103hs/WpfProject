   M S B A M L   `   `   `  ����J  FBaseWPFClientEx, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:BaseWPFClientEx.PagesBaseWPFClientEx.Pages  5      (    "BaseWPFClientEx.Pages.ReadingsPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:BaseWPFClientEx.Pages     5      -   5          N� Title$  ReadingsPage��5	      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   $	��250��6)   �� 5      $��3 �6   �� 5      $	��250 �6   $	��250��6+    � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��* �6   .+�5   
   �� $+��%Пользования книгами��6   $��5��6.      CaptionLabelStyle#��[  6B   ]� 5   
   -   5         ]�Name$ ReadingsDataGridDockPanel��5      $��1��67   $��0��6D   $��3��5      .+�5      ��  �� Orientation$ 
Horizontal=�6   $	��Top=�62   $��25��6G   $��0,5,0,0q�5      .+�5      �� -   5         ��Name$ AddReadingButton��5      $��
Новое��63   $��75��6C   $��5,0,0,0q�5      �� 5      -   5        $ CopyReadingButton��5      $��Копировать��64   $��75��6I   $��5,0,0,0q�5      �� 5       -   5        $ EditReadingButton��5       $��Изменить��64   $��75��6G   $��5,0,0,0q�5!      �� 5"      -   5        $ DeleteReadingButton��5"      $��Удалить��66   $��75��6H   $��5,0,0,0q�5#      5$      ]� 5&      $	��Top=�6   $��0,3,0,0q�6-   .+�5'      �� $3��-Быстрый поиск по столбцу��6   �� 5(      -   5         ��Name$ FilterComboBox��5(      $	��120��63   5)      �� 5*      -   5         ��Name$ FilterTextBox��5*      $��5,0,5,0q�61   5+      5,      ]� 5.      $��0,3,0,0q�6   .+�5/      �  50      �� 51      $��* �6+   $	��250��65   �� 52      $
��Auto �6+   �� 53      $��* �6+   $	��250��65   .+�55      &   System.Windows.Controls.DataGrid  -	   5          Name$ ReadersDataGrid��55      $��0��68   	   Columns	 56      0  *System.Windows.Controls.DataGridTextColumn  57      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Фамилия��61      Width5  /System.Windows.Controls.DataGridLengthConverter$	 10* 6c   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6B   ��  �� Path$ Surname�6c     58      $
 Имя��61   $	 10* 6\    6>   �� $
 Name�6\     59      $
 Отчество��61   $	 10* 6g    6C   �� $ 
Patronymic�6g     5:      $
 Дата рождения��61   $	 100 5;   B    5:   H   �� $ 	Birthdate� �� StringFormat$ {0:dd.MM.yyyy}��5;   B     5<      $
 Пол��61   $ 35 6g    6>   �� $ Sexes.ShortName�6g   5>      �� 5?      $��1��6#   $��Center=�63   $��3��6P     5@      -
   5        $ ReadingsDataGrid��5@      $��2��69   	 5A        5B      $
 
Книга��61   $ * 5C   M    6"   �� $ Copies.Books.Name�6M     5D      $
 Инв. номер��61   $ 90 5E   I    6"   �� $ Copies.InvNum�6I     5F      $
 Дата выдачи��61   $ 90 5H   "    5G   "   �� $ 	BeginDate�$ {0:dd.MM.yyyy}��5H   "     5I      $
 Дата возврата��61   $ 90 5J   B    5I   H   �� $ EndDate�$ {0:dd.MM.yyyy}��5J   B   5L      5M      5N      5O      �� 5Q   
   -   5         ��Name$ DialogGridSplitter��5Q      $��3��63   $��1��6C   $��Center=�5R      $��3��64   $y�Hidden=�6>   �� 5T   
   -   5         ��Name$ DialogScrollViewer��5T      $��1��63   $��2��6@   $��4��6Q   $y�Hidden=�5U      $
��Auto=�6+   .��5V       � -   5          �Name$ DialogFrame��5V      5W      5X      5Y      