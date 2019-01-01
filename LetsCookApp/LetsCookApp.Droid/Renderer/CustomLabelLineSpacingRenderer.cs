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
using LetsCookApp.Droid.Renderer;
using LetsCookApp.CustomViews;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;

[assembly: ExportRenderer(typeof(CustomLabelLineSpacing), typeof(CustomLabelLineSpacingRenderer))]
namespace LetsCookApp.Droid.Renderer
{
    

    public class CustomLabelLineSpacingRenderer : LabelRenderer
    {
        protected CustomLabelLineSpacing LineSpacingLabel { get; private set; }
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                this.LineSpacingLabel = (CustomLabelLineSpacing)this.Element;
            }

            var lineSpacing = this.LineSpacingLabel.LineSpacing;

            this.Control.SetLineSpacing(1f, (float)lineSpacing);

            Typeface font = Typeface.CreateFromAsset(Forms.Context.Assets, "Uniform.otf");
            this.Control.Typeface = font;
            this.UpdateLayout();

        }
    }
}