
using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LetsCookApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpView : ContentPage
    {
        public ICommand TakePicture { get; set; }
        public ICommand SelectPicture
        {
            get; set;
        }
        public SignUpView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            TakePicture = new Command(async () => await TakePictureAsync());
            SelectPicture = new Command(async () => await SelectPictureAsync());
            BindingContext = App.AppSetup.SignUpViewModel;
            imgPlus.IsVisible = true;
            var lst = App.AppSetup.SignUpViewModel.CountryResponse;
            foreach (var item in lst?.country)
            {
                drpcountry.Items.Add(item.name);
            }
            imgphoto.Source = App.AppSetup.SignUpViewModel.PictureSource;
        }

        private void Create_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new Views.HomeView();
            //Navigation.PushAsync(new CategoriesView());
        }
        private void SignIn_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignInView());
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            var viewmodel = App.AppSetup.SignUpViewModel;
            viewmodel.FullName = viewmodel.FirstName = viewmodel.DateOfBirth = viewmodel.Ocupation = viewmodel.Email = viewmodel.UserName = viewmodel.Password = viewmodel.MobileNumber = viewmodel.AboutMe = "";
            viewmodel.Address1 = viewmodel.Address2 = viewmodel.Address3 = viewmodel.City = viewmodel.State = viewmodel.Country = viewmodel.Postcode = viewmodel.Gender = viewmodel.Hobbies = viewmodel.PhoneNumber = "";

        }

        private async void Close_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


        public async Task TakePictureAsync()
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return;
            }
            try
            {
                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "Test",

                    DefaultCamera = CameraDevice.Front
                });

                if (file == null)
                    return;
                var stream = file.GetStream();
                var bytes = new byte[stream.Length];
                await stream.ReadAsync(bytes, 0, (int)stream.Length);
                App.AppSetup.SignUpViewModel.ImageBase64 = System.Convert.ToBase64String(bytes);
                imgphoto.Source = ImageSource.FromStream(() =>
                {
                    stream = file.GetStream();
                    file.Dispose();
                    imgPlus.IsVisible = false;
                    return stream;
                });

            }
            catch (Exception ex)
            {
                await DisplayAlert("No Camera", ":( " + ex.Message, "OK");
            }
        }
        public async Task SelectPictureAsync()
        {

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null)
                return;
            // imgPlus.IsVisible = false;
            var stream = file.GetStream();
            var bytes = new byte[stream.Length];
            await stream.ReadAsync(bytes, 0, (int)stream.Length);
            App.AppSetup.SignUpViewModel.ImageBase64 = System.Convert.ToBase64String(bytes);

            imgphoto.Source = ImageSource.FromStream(() =>
            {
                stream = file.GetStream();


                file.Dispose();

                return stream;
            });

        }


       
        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            var actionSheet = await DisplayActionSheet("", "Cancel", null, new string[] { "Take Picture", "Select Picture" });
            if (actionSheet == "Select Picture")
            {
                SelectPicture.Execute(null);
            }
            else if (actionSheet == "Take Picture")
            {
                TakePicture.Execute(null);
            }
        }

        private void drpcountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            App.AppSetup.SignUpViewModel.Country = drpcountry.Items[drpcountry.SelectedIndex];
        }

        private void drpgender_SelectedIndexChanged(object sender, EventArgs e)
        {
            App.AppSetup.SignUpViewModel.Gender = drpgender.Items[drpgender.SelectedIndex];
        }

        private void dobpickar_DateSelected(object sender, DateChangedEventArgs e)
        {
            App.AppSetup.SignUpViewModel.DateOfBirth = dobpickar.Date.ToString();
        }

        bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        private bool Validate()
        {
            var vm = App.AppSetup.SignUpViewModel;
            bool val = false;
            if (string.IsNullOrEmpty(vm.UserName))
            {
                UserDialogs.Instance.Alert("Username is Required");
                entUserName.Focus();
                val = false;
            }
            else if (string.IsNullOrEmpty(vm.Email))
            {
                UserDialogs.Instance.Alert("Email is Required.");
                entEmail.Focus();
                val = false;
            }
            else if (!IsValidEmail(vm.Email))
            {
                UserDialogs.Instance.Alert("Please enter valid emailID.", null, "OK");
                entEmail.Focus();
                val = false;
            }
           
            else if (vm.Email != vm.RetypeEmail)
            {
                UserDialogs.Instance.Alert("Emailid and retype-emailid should be equal.");
                entRetypeEmail.Focus();
                val = false;
            }
            else if (string.IsNullOrEmpty(vm.Password))
            {
                UserDialogs.Instance.Alert("Password is Required.");
                entPassword.Focus();
                val = false;
            }
            else if (vm.Password != vm.RetypePassword)
            {
                UserDialogs.Instance.Alert("Password and retype-password should be equal.");
                entRetypePassword.Focus();
                val = false;
            }
            else if (string.IsNullOrEmpty(vm.FirstName))
            {
                UserDialogs.Instance.Alert("FirstName is Required");
                entFirstName.Focus();
                val = false;
            }
            else if (string.IsNullOrEmpty(vm.LastName))
            {
                UserDialogs.Instance.Alert("LastName is Required");
                entLastName.Focus();
                val = false;
            }
            else if (string.IsNullOrEmpty(vm.Gender))
            {
                UserDialogs.Instance.Alert("Gender is Required.");
                drpgender.Focus();
                val = false;
            }
            else if (string.IsNullOrEmpty(vm.DateOfBirth))
            {
                UserDialogs.Instance.Alert("Date of birth is Required.");
                dobpickar.Focus();
                val = false;
            }
            else if (DateTime.Parse(vm.DateOfBirth).AddYears(18).Date >= DateTime.Now.Date)
            {
                UserDialogs.Instance.Alert("Date of birth should be gretter than 15 years.");
                dobpickar.Focus();
                val = false;
            }
          
            else if (string.IsNullOrEmpty(vm.MobileNumber))
            {
                UserDialogs.Instance.Alert("MobileNumber is Required.");
                entMobileNumber.Focus();
                val = false;
            }
            else if (string.IsNullOrEmpty(vm.PhoneNumber))
            {
                UserDialogs.Instance.Alert("PhoneNumber is Required.");
                entPhoneNumber.Focus();
                val = false;
            }
          
            else if (string.IsNullOrEmpty(vm.Address1))
            {
                UserDialogs.Instance.Alert("Address1 is Required.");
                entAddress1.Focus();
                val = false;
            }
            else if (string.IsNullOrEmpty(vm.City))
            {
                UserDialogs.Instance.Alert("City is Required.");
                entCity.Focus();
                val = false;
            }
            else if (string.IsNullOrEmpty(vm.Postcode))
            {
                UserDialogs.Instance.Alert("Postcode is Required.");
                entPostcode.Focus();
                val = false;
            }

            else
            {
                val = true;
            }
            return val;
        }

        private void btntext_Clicked(object sender, EventArgs e)
        {
            if (Validate())
            {
                App.AppSetup.SignUpViewModel.FinishCommand.Execute(null);
            }
        }




    }
}