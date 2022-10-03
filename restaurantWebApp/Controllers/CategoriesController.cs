using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using restaurantWebApp.Data;
using restaurantWebApp.Dto;
using restaurantWebApp.Models;
using restaurantWebApp.ViewModels;
using System.Drawing;

namespace restaurantWebApp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private IWebHostEnvironment webHostEnvironment;
        public CategoriesController(ApplicationDbContext db, IMapper mapper, IWebHostEnvironment environment)
        {
            _db = db;
            _mapper = mapper;
            webHostEnvironment = environment;
        }

        public IActionResult Index()
        {
            var categories = _db.Categories!.Include(m=>m.Meals).ToList();

            return View(categories);
        }
        public IActionResult Add()
        {
            var category = new CategoriesViewModel();
            
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CategoriesViewModel categoryVM)
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
            
            _db.Categories!.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var category = _db.Categories!.FirstOrDefault(c => c.Id == id);
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
        public IActionResult Edit(CategoriesViewModel categoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryVM);
            }
            var category = _db.Categories!.FirstOrDefault(c=>c.Id==categoryVM.Id);
            string uniqueFileName = UploadedFile(categoryVM);
            category!.Name=categoryVM.Name;
            category.Description=categoryVM.Description;
            category.Image = uniqueFileName;
            _db.Categories!.Update(category!);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var category = _db.Categories!.Include(m=>m.Meals).FirstOrDefault(c => c.Id == id);
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
