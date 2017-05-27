using System;

namespace AngularAndWebApi.Models.DTOs
{
    public class VehicleDTO
    {

        //// Fields
        public int      id              { get; set; }
        public string   model           { get; set; }
        public Int16    makeYear        { get; set; }
        public string   chassisNumber   { get; set; }
        public Int16    engineCapacity  { get; set; }

        ////// Getter only fields
        /// <summary>
        /// Public object getter-only field returning the specific DTO's "Engine Capacity" property
        /// in a user - friendly manner
        /// </summary>
        public string engineCapacityDisplayText {
            get {
                string engineCapacityString = Convert.ToString(this.engineCapacity);
                return $"{engineCapacityString} {Resources.sCC}";
            }
        }

    }
}