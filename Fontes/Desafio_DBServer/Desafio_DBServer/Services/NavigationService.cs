using Desafio_DBServer.Enumerates;
using Desafio_DBServer.Helpers;
using Desafio_DBServer.Interfaces;
using Desafio_DBServer.Model.System;
using Desafio_DBServer.Services;
using Desafio_DBServer.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(NavigationService))]
namespace Desafio_DBServer.Services
{
    public class NavigationService : INavigationService
    {
        private readonly MasterBehavior masterBehavior = MasterBehavior.Popover;

        public NavigationService()
        {
            if (Device.RuntimePlatform == Device.UWP)
                masterBehavior = MasterBehavior.Split;
        }

        public async Task<ResponseNavigationServiceModel> NavigateTo<TViewModel>(string title,
                                                                                 NavigationParameterHelper parameters = null,
                                                                                 ViewTypeEnum viewType = ViewTypeEnum.Page) where TViewModel : BaseViewModel
        {
            try
            {
                ResponseNavigationServiceModel retorno = await ResolveView<TViewModel>(title, parameters, viewType);
                if (retorno == null)
                    return null;

                switch (viewType)
                {
                    case ViewTypeEnum.Page:
                        NavigationHelper.CallAccessPage(retorno.viewModel);
                        if (Device.RuntimePlatform == Device.UWP)
                            await GetNavigationPage().PushAsync(retorno.view as Page, false);
                        else
                            await GetNavigationPage().PushAsync(retorno.view as Page, false);
                        break;

                    case ViewTypeEnum.Popup:
                        await GetNavigationPage().PushPopupAsync(retorno.view as PopupPage, false);
                        break;
                }

                return retorno;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ResponseNavigationServiceModel> StartNavigate<TViewModel>(string title, 
                                                                                    bool navigationPage = true, 
                                                                                    NavigationParameterHelper parameters = null) where TViewModel : BaseViewModel
        {
            try
            {
                ResponseNavigationServiceModel retorno = await ResolveView<TViewModel>(title, parameters);
                if (retorno == null)
                    return null;

                if (navigationPage)
                {
                    Application.Current.MainPage = new NavigationPage(retorno.view as Page);
                }
                else
                {
                    Application.Current.MainPage = retorno.view as Page;
                }

                NavigationHelper.CallAccessPage(retorno.viewModel);

                return retorno;
            }
            catch
            {
                return null;
            }
        }

        public async Task StartNavigate<TMasterViewModel, TViewModel>(string titleMaster, 
                                                                      string titleDetail) where TMasterViewModel : BaseViewModel 
                                                                                          where TViewModel : BaseViewModel
        {
            try
            {
                ResponseNavigationServiceModel retornoMasterView = await ResolveView<TMasterViewModel>(titleMaster);
                if (retornoMasterView == null)
                    return;

                ResponseNavigationServiceModel retornoDetailView = await ResolveView<TViewModel>(titleDetail);
                if (retornoDetailView == null)
                    return;

                Application.Current.MainPage = new MasterDetailPage()
                {
                    Master = retornoMasterView.view as Page,
                    Detail = new NavigationPage(retornoDetailView.view as Page),
                    MasterBehavior = masterBehavior
                };

                //Desativa o comportamento chato de arrastar para a direita e o menu lateral ser exibido.
                if (Device.RuntimePlatform == Device.iOS)
                    (Application.Current.MainPage as MasterDetailPage).IsGestureEnabled = false;

                NavigationHelper.CallAccessPage(retornoDetailView.viewModel);
            }
            catch { }
        }

        public async Task SetNavigateDetailPage<TViewModel>(object masterPage, 
                                                            string titleDetail, 
                                                            NavigationParameterHelper parameters = null) where TViewModel : BaseViewModel
        {
            try
            {
                ResponseNavigationServiceModel retornoDetailView = await ResolveView<TViewModel>(titleDetail, parameters);
                if (retornoDetailView == null)
                    return;

                if (Device.RuntimePlatform == Device.iOS)
                    (masterPage as MasterDetailPage).IsGestureEnabled = false;

                NavigationHelper.CallAccessPage(retornoDetailView.viewModel);

                try
                {
                    ((MasterDetailPage)masterPage).Detail.BindingContext = null;
                }
                catch { }

                ((MasterDetailPage)masterPage).Detail = new NavigationPage(retornoDetailView.view as Page);
            }
            catch { }
        }

        public async Task Back(bool IsPopup = false)
        {
            try
            {
                INavigation navigationPage = GetNavigationPage();

                if (IsPopup)
                {
                    if (Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Count > 0)
                    {
                        await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync(false);
                    }
                }
                else
                {
                    await navigationPage.PopAsync();
                }
            }
            catch { }
        }

        #region Funções Privadas
        private async Task<ResponseNavigationServiceModel> ResolveView<TViewModel>(string title,
                                                                                   NavigationParameterHelper parameters = null,
                                                                                   ViewTypeEnum viewType = ViewTypeEnum.Page) where TViewModel : BaseViewModel
        {
            try
            {
                ResponseNavigationServiceModel defaultResponse = new ResponseNavigationServiceModel();

                var tipoViewModel = typeof(TViewModel);

                var tipoView = Type.GetType(tipoViewModel.FullName.Substring(0, tipoViewModel.FullName.Length - 5).Replace(".ViewModels.", ".Views."));

                BaseViewModel viewModel = Activator.CreateInstance(tipoViewModel) as BaseViewModel;

                if (viewModel == null)
                    return null;

                defaultResponse.viewModel = viewModel;

                if (String.IsNullOrEmpty(title))
                    viewModel.Title = tipoView.Name;
                else
                    viewModel.Title = title;

                if (parameters == null)
                    await viewModel.Init();
                else
                    await viewModel.Init(parameters);

                switch (viewType)
                {
                    case ViewTypeEnum.Page:
                        #region PAGE
                        var viewPage = Activator.CreateInstance(tipoView) as Page;
                        if (viewPage == null)
                            return null;

                        viewPage.Appearing += delegate
                        {
                            viewModel.OnAppearing();
                            viewModel.CallOnAppearing();
                        };

                        try
                        {
                            viewPage.Disappearing += delegate
                            {
                                viewModel.OnDisappearing();
                                viewModel.CallOnDisappearing();
                            };
                        }
                        catch { }

                        try
                        {
                            viewPage.LayoutChanged += delegate
                            {
                                viewModel.OnLayoutChanged();
                                viewModel.CallOnLayoutChanged();
                            };
                        }
                        catch { }

                        viewPage.BindingContext = viewModel;
                        defaultResponse.view = viewPage;
                        break;
                    #endregion PAGE

                    case ViewTypeEnum.Popup:
                        #region POPUP
                        var viewPopup = Activator.CreateInstance(tipoView) as PopupPage;
                        if (viewPopup == null)
                            return null;

                        viewPopup.Appearing += delegate
                        {
                            viewModel.OnAppearing();
                            viewModel.CallOnAppearing();
                        };

                        try
                        {
                            viewPopup.Disappearing += delegate
                            {
                                viewModel.OnDisappearing();
                                viewModel.CallOnDisappearing();
                            };
                        }
                        catch { }

                        viewPopup.BindingContext = viewModel;

                        defaultResponse.view = viewPopup;
                        break;
                    #endregion POPUP

                    case ViewTypeEnum.MasterDetailPage:
                        var viewMasterDetailPage = Activator.CreateInstance(tipoView) as MasterDetailPage;
                        if (viewMasterDetailPage == null)
                            return null;

                        viewMasterDetailPage.Appearing += delegate
                        {
                            viewModel.OnAppearing();
                            viewModel.CallOnAppearing();
                        };
                        try
                        {
                            viewMasterDetailPage.Disappearing += delegate
                            {
                                viewModel.OnDisappearing();
                                viewModel.CallOnDisappearing();
                            };
                        }
                        catch { }

                        viewMasterDetailPage.BindingContext = viewModel;
                        defaultResponse.view = viewMasterDetailPage;
                        break;
                }

                return defaultResponse;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static INavigation GetNavigationPage()
        {
            if (Application.Current.MainPage is MasterDetailPage)
            {
                return (Application.Current.MainPage as MasterDetailPage).Detail.Navigation;
            }
            else
            {
                return Application.Current.MainPage.Navigation;
            }
        }

        #endregion
    }
}