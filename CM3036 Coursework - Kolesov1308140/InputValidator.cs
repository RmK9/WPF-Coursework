using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CM3036_Coursework___Kolesov1308140
{
    public static class InputValidator
    {
        #region InputValidator - Allow only numbers input handling

        public static void AllowOnlyNumbers_OnKeyDown(object sender, KeyEventArgs e)
        {
            //Get Key from event, cast it to string and then check if it's not a digit with regex
            //AND if pressed Key is not a backspace
            if (Regex.IsMatch(KeyToCharCaster.GetCharFromKey(e.Key).ToString(), @"[^0-9]") &&
                e.Key != Key.Back && !IsModifierOrArrowKeyDown(e))
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
                e.Key != Key.Back && !IsModifierOrArrowKeyDown(e))
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
                e.Key != Key.Back && !IsModifierOrArrowKeyDown(e))
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

        public static bool IsModifierOrArrowKeyDown(KeyEventArgs e)
        {
            return ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) ||
                   ((Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt) ||
                   (e.Key == Key.Left) || (e.Key == Key.Right) || (e.Key == Key.Up) || (e.Key == Key.Down);
        }

    }

}
