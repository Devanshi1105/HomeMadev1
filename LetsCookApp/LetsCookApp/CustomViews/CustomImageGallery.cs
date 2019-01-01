using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LetsCookApp.CustomViews
{
    public class CustomImageGallery : View
    {
        public CustomImageGallery()
        {

        }

        //public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(CustomImageGallery), null, BindingMode.TwoWay, null, null, null, null);
        //public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(CustomImageGallery), null, BindingMode.TwoWay, null, null, null, null);

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(CustomImageGallery), null, BindingMode.TwoWay);
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(CustomImageGallery), null, BindingMode.TwoWay);

        //public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(CustomImageGallery), null);
        //public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(CustomImageGallery), null);

        // Properties
        //
        public IEnumerable ItemsSource
        {
            get
            {
                return (IEnumerable)base.GetValue(CustomImageGallery.ItemsSourceProperty);
            }
            set
            {
                base.SetValue(CustomImageGallery.ItemsSourceProperty, value);
            }
        }

        public DataTemplate ItemTemplate
        {
            get
            {
                return (DataTemplate)base.GetValue(CustomImageGallery.ItemTemplateProperty);
            }
            set
            {
                base.SetValue(CustomImageGallery.ItemTemplateProperty, value);
            }
        }

        public ICommand Clean;
    }
}
