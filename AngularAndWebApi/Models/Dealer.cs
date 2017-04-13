using System.Collections.Generic;

namespace AngularAndWebApi.Models
{
    public class Dealer
    {
        public int                  ID           { get; set; }
        public string               Name         { get; set; }

        // Foreign Key (==> to Region)
        public int                  RegionID     { get; set; }
        // Navigation property (==> to Region)
        public virtual  Region      Region       { get; set; }

        // "Detail" collection property (==> to StaffMembers)
        public virtual  List<Staff> StaffMembers { get; set; }

        // "Detail" collection property (==> to Sales)
        public virtual  List<Sale>  Sales        { get; set; }
    }
}