using System;

namespace AngularAndWebApi.Models.DTOs
{
    public class SaleDTO
    {
        public int      id                      { get; set; }
        public DateTime saleDate                { get; set; }
        public decimal  saleValue               { get; set; }

        public int      vehicleID               { get; set; }
        public string   vehicleModel            { get; set; }
        public Int16    vehicleMakeYear         { get; set; }
        public string   vehicleChassisNumber    { get; set; }
        public Int16    vehicleEngineCapacity   { get; set; }

        public int      staffID                 { get; set; }
        public string   staffFirstName          { get; set; }
        public string   staffLastName           { get; set; }

        public int      dealerID                { get; set; }
        public string   dealerName              { get; set; }
    }
}