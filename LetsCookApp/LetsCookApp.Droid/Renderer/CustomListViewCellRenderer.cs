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
using LetsCookApp.CustomViews;
using LetsCookApp.Droid.Renderer;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(CustomListViewCell), typeof(CustomListViewCellRenderer))]
namespace LetsCookApp.Droid.Renderer
{
    public class CustomListViewCellRenderer : ViewCellRenderer
    {
        protected override Android.Views.View GetCellCore(Xamarin.Forms.Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
        {
            var view = base.GetCellCore(item, convertView, parent, context);
            Android.Views.View v = new Android.Views.View(context);
            if (view != null)
            {
                view.Focusable = false;
            }
            return view;
        }
    }
}