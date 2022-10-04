using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using restaurantWebApp_DAL.Data;
using restaurantWebApp_DAL.Dto;
using restaurantWebApp_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace restaurantWebApp_DAL.Repositeries
{
    class MealsRepositery
    {
        //private readonly ApplicationDbContext _db;
        //private readonly IMapper _mapper;
        //private IWebHostEnvironment webHostEnvironment;
        //public MealsRepositery(ApplicationDbContext db, IMapper mapper, IWebHostEnvironment environment)
        //{
        //    _db = db;
        //    _mapper = mapper;
        //    webHostEnvironment = environment;
        //}

        //public Task<List<Meal>> GetAllMeals()
        //{
        //    var meals = _db.Meals!.Include(c => c.Category).ToList();
            
        //    return Task.Run(()=>meals);
        //}
        //public Task<Meal> Add(Meal meal)
        //{



        //    meal.Image = UploadedFile(meal);
        //    _db.Meals!.Add(meal);
        //    _db.SaveChanges();
        //    return Task.Run(()=> meal) ;
        //}
        //public IActionResult Edit(int? id)
        //{
           
        //    var meal = _db.Meals!.FirstOrDefault(c => c.Id == id);
        //    if (meal == null)
        //    {
        //        return NotFound();
        //    }
        //    var mealVM = new MealsViewModel
        //    {
        //        Id = meal.Id,
        //        Name = meal.Name,
        //        CategoryId = meal.CategoryId,
        //        Description = meal.Description,
        //        Price = meal.Price,

        //    };
        //    var categories = _db.Categories!.ToList();
        //    List<SelectListItem> categoriesList = new List<SelectListItem>();
        //    foreach (var category in categories)
        //    {
        //        categoriesList.Add(new SelectListItem { Value = category.Id.ToString(), Text = category.Name });
        //    }
        //    ViewBag.categoriesList = categoriesList;
        //    return View(mealVM);

        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(Meal meal)
        //{
           
        //    var meal = _db.Meals!.ToList().FirstOrDefault(m => m.Id == meal.Id);
        //    string uniqueFileName = UploadedFile(meal);
        //    meal!.Name = mealVM.Name;
        //    meal.Price = mealVM.Price;
        //    meal.Description = mealVM.Description;
        //    meal.CategoryId = mealVM.CategoryId;
        //    meal.Image = uniqueFileName;
        //    _db.Meals!.Update(meal!);
        //    _db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        //public IActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return BadRequest();
        //    }
        //    var meal = _db.Meals!.FirstOrDefault(m => m.Id == id);
        //    var mealDto = _mapper.Map<MealDto>(meal);
        //    return View(mealDto);
        //}
        //private string UploadedFile(Meal model)
        //{
        //    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
        //    string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image!.FileName;
        //    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //    var fileStream = new FileStream(filePath, FileMode.Create);

        //    model.Image.CopyTo(fileStream);


        //    return uniqueFileName;
        //}
    }
}
