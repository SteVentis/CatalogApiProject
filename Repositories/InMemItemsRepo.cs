using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogApiProject.Entities;

namespace CatalogApiProject.Repositories
{
    public class InMemItemsRepo : IItemsRepository
    {
        private readonly List<Item> Items = new() //Target-typed new expression(C# 9)
        {
            new Item(){ Id = Guid.NewGuid(), Name ="Potion", Price = 9,CreatedDate = DateTimeOffset.UtcNow },
            new Item(){ Id = Guid.NewGuid(), Name ="Iron Sword", Price = 20,CreatedDate = DateTimeOffset.UtcNow },
            new Item(){ Id = Guid.NewGuid(), Name ="Bronze Shield", Price = 18,CreatedDate = DateTimeOffset.UtcNow }
        };

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(Items);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var item = Items.Where(item => item.Id == id).SingleOrDefault();
            return await Task.FromResult(item);
        }

        public async Task CreateItemAsync(Item item)
        {
            Items.Add(item);
            await Task.CompletedTask;

        }

        public async Task UpdateItemAsync(Item item)
        {
            var index = Items.FindIndex(existingItem => existingItem.Id == item.Id);
            Items[index] = item;
            await Task.CompletedTask;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var index = Items.FindIndex(existingItem => existingItem.Id == id);
            Items.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}