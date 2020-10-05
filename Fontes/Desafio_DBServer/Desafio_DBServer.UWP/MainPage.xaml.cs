using Desafio_DBServer.Helpers;
using Desafio_DBServer.UWP.Config;

namespace Desafio_DBServer.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            ContainerHelper.SQLiteConfig = new SQLiteConfig();

            LoadApplication(new Desafio_DBServer.App());
        }
    }
}
