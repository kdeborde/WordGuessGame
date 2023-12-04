using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordGuessGame.Core.Entities;

namespace WordGuessGame.Infrastructure.Contexts
{
    public class WordDbContext : DbContext
    {
        public WordDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Word> Words { get; set; }
    }
}
