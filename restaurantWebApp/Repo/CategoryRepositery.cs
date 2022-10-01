using restaurantWebApp.Contracts;
using restaurantWebApp.Data;
using restaurantWebApp.Models;
using System.Collections.Concurrent;

namespace restaurantWebApp.Repo
{
    public class CategoryRepositery:IRepositoryBase<Category>
    {
        private readonly ConcurrentDictionary<int, Category> catregoriesCash;
        private readonly ApplicationDbContext db;
        public CategoryRepositery(ApplicationDbContext db)
        {
            this.db = db;
            if (catregoriesCash == null)
            {
                catregoriesCash = new ConcurrentDictionary<int, Category>(db.Categories!.ToDictionary(c => c.Id));
            }

        }

        public async Task<Category> CreateAsync(Category category)
        {
            await db.Categories!.AddAsync(category);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                return catregoriesCash.AddOrUpdate(category.Id, category, UpdateCash!);
            }
            else
            {
                return null!;
            }
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Category category = db.Categories!.Find(id)!;
            db.Categories!.Remove(category);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                return catregoriesCash.TryRemove(id, out category!);
            }
            return null;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await Task.Run<IEnumerable<Category>>(() => catregoriesCash.Values);
        }

        public Task<Category> GetByIdAsync(int id)
        {
            return Task.Run(() =>
            {
                catregoriesCash.TryGetValue(id, out Category? category);
                return category!;
            });
        }

        public async Task<Category> UpadteAsync(int id, Category category)
        {
            // update in database
            db.Categories!.Update(category!);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                // update in cache
                return UpdateCash(id, category!)!;
            }
            return null!;
        }
        private Category UpdateCash(int id, Category category)
        {
            Category? old;
            if (catregoriesCash!.TryGetValue(id, out old))
            {
                if (catregoriesCash.TryUpdate(id, category, old))
                {
                    return category;
                }
            }
            return null!;
        }
    }
}
