using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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

            ResetTextBoxesAndBindValidationRules();
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

        private void ResetTextBoxesAndBindValidationRules()
        {
            foreach (var tb in InputValidationHelper.FindLogicalChildren<TextBox>(this))
            {
                tb.Text = "";
                var b = BindingOperations.GetBinding(tb, TextBox.TextProperty);
                if (b == null) return;
                var val = b.ValidationRules;
                val.Clear();
                val.Add(new TextBoxValidationRules { TextBoxRuleType = tb.Name.Substring(0, tb.Name.IndexOf("TextBox", StringComparison.Ordinal)) });
            }
        }

        private void AddStudentButton_OnClick(object sender, RoutedEventArgs e)
        {
            var studentEntities = new StudentEntities();

            var errorList = InputValidationHelper.GetErrorListForWrongInput(this);

            //If no errors
            if (errorList == "")
            {
                try
                {
                    //Force open the connection
                    studentEntities.Database.Connection.Open();
                    var newStudent = new Student();

                    newStudent.matriculationNumber = int.Parse(MatriculationNumberTextBoxN.Text);
                    newStudent.firstName = FirstNameTextBoxS.Text;
                    newStudent.lastName = LastNameTextBoxS.Text;
                    if (!NonSubmissionCheckBox.IsChecked ?? false)
                    {
                        newStudent.componentOne = ComponentOneTextBoxG.Text;
                        newStudent.componentTwo = ComponentTwoTextBoxG.Text;
                        newStudent.componentThree = ComponentThreeTextBoxG.Text;
                        newStudent.nonSubmission = false;
                    }
                    else
                    {
                        newStudent.componentOne = "";
                        newStudent.componentTwo = "";
                        newStudent.componentThree = "";
                        newStudent.nonSubmission = true;
                    }
                    newStudent.finalGrade = FinalGradeTextBox.Text;

                    studentEntities.Students.Add(newStudent);

                    //Save change made to DB
                    studentEntities.SaveChanges();
                    //Refresh the listbox
                    ((MainWindow)Owner).BindListBoxData();
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
            else
            {
                MessageBox.Show("Please verify your input, error:\n\n" + errorList);
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

        private void ResetGradeValidationRules(TextBox tb, bool isChecked)
        {
            var b = BindingOperations.GetBinding(tb, TextBox.TextProperty);
            if (b == null) return;
            var val = b.ValidationRules;
            val.Clear();
            val.Add(new TextBoxValidationRules { TextBoxRuleType = tb.Name.Substring(0, tb.Name.IndexOf("TextBox", StringComparison.Ordinal)), IsNonSubmission = isChecked });
        }
    }
}
