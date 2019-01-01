using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LetsCookApp.Views
{
    public partial class SettingsView : ContentPage
    {
        public SettingsView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        private void Menu_Tapped(object sender, EventArgs e)
        {
            App.AppSetup.HomeViewModel.IsMenuListPresented = true;
        }
        private void Search_Tapped(object sender, EventArgs e)
        {
            var page = new SearchView();

            Rg.Plugins.Popup.Services.PopupNavigation.PushAsync(page);
        }
        private void MyProfileSettings_Tapped(object sender, EventArgs e)
        {
            App.AppSetup.SignUpViewModel.IsEn = false; 
            App.AppSetup.SignUpViewModel.BtnText = "UPDATE";
            App.AppSetup.SignUpViewModel.GetProfile(); 
        }
    }
}
