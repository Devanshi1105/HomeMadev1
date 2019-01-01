using LetsCookApp.Models;
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
    public partial class SubCategoryView : ContentPage
    {
        public SubCategoryView()
        { 
            InitializeComponent(); 
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = App.AppSetup.CategoryViewModel;
        }
        private void Search_Tapped(object sender, EventArgs e)
        {
            var page = new SearchView();

            Rg.Plugins.Popup.Services.PopupNavigation.PushAsync(page);
        }
        private void Menu_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        private void listSubCatgory_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var v = e.SelectedItem as Recipe;
            App.AppSetup.CategoryViewModel.RecipeId = Convert.ToInt32(v.Id);
            ((ListView)sender).SelectedItem = null; // de-select the row
            App.AppSetup.CategoryViewModel.GetDishViewCommand.Execute(null); 
        }

        private void favorite_Tapped(object sender, EventArgs e)
        {
            var vm = App.AppSetup.CategoryViewModel;
            var recipe = ((Image)sender).BindingContext as Recipe;
            vm.RecipeId = Convert.ToInt32(recipe.Id);
            vm.SavefavRecipeCommand.Execute(null);
        }
    }

    public class SubCategory
    {
        public ImageSource foodIcon { get; set; }
        public string DishName { get; set; }
        public ImageSource likeIcon { get; set; }
        public ImageSource timeIcon { get; set; }
        public string Time { get; set; }
        public ImageSource servingIcon { get; set; }
        public string Servings { get; set; }
        public ImageSource ingrendIcon { get; set; }
        public string Ingrendients { get; set; }
        public ImageSource plusIcon { get; set; }
        public double UserRating { get; set; }
    }


}