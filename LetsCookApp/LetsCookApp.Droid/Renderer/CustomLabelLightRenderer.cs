using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using LetsCookApp.CustomViews;
using LetsCookApp.Droid.Renderer;
using Android.Graphics;

[assembly: ExportRenderer(typeof(CustomLabelLight), typeof(CustomLabelLightRenderer))]
namespace LetsCookApp.Droid.Renderer
{
    public class CustomLabelLightRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var label = (TextView)Control; // for example
               // Typeface font = Typeface.CreateFromAsset(Forms.Context.Assets, "Uniform Light.otf");
               // label.Typeface = font;
                // do whatever you want to the textField here!
                // Control.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
            }
        }
    }
}