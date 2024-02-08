using Catalog.Application.Interface;
using Catalog.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Persistance
{
    public class DataBaseContext : IDataBaseContext
    {
        private readonly IMongoDatabase _database;
        private readonly string _collectionName;

        public DataBaseContext(DatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
            _collectionName = settings.CollectionName;
            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products => _database.GetCollection<Product>(_collectionName);
    }
}
