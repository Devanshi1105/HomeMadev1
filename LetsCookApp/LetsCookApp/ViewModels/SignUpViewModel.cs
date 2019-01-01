using Acr.UserDialogs;
using LetsCookApp.Models;
using LetsCookApp.Views;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LetsCookApp.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {

        #region Constructor

        public ICommand TakePicture { get; set; }
        public ICommand SelectPicture { get; set; }
        public SignUpViewModel()
        {
            TakePicture = new Command(async () => await TakePictureAsync());
            SelectPicture = new Command(async () => await SelectPictureAsync());
            CountryResponse = new CountryResponse();
            FinishCommand = new Command(() => FinishCommandExecute());
           // GetCountry();
        }


        #endregion

        #region Property
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; RaisePropertyChanged(() => Email); }
        }
         private string retypeEmail;
        public string RetypeEmail
        {
            get { return retypeEmail; }
            set { retypeEmail = value; RaisePropertyChanged(() => RetypeEmail); }
        }


        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; RaisePropertyChanged(() => FirstName); }
        }


        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; RaisePropertyChanged(() => Password); }
        }

        private string retypePassword;
        public string RetypePassword
        {
            get { return retypePassword; }
            set
            {

                retypePassword = value;
                RaisePropertyChanged(() => RetypePassword);
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; RaisePropertyChanged(() => LastName); }
        }
        private string mobileNumber;
        public string MobileNumber
        {
            get { return mobileNumber; }
            set { mobileNumber = value; RaisePropertyChanged(() => MobileNumber); }
        }
        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; RaisePropertyChanged(() => PhoneNumber); }
        }

        private string address1;
        public string Address1
        {
            get { return address1; }
            set { address1 = value; RaisePropertyChanged(() => Address1); }
        }
        private string address2;
        public string Address2
        {
            get { return address2; }
            set { address2 = value; RaisePropertyChanged(() => Address2); }
        }
        private string address3;
        public string Address3
        {
            get { return address3; }
            set { address3 = value; RaisePropertyChanged(() => Address3); }
        }
        private string city;
        public string City
        {
            get { return city; }
            set { city = value; RaisePropertyChanged(() => City); }
        }

        private string state;
        public string State
        {
            get { return state; }
            set { state = value; RaisePropertyChanged(() => State); }
        }
        private string country;
        public string Country
        {
            get { return country; }
            set { country = value; RaisePropertyChanged(() => Country); }
        }

        private string postCode;
        public string Postcode
        {
            get { return postCode; }
            set { postCode = value; RaisePropertyChanged(() => Postcode); }
        }
        private string hobbies;
        public string Hobbies
        {
            get { return hobbies; }
            set { hobbies = value; RaisePropertyChanged(() => Hobbies); }
        }
        private string picture;
        public string Picture
        {
            get { return picture; }
            set { picture = value; RaisePropertyChanged(() => Picture); }
        }

        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = value;
                RaisePropertyChanged(() => FullName);
            }
        }

        private string userid;
        public string UserId
        {
            get { return userid; }
            set
            {
                userid = value;
                RaisePropertyChanged(() => UserId);
            }
        }

        private string dateOfBirth;
        public string DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; RaisePropertyChanged(() => DateOfBirth); }
        }
        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value; RaisePropertyChanged(() => UserName);
            }
        }

        private string occupation;
        public string Ocupation
        {
            get { return occupation; }
            set
            {
                occupation = value; RaisePropertyChanged(() => Ocupation);
            }
        }
        private string aboutMe;
        public string AboutMe
        {
            get { return aboutMe; }
            set { aboutMe = value; RaisePropertyChanged(() => AboutMe); }
        }

        private string gender;

        public string Gender
        {
            get { return gender; }
            set { gender = value; RaisePropertyChanged(() => Gender); }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;

                RaisePropertyChanged(() => Title);
            }
        }

        private string btnText;
        public string BtnText
        {
            get { return btnText; }
            set
            {
                btnText = value;
                RaisePropertyChanged(() => BtnText);
            }
        }

        private bool isenable;
        public bool IsEn
        {
            get { return isenable; }
            set { isenable = value; RaisePropertyChanged(() => IsEn); }
        }

        private string imageBase64;
        public string ImageBase64
        {
            get { return this.imageBase64; }
            set
            {
                if (Equals(value, this.imageBase64))
                {
                    return;
                }
                this.imageBase64 = value;
                RaisePropertyChanged(() => ImageBase64);
            }
        }


        private ImageSource pictureSource;
        public ImageSource PictureSource
        {
            get { return pictureSource; }
            set
            {

                pictureSource = value;
                 RaisePropertyChanged(() => PictureSource);
            }
        }




        #endregion

        #region Validation
        private bool Validate()
        {
            bool val = false;
            if (string.IsNullOrEmpty(UserName))
            {
                UserDialogs.Instance.Alert("Username is Required");
                val = false;
            }
            else if (string.IsNullOrEmpty(FirstName))
            {
                UserDialogs.Instance.Alert("FullName is Required");
                val = false;
            }
            else if (string.IsNullOrEmpty(LastName))
            {
                UserDialogs.Instance.Alert("LastName is Required");
                val = false;
            }
            else if (string.IsNullOrEmpty(Gender))
            {
                UserDialogs.Instance.Alert("Gender is Required.");
                val = false;
            }
            else if (string.IsNullOrEmpty(DateOfBirth))
            {
                UserDialogs.Instance.Alert("BirthDay is Required.");
                val = false;
            }
            else if (string.IsNullOrEmpty(Password))
            {
                UserDialogs.Instance.Alert("Password is Required.");
                val = false;
            }
            else if (Password != RetypePassword)
            {
                UserDialogs.Instance.Alert("Password and retype-password should be equal.");
                val = false;
            }


            else if (string.IsNullOrEmpty(MobileNumber))
            {
                UserDialogs.Instance.Alert("MobileNumber is Required.");
                val = false;
            }
            else if (string.IsNullOrEmpty(PhoneNumber))
            {
                UserDialogs.Instance.Alert("PhoneNumber is Required.");
                val = false;
            }
            else if (string.IsNullOrEmpty(Email))
            {
                UserDialogs.Instance.Alert("Email is Required.");
                val = false;
            }
            else if (string.IsNullOrEmpty(Address1))
            {
                UserDialogs.Instance.Alert("Address1 is Required.");
                val = false;
            }
            else if (string.IsNullOrEmpty(City))
            {
                UserDialogs.Instance.Alert("City is Required.");
                val = false;
            }
            else if (string.IsNullOrEmpty(Postcode))
            {
                UserDialogs.Instance.Alert("PostCode is Required.");
                val = false;
            }

            else
            {
                val = true;
            }
            return val;
        }
        #endregion

        #region Command

        public async Task TakePictureAsync()
        {
            //await CrossMedia.Current.Initialize();
            //if (!CrossMedia.Current.IsPickPhotoSupported || CrossMedia.Current.IsCameraAvailable)
            //{
            //    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            //    {
            //        //UserDialogs.Instance.Alert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
            //        return;
            //    });
            //}
            //var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            //{
            //    Directory = "TimePeace App",
            //    Name = "Aes_" + DateTime.Now.ToString("yyyyMMdd") + ".jpg",
            //    DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear,
            //    //SaveToAlbum = false
            //});
            //if (file == null) return;
            /////Mohit
            //var cropedBytes = await CrossXMethod.Current.CropImageFromOriginalToBytes(file.Path);
            //if (cropedBytes != null)
            //{
            //    Picture = ImageSource.FromStream(() =>
            //    {
            //        file.Dispose();
            //        var cropedImage = new MemoryStream(cropedBytes);
            //        return cropedImage;
            //    });
            //    ImageBase64 = Convert.ToBase64String(cropedBytes);
            //}
            //else
            //{
            //    file.Dispose();
            //    return;
            //}
            ////byte[] ReceiptData = DependencyService.Get<IMediaService>().ResizeImage(cropedBytes, 300, 400);
            /////Mohit
            ////byte[] ReceiptData = DependencyService.Get<IMediaService>().ResizeImage(DependencyService.Get<IMediaService>().GetMediaInBytes(file.Path), 450, 650);
            ////ImageBase64 = Convert.ToBase64String(ReceiptData);
            ////Picture = ImageSource.FromStream(() => new MemoryStream(ReceiptData));

            //byte[] resizedImage = DependencyService.Get<IMediaService>().ResizeImage(cropedBytes, 400, 400);
            //ImageBase64 = Convert.ToBase64String(resizedImage);
            //Picture = ImageSource.FromStream(() => new MemoryStream(cropedBytes));
        }
        public async Task SelectPictureAsync()
        {
            //await CrossMedia.Current.Initialize();
            //var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            //{
            //    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Custom
            //});
            //if (file == null) return;
            /////Mohit
            //var cropedBytes = await CrossXMethod.Current.CropImageFromOriginalToBytes(file.Path);
            //if (cropedBytes != null)
            //{
            //    Picture = ImageSource.FromStream(() =>
            //    {
            //        file.Dispose();
            //        var cropedImage = new MemoryStream(cropedBytes);
            //        return cropedImage;
            //    });
            //    ImageBase64 = Convert.ToBase64String(cropedBytes);
            //}
            //else
            //{
            //    file.Dispose();
            //    return;
            //}
            /////Mohit
            ////byte[] ReceiptData = DependencyService.Get<IMediaService>().ResizeImage(cropedBytes, 450, 650);
            ////byte[] ReceiptData = DependencyService.Get<IMediaService>().ResizeImage(DependencyService.Get<IMediaService>().GetMediaInBytes(file.Path), 450, 650);
            //byte[] resizedImage = DependencyService.Get<IMediaService>().ResizeImage(cropedBytes, 400, 400);
            //ImageBase64 = Convert.ToBase64String(resizedImage);
            ////ImageBase64 = Convert.ToBase64String(cropedBytes);
            //Picture = ImageSource.FromStream(() => new MemoryStream(cropedBytes));
        }
        public ICommand FinishCommand { get; private set; }
        private async void FinishCommandExecute()
        {
 
                var SignupRequest = new SignupRequest
                {
                    Address1 = Address1,
                    Address2 = Address2,
                    Address3 = Address3,
                    State = State,
                    City = City,
                    Country = Country,
                    Email = Email,
                    FirstName = FirstName,
                    Hobbies = Hobbies,
                    LastName = LastName,
                    UserName = UserName,
                    MobileNumber = MobileNumber,
                    Password = Password,
                    PhoneNumber = PhoneNumber,
                    Postcode = postCode,
                    Picture = ImageBase64,
                    DateOfBirth = DateOfBirth,
                    Gender = Gender,
                   
                };



                await Task.Run(async () =>
                {
                    UserDialogs.Instance.ShowLoading("Requesting..");
                    if (BtnText == "UPDATE")
                    {
                        var ProfileUpdateRequest = new ProfileUpdateRequest
                        {
                            Address1 = Address1,
                            Address2 = Address2,
                            Address3 = Address3,
                            State = State,
                            City = City,
                            Country = Country,
                            EmailId = Email,
                            FirstName = FirstName,
                            Hobbies = Hobbies,
                            LastName = LastName,
                            UserName = UserName,
                            MobileNumber = MobileNumber,
                            Password = Password,
                            PhoneNumber = PhoneNumber,
                            Postcode = postCode,
                            PhotoUrl = ImageBase64 == "" ? ImageBase64 = await GetImageAsBase64Url(Picture): ImageBase64,
                            DateOfBirth = DateOfBirth,
                            Gender = Gender,
                            AboutMe = AboutMe,
                            UserId = UserId
                        };

                        userManager.SignUpUpdate(ProfileUpdateRequest, () =>
                        {
                            var SignupResponse = userManager.SignupResponse;
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                if (SignupResponse.StatusCode == 200)
                                {
                                    UserDialogs.Instance.HideLoading();

                                    //FullName = DateOfBirth = Ocupation = Email = UserName = Password = MobileNumber = AboutMe = "";
                                    //Address1 = Address2 = Address3 = City = State = Country = Postcode = Gender = Hobbies = PhoneNumber = "";
                                    UserDialogs.Instance.Alert(SignupResponse.Message, "Success", "OK");
                                    //App.Current.MainPage.Navigation.PushAsync(new SignInView());
                                }
                                else
                                {
                                    UserDialogs.Instance.Alert(SignupResponse.Message, "Error", "OK");
                                }
                            });

                            UserDialogs.Instance.HideLoading();
                        },
                     (requestFailedReason) =>
                     {
                         UserDialogs.Instance.Alert(requestFailedReason.Message, "Error", "OK");
                         UserDialogs.Instance.HideLoading();
                     });

                    }

                    else
                    {
                        userManager.SignUp(SignupRequest, () =>
                        {
                            var SignupResponse = userManager.SignupResponse;
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                if (SignupResponse.StatusCode == 200)
                                {
                                    UserDialogs.Instance.HideLoading();
                                    UserDialogs.Instance.Alert(SignupResponse.Message);
                                    FullName = DateOfBirth = Ocupation = Email = UserName = Password = MobileNumber = AboutMe = "";
                                    Address1 = Address2 = Address3 = City = State = Country = Postcode = Gender = Hobbies = PhoneNumber = "";
                                    App.Current.MainPage.Navigation.PushAsync(new SignInView());
                                }
                                else
                                {
                                    UserDialogs.Instance.Alert(SignupResponse.Message, "Error", "OK");
                                }
                            });

                            UserDialogs.Instance.HideLoading();
                        },
                      (requestFailedReason) =>
                      {
                          UserDialogs.Instance.Alert(requestFailedReason.Message, "Error", "OK");
                          UserDialogs.Instance.HideLoading();
                      });

                    }

                });


                #endregion



           
        }

        public void GetProfile()
        {

            GetProfileRequest obj = new GetProfileRequest();
            obj.EmailId = Email;
            obj.UserId = UserId;
            UserDialogs.Instance.ShowLoading("Requesting..");
            userManager.getProfile(obj, () =>
            {
                var userProfileResponse = userManager.ProfileResponse;

                if (userProfileResponse.StatusCode == 202)
                {
                    var udata = userProfileResponse.UserData;
                    Address1 = udata.Address1;
                    Address2 = udata.Address2;
                    Address3 = udata.Address3;
                    State = udata.State;
                    City = udata.City;
                    Country = udata.Country;
                    Email = udata.EmailId;
                    FirstName = udata.FirstName;
                    Hobbies = udata.Hobbies;
                    LastName = udata.LastName;
                    UserName = udata.UserName;
                    MobileNumber = udata.MobileNumber;
                    Password = udata.Password;
                    PhoneNumber = udata.PhoneNumber;
                    Postcode = udata.Postcode;
                    Picture = udata.PhotoURL;
                    DateOfBirth =  udata.DateOfBirth;
                    Gender = udata.Gender;
                    AboutMe = udata.AboutMe;
                    ImageBase64 = "";
                    if (!string.IsNullOrEmpty(udata.PhotoURL))
                    {
                        PictureSource = new UriImageSource
                        {
                            Uri = new Uri(udata.PhotoURL),
                            CachingEnabled = true,
                        };
                        // ImageBase64 = await GetImageAsBase64Url(udata.PhotoURL);
                    }
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                    {
                        UserDialogs.Instance.HideLoading();
                      await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new ProfileSetting());

                    });


                }
            },
             (requestFailedReason) =>
             {
                 Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                 {
                     //  UserDialogs.Instance.Alert(requestFailedReason.Message, null, "OK");
                     UserDialogs.Instance.HideLoading();
                 });
             });
        }

        private ObservableCollection<Country> lstCountry;
        public ObservableCollection<Country> LstCountry
        {
            get { return lstCountry; }
            set
            {
                lstCountry = value;
                RaisePropertyChanged(() => LstCountry);
            }
        }

        private CountryResponse countryResponse;

        public CountryResponse CountryResponse
        {
            get { return countryResponse; }
            set { countryResponse = value;
                RaisePropertyChanged(() => CountryResponse);
            }
        }


        public CountryResponse GetCountry()
        {
            CommonRequest obj = new CommonRequest();

            UserDialogs.Instance.ShowLoading("Requesting..");
            userManager.getCountry(obj,  () =>
            {
                 CountryResponse = userManager.CountryResponse;
                //LstCountry = new ObservableCollection<Models.Country>();
                //foreach (var item in userCountryResponse.country)
                //{
                //    LstCountry.Add(item);
                //}

                UserDialogs.Instance.HideLoading();
            });

            return CountryResponse;
        }

        public async static Task<string> GetImageAsBase64Url(string url)
        {
            var credentials = new NetworkCredential();
            using (var handler = new HttpClientHandler { Credentials = credentials })
            using (var client = new HttpClient(handler))
            {
                var bytes = await client.GetByteArrayAsync(url);
                return Convert.ToBase64String(bytes);
            }
        }

    }
}