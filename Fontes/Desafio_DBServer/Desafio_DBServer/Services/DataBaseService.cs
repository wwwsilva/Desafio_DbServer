using Desafio_DBServer.Helpers;
using Desafio_DBServer.Interfaces;
using Desafio_DBServer.Model.Api;
using Desafio_DBServer.Model.DataBase;
using Desafio_DBServer.Services;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(DataBaseService))]
namespace Desafio_DBServer.Services
{
    public class DataBaseService : IDataBaseService
    {
        private SQLiteConnection _SQLiteConnection;
        private ISQLiteConfig config;

        public DataBaseService()
        {
            this.config = ContainerHelper.SQLiteConfig;
        }

        public void Init(string baseName)
        {
            _SQLiteConnection = new SQLiteConnection(Path.Combine(config._SQLiteDirectory, baseName + ".db3"));
            CreateTables();
        }

        public void StopDataBase()
        {
            _SQLiteConnection.Close();
        }

        public async Task<bool> DeleteFileDataBase(string baseName)
        {
            try
            {
                if (Device.RuntimePlatform == Device.iOS)
                    baseName = Path.Combine(config._SQLiteDirectory, baseName + ".db3");
                else
                    baseName += ".db3";

                string localFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var filePath = Path.Combine(localFolder, baseName);
                bool exist = File.Exists(filePath);
                if (exist)
                {
                    File.Delete(filePath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string GetFileDatabasePath(string baseName)
        {
            try
            {
                return Path.Combine(config._SQLiteDirectory, baseName + ".db3");
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        private void CreateTables()
        {
            try
            {
                #region Tabelas Clientes
                _SQLiteConnection.CreateTable<DbFileModel>();
                _SQLiteConnection.CreateTable<DbPokemon>();
                _SQLiteConnection.CreateTable<DbFavorite>();
                #endregion
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public int CreateIndex(string indexName, string tableName, string[] columnNames, bool unique = false)
        {
            try
            {
                return _SQLiteConnection.CreateIndex(indexName, tableName, columnNames, unique);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DropTable<T>()
        {
            return _SQLiteConnection.DropTable<T>();
        }

        public CreateTableResult CreateTable<T>()
        {
            return _SQLiteConnection.CreateTable<T>();
        }

        public int InsertData(Object objectToInsert)
        {
            try
            {
                return _SQLiteConnection.Insert(objectToInsert);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertAllData<T>(List<T> objectList)
        {
            try
            {
                return _SQLiteConnection.InsertAll(objectList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateData(Object objectToUpdate)
        {
            try
            {
                return _SQLiteConnection.Update(objectToUpdate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteData(Object objectToDelete)
        {
            try
            {
                return _SQLiteConnection.Delete(objectToDelete);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteData<T>(Expression<Func<T, bool>> predExpr) where T : new()
        {
            try
            {
                return _SQLiteConnection.Table<T>().Delete(predExpr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteAll<T>()
        {
            try
            {
                return _SQLiteConnection.DeleteAll<T>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T GetData_PK<T>(string primaryKey) where T : new()
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(primaryKey))
                    return _SQLiteConnection.GetWithChildren<T>(primaryKey);
                else
                    return default(T);
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Sequence contains no elements"))
                    return default(T);
                else
                    throw ex;
            }
        }

        public List<T> GetDataWithChildren<T>(List<Expression<Func<T, bool>>> lPredExpr, Expression<Func<T, Object>> keySelector) where T : new()
        {
            try
            {
                TableQuery<T> tableQuery = _SQLiteConnection.Table<T>();

                if (lPredExpr != null)
                {
                    foreach (var expr in lPredExpr)
                    {
                        tableQuery = tableQuery.Where(expr);
                    }
                }

                if (keySelector != null)
                    tableQuery = tableQuery.OrderBy(keySelector);

                var lista = tableQuery.ToList();

                foreach (T element in lista)
                {
                    _SQLiteConnection.GetChildren(element);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<T> GetDataWithChildren<T>(List<T> lista) where T : new()
        {
            try
            {
                foreach (T element in lista)
                {
                    _SQLiteConnection.GetChildren(element);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<T> GetDataQueryWithChildren<T>(string query) where T : new()
        {
            try
            {
                var lista = _SQLiteConnection.Query<T>(query);

                foreach (T element in lista)
                {
                    _SQLiteConnection.GetChildren(element);
                }

                return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<T> GetDataQuery<T>(string query, params object[] args) where T : new()
        {
            try
            {
                return _SQLiteConnection.Query<T>(query, args);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ExecuteQuery(string query)
        {
            try
            {
                return _SQLiteConnection.Execute(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<T> GetData<T>(Expression<Func<T, bool>> predExpr, Expression<Func<T, Object>> keySelector) where T : new()
        {
            try
            {
                TableQuery<T> tableQuery = _SQLiteConnection.Table<T>();

                if (predExpr != null)
                    tableQuery = tableQuery.Where(predExpr);

                if (keySelector != null)
                    tableQuery = tableQuery.OrderBy(keySelector);

                return tableQuery.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<T> GetData_ListExpr<T>(List<Expression<Func<T, bool>>> lPredExpr, Expression<Func<T, Object>> keySelector) where T : new()
        {
            try
            {
                TableQuery<T> tableQuery = _SQLiteConnection.Table<T>();

                if (lPredExpr != null)
                {
                    foreach (var expr in lPredExpr)
                    {
                        tableQuery = tableQuery.Where(expr);
                    }
                }

                if (keySelector != null)
                    tableQuery = tableQuery.OrderBy(keySelector);

                return tableQuery.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<T> GetAll<T>() where T : new()
        {
            try
            {
                return _SQLiteConnection.Table<T>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BeginTransaction()
        {
            _SQLiteConnection.BeginTransaction();
        }

        public void Commit()
        {
            _SQLiteConnection.Commit();
        }

        public void Rollback()
        {
            _SQLiteConnection.Rollback();
        }

        public bool IsInTransaction()
        {
            return _SQLiteConnection.IsInTransaction;
        }
    }
}