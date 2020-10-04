using Desafio_DBServer.Interfaces;
using Newtonsoft.Json;
using SQLite;
using System;

namespace Desafio_DBServer.Model.DataBase
{
    [Table("Files")]
    public class DbFileModel : IDataBaseModel
    {
        public DbFileModel()
        {

        }

        public DbFileModel(string fileName, byte[] fileBytes)
        {
            this.FileName = fileName;
            this.FileBytes = fileBytes;
        }

        private String _fileName = "";
        private byte[] _fileBytes = null;

        [PrimaryKey]
        public String FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        [JsonProperty("FileBytes")]
        public byte[] FileBytes
        {
            get { return _fileBytes; }
            set { _fileBytes = value; }
        }

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
            return db.DeleteData<DbFileModel>(f => f.FileName == FileName);
        }
    }
}
