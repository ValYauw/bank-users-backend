using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BNI_Users_backend.Models;

namespace BNI_Users_backend.Data
{
    public class UserContext : DbContext
    {
        public UserContext (DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<BNI_Users_backend.Models.User> User { get; set; } = default!;
    }
}
