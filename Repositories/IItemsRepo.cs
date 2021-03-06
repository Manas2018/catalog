using System;
using System.Collections.Generic;
using catalog.Entities;

namespace catalog.Repositories
{
    public interface IItemsRepo
    {
        Item GetItem(Guid id);
        IEnumerable<Item> GetItems();
        void CreateItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(Guid id);


    }
}