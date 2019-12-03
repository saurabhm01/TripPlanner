using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tripzz.Entity
{
    [Table("Place")]
    public class PlaceModel : BaseEntity
    {
        public long CityId { get; set; }

        [NotMapped]
        public string CityName { get; set; }
        public string Name { get; set; }
        public string PlaceDescription { get; set; }
        public string BestTimeToVisit { get; set; }
        public int Distance { get; set; }

        public string Tips { get; set; }
        public string ImageUrl { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Address { get; set; }
    }
}
