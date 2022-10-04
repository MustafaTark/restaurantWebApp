using AutoMapper;
using Grpc.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using restaurantWebApp_DAL.Data;
using restaurantWebApp_DAL.Dto;
using restaurantWebApp_DAL.Models;
using restaurantWebApp_BAL.ViewModels;
using restaurantWebApp_DAL.Repo;
using restaurantWebApp_DAL.Contracts;

namespace restaurantWebApp.Controllers
{
    public class MealsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
       private IWebHostEnvironment webHostEnvironment;
        private readonly IRepositoryBase<Meal>  _repoToMeal;
        private readonly IRepositoryBase<Category> _repoToCategory;
        public MealsController(ApplicationDbContext db,
            IMapper mapper,
            IWebHostEnvironment environment,
            IRepositoryBase<Meal> repoToMeal,IRepositoryBase<Category> repoToCategory)
        {
            _db = db;
            _mapper = mapper;
            webHostEnvironment = environment;
            _repoToMeal= repoToMeal;
            _repoToCategory= repoToCategory;
        }

        public async Task<IActionResult> Index()
        {
            var meals= await _repoToMeal.GetAllAsync();
            return View(meals);
        }
        public async Task<IActionResult> Add()
        {
            var categories =await _repoToCategory.GetAllAsync();
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
        public async Task<IActionResult> Add(MealsViewModel mealVM)
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
          await  _repoToMeal.CreateAsync(meal);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            
            var meal=await _repoToMeal.GetByIdAsync(id);
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
            var categories =await _repoToCategory.GetAllAsync();
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
        public async Task<IActionResult> Edit(MealsViewModel mealVM)
        {
            if (!ModelState.IsValid)
            {
                return View(mealVM);
            }
            var meal =await _repoToMeal.GetByIdAsync(mealVM.Id);
            string uniqueFileName = UploadedFile(mealVM);
            meal!.Name = mealVM.Name;
            meal.Price = mealVM.Price;
            meal.Description = mealVM.Description;
            meal.CategoryId=mealVM.CategoryId;
            meal.Image = uniqueFileName;
           await _repoToMeal.UpadteAsync(meal.Id, meal);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            
            var meal= await _repoToMeal.GetByIdAsync(id);
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

