using Desafio_DBServer.Helpers;
using Desafio_DBServer.Model;
using Desafio_DBServer.Model.Api;
using Desafio_DBServer.Model.DataBase;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Desafio_DBServer.ViewModels
{
    public class PokemonViewModel :  BaseViewModel
    {
        #region Properties
        private SimplePokemonModel simplePokemon;
        public  SimplePokemonModel SimplePokemon
        {
            get { return simplePokemon; }
            set { simplePokemon = value; OnPropertyChanged(); }
        }

        private ApiPokemonModel pokemon;
        public ApiPokemonModel Pokemon
        {
            get { return pokemon; }
            set { pokemon = value; OnPropertyChanged(); }
        }

        private TypeModel type1;
        public TypeModel Type1
        {
            get { return type1; }
            set { type1 = value; OnPropertyChanged(); }
        }

        private TypeModel type2;
        public TypeModel Type2
        {
            get { return type2; }
            set { type2 = value; OnPropertyChanged(); }
        }

        private TypeModel type3;
        public TypeModel Type3
        {
            get { return type3; }
            set { type3 = value; OnPropertyChanged(); }
        }

        private ImageSource favoriteImageSource;
        public ImageSource FavoriteImageSource
        {
            get
            {
                if (favoriteImageSource == null)
                    ChangeFavoriteImage();
                return favoriteImageSource;
            }
            set { favoriteImageSource = value; OnPropertyChanged(); }
        }
        #endregion

        #region Commands
        public Command CloseCommand { get; private set; }
        public Command ChangeFavoriteCommand { get; set; }
        #endregion

        public PokemonViewModel()
        {
            CloseCommand = new Command(async () => await Close());
            ChangeFavoriteCommand = new Command(async () => await ChangeFavorite());
        }

        public override async Task Init(NavigationParameterHelper parameters)
        {
            IsBusy = true;
            if (parameters.ContainsKey("POKEMON"))
            {
                simplePokemon = parameters["POKEMON"] as SimplePokemonModel;
            }
        }

        public override async void OnAppearing()
        {
            try
            {
                if (simplePokemon != null)
                {
                    var dbPokemon = ContainerHelper.DataBaseService.GetData_PK<DbPokemon>(simplePokemon.Id);
                    if (dbPokemon == null)
                    {
                        Pokemon = await ContainerHelper.ApiService.GetAsync<ApiPokemonModel>(simplePokemon.Url);
                        if (pokemon != null)
                        {
                            dbPokemon = new DbPokemon();
                            dbPokemon.Id = simplePokemon.Id;
                            dbPokemon.Json = JsonConvert.SerializeObject(Pokemon);
                            dbPokemon.Insert(ContainerHelper.DataBaseService);
                        }
                    }
                    else
                    {
                        Pokemon = JsonConvert.DeserializeObject<ApiPokemonModel>(dbPokemon.Json);
                    }
                }
            }
            catch { }

            if (Pokemon == null)
            {
                await ContainerHelper.MessageService.ShowMessage("", "Não foi possível buscar os dados do pokemon.", "Ok");
                await Close();
            }
            else
            {
                GetPokemonTypes();
            }

            IsBusy = false;
        }

        private async Task GetFavorite()
        {
            if (ContainerHelper.DataBaseService.GetData_PK<DbFavorite>(SimplePokemon.Id) != null)
            {
                SimplePokemon.Favorite = true;
                OnPropertyChanged("Favorite");
            }
        }

        private async Task Close()
        {
            await ContainerHelper.NavigationService.Back(true);
        }

        private void GetPokemonTypes()
        {
            Type1 = new TypeModel(false);
            Type2 = new TypeModel(false);
            Type3 = new TypeModel(false);

            if (Pokemon != null)
            {
                if (Pokemon.Types.Count > 0)
                    Type1 =  ConvertTypes(Pokemon.Types[0].type);

                if (Pokemon.Types.Count > 1)
                    Type2 = ConvertTypes(Pokemon.Types[1].type);

                if (Pokemon.Types.Count > 2)
                    Type3 = ConvertTypes(Pokemon.Types[2].type);
            }
        }

        private TypeModel ConvertTypes(PokemonType2 type)
        {
            TypeModel result = null;
            switch (type.name)
            {
                case "normal":
                    result = new TypeModel(Color.FromHex("#a4acaf"), Color.Transparent, Color.Black, "Normal");
                    break;
                case "fighting":
                    result = new TypeModel(Color.FromHex("#d56723"), Color.Transparent, Color.White, "Lutador");
                    break;
                case "flying":
                    result = new TypeModel(Color.FromHex("#3dc7ef"), Color.FromHex("#bdb9b8"), Color.Black, "Voador");
                    break;
                case "poison":
                    result = new TypeModel(Color.FromHex("#b97fc9"), Color.Transparent, Color.White, "Venenoso");
                    break;
                case "ground":
                    result = new TypeModel(Color.FromHex("#f7de3f"), Color.FromHex("#ab9842"), Color.Black, "Terra");
                    break;
                case "rock":
                    result = new TypeModel(Color.FromHex("#a38c21"), Color.Transparent, Color.White, "Pedra");
                    break;
                case "bug":
                    result = new TypeModel(Color.FromHex("#729f3f"), Color.Transparent, Color.White, "Inseto");
                    break;
                case "ghost":
                    result = new TypeModel(Color.FromHex("#7b62a3"), Color.Transparent, Color.White, "Fantasma");
                    break;
                case "steel":
                    result = new TypeModel(Color.FromHex("#9eb7b8"), Color.Transparent, Color.Black, "Metálico");
                    break;
                case "fire":
                    result = new TypeModel(Color.FromHex("#fd7d24"), Color.Transparent, Color.White, "Fogo");
                    break;
                case "water":
                    result = new TypeModel(Color.FromHex("#4592c4"), Color.Transparent, Color.White, "Água");
                    break;
                case "grass":
                    result = new TypeModel(Color.FromHex("#9bcc50"), Color.Transparent, Color.Black, "Planta");
                    break;
                case "electric":
                    result = new TypeModel(Color.FromHex("#eed535"), Color.Transparent, Color.Black, "Elétrico");  
                    break;
                case "psychic":
                    result = new TypeModel(Color.FromHex("#f366b9"), Color.Transparent, Color.White, "Psíquico");
                    break;
                case "ice":
                    result = new TypeModel(Color.FromHex("#51c4e7"), Color.Transparent, Color.Black, "Gelo");
                    break;
                case "dragon":
                    result = new TypeModel(Color.FromHex("#53a4cf"), Color.FromHex("#f16e57"), Color.White, "Dragão");
                    break;
                case "dark":
                    result = new TypeModel(Color.FromHex("#707070"), Color.Transparent, Color.White, "Noturno");
                    break;
                case "fairy":
                    result = new TypeModel(Color.FromHex("#fdb9e9"), Color.Transparent, Color.Black, "Fada");
                    break;
                case "unknown":
                    result = new TypeModel(Color.FromHex("#d9d9d9"), Color.Transparent, Color.Black, "Desconhecido");
                    break;
                case "shadow":
                    result = new TypeModel(Color.FromHex("#b5b3b3"), Color.Transparent, Color.Black, "Sombra");
                    break;
            }
            return result;
        }

        private void ChangeFavoriteImage()
        {
            if (SimplePokemon.Favorite)
                FavoriteImageSource = ImageHelper.GetImageResource("StarOn.png");
            else
                FavoriteImageSource = ImageHelper.GetImageResource("StarOff.png");
        }

        private async Task ChangeFavorite()
        {
            try
            {
                SimplePokemon.Favorite = !SimplePokemon.Favorite;

                ChangeFavoriteImage();

                if (SimplePokemon.Favorite)
                {
                    var favorite = new DbFavorite(SimplePokemon.Id);
                    var dbFav = ContainerHelper.DataBaseService.GetData_PK<DbFavorite>(SimplePokemon.Id);
                    if (dbFav == null)
                        favorite.Insert(ContainerHelper.DataBaseService);

                    if (ContainerHelper.Favorites.Any(f => f.Id == SimplePokemon.Id) == false)
                        ContainerHelper.Favorites.Add(favorite);
                }
                else
                {
                    ContainerHelper.DataBaseService.DeleteData<DbFavorite>(f => f.Id == SimplePokemon.Id);
                    var fav = ContainerHelper.Favorites.Where(f => f.Id == SimplePokemon.Id).FirstOrDefault();
                    if (fav != null)
                        ContainerHelper.Favorites.Remove(fav);
                }
            }
            catch { }
        }
    }
}
