using Desafio_DBServer.Enumerates;
using Desafio_DBServer.Helpers;
using Desafio_DBServer.Model;
using Desafio_DBServer.Model.Api;
using Desafio_DBServer.Model.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace Desafio_DBServer.ViewModels
{
    public class ListPokemonViewModel : BaseViewModel
    {
        #region Properties
        private BaseListModel<SimplePokemonModel> baseList = null;
        private bool getByType = false;

        private bool loading = false;
        public bool Loading
        {
            get { return loading; }
            set { loading = value; OnPropertyChanged(); }
        }

        private InfiniteScrollCollection<SimplePokemonModel> pokemons;
        public InfiniteScrollCollection<SimplePokemonModel> Pokemons
        {
            get { return pokemons; }
            set { pokemons = value; OnPropertyChanged(); }
        }

        private bool showFilter;
        public bool ShowFilter
        {
            get { return showFilter; }
            set { showFilter = value; OnPropertyChanged(); }
        }

        private string selectedType;
        public string SelectedType
        {
            get { return selectedType; }
            set { selectedType = value; OnPropertyChanged(); }
        }
        #endregion

        #region Commands
        public Command OpenPokemonCommand { get; private set; }
        public Command ShowHideFilterCommand { get; private set; }
        public Command SearchCommand { get; private set; }
        public Command RefreshListCommand { get; private set; }
        #endregion

        public ListPokemonViewModel()
        {
            OpenPokemonCommand = new Command(async (pokemon) => OpenPokemonData((SimplePokemonModel)pokemon));
            ShowHideFilterCommand = new Command(() => { ShowFilter = !ShowFilter; });
            SearchCommand = new Command(async () => GetPokemonsByType());
            RefreshListCommand = new Command(async () => GetPokemonsByType());
        }

        public override async void OnAppearing()
        {
            GetFavoriteList();
            await GetPokemons();
        }

        private async Task GetPokemons()
        {
            baseList = null;
            Pokemons = new InfiniteScrollCollection<SimplePokemonModel>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;
                    try
                    {
                        if (baseList == null)
                        {
                            baseList = await ContainerHelper.ApiService.GetAsync<BaseListModel<SimplePokemonModel>>(ContainerHelper.UrlBase + "pokemon/?limit=40");
                        }
                        else if (!baseList.Next.IsNull())
                        {
                            string param = baseList.Next.Split('?')[1];
                            baseList = await ContainerHelper.ApiService.GetAsync<BaseListModel<SimplePokemonModel>>(ContainerHelper.UrlBase + "pokemon/?" + param);
                        }
                        else
                        {
                            baseList.Results = null;
                        }
                    }
                    catch
                    {
                        baseList.Results = null;
                    }

                    IsBusy = false;
                    Loading = false;
                    return baseList.Results;
                }
            };
            await Pokemons.LoadMoreAsync();
            ShowFilter = false;
        }

        private async Task GetPokemonsByType()
        {
            if (SelectedType.IsNull() || SelectedType.Equals("Buscar Todos"))
            {
                await GetPokemons();
                return;
            }

            getByType = false;
            Pokemons = new InfiniteScrollCollection<SimplePokemonModel>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;
                    List<SimplePokemonModel> result = null;
                    try
                    {
                        if (getByType == false)
                        {
                            string searchType = ConvertTypeLanguage();
                            var apiTypeModel = await ContainerHelper.ApiService.GetAsync<ApiTypeModel>(ContainerHelper.UrlBase + "type/" + searchType);
                            result = apiTypeModel.pokemon.Select(p => p.pokemon).ToList();
                        }
                    }
                    catch { }

                    getByType = true;
                    IsBusy = false;
                    Loading = false;
                    return result;
                }
            };
            await Pokemons.LoadMoreAsync();
            ShowFilter = false;
        }

        private string ConvertTypeLanguage()
        {
            switch (SelectedType)
            {
                case "Normal":
                    return "normal";
                case "Lutador":
                    return "fighting";
                case "Voador":
                    return "flying";
                case "Venenoso":
                    return "poison";
                case "Terra":
                    return "ground";
                case "Pedra":
                    return "rock";
                case "Inseto":
                    return "bug";
                case "Fantasma":
                    return "ghost";
                case "Metálico":
                    return "steel";
                case "Fogo":
                    return "fire";
                case "Água":
                    return "water";
                case "Planta":
                    return "grass";
                case "Elétrico":
                    return "electric";
                case "Psíquico":
                    return "psychic";
                case "Gelo":
                    return "ice";
                case "Dragão":
                    return "dragon";
                case "Noturno":
                    return "dark";
                case "Fada":
                    return "fairy";
                case "Desconhecido":
                    return "unknown";
                case "Sombra":
                    return "shadow";
            }
            return "";
        }

        private async Task OpenPokemonData(SimplePokemonModel pokemon)
        {
            if (pokemon == null)
                return;

            bool modalOpened = false;

            try
            {
                NavigationParameterHelper parameters = new NavigationParameterHelper();
                parameters.Add("POKEMON", pokemon);

                var response = await ContainerHelper.NavigationService.NavigateTo<PokemonViewModel>(pokemon.Name, parameters, ViewTypeEnum.Popup);
                response.viewModel.OnDisappearingEvent += ViewModel_OnDisappearingEvent;
            }
            catch (Exception)
            {
                if (modalOpened)
                    await ContainerHelper.NavigationService.Back(true);
            }
        }

        private void ViewModel_OnDisappearingEvent(object sender, EventArgs e)
        {
            (sender as BaseViewModel).OnDisappearingEvent -= ViewModel_OnDisappearingEvent;
        }

        private async Task GetFavoriteList()
        {
            ContainerHelper.Favorites =  ContainerHelper.DataBaseService.GetAll<DbFavorite>();
        }
    }
}
