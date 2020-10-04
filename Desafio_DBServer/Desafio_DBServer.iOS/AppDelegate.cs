using Desafio_DBServer.Enumerates;
using Desafio_DBServer.Helpers;
using Desafio_DBServer.iOS.Config;
using Desafio_DBServer.ViewModels;
using Foundation;
using System;
using UIKit;

namespace Desafio_DBServer.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        private OrientationEnum actualOrientation = OrientationEnum.All;

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            ContainerHelper.SQLiteConfig = new SQLiteConfig();

            NavigationHelper.AccessPageEvent += NavigationHelper_AccessPageEvent;

            Rg.Plugins.Popup.Popup.Init();

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        private void NavigationHelper_AccessPageEvent(object sender, EventArgs e)
        {
            try
            {
                actualOrientation = (sender as BaseViewModel).Orientation;

                switch ((sender as BaseViewModel).Orientation)
                {
                    case OrientationEnum.Landscape:
                        UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.LandscapeLeft), new NSString("orientation"));
                        break;
                    case OrientationEnum.LandscapeInverse:
                        UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.LandscapeRight), new NSString("orientation"));
                        break;
                    case OrientationEnum.Portrait:
                        UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.Portrait), new NSString("orientation"));
                        break;
                    default:
                        UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.Unknown), new NSString("orientation"));
                        break;
                }
            }
            catch
            {
                UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.Portrait), new NSString("orientation"));
            }
        }

        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, UIWindow forWindow)
        {
            try
            {
                switch (actualOrientation)
                {
                    case OrientationEnum.Landscape:
                        return UIInterfaceOrientationMask.LandscapeLeft;
                    case OrientationEnum.Portrait:
                        return UIInterfaceOrientationMask.Portrait;
                    case OrientationEnum.LandscapeInverse:
                        return UIInterfaceOrientationMask.LandscapeRight;
                    default:
                        return UIInterfaceOrientationMask.All;
                }
            }
            catch
            {
                return UIInterfaceOrientationMask.All;
            }
        }
    }
}
