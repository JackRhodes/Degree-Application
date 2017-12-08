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

namespace Degree_Application.Controllers
{
    public class ItemController : Controller
    {
        
        private IItemRepository _itemRepository;

        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }


        //[HttpGet("id")]
        // GET: ItemModels
        public async Task<IActionResult> Index(string search)
        {
            return View("Index", await _itemRepository.FuzzySearchTitleAsync(search));
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
            return View();
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
