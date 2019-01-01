using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Webkit;
using System.Threading.Tasks;
using Android.Graphics;
using LetsCookApp.CustomViews;
using LetsCookApp.Droid.Renderer;
using System.Net;
using LetsCookApp.Droid.Helper;

[assembly: ExportRenderer(typeof(CustomImageGallery), typeof(CustomImageGalleryRenderer))]
namespace LetsCookApp.Droid.Renderer
{
    class CustomImageGalleryRenderer : ViewRenderer<CustomImageGallery, Gallery>
    {
        static public Android.Views.View mCustomView;
        static public WebChromeClient.ICustomViewCallback customViewCallback;

        static public string urls;

        public List<Android.Webkit.WebView> WebViews { get; set; }

        static public FrameLayout customViewContainer;
        /// <summary>
        /// The gallery
        /// </summary>
        private Gallery _gallery;
        /// <summary>
        /// The source
        /// </summary>
        private DataSource _source;

        public LinearLayout PageControl { get; set; }
        public TextView PageText { get; set; }

        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <value>The source.</value>
        private DataSource Source
        {
            get
            {
                return new DataSource(this); //_source ?? (_source = new DataSource(this));
            }
        }

        /// <summary>
        /// Called when [element changed].
        /// </summary>
        /// <param name="e">The e.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<CustomImageGallery> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                WebViews = new List<Android.Webkit.WebView>();
                _gallery = new MyGallery(Android.App.Application.Context);
                this.Element.Clean = new Command(() =>
                {
                    foreach (var view in WebViews)
                    {
                        view.LoadUrl(view.Url);
                        Java.Lang.Class.ForName("android.webkit.WebView").GetMethod("onPause", (Java.Lang.Class[])null).Invoke(view, (Java.Lang.Object[])null);
                    }
                });
                SetNativeControl(_gallery);
            }
            Bind(e.NewElement);
            _gallery.Adapter = Source;

            //var inflatorservice = (Android.Views.LayoutInflater)Android.App.Application.Context.GetSystemService(Android.App.Application.LayoutInflaterService);
            //var convertview = inflatorservice.Inflate(Resource.Layout.PageControl, null, false);
            //PageControl = ((LinearLayout)convertview);
            //PageControl.SetBackgroundColor(Android.Graphics.Color.White);

            //PageText = new TextView(Android.App.Application.Context);
            //PageText.Text = "Hallo";
            //PageControl.AddView(PageText);

            //_gallery.AddView(PageControl);

        }

        //public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        //{
        //    return base.OnKeyDown(keyCode, e);
        //}

        //protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    base.OnElementPropertyChanged(sender, e);
        //}

        public class CustomWebChromeClient : WebChromeClient
        {

            public Android.Webkit.WebView WebView;

            public CustomWebChromeClient(Android.Webkit.WebView webView)
            {
                WebView = webView;
            }


            public interface ToggledFullscreenCallback
            {
                void toggledFullscreen(bool fullscreen);
            }
            private ToggledFullscreenCallback toggledFullscreenCallback;

            public void setOnToggledFullscreen(ToggledFullscreenCallback callback)
            {
                this.toggledFullscreenCallback = callback;
            }




            public override void OnShowCustomView(Android.Views.View view, Android.Content.PM.ScreenOrientation requestedOrientation, WebChromeClient.ICustomViewCallback callback)
            {
                OnShowCustomView(view, callback);
            }

            public override void OnShowCustomView(Android.Views.View view, WebChromeClient.ICustomViewCallback callback)
            {
                base.OnShowCustomView(view, callback);

                if (mCustomView != null)
                {
                    callback.OnCustomViewHidden();
                    return;
                }
                var v = urls;
                Intent inte = new Intent(Xamarin.Forms.Forms.Context, typeof(activity));
                inte.PutExtra("url", v);
                Xamarin.Forms.Forms.Context.StartActivity(inte);

                FrameLayout frameLayout = (FrameLayout)view;
                var focusedChild = frameLayout.FocusedChild;

                //WebView.Visibility = ViewStates.Gone;
                customViewContainer.Visibility = ViewStates.Visible;
                //customViewContainer.AddView(view);



                //customViewCallback = callback;
                //customViewContainer.RefreshDrawableState();
                //customViewContainer.BringToFront();
            }

            public override void OnHideCustomView()
            {
                base.OnHideCustomView();

                if (mCustomView == null)
                    return;

                WebView.Visibility = ViewStates.Visible;
                customViewContainer.Visibility = ViewStates.Gone;
                mCustomView.Visibility = ViewStates.Gone;
                customViewContainer.RemoveView(mCustomView);
                customViewCallback.OnCustomViewHidden();
                mCustomView = null;
            }
        }

        /// <summary>
        /// Gets the view.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="convertView">The convert view.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="position">The position.</param>
        /// <returns>Android.Views.View.</returns>
        protected virtual Android.Views.View GetView(string item, Android.Views.View convertView, Android.Views.ViewGroup parent, int position)
        {
            if (item.Contains("youtu"))
            {
                var inflatorservice = (LayoutInflater)Android.App.Application.Context.GetSystemService(Android.App.Application.LayoutInflaterService);
                var convertview = inflatorservice.Inflate(Resource.Layout.Video, parent, false);
                // convertview ^
                var control = ((LinearLayout)convertview);
                customViewContainer = convertview.FindViewById<FrameLayout>(Resource.Id.customViewContainer);
                //string youTubeVideoHTML = "<body style='padding: 0; margin: 0; background: #000000;'><iframe width='100%' height='{1}' src='{0}' allowfullscreen = 'allowfullscreen' mozallowfullscreen = 'mozallowfullscreen' msallowfullscreen ='msallowfullscreen' oallowfullscreen = 'oallowfullscreen' webkitallowfullscreen = 'webkitallowfullscreen'></iframe></body>";

                string youTubeVideoHTML = "<body style='padding: 0; margin: 0; background: #000000;'><iframe width='100%' height={0} src='{0}' allowfullscreen></iframe></body>";
                string html = string.Format(youTubeVideoHTML, "https://www.youtube.com/embed/" + item.Split('=').LastOrDefault() + "?start=0&amp;wmode=transparent&amp;fs=1", 160);



                var webView = convertview.FindViewById<Android.Webkit.WebView>(Resource.Id.webView);
                webView.Settings.JavaScriptEnabled = true;
                webView.Settings.SetPluginState(WebSettings.PluginState.On);
                webView.Settings.PluginsEnabled = true;

                webView.SetWebChromeClient(new CustomWebChromeClient(webView));

                webView.Focusable = true;
                webView.Clickable = true;
                webView.Click += (s, e) =>
                {

                };


                var mime = "text/html";
                var encoding = "utf-8";

                webView.LoadData(html, mime, encoding);

                WebViews.Add(webView);

                return control;
            }
            else
            {
                var imageView = convertView as ImageView ?? new ImageView(parent.Context);

                if (IsValidUrl(item))
                {
                    Task.Run(async () =>
                    {
                        Bitmap imageBitmap = null;

                        using (var webClient = new WebClient())
                        {
                            var imageBytes = await webClient.DownloadDataTaskAsync(new Uri(item));
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                try
                                {
                                    double newHeight = 142;
                                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);

                                    //var width = (320 * imageBitmap.Width) / (imageBitmap.Height);
                                    decimal ratio = (App.MobileDevice.CurrentDevice.Display.Width / 160);

                                    if ((imageBitmap.Width / imageBitmap.Height) > ratio)
                                    {
                                        //var height = (App.MobileDevice.CurrentDevice.Display.Width * imageBitmap.Height) / imageBitmap.Width;
                                        //Bitmap bitmap = Bitmap.CreateScaledBitmap(imageBitmap, App.MobileDevice.CurrentDevice.Display.Width, height, false);
                                        //imageView.SetImageBitmap(bitmap);
                                        var width = 0.0;
                                        if (imageBitmap.Height > 160)
                                        {
                                            width = Convert.ToDouble(imageBitmap.Width) / (Convert.ToDouble(imageBitmap.Height) / 160);
                                        }
                                        else
                                        {
                                            width = (160 / Convert.ToDouble(imageBitmap.Height)) * Convert.ToDouble(App.MobileDevice.CurrentDevice.Display.Width);
                                        }

                                        var newWidth = Convert.ToInt32(Math.Round(width, 0));
                                        Bitmap bitmap = Bitmap.CreateScaledBitmap(imageBitmap, newWidth, 160, false);
                                        imageView.SetImageBitmap(bitmap);
                                    }
                                    else
                                    {
                                        //var width = (App.MobileDevice.CurrentDevice.Display.Height * imageBitmap.Width) / imageBitmap.Height;
                                        //Bitmap bitmap = Bitmap.CreateScaledBitmap(imageBitmap, width, App.MobileDevice.CurrentDevice.Display.Height, false);
                                        //imageView.SetImageBitmap(bitmap);

                                        var height = 0.0;

                                        if (imageBitmap.Width > App.MobileDevice.CurrentDevice.Display.Width)
                                        {
                                            height = imageBitmap.Height / (Convert.ToDouble(imageBitmap.Width) / Convert.ToDouble(App.MobileDevice.CurrentDevice.Display.Width));
                                        }
                                        else
                                        {
                                            height = (Convert.ToDouble(App.MobileDevice.CurrentDevice.Display.Width) / Convert.ToDouble(imageBitmap.Width)) * imageBitmap.Height;
                                        }

                                        newHeight = Convert.ToInt32(Math.Round(height, 0));
                                        Bitmap bitmap = Bitmap.CreateScaledBitmap(imageBitmap, App.MobileDevice.CurrentDevice.Display.Width, (int)newHeight, false);
                                        imageView.SetImageBitmap(bitmap);
                                    }

                                    imageView.SetBackgroundColor(Android.Graphics.Color.Black);
                                    //imageView.sethei
                                }
                                catch (Exception ex) { }
                            });

                        }
                        //await GetBitmapFromUrl(imageView, item);
                    });
                }
                else
                {
                    imageView.SetImageResource(Resources.GetIdentifier(System.IO.Path.GetFileNameWithoutExtension(item), "drawable", Context.PackageName));
                }

                //imageView.SetMinimumWidth(App.MobileDevice.CurrentDevice.Display.Width);
                imageView.SetScaleType(ImageView.ScaleType.CenterCrop);
                imageView.SetAdjustViewBounds(true);
                //imageView.SetBackgroundColor(Android.Graphics.Color.Yellow);

                return imageView;
            }

        }



        /// <summary>
        /// Binds the specified new element.
        /// </summary>
        /// <param name="newElement">The new element.</param>
        private void Bind(CustomImageGallery newElement)
        {
            if (newElement != null)
            {
                newElement.PropertyChanging += ElementPropertyChanging;
                newElement.PropertyChanged += ElementPropertyChanged;
                if (newElement.ItemsSource != null)
                    this.Source.NotifyDataSetChanged();
                //((INotifyCollectionChanged)newElement.ItemsSource).CollectionChanged += DataCollectionChanged;
            }
        }

        /// <summary>
        /// Elements the property changing.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangingEventArgs"/> instance containing the event data.</param>
        private void ElementPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == "ItemsSource")
                this.Source.NotifyDataSetChanged();
            //((INotifyCollectionChanged)Element.ItemsSource).CollectionChanged -= DataCollectionChanged;
        }

        /// <summary>
        /// Datas the collection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void DataCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.Source.NotifyDataSetChanged();
        }

        /// <summary>
        /// Elements the property changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void ElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ItemsSource")
                this.Source.NotifyDataSetChanged();
            // ((INotifyCollectionChanged)Element.ItemsSource).CollectionChanged += DataCollectionChanged;
        }

        /// <summary>
        /// Determines whether [is valid URL] [the specified URL string].
        /// </summary>
        /// <param name="urlString">The URL string.</param>
        /// <returns><c>true</c> if [is valid URL] [the specified URL string]; otherwise, <c>false</c>.</returns>
        private bool IsValidUrl(string urlString)
        {
            return URLUtil.IsValidUrl(urlString);
        }

        /// <summary>
        /// Gets the bitmap from URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>Bitmap.</returns>
        private async Task GetBitmapFromUrl(ImageView image, string url)
        {

        }

        /// <summary>
        /// Class DataSource.
        /// </summary>
        private class DataSource : BaseAdapter
        {
            /// <summary>
            /// The gallery renderer
            /// </summary>
            CustomImageGalleryRenderer _galleryRenderer;

            /// <summary>
            /// Initializes a new instance of the <see cref="DataSource"/> class.
            /// </summary>
            /// <param name="galleryRenderer">The gallery renderer.</param>
            public DataSource(CustomImageGalleryRenderer galleryRenderer)
            {
                this._galleryRenderer = galleryRenderer;
            }

            #region abstract members of BaseAdapter
            /// <summary>
            /// To be added.
            /// </summary>
            /// <param name="position">To be added.</param>
            /// <returns>To be added.</returns>
            /// <remarks>To be added.</remarks>
            public override Java.Lang.Object GetItem(int position)
            {
                return position;
            }

            /// <summary>
            /// To be added.
            /// </summary>
            /// <param name="position">To be added.</param>
            /// <returns>To be added.</returns>
            /// <remarks>To be added.</remarks>
            public override long GetItemId(int position)
            {
                return position;
            }

            /// <summary>
            /// To be added.
            /// </summary>
            /// <param name="position">To be added.</param>
            /// <param name="convertView">To be added.</param>
            /// <param name="parent">To be added.</param>
            /// <returns>To be added.</returns>
            /// <remarks>To be added.</remarks>
            public override Android.Views.View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
            {
                urls = _galleryRenderer.Element.ItemsSource.Cast<string>().ToArray()[position];
                return _galleryRenderer.GetView(_galleryRenderer.Element.ItemsSource.Cast<string>().ToArray()[position], convertView, parent, position);
            }

            /// <summary>
            /// To be added.
            /// </summary>
            /// <value>To be added.</value>
            /// <remarks>To be added.</remarks>
            public override int Count
            {
                get
                {
                    if (_galleryRenderer.Element.ItemsSource != null)
                    {
                        return _galleryRenderer.Element.ItemsSource.Cast<string>().Count();
                    }
                    else
                    {
                        return 0;
                    }

                }
            }
            #endregion
        }
    }
}