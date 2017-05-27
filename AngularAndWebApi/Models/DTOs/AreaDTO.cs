using System.Collections.Generic;

namespace AngularAndWebApi.Models.DTOs
{
    public class AreaDTO {

        //// Fields
        public int              id      { get; set; }
        public string           name    { get; set; }

        //// Detail colections
        public List<RegionDTO>  regions { get; set; }

        //// Constructor
        public AreaDTO() {
            regions = new List<RegionDTO>();
        }

    }
}