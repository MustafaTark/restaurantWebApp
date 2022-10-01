using AutoMapper;
using Grpc.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using restaurantWebApp.Data;
using restaurantWebApp.Dto;
using restaurantWebApp.Models;
using restaurantWebApp.ViewModels;

namespace restaurantWebApp.Controllers
{
    public class MealsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
       private IWebHostEnvironment webHostEnvironment;
        public MealsController(ApplicationDbContext db,IMapper mapper,IWebHostEnvironment environment)
        {
            _db = db;
            _mapper = mapper;
            webHostEnvironment = environment;
        }

        public IActionResult Index()
        {
            var meals= _db.Meals!.Include(c=>c.Category).ToList();
            return View(meals);
        }
        public IActionResult Add()
        {
            var categories = _db.Categories!.ToList();
            var meal = new MealsViewModel();
            List<SelectListItem> categoriesList = new List<SelectListItem>();
            foreach (var category in categories)
            {
                categoriesList.Add(new SelectListItem { Value = category.Id.ToString(), Text = category.Name});
            }
            ViewBag.categoriesList = categoriesList;
            return View(meal);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(MealsViewModel mealVM)
        {
            if(!ModelState.IsValid){
                return View(mealVM);
            }
            
            string uniqueFileName = UploadedFile(mealVM);
            var meal = new Meal
            {
                Name = mealVM.Name,
                Description = mealVM.Description,
                CategoryId = mealVM.CategoryId,
                Image = uniqueFileName,
                Price=mealVM.Price
            };
            _db.Meals!.Add(meal);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var meal=_db.Meals!.FirstOrDefault(c => c.Id == id);
            if(meal == null)
            {
                return NotFound();
            }
            var mealVM = new MealsViewModel
            {
                Id = meal.Id,
                Name = meal.Name,
                CategoryId = meal.CategoryId,
                Description = meal.Description,
                Price = meal.Price,
               
            };
            var categories = _db.Categories!.ToList();
            List<SelectListItem> categoriesList = new List<SelectListItem>();
            foreach (var category in categories)
            {
                categoriesList.Add(new SelectListItem { Value = category.Id.ToString(), Text = category.Name });
            }
            ViewBag.categoriesList = categoriesList;
            return View(mealVM);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MealsViewModel mealVM)
        {
            if (!ModelState.IsValid)
            {
                return View(mealVM);
            }
            var meal = _db.Meals!.ToList().FirstOrDefault(m => m.Id == mealVM.Id);
            string uniqueFileName = UploadedFile(mealVM);
            meal!.Name = mealVM.Name;
            meal.Price = mealVM.Price;
            meal.Description = mealVM.Description;
            meal.CategoryId=mealVM.CategoryId;
            meal.Image = uniqueFileName;
            _db.Meals!.Update(meal!);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var meal= _db.Meals!.FirstOrDefault(m => m.Id == id);
            var mealDto = _mapper.Map<MealDto>(meal);
            return View(mealDto);
        }
        private string UploadedFile(MealsViewModel model)
        {
            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image!.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                var fileStream = new FileStream(filePath, FileMode.Create);
                
                    model.Image.CopyTo(fileStream);
                
            
            return uniqueFileName;
        }
    }

}

