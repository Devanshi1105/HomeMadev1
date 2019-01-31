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
    public class NewlyAddedRecipeViewModel : BaseViewModel
    {
        #region Constructor    

        public ICommand GetNewlyAddedRecipeCommand { get; private set; }
        public ICommand RefreshNewlyAddedRecipeCommand { get; private set; }

        public NewlyAddedRecipeViewModel()
        {
            GetNewlyAddedRecipeCommand = new Command(() => GetNewlyAddedRecipeExecute());
            RefreshNewlyAddedRecipeCommand = new Command(() => RefreshNewlyAddedRecipeExecute());

        }

        #endregion

        #region Property

        private ObservableCollection<NewlyAddedRecipe> newlyAddedRecipes;

        public ObservableCollection<NewlyAddedRecipe> NewlyAddedRecipes
        {
            get { return newlyAddedRecipes; }
            set { newlyAddedRecipes = value; RaisePropertyChanged(() => NewlyAddedRecipes); }
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


        #endregion

        #region Method

        #endregion

        #region Command Excute

        public void GetNewlyAddedRecipeExecute()
        {
            try
            {

            var obj = new CommonRequest();
            UserDialogs.Instance.ShowLoading("Requesting..");
            userManager.getNewlyAddedRecipe(obj, () =>
            { 
                var newlyAddedRecipe = userManager.NewlyAddedRecipeResponse; 
                if (newlyAddedRecipe.StatusCode == 200)
                {
                    UserDialogs.Instance.HideLoading();
                    NewlyAddedRecipes = new ObservableCollection<NewlyAddedRecipe>(newlyAddedRecipe.NewlyAddedRecipes);
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new NewlyAddedRecipes());
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

        public void RefreshNewlyAddedRecipeExecute()
        {
            try
            {

                IsRefreshing = true;
                IsVisbleSearchBar = false;
                SearchBarText = "";
                var obj = new CommonRequest();
                
                userManager.getNewlyAddedRecipe(obj, () =>
                {
                    IsRefreshing = false;
                    var newlyAddedRecipe = userManager.NewlyAddedRecipeResponse;
                    if (newlyAddedRecipe.StatusCode == 200)
                    {
                        UserDialogs.Instance.HideLoading();
                        NewlyAddedRecipes = new ObservableCollection<NewlyAddedRecipe>(newlyAddedRecipe.NewlyAddedRecipes);

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

        #endregion



    }
}