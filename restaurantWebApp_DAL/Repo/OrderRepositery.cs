using restaurantWebApp_DAL.Contracts;
using restaurantWebApp_DAL.Data;
using restaurantWebApp_DAL.Models;
using System.Collections.Concurrent;

namespace restaurantWebApp_DAL.Repo
{
    public class OrderRepositery:IRepositoryBase<Order>
    {
        private readonly ConcurrentDictionary<int, Order> ordersCash;
        private readonly ApplicationDbContext db;
        public OrderRepositery(ApplicationDbContext db)
        {
            this.db = db;
            if (ordersCash == null)
            {
                ordersCash = new ConcurrentDictionary<int, Order>(db.Orders!.ToDictionary(o => o.Id));
            }

        }

        public async Task<Order> CreateAsync(Order order)
        {
            await db.Orders!.AddAsync(order);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                return ordersCash.AddOrUpdate(order.Id, order, UpdateCash!);
            }
            else
            {
                return null!;
            }
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Order order = db.Orders!.Find(id)!;
            db.Orders!.Remove(order);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                return ordersCash.TryRemove(id, out order!);
            }
            return null;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await Task.Run<IEnumerable<Order>>(() => ordersCash.Values.ToList());
        }

        public Task<Order> GetByIdAsync(int id)
        {
            return Task.Run(() =>
            {
                ordersCash.TryGetValue(id, out Order? order);
                return order!;
            });
        }

        public async Task<Order> UpadteAsync(int id, Order order)
        {
            // update in database
            db.Orders!.Update(order!);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                // update in cache
                return UpdateCash(id, order!)!;
            }
            return null!;
        }
        private Order UpdateCash(int id, Order order)
        {
            Order? old;
            if (ordersCash!.TryGetValue(id, out old))
            {
                if (ordersCash.TryUpdate(id, order, old))
                {
                    return order;
                }
            }
            return null!;
        }
    }
}
