//using LetsCookApp.Views;
using Acr.UserDialogs;
using LetsCookApp.Models;
using LetsCookApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LetsCookApp.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ICommand GetFriendCommand { get; private set; }
        public HomeViewModel()
        {
            IsMenuListPresented = false;
            _menuItemList = new List<Menu>()
            {
                new Menu {Title = "My Profile", imagesource="Profile.png", TargetType = typeof(MyProfileView)},
                new Menu {Title = "Newly Added Recipes", imagesource="Newly.png", TargetType = typeof(NewlyAddedRecipes)},
                new Menu {Title = "Popular Recipes", imagesource="Popular.png", TargetType = typeof(PopularReceipesView)},
                new Menu {Title = "Categories", imagesource="Categories", TargetType = typeof(CategoriesView)},
                new Menu {Title = "My Favourites Recipes",imagesource="Favourites.png",  TargetType = typeof(MyFavouritesRecipesView)},
                new Menu {Title = "Shopping List", imagesource="Shopping.png", TargetType = typeof(ShoppingListView)},
                new Menu {Title = "Suggest Recipes", imagesource="Suggest.png", TargetType = typeof(SuggestRecipesView)},
                new Menu {Title = "Please Help Me",imagesource="Help.png", TargetType = typeof(HelpMeView)},
                new Menu {Title = "About the App",imagesource="About.png", TargetType = typeof(AboutUsView)},
                new Menu {Title = "Settings", imagesource="Settings.png", TargetType = typeof(SettingsView)},
                new Menu {Title = "ShareApp", imagesource="ShareApp.png", TargetType = typeof(ShareAppView)},
                new Menu {Title = "Signout", imagesource="logout.png", TargetType = typeof(SignInSignUpView)},
            };
            RaisePropertyChanged(() => MenuItemList);

            GetFriendCommand = new Command(() => GetFriendExecute());
        }

        #region Property
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; RaisePropertyChanged(() => Email); }
        }


        private int height = 40;
        public int TitleHeight
        {
            get { return height; }
            set { height = value; RaisePropertyChanged(() => TitleHeight); }
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

        private ObservableCollection<FriendsData> friendsDataList;

        public ObservableCollection<FriendsData> FriendsDataList
        {
            get { return friendsDataList; }
            set { friendsDataList = value; RaisePropertyChanged(() => FriendsDataList); }
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

        #region Set Properties

        private UserData userData;

        public UserData UserData
        {
            get { return userData; }
            set { userData = value;
                RaisePropertyChanged(() => UserData);
            }
        }




        private bool _isMenuListPresented;
        public bool IsMenuListPresented
        {
            get { return _isMenuListPresented; }
            set
            {
                _isMenuListPresented = value;
                RaisePropertyChanged(() => IsMenuListPresented);
            }
        }

        private List<Menu> _menuItemList;
        public List<Menu> MenuItemList
        {
            get { return _menuItemList; }
            set
            {
                _menuItemList = value;
                RaisePropertyChanged(() => MenuItemList);
            }
        }

        #endregion

        public void GetProfile()
        {

            GetProfileRequest obj = new GetProfileRequest();
            obj.EmailId = Email;
            obj.UserId = UserId;
            UserDialogs.Instance.ShowLoading("Requesting..");
            userManager.getProfile(obj,() =>
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
                    DateOfBirth = udata.DateOfBirth;
                    Gender = udata.Gender;
                    AboutMe = udata.AboutMe;
                    if (!string.IsNullOrEmpty(udata.PhotoURL))
                    {
                        PictureSource = new UriImageSource
                        {
                            Uri = new Uri(udata.PhotoURL),
                            CachingEnabled = true,
                        };
                        ////  ImageBase64 = await GetImageAsBase64Url(udata.PhotoURL);
                    }
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        UserDialogs.Instance.HideLoading();
                        await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new MyProfileView());
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


        
        public void GetFriendExecute()
        {

            FriendRequest obj = new FriendRequest()
            {
                UserId = Convert.ToInt32(UserId)
            };
            
            UserDialogs.Instance.ShowLoading("Requesting..");
            userManager.getFriends(obj, () =>
            {
                var friendResponse = userManager.FriendResponse;

                if (friendResponse.StatusCode == 200)
                {
                    UserDialogs.Instance.HideLoading();
                    var udata = friendResponse.FriendsData;
                    FriendsDataList = new ObservableCollection<FriendsData>(udata);

                    //Device.BeginInvokeOnMainThread(async () =>
                    //{

                    //    await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new MyProfileView());
                    //}); 
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

    public class Menu
    {
        public string Title { get; set; }
        public Type TargetType { get; set; }

        public ImageSource imagesource { get; set; }
    }
}
