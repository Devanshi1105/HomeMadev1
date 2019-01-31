using LetsCookApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LetsCookApp.Views
{
    public partial class MyFavouritesRecipesView : ContentPage
    {
        public MyFavouritesRecipesView()
        { 
            InitializeComponent();  
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = App.AppSetup.MyFavouritesRecipesViewModel;
        }
        private void Menu_Tapped(object sender, EventArgs e)
        {
            App.AppSetup.HomeViewModel.IsMenuListPresented = true;
        }
        private void Search_Tapped(object sender, EventArgs e)
        {
            //var page = new SearchView();

            //Rg.Plugins.Popup.Services.PopupNavigation.PushAsync(page);
            srchbar.Focus();
            App.AppSetup.MyFavouritesRecipesViewModel.IsVisbleSearchBar = true;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vm = App.AppSetup.MyFavouritesRecipesViewModel;
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                listSubCatgory.ItemsSource = vm.FavouriteRecipes;
            }
            else
            {
                listSubCatgory.ItemsSource = vm.FavouriteRecipes.Where(x => x.Title.ToLower().Contains(e.NewTextValue.ToLower()));
            }
        }

        private void srchbar_Unfocused(object sender, FocusEventArgs e)
        {
            App.AppSetup.MyFavouritesRecipesViewModel.IsVisbleSearchBar = false;
        }

        private void Favorate_Click(object sender, EventArgs e)
        {
            var vm = App.AppSetup.MyFavouritesRecipesViewModel;
            var recipe = ((Image)sender).BindingContext as FavouriteRecipe;
            vm.RecipeId = Convert.ToInt32(recipe.Id);
            vm.SavefavRecipeCommand.Execute(null);
            vm.FavouriteRecipes.Remove(recipe);
        }

        private void ItemImage_Tapped(object sender, EventArgs e)
        { 
            var recipe = ((Image)sender).BindingContext as FavouriteRecipe;  
            App.AppSetup.CategoryViewModel.RecipeId = Convert.ToInt32(recipe.Id); 
            App.AppSetup.CategoryViewModel.GetDishViewCommand.Execute(null);
        }
    }
}
