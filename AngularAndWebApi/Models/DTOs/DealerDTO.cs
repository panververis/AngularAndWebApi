using System.Collections.Generic;

namespace AngularAndWebApi.Models.DTOs
{
    public class DealerDTO
    {

        ///// Fields
        public int                  id              { get; set; }
        public string               name            { get; set; }

        ///// Detail Collections
        public List<StaffDTO>       staffMembers   { get; set; }

        ///// Constructor
        public DealerDTO() {
            staffMembers = new List<StaffDTO>();
        }
    }
}