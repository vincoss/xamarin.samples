using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using System.ComponentModel;


namespace Xamarin_Samples.Xaml
{
    public class LabelVisibilityBehavior : Behavior<Label>
    {
        protected override void OnAttachedTo(Label entry)
        {
            entry.IsVisible = false;
            entry.PropertyChanged += Entry_PropertyChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Label entry)
        {
            entry.IsVisible = false;
            entry.PropertyChanged -= Entry_PropertyChanged;
            base.OnDetachingFrom(entry);
        }

        private void Entry_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var label = (Label)sender;
            if (label == null)
            {
                return;
            }

            if (e.PropertyName == nameof(Label.Text))
            {
                label.IsVisible = false;

                if (string.IsNullOrWhiteSpace(label.Text) == false)
                {
                    label.IsVisible = true;
                }
            }
        }

    }
}
