using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CM3036_Coursework___Kolesov1308140
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        private readonly StudentEntities _studentEntities = new StudentEntities();

        public MainWindow()
        {
            InitializeComponent();

            BindListBoxData();

            InputValidationHelper.AttachTextBoxInputEventHandlers(this);
        }

        public void BindListBoxData()
        {
            //Gets student list
            StudentsListBox.ItemsSource = GetStudentList();
        }

        public List<Student> GetStudentList()
        {
            try
            {
                //Gets the student list from DB in a reverse order to keep the records
                //in the same order as in DB and keep oldest records at top
                return _studentEntities.Students.AsEnumerable().Reverse().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error while trying to get Students from Database\nError Message: " + ex.Message +
                                "\nPlease try again.");
                return new List<Student>();
            }
        }

        private Student GetSelectedStudent()
        {
            return StudentsListBox.SelectedItem as Student;
        }

        private void ApplyChangesButton_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedStudent = GetSelectedStudent();

            //Early exit (do nothing) if no student selected
            if (selectedStudent == null) return;

            _studentEntities.Students.Attach(selectedStudent);

            selectedStudent.matriculationNumber = int.Parse(MatriculationNumberTextBoxN.Text);
            selectedStudent.firstName = FirstNameTextBoxS.Text;
            selectedStudent.lastName = LastNameTextBoxS.Text;
            selectedStudent.componentOne = ComponentOneTextBoxG.Text;
            selectedStudent.componentTwo = ComponentTwoTextBoxG.Text;
            selectedStudent.componentThree = ComponentThreeTextBoxG.Text;

            _studentEntities.Entry(selectedStudent).State = EntityState.Modified;
            _studentEntities.SaveChanges();

            //Refresh ListBox data
            BindListBoxData();
        }

        public void DeleteSelectedStudentButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedStudent = GetSelectedStudent();

                //Early exit (do nothing) if no student selected
                if (selectedStudent == null) return;

                _studentEntities.Students.Remove(selectedStudent);
                _studentEntities.SaveChanges();

                //Refresh ListBox data
                BindListBoxData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error while trying remove the Student from Database\nError Message: " + ex.Message +
                                "\nPlease try again.");
            }
        }

        private void AddNewStudentButton_OnClick(object sender, RoutedEventArgs e)
        {
            var addNewStudentWindow = new AddANewStudentWindow { Owner = this };
            addNewStudentWindow.ShowDialog();
        }

        private IEnumerable<TextBox> GetGradeTextBoxes()
        {
            return InputValidationHelper.FindLogicalChildren<TextBox>(this).Where(c => c.Name.EndsWith("G"));
        }

        private void NonSubmissionCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            foreach (var tb in GetGradeTextBoxes())
            {
                tb.IsEnabled = false;
            }

        }

        private void NonSubmissionCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            foreach (var tb in GetGradeTextBoxes())
            {
                tb.IsEnabled = true;
            }

        }
    }
}
