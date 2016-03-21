using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using MahApps.Metro.Controls;

namespace CM3036_Coursework___Kolesov1308140
{
    public static class InputValidationHelper
    {
        #region InputValidator - Allow only numbers input handling

        public static void AllowOnlyNumbers_OnKeyDown(object sender, KeyEventArgs e)
        {
            //Get Key from event, cast it to string and then check if it's not a digit with regex
            //AND if pressed Key is not a backspace
            if (Regex.IsMatch(KeyToCharCaster.GetCharFromKey(e.Key).ToString(), @"[^0-9]") &&
                e.Key != Key.Back && !IsModifierOrSpecialKeyDown(e))
            {
                //Prevent input of forbidden character
                e.Handled = true;
            }

        }

        public static void AllowOnlyNumbers_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = e.Source as TextBox;
            if (textBox != null && Regex.IsMatch(textBox.Text, @"[^0-9]"))
            {
                e.Handled = true;
            }
        }

        public static void AllowOnlyNumbers_PasteHandler(object sender, DataObjectPastingEventArgs e)
        {
            TextBox tb = sender as TextBox;
            bool allow = false;

            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string pasteText = e.DataObject.GetData(typeof(string)) as string;
                if (pasteText != null && Regex.IsMatch(pasteText, @"[0-9]"))
                {
                    allow = true;
                }
            }

            if (!allow)
            {
                e.CancelCommand();
            }

        }

        #endregion

        #region InputValidator - Allow only Alphabetical Characters input handling

        public static void AllowOnlyAlphabeticalChars_OnKeyDown(object sender, KeyEventArgs e)
        {
            //Get Key from event, cast it to string and then check if it's not a digit with regex
            //AND if pressed Key is not a backspace
            if (Regex.IsMatch(KeyToCharCaster.GetCharFromKey(e.Key).ToString(), @"^[^a-z^A-Z^'^\.]+$") &&
                e.Key != Key.Back && !IsModifierOrSpecialKeyDown(e))
            {
                //Prevent input of forbidden character
                e.Handled = true;
            }

        }

        public static void AllowOnlyAlphabeticalChars_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = e.Source as TextBox;
            if (textBox != null && Regex.IsMatch(textBox.Text, @"^[^a-z^A-Z^'^\.]+$"))
            {
                e.Handled = true;
            }
        }

        public static void AllowOnlyAlphabeticalChars_PasteHandler(object sender, DataObjectPastingEventArgs e)
        {
            TextBox tb = sender as TextBox;
            bool allow = false;

            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string pasteText = e.DataObject.GetData(typeof(string)) as string;
                if (pasteText != null && Regex.IsMatch(pasteText, @"^[a-zA-Z'\.]+$"))
                {
                    allow = true;
                }
            }

            if (!allow)
            {
                e.CancelCommand();
            }

        }

        #endregion

        #region InputValidator - Allow only Grade Letters input handling

        public static void AllowOnlyGradeLetters_OnKeyDown(object sender, KeyEventArgs e)
        {
            //Get Key from event, cast it to string and then check if it's not a digit with regex
            //AND if pressed Key is not a backspace
            if (Regex.IsMatch(KeyToCharCaster.GetCharFromKey(e.Key).ToString(), @"^[^ABCDEFabcdef]+$") &&
                e.Key != Key.Back && !IsModifierOrSpecialKeyDown(e))
            {
                //Prevent input of forbidden character
                e.Handled = true;
            }

        }

        public static void AllowOnlyGradeLetters_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = e.Source as TextBox;
            if (textBox != null && Regex.IsMatch(textBox.Text, @"^[^ABCDEFabcdef]+$"))
            {
                e.Handled = true;
            }
        }

        public static void AllowOnlyGradeLetters_PasteHandler(object sender, DataObjectPastingEventArgs e)
        {
            TextBox tb = sender as TextBox;
            bool allow = false;

            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string pasteText = e.DataObject.GetData(typeof(string)) as string;
                if (pasteText != null && Regex.IsMatch(pasteText, @"^[ABCDEFabcdef]+$"))
                {
                    allow = true;
                }
            }

            if (!allow)
            {
                e.CancelCommand();
            }

        }

        #endregion

        #region InputValidator - Helper methods
        public static bool IsModifierOrSpecialKeyDown(KeyEventArgs e)
        {
            return ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) ||
                   ((Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt) ||
                   (e.Key == Key.Left) || (e.Key == Key.Right) || (e.Key == Key.Up) ||
                   (e.Key == Key.Down) || (e.Key == Key.Tab);
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

        public static void AttachTextBoxInputEventHandlers(MetroWindow mw)
        {
            foreach (TextBox tb in FindLogicalChildren<TextBox>(mw))
            {
                var textBoxType = tb.Name.Substring(tb.Name.Length - 1);
                switch (textBoxType)
                {
                    case "N":
                        tb.PreviewKeyDown += InputValidationHelper.AllowOnlyNumbers_OnKeyDown;
                        tb.TextChanged += InputValidationHelper.AllowOnlyNumbers_TextChanged;
                        DataObject.AddPastingHandler(tb, InputValidationHelper.AllowOnlyNumbers_PasteHandler);
                        break;
                    case "S":
                        tb.PreviewKeyDown += InputValidationHelper.AllowOnlyAlphabeticalChars_OnKeyDown;
                        tb.TextChanged += InputValidationHelper.AllowOnlyAlphabeticalChars_TextChanged;
                        DataObject.AddPastingHandler(tb, InputValidationHelper.AllowOnlyAlphabeticalChars_PasteHandler);
                        break;
                    case "G":
                        tb.PreviewKeyDown += InputValidationHelper.AllowOnlyGradeLetters_OnKeyDown;
                        tb.TextChanged += InputValidationHelper.AllowOnlyGradeLetters_TextChanged;
                        DataObject.AddPastingHandler(tb, InputValidationHelper.AllowOnlyGradeLetters_PasteHandler);
                        break;
                }
            }
        }

        public static string GetErrorListForWrongInput(MetroWindow mw)
        {
            var errorList = "";
            var NSIsChecked = FindLogicalChildren<CheckBox>(mw).First().IsChecked??false;

            foreach (TextBox tb in FindLogicalChildren<TextBox>(mw))
            {
                var textBoxType = tb.Name.Substring(tb.Name.Length - 1);
                if (textBoxType == "N" && tb.Text.Length < 1)
                {
                    errorList += "Matriculation Number must be a number between 1 and 20 characters.\n";
                }
                else if (textBoxType == "S" && tb.Text.Length < 2)
                {
                    errorList += "Student First and Last names must be between 2 and 50 alphabetical characters each.\n";
                }
                else if ((textBoxType == "G" && tb.Name.Contains("One")) && tb.Text.Length != 3 && !NSIsChecked)
                {
                    errorList += "Component One must contain Grades (A, B, C, D, E, F) 3 characters long exactly.\n";
                }
                else if ((textBoxType == "G" && tb.Name.Contains("Two")) && tb.Text.Length != 5 && !NSIsChecked)
                {
                    errorList += "Component Two must contain Grades (A, B, C, D, E, F) 5 characters long exactly.\n";
                }
                else if ((textBoxType == "G" && tb.Name.Contains("Three")) && tb.Text.Length != 2 && !NSIsChecked)
                {
                    errorList += "Component Three must contain Grades (A, B, C, D, E, F) 2 characters long exactly.\n";
                }
            }

            return errorList;
        } 
        #endregion
    }

}
