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
   public class PopularReceipesViewModel : BaseViewModel
    {
        #region Constructor    

        public ICommand GetPopularReceipeCommand { get; private set; }
      
        public PopularReceipesViewModel()
        {
            GetPopularReceipeCommand = new Command(() => GetPopularReceipeExecute());
           
        }


        public void test()
        {


        }
        #endregion

        #region Property 
        private List<PopularRecipe> popularRecipes;

        public List<PopularRecipe> PopularRecipes
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
            var obj = new CommonRequest();
            UserDialogs.Instance.ShowLoading("Requesting..");
            userManager.getPopularRecipe(obj, () =>
            {

                var popularRecipeResponse = userManager.PopularRecipeResponse;
                if (popularRecipeResponse.StatusCode == 200)
                {
                    UserDialogs.Instance.HideLoading();
                    PopularRecipes = new List<PopularRecipe>(popularRecipeResponse.PopularRecipes);
                    Device.BeginInvokeOnMainThread(async() =>
                    {
                       await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new PopularReceipesView());
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