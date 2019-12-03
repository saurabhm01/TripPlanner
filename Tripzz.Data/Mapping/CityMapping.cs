using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tripzz.Entity;

namespace Tripzz.Data.Mapping
{
    public class CityMapping
    {
        public CityMapping(EntityTypeBuilder<CityModel> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Name).IsRequired();
        }
    }
}
