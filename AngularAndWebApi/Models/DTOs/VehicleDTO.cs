using System;

namespace AngularAndWebApi.Models.DTOs
{
    public class VehicleDTO
    {

        //// Fields
        public int      ID              { get; set; }
        public string   Model           { get; set; }
        public Int16    MakeYear        { get; set; }
        public string   ChassisNumber   { get; set; }
        public Int16    EngineCapacity  { get; set; }

        ////// Getter only fields
        /// <summary>
        /// Public object getter-only field returning the specific DTO's "Engine Capacity" property
        /// in a user - friendly manner
        /// </summary>
        public string EngineCapacityDisplayText {
            get {
                string engineCapacityString = Convert.ToString(this.EngineCapacity);
                return $"{engineCapacityString} {Resources.sCC}";
            }
        }

    }
}