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
   public class PopularReceipesViewModel : BaseViewModel
    {
        #region Constructor    

        public ICommand GetPopularReceipeCommand { get; private set; }
        public ICommand RefreshPopularReceipeCommand { get; private set; }
      
        public PopularReceipesViewModel()
        {
            GetPopularReceipeCommand = new Command(() => GetPopularReceipeExecute());
            RefreshPopularReceipeCommand = new Command(() => RefreshPopularReceipeExecute());
           
        }


        #endregion

        #region Property 

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

        private ObservableCollection<PopularRecipe> popularRecipes;

        public ObservableCollection<PopularRecipe> PopularRecipes
        {
            get { return popularRecipes; }
            set { popularRecipes = value; RaisePropertyChanged(() => PopularRecipes); }
        }


        #endregion

        #region Method

        #endregion

        #region Command Excute

        public void GetPopularReceipeExecute()
        {
            try
            {

                var obj = new CommonRequest();
                UserDialogs.Instance.ShowLoading("Requesting..");
                userManager.getPopularRecipe(obj, () =>
                {

                    var popularRecipeResponse = userManager.PopularRecipeResponse;
                    if (popularRecipeResponse.StatusCode == 200)
                    {
                        UserDialogs.Instance.HideLoading();
                        PopularRecipes = new ObservableCollection<PopularRecipe>(popularRecipeResponse.PopularRecipes);
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new PopularReceipesView());
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
                UserDialogs.Instance.Alert(ex.Message, null, "OK");
            }
        }

        public void RefreshPopularReceipeExecute()
        {
            try
            {

                IsRefreshing = true;
                IsVisbleSearchBar = false;
                SearchBarText = "";
                var obj = new CommonRequest();
                
                userManager.getPopularRecipe(obj, () =>
                {
                    IsRefreshing = false;
                    var popularRecipeResponse = userManager.PopularRecipeResponse;
                    if (popularRecipeResponse.StatusCode == 200)
                    {
                        UserDialogs.Instance.HideLoading();
                        PopularRecipes = new ObservableCollection<PopularRecipe>(popularRecipeResponse.PopularRecipes);
                        
                    }
                },
                 (requestFailedReason) =>
                 {
                     Device.BeginInvokeOnMainThread(() =>
                     {
                         IsRefreshing = false;
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

        
        #endregion



    }
}