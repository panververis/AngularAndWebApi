using System.Collections.Generic;

namespace AngularAndWebApi.Models.DTOs
{
    public class AreaDTO {

        //// Fields
        public int              ID { get; set; }
        public string           Name { get; set; }

        //// Detail colections
        public List<RegionDTO>  Regions { get; set; }

        //// Constructor
        public AreaDTO() {
            Regions = new List<RegionDTO>();
        }

    }
}