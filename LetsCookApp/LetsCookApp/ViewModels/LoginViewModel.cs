using Acr.UserDialogs;
using LetsCookApp.Models;
using LetsCookApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LetsCookApp.ViewModels
{
    public class LoginViewModel :BaseViewModel
    {

        public LoginViewModel()
        {
                   
           
        }
        private static readonly HttpClient _Client = new HttpClient();
        async Task<HttpResponseMessage> Request(HttpMethod pMethod, string pUrl, string pJsonContent)
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = pMethod;
            httpRequestMessage.RequestUri = new Uri(pUrl);

            switch (pMethod.Method)
            {
                case "POST":
                    HttpContent httpContent = new StringContent(pJsonContent, Encoding.UTF8, "application/json");
                    httpRequestMessage.Content = httpContent;
                    break;

            }
            return await _Client.SendAsync(httpRequestMessage);
        }


        public Command LoginCommand { get { return new Command(LoginCommandExecution); } }
        private async void LoginCommandExecution()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                UserDialogs.Instance.Alert("Username/EmailId and Password is Required");
                return;
            }
            
            else
            {
                var LoginRequest = new LoginRequest
                {
                    Email = UserName,//"ksantosh.kundkar12@gmail.com",//UserName, 
                    Password = Password// "123456",// Password
                };

               
                await Task.Run(() =>
                {
                    UserDialogs.Instance.ShowLoading("Requesting..");
                    userManager.Login(LoginRequest, () =>
                    {
                        var LoginResponse = userManager.LoginResponse;

                        if (LoginResponse.StatusCode == 202)
                        {
                            App.AppSetup.HomeViewModel.UserData = LoginResponse.UserData;
                            App.AppSetup.SignUpViewModel.Email = App.AppSetup.HomeViewModel.Email = LoginResponse.UserData.EmailId;
                            App.AppSetup.SignUpViewModel.UserId = App.AppSetup.HomeViewModel.UserId = LoginResponse.UserData.UserId;
                            App.AppSetup.HomeViewModel.PictureSource = new UriImageSource
                            {
                                Uri = new Uri(LoginResponse.UserData.PhotoURL),
                                CachingEnabled = true,
                            };
                            UserName = Password = ""; 
                            App.AppSetup.CategoryViewModel.GetCotegaryCommand.Execute(null);
                        }
                        else
                        {
                            UserDialogs.Instance.HideLoading();
                            UserDialogs.Instance.Alert(LoginResponse.Message, "Error", "OK");
                        } 
                    },
                       (requestFailedReason) =>
                       {
                           UserDialogs.Instance.HideLoading();
                           UserDialogs.Instance.Alert( requestFailedReason.Message, "Error", "OK");
                       });
                });


            }
        }


        public Command ForgetCommand { get { return new Command(ForgetCommandExecution); } }
        private async void ForgetCommandExecution()
        {
            if (string.IsNullOrEmpty(EmailId))
            {
                UserDialogs.Instance.Alert("EmailId is Required");
                return;
            }

            else
            {
                var LoginRequest = new LoginRequest
                {
                    Email = EmailId,//"ksantosh.kundkar12@gmail.com",// UserName,
                   
                };


                await Task.Run(() =>
                {
                    UserDialogs.Instance.ShowLoading("Requesting..");
                    userManager.ForgetPassword(LoginRequest, () =>
                    {
                        var LoginResponse = userManager.LoginResponse;
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            if (LoginResponse.StatusCode == 202)
                            {
                              
                                EmailId = "";
                                UserDialogs.Instance.HideLoading();
                                UserDialogs.Instance.Alert(LoginResponse.Message, "Success", "OK");
                                App.Current.MainPage = new NavigationPage(new SignInSignUpView());
                            }
                            else
                            {
                                UserDialogs.Instance.Alert(LoginResponse.Message, "Error", "OK");
                            }
                        });
                        // RaisePropertyChanged(() => LoginResponse);
                        UserDialogs.Instance.HideLoading();
                    },
                       (requestFailedReason) =>
                       {
                           UserDialogs.Instance.HideLoading();
                           UserDialogs.Instance.Alert( requestFailedReason.Message, "Error", "OK");
                       });
                });


            }
        }


        public void GetAllCategory()
        {
            CommonRequest obj = new CommonRequest();

            userManager.getAllCategory(obj, () =>
            {
                var userProfileResponse = userManager.CategoryResponse;
            },
             (requestFailedReason) =>
             {
                 Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                 {
                    //  UserDialogs.Instance.Alert(requestFailedReason.Message, null, "OK");
                    // UserDialogs.Instance.HideLoading();
                });
             });
        }

     
        private string emailId;

        public string EmailId
        {
            get { return emailId; }
            set { emailId = value;
                RaisePropertyChanged(() => EmailId);
            }
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(() => UserName); }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; RaisePropertyChanged(() => Password); }
        }


    }

    
   
}


