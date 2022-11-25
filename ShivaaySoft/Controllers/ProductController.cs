using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShivaaySoft.Contract;
using ShivaaySoft.Models.Entities;
using ShivaaySoft.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShivaaySoft.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _ProductRepository;
        private readonly ICategoryRepository _CategoryRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductController(IProductRepository ProductRepository,
            ICategoryRepository CategoryRepository, IWebHostEnvironment hostingEnvironment)
        {
            _ProductRepository = ProductRepository;
            _CategoryRepository = CategoryRepository;
            _hostingEnvironment = hostingEnvironment;
        }


        public IActionResult Index()
        {

            return View(_ProductRepository.GetAll());
        }
        public IActionResult Create()
        {
            ViewBag.Categories = _CategoryRepository.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (vm.File == null)
                    {
                        TempData["Message"] = "File cannot be null";
                        return View();
                    }
                    var fileName = Path.GetFileName(vm.File.FileName);
                    //judge if it is pdf file
                    string ext = Path.GetExtension(vm.File.FileName);
                    if (ext.ToLower() == ".pdf")
                    {
                        TempData["Message"] = "Only Images are allowed";
                        return View();
                    }

                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", fileName);
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await vm.File.CopyToAsync(fileSteam);
                    }
                    ProductEntity Product = new ProductEntity
                    {
                        Title = vm.Title,
                        CategoryId = vm.CategoryId,
                        FilePath = fileName,
                        Description = vm.Description,
                        Excerpt = vm.Excerpt
                    };
                    await _ProductRepository.AddAsync(Product);
                    TempData["Message"] = $"{Product.Title} added successfully";
                    return RedirectToAction("Index");
                }
                ViewBag.Categories = _CategoryRepository.GetAll();
                TempData["Message"] = $"All Field Are Required";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Categories = _CategoryRepository.GetAll();
                TempData["Message"] = $"{ex} data contains some invald items";
                throw;
            }
        }


        [HttpPost]
        public IActionResult GetProductList()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault(); // get total page size
                var start = Request.Form["start"].FirstOrDefault(); // get starte length size from request.
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault(); // check if there is any search characters passed
                int pageSize = length != null ? Convert.ToInt32(length) : 1;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var personData = _ProductRepository.GetAll(); // get data from database
                                                              //check for sorting column number and direction


                // if there is any search value, filter results
                if (!string.IsNullOrEmpty(searchValue))
                {
                    personData = personData.Where(m => m.Title.ToLower().Contains(searchValue.ToLower()));
                }
                // get total records acount
                recordsTotal = personData.Count();
                //get page data
                var data = personData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                throw;
            }

        }



        public IActionResult Edit(int id)
        {
            ProductEntity result = _ProductRepository.GetFirstOrDefault(a => a.Id.Equals(id));
            if (result == null)
                return RedirectToAction("Index");

            ProductViewModel vm = new ProductViewModel
            {
                Title = result.Title,
                Description = result.Description,
                CategoryId = result.CategoryId,
                Excerpt = result.Excerpt
            };

            ViewBag.Categories = _CategoryRepository.GetAll();
            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProductEntity result = _ProductRepository.GetFirstOrDefault(a => a.Id.Equals(vm.Id)); // equals Returns a value indicating whether this instance is equal to a specified object.
                    if (result == null)
                        return RedirectToAction("Index");


                    var uniqueFileName = string.Empty;
                    if (vm.File != null)
                    {
                        var fileName = Path.GetFileName(vm.File.FileName);
                        //judge if it is pdf file
                        string ext = Path.GetExtension(vm.File.FileName);
                        if (ext.ToLower() == ".pdf")
                        {
                            TempData["Message"] = "Only Images are allowed";
                            return View();
                        }

                        var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", fileName);
                        uniqueFileName = vm.File.FileName;
                        using (var fileSteam = new FileStream(filePath, FileMode.Create))
                        {
                            await vm.File.CopyToAsync(fileSteam);
                        }

                    }

                    result.Title = vm.Title;
                    result.Excerpt = vm.Excerpt;
                    result.CategoryId = vm.CategoryId;
                    result.Description = vm.Description;
                    result.FilePath = uniqueFileName;

                    await _ProductRepository.UpdateAsync(result);
                    TempData["Message"] = $"{result.Title} edited successfully";
                    return RedirectToAction("Index");
                }
                TempData["Message"] = $"Data is invalid";
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
            ProductEntity result = _ProductRepository.GetFirstOrDefault(a => a.Id.Equals(id));
            if (result == null)
            {
                return RedirectToAction("Index");
            }
            await _ProductRepository.RemoveAsync(result);

            TempData["Message"] = $"{result.Title} Deleted succesfully";
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            ProductEntity result = _ProductRepository.GetFirstOrDefault(a => a.Id.Equals(id));
            if (result == null)
            {
                return RedirectToAction("Index");
            }

            return View(result);
        }



    }
}
