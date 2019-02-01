using Acr.UserDialogs;
using LetsCookApp.Models;
using LetsCookApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LetsCookApp.ViewModels
{
   public class MyFavouritesRecipesViewModel : BaseViewModel
    {
        #region Constructor    


        public ICommand GetFavsByUserIdCommand { get; private set; }
        public ICommand RefreshFavsByUserIdCommand { get; private set; }
        public ICommand SavefavRecipeCommand { get; private set; }
        public MyFavouritesRecipesViewModel()
        {
            GetFavsByUserIdCommand = new Command(() => GetFavsByUserIdExecute());
            RefreshFavsByUserIdCommand = new Command(() => RefreshFavsByUserIdExecute());
            SavefavRecipeCommand = new Command(() => SavefavRecipeExecute());

        }

        #endregion

        #region Property
     

        private ObservableCollection<FavouriteRecipe> favouriteRecipes;

        public ObservableCollection<FavouriteRecipe> FavouriteRecipes
        {
            get { return favouriteRecipes; }
            set { favouriteRecipes = value; RaisePropertyChanged(() => FavouriteRecipes); }
        }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                RaisePropertyChanged(() => IsRefreshing);
            }
        }

        private bool _isVisbleSearchBar = false;
        public bool IsVisbleSearchBar
        {
            get { return _isVisbleSearchBar; }
            set
            {
                _isVisbleSearchBar = value;
                RaisePropertyChanged(() => IsVisbleSearchBar);
            }
        }
        private string searchBarText;

        public string SearchBarText
        {
            get { return searchBarText; }
            set { searchBarText = value; RaisePropertyChanged(() => SearchBarText); }
        }
        private int recipeId;

        public int RecipeId
        {
            get { return recipeId; }
            set { recipeId = value; RaisePropertyChanged(() => RecipeId); }
        }
         private string favorite;

        public string Favorite
        {
            get { return favorite; }
            set { favorite = value; RaisePropertyChanged(() => Favorite); }
        }


        #endregion

        #region Method

        #endregion

        #region Command Excute

        public void GetFavsByUserIdExecute()
        {
            try
            {

            var obj = new GetFavsByUserIdRequest()
            {
                 UserId=(App.AppSetup.HomeViewModel.UserId),
            };
            UserDialogs.Instance.ShowLoading("Requesting..");
            userManager.GetFavsByUserId(obj, () =>
            {

                var response = userManager.GetFavsByUserIdResponse;
                if (response.StatusCode == 200)
                { 
                    UserDialogs.Instance.HideLoading();
                    FavouriteRecipes = new ObservableCollection<FavouriteRecipe>(response.FavouriteRecipes);
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new MyFavouritesRecipesView());
                    });

                }
            },
             (requestFailedReason) =>
             {
                 Device.BeginInvokeOnMainThread(() =>
                 {
                     UserDialogs.Instance.HideLoading();
                     UserDialogs.Instance.Alert(requestFailedReason?.Message == null ? "Network Error" : requestFailedReason.Message, null, "OK");
                 });
             });

            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert( ex.Message, null, "OK");
            }
        }

        public void RefreshFavsByUserIdExecute()
        {
            try
            {
                IsRefreshing = true;
                IsVisbleSearchBar = false;
                SearchBarText = "";
                var obj = new GetFavsByUserIdRequest()
                {
                    UserId = (App.AppSetup.HomeViewModel.UserId),
                };
               
                userManager.GetFavsByUserId(obj, () =>
                {
                    IsRefreshing = false;
                    var response = userManager.GetFavsByUserIdResponse;
                    if (response.StatusCode == 200)
                    {
                        UserDialogs.Instance.HideLoading();
                        FavouriteRecipes = new ObservableCollection<FavouriteRecipe>(response.FavouriteRecipes); 
                    }
                },
                 (requestFailedReason) =>
                 {
                     IsRefreshing = false;
                     Device.BeginInvokeOnMainThread(() =>
                     {
                         UserDialogs.Instance.HideLoading();
                         UserDialogs.Instance.Alert(requestFailedReason?.Message == null ? "Network Error" : requestFailedReason.Message, null, "OK");
                     });
                 });

            }
            catch (Exception ex)
            {
                IsRefreshing = false;
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert(ex.Message, null, "OK");
            }
        }

        public void SavefavRecipeExecute()
        {
            try
            {
                var obj = new SaveFavRecipeRequest()
                {
                    Recipe_Id = RecipeId,
                    Comments = "This is bad recipe",
                    Favorite = Favorite,
                    Member_Id = Convert.ToInt32(App.AppSetup.HomeViewModel.UserId)
                };
                UserDialogs.Instance.ShowLoading("Requesting..");
                userManager.SavefaRrecipe(obj, () =>
                {
                    UserDialogs.Instance.HideLoading();
                    Favorite = "";
                    var savefaRrecipeResponse = userManager.SavefaRrecipeResponse;
                    if (savefaRrecipeResponse.StatusCode == 200)
                    {
                        UserDialogs.Instance.Alert(savefaRrecipeResponse.Message, null, "OK");
                        //RefreshFavsByUserIdExecute();
                    }
                    else
                    {
                        UserDialogs.Instance.Alert(savefaRrecipeResponse.Message, null, "OK");
                    }
                },
                 (requestFailedReason) =>
                 {
                     Device.BeginInvokeOnMainThread(() =>
                     {
                         UserDialogs.Instance.HideLoading();
                         UserDialogs.Instance.Alert(requestFailedReason?.Message == null ? "Network Error" : requestFailedReason.Message, null, "OK");
                     });
                 });
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert(ex.Message, null, "OK");
            }
        }

        #endregion



    }
}