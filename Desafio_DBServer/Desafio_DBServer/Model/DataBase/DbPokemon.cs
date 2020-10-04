using Desafio_DBServer.Interfaces;
using SQLite;

namespace Desafio_DBServer.Model.DataBase
{
    [Table("Pokemons")]
    public class DbPokemon : IDataBaseModel
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Json { get; set; }

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
            return db.DeleteData<DbPokemon>(p => p.Id == this.Id);
        }
    }
}
