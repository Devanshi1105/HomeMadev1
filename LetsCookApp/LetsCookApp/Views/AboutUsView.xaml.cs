using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LetsCookApp.Views
{
    public partial class AboutUsView : ContentPage
    {
        public AboutUsView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            var source = new HtmlWebViewSource();
            var str0 = "<p> <span style=\"color: red; font-size:20px;padding: 10px 5px 10px 5px;\">Home Made Food</span> <span style=\"font-size:12px;\"> Veg Only</span> <p style=\"padding: 10px 5px 10px 5px;\">We offer vegetarian Recipes in English language because we feel that you can learn new recipes and dishes only in the language that you speak at home.<br>  <span style='color:red;'>Home Made Food (Veg Only) </span> Is a Global Platform for Indian community to share your Cooking Art and food to the world. <br><br> ";
            var str1 = "With our unique collection of Recipes in English, you don't have to browse the web every time you want to cook some food.Our app is going to be your one - stop solution for all cokking ideas. In our app, you will find -</p>";

            var str2 = "<p>  <p style=\"background-color:#d9d9d9;padding: 10px 5px 10px 10px;\"><span style=\"color:red;\">+ </span> <span> Indian Recipes in English language  </span> <br> <span style=\"color:red;\">+ </span> <span> Indian Food Recipes in English  </span><br> <span style=\"color:red;\">+ </span> <span> Fast Food recipes in English  </span><br> <span style=\"color:red;\">+ </span> <span> Punjabi Recipes in English </span> <br> <span style=\"color:red;\"> + </span> <span> Chines Recipes in English </span><br> <span style=\"color:red;\"> + </span> <span> South Indian Recipes in English, and many more...  </ span><br> </p> <p style=\"padding: 10px 5px 10px 10px;\"> You can easily share the recipes with others through email, Facebook, Whatsapp etc. directly from with in the app</p>  <p style=\"padding: 10px 5px 10px 10px;\"> Our unique layout based on different categories help you in easily finding your desired recipe without excessive search. <br> Indian food stand for Food and Business.You can find Indian in Every Corner of the World.This Platform is a connecting medium for such food  lovers!  </p>  <p style=\"background-color:#d9d9d9;padding: 10px 5px 10px 10px;\"> Ancient and Modern Research has proven this fact that 'Emotional State of food maker has a Great Impact on the Mind of the one who eats the Food.' If Great foood is cooked at home then people forget the hotel fod.We are on a mission to keep your  family members around your dining table!We are here  to discover and ignite hidden cooking quean in you.We are a community driven platform to  share what you have made today in your kitchen.So go ahead and enjoy your Indian Recipes in English lanhuage, and feel free to send us your suggestions. </p> <p style=\"padding: 10px 5px 10px 10px;\"> If you have own recipes in English, Please do share then with us.We will be more than happy to share your dishes on our platform along with your  name.We strongly belive that your unique cooking art sholud be heighlighted to the whole world so that everyone can benefit from your talent. </p>";
            var text = "<html>" +
                    "<body  style=\"text-align: justify;padding: 10px 5px 10px 0px;\">" +
                    String.Format("{0}{1}{2}", str0, str1, str2) +
                    "</body>" +
                    "</html>";
            source.Html = text;
            browser.Source = source;
        }
        private void Menu_Tapped(object sender, EventArgs e)
        {
            App.AppSetup.HomeViewModel.IsMenuListPresented = true;
        }
        private void Search_Tapped(object sender, EventArgs e)
        {
            var page = new SearchView();

            Rg.Plugins.Popup.Services.PopupNavigation.PushAsync(page);
        }
    }
}
