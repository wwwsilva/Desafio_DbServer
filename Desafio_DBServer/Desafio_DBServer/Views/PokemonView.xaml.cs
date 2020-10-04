using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Desafio_DBServer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PokemonView : PopupPage
    {
        public PokemonView()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            if (Device.RuntimePlatform != Device.Android)
                return base.OnBackButtonPressed();
            else
                return true;
        }
    }
}