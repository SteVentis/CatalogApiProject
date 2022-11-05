using System;
using System.Collections.Generic;
using CatalogApiProject.Entities;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace CatalogApiProject.Repositories
{
    public class MongoDbItemsRepo : IItemsRepository
    {
        private const string databaseName = "catalog";
        private const string collectionName = "items";
        private readonly IMongoCollection<Item> itemsCollection;
        private readonly FilterDefinitionBuilder<Item> filteBuilder = Builders<Item>.Filter;

        public MongoDbItemsRepo(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            itemsCollection = database.GetCollection<Item>(collectionName);
        }
        public async Task CreateItemAsync(Item item)
        {
           await itemsCollection.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var filter = filteBuilder.Eq(item => item.Id,id);
            await itemsCollection.DeleteOneAsync(filter);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var filter = filteBuilder.Eq(item => item.Id,id);
            return await itemsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            var filter = filteBuilder.Eq(existingItem => existingItem.Id,item.Id);
            await itemsCollection.ReplaceOneAsync(filter,item);
        }
    }
}