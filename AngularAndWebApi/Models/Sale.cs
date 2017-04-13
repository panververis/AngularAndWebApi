using System;

namespace AngularAndWebApi.Models
{
    public class Sale
    {
        public int              ID          { get; set; }
        public DateTime         SaleDate    { get; set; }
        public decimal          SaleValue   { get; set; }

        // Foreign Key (==> to Vehicle)
        public int              VehicleID   { get; set; }
        // Navigation property (==> to Vehicle)
        public virtual Vehicle  Vehicle     { get; set; }

        // Foreign Key (==> to Staff)
        public int              StaffID     { get; set; }
        // Navigation property (==> to Staff)
        public virtual Staff    Staff       { get; set; }

        // Foreign Key (==> to Dealer)
        public int              DealerID    { get; set; }
        // Navigation property (==> to Dealer)
        public virtual Dealer   Dealer      { get; set; }

    }
}