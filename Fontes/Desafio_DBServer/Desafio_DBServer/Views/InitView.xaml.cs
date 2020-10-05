using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Desafio_DBServer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InitView : ContentPage
    {
        double imgPokeballTranslationX = 0;
        double imgPokeballTranslationY = 0;

        public InitView()
        {
            InitializeComponent();
        }


        private async Task Animate()
        {
            await Task.Delay(1000);
            imgPokeballTranslationX = imgPokeball.TranslationX;
            imgPokeballTranslationY = imgPokeball.TranslationY;
            imgPokeball.TranslateTo(imgPokeballTranslationX, imgPokeballTranslationY - 150, 10);
            await imgPokeball.ScaleTo(0, 10);

            await frmBase.FadeTo(1, 10);

            imgTitle.ScaleTo(1, 1000);
            imgTitle.FadeTo(1, 1000);
            imgTitle.TranslateTo(imgTitle.TranslationX, imgTitle.TranslationY + 100, 1000);
            await imgBackground.FadeTo(0.5, 1000);

            imgBackground.FadeTo(1, 1000);
            imgPokeball.ScaleTo(1, 2000);
            await imgPokeball.TranslateTo(imgPokeballTranslationX, imgPokeballTranslationY, 2000, Easing.BounceOut);

            imgPikachu.FadeTo(1, 500);
            imgPokeball.RotateTo(45, 300);
            await imgPokeball.TranslateTo(imgPokeballTranslationX + 70, imgPokeballTranslationY, 300);
        }

        private async void frmBase_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Renderer"))
                await Animate();

            base.OnPropertyChanged();
        }
    }
}