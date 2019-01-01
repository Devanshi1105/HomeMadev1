using GalaSoft.MvvmLight.Ioc;
using LetsCookApp.Managers.ApiProvider;
using LetsCookApp.Managers.SettingsManager;
using LetsCookApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsCookApp
{
    public class AppSetup
    {
        public AppSetup()
        {
            //Viewmodel
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<SignUpViewModel>();
            SimpleIoc.Default.Register<CategoryViewModel>();
            SimpleIoc.Default.Register<PopularReceipesViewModel>();
            SimpleIoc.Default.Register<NewlyAddedRecipeViewModel>();
            SimpleIoc.Default.Register<MyFavouritesRecipesViewModel>();
            SimpleIoc.Default.Register<ShoppingListViewModel>();
            SimpleIoc.Default.Register<HelpMeViewModel>();

            // Services
            SimpleIoc.Default.Register<ISettingsManager, SettingsManager>();
            SimpleIoc.Default.Register<IApiProvider, ApiProvider>();
            SimpleIoc.Default.Register<IManager, Manager>();
        }

        public void Clear()
        {
            SimpleIoc.Default.Unregister<LoginViewModel>();
            SimpleIoc.Default.Unregister<SignUpViewModel>();
            SimpleIoc.Default.Unregister<CategoryViewModel>();
            SimpleIoc.Default.Unregister<PopularReceipesViewModel>();
            SimpleIoc.Default.Unregister<NewlyAddedRecipeViewModel>();
            SimpleIoc.Default.Unregister<MyFavouritesRecipesViewModel>();
            SimpleIoc.Default.Unregister<ShoppingListViewModel>();
            SimpleIoc.Default.Unregister<HelpMeViewModel>();

            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<SignUpViewModel>();
            SimpleIoc.Default.Register<CategoryViewModel>();
            SimpleIoc.Default.Register<PopularReceipesViewModel>();
            SimpleIoc.Default.Register<NewlyAddedRecipeViewModel>();
            SimpleIoc.Default.Register<MyFavouritesRecipesViewModel>();
            SimpleIoc.Default.Register<ShoppingListViewModel>();
            SimpleIoc.Default.Register<HelpMeViewModel>();

        }
        public LoginViewModel LoginViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<LoginViewModel>();
            }
        }
        public SignUpViewModel SignUpViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<SignUpViewModel>();
            }
        }

        public HomeViewModel HomeViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<HomeViewModel>();
            }
        }
        public CategoryViewModel CategoryViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<CategoryViewModel>();
            }
        }
        public PopularReceipesViewModel PopularReceipesViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<PopularReceipesViewModel>();
            }
        }
        public NewlyAddedRecipeViewModel NewlyAddedRecipeViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<NewlyAddedRecipeViewModel>();
            }
        }
        public MyFavouritesRecipesViewModel MyFavouritesRecipesViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<MyFavouritesRecipesViewModel>();
            }
        }
        public ShoppingListViewModel ShoppingListViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<ShoppingListViewModel>();
            }
        }
        public HelpMeViewModel HelpMeViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<HelpMeViewModel>();
            }
        }
    }
}
