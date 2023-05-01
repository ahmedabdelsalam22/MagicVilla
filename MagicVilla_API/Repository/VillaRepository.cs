using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace MagicVilla_API.Repository
{
    public class VillaRepository : IVillaRepository
    {
        private readonly ApplicationDbContext _db;

        public VillaRepository(ApplicationDbContext db) 
        {
            _db = db;
        }

        public async Task CreateAsync(Villa entity)
        {
            await _db.Villas.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<Villa> GetAsync(Expression<Func<Villa, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Villa> villas = _db.Villas;
            if (!tracked) 
            {
                villas= villas.AsNoTracking();
            }
            if (filter != null)
            {
                villas = villas.Where(filter);
            }
            return await villas.FirstOrDefaultAsync();
        }

        public async Task<List<Villa>> GetAllAsync(Expression<Func<Villa, bool>> filter = null)
        {
            IQueryable<Villa> villas = _db.Villas;

            if (filter != null) 
            {
                villas = villas.Where(filter);
            }
            return await villas.ToListAsync();
        }

        public async Task RemoveAsync(Villa entity)
        {
            _db.Villas.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
           await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Villa entity)
        {
            _db.Villas.Update(entity);
            await SaveAsync();
        }
    }
}
