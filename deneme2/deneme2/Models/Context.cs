using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace deneme2.Models
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }


        // DbSet'ler burada tanımlanır, her DbSet bir veritabanı tablosuna karşılık gelir.
        public DbSet<Product> Products { get; set; }
        public DbSet<Compan> Compans { get; set; }
    }
}
