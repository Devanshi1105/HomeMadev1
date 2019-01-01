using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LetsCookApp.Views
{
    public partial class ProfileSetting : ContentPage
    {
        public ProfileSetting()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            TakePicture = new Command(async () => await TakePictureAsync());
            SelectPicture = new Command(async () => await SelectPictureAsync());
            var vm = App.AppSetup.SignUpViewModel;
            BindingContext = vm;
            imgPlus.IsVisible = true;
           // imgphoto.Source = App.AppSetup.SignUpViewModel.Picture; 
            var lst = App.AppSetup.SignUpViewModel.CountryResponse;
            foreach (var item in lst.country)
            {
                drpcountry.Items.Add(item.name.ToUpper());
            }

            if (!string.IsNullOrEmpty(vm.Gender))
            {
                drpgender.SelectedIndex = drpgender.Items.IndexOf(vm.Gender.ToUpper());
            }
            if (!string.IsNullOrEmpty(vm.Country))
            {
                drpcountry.SelectedIndex = drpcountry.Items.IndexOf(vm.Country.ToUpper());
            }
            if (!string.IsNullOrEmpty(vm.DateOfBirth))
            {
                lbldob.Text = vm.DateOfBirth;
                lbldob.TextColor = Color.Black;
            }
            else
            {
                lbldob.Text = "DD/MM/YYYY";
                lbldob.TextColor = Color.Gray;
            }


        }
        public ICommand TakePicture { get; set; }
        public ICommand SelectPicture
        {
            get; set;
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
            //var viewmodel = App.AppSetup.SignUpViewModel;
            //viewmodel.FullName = viewmodel.FirstName = viewmodel.DateOfBirth = viewmodel.Ocupation = viewmodel.Email = viewmodel.UserName = viewmodel.Password = viewmodel.MobileNumber = viewmodel.AboutMe = "";
            //viewmodel.Address1 = viewmodel.Address2 = viewmodel.Address3 = viewmodel.City = viewmodel.State = viewmodel.Country = viewmodel.Postcode = viewmodel.Gender = viewmodel.Hobbies = viewmodel.PhoneNumber = "";

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
            var date = dobpickar.Date.ToString("dd/MM/yyyy");
            //if (DateTime.Parse(date).AddYears(15).Date >= DateTime.Now.Date)
            //{
            //    UserDialogs.Instance.Alert("Date of birth should be gretter than 15 years.");
            //    return;
            //}
            //else
            {
                lbldob.Text = date;
                App.AppSetup.SignUpViewModel.DateOfBirth = date;
            }
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
            //else if (vm.Email != vm.RetypeEmail)
            //{
            //    UserDialogs.Instance.Alert("Emailid and retype-emailid should be equal.");
            //    entRetypeEmail.Focus();
            //    val = false;
            //}
            //else if (string.IsNullOrEmpty(vm.Password))
            //{
            //    UserDialogs.Instance.Alert("Password is Required.");
            //    entPassword.Focus();
            //    val = false;
            //}
            //else if (vm.Password != vm.RetypePassword)
            //{
            //    UserDialogs.Instance.Alert("Password and retype-password should be equal.");
            //    entRetypePassword.Focus();
            //    val = false;
            //}
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

        private void Menu_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        
        private void lbldob_Tapped(object sender, EventArgs e)
        {
            dobpickar.Focus();
        }
    }
}