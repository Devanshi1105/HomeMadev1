using Android.Graphics;
using LetsCookApp.CustomViews;
using LetsCookApp.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace LetsCookApp.Droid.Renderer
{
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        CustomDatePicker customDatePicker;
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                var datePicker = (Element as CustomDatePicker);
                if (datePicker != null)
                {
                    customDatePicker = datePicker;
                    SetFontSize(datePicker);
                    SetTextColor(datePicker);
                    //Typeface font = Typeface.CreateFromAsset(Forms.Context.Assets, "Roboto-Regular.ttf");
                    //this.Control.Typeface = font;
                    Control.Background = null;
                    //Control.SetBackgroundResource(Resource.Layout.Datepicker);
                }
            }
        }

        private void SetTextColor(CustomDatePicker view)
        {
            this.Control.SetTextColor(Android.Graphics.Color.Rgb(0, 0, 0));
            this.Control.SetHintTextColor(Android.Graphics.Color.Rgb(158, 158, 158));
        }

        private void SetFontSize(CustomDatePicker view)
        {
            if (view.FontSize != Font.Default.FontSize)
            {
                Control.SetTextSize(Android.Util.ComplexUnitType.Dip, (float)view.FontSize);
            }
            else if (view.FontSize == Font.Default.FontSize)
                Control.SetTextSize(Android.Util.ComplexUnitType.Dip, (float)14);
        }
    }
}