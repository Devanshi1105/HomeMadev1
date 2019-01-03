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
    public partial class EmailPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public EmailPopup()
        {
            InitializeComponent();
            BindingContext = App.AppSetup.CategoryViewModel;
        }


    }
}
