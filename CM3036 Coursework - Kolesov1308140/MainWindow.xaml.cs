using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

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

            AttachTextBoxInputEventHandlers();
        }

        public List<Student> GetStudentList()
        {
            try
            {
                //Force open the connection
                _studentEntities.Database.Connection.Open();
                
                //Gets the student list from DB in a reverse order to keep the records
                //in the same order as in DB and keep oldest records at top
                return _studentEntities.Students.AsEnumerable().Reverse().ToList();
            }
            catch (EntityException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error while trying to get Students from Database\nError Message: " + ex.Message +
                                "\nPlease try to run the application again.");
                return new List<Student>();
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error while trying to get Students from Database\nError Message: " + ex.Message +
                                "\nPlease try to run the application again.");
                return new List<Student>();
            }
            finally
            {
                //Close the connection
                _studentEntities.Database.Connection.Close();
            }
        }

        public void SaveStudentChanges(Student changedStudent, int matricNumber)
        {
            try
            {
                //Force open the connection
                _studentEntities.Database.Connection.Open();
                //_studentEntities.Students.wh
            }
            catch (EntityException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error while trying to get Students from Database\nError Message: " + ex.Message +
                                "\nPlease try to run the application again.");
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error while trying to get Students from Database\nError Message: " + ex.Message +
                                "\nPlease try to run the application again.");
            }
            finally
            {
                //Close the connection
                _studentEntities.Database.Connection.Close();
            }
        }

        public void BindListBoxData()
        {
            //Gets student list
            StudentsListBox.ItemsSource = GetStudentList();
        }

        private string CalculateFinalGrade(Student student)
        {
            var finalGrade = "";



            return finalGrade;
        }

        private Student GetSelectedStudent()
        {
            return StudentsListBox.SelectedItem as Student;
        }

        private void AttachTextBoxInputEventHandlers()
        {
            foreach (TextBox tb in FindLogicalChildren<TextBox>(this))
            {
                var textBoxType = tb.Name.Substring(tb.Name.Length - 1);
                switch (textBoxType)
                {
                    case "N" :
                        tb.PreviewKeyDown += InputValidator.AllowOnlyNumbers_OnKeyDown;
                        tb.TextChanged += InputValidator.AllowOnlyNumbers_TextChanged;
                        DataObject.AddPastingHandler(tb, InputValidator.AllowOnlyNumbers_PasteHandler);
                        break;
                    case "S" :
                        tb.PreviewKeyDown += InputValidator.AllowOnlyAlphabeticalChars_OnKeyDown;
                        tb.TextChanged += InputValidator.AllowOnlyAlphabeticalChars_TextChanged;
                        DataObject.AddPastingHandler(tb, InputValidator.AllowOnlyAlphabeticalChars_PasteHandler);
                        break;
                    case "G" :
                        tb.PreviewKeyDown += InputValidator.AllowOnlyGradeLetters_OnKeyDown;
                        tb.TextChanged += InputValidator.AllowOnlyGradeLetters_TextChanged;
                        DataObject.AddPastingHandler(tb, InputValidator.AllowOnlyGradeLetters_PasteHandler);
                        break;
                }
            }
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

        public static IEnumerable<T> FindLogicalChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield break;

            foreach (DependencyObject child in LogicalTreeHelper.GetChildren(depObj).OfType<DependencyObject>())
            {
                if (child is T)
                {
                    yield return (T)child;
                }

                foreach (T childOfChild in FindLogicalChildren<T>(child))
                {
                    yield return childOfChild;
                }
            }
        }

        private void AddNewStudentButton_OnClick(object sender, RoutedEventArgs e)
        {
            var popup = new Window();
            popup.ShowDialog();
        }
    }
}
