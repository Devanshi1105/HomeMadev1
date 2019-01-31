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
       
        public LoginViewModel( )
        {

        }
     //   public string AdUnitId { get; set; } = "ca-app-pub-3940256099942544/6300978111";
        public string AdUnitId { get; set; } = "ca-app-pub-5013023089301852/7364805776";

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
            try
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
                               Device.BeginInvokeOnMainThread(() =>
                               {
                                    UserDialogs.Instance.HideLoading();
                                    UserDialogs.Instance.Alert(requestFailedReason?.Message == null ? "Network Error" : requestFailedReason.Message, null, "OK");
                               });
                           });
                    });
                }

            }
            catch (Exception ex)
            {
               UserDialogs.Instance.HideLoading();
               UserDialogs.Instance.Alert(ex.Message, "Error", "OK");
            }
        }


        public Command ForgetCommand { get { return new Command(ForgetCommandExecution); } }
        private async void ForgetCommandExecution()
        {
            try
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
                               Device.BeginInvokeOnMainThread(() =>
                               {
                                   UserDialogs.Instance.HideLoading();
                                   UserDialogs.Instance.Alert(requestFailedReason?.Message == null ? "Network Error" : requestFailedReason.Message, null, "OK");
                               });
                           });
                    });
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert(ex.Message, null, "OK");
            }
        }


        public void GetAllCategory()
        {
            try
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
                     UserDialogs.Instance.HideLoading();
                     UserDialogs.Instance.Alert(requestFailedReason?.Message == null ? "Network Error" : requestFailedReason.Message, null, "OK");
                 });
             }); 
            }
            catch (Exception ex)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    UserDialogs.Instance.HideLoading();
                    UserDialogs.Instance.Alert(ex.Message, null, "OK");
                });
            }
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


