using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using CommonServiceLocator;
using LetsCookApp.Views;
using LetsCookApp.Services;

namespace LetsCookApp
{
    public class App : Application
    {
        public static AppSetup AppSetup { get { return appSetup; } }
        private static AppSetup appSetup;
        public static IDevice MobileDevice { get; set; }


        public App()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<AppSetup>();
            appSetup = ServiceLocator.Current.GetInstance<AppSetup>();
            var nativeDevice = DependencyService.Get<IDevice>();
            MobileDevice = nativeDevice.CurrentDevice;
            // The root page of your application
            var content = new ContentPage
            {
                Title = "LetsCookApp",
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        new Label {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = "Welcome to Xamarin Forms!"
                        }
                    }
                }
            };

           // page = new Main();
           // Current.MainPage = page;
             appSetup.SignUpViewModel.GetCountry();
             MainPage = new NavigationPage(new SignInSignUpView());
            //MainPage = new NavigationPage(new SignUpView());
        }

       
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
