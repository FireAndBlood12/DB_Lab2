using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DB_LAB2.Database.Entities;

namespace DB_LAB2.Models
{
    public class DB_LAB2Context : DbContext
    {
        public DB_LAB2Context (DbContextOptions<DB_LAB2Context> options)
            : base(options)
        {
        }

        public DbSet<DB_LAB2.Database.Entities.Group> Group { get; set; }

        public DbSet<DB_LAB2.Database.Entities.Subject> Subject { get; set; }

        public DbSet<DB_LAB2.Database.Entities.Student> Student { get; set; }

        public DbSet<DB_LAB2.Database.Entities.Mark> Mark { get; set; }

        public DbSet<DB_LAB2.Database.Entities.Teacher> Teacher { get; set; }
    }
}
