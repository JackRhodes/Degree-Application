using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Degree_Application.Data;
using Degree_Application.Models;
using Microsoft.AspNetCore.Identity;
using Degree_Application.Data.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Degree_Application.Controllers
{
    public class ItemController : Controller
    {

        private IItemRepository _itemRepository;
        private SignInManager<AccountModel> _signInManager;

        public ItemController(IItemRepository itemRepository, SignInManager<AccountModel> signInManager)
        {
            _itemRepository = itemRepository;
            _signInManager = signInManager;

        }

        //[HttpGet("id")]
        [Route("Item")]
        [Route("Item/Index")]
        // GET: ItemModels
        public async Task<IActionResult> Index(string search, string sortOrder)
        {

            //This controller was influenced by: https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/sort-filter-page

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "item_desc" : "";
            //Swap Date and Date 
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            
            var result = await _itemRepository.FuzzySearchTitleAsync(search);
            
            //Create IEnumerable to enable iteration through the results ordered list. This saves database calls.
            IOrderedEnumerable<ItemModel> itemList;

            switch (sortOrder)
            {
                case "item_desc":
                    itemList = result.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    itemList = result.OrderBy(s => s.DatePosted);
                    break;
                case "date_desc":
                    itemList = result.OrderByDescending(s => s.DatePosted);
                    break;
                default:
                    itemList = result.OrderBy(s => s.DatePosted);
                    break;
            }

            //Convert back to a list to enable the view to output.
            return View("Index", itemList.ToList());
        }

        [Route("{id}")]
        // GET: ItemModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ItemModel item = await _itemRepository.GetItemByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: ItemModels/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
                return View();

            return RedirectToAction("Login", "Account");
        }

        // POST: ItemModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Price,Status")] ItemModel itemModel)
        {
            if (ModelState.IsValid)
            {
                itemModel.DatePosted = DateTime.Now;
                //Get the HttpContext from the Post.
                HttpContext httpContext = HttpContext;

                await _itemRepository.CreateItemAsync(itemModel, httpContext);

                return RedirectToAction(nameof(Index));
            }
            return View(itemModel);
        }

        // GET: ItemModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemModel = await _itemRepository.GetSingleItemByIdAsync(id);

            if (itemModel == null)
            {
                return NotFound();
            }
            return View(itemModel);
        }

        // POST: ItemModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Price,Status")] ItemModel itemModel)
        {
            if (id != itemModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _itemRepository.UpdateItemAsync(itemModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemModelExists(itemModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(itemModel);
        }

        // GET: ItemModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemModel = await _itemRepository.GetSingleItemByIdAsync(id);

            if (itemModel == null)
            {
                return NotFound();
            }

            return View(itemModel);
        }

        // POST: ItemModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ItemModel itemModel = await _itemRepository.GetSingleItemByIdAsync(id);
            await _itemRepository.DeleteItemAsync(itemModel);
            return RedirectToAction(nameof(Index));
        }

        private bool ItemModelExists(int id)
        {
            return _itemRepository.CheckIfItemExists(id);
        }

    }
}
