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
            var page = new SearchView();

            Rg.Plugins.Popup.Services.PopupNavigation.PushAsync(page);
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
