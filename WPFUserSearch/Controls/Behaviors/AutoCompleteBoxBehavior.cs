using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace WPFUserSearch.Controls.Behaviors
{
    public class AutoCompleteBoxKeyDownBehavior : Behavior<UIElement>
    {
        protected override void OnAttached()
        {
            AutoCompleteBox autoCompleteBox = (AutoCompleteBox)AssociatedObject;

            autoCompleteBox.KeyUp -= AutoCompleteBox_KeyUp;
            autoCompleteBox.KeyUp += AutoCompleteBox_KeyUp;
        }

        private void AutoCompleteBox_KeyUp(object sender, KeyEventArgs e)
        {
            AutoCompleteBox autoCompleteBox = (AutoCompleteBox)sender;

            if (e.Key == Key.Enter)
            {
                if (autoCompleteBox.InputBindings != null && autoCompleteBox.InputBindings.Count > 0 &&
                    autoCompleteBox.InputBindings[0].GetType().Equals(typeof(KeyBinding)))
                {
                    KeyBinding keyBinding = autoCompleteBox.InputBindings[0] as KeyBinding;

                    if(keyBinding.Command != null)
                    {
                        keyBinding.Command.Execute(keyBinding.CommandParameter);
                    }
                }
            }
        }
    }
}
