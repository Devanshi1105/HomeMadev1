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
using Xamarin.Forms;
using LetsCookApp.CustomViews;
using LetsCookApp.Droid.Renderer;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;

[assembly: ExportRenderer(typeof(CustomLabelRegular), typeof(CustomLabelRegularRenderer))]
namespace LetsCookApp.Droid.Renderer
{
   
    public class CustomLabelRegularRenderer : LabelRenderer
    {
        protected CustomLabelRegular LineSpacingLabel { get; private set; }
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                //var label = (TextView)Control; // for example
                //Typeface font = Typeface.CreateFromAsset(Forms.Context.Assets, "Uniform.otf");
                //label.Typeface = font;
                // do whatever you want to the textField here!
                // Control.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
            }

         
        }
    }
}