using System.Collections.Generic;

namespace AngularAndWebApi.Models
{
    public class Area
    {
        public int                   ID      { get; set; }
        public string                Name    { get; set; }

        // "Detail" collection property (==> to StaffMembers)
        public virtual  List<Region> Regions { get; set; }
    }
}