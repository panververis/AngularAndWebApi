using System.Collections.Generic;

namespace AngularAndWebApi.Models.DTOs
{
    public class StaffDTO
    {
        //// Fields
        public int              id          { get; set; }
        public string           firstName   { get; set; }
        public string           lastName    { get; set; }
        public JobType          jobType     { get; set; }

        public int              dealerID    { get; set; }
        public string           dealerName  { get; set; }

        //// Detail colections
        public List<SaleDTO>    sales       { get; set; }

        //// Constructor
        public StaffDTO() {
            sales               =  new List<SaleDTO>();
        }
    }
}