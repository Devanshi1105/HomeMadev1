using LetsCookApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LetsCookApp.Views
{
    public partial class PopularReceipesView : ContentPage
    {
        public PopularReceipesView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false); 
            BindingContext = App.AppSetup.PopularReceipesViewModel;  
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
            var vm = App.AppSetup.CategoryViewModel;
            var recipe = ((Image)sender).BindingContext as PopularRecipe;
            vm.RecipeId = Convert.ToInt32(recipe.id);
            vm.GetDishViewCommand.Execute(null);

        }
    }
}
