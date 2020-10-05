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
    }
}