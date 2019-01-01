using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using ModernHttpClient;

namespace LetsCookApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInView : ContentPage
    {
        public SignInView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            var viewmodel= App.AppSetup.LoginViewModel;
            BindingContext = viewmodel;
            viewmodel.UserName = "ksantosh.kundkar12@gmail.com";
            viewmodel.Password = "123456";


        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            var viewmodel = App.AppSetup.LoginViewModel;
            viewmodel.UserName = viewmodel.Password = "";
        }

        private void CreateAccount_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpView());
        }
        private void ForgotPassword_Tapped(object sender, EventArgs e)
        {
          Navigation.PushAsync(new ForgotPasswordView());
        }
       
       
        private void Close_Tapped(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }

       
    }
   
}