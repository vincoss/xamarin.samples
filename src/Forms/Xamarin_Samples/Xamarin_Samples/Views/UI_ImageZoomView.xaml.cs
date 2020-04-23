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
        See PinchGesture project sample

        Resources
        https://github.com/AlexPshul/SvgXFInteractive
        https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/graphics/skiasharp/bitmaps/segmented
        https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/graphics/skiasharp/
        https://stackoverflow.com/questions/55493569/how-to-achieve-pinch-zoom-swipe-on-skcanvasview
        https://www.pshul.com/2018/02/01/xamarin-forms-interactive-svg-image-using-skiasharp-with-pan-and-zoom/
        https://stackoverflow.com/questions/49766941/how-to-do-zooming-and-panning-in-image-using-xamarin-cross-platform
        https://forums.xamarin.com/discussion/74168/full-screen-image-viewer-with-pinch-to-zoom-pan-to-move-tap-to-show-captions-for-xamarin-forms
        https://www.c-sharpcorner.com/article/zoom-in/
        https://www.c-sharpcorner.com/article/how-to-load-large-images-in-xamarin-android-app-using-visual-studio-2015/
        https://stackoverflow.com/questions/40159715/xamarin-display-image-at-real-size
        https://docs.microsoft.com/en-us/samples/xamarin/monodroid-samples/loadinglargebitmaps/
        https://stackoverflow.com/questions/52225049/get-original-height-and-width-of-a-cached-images-source-in-xamarin-forms
        https://stackoverflow.com/questions/59541004/how-to-animate-scaling-a-view-to-fit-the-screen-size'
        https://medium.com/a-developer-in-making/how-to-work-with-images-in-xamarin-forms-4d2e8b3bd1c9
        https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/graphics/skiasharp/bitmaps/displaying
        https://forums.xamarin.com/discussion/101371/skiasharp-zoom
        https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/graphics/skiasharp/transforms/touch
        https://stackoverflow.com/questions/37679872/how-to-determine-location-of-a-tap-in-xamarin-forms
        https://forums.xamarin.com/discussion/74168/full-screen-image-viewer-with-pinch-to-zoom-pan-to-move-tap-to-show-captions-for-xamarin-forms

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
            //if (Scale > MIN_SCALE)
            //    switch (e.StatusType)
            //    {
            //        case GestureStatus.Started:
            //            StartX = (1 - AnchorX) * Width;
            //            StartY = (1 - AnchorY) * Height;
            //            break;
            //        case GestureStatus.Running:
            //            TranslationX = Clamp(LastX + e.TotalX * Scale, -Width / 2, Width / 2);
            //            TranslationY = Clamp(LastY + e.TotalY * Scale, -Height / 2, Height / 2);
            //            break;
            //    }

            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    StartX = (1 - AnchorX) * Width;
                    StartY = (1 - AnchorY) * Height;
                    break;
                case GestureStatus.Running:
                    AnchorX = Clamp(1 - (StartX + e.TotalX) / Width, 0, 1);
                    AnchorY = Clamp(1 - (StartY + e.TotalY) / Height, 0, 1);
                    break;
            }

        }

        double currentScale = 1;
        double startScale = 1;
        double xOffset = 0;
        double yOffset = 0;

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

           // img.TranslateTo(0, 0, 0, Easing.Linear);
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

    public static class DoubleExtensions
    {
        public static double Clamp(this double self, double min, double max)
        {
            return Math.Min(max, Math.Max(self, min));
        }
    }

    public class PinchToZoomContainer : ContentView
    {
        private const double MIN_SCALE = 1;
        private const double MAX_SCALE = 4;
        double x, y;
        private double LastX, LastY;
        private double StartX, StartY;

        double currentScale = 1;
        double startScale = 1;
        double xOffset = 0;
        double yOffset = 0;

        public PinchToZoomContainer()
        {
            //var pinch = new PinchGestureRecognizer();
            //pinch.PinchUpdated += OnPinchUpdated;
            //GestureRecognizers.Add(pinch);

            var tap = new TapGestureRecognizer();
            tap.NumberOfTapsRequired = 2; // TOOD: dependecy property default 2
            tap.Tapped += TapGesture_Tapped;
            GestureRecognizers.Add(tap);

            var pan = new PanGestureRecognizer();
            pan.PanUpdated += Pan_PanUpdated;
            GestureRecognizers.Add(pan);


        }

        private void Pan_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (Content.Scale <= MIN_SCALE)
            {
                return;
            }

            //switch (e.StatusType)
            //{
            //    case GestureStatus.Started:
            //        StartX = (1 - Content.AnchorX) * Content.Width;
            //        StartY = (1 - Content.AnchorY) * Content.Height;
            //        break;
            //    case GestureStatus.Running:
            //        Content.AnchorX = Clamp(1 - (StartX + e.TotalX) / Content.Width, 0, 1);
            //        Content.AnchorY = Clamp(1 - (StartY + e.TotalY) / Content.Height, 0, 1);
            //        break;
            //}

            var w = Content.Width * Content.Scale;
            var h = Content.Height * Content.Scale;

            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
                    Content.TranslationX = Math.Max(Math.Min(0, x + e.TotalX), -Math.Abs(w - App.ScreenWidth));
                    Content.TranslationY = Math.Max(Math.Min(0, y + e.TotalY), -Math.Abs(h - App.ScreenHeight));
                    break;

                case GestureStatus.Completed:
                    // Store the translation applied during the pan
                    x = Content.TranslationX;
                    y = Content.TranslationY;
                    break;
            }
        }

        private void TapGesture_Tapped(object sender, EventArgs e)
        {
            if (Content.Scale > MIN_SCALE)
            {
                Content.ScaleTo(MIN_SCALE, 250, Easing.CubicInOut);
                Content.TranslateTo(0, 0, 250, Easing.CubicInOut);
            }
            else
            {
                // TODO tapped position if better, otherwise keep center.
                Content.AnchorX = .5;
                Content.AnchorY = .5;
                Content.ScaleTo(MAX_SCALE, 250, Easing.CubicInOut);
            }

            var parent = Content.Parent;
        
        }

        void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            if (e.Status == GestureStatus.Started)
            {
                // Store the current scale factor applied to the wrapped user interface element,
                // and zero the components for the center point of the translate transform.
                startScale = Content.Scale;

                Content.AnchorX = 0;
                Content.AnchorY = 0;
            }
            if (e.Status == GestureStatus.Running)
            {
                // Calculate the scale factor to be applied.
                currentScale += (e.Scale - 1) * startScale;
                currentScale = Math.Max(1, currentScale);

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the X pixel coordinate.
                double renderedX = Content.X + xOffset;
                double deltaX = renderedX / Width;
                double deltaWidth = Width / (Content.Width * startScale);
                double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the Y pixel coordinate.
                double renderedY = Content.Y + yOffset;
                double deltaY = renderedY / Height;
                double deltaHeight = Height / (Content.Height * startScale);
                double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                // Calculate the transformed element pixel coordinates.
                double targetX = xOffset - (originX * Content.Width) * (currentScale - startScale);
                double targetY = yOffset - (originY * Content.Height) * (currentScale - startScale);

                // Apply translation based on the change in origin.
                Content.TranslationX = targetX.Clamp(-Content.Width * (currentScale - 1), 0);
                Content.TranslationY = targetY.Clamp(-Content.Height * (currentScale - 1), 0);

                // Apply scale factor
                Content.Scale = currentScale;
            }

            if (e.Status == GestureStatus.Completed)
            {
                // Store the translation delta's of the wrapped user interface element.
                xOffset = Content.TranslationX;
                yOffset = Content.TranslationY;
            }
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