using Desafio_DBServer.Helpers;
using System.Threading.Tasks;

namespace Desafio_DBServer.ViewModels
{
    public class InitViewModel : BaseViewModel
    {
        public override async void OnAppearing()
        {
            await Task.Delay(6000);
            await ContainerHelper.NavigationService.NavigateTo<ListPokemonViewModel>("Pokédex", null, Enumerates.ViewTypeEnum.Page);
        }
    }
}
