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
using Com.Google.Android.Youtube.Player;
using Xamarin.Forms;

namespace LetsCookApp.Droid
{
    [Activity(Label = "activity", Theme = "@style/MainTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape, AllowEmbedded = true)]
    public class activity : YouTubeBaseActivity, IYouTubePlayerOnInitializedListener
    {
        YouTubePlayerView youtube;
        static string vedioId;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.player);
            var intent = Intent;
            var extras = intent.Extras;
            if (extras != null)
            {
                var type = extras.GetString("url");
                vedioId = type.Split('=').LastOrDefault();

                youtube = this.FindViewById<YouTubePlayerView>(Resource.Id.youtube_view);
                youtube.Initialize("AIzaSyBAwFnF-qEiy-hOhH2ogkDTf7VsfWiwB14", this);
            }
        }
        protected override void OnStop()
        {
            MessagingCenter.Send<string>("data", "PlayVideo");
            base.OnStop();

        }
        public void OnInitializationFailure(IYouTubePlayerProvider provider, YouTubeInitializationResult errorReason)
        {
            //if (errorReason.IsUserRecoverableError)
            //{
            //   
            //}
            //else
            //{
            //    //String errorMessage = String.Format(
            //    //        GetString(Resource.String.ErrorMessage), errorReason.ToString());
            //    //Toast.MakeText(this, errorMessage, ToastLength.Long).Show();
            //}
        }
        public void OnInitializationSuccess(IYouTubePlayerProvider p0, IYouTubePlayer yPlayer, bool p2)
        {
            yPlayer.LoadVideo(vedioId, 0);
        }
    }
}
