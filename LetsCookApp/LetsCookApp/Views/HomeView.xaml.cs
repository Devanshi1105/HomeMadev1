using LetsCookApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LetsCookApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : MasterDetailPage
    {
        public HomeView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = App.AppSetup.HomeViewModel;

            //var menupage = masterMenuPage;
            //Master = menupage;

            //masterMenuPage.menuList.ItemTapped += (sender, e) =>
            //{
            //    if (e.Item == null) return;
            //    NavigateTo(e.Item as ViewModels.Menu);
            //};

           
            this.SetBinding(MasterDetailPage.IsPresentedProperty, "IsMenuListPresented");
            //this.IsPresentedChanged += HomeView_IsPresentedChanged;

           // void HomeView_IsPresentedChanged(object sender, EventArgs e)
            //{
              //  App.AppSetup.HomeViewModel.IsMenuListPresented = ((MasterDetailPage)sender).IsPresented;
            //}
        }
        #region first
        // TODO: Set the navigation menu at the runtime.      
        void NavigateTo(ViewModels.Menu menu)
        {
            if (menu.TargetType == typeof(CategoriesView))
            {
                Detail.Navigation.PopToRootAsync();
            }
            else if (menu.TargetType == typeof(SignInView))
            {
                Detail.Navigation.PushAsync(new SignInView());
            }
            else if (menu.TargetType == typeof(SignUpView))
            {
                Detail.Navigation.PushAsync(new SignUpView());
            }

            else if (menu.TargetType == typeof(Nullable))
            {
                App.Current.MainPage = new NavigationPage(new Views.SignInSignUpView());
            }
            App.AppSetup.HomeViewModel.IsMenuListPresented = false;
        }
        #endregion    

    }
}