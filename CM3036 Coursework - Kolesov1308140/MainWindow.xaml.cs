using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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
                if (PassedRadioButton.IsChecked ?? false) return _studentEntities.Students.Where(g => g.finalGrade != "E" && g.finalGrade != "F").AsEnumerable().Reverse().ToList();
                if (FailedRadioButton.IsChecked ?? false) return _studentEntities.Students.Where(g => g.finalGrade == "E" || g.finalGrade == "F").AsEnumerable().Reverse().ToList();
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
            try
            {
                var selectedStudent = GetSelectedStudent();

                //Early exit (do nothing) if no student selected
                if (selectedStudent == null) return;

                var errorList = InputValidationHelper.GetErrorListForWrongInput(this);

                //If no errors
                if (errorList == "") {
                    _studentEntities.Database.Connection.Open();
                    _studentEntities.Students.Attach(selectedStudent);

                    selectedStudent.matriculationNumber = int.Parse(MatriculationNumberTextBoxN.Text);
                    selectedStudent.firstName = FirstNameTextBoxS.Text;
                    selectedStudent.lastName = LastNameTextBoxS.Text;
                    if (!NonSubmissionCheckBox.IsChecked ?? false)
                    {
                        selectedStudent.componentOne = ComponentOneTextBoxG.Text;
                        selectedStudent.componentTwo = ComponentTwoTextBoxG.Text;
                        selectedStudent.componentThree = ComponentThreeTextBoxG.Text;
                        selectedStudent.nonSubmission = false;
                    }
                    else
                    {
                        selectedStudent.componentOne = "";
                        selectedStudent.componentTwo = "";
                        selectedStudent.componentThree = "";
                        selectedStudent.nonSubmission = true;
                    }
                    selectedStudent.finalGrade = FinalGradeTextBox.Text;

                    _studentEntities.Entry(selectedStudent).State = EntityState.Modified;
                    _studentEntities.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Did not save the student information, error:\n\n" + errorList);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(
                    "Error while trying to save Student from Database\n\nPlease try again.\n\nError Message: " +
                    ex.Message);
            }
            finally
            {
                _studentEntities.Database.Connection.Close();
                //Unselect student in ListBox
                StudentsListBox.UnselectAll();
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
                _studentEntities.Students.Remove(selectedStudent);
                _studentEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error while trying to remove Student from Database\n\nPlease try again.\n\nError Message: " + ex.Message);
            }
            finally
            {
                _studentEntities.Database.Connection.Close();
                //Refresh ListBox data
                BindListBoxData();
            }
        }

        private void DeleteAllStudentsButton_OnClick(object sender, RoutedEventArgs e)
        {
            var confirmationMsgBox = MessageBox.Show("All student recors will be deleted.\n\nDo you wish to continue?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            if (confirmationMsgBox == MessageBoxResult.No) return;
            try
            {
                _studentEntities.Database.Connection.Open();
                _studentEntities.Students.RemoveRange(_studentEntities.Students.Where(s => true));
                _studentEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error while trying to remove Student from Database\n\nPlease try again.\n\nError Message: " + ex.Message);
            }
            finally
            {
                _studentEntities.Database.Connection.Close();
                //Refresh ListBox data
                BindListBoxData();
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
                ResetGradeValidationRules(tb, true);
                tb.Text = tb.Text.Trim();
                tb.PreviewKeyDown -= CalculateFinalGrade;
                tb.TextChanged -= CalculateFinalGrade;
                DataObject.RemovePastingHandler(tb, CalculateFinalGrade);
            }

            FinalGradeTextBox.Text = "F";
        }

        private void NonSubmissionCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            foreach (var tb in GetGradeTextBoxes())
            {
                tb.IsEnabled = true;
                ResetGradeValidationRules(tb, false);
                tb.PreviewKeyDown += CalculateFinalGrade;
                tb.TextChanged += CalculateFinalGrade;
                DataObject.AddPastingHandler(tb, CalculateFinalGrade);
            }

            FinalGradeTextBox.Text = Student.CalculateFinalGrade(GetCountedGradesDictionary());
        }

        public void CalculateFinalGrade(object sender, RoutedEventArgs e)
        {
            FinalGradeTextBox.Text = Student.CalculateFinalGrade(GetCountedGradesDictionary());
        }

        private Dictionary<char, int> GetCountedGradesDictionary()
        {
            var gradeList = (ComponentOneTextBoxG.Text + ComponentTwoTextBoxG.Text + ComponentThreeTextBoxG.Text).ToList();

            //Hardcoding grade keys here since those are not going to change or be dynamic anyway
            //Initializing new[]{char} array for example would just waste space in this scenario
            var dict = new Dictionary<char, int> { { 'A', gradeList.Count(c => c == 'A') } };

            //Counts the number of occurences and summs it up with the higher value grade then adds the key and pair
            //This assumes a grade "A" is also a grade "B", "C", "D" and etc. - this logic is taken into account in the
            //final grade calculating formula. 
            dict.Add('B', gradeList.Count(c => c == 'B') + dict['A']);
            dict.Add('C', gradeList.Count(c => c == 'C') + dict['B']);
            dict.Add('D', gradeList.Count(c => c == 'D') + dict['C']);
            dict.Add('E', gradeList.Count(c => c == 'E') + dict['D']);
            dict.Add('F', gradeList.Count(c => c == 'F') + dict['E']);

            return dict;
        }

        private void ResetGradeValidationRules(TextBox tb, bool isChecked)
        {
            var b = BindingOperations.GetBinding(tb, TextBox.TextProperty);
            if (b == null) return;
            var val = b.ValidationRules;
            val.Clear();
            val.Add(new TextBoxValidationRules { TextBoxRuleType = tb.Name.Substring(0, tb.Name.IndexOf("TextBox", StringComparison.Ordinal)), IsNonSubmission = isChecked });
        }

        private void RefreshStudentListImage_OnMouseEnter(object sender, MouseEventArgs e)
        {
            RefreshStudentListImage.Source = new BitmapImage(new Uri(@"/Images/refresh-icon-hover.png", UriKind.Relative));
        }

        private void RefreshStudentListImage_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BindListBoxData();
            RefreshStudentListImage.Source = new BitmapImage(new Uri(@"/Images/refresh-icon-clicked.png", UriKind.Relative));
        }

        private void RefreshStudentListImage_OnMouseLeave(object sender, MouseEventArgs e)
        {
            RefreshStudentListImage.Source = new BitmapImage(new Uri(@"/Images/refresh-icon.png", UriKind.Relative));
        }

        private void RebindListBoxRadioButtons_OnChecked(object sender, RoutedEventArgs e)
        {
            BindListBoxData();
        }
    }
}
