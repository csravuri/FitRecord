using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FitRecord.Common;
using FitRecord.Models;
using SQLite;

namespace FitRecord.Database
{
    public class DbConnection
    {
        private SQLiteAsyncConnection _connection;
        private static DbConnection _dbConnection;
        private string _dbPath;
        private DbConnection()
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Database");

            Utils.MakeSureDirectoryExists(folderPath);

            _dbPath = Path.Combine(folderPath, "FitRecordDB.db3");

            _connection = new SQLiteAsyncConnection(_dbPath);

            CreateTable();

        }

        public static DbConnection GetDbConnection()
        {
            if (_dbConnection == null)
            {
                _dbConnection = new DbConnection();
            }

            return _dbConnection;
        }

        private async void CreateTable()
        {
            await _connection.CreateTableAsync<PersonModel>();
            await _connection.CreateTableAsync<ParameterModel>();
            //await _connection.CreateTableAsync<OrderLine>();
            //await _connection.CreateTableAsync<Sequence>();
        }

        public async Task InsertRecord<E>(E item) where E : BaseModel
        {
            item.CreatedDate = DateTime.Now;
            await _connection.InsertAsync(item);
        }

        public async Task UpdateRecord<E>(E item) where E : BaseModel
        {
            item.ModifiedDate = DateTime.Now;
            await _connection.UpdateAsync(item);
        }

        public async Task DeleteRecord<E>(E item) where E : BaseModel
        {
            await _connection.DeleteAsync(item);
        }
        public async Task InsertRecord<E>(List<E> items) where E : BaseModel
        {
            items.ForEach(x => x.CreatedDate = DateTime.Now);
            await _connection.InsertAllAsync(items);
        }

        public async Task UpdateRecord<E>(List<E> items) where E : BaseModel
        {
            items.ForEach(x => x.ModifiedDate = DateTime.Now);
            await _connection.UpdateAllAsync(items);
        }

        public async Task DeleteRecord<E>(List<E> items) where E : BaseModel
        {
            foreach (E item in items)
            {
                await DeleteRecord<E>(item);
            }
        }


        public async Task<List<PersonModel>> GetPersons()
        {
            return await _connection.Table<PersonModel>().ToListAsync();
        }
        public async Task<List<ParameterModel>> GetParameters()
        {
            return await _connection.Table<ParameterModel>().ToListAsync();
        }

        //public async Task<List<OrderLine>> GetOrderLines()
        //{
        //    return await _connection.Table<OrderLine>().ToListAsync();
        //}

        //public async Task<List<Sequence>> GetSequences()
        //{
        //    return await _connection.Table<Sequence>().ToListAsync();
        //}

        //public string GetDBPath()
        //{
        //    return _dbPath;
        //}
    }
}
