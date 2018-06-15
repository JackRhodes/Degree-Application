using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Degree_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Degree_Application.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private Degree_ApplicationContext _context;
        private readonly UserManager<AccountModel> _userManager;
        public ItemRepository(Degree_ApplicationContext context, UserManager<AccountModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        public async Task<List<ItemModel>> FuzzySearchTitleAsync(string searchParam)
        {
            // Get everything in from the items table
            var items = from x in _context.Items.AsNoTracking().Include(_ => _.Account)
                        select x;
            //If the Id being sent to the Id is not null, then filter.
            if (!String.IsNullOrEmpty(searchParam))
            {
                items = items.Where(x => x.Title.Contains(searchParam));
                
            }

            return await items.ToListAsync();
        }

        public async Task<ItemModel> GetItemByIdAsync(int? id)
        {
           
            var itemModel = await _context.Items
                .SingleOrDefaultAsync(m => m.Id == id);
          
            return itemModel;
        }

        public async Task<int> CreateItemAsync(ItemModel itemModel, HttpContext httpContext)
        {
            //Potential security risk via Http spoofing?
            AccountModel account = _context.Users.FirstOrDefault(x => x.Id == _userManager.GetUserId(httpContext.User));
            
            itemModel.Account = account;

            _context.Add(itemModel); 

            return await _context.SaveChangesAsync();   
        }

        public async Task<ItemModel> GetSingleItemByIdAsync(int? id)
        {
            ItemModel item = new ItemModel();
            return item = await _context.Items.SingleOrDefaultAsync(m => m.Id == id);   
        }

        public async Task<int> UpdateItemAsync(ItemModel itemModel)
        {
            _context.Update(itemModel);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteItemAsync(ItemModel itemModel)
        {
            _context.Items.Remove(itemModel);

            return await _context.SaveChangesAsync();
        }

        public bool CheckIfItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }

        public Task<List<ItemModel>> GetAllItemFromUser(HttpContext httpContext)
        {
           
            AccountModel account = _context.Users.FirstOrDefault(x => x.Id == _userManager.GetUserId(httpContext.User));

            var items = from x in _context.Items.AsNoTracking().Include(_ => _.Account)
                        select x;

            return items.Where(x => x.Account.Id == account.Id).ToListAsync();

        }
    }
}
