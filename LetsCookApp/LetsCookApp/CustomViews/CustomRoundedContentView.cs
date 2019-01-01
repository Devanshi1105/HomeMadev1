using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LetsCookApp.CustomViews
{
    public class CustomRoundedContentView : ContentView
    {
        public static readonly BindableProperty CornerRaidusProperty =
            BindableProperty.Create<CustomRoundedContentView, double>(view => view.CornerRadius,0);


        public double CornerRadius
        {
            get { return (double)GetValue(CornerRaidusProperty); }
            set { SetValue(CornerRaidusProperty, value); }
        }
    }
}
