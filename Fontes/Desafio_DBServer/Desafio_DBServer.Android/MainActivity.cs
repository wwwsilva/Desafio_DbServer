    using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Desafio_DBServer.Droid.Config;
using Desafio_DBServer.Enumerates;
using Desafio_DBServer.Helpers;
using Desafio_DBServer.ViewModels;
using System;

namespace Desafio_DBServer.Droid
{
    [Activity(Label = "Desafio DBServer", Icon = "@drawable/PokeBall", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            ContainerHelper.SQLiteConfig = new SQLiteConfig();

            NavigationHelper.AccessPageEvent += NavigationHelper_AccessPageEvent;

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 26, 25, 25));
        }

        private void NavigationHelper_AccessPageEvent(object sender, EventArgs e)
        {
            try
            {
                switch ((sender as BaseViewModel).Orientation)
                {
                    case OrientationEnum.Landscape:
                        RequestedOrientation = ScreenOrientation.Landscape;
                        break;
                    case OrientationEnum.LandscapeInverse:
                        RequestedOrientation = ScreenOrientation.ReverseLandscape;
                        break;
                    case OrientationEnum.Portrait:
                        RequestedOrientation = ScreenOrientation.Portrait;
                        break;
                    default:
                        RequestedOrientation = ScreenOrientation.Unspecified;
                        break;
                }
            }
            catch
            {
                RequestedOrientation = ScreenOrientation.Unspecified;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}