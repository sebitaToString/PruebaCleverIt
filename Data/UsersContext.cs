using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaSRamos.Models;

namespace PruebaTecnicaSRamos.Data
{
    public class UsersContext : DbContext
    {
        public UsersContext (DbContextOptions<UsersContext> options)
            : base(options)
        {
        }

        public DbSet<PruebaTecnicaSRamos.Models.User> User { get; set; }
    }
}
