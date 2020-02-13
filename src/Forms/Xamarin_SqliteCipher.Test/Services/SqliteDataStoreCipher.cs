﻿using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin_SqliteCipher.Models;

namespace Xamarin_SqliteCipher
{
    public class SqliteDataStoreCipher : IDataStore<Item>
    {
        private string _path;
        public SQLiteAsyncConnection _connection;

        public SqliteDataStoreCipher(string path = null)
        {
            _path = path;
            if (string.IsNullOrWhiteSpace(_path))
            {
                _path = GetDatabasePath();
            }

            bool isCreated = File.Exists(_path);

            CreateTables();

            if (!isCreated)
            {
                CreateDemoData();
            }
        }

        #region Setup database

        public void CreateTables()
        {
            var db = Conn;
            db.CreateTableAsync<Item>().Wait();
        }

        public string Password { get; set; } = "password123";

        public SQLiteAsyncConnection Conn
        {
            get
            {
                if (_connection == null)
                {
                    var options = new SQLiteConnectionString(_path, true, key: Password);
                    _connection = new SQLiteAsyncConnection(options);
                }
                return _connection;
            }
            set { _connection = value; }
        }

        public string GetDatabasePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "sqlitedb.db3");
        }

        private void CreateDemoData()
        {
            // project site            
            Task.Run(async () =>
            {
                var db = Conn;

                var item1 = new Item() { Id = "1", Text = "One", Description = "Number One" };
                var item2 = new Item() { Id = "2", Text = "Two", Description = "Number Two" };
                var item3 = new Item() { Id = "3", Text = "Three", Description = "Number Three" };

                await db.InsertAsync(item1);
                await db.InsertAsync(item2);
                await db.InsertAsync(item3);
            }).Wait();
        } 

        #endregion

        public async Task<bool> AddItemAsync(Item item)
        {
            var db = Conn;
            var result = await db.InsertAsync(item);
            return result > 0;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var db = Conn;
            var result = await db.DeleteAsync<Item>(id);
            return result > 0;
        }

        public async Task<Item> GetItemAsync(string id)
        {
            var db = Conn;
            return await db.FindAsync<Item>(x => x.Id == id);
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            var db = Conn;

            var results = await db.Table<Item>().ToListAsync();

            return results;
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var db = Conn;
            var result = await db.UpdateAsync(item);
            return result > 0;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(string search)
        {
            var db = Conn;

            if(string.IsNullOrWhiteSpace(search))
            {
                return await db.Table<Item>().ToArrayAsync();
            }

            var results = await db.Table<Item>().Where(x=> x.Text.StartsWith(search, StringComparison.OrdinalIgnoreCase)).ToListAsync();

            return results;
        }
    }
}
