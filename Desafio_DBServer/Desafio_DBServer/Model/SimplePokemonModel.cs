using Desafio_DBServer.Helpers;
using Desafio_DBServer.Model.DataBase;
using Desafio_DBServer.Model.System;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Desafio_DBServer.Model
{
    public class SimplePokemonModel : BaseObjectModel
    {
        private string id;
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged();
                GetFavorite();
            }
        }

        private string name;
        public string Name
        {
            get { return name.FirstCharToUpper(); }
            set { name = value; }
        }

        private string url;
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        private bool favorite;
        public bool Favorite
        {
            get { return favorite; }
            set { favorite = value; OnPropertyChanged(); }
        }

        private bool loadedImage = false;
        public bool LoadedImage
        {
            get { return loadedImage; }
            set { loadedImage = value; OnPropertyChanged(); }
        }

        private bool loadingImage = false;
        private ImageSource pokemonImage;
        public ImageSource PokemonImage
        {
            get
            {
                LoadImage();
                return pokemonImage;
            }
            set { pokemonImage = value; }
        }

        private async Task LoadImage()
        {
            if (pokemonImage == null && !loadingImage)
            {
                try
                {
                    loadingImage = true;

                    Id = Url.Replace(ContainerHelper.UrlBase + "pokemon/", "").Split('/')[0];

                    string imageUrl = ContainerHelper.UrlBaseImage + Id + ".png";

                    var dbFileModel = ContainerHelper.DataBaseService.GetData_PK<DbFileModel>(imageUrl);
                    if (dbFileModel != null)
                    {
                        PokemonImage = ImageSource.FromStream(() => new MemoryStream(dbFileModel.FileBytes));
                    }
                    else
                    {
                        var bytes = await ContainerHelper.ApiService.GetImageBytesAsync(imageUrl);
                        if (bytes != null)
                        {
                            dbFileModel = new DbFileModel(imageUrl, bytes);
                            dbFileModel.Insert(ContainerHelper.DataBaseService);

                            PokemonImage = ImageSource.FromStream(() => new MemoryStream(bytes));
                        }
                        else
                        {
                            pokemonImage = ImageHelper.ImgError;
                        }
                    }
                }
                catch
                {
                    pokemonImage = ImageHelper.ImgError;
                }

                LoadedImage = true;
                OnPropertyChanged("PokemonImage");
                loadingImage = false;
            }
        }

        private async Task GetFavorite()
        {
            if (ContainerHelper.Favorites != null)
            {
                Favorite = ContainerHelper.Favorites.Any(f => f.Id == Id);
            }
        }
    }
}
