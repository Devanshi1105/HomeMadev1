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
            
            App.AppSetup.ShoppingListViewModel.IsVisbleSearchBar = true;
            srchbar.Focus();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vm = App.AppSetup.ShoppingListViewModel;
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                masterMenuList.ItemsSource = vm.Grouped;
            }
            else
            {
                masterMenuList.ItemsSource = vm.Grouped.Where(x =>x.LongName.ToLower().Contains(e.NewTextValue.ToLower()));
            }
        }

        private void srchbar_Unfocused(object sender, FocusEventArgs e)
        {
            App.AppSetup.ShoppingListViewModel.IsVisbleSearchBar = false;
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
