using Desafio_DBServer.Helpers;
using Desafio_DBServer.Interfaces;
using Desafio_DBServer.ViewModels;
using Xamarin.Forms;

namespace Desafio_DBServer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ContainerHelper.NavigationService = DependencyService.Get<INavigationService>();
            ContainerHelper.ApiService = DependencyService.Get<IApiService>();
            ContainerHelper.MessageService = DependencyService.Get<IMessageService>();
            ContainerHelper.DataBaseService = DependencyService.Get<IDataBaseService>();

            ContainerHelper.DataBaseService.Init("pokedex");

            ContainerHelper.NavigationService.StartNavigate<InitViewModel>("Pokedex", true);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
