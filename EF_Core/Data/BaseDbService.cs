using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Data
{
    public class BaseDbService
    {

        private static BaseDbService? _instance;
        private readonly AppDbContext _context;

        private BaseDbService()
        {
            _context = new AppDbContext();
        }

        public static BaseDbService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BaseDbService();
                return _instance;
            }
        }

        public AppDbContext Context => _context;
    }
}
