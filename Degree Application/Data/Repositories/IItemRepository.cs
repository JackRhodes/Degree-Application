using Degree_Application.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Degree_Application.Data.Repositories
{
    public interface IItemRepository
    {
        /// <summary>
        /// Searches the database and returns fuzzy matches of items
        /// </summary>
        /// <param name="searchParam">The title of an item</param>
        /// <returns></returns>
        Task<List<ItemModel>> FuzzySearchTitle(string searchParam);

        Task<ItemModel> GetItemById(int? id);

    }
}
