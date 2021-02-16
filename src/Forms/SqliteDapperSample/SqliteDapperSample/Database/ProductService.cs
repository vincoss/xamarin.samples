using Dapper;
using Microsoft.Data.Sqlite;
using SqliteDapperSample.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace SqliteDapperSample.Database
{
    public interface IProductService
    {
        Task Create(Product product);
        Task<IEnumerable<Product>> Get();
    }

    public class ProductService : IProductService
    {
        private readonly DatabaseConfig databaseConfig;

        public ProductService(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public async Task Create(Product product)
        {
            using (var connection = new SqliteConnection(databaseConfig.ConnectionString))
            {
                await connection.ExecuteAsync(@"INSERT INTO Product (Name, Description) VALUES (@Name, @Description);", product);
            }
        }

        public async Task<IEnumerable<Product>> Get()
        {
            using (var connection = new SqliteConnection(databaseConfig.ConnectionString))
            {
                return await connection.QueryAsync<Product>("SELECT rowid AS Id, Name, Description FROM Product;");
            }
        }
    }
}