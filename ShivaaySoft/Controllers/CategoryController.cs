using Microsoft.AspNetCore.Mvc;
using ShivaaySoft.Contract;
using ShivaaySoft.Models.Entities;
using ShivaaySoft.ViewModels;
using System;
using System.Threading.Tasks;

namespace ShivaaySoft.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _CategoryRepository;
        public CategoryController(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }
        public IActionResult Index()
        {
            var result = _CategoryRepository.GetAll();
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel vm)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    CategoryEntity Category = new CategoryEntity
                    {
                        Title = vm.Title,
                    };

                    await _CategoryRepository.AddAsync(Category);
                    TempData["message"] = $"{Category.Title} added successfully";
                    return RedirectToAction("Index");
                }
                TempData["message"] = $"All Field Are Required";
                return RedirectToAction("create");
            }
            catch (Exception ex)
            {
                TempData["message"] = $"{ex} data contains some invald items";
                throw;
            }
        }

        public IActionResult Edit(int id)
        {
            CategoryEntity result = _CategoryRepository.GetFirstOrDefault(a => a.Id.Equals(id));
            if (result == null)
            {
                return RedirectToAction("Index");
            }

            CategoryViewModel vm = new CategoryViewModel
            {
                Title = result.Title,
            };

            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CategoryEntity result = _CategoryRepository.GetFirstOrDefault(a => a.Id.Equals(vm.Id)); // equals Returns a value indicating whether this instance is equal to a specified object.
                    if (result == null)
                    {
                        return RedirectToAction("Index");
                    }
                    result.Title = vm.Title;

                    await _CategoryRepository.UpdateAsync(result);
                    TempData["message"] = $"{result.Title} edited successfully";
                    return RedirectToAction("Index");
                }
                TempData["message"] = $"Data is invalid";
                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                TempData["Message"] = $"{ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost] 
        public async Task<IActionResult> Delete(int id)
        {
            CategoryEntity result = _CategoryRepository.GetFirstOrDefault(a => a.Id.Equals(id));
            if (result == null)
            {
                return RedirectToAction("Index");
            }
            await _CategoryRepository.RemoveAsync(result);

            TempData["message"] = $"{result.Title}Deleted succesfully";
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            CategoryEntity result = _CategoryRepository.GetFirstOrDefault(a => a.Id.Equals(id));
            if (result == null)
            {
                return RedirectToAction("Index");
            }
            return View(result);
        }



    }
}
