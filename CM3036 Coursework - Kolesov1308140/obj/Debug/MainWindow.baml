   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   ] YCM3036 Coursework - Kolesov1308140, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:CM3036_Coursework___Kolesov1308140"CM3036_Coursework___Kolesov1308140 5   	   3   -CM3036_Coursework___Kolesov1308140.MainWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   Blocal0clr-namespace:CM3036_Coursework___Kolesov1308140     5   	   -   5        (   "MahApps.Metro.Controls.MetroWindow   Name$!  CM3036GradeManagementSystem��5       � Icon$ Images/RGU Logo.ico��5	   	      ShowIconOnTitleBar .  $��0q�5   	      	GlowBrush
 �   � � 
ResizeMode$ CanResizeWithGrip=�5   	      WindowTransitionsEnabled .   � Title$) #   CM3036 - Grade Management System��5   	   $	��530��65   $	��670��6B   $	��500��6N   $	��670��6^    � WindowStartupLocation$ CenterScreen=�6m   .��5      � $��	10,20,0,0q�6   $	��Top=�6   $	��500��66    � RowDefinitions 5   
   �� 5      $	��360 �6   �� 5      $	��140 �6   	 � ColumnDefinitions	 5   
   �� 5      $	��350 �6   �� 5      $	��350 �6   .+�5   
   �� $��Students��6   $��	10,10,0,0q�6%   $	��Top=�68   $��0��6P   $	��340��6`   $
��Left=�6m   $	��324��6�   .��5      � .+�5      �� $&�� Matriculation number - Full Name��6   $
��Left=�6C   $��4,0,0,0q�6^   $	��Top=�6o   $��26��6�   $	��210��6�   �� 5      -   5        
 ��Name$
 RefreshStudentListImage��5   5   $��0��6   $��0��6%   $��Images/refresh-icon.png��6V    �� BitmapScalingMode$ NearestNeighbor=�6    �� EdgeMode$ Aliased=�5      $
��Left=�6)   $��22��6D   $��281,-30,0,0q�6P   $	��Top=�6e   $��21��6}    � ToolTip 5       _� 5!      $��Refresh Student List��6"   6C   5#      �� 5$      -   5         ��Name$ StudentsListBox��5$      $	��202��6K   $��
10,26,10,0q�6X   $	��Top=�6l   $��16(�6�   $��Segoe UI Semibold)�6�   ��63   �� 6�   �� 5&      $��Show Students who:��6   $��163,233,29,40q�65   6N   	� 5'      -   5         	�Name$ PassedRadioButton��5'       	� 	GroupName$ DisplayType��69   $��Passed��6Q   $��134,266,116,10q�6b   6z   	� 5(      -   5        $ FailedRadioButton��5(      $ DisplayType��69   $��Failed��6Q   $��250,266,4,10q�6b   6x   	� 5)      -   5        $ ShowEveryoneRadioButton��5)      $ DisplayType��6?   $��Show Everyone��6W   $��10,267,194,9q�6o    d� 	IsChecked$
 TrueY�6�   6�   �� 5*      -   5         ��Name$ PercentagePassedLabel��5*      $
��Left=�67   $��193,265,0,0q�6R   $	��Top=�6g   $��52��6   $��26��6�   5+      5,      �� 5.   
   $��0��6   $��1��6    $��Student Information��60   $��	10,10,0,0q�6M   $	��Top=�6`   $	��180��6x   $
��Left=�6�   $	��277��6�   .��5/      � .+�50      �� $��Matriculation
Number��6   $
��Left=�6@   $��10,9,0,0q�6[   $	��Top=�6m   $��42��6�   $��88��6�   �� 51      $��
First Name��6   $��	10,66,0,0q�6-   $	��Top=�6@   $��26��6X   $
��Left=�6d   $��88��6   �� 52      $��	Last Name��6   $
��Left=�6,   $��
10,108,0,0q�6G   $	��Top=�6[   $��26��6s   $��88��6   �� 54      -   5         ��Name$! MatriculationNumberTextBoxN��54      $
��Left=�6?   $��24��6Z   $��
103,19,0,0q�6f    �� TextWrapping$
 Wrap=�6z   $	��Top=�6�   $	��150��6�    �� 	MaxLength$ 20��6�   .��6�   ��55      �� 56       �� ElementName$ StudentsListBox��6"    �� Path$6 0SelectedItem.(local:Student.matriculationNumber)�6@    �� UpdateSourceTrigger$ PropertyChanged=�6x    �� Mode$ OneWay=�6�    �� ValidationRules 57      ? 9CM3036_Coursework___Kolesov1308140.TextBoxValidationRules  58   "      TextBoxRuleType$ MatriculationNumber��6?   5:      .��5;   $   {�5<      �� 5=      $ StudentsListBox��6"   $ SelectedItems.Count�6@   .��5>   )   5?      �� 5A      -	   5        $ FirstNameTextBoxS��5A      $��103,63,12,52q�65   $
��Left=�6K   $��23��6f   $
 Wrap=�6r   $	��Top=�6�   $	��150��6�   $ 50��6�   .��6�   ��5B      �� 5C      $ StudentsListBox��6"   $, &SelectedItem.(local:Student.firstName)�6@   $ PropertyChanged=�6n   $ OneWay=�6�    5D        5E   "   $ 	FirstName��6?   5G      .��5H   $   {�5I      �� 5J      $ StudentsListBox��6"   $ SelectedItems.Count�6@   .��5K   )   5L      �� 5N      -
   5        $ LastNameTextBoxS��5N      $
��Left=�64   $��23��6O   $��103,104,0,0q�6[   $
 Wrap=�6p   $	��Top=�6�   $	��150��6�   $ 50��6�   .��6�   ��5O      �� 5P      $ StudentsListBox��6"   $+ %SelectedItem.(local:Student.lastName)�6@   $ PropertyChanged=�6m   $ OneWay=�6�    5Q        5R   "   $ LastName��6?   5T      .��5U   $   {�5V      �� 5W      $ StudentsListBox��6"   $ SelectedItems.Count�6@   .��5X   )   5Y      5[      5\      �� 5^   
   $��Student Grades��6   $��10,208,0,-24q�6+   $��1��6A    J� RenderTransformOrigin$ 
-0.2,0.087&�6Q   $	��243��6t   $	��Top=�6�   $��0��5_      $��2��6(   $
��Left=�69   $	��277��6T   .��5`      � .+�5a      �� $��Component One��6   $
��Left=�60   $	��Top=�6K   $��10,4,0,0q�6c   $��26��6u   $	��130��6�   �� 5b      $��Component Two��6   $	��Top=�60   $
��Left=�6H   $��	10,44,0,0q�6c   $��26��6v   $	��130��6�   �� 5c      $��Component Three��6   $
��Left=�62   $	��Top=�6M   $��26��6e   $	��130��6q   $��	10,84,0,0q�6}   �� 5e      -   5        $ ComponentOneTextBoxG��5e       �� CharacterCasing$ Upper=�68   $
��Left=�6P   $	��Top=�6k   $��	145,4,0,0q�6�   $��70��6�   $��22��6�   $ 3��6�   .��6�   ��5f      �� 5g      $ StudentsListBox��6"   $/ )SelectedItem.(local:Student.componentOne)�6@   $ PropertyChanged=�6q    5h        5i   "   $ ComponentOne��6?   5k      .��5l   $   {�5m      �� 5n      $ StudentsListBox��6"   $ SelectedItems.Count�6@   .��5o   )   5p      �� 5r      -   5        $ ComponentTwoTextBoxG��5r      $ Upper=�68   $
��Left=�6P   $	��Top=�6k   $��70��6�   $��
145,44,0,0q�6�   $��22��6�   $ 5��6�   .��6�   ��5s      �� 5t      $ StudentsListBox��6"   $/ )SelectedItem.(local:Student.componentTwo)�6@   $ PropertyChanged=�6q    5u        5v   "   $ ComponentTwo��6?   5x      .��5y   $   {�5z      �� 5{      $ StudentsListBox��6"   $ SelectedItems.Count�6@   .��5|   )   5}      �� 5      -   5        $ ComponentThreeTextBoxG��5      $ Upper=�6:   $
��Left=�6R   $	��Top=�6m   $��70��6�   $��22��6�   $��
145,84,0,0q�6�   $ 2��6�   .��6�   ��5�      �� 5�      $ StudentsListBox��6"   $1 +SelectedItem.(local:Student.componentThree)�6@   $ PropertyChanged=�6s    5�        5�   "   $ ComponentThree��6?   5�      .��5�   $   {�5�      �� 5�      $ StudentsListBox��6"   $ SelectedItems.Count�6@   .��5�   )   5�      �� 5�      -   5         ��Name$ NonSubmissionCheckBox��5�      $��Non-submission��6:   $
��Left=�6�   $��
15,121,0,0q�6�   $	��Top=�6�   /   )MahApps.Metro.Controls.ToggleButtonHelper   ContentDirection$ RightToLeft=�6�   $��22��6!  $	��148��6-  {�6S   �� $ StudentsListBox��$ SelectedItems.Count� 69  �� $ StudentsListBox��$0 *SelectedItem.(local:Student.nonSubmission)�6�  �� 5�      $��15,148,50,41q�6   �� 5�      $��Final Grade��6   $
��Left=�6.   $��
10,168,0,0q�6I   $	��Top=�6]   $��26��6u   $	��107��6�   �� 5�      -   5        $ FinalGradeTextBox��5�        �� 
IsReadOnly  . $
��Left=�6�   $��14��6�   $��145,168,0,0q�6�   $	��Top=�6�   $��26��6�   ! �� 
CaretBrush
! �   �
���   ���65   �� $ StudentsListBox��$- 'SelectedItem.(local:Student.finalGrade)�6  5�      5�      �� 5�   
   -   5        " ��Name$" AddNewStudentButton��5�      $��Add a New Student��6.   $
��Left=�6J   $��	10,13,0,0q�6e   $��1��5�      $��0��6&   $	��146��66   $��30��6f   $	��Top=�6r   �� 5�   
   -   5        $" ApplyChangesButton��5�      $��Apply Changes��6-   $
��Left=�6E   $��
189,13,0,0q�6`   $��1��6t   $��0��5�      $	��145��6)   $��30��65   $	��Top=�6d   �� 5�   
   -   5        $" DeleteAllStudentsButton��5�      $��DELETE ALL STUDENTS��62   $
��Left=�6P   $��	10,61,0,0q�6k   $��1��6~   $��0��5�      $	��Top=�6)   $	��146��6A   $��30��6M   6Y   �� 5�   
   -   5        $!" DeleteSelectedStudentButton��5�      $��DELETE SELECTED STUDENT��66   $
��Left=�6X   $��
189,61,0,0q�6s   $��1��5�      $��0��6   $	��145��6/   $��30��6;   $	��Top=�6G   6_   5�      5�      