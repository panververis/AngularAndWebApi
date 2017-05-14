using System.Collections.Generic;

namespace AngularAndWebApi.Models.DTOs
{
    public class StaffDTO
    {
        //// Fields
        public int              ID          { get; set; }
        public string           FirstName   { get; set; }
        public string           LastName    { get; set; }
        public JobType          JobType     { get; set; }

        public int              DealerID    { get; set; }
        public string           DealerName  { get; set; }

        //// Detail colections
        public List<SaleDTO>    Sales       { get; set; }

        //// Constructor
        public StaffDTO() {
            Sales               =  new List<SaleDTO>();
        }
    }
}