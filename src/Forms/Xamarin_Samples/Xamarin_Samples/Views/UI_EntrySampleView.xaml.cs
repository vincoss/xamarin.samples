using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UI_EntrySampleView : ContentPage
    {
        public UI_EntrySampleView()
        {
            InitializeComponent();
        }

        void EntryCompleted(object sender, EventArgs e)
        {
            var editor = (Entry)sender;

            labelInteractivity.Text = editor.Text;
        }

        void EntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var oldText = e.OldTextValue;
            var newText = e.NewTextValue;

            labelInteractivity.Text = newText;
        }

        private void Entry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var editor = (Entry)sender;

            if (e.PropertyName == nameof(Entry.Text))
            {
                labelInteractivity.Text = editor.Text;
            }
        }

        private void Entry_FocusedAndUnFocused(object sender, FocusEventArgs e)
        {
            var editor = (Entry)sender;
            labelInteractivity.Text = $"Focused: {e.IsFocused}, Value: {editor.Text}";
        }
    }
}