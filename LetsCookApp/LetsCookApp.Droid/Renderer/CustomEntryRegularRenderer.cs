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
using Android.Graphics;
using LetsCookApp.CustomViews;
using LetsCookApp.Droid.Renderer;

[assembly: ExportRenderer(typeof(CustomEntryRegular), typeof(CustomEntryRegularRenderer))]
namespace LetsCookApp.Droid.Renderer
{
    public class CustomEntryRegularRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
               
                var textView = (TextView)Control; // for example
               // Typeface font = Typeface.CreateFromAsset(Forms.Context.Assets, "OpenSans-Regular.ttf");
               // textView.Typeface = font;
               //   textView.SetBackgroundResource(Resource.Layout.EntryShape); 
                //   textView.SetHeight(80);
                

                textView.Background = null;
                // textView.Gravity = GravityFlags.CenterVertical;
                // do whatever you want to the textField here!
                // Control.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
            }
        }
    }
}