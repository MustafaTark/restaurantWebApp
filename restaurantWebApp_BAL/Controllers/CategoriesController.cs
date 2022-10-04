using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using restaurantWebApp_DAL.Data;
using restaurantWebApp_DAL.Dto;
using restaurantWebApp_DAL.Models;
using restaurantWebApp_BAL.ViewModels;
using System.Drawing;
using Microsoft.AspNetCore.Hosting;
using restaurantWebApp_DAL.Contracts;

namespace restaurantWebApp_BAL.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private IWebHostEnvironment webHostEnvironment;
        private readonly IRepositoryBase<Meal> _repoToMeal;
        private readonly IRepositoryBase<Category> _repoToCategory;
        public CategoriesController(ApplicationDbContext db,
            IMapper mapper,
            IWebHostEnvironment environment,
            IRepositoryBase<Meal> repoToMeal, IRepositoryBase<Category> repoToCategory
            )
        {
            _db = db;
            _mapper = mapper;
            webHostEnvironment = environment;
            _repoToMeal = repoToMeal;
            _repoToCategory = repoToCategory;
        }

        public async Task< IActionResult> Index()
        {
            var categories =await _repoToCategory.GetAllAsync();

            return View(categories);
        }
        public IActionResult Add()
        {
            var category = new CategoriesViewModel();
            
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Add(CategoriesViewModel categoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryVM);
            }

            string uniqueFileName = UploadedFile(categoryVM);
            var category=new Category
            {
                Name = categoryVM.Name,
                Image=uniqueFileName
            };
            
           await _repoToCategory.CreateAsync(category);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            
            var category =await _repoToCategory.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            

            var categoryVM = new CategoriesViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description=category.Description,

            };
            return View(categoryVM);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Edit(CategoriesViewModel categoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryVM);
            }
            var category = await _repoToCategory.GetByIdAsync(categoryVM.Id);
            string uniqueFileName = UploadedFile(categoryVM);
            category!.Name=categoryVM.Name;
            category.Description=categoryVM.Description;
            category.Image = uniqueFileName;
            await _repoToCategory.UpadteAsync(category.Id,category);
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
           
            var category = _repoToCategory.GetByIdAsync(id);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return View(categoryDto);
        }
        private string UploadedFile(CategoriesViewModel model)
        {
            string uniqueFileName;

          

                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image!.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                var fileStream = new FileStream(filePath, FileMode.Create);
                
                model.Image.CopyTo(fileStream);
                
            
            return uniqueFileName;
        }
    }
}
