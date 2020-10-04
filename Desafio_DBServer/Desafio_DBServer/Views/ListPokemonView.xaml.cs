using System.ComponentModel;
using Xamarin.Forms;

namespace Desafio_DBServer.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ListPokemonView : ContentPage
    {
        public ListPokemonView()
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
