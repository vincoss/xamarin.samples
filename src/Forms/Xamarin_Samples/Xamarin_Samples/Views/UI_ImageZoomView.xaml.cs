using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Samples.Views
{
    /*
        Image Zoom -  Pan, Pinch, Tap 

        Resources
        https://stackoverflow.com/questions/49766941/how-to-do-zooming-and-panning-in-image-using-xamarin-cross-platform
        https://forums.xamarin.com/discussion/74168/full-screen-image-viewer-with-pinch-to-zoom-pan-to-move-tap-to-show-captions-for-xamarin-forms
        https://www.c-sharpcorner.com/article/zoom-in/
    */

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UI_ImageZoomView : ContentPage
    {
        private const double MIN_SCALE = 1;
        private const double MAX_SCALE = 4;
        private const double OVERSHOOT = 0.15;
        private double StartScale;
        private double LastX, LastY;
        private double StartX, StartY;

        public UI_ImageZoomView()
        {
            InitializeComponent();

            Scale = MIN_SCALE;
            TranslationX = TranslationY = 0;
            AnchorX = AnchorY = 0;
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            Scale = MIN_SCALE;
            TranslationX = TranslationY = 0;
            AnchorX = AnchorY = 0;
            return base.OnMeasure(widthConstraint, heightConstraint);
        }

        private void OnTapped(object sender, EventArgs e)
        {
            if (Scale > MIN_SCALE)
            {
                this.ScaleTo(MIN_SCALE, 250, Easing.CubicInOut);
                this.TranslateTo(0, 0, 250, Easing.CubicInOut);
            }
            else
            {
                AnchorX = AnchorY = 0.5; //TODO tapped position
                this.ScaleTo(MAX_SCALE, 250, Easing.CubicInOut);
            }
        }

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (Scale > MIN_SCALE)
                switch (e.StatusType)
                {
                    case GestureStatus.Started:
                        StartX = (1 - AnchorX) * Width;
                        StartY = (1 - AnchorY) * Height;
                        break;
                    case GestureStatus.Running:
                        TranslationX = Clamp(LastX + e.TotalX * Scale, -Width / 2, Width / 2);
                        TranslationY = Clamp(LastY + e.TotalY * Scale, -Height / 2, Height / 2);
                        break;
                }

            //switch (e.StatusType)
            //{
            //    case GestureStatus.Started:
            //        StartX = (1 - AnchorX) * Width;
            //        StartY = (1 - AnchorY) * Height;
            //        break;
            //    case GestureStatus.Running:
            //        AnchorX = Clamp(1 - (StartX + e.TotalX) / Width, 0, 1);
            //        AnchorY = Clamp(1 - (StartY + e.TotalY) / Height, 0, 1);
            //        break;
            //}

        }

        private void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            switch (e.Status)
            {
                case GestureStatus.Started:
                    StartScale = Scale;
                    AnchorX = e.ScaleOrigin.X;
                    AnchorY = e.ScaleOrigin.Y;
                    break;
                case GestureStatus.Running:
                    double current = Scale + (e.Scale - 1) * StartScale;
                    Scale = Clamp(current, MIN_SCALE * (1 - OVERSHOOT), MAX_SCALE * (1 + OVERSHOOT));
                    break;
                case GestureStatus.Completed:
                    if (Scale > MAX_SCALE)
                        this.ScaleTo(MAX_SCALE, 250, Easing.SpringOut);
                    else if (Scale < MIN_SCALE)
                        this.ScaleTo(MIN_SCALE, 250, Easing.SpringOut);
                    break;
            }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called

            img.TranslateTo(0, 0, 0, Easing.Linear);
        }

        private T Clamp<T>(T value, T minimum, T maximum) where T : IComparable
        {
            if (value.CompareTo(minimum) < 0)
                return minimum;
            else if (value.CompareTo(maximum) > 0)
                return maximum;
            else
                return value;
        }

    }
}