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
    public partial class MenuView : ContentPage
    {
        public ListView menuList;
        public MenuView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.menuList = masterMenuList;
            BindingContext = App.AppSetup.HomeViewModel; 
        }

        private void masterMenuList_ItemTapped(object sender, ItemTappedEventArgs e)
        { 
        
            NavigateTo((ViewModels.Menu)e.Item);
        }

        private void Menu_Tapped(object sender, EventArgs e)
        {
            App.AppSetup.HomeViewModel.IsMenuListPresented = false;
        }

        void NavigateTo(ViewModels.Menu menu)
        {
            if (menu.TargetType == typeof(CategoriesView))
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PopToRootAsync();
            }
            else if (menu.TargetType == typeof(MyProfileView))
            {
                App.AppSetup.HomeViewModel.GetProfile();
              
            }
            else if (menu.TargetType == typeof(NewlyAddedRecipes))
            {
                App.AppSetup.NewlyAddedRecipeViewModel.GetNewlyAddedRecipeCommand.Execute(null);
                //((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new NewlyAddedRecipes());
            }
            else if (menu.TargetType == typeof(PopularReceipesView))
            {
                App.AppSetup.PopularReceipesViewModel.GetPopularReceipeCommand.Execute(null); 
            }
            else if (menu.TargetType == typeof(MyFavouritesRecipesView))
            {
                App.AppSetup.MyFavouritesRecipesViewModel.GetFavsByUserIdCommand.Execute(null);
                //((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new MyFavouritesRecipesView());
            }
            else if (menu.TargetType == typeof(ShoppingListView))
            {
                App.AppSetup.ShoppingListViewModel.GetShoppingListByUserIdCommand.Execute(null);
                //((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new ShoppingListView());
            }
            else if (menu.TargetType == typeof(SuggestRecipesView))
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new SuggestRecipesView());
            }
            else if (menu.TargetType == typeof(HelpMeView))
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new HelpMeView());
            }
            else if (menu.TargetType == typeof(AboutUsView))
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new AboutUsView());
            }
            else if (menu.TargetType == typeof(SettingsView))
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new SettingsView());
            }
            else if (menu.TargetType == typeof(ShareAppView))
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new ShareAppView());
            }
            else if (menu.TargetType == typeof(SignInSignUpView))
            {
                App.AppSetup.Clear();
                App.Current.MainPage = new NavigationPage(new Views.SignInSignUpView());
            }
            else if (menu.TargetType == typeof(Nullable))
            {
                App.Current.MainPage = new NavigationPage(new Views.SignInSignUpView());
            }
            App.AppSetup.HomeViewModel.IsMenuListPresented = false;
        }
    }
}