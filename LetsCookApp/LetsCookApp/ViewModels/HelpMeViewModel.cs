using Acr.UserDialogs;
using LetsCookApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LetsCookApp.ViewModels
{
  public class HelpMeViewModel : BaseViewModel
    {
        #region Constructor    

        public ICommand SaveHelpCommand { get; private set; }

        public HelpMeViewModel()
        {
            SaveHelpCommand = new Command(() => SaveHelpExecute());

        }

        #endregion

        #region Property
     

        private string query;

        public string Query
        {
            get { return query; }
            set { query = value; RaisePropertyChanged(() => Query); }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(() => Name); }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; RaisePropertyChanged(() => Email); }
        }

        private string contact;

        public string Contact
        {
            get { return contact; }
            set { contact = value; RaisePropertyChanged(() => Contact); }
        }


        #endregion

        #region Method

        #endregion

        #region Command Excute

        public void SaveHelpExecute()
        {
            try
            { 
            UserDialogs.Instance.ShowLoading("Requesting..");
            var obj = new SaveHelpRequest()
            {
                Member_Id = Convert.ToString(App.AppSetup.HomeViewModel.UserId),
                Contact=Contact,
                Email=Email,
                Name=Name,
                Query=Query
            }; 
            userManager.SaveHelp(obj, () =>
            {

                var response = userManager.SaveHelpResponse;
                if (response.StatusCode == 200)
                {
                    UserDialogs.Instance.HideLoading();
                    Clear();
                    UserDialogs.Instance.Alert(response.Message, null, "OK");
                    //NewlyAddedRecipes = new List<NewlyAddedRecipe>(newlyAddedRecipe.NewlyAddedRecipes);
                    //Device.BeginInvokeOnMainThread(async () =>
                    //{
                    //    await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new ShoppingListView());
                    //});

                }
            },
             (requestFailedReason) =>
             {
                 Device.BeginInvokeOnMainThread(() =>
                 {
                     UserDialogs.Instance.HideLoading();
                     UserDialogs.Instance.Alert(requestFailedReason.Message, null, "OK");                     
                 });
             });
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    UserDialogs.Instance.HideLoading();
                    UserDialogs.Instance.Alert(ex.Message, null, "OK");
                });
            }
        }

        #endregion

        #region Clear

        public void Clear()
        {
            Name = Email = Contact = Query = "";
        }
        #endregion



    }
}