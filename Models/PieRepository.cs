using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly AppDbContext _context;

        public PieRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Pie> GetAllPies()
        {
            return _context.Pies.OrderBy(p => p.Name);
        }

        public Pie GetPieById(int pieId)
        {
            return _context.Pies.FirstOrDefault(p => p.Id == pieId);
        }
    }
}
