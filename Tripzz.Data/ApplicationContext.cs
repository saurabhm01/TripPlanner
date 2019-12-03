using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tripzz.Data.Mapping;
using Tripzz.Entity;

namespace Tripzz.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new CityMapping(modelBuilder.Entity<CityModel>());
            new PlaceMapping(modelBuilder.Entity<PlaceModel>());
        }
    }
}
