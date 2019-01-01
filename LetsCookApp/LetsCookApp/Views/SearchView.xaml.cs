using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LetsCookApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchView : Rg.Plugins.Popup.Pages.PopupPage

    {
        public SearchView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        private async void Menu_Tapped(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
            App.AppSetup.HomeViewModel.IsMenuListPresented = true;
        }

        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }
    }
}