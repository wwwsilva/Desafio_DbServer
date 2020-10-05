namespace Desafio_DBServer.Interfaces
{
    public interface IDataBaseModel
    {
        int Insert(IDataBaseService db);

        int Update(IDataBaseService db);

        int Delete(IDataBaseService db);
    }
}
