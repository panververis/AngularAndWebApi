using System;

namespace AngularAndWebApi.Models.DTOs
{
    public class VehicleDTO
    {
        public int      ID              { get; set; }
        public string   Model           { get; set; }
        public Int16    MakeYear        { get; set; }
        public string   ChassisNumber   { get; set; }
        public Int16    EngineCapacity  { get; set; }
    }
}