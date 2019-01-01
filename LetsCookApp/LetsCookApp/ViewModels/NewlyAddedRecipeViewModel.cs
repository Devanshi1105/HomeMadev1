using Acr.UserDialogs;
using LetsCookApp.Models;
using LetsCookApp.Views;
using System;
using System.Collections.Generic;
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

        public NewlyAddedRecipeViewModel()
        {
            GetNewlyAddedRecipeCommand = new Command(() => GetNewlyAddedRecipeExecute());

        }

        #endregion

        #region Property

        private List<NewlyAddedRecipe> newlyAddedRecipes;

        public List<NewlyAddedRecipe> NewlyAddedRecipes
        {
            get { return newlyAddedRecipes; }
            set { newlyAddedRecipes = value; RaisePropertyChanged(() => NewlyAddedRecipes); }
        }



        #endregion

        #region Method

        #endregion

        #region Command Excute

        public void GetNewlyAddedRecipeExecute()
        {
            var obj = new CommonRequest();
            UserDialogs.Instance.ShowLoading("Requesting..");
            userManager.getNewlyAddedRecipe(obj, () =>
            {

                var newlyAddedRecipe = userManager.NewlyAddedRecipeResponse;
                if (newlyAddedRecipe.StatusCode == 200)
                {
                    UserDialogs.Instance.HideLoading();
                    NewlyAddedRecipes = new List<NewlyAddedRecipe>(newlyAddedRecipe.NewlyAddedRecipes);
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
                     //  UserDialogs.Instance.Alert(requestFailedReason.Message, null, "OK");
                     UserDialogs.Instance.HideLoading();
                 });
             });
        }

        #endregion



    }
}