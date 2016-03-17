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
                //Force open the connection to try to tackle "Underlying provider failed to open" exception
                _studentEntities.Database.Connection.Open();
                //Gets the student list from DB in a reverse order to keep the records
                //in the same order as in DB and keep oldest records at top
                return _studentEntities.Students.AsEnumerable().Reverse().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error while trying to get Students from Database\n\nPlease try again.\n\nError Message: " + ex.Message);
                return new List<Student>();
            }
            finally
            {
                //Force close the connection
                _studentEntities.Database.Connection.Close();
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

            try
            {
                _studentEntities.Database.Connection.Open();
                _studentEntities.Students.Attach(selectedStudent);

                selectedStudent.matriculationNumber = int.Parse(MatriculationNumberTextBoxN.Text);
                selectedStudent.firstName = FirstNameTextBoxS.Text;
                selectedStudent.lastName = LastNameTextBoxS.Text;
                if (!NonSubmissionCheckBox.IsChecked??false)
                {
                    selectedStudent.componentOne = ComponentOneTextBoxG.Text;
                    selectedStudent.componentTwo = ComponentTwoTextBoxG.Text;
                    selectedStudent.componentThree = ComponentThreeTextBoxG.Text;
                    selectedStudent.nonSubmission = true;
                }
                else
                {
                    selectedStudent.componentOne = "";
                    selectedStudent.componentTwo = "";
                    selectedStudent.componentThree = "";
                    selectedStudent.nonSubmission = false;
                }

                _studentEntities.Entry(selectedStudent).State = EntityState.Modified;
                _studentEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error while trying to save Student from Database\n\nPlease try again.\n\nError Message: " + ex.Message);
            }
            finally
            {
                _studentEntities.Database.Connection.Close();
                //Refresh ListBox data
                BindListBoxData();
            }
        }

        public void DeleteSelectedStudentButton_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedStudent = GetSelectedStudent();

            //Early exit (do nothing) if no student selected
            if (selectedStudent == null) return;

            try
            {
                _studentEntities.Database.Connection.Open();

                _studentEntities.Database.Connection.Open();
                _studentEntities.Students.Remove(selectedStudent);
                _studentEntities.SaveChanges();

                //Refresh ListBox data
                BindListBoxData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error while trying to remove Student from Database\n\nPlease try again.\n\nError Message: " + ex.Message);
            }
            finally
            {
                _studentEntities.Database.Connection.Close();
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
                tb.Text = tb.Text.Trim();
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
