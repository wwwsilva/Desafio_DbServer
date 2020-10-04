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

        int DropTable<T>();

        CreateTableResult CreateTable<T>();

        int CreateIndex(string indexName, string tableName, string[] columnNames, bool unique = false);

        int InsertData(Object objectToInsert);

        int InsertAllData<T>(List<T> objectList);

        int UpdateData(Object objectToUpdate);

        int DeleteData(Object objectToDelete);

        int DeleteData<T>(Expression<Func<T, bool>> predExpr) where T : new();

        int DeleteAll<T>();

        int ExecuteQuery(string query);

        T GetData_PK<T>(string primaryKey) where T : new();

        List<T> GetDataQuery<T>(string query, params object[] args) where T : new();

        List<T> GetDataQueryWithChildren<T>(string query) where T : new();

        List<T> GetDataWithChildren<T>(List<Expression<Func<T, bool>>> lPredExpr, Expression<Func<T, Object>> keySelector) where T : new();

        List<T> GetDataWithChildren<T>(List<T> lista) where T : new();

        List<T> GetData<T>(Expression<Func<T, bool>> predExpr, Expression<Func<T, Object>> keySelector) where T : new();

        List<T> GetData_ListExpr<T>(List<Expression<Func<T, bool>>> lPredExpr, Expression<Func<T, Object>> keySelector) where T : new();

        List<T> GetAll<T>() where T : new();

        void BeginTransaction();

        void Commit();

        void Rollback();

        void StopDataBase();

        Task<bool> DeleteFileDataBase(string baseName);

        bool IsInTransaction();

        string GetFileDatabasePath(string baseName);
    }
}
