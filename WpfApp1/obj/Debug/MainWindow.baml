   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	     � Title$-  'Система тестирования��5   	   $	��450��6&   $	��800��63   .��5	      �  � ColumnDefinitions 5   
   �� 5      $	��110 �6   �� 5      $��* �6   $	��300��6)    � RowDefinitions 5   
   �� 5      $��* �6   6)   �� 5      $��35 �6   .+�5   
   �� $��0��6   $��0��6%   $��2��62   
�������.+�5      �� $��image/Library.png��6   $��70��6/   $��70��6:   $��0,5,0,0q�6F   6Y   �� 5      -   5        $��GroupsButton��5      $��Группы��6)   $��25��6:   $��5,5,5,0q�6F   6t   �� 5      -   5        $��TeachersButton��5      $ ��Преподаватели��6+   $��25��6C   $��5,5,5,0q�6O   6   �� 5      -   5        $��StudentsButton��5      $��Студенты��6+   $��25��6>   $��5,5,5,0q�6J   6z   �� 5      -   5        $��SubjectsButton��5      $��Дисциплины��6+   $��25��6@   $��5,5,5,0q�6L   6|   �� 5      -   5        $��
TestButton��5      $!��Составить тест��6'   $��25��6@   $��5,5,5,0q�6L   6_   5       � 5   
   -   5          �Name$ 	RootFrame��5   -   $��0��6   $��1��6     � NavigationUIVisibility$ Hidden=�6@   6b   �� 5   
   $��1��6   $��1��6$   
�������6@   �� 5   
   $��1��6   $��1��6%   
������� �� Orientation$ 
Horizontal=�6E   $
��Left=�6^   .+�5      �� -   5        $��PreviousPageButton��5      $��
Назад��6/   $��75��6?   $��5,5,0,5q�6J   6~   �� 5       -   5        $��NextPageButton��5       $��Вперед��6+   $��75��6<   $��5,5,0,0q�6G   $��25��6X   $	��Top=�6d   6~   5!      5$      5%      