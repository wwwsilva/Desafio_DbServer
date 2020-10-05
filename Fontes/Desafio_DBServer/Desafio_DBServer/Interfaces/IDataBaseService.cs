using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Desafio_DBServer.Interfaces
{
    public interface IDataBaseService
    {
        void Init(string baseName);

        int InsertData(Object objectToInsert);

        int UpdateData(Object objectToUpdate);

        int DeleteData<T>(Expression<Func<T, bool>> predExpr) where T : new();

        T GetData_PK<T>(string primaryKey) where T : new();

        List<T> GetAll<T>() where T : new();
    }
}
