using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LetsCookApp.Views
{

    public partial class ShoppingListView : ContentPage
    {
      
        public ShoppingListView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = App.AppSetup.ShoppingListViewModel;
        }
        private void Search_Tapped(object sender, EventArgs e)
        {
            var page = new SearchView();

            Rg.Plugins.Popup.Services.PopupNavigation.PushAsync(page);
        }
        private void Menu_Tapped(object sender, EventArgs e)
        {
            App.AppSetup.HomeViewModel.IsMenuListPresented = true;
        }

        private void masterMenuList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            ((ListView)sender).SelectedItem = null; // de-select the row 
            
        }
    } 
}
