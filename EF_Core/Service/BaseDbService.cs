using EF_Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Service
{
    public class BaseDbService
    {

        public static AppDbContext Instance { get; } = new AppDbContext();
        private readonly AppDbContext _context;

        private BaseDbService()
        {
            _context = new AppDbContext();
        }


        public AppDbContext Context => _context;
    }
}
