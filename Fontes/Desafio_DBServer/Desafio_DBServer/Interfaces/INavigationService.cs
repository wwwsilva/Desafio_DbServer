using Desafio_DBServer.Enumerates;
using Desafio_DBServer.Helpers;
using Desafio_DBServer.Model.System;
using Desafio_DBServer.ViewModels;
using System.Threading.Tasks;

namespace Desafio_DBServer.Interfaces
{
    public interface INavigationService
    {
        Task<ResponseNavigationServiceModel> NavigateTo<TViewModel>(string title,
                                                                    NavigationParameterHelper parameters = null,
                                                                    ViewTypeEnum viewType = ViewTypeEnum.Page) where TViewModel : BaseViewModel;

        Task<ResponseNavigationServiceModel> StartNavigate<TViewModel>(string title,
                                                                       bool navigationPage = true,
                                                                       NavigationParameterHelper parameters = null) where TViewModel : BaseViewModel;

        Task Back(bool IsPopup = false);
    }
}
