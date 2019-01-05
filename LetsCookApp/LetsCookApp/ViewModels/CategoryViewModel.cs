
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
    public class CategoryViewModel : BaseViewModel
    {
        #region Constructor    

        public ICommand GetCotegaryCommand { get; private set; }
        public ICommand GetSubCotegaryCommand { get; private set; }
        public ICommand GetDishViewCommand { get; private set; }
        public ICommand SavefavRecipeCommand { get; private set; }
        public ICommand SaveShoppingCommand { get; private set; }
        public ICommand ShareRecipeBeEmailCommand { get; private set; }

        public CategoryViewModel()
        {
            GetCotegaryCommand = new Command(() => GetCotegaryExecute());
            GetSubCotegaryCommand = new Command(() => GetSubCotegaryExecute());
            GetDishViewCommand = new Command(() => GetDishViewExecute());
            SavefavRecipeCommand = new Command(() => SavefavRecipeExecute());
            SaveShoppingCommand = new Command(() => SaveShoppingExecute());
            ShareRecipeBeEmailCommand = new Command(() => ShareRecipeBeEmailExecute());
        }


        public void test()
        {


        }
        #endregion

        #region Property

        private string shareEmail;

        public string ShareEmail
        {
            get { return shareEmail; }
            set { shareEmail = value; RaisePropertyChanged(() => ShareEmail); }
        }

        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; RaisePropertyChanged(() => FirstName); }
        }

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; RaisePropertyChanged(() => LastName); }
        }


        private int height = 40;
        public int TitleHeight
        {
            get { return height; }
            set { height = value; RaisePropertyChanged(() => TitleHeight); }
        }

        private List<Category> categories;
        public List<Category> Categories
        {
            get { return categories; }
            set { categories = value; RaisePropertyChanged(() => Categories); }
        }

        private List<Recipe> recipe;
        public List<Recipe> Recipes
        {
            get { return recipe; }
            set { recipe = value; RaisePropertyChanged(() => Recipes); }
        }

        private DishViewResponse dishViewResponse;
        public DishViewResponse DishViewResponse
        {
            get { return dishViewResponse; }
            set { dishViewResponse = value; RaisePropertyChanged(() => DishViewResponse); }
        }

        private RecipeDishView recipeDishView;
        public RecipeDishView RecipeDishView
        {
            get { return recipeDishView; }
            set { recipeDishView = value; RaisePropertyChanged(() => RecipeDishView); }
        }

        private List<Ingredient> ingredients;
        public List<Ingredient> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; RaisePropertyChanged(() => Ingredients); }
        }

        private List<int> ingredientIds=new List<int> ();
        public List<int> IngredientIds
        {
            get { return ingredientIds; }
            set { ingredientIds = value; RaisePropertyChanged(() => IngredientIds); }
        }

        private int catId;

        public int CatId
        {
            get { return catId; }
            set { catId = value; RaisePropertyChanged(() => CatId); }
        }

        private int recipeId;

        public int RecipeId
        {
            get { return recipeId; }
            set { recipeId = value; RaisePropertyChanged(() => RecipeId); }
        }


        private string subCategoryTitle;

        public string SubCategoryTitle
        {
            get { return subCategoryTitle; }
            set { subCategoryTitle = value; RaisePropertyChanged(() => SubCategoryTitle); }
        }


        #endregion

        #region Method

        #endregion

        #region Command Excute

        public void GetCotegaryExecute()
        {
            try
            {
                var obj = new CommonRequest();
                UserDialogs.Instance.ShowLoading("Requesting..");
                userManager.getAllCategory(obj, () =>
                {

                    var categoryResponse = userManager.CategoryResponse;
                    if (categoryResponse.StatusCode == 200)
                    {
                        UserDialogs.Instance.HideLoading();
                        Categories = new List<Category>(categoryResponse.Categories);
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            App.Current.MainPage = new Views.HomeView();
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
                UserDialogs.Instance.Alert(ex.Message , null, "OK");
            }
        }

        public void GetSubCotegaryExecute()
        {
            try
            {

            var obj = new SubCategoryRequest()
            {
                CatId = CatId
            };
            UserDialogs.Instance.ShowLoading("Requesting..");
            userManager.getSubCategory(obj, () =>
            {
                var subCategoryResponse = userManager.SubCategoryResponse;
                if (subCategoryResponse.StatusCode == 200)
                {
                    UserDialogs.Instance.HideLoading();
                    Recipes = new List<Recipe>(subCategoryResponse.Recipes);
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new SubCategoryView());
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
                UserDialogs.Instance.Alert(ex.Message , null, "OK");
            }
        }

        public void GetDishViewExecute()
        {
            try
            {
                var obj = new DishViewRequest()
                {
                    RecipeId = RecipeId
                };
                UserDialogs.Instance.ShowLoading("Requesting..");
                userManager.getDishView(obj, () =>
                {
                    var dishViewResponse = userManager.DishViewResponse;
                    if (dishViewResponse.StatusCode == 200)
                    {
                        UserDialogs.Instance.HideLoading();
                        dishViewResponse = new DishViewResponse() { Recipe = dishViewResponse.Recipe };
                        RecipeDishView = dishViewResponse.Recipe;
                        Ingredients = RecipeDishView.Ingredients;//.FindAll(p=>p.IsItemSelected=false);
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            //await  App.Current.MainPage.Navigation.PushAsync(new DishView());
                            await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new DishView());
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
        public void SavefavRecipeExecute()
        {
            try
            {
                var obj = new SaveFavRecipeRequest()
                {
                    Recipe_Id = RecipeId,
                    Comments = "This is nice recipe",
                    Favorite = "Yes",
                    Member_Id = Convert.ToInt32(App.AppSetup.HomeViewModel.UserId)
                };
                UserDialogs.Instance.ShowLoading("Requesting..");
                userManager.SavefaRrecipe(obj, () =>
                {
                    UserDialogs.Instance.HideLoading();
                    var savefaRrecipeResponse = userManager.SavefaRrecipeResponse;
                    if (savefaRrecipeResponse.StatusCode == 200)
                    {
                        UserDialogs.Instance.Alert(savefaRrecipeResponse.Message, null, "OK");
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
        public void SaveShoppingExecute()
        {
            try
            {
                var obj = new SaveShoppingRequest()
                {
                    Recipe_Id = RecipeId,
                    Ingredient_Id = 5,//IngredientIds
                    Member_Id = Convert.ToInt32(App.AppSetup.HomeViewModel.UserId)
                };
                UserDialogs.Instance.ShowLoading("Requesting..");
                userManager.SaveShopping(obj, () =>
                {
                    UserDialogs.Instance.HideLoading();
                    var saveShoppingResponse = userManager.SaveShoppingResponse;
                    if (saveShoppingResponse.StatusCode == 200)
                    {
                        UserDialogs.Instance.Alert(saveShoppingResponse.Message, null, "OK");
                    }
                    else
                    {
                        UserDialogs.Instance.Alert(saveShoppingResponse.Message, null, "OK");
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

        public void ShareRecipeBeEmailExecute()
        {
            try
            {
                var obj = new ShareRecipeBeEmailRequest()
                {

                    RecipeId = Convert.ToString(RecipeId),
                    Email = ShareEmail,//"santoshkundkar12@gmail.com",//ShareEmail,
                    FirstName = FirstName,
                    LastName = LastName
                };
                UserDialogs.Instance.ShowLoading("Requesting..");
                userManager.ShareRecipeByEmail(obj, () =>
                {
                    UserDialogs.Instance.HideLoading();
                    var response = userManager.ShareRecipeBeEmailResponse;

                    if (response.StatusCode == 200)
                    {
                        UserDialogs.Instance.Alert(response.Message, null, "OK");
                        Rg.Plugins.Popup.Services.PopupNavigation.PopAsync();
                    }
                    else
                    {
                        UserDialogs.Instance.Alert(response.Message, null, "OK");
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