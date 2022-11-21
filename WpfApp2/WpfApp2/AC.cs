using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    internal class AC : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Online> Onlines { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public AC() : base("DefaultConnection") { }
    }
}
