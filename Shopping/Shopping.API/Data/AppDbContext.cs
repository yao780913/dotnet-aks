﻿using Microsoft.EntityFrameworkCore;
using Shopping.Domain;

namespace Shopping.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext (DbContextOptions<AppDbContext> options)
        :base (options)
    {
        
    }
    
    public DbSet<Product> Products { get; set; }
}