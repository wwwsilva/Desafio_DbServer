using Desafio_DBServer.Interfaces;

namespace Desafio_DBServer.UWP.Config
{
    public class SQLiteConfig : ISQLiteConfig
    {
        private string _SQLiteDirectory;

        string ISQLiteConfig._SQLiteDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(_SQLiteDirectory))
                {
                    _SQLiteDirectory = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
                }
                return _SQLiteDirectory;
            }
        }
    }
}
