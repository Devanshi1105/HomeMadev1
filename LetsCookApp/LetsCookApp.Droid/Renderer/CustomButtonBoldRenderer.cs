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

[assembly: ExportRenderer(typeof(CustomButtonBold), typeof(CustomButtonBoldRenderer))]
namespace LetsCookApp.Droid.Renderer
{
    internal class CustomButtonBoldRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var button = (Android.Widget.Button)Control; // for example
                Typeface font = Typeface.CreateFromAsset(Forms.Context.Assets, "Uniform Bold.otf");
                button.SetTypeface(font, TypefaceStyle.Normal);

                // do whatever you want to the textField here!
                // Control.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
            }
        }
    }
}