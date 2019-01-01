using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace LetsCookApp.CustomViews
{
    public class CustomRatingReadOnlyView : ContentView
    {
        Image staroff1, staron1, staroff2, staron2, staroff3, staron3, staroff4, staron4, staroff5, staron5;
        Image starhalfoff1, starhalfoff2, starhalfoff3, starhalfoff4, starhalfoff5;
        StackLayout stlLayout;

        public static readonly BindableProperty RatingProperty =
        BindableProperty.Create<CustomRatingReadOnlyView, double>(view => view.Rating, 0, BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) => { ((CustomRatingReadOnlyView)bindable).UpdateSelectedItem(); });

        public double Rating
        {
            get
            {
                return (double)GetValue(RatingProperty);
            }
            set
            {
                SetValue(RatingProperty, value);
            }
        }

        public static readonly BindableProperty OffStarImageProperty = BindableProperty.Create<CustomRatingReadOnlyView, ImageSource>(
            view => view.OffStar, "bigblankstar.png", BindingMode.TwoWay);

        public ImageSource OffStar
        {
            get { return (ImageSource)GetValue(OffStarImageProperty); }
            set { SetValue(OffStarImageProperty, value); }
        }

        public static readonly BindableProperty HalfStarImageProperty = BindableProperty.Create<CustomRatingView, ImageSource>(
            view => view.HalfStar, "bighalfstar.png", BindingMode.TwoWay);

        public ImageSource HalfStar
        {
            get { return (ImageSource)GetValue(HalfStarImageProperty); }
            set { SetValue(HalfStarImageProperty, value); }
        }


        public static readonly BindableProperty OnStarImageProperty = BindableProperty.Create<CustomRatingReadOnlyView, ImageSource>(view => view.OnStar,
            "biggreystar.png", BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) => { ((CustomRatingReadOnlyView)bindable).UpdateElementProperty(); });

        public ImageSource OnStar
        { get { return (ImageSource)GetValue(OnStarImageProperty); } set { SetValue(OnStarImageProperty, value); } }

        public static readonly BindableProperty StarSizeProperty = BindableProperty.Create<CustomRatingReadOnlyView, double>(view => view.StarSize, 16, BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) => { ((CustomRatingReadOnlyView)bindable).UpdateElementProperty(); });
        public double StarSize { get { return (double)GetValue(StarSizeProperty); } set { SetValue(StarSizeProperty, value); } }

        public static readonly BindableProperty StarSpaceProperty = BindableProperty.Create<CustomRatingReadOnlyView, double>(view => view.StarSpace, 3, BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) => { ((CustomRatingReadOnlyView)bindable).UpdateElementProperty(); });
        public double StarSpace { get { return (double)GetValue(StarSpaceProperty); } set { SetValue(StarSpaceProperty, value); } }
        public CustomRatingReadOnlyView()
        {
            #region Rating Control

            staroff1 = new Image();
            staron1 = new Image() { IsVisible = false };
            starhalfoff1 = new Image() { IsVisible = false };
            staroff2 = new Image();
            staron2 = new Image() { IsVisible = false };
            starhalfoff2 = new Image() { IsVisible = false };
            staroff3 = new Image();
            staron3 = new Image() { IsVisible = false };
            starhalfoff3 = new Image() { IsVisible = false };
            staroff4 = new Image();
            staron4 = new Image() { IsVisible = false };
            starhalfoff4 = new Image() { IsVisible = false };
            staroff5 = new Image();
            staron5 = new Image() { IsVisible = false };
            starhalfoff5 = new Image() { IsVisible = false };

            var grd1 = new Grid() { Children = { staroff1, staron1, starhalfoff1 } };
            var grd2 = new Grid() { Children = { staroff2, staron2, starhalfoff2 } };
            var grd3 = new Grid() { Children = { staroff3, staron3, starhalfoff3 } };
            var grd4 = new Grid() { Children = { staroff4, staron4, starhalfoff4 } };
            var grd5 = new Grid() { Children = { staroff5, staron5, starhalfoff5 } };
            stlLayout = new StackLayout { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.CenterAndExpand, Children = { grd1, grd2, grd3, grd4, grd5 } };

            #endregion

            Content = stlLayout;
        }

        void UpdateElementProperty()
        {
            stlLayout.Spacing = StarSpace;
            staroff1.HeightRequest = StarSize; staroff1.WidthRequest = StarSize; staroff1.Source = OffStar;
            staron1.HeightRequest = StarSize; staron1.WidthRequest = StarSize; staron1.Source = OnStar;
            staroff2.HeightRequest = StarSize; staroff2.WidthRequest = StarSize; staroff2.Source = OffStar;
            staron2.HeightRequest = StarSize; staron2.WidthRequest = StarSize; staron2.Source = OnStar;
            staroff3.HeightRequest = StarSize; staroff3.WidthRequest = StarSize; staroff3.Source = OffStar;
            staron3.HeightRequest = StarSize; staron3.WidthRequest = StarSize; staron3.Source = OnStar;
            staroff4.HeightRequest = StarSize; staroff4.WidthRequest = StarSize; staroff4.Source = OffStar;
            staron4.HeightRequest = StarSize; staron4.WidthRequest = StarSize; staron4.Source = OnStar;
            staroff5.HeightRequest = StarSize; staroff5.WidthRequest = StarSize; staroff5.Source = OffStar;
            staron5.HeightRequest = StarSize; staron5.WidthRequest = StarSize; staron5.Source = OnStar;

            starhalfoff1.HeightRequest = StarSize; starhalfoff1.WidthRequest = StarSize; starhalfoff1.Source = HalfStar;
            starhalfoff2.HeightRequest = StarSize; starhalfoff2.WidthRequest = StarSize; starhalfoff2.Source = HalfStar;
            starhalfoff3.HeightRequest = StarSize; starhalfoff3.WidthRequest = StarSize; starhalfoff3.Source = HalfStar;
            starhalfoff4.HeightRequest = StarSize; starhalfoff4.WidthRequest = StarSize; starhalfoff4.Source = HalfStar;
            starhalfoff5.HeightRequest = StarSize; starhalfoff5.WidthRequest = StarSize; starhalfoff5.Source = HalfStar;
        }

        void UpdateSelectedItem()
        {
            double ratevalue = Rating;
            staron5.IsVisible = staron4.IsVisible = staron3.IsVisible = staron2.IsVisible = staron1.IsVisible = false;
            starhalfoff1.IsVisible = starhalfoff2.IsVisible = starhalfoff3.IsVisible = starhalfoff4.IsVisible = starhalfoff5.IsVisible = false;

            if (ratevalue > 0 && ratevalue < 0.6)
            {
                starhalfoff1.IsVisible = true;
                staron5.IsVisible = staron4.IsVisible = staron3.IsVisible = staron2.IsVisible = staron1.IsVisible = false;
                starhalfoff2.IsVisible = starhalfoff3.IsVisible = starhalfoff4.IsVisible = starhalfoff5.IsVisible = false;
            }
            else if (ratevalue > 0.5 && ratevalue < 1.6)
            {
                starhalfoff2.IsVisible = staron1.IsVisible = true;
                staron5.IsVisible = staron4.IsVisible = staron3.IsVisible = staron2.IsVisible = false;
                starhalfoff1.IsVisible = starhalfoff3.IsVisible = starhalfoff4.IsVisible = starhalfoff5.IsVisible = false;
            }
            else if (ratevalue > 2 && ratevalue < 2.6)
            {
                starhalfoff3.IsVisible = staron1.IsVisible = staron2.IsVisible = true;
                staron5.IsVisible = staron4.IsVisible = staron3.IsVisible = false;
                starhalfoff1.IsVisible = starhalfoff2.IsVisible = starhalfoff4.IsVisible = starhalfoff5.IsVisible = false;
            }
            else if (ratevalue > 3 && ratevalue < 3.6)
            {
                starhalfoff4.IsVisible = staron2.IsVisible = staron1.IsVisible = staron3.IsVisible = true;
                staron5.IsVisible = staron4.IsVisible = false;
                starhalfoff2.IsVisible = starhalfoff1.IsVisible = starhalfoff3.IsVisible = starhalfoff5.IsVisible = false;
            }
            else if (ratevalue > 4 && ratevalue < 4.6)
            {
                starhalfoff5.IsVisible = staron3.IsVisible = staron2.IsVisible = staron1.IsVisible = staron4.IsVisible = true;
                staron5.IsVisible = false;
                starhalfoff2.IsVisible = starhalfoff3.IsVisible = starhalfoff1.IsVisible = starhalfoff4.IsVisible = false;
            }
            else if (ratevalue > 5 && ratevalue < 5.6)
            {
                starhalfoff5.IsVisible = staron4.IsVisible = staron3.IsVisible = staron2.IsVisible = staron1.IsVisible = true;
                staron5.IsVisible = false;
                starhalfoff2.IsVisible = starhalfoff3.IsVisible = starhalfoff4.IsVisible = starhalfoff1.IsVisible = false;
            }
            else if (ratevalue > 0.5 && ratevalue <= 1)
            {
                staron1.IsVisible = true;
                staron5.IsVisible = staron4.IsVisible = staron3.IsVisible = staron2.IsVisible = false;
                starhalfoff2.IsVisible = starhalfoff3.IsVisible = starhalfoff4.IsVisible = starhalfoff1.IsVisible = starhalfoff5.IsVisible = false;
            }
            else if (ratevalue > 1.5 && ratevalue <= 2)
            {
                staron2.IsVisible = staron1.IsVisible = true;
                staron5.IsVisible = staron4.IsVisible = staron3.IsVisible = false;
                starhalfoff2.IsVisible = starhalfoff3.IsVisible = starhalfoff4.IsVisible = starhalfoff1.IsVisible = starhalfoff5.IsVisible = false;
            }
            else if (ratevalue > 2.5 && ratevalue <= 3)
            {
                staron3.IsVisible = staron1.IsVisible = staron2.IsVisible = true;
                staron5.IsVisible = staron4.IsVisible = false;
                starhalfoff2.IsVisible = starhalfoff3.IsVisible = starhalfoff4.IsVisible = starhalfoff1.IsVisible = starhalfoff5.IsVisible = false;
            }
            else if (ratevalue > 3.5 && ratevalue <= 4)
            {
                staron4.IsVisible = staron1.IsVisible = staron3.IsVisible = staron2.IsVisible = true;
                staron5.IsVisible = false;
                starhalfoff2.IsVisible = starhalfoff3.IsVisible = starhalfoff4.IsVisible = starhalfoff1.IsVisible = starhalfoff5.IsVisible = false;
            }
            else if (ratevalue > 4.5 && ratevalue <= 5)
            {
                staron5.IsVisible = true;
                staron1.IsVisible = staron4.IsVisible = staron3.IsVisible = staron2.IsVisible = true;
                starhalfoff2.IsVisible = starhalfoff3.IsVisible = starhalfoff4.IsVisible = starhalfoff1.IsVisible = starhalfoff5.IsVisible = false;
            }
        }
    }
}
