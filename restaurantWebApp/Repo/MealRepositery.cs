using Microsoft.EntityFrameworkCore;
using restaurantWebApp.Contracts;
using restaurantWebApp.Data;
using restaurantWebApp.Models;
using System;
using System.Collections.Concurrent;

namespace restaurantWebApp.Repo
{
    public class MealRepositery: IRepositoryBase<Meal>
    {
        private readonly ConcurrentDictionary<int, Meal> mealsCash;
        private readonly ApplicationDbContext db;
        public MealRepositery(ApplicationDbContext db)
        {
            this.db = db;
            if (mealsCash == null)
            {
                mealsCash = new ConcurrentDictionary<int, Meal>(db.Meals!.Include(c=>c.Category).ToDictionary(s => s.Id));
            }

        }

        public async Task<Meal> CreateAsync(Meal meal)
        {
            await db.Meals!.AddAsync(meal);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                return mealsCash.AddOrUpdate(meal.Id, meal, UpdateCash!);
            }
            else
            {
                return null!;
            }
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Meal meal = db.Meals!.Find(id)!;
            db.Meals!.Remove(meal);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                return mealsCash.TryRemove(id, out meal!);
            }
            return null;
        }

        public async Task<IEnumerable<Meal>> GetAllAsync()
        {
            return await Task.Run<IEnumerable<Meal>>(() => mealsCash.Values);
        }

        public Task<Meal> GetByIdAsync(int id)
        {
            return Task.Run(() =>
            {
                mealsCash.TryGetValue(id, out Meal? meal);
                return meal!;
            });
        }

        public async Task<Meal> UpadteAsync(int id, Meal meal)
        {
            // update in database
            db.Meals!.Update(meal!);
            
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                // update in cache
                return UpdateCash(id, meal!)!;
            }
            return null!;
        }
        private Meal UpdateCash(int id, Meal meal)
        {
            Meal? old;
            if (mealsCash!.TryGetValue(id, out old))
            {
                if (mealsCash.TryUpdate(id, meal, old))
                {
                    return meal;
                }
            }
            return null!;
        }
    }
}
