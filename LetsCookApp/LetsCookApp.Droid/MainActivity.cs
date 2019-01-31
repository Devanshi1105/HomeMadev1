using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using Plugin.CurrentActivity;
using Plugin.Permissions;
using FFImageLoading.Forms.Droid;
using Android.Gms.Ads;

namespace LetsCookApp.Droid
{
    [Activity(Label = "HomeMade", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            //TabLayoutResource = Resource.Layout.Tabbar;
            //ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
           
            //CrossCurrentActivity.Current.Activity.ini(this, bundle);
            ImageCircleRenderer.Init();
            CachedImageRenderer.Init();
            MobileAds.Initialize(ApplicationContext, "ca-app-pub-5013023089301852/7364805776");
            global::Xamarin.Forms.Forms.Init(this, bundle);
            global::Acr.UserDialogs.UserDialogs.Init(this);
            LoadApplication(new App());
        }
        public static MainActivity Current { private set; get; }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults); base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}

