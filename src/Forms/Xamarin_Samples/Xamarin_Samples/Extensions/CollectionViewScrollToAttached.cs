using System;
using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;


namespace Xamarin_Samples.Extensions
{
    public class CollectionViewScrollToAttached
    {
        public static BindableProperty ScrollToIndexProperty = BindableProperty.CreateAttached("ScrollToIndex", typeof(int), typeof(int), 0, propertyChanged: HandleChanged);

        public static int GetScrollToIndex(BindableObject view)
        {
            return (int)view.GetValue(ScrollToIndexProperty);
        }

        public static void SetScrollToIndex(BindableObject view, int value)
        {
            view.SetValue(ScrollToIndexProperty, value);
        }

        private static void HandleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as CollectionView;

            if (control == null)
            {
                return;
            }

            var nindex = 0;
            var oindex = 0;

            if (newValue != null)
            {
                int.TryParse(newValue.ToString(), out nindex);
            }
            
            if (oldValue != null)
            {
                int.TryParse(oldValue.ToString(), out oindex);
            }

            control.ScrollTo(nindex, -1, ScrollToPosition.Center);
        }
    }
}