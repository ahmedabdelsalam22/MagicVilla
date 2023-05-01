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
        public async Task UpdateAsync(Villa entity)
        {
            _db.Villas.Update(entity);
            await SaveAsync();
        }
    }
}
