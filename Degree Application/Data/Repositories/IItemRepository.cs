using Degree_Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Degree_Application.Data.Repositories
{
    /// <summary>
    /// This Interface provides access to the items used by the application. This has been isolated from the default DBContext as in future the application may want to access items from an alternative source, therefore we can use this interface to return the same results.
    /// </summary>
    public interface IItemRepository
    {
        /// <summary>
        /// Searches the datasource by the searchParam.
        /// </summary>
        /// <param name="searchParam">The title of an item<./param>
        /// <returns>Fuzzy matches of all Items containing searchParam.</returns>
        Task<List<ItemModel>> FuzzySearchTitleAsync(string searchParam);
        /// <summary>
        /// Searches the datasource by the Id.
        /// </summary>
        /// <param name="id">Id of Item.</param>
        /// <returns>Item matching the Id.</returns>
        Task<ItemModel> GetItemByIdAsync(int? id);
        /// <summary>
        /// Searches the datasource by the Id.
        /// </summary>
        /// <param name="id">Item matching the Id.</param>
        /// <returns>Single Item matching the Id.</returns>
        Task<ItemModel> GetSingleItemByIdAsync(int? id);
        /// <summary>
        /// Creates a new item in the datasource.
        /// </summary>
        /// <param name="itemModel">The item to be added.</param>
        /// <param name="httpcontext">The request sent with the creation.</param>
        /// <returns>Success status/</returns>
        Task<int> CreateItemAsync(ItemModel itemModel, HttpContext httpContext);
        /// <summary>
        /// Updates an existing record in the item datasource.
        /// </summary>
        /// <param name="itemModel">The new item structure.</param>
        /// <returns>Success status.</returns>
        Task<int> UpdateItemAsync(ItemModel itemModel);
        /// <summary>
        /// Deletes an existing item from the datasource.
        /// </summary>
        /// <param name="itemModel">The item to delete.</param>
        /// <returns>Success status.</returns>
        Task<int> DeleteItemAsync(ItemModel itemModel);
        /// <summary>
        /// Check if item is already in datasource.
        /// </summary>
        /// <param name="id">Id of Item.</param>
        /// <returns>Boolean value.</returns>
        bool CheckIfItemExists(int id);

        Task<List<ItemModel>> GetAllItemFromUser(HttpContext httpContext);

    }
}
