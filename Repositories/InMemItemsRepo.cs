using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Item> GetItems()
        {
            return Items;
        }

        public Item GetItem(Guid id)
        {
            return Items.Where(item => item.Id == id).SingleOrDefault();
        }
    }
}