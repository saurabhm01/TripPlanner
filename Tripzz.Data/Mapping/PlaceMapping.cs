using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tripzz.Entity;

namespace Tripzz.Data.Mapping
{
    public class PlaceMapping
    {
        public PlaceMapping(EntityTypeBuilder<PlaceModel> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Name).IsRequired();
            entityBuilder.Property(t => t.PlaceDescription).IsRequired();
            entityBuilder.Property(t => t.Latitude).IsRequired();
            entityBuilder.Property(t => t.Longitude).IsRequired();
        }
    }
}
