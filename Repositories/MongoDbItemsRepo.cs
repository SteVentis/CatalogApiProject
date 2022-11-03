using System;
using System.Collections.Generic;
using CatalogApiProject.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

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
        public void CreateItem(Item item)
        {
            itemsCollection.InsertOne(item);
        }

        public void DeleteItem(Guid id)
        {
            var filter = filteBuilder.Eq(item => item.Id,id);
            itemsCollection.DeleteOne(filter);
        }

        public Item GetItem(Guid id)
        {
            var filter = filteBuilder.Eq(item => item.Id,id);
            return itemsCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            return itemsCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateItem(Item item)
        {
            var filter = filteBuilder.Eq(existingItem => existingItem.Id,item.Id);
            itemsCollection.ReplaceOne(filter,item);
        }
    }
}