
using LetsCookApp.Models;
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
    public partial class CategoriesView : ContentPage
    {
        public CategoriesView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            var viewmodel = App.AppSetup.CategoryViewModel;
            BindingContext = viewmodel;
            categorieslist.ItemsSource = viewmodel.Categories;
        }

        private void SubCategories_Tapped(object sender, EventArgs e)
        {

            Navigation.PushAsync(new SubCategoryView());
        }
        private void Menu_Tapped(object sender, EventArgs e)
        {
            App.AppSetup.HomeViewModel.IsMenuListPresented = true;
        }
        private void Search_Tapped(object sender, EventArgs e)
        {
            App.AppSetup.CategoryViewModel.IsVisbleSearchBar = true;
            srchbar.Focus();
          
        }

        private void categorieslist_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var v = e.SelectedItem as Category;
            App.AppSetup.CategoryViewModel.CatId = Convert.ToInt32(v.Id);
            App.AppSetup.CategoryViewModel.SubCategoryTitle = v.Title;
            ((ListView)sender).SelectedItem = null; // de-select the row
            App.AppSetup.CategoryViewModel.GetSubCotegaryCommand.Execute(null);
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vm = App.AppSetup.CategoryViewModel;
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                categorieslist.ItemsSource = vm.Categories;
            } 
            else
            {
                categorieslist.ItemsSource = vm.Categories.Where(x => x.Title.ToLower().Contains(e.NewTextValue.ToLower()));
            }
        }

        private void srchbar_Unfocused(object sender, FocusEventArgs e)
        {
            App.AppSetup.CategoryViewModel.IsVisbleSearchBar = false;
        }
    }

     
}
