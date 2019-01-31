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
            App.AppSetup.CategoryViewModel.IsVisbleSearchBar = true;
            srchbar.Focus(); 
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

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vm = App.AppSetup.CategoryViewModel;
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                listSubCatgory.ItemsSource = vm.Recipes;
            }
            else
            {
                listSubCatgory.ItemsSource = vm.Recipes.Where(x => x.Title.ToLower().Contains(e.NewTextValue.ToLower()));
            }
        }

        private void srchbar_Unfocused(object sender, FocusEventArgs e)
        {
            App.AppSetup.CategoryViewModel.IsVisbleSearchBar = false;
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