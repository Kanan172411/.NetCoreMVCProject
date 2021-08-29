using EduHomeBackendProject.DAL;
using EduHomeBackendProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }
        public Setting GetSetting()
        {
            return _context.Setting.FirstOrDefault();
        }
    }
}
