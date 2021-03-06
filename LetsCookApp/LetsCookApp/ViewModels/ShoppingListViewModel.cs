﻿using Acr.UserDialogs;
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
 

    public class ShoppingListViewModel : BaseViewModel
    {
        #region Constructor    

        public ICommand GetShoppingListByUserIdCommand { get; private set; }
        public ICommand RefreshShoppingListByUserIdCommand { get; private set; }
       

        public ShoppingListViewModel()
        {
            GetShoppingListByUserIdCommand = new Command(() => GetShoppingListByUserIdExecute());
            RefreshShoppingListByUserIdCommand = new Command(() => RefreshShoppingListByUserIdExecute()); 
        }

        #endregion

        #region Property

        private ObservableCollection<GroupedIngredientDetailModel> grouped;

        public ObservableCollection<GroupedIngredientDetailModel> Grouped
        {
            get { return grouped; }
            set { grouped = value; RaisePropertyChanged(() => Grouped); }
        }
        private ObservableCollection<ShoppingList> shoppingList;

        public ObservableCollection<ShoppingList> ShoppingList
        {
            get { return shoppingList; }
            set { shoppingList = value; RaisePropertyChanged(() => ShoppingList); }
        }
          private List<IngredientDetail> ingredientDetails;

        public List<IngredientDetail> IngredientDetails
        {
            get { return ingredientDetails; }
            set { ingredientDetails = value; RaisePropertyChanged(() => IngredientDetails); }
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

        public void GetShoppingListByUserIdExecute()
        {
            try
            { 
            var obj = new GetShoppingListByUserIdRequest()
            {
                UserId =Convert.ToInt32(App.AppSetup.HomeViewModel.UserId),
             
            };
            UserDialogs.Instance.ShowLoading("Requesting..");
            userManager.GetShoppingListByUserId(obj, () =>
            {

                var response = userManager.GetShoppingListByUserIdResponse;
               
                Grouped = new ObservableCollection<GroupedIngredientDetailModel>(); 
                foreach (var item in response.ShoppingList)
                {
                    var IngredientDetailGroup = new GroupedIngredientDetailModel() { LongName = item.recipeDetails.RecipeTitle, ShortName = " " };
                   
                    foreach (var rec in item.recipeDetails.IngredientDetails)
                    {
                        IngredientDetailGroup.Add(new IngredientDetail() { IngredientId = rec.IngredientId, IngredientName = rec.IngredientName });
                    }
                    Grouped.Add(IngredientDetailGroup);
                }
                
                if (response.StatusCode == 200)
                {
                    UserDialogs.Instance.HideLoading();
                    ShoppingList = new ObservableCollection<ShoppingList>(response.ShoppingList);
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new ShoppingListView());
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

        public void RefreshShoppingListByUserIdExecute()
        {
            try
            {
                IsRefreshing = true;
                IsVisbleSearchBar = false;
                SearchBarText = "";
                var obj = new GetShoppingListByUserIdRequest()
                {
                    UserId = Convert.ToInt32(App.AppSetup.HomeViewModel.UserId),

                }; 
                userManager.GetShoppingListByUserId(obj, () =>
                {

                    IsRefreshing = false;
                    var response = userManager.GetShoppingListByUserIdResponse;

                    Grouped = new ObservableCollection<GroupedIngredientDetailModel>();
                    foreach (var item in response.ShoppingList)
                    {
                        var IngredientDetailGroup = new GroupedIngredientDetailModel() { LongName = item.recipeDetails.RecipeTitle, ShortName = " " };

                        foreach (var rec in item.recipeDetails.IngredientDetails)
                        {
                            IngredientDetailGroup.Add(new IngredientDetail() { IngredientId = rec.IngredientId, IngredientName = rec.IngredientName });
                        }
                        Grouped.Add(IngredientDetailGroup);
                    }

                    if (response.StatusCode == 200)
                    {

                        UserDialogs.Instance.HideLoading();
                        ShoppingList = new ObservableCollection<ShoppingList>(response.ShoppingList);
                        

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