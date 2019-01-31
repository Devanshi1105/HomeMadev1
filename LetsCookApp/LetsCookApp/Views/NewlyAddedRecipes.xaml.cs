using LetsCookApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LetsCookApp.Views
{
    public partial class NewlyAddedRecipes : ContentPage
    {
        public NewlyAddedRecipes()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = App.AppSetup.NewlyAddedRecipeViewModel;
        }
        private void Menu_Tapped(object sender, EventArgs e)
        {
            App.AppSetup.HomeViewModel.IsMenuListPresented = true;
        }
        private void Search_Tapped(object sender, EventArgs e)
        {
            srchbar.Focus();
            App.AppSetup.NewlyAddedRecipeViewModel.IsVisbleSearchBar = true;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vm = App.AppSetup.NewlyAddedRecipeViewModel;
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                listSubCatgory.ItemsSource = vm.NewlyAddedRecipes;
            }
            else
            {
                listSubCatgory.ItemsSource = vm.NewlyAddedRecipes.Where(x => x.Name.ToLower().Contains(e.NewTextValue.ToLower()));
            }
        }

        private void srchbar_Unfocused(object sender, FocusEventArgs e)
        {
            App.AppSetup.NewlyAddedRecipeViewModel.IsVisbleSearchBar = false;
        }
        private void Recipe_Tapped(object sender, EventArgs e)
        {
           var vm= App.AppSetup.CategoryViewModel;
           var recipe  = ((Image)sender).BindingContext as NewlyAddedRecipe;
            vm.RecipeId =Convert.ToInt32(recipe.Id);
            vm.GetDishViewCommand.Execute(null);

        }
    }
}
