using System.Collections.Generic;

namespace AngularAndWebApi.Models.DTOs
{
    public class DealerDTO
    {

        ///// Fields
        public int                  ID              { get; set; }
        public string               Name            { get; set; }

        ///// Detail Collections
        public List<StaffDTO>       StaffMembers   { get; set; }

        ///// Constructor
        public DealerDTO() {
            StaffMembers = new List<StaffDTO>();
        }
    }
}