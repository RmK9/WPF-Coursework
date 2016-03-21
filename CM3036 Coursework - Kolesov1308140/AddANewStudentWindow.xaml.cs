using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CM3036_Coursework___Kolesov1308140
{
    /// <summary>
    /// Interaction logic for AddANewStudentWindow.xaml
    /// </summary>
    public partial class AddANewStudentWindow
    {
        public AddANewStudentWindow()
        {
            InitializeComponent();

            InputValidationHelper.AttachTextBoxInputEventHandlers(this);

            BindFinalGradeCalculation_OnInputChange();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BindFinalGradeCalculation_OnInputChange()
        {
            foreach (var tb in GetGradeTextBoxes())
            {
                tb.PreviewKeyDown += CalculateFinalGrade;
                tb.TextChanged += CalculateFinalGrade;
                DataObject.AddPastingHandler(tb, CalculateFinalGrade);
            }
        }

        private void CalculateFinalGrade(object sender, RoutedEventArgs e)
        {
            FinalGradeTextBox.Text = Student.CalculateFinalGrade(GetCountedGradesDictionary());
        }

        private Dictionary<char, int> GetCountedGradesDictionary()
        {
            var gradeList = (ComponentOneTextBoxG.Text + ComponentTwoTextBoxG.Text + ComponentThreeTextBoxG.Text).ToList();

            //Hardcoding grade keys here since those are not going to change or be dynamic anyway
            //Initializing new[]{char} array for example would just waste space in this scenario
            var dict = new Dictionary<char, int>{{'A', gradeList.Count(c => c == 'A')}};

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

        private void AddStudentButton_OnClick(object sender, RoutedEventArgs e)
        {
            var studentEntities = new StudentEntities();

            try
            {
                //Force open the connection
                studentEntities.Database.Connection.Open();
                studentEntities.Students.Add(new Student
                {
                    matriculationNumber = int.Parse(MatriculationNumberTextBoxN.Text),
                    firstName = FirstNameTextBoxS.Text,
                    lastName = LastNameTextBoxS.Text,
                    componentOne = ComponentOneTextBoxG.Text,
                    componentTwo = ComponentTwoTextBoxG.Text,
                    componentThree = ComponentThreeTextBoxG.Text,
                    finalGrade = FinalGradeTextBox.Text
                });

                //Save change made to DB
                studentEntities.SaveChanges();
                //Refresh the listbox
                ((MainWindow) Owner).BindListBoxData();
                //Close secondary window
                Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error while trying to add a Student to Database\nError Message: " + ex.Message +
                                "\nPlease try to run the application again.");
            }
            finally
            {
                //Close the connection
                studentEntities.Database.Connection.Close();
            }
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

            FinalGradeTextBox.Text = "F";
        }

        private void NonSubmissionCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            foreach (var tb in GetGradeTextBoxes())
            {
                tb.IsEnabled = true;
            }

            FinalGradeTextBox.Text = Student.CalculateFinalGrade(GetCountedGradesDictionary());
        }
    }
}
