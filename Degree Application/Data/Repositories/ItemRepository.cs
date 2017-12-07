using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Degree_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Degree_Application.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private Degree_ApplicationContext _context;
        public ItemRepository(Degree_ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<ItemModel>> FuzzySearchTitle(string searchParam)
        {
            // Get everything in from the items table
            var items = from x in _context.Items
                        select x;
            //If the Id being sent to the Id is not null, then filter.
            if (!String.IsNullOrEmpty(searchParam))
            {
                items = items.Where(x => x.Title.Contains(searchParam));
            }

            return await items.ToListAsync();
        }

        public async Task<ItemModel> GetItemById(int? id)
        {
           
            var itemModel = await _context.Items
                .SingleOrDefaultAsync(m => m.Id == id);
          
            return itemModel;

        }
    }
}
