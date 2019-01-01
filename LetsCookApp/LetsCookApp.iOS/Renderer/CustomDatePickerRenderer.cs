using System.ComponentModel;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using LetsCookApp.CustomViews;
using LetsCookApp.iOS.Renderer;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace LetsCookApp.iOS.Renderer
{
    public class CustomDatePickerRenderer : DatePickerRenderer
    {

        CustomDatePicker customDatePicker;
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var view = (Element as CustomDatePicker);
                if (view != null)
                {
                    view.BackgroundColor = Xamarin.Forms.Color.Transparent;
                    SetFontSize(view);
                    customDatePicker = view;
                }

                //Control.Layer.BorderWidth = 1f;
                //Control.Layer.CornerRadius = 3f;
                //Control.Layer.BorderColor = Xamarin.Forms.Color.FromHex("#3D95AD").ToCGColor();
                Control.BorderStyle = UITextBorderStyle.None;
                Control.TextColor = UIKit.UIColor.FromRGB(0, 0, 0);
                Control.TintColor = UIKit.UIColor.FromRGB(158, 158, 158);
                Control.Font = UIFont.FromName("Roboto-Regular", Control.Font.PointSize);
                Control.Background = null;

                //Control.LeftView = new UIView(new CGRect(5, 10, 5, 10));
                //Control.LeftViewMode = UITextFieldViewMode.Always;
                //Control.RightView = new UIView(new CGRect(0, 0, 5, 0));
                //Control.RightViewMode = UITextFieldViewMode.Always;
            }
        }

        void SetFontSize(CustomDatePicker view)
        {
            if (view.FontSize != Font.Default.FontSize)
                Control.Font = UIFont.SystemFontOfSize((System.nfloat)view.FontSize);
            else if (view.FontSize == Font.Default.FontSize)
                Control.Font = UIFont.SystemFontOfSize(14f);
        }
    }
}