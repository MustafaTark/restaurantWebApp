using restaurantWebApp.Contracts;
using restaurantWebApp.Data;
using restaurantWebApp.Models;
using System.Collections.Concurrent;

namespace restaurantWebApp.Repo
{
    public class CartRepositery:IRepositoryBase<Cart>
    {
        private readonly ConcurrentDictionary<int, Cart> cartsCash;
        private readonly ApplicationDbContext db;
        public CartRepositery(ApplicationDbContext db)
        {
            this.db = db;
            if (cartsCash == null)
            {
                cartsCash = new ConcurrentDictionary<int, Cart>(db.Carts!.ToDictionary(c => c.Id));
            }

        }

        public async Task<Cart> CreateAsync(Cart cart)
        {
            await db.Carts!.AddAsync(cart);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                return cartsCash.AddOrUpdate(cart.Id, cart, UpdateCash!);
            }
            else
            {
                return null!;
            }
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Cart cart = db.Carts!.Find(id)!;
            db.Carts!.Remove(cart);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                return cartsCash.TryRemove(id, out cart!);
            }
            return null;
        }

        public async Task<IEnumerable<Cart>> GetAllAsync()
        {
            return await Task.Run<IEnumerable<Cart>>(() => cartsCash.Values);
        }

        public Task<Cart> GetByIdAsync(int id)
        {
            return Task.Run(() =>
            {
                cartsCash.TryGetValue(id, out Cart? cart);
                return cart!;
            });
        }

        public async Task<Cart> UpadteAsync(int id, Cart cart)
        {
            // update in database
            db.Carts!.Update(cart!);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                // update in cache
                return UpdateCash(id, cart!)!;
            }
            return null!;
        }
        private Cart UpdateCash(int id, Cart cart)
        {
            Cart? old;
            if (cartsCash!.TryGetValue(id, out old))
            {
                if (cartsCash.TryUpdate(id, cart, old))
                {
                    return cart;
                }
            }
            return null!;
        }
    }
}
