using System;
using System.Collections.Generic;
using CatalogApiProject.Entities;

namespace CatalogApiProject.Repositories
{
    
    public interface IItemsRepository
    {
        Item GetItem(Guid id);
        IEnumerable<Item> GetItems();
    }

}