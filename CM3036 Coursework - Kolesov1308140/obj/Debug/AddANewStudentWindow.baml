   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   ] YCM3036 Coursework - Kolesov1308140, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:CM3036_Coursework___Kolesov1308140"CM3036_Coursework___Kolesov1308140 5   	   =   7CM3036_Coursework___Kolesov1308140.AddANewStudentWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   Blocal0clr-namespace:CM3036_Coursework___Kolesov1308140     5   	   -   5        (   "MahApps.Metro.Controls.MetroWindow   Name$  AddANewStudent��5       � Icon$ Images/RGU Logo.ico��5   	      ShowIconOnTitleBar .  $��0q�5   	      	GlowBrush
 �   � � 
ResizeMode$ NoResize=�5   	      WindowTransitionsEnabled .   � Title$ Add a new Student��5   	   $	��510��6#   $	��285��60    � WindowStartupLocation$ CenterScreen=�6<      IsWindowDraggable .  	 � DataContext	 5      0  *CM3036_Coursework___Kolesov1308140.Student  5   
   .��5      � .+�5   
   �� $��Matriculation Number��6   $��	42,15,0,0q�6/   $	��Top=�6B   $��23��6Z   $
��Left=�6f   $	��169��6�   �� 5   
   -   5        
 ��Name$!
 MatriculationNumberTextBoxN��5      $��24��6\   $��	47,40,0,0q�6h    �� TextWrapping$
 Wrap=�6{   $	��Top=�6�    �� 	MaxLength$ 20��6�   $
��Left=�6�   $	��180��6�   ��67   �� *matriculationNumber+6�   �� 5   
   $��
First Name��6   $��	42,75,0,0q�6%   $	��Top=�68   $��23��6P   $
��Left=�6\   $��88��6w   �� 5   
   -   5        $
 FirstNameTextBoxS��5      $
��Left=�6I   $��23��6d   $��
47,100,0,0q�6p   $
 Wrap=�6�   $	��Top=�6�   $	��180��6�   $ 50��6�   ��6-   �� *	firstName+6�   �� 5   
   $��	Last Name��6   $
��Left=�6$   $��
42,135,0,0q�6?   $	��Top=�6S   $��23��6k   $��88��6w   �� 5   
   -   5        $
 LastNameTextBoxS��5      $
��Left=�6F   $��23��6a   $��
47,160,0,0q�6m   $
 Wrap=�6�   $	��Top=�6�   $	��180��6�   $ 50��6�   ��6,   �� *
lastName+6�   �� 5   
   $��Component One��6   $
��Left=�6(   $	��Top=�6C   $��
42,206,0,0q�6[   $��26��6o   $��98��6{   �� 5   
   -   5        $
 ComponentOneTextBoxG��5       �� CharacterCasing$ Upper=�6N   $
��Left=�6f   $	��Top=�6�   $��158,206,0,0q�6�   $��70��6�   $��22��6�   $ 3��6�   ��60   �� *componentOne+6�   �� 5!   
   $��Component Two��6   $	��Top=�6(   $
��Left=�6@   $��
42,246,0,0q�6[   $��26��6o   $��98��6{   �� 5"   
   -   5        $
 ComponentTwoTextBoxG��5"      $ Upper=�6N   $
��Left=�6f   $	��Top=�6�   $��70��6�   $��158,246,0,0q�6�   $��22��6�   $ 5��6�   ��60   �� *componentTwo+6�   �� 5$   
   $��Component Three��6   $
��Left=�6*   $	��Top=�6E   $��26��6]   $	��107��6i   $��
42,286,0,0q�6u   �� 5%   
   -   5        $
 ComponentThreeTextBoxG��5%      $ Upper=�6R   $
��Left=�6j   $	��Top=�6�   $��70��6�   $��22��6�   $��158,286,0,0q�6�   $ 2��6�   ��62   �� *componentThree+6�   �� 5'   
   -   5         ��Name$ NonSubmissionCheckBox��5'      $��Non-submission��62   $
��Left=�6K   $��
47,327,0,0q�6f   $	��Top=�6z   /   )MahApps.Metro.Controls.ToggleButtonHelper   ContentDirection$ RightToLeft=�6�   $��22��6�   $	��129��6�   6  �� 5)   
   $��47,361,51,112q�6   �� 5+   
   $��Final Grade��6   $
��Left=�6&   $��
42,387,0,0q�6A   $	��Top=�6U   $��23��6m   $��77��6y   �� 5,   
   -	   5        $
 FinalGradeTextBox��5,       �� 
IsReadOnly . $
��Left=�6?   $��22��6Z   $��158,383,0,0q�6f   $	��Top=�6{   $��26��6�    �� 
CaretBrush
 �   �
���   �6�   �� 5.   
   -
   5         ��Name$ AddStudentButton��5.      $��ADD STUDENT��6+   $
��Left=�6A   $��
23,433,0,0q�6\   $	��Top=�6p   $	��100��6�   6�   �� 5/   
   -   5        $ CancelButton��5/      $��Cancel��6'   $
��Left=�68   $��158,433,0,0q�6S   $	��Top=�6h   $	��100��6�   6�   51      52      