using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LetsCookApp.Views
{
    public partial class ShoppingListView : ContentPage
    {
        public ShoppingListView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = App.AppSetup.ShoppingListViewModel;

            masterMenuList.ItemsSource = new List<Contacts>()
            {
    new Contacts() {
            Name = "Umair Here, you can see that we used the ImageCell and set Binding. We set Binding of ‘Text’ with ‘Name’, ", Num = "0456445450945", imgsource = "checkmark.png",
        },
        new Contacts() {
            Name = "Cat Here, you can see that we used the ImageCell and set Binding. We set Binding of ‘Text’ with ‘Name’, ", Num = "034456445905", imgsource = "checkmark.png",
        },
        new Contacts() {
            Name = "Nature Here, you can see that we used the ImageCell and set Binding. We set Binding of ‘Text’ with ‘Name’, ", Num = "56445905", imgsource = "checkmark.png",
        }
            };
        }
        private void Search_Tapped(object sender, EventArgs e)
        {
            var page = new SearchView();

            Rg.Plugins.Popup.Services.PopupNavigation.PushAsync(page);
        }
        private void Menu_Tapped(object sender, EventArgs e)
        {
            App.AppSetup.HomeViewModel.IsMenuListPresented = true;
        }
    }
}
