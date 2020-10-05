using Desafio_DBServer.Interfaces;
using Desafio_DBServer.Model.DataBase;
using System.Collections.Generic;

namespace Desafio_DBServer.Helpers
{
    public static class ContainerHelper
    {
        public static string UrlBase = "https://pokeapi.co/api/v2/";
        public static string UrlBaseImage = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/";

        public static List<DbFavorite> Favorites = null;

        public static IApiService ApiService;
        public static IMessageService MessageService;
        public static INavigationService NavigationService;
        public static IDataBaseService DataBaseService;

        public static ISQLiteConfig SQLiteConfig;
    }
}
