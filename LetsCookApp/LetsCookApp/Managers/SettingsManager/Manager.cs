using Acr.UserDialogs;
using LetsCookApp.Managers.ApiProvider;
using LetsCookApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LetsCookApp.Managers.SettingsManager
{
    public class Manager : IManager
    {
        private readonly IApiProvider _apiProvider;
        private readonly ISettingsManager _settingsManager;

        public CategoryResponse CategoryResponse { get { return categoryResponse; } }
        public SubCategoryResponse SubCategoryResponse { get { return subCategoryResponse; } }
        public DishViewResponse DishViewResponse { get { return dishViewResponse; } }
        public PopularRecipeResponse PopularRecipeResponse { get { return popularRecipeResponse; } }
        public GetFavsByUserIdResponse GetFavsByUserIdResponse { get { return getFavsByUserIdResponse; } }
        public GetShoppingListByUserIdResponse GetShoppingListByUserIdResponse { get { return getShoppingListByUserIdResponse; } }
        public NewlyAddedRecipeResponse NewlyAddedRecipeResponse { get { return newlyAddedRecipeResponse; } }
        public ProfileResponse ProfileResponse { get { return profileResponse; } } 
        public LoginResponse LoginResponse { get { return loginResponse; } }
        public BaseResponseModel SaveHelpResponse { get { return saveHelpResponse; } }
        public BaseResponseModel ShareRecipeBeEmailResponse { get { return shareRecipeBeEmailResponse; } }

        public SignupResponse SignupResponse { get { return signupResponse; } }
        public BaseResponseModel SavefaRrecipeResponse { get { return saveFavRecipeResponse; } }
        public BaseResponseModel SaveShoppingResponse { get { return saveShoppingResponse; } }

        public CountryResponse CountryResponse { get { return countryResponse; } }
        public FriendResponse FriendResponse { get { return friendResponse; } }
        public Manager(IApiProvider apiProvider, ISettingsManager settingsManager)
        {
            _apiProvider = apiProvider;
            _settingsManager = settingsManager;
        }
        #region Header
        public Dictionary<string, string> GetHeaders()
        {
            Dictionary<string, string> header = new Dictionary<string, string>();
           // header.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("LetsCookApp" + ":" + "LetsCookApp")));
            return header;
        }

        #endregion
        string error = "Please Check Internet Connection.";
        private CategoryResponse categoryResponse { get; set; }
        private SubCategoryResponse subCategoryResponse { get; set; }
        private DishViewResponse dishViewResponse { get; set; }
        private PopularRecipeResponse popularRecipeResponse { get; set; }
        private GetFavsByUserIdResponse getFavsByUserIdResponse { get; set; }
        private GetShoppingListByUserIdResponse getShoppingListByUserIdResponse { get; set; }
        private ProfileResponse profileResponse { get; set; }
        private CountryResponse countryResponse { get; set; }

        private NewlyAddedRecipeResponse newlyAddedRecipeResponse { get; set; }
        private FriendResponse friendResponse { get; set; }

        private LoginResponse loginResponse { get; set; }
        private SignupResponse signupResponse { get; set; }
        private BaseResponseModel saveFavRecipeResponse { get; set; }
        private BaseResponseModel saveShoppingResponse { get; set; }
        private BaseResponseModel saveHelpResponse { get; set; }
        private BaseResponseModel shareRecipeBeEmailResponse { get; set; }
        public async void  getAllCategory(CommonRequest commonRequest, Action success, Action<CategoryResponse> failed)
        {
            bool IsNetwork = true;//await DependencyService.Get<IMediaService>().CheckNewworkConnectivity();
            if (IsNetwork)
            {

                var url = string.Format("{0}getAllCategories.php", _settingsManager.ApiHost);

                await Task.Run( () =>
                {
                    Dictionary<string, string> head = GetHeaders();
                    var result =  _apiProvider.Get<CategoryResponse, CommonRequest>(url,  null).Result;
                    if (result.IsSuccessful)
                    {
                        if (success != null)
                        {
                            categoryResponse = result.Result;
                            success.Invoke();
                        }
                    }
                    else
                    {
                        failed.Invoke(result.Result);
                    }
                });
            }
            else
            {
                UserDialogs.Instance.HideLoading(); UserDialogs.Instance.Alert(error, null, "OK");
            }
        }
        public async void getSubCategory(SubCategoryRequest commonRequest, Action success, Action<SubCategoryResponse> failed)
        {
            bool IsNetwork = true;//await DependencyService.Get<IMediaService>().CheckNewworkConnectivity();
            if (IsNetwork)
            {
                string para = "catId=" + commonRequest.CatId ;
                var url = string.Format("{0}getRecipesByCat.php?" + para, _settingsManager.ApiHost); 

                await Task.Run(() =>
                {
                    Dictionary<string, string> head = GetHeaders();
                    var result = _apiProvider.Get<SubCategoryResponse, SubCategoryRequest>(url, null).Result;
                    if (result.IsSuccessful)
                    {
                        if (success != null)
                        {
                            subCategoryResponse = result.Result;
                            success.Invoke();
                        }
                    }
                    else
                    {
                        failed.Invoke(result.Result);
                    }
                });
            }
            else
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert(error, null, "OK");
            }
        }
        public async void getDishView(DishViewRequest commonRequest, Action success, Action<DishViewResponse> failed)
        {
            bool IsNetwork = true;//await DependencyService.Get<IMediaService>().CheckNewworkConnectivity();
            if (IsNetwork)
            {
                string para = "RecipeId=" + commonRequest.RecipeId;
                var url = string.Format("{0}getRecipeById.php?" + para, _settingsManager.ApiHost);

                await Task.Run(() =>
                {
                    Dictionary<string, string> head = GetHeaders();
                    var result = _apiProvider.Get<DishViewResponse, DishViewRequest>(url, null).Result;
                    if (result.IsSuccessful)
                    {
                        if (success != null)
                        {
                            dishViewResponse = result.Result;
                            success.Invoke();
                        }
                    }
                    else
                    {
                        failed.Invoke(result.Result);
                    }
                });
            }
            else
            {
                UserDialogs.Instance.HideLoading(); UserDialogs.Instance.Alert(error, null, "OK");
            }
        }
          public async void getPopularRecipe(CommonRequest commonRequest, Action success, Action<PopularRecipeResponse> failed)
        {
            bool IsNetwork = true;//await DependencyService.Get<IMediaService>().CheckNewworkConnectivity();
            if (IsNetwork)
            {
               // string para = "RecipeId=" + commonRequest.RecipeId;
                var url = string.Format("{0}popularrecipe.php?", _settingsManager.ApiHost);

                await Task.Run(() =>
                {
                    Dictionary<string, string> head = GetHeaders();
                    var result = _apiProvider.Get<PopularRecipeResponse, CommonRequest>(url, null).Result;
                    if (result.IsSuccessful)
                    {
                        if (success != null)
                        {
                            popularRecipeResponse = result.Result;
                            success.Invoke();
                        }
                    }
                    else
                    {
                        failed.Invoke(result.Result);
                    }
                });
            }
            else
            {
                UserDialogs.Instance.HideLoading(); UserDialogs.Instance.Alert(error, null, "OK");
            }
        }

        public async void GetFavsByUserId(GetFavsByUserIdRequest commonRequest, Action success, Action<GetFavsByUserIdResponse> failed)
        {
            bool IsNetwork = true;//await DependencyService.Get<IMediaService>().CheckNewworkConnectivity();
            if (IsNetwork)
            {
                 string para = "userId=" + commonRequest.UserId;
                var url = string.Format("{0}getFavsByUserId.php?"+ para, _settingsManager.ApiHost);

                await Task.Run(() =>
                {
                    Dictionary<string, string> head = GetHeaders();
                    var result = _apiProvider.Get<GetFavsByUserIdResponse, GetFavsByUserIdRequest>(url, null).Result;
                    if (result.IsSuccessful)
                    {
                        if (success != null)
                        {
                            getFavsByUserIdResponse = result.Result;
                            success.Invoke();
                        }
                    }
                    else
                    {
                        failed.Invoke(result.Result);
                    }
                });
            }
            else
            {
                UserDialogs.Instance.HideLoading(); UserDialogs.Instance.Alert(error, null, "OK");
            }
        }

        public async void GetShoppingListByUserId(GetShoppingListByUserIdRequest request, Action success, Action<GetShoppingListByUserIdResponse> failed)
        {
            bool IsNetwork = true;//await DependencyService.Get<IMediaService>().CheckNewworkConnectivity();
            if (IsNetwork)
            {
                string para = "userId=" + request.UserId;
                var url = string.Format("{0}getShoppingListByUserId.php?" + para, _settingsManager.ApiHost);

                await Task.Run(() =>
                {
                    Dictionary<string, string> head = GetHeaders();
                    var result = _apiProvider.Get<GetShoppingListByUserIdResponse, GetShoppingListByUserIdRequest>(url, null).Result;
                    if (result.IsSuccessful)
                    {
                        if (success != null)
                        {
                            getShoppingListByUserIdResponse = result.Result;
                            success.Invoke();
                        }
                    }
                    else
                    {
                        failed.Invoke(result.Result);
                    }
                });
            }
            else
            {
                UserDialogs.Instance.HideLoading(); UserDialogs.Instance.Alert(error, null, "OK");
            }
        }


        public async void getProfile(GetProfileRequest commonRequest, Action success, Action<ProfileResponse> failed)
        {
            bool IsNetwork = true;//await DependencyService.Get<IMediaService>().CheckNewworkConnectivity();
            if (IsNetwork)
            {
                //commonRequest.Email = "ksantosh.kundkar12@gmail.com";
                //commonRequest.Password = "123456";
                string para = "email=" + commonRequest.EmailId + "&userid=" + commonRequest.UserId;
                var url = string.Format("{0}profile.php?"+para, _settingsManager.ApiHost);

                await Task.Run(() =>
                {
                    Dictionary<string, string> head = GetHeaders();
                    var result = _apiProvider.Get<ProfileResponse,LoginRequest >(url, null).Result;
                    if (result.IsSuccessful)
                    {
                        if (success != null)
                        {
                            profileResponse = result.Result;
                            success.Invoke();
                        }
                    }
                    else
                    {
                        failed.Invoke(result.Result);
                    }
                });
            }
            else
            {
                UserDialogs.Instance.HideLoading(); UserDialogs.Instance.Alert(error, null, "OK");
            }
        }
        public async void getNewlyAddedRecipe(CommonRequest commonRequest, Action success, Action<NewlyAddedRecipeResponse> failed)
        {
            bool IsNetwork = true;//await DependencyService.Get<IMediaService>().CheckNewworkConnectivity();
            if (IsNetwork)
            {
                var url = string.Format("{0}getnewlyaddrecipe.php?", _settingsManager.ApiHost);

                await Task.Run(() =>
                {
                    Dictionary<string, string> head = GetHeaders();
                    var result = _apiProvider.Get<NewlyAddedRecipeResponse, CommonRequest>(url, null).Result;
                    if (result.IsSuccessful)
                    {
                        if (success != null)
                        {
                            newlyAddedRecipeResponse = result.Result;
                            success.Invoke();
                        }
                    }
                    else
                    {
                        failed.Invoke(result.Result);
                    }
                });
            }
            else
            {
                UserDialogs.Instance.HideLoading(); UserDialogs.Instance.Alert(error, null, "OK");
            }
        }


        public async void getFriends(FriendRequest friendRequest, Action success, Action<FriendResponse> failed)
        {
            bool IsNetwork = true;//await DependencyService.Get<IMediaService>().CheckNewworkConnectivity();
            if (IsNetwork)
            {
                string para = "userId=" + friendRequest.UserId;
                var url = string.Format("{0}getFriendsByUserId.php?"+ para, _settingsManager.ApiHost);

                await Task.Run(() =>
                {
                    Dictionary<string, string> head = GetHeaders();
                    var result = _apiProvider.Get<FriendResponse, FriendRequest>(url, null).Result;
                    if (result.IsSuccessful)
                    {
                        if (success != null)
                        {
                            friendResponse = result.Result;
                            success.Invoke();
                        }
                    }
                    else
                    {
                        failed.Invoke(result.Result);
                    }
                });
            }
            else
            {
                UserDialogs.Instance.HideLoading(); UserDialogs.Instance.Alert(error, null, "OK");
            }
        }

        public async void getCountry(CommonRequest commonRequest, Action success)
        {
            bool IsNetwork = true;//await DependencyService.Get<IMediaService>().CheckNewworkConnectivity();
            if (IsNetwork)
            {
               
                var url = string.Format("{0}country.php?" , _settingsManager.ApiHost);

                await Task.Run(() =>
                {
                    Dictionary<string, string> head = GetHeaders();
                    var result = _apiProvider.Get<CountryResponse,CommonRequest>(url, null).Result;
                    if (result.IsSuccessful)
                    {
                        if (success != null)
                        {
                            countryResponse = result.Result;
                            success.Invoke();
                        }
                    }
                    //else
                    //{
                    //    failed.Invoke(result.Result);
                    //}
                });
            }
            else
            {
                UserDialogs.Instance.HideLoading(); UserDialogs.Instance.Alert(error, null, "OK");
            }
        }

        public async void SavefaRrecipe(SaveFavRecipeRequest saveFavRecipeRequest, Action success, Action<BaseResponseModel> failed)
        {
            bool IsNetwork = true;//await DependencyService.Get<IMediaService>().CheckNewworkConnectivity();
            if (IsNetwork)
            {

                var url = string.Format("{0}savefavrecipe.php", _settingsManager.ApiHost);

                await Task.Factory.StartNew(() =>
                {

                    var result = _apiProvider.Post<BaseResponseModel, SaveFavRecipeRequest>(url, saveFavRecipeRequest).Result;
                    if (result.IsSuccessful)
                    {
                        if (success != null)
                        {
                            saveFavRecipeResponse = result.Result;
                            success.Invoke();
                        }
                    }
                    else
                    {
                        failed.Invoke(result.Result);
                    }
                });
            }
            else
            {
                UserDialogs.Instance.HideLoading(); UserDialogs.Instance.Alert(error, null, "OK");
            }
        }

        public async void SaveShopping(SaveShoppingRequest saveShoppingRequest, Action success, Action<BaseResponseModel> failed)
        {
            bool IsNetwork = true;//await DependencyService.Get<IMediaService>().CheckNewworkConnectivity();
            if (IsNetwork)
            {

                var url = string.Format("{0}saveshoppinglist.php", _settingsManager.ApiHost);

                await Task.Factory.StartNew(() =>
                {

                    var result = _apiProvider.Post<BaseResponseModel, SaveShoppingRequest>(url, saveShoppingRequest).Result;
                    if (result.IsSuccessful)
                    {
                        if (success != null)
                        {
                            saveShoppingResponse = result.Result;
                            success.Invoke();
                        }
                    }
                    else
                    {
                        failed.Invoke(result.Result);
                    }
                });
            }
            else
            {
                UserDialogs.Instance.HideLoading(); UserDialogs.Instance.Alert(error, null, "OK");
            }
        }


        public async void Login(LoginRequest request, Action success, Action<BaseResponseModel> failed)
        {
            var pp = request;
            bool IsNetwork = true;//await DependencyService.Get<IMediaService>().CheckNewworkConnectivity();
            if (IsNetwork)
            {
               
                var url = string.Format("{0}login.php", _settingsManager.ApiHost);

                await Task.Factory.StartNew(() =>
                {
                   
                    var result = _apiProvider.Post<LoginResponse, LoginRequest>(url, request).Result;
                    if (result.IsSuccessful)
                    {
                        if (success != null)
                        {
                            loginResponse = result.Result;
                            success.Invoke();
                        }
                    }
                    else
                    {
                        failed.Invoke(result.Result);
                    }
                });
            }
            else
            {
                UserDialogs.Instance.HideLoading(); UserDialogs.Instance.Alert(error, null, "OK");
            }
        }

        public async void ForgetPassword(LoginRequest request, Action success, Action<BaseResponseModel> failed)
        {
            var pp = request;
            bool IsNetwork = true;//await DependencyService.Get<IMediaService>().CheckNewworkConnectivity();
            if (IsNetwork)
            {

                var url = string.Format("{0}forgotpassword.php", _settingsManager.ApiHost);

                await Task.Factory.StartNew(() =>
                {

                    var result = _apiProvider.Post<LoginResponse, LoginRequest>(url, request).Result;
                    if (result.IsSuccessful)
                    {
                        if (success != null)
                        {
                            loginResponse = result.Result;
                            success.Invoke();
                        }
                    }
                    else
                    {
                        failed.Invoke(result.Result);
                    }
                });
            }
            else
            {
                UserDialogs.Instance.HideLoading(); UserDialogs.Instance.Alert(error, null, "OK");
            }
        }


        public async void SignUp(SignupRequest signupRequest, Action success, Action<SignupResponse> failed)
        {
            bool IsNetwork = true;//await DependencyService.Get<IMediaService>().CheckNewworkConnectivity();
            if (IsNetwork)
            {

                var url = string.Format("{0}register.php", _settingsManager.ApiHost);

                await Task.Factory.StartNew(() =>
                {

                    var result = _apiProvider.Post<SignupResponse, SignupRequest>(url, signupRequest).Result;
                    if (result.IsSuccessful)
                    {
                        if (success != null)
                        {
                            signupResponse = result.Result;
                            success.Invoke();
                        }
                    }
                    else
                    {
                        failed.Invoke(result.Result);
                    }
                });
            }
            else
            {
                UserDialogs.Instance.HideLoading(); UserDialogs.Instance.Alert(error, null, "OK");
            }
        }

        public async void SignUpUpdate(ProfileUpdateRequest signupRequest, Action success, Action<SignupResponse> failed)
        {
            bool IsNetwork = true;//await DependencyService.Get<IMediaService>().CheckNewworkConnectivity();
            if (IsNetwork)
            {

                var url = string.Format("{0}profile.php", _settingsManager.ApiHost);

                await Task.Factory.StartNew(() =>
                {

                    var result = _apiProvider.Post<SignupResponse, ProfileUpdateRequest>(url, signupRequest).Result;
                    if (result.IsSuccessful)
                    {
                        if (success != null)
                        {
                            signupResponse = result.Result;
                            success.Invoke();
                        }
                    }
                    else
                    {
                        failed.Invoke(result.Result);
                    }
                });
            }
            else
            {
                UserDialogs.Instance.HideLoading(); UserDialogs.Instance.Alert(error, null, "OK");
            }
        }

        public async void SaveHelp(SaveHelpRequest saveHelpRequest, Action success, Action<BaseResponseModel> failed)
        {
            bool IsNetwork = true;//await DependencyService.Get<IMediaService>().CheckNewworkConnectivity();
            if (IsNetwork)
            {

                var url = string.Format("{0}savehelp.php", _settingsManager.ApiHost);

                await Task.Factory.StartNew(() =>
                {

                    var result = _apiProvider.Post<BaseResponseModel, SaveHelpRequest>(url, saveHelpRequest).Result;
                    if (result.IsSuccessful)
                    {
                        if (success != null)
                        {
                            saveHelpResponse = result.Result;
                            success.Invoke();
                        }
                    }
                    else
                    {
                        failed.Invoke(result.Result);
                    }
                });
            }
            else
            {
                UserDialogs.Instance.HideLoading(); UserDialogs.Instance.Alert(error, null, "OK");
            }
        }
        public async void ShareRecipeByEmail(ShareRecipeBeEmailRequest shareRecipeBeEmailRequest, Action success, Action<BaseResponseModel> failed)
        {
            bool IsNetwork = true;//await DependencyService.Get<IMediaService>().CheckNewworkConnectivity();
            if (IsNetwork)
            {

                var url = string.Format("{0}shareRecipeByEmail.php", _settingsManager.ApiHost);

                await Task.Factory.StartNew(() =>
                {

                    var result = _apiProvider.Post<BaseResponseModel, ShareRecipeBeEmailRequest>(url, shareRecipeBeEmailRequest).Result;
                    if (result.IsSuccessful)
                    {
                        if (success != null)
                        {
                            shareRecipeBeEmailResponse= result.Result;
                            success.Invoke();
                        }
                    }
                    else
                    {
                        failed.Invoke(result.Result);
                    }
                });
            }
            else
            {
                UserDialogs.Instance.HideLoading(); UserDialogs.Instance.Alert(error, null, "OK");
            }
        }


    }
}
