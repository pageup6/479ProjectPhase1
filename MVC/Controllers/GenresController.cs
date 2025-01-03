﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;
using BLL.DAL;
using BLL.Services.Bases;

// Generated from Custom Template.

namespace MVC.Controllers
{
    public class GenresController : MvcController
    {
        // Service injections:
        private readonly IService<Genre, GenreModel> _genreService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //private readonly IManyToManyRecordService _ManyToManyRecordService;

        public GenresController(
            IService<Genre, GenreModel> genreService

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //, IManyToManyRecordService ManyToManyRecordService
        )
        {
            _genreService = genreService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //_ManyToManyRecordService = ManyToManyRecordService;
        }

        // GET: Genres
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _genreService.Query().ToList();
            return View(list);
        }

        // GET: Genres/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _genreService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            
            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //ViewBag.ManyToManyRecordIds = new MultiSelectList(_ManyToManyRecordService.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Genres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GenreModel genre)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _genreService.Create(genre.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = genre.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(genre);
        }

        // GET: Genres/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _genreService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Genres/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GenreModel genre)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _genreService.Update(genre.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = genre.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(genre);
        }

        // GET: Genres/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _genreService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Genres/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _genreService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
