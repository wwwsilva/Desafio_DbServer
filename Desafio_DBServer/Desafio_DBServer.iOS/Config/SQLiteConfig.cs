using Desafio_DBServer.Interfaces;

namespace Desafio_DBServer.iOS.Config
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
                    _SQLiteDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                }
                return _SQLiteDirectory;
            }
        }
    }
}