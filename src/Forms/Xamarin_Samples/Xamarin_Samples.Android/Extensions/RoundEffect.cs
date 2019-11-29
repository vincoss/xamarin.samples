using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(Xamarin_Samples.Droid.Extensions.RoundEffect), nameof(Xamarin_Samples.Droid.Extensions.RoundEffect))]
namespace Xamarin_Samples.Droid.Extensions
{
    public class RoundEffect : PlatformEffect
    {
        ViewOutlineProvider originalProvider;
        Android.Views.View effectTarget;

        protected override void OnAttached()
        {
            try
            {
                effectTarget = Control ?? Container;
                originalProvider = effectTarget.OutlineProvider;
                effectTarget.OutlineProvider = new CornerRadiusOutlineProvider(Element);
                effectTarget.ClipToOutline = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to set corner radius: {ex.Message}");
            }
        }

        protected override void OnDetached()
        {
            if (effectTarget != null)
            {
                effectTarget.OutlineProvider = originalProvider;
                effectTarget.ClipToOutline = false;
            }
        }

        class CornerRadiusOutlineProvider : ViewOutlineProvider
        {
            Element element;

            public CornerRadiusOutlineProvider(Element formsElement)
            {
                element = formsElement;
            }

            public override void GetOutline(Android.Views.View view, Outline outline)
            {
                float scale = view.Resources.DisplayMetrics.Density;
                double width = (double)element.GetValue(VisualElement.WidthProperty) * scale;
                double height = (double)element.GetValue(VisualElement.HeightProperty) * scale;
                float minDimension = (float)Math.Min(height, width);
                float radius = minDimension / 2f;
                Rect rect = new Rect(0, 0, (int)width, (int)height);
                outline.SetRoundRect(rect, radius);
            }
        }
    }
}