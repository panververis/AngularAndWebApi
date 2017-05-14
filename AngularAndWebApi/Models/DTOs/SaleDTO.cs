using System;

namespace AngularAndWebApi.Models.DTOs
{
    public class SaleDTO
    {
        public int      ID                      { get; set; }
        public DateTime SaleDate                { get; set; }
        public decimal  SaleValue               { get; set; }

        public int      VehicleID               { get; set; }
        public string   VehicleModel            { get; set; }
        public Int16    VehicleMakeYear         { get; set; }
        public string   VehicleChassisNumber    { get; set; }
        public Int16    VehicleEngineCapacity   { get; set; }

        public int      StaffID                 { get; set; }
        public string   StaffFirstName          { get; set; }
        public string   StaffLastName           { get; set; }

        public int      DealerID                { get; set; }
        public string   DealerName              { get; set; }
    }
}