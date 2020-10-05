using Desafio_DBServer.Interfaces;
using SQLite;

namespace Desafio_DBServer.Model.DataBase
{
    [Table("Favorites")]
    public class DbFavorite : IDataBaseModel
    {
        public DbFavorite()
        {

        }

        public DbFavorite(string id)
        {
            Id = id;
        }

        public string Id { get; set; }

        public int Insert(IDataBaseService db)
        {
            return db.InsertData(this);
        }

        public int Update(IDataBaseService db)
        {
            return db.UpdateData(this);
        }

        public int Delete(IDataBaseService db)
        {
            return db.DeleteData<DbFavorite>(f => f.Id == Id);
        }
    }
}
