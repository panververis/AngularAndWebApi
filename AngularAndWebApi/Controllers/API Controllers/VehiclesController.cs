using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AngularAndWebApi.Models;
using AngularAndWebApi.Models.DTOs;

namespace AngularAndWebApi.Controllers.API_Controllers {

    public class VehiclesController : ApiController {

        // Initializing the DBContext
        private AngularAndWebApiContext _DB = new AngularAndWebApiContext();

        // GET: api/Vehicles
        #region  Get (All) Vehicles

        /// <summary>
        /// API method getting all the Vehicles (in DTOs) in an IQueryable
        /// </summary>
        public IQueryable<VehicleDTO> GetVehicles() {

            // Getting all of the DbContext's Vehicles in a IQueryable of VehicleDTOs
            IQueryable<VehicleDTO> vehicles = 
                                _DB.Vehicles
                                    .Select(x => new VehicleDTO() {
                                        ID = x.ID,
                                        Model = x.Model,
                                        MakeYear = x.MakeYear,
                                        ChassisNumber = x.ChassisNumber,
                                        EngineCapacity = x.EngineCapacity
                                    });

            // Returning the VehicleDTOs' IQueryable
            return vehicles;
        }

        #endregion

        // GET: api/Vehicles/5
        #region Get (Specific) Vehicle (by ID)

        /// <summary>
        /// API Method getting a specific Vehicle (in DTO) by the provided as input ID
        /// </summary>
        [ResponseType(typeof(VehicleDTO))]
        public async Task<IHttpActionResult> GetVehicle(int ID) {

            // Attempting to fetch the Vehicle
            VehicleDTO vehicleDTO = await _DB
                                       .Vehicles
                                        .Select(x => new VehicleDTO() {
                                            ID = x.ID,
                                            Model = x.Model,
                                            MakeYear = x.MakeYear,
                                            ChassisNumber = x.ChassisNumber,
                                            EngineCapacity = x.EngineCapacity
                                        }).FirstOrDefaultAsync(x => x.ID == ID);

            // If not fetched, return a NotFoundResult
            if (vehicleDTO == null) {
                return NotFound();
            }

            // Return an OkResult, with the Vehicle
            return Ok(vehicleDTO);
        }

        #endregion

        // PUT: api/Vehicles/5
        #region Put a Vehicle

        /// <summary>
        /// API Method Putting a Vehicle (identified by ID)
        /// </summary>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVehicle(int ID, Vehicle Vehicle) {

            // If the ModelState is not valid, return a BadRequestResult
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            // Validation: If the provided as input ID and the Vehicle's to be put are not matching,
            // return a BadRequestResult
            if (ID != Vehicle.ID) {
                return BadRequest();
            }

            // Mark the Vehicle's State as "Modified"
            _DB.Entry(Vehicle).State = EntityState.Modified;

            // Attempt to Save any changes made
            try {
                await _DB.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)  {
                // If the referenced Vehicle does not exist, return a NotFoundResult
                if (!VehicleExists(ID)) {
                    return NotFound();
                }

                // ..else simply throw
                else {
                    throw;
                }
            }

            // Return a Status Code of "No Content", as no Content need be returned
            return StatusCode(HttpStatusCode.NoContent);
        }

        #endregion

        // POST: api/Vehicles
        #region Post a Vehicle

        /// <summary>
        /// API Method posting a Vehicle
        /// </summary>
        [ResponseType(typeof(Vehicle))]
        public async Task<IHttpActionResult> PostVehicle(Vehicle Vehicle) {

            // In case the ModelState is not valid, return a BadRequestResult
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            // Add the new Vehicle to the DBContext's Vehicles
            _DB.Vehicles.Add(Vehicle);

            // Attempt to Save the Change
            await _DB.SaveChangesAsync();

            // Return a CreatedAtRouteResult
            return CreatedAtRoute("DefaultApi", new { id = Vehicle.ID }, Vehicle);
        }

        #endregion

        // DELETE: api/Vehicles/5
        #region Delete a Vehicle

        /// <summary>
        /// API Method Deleting a Vehicle
        /// </summary>
        [ResponseType(typeof(Vehicle))]
        public async Task<IHttpActionResult> DeleteVehicle(int ID) {

            // Fetching the referenced Vehicle
            Vehicle vehicle = await _DB.Vehicles.FindAsync(ID);

            // In case this Vehicle could not be found, return a NotFoundResult
            if (vehicle == null) {
                return NotFound();
            }

            // Remove the referenced Vehicle from the DBContext's Vehicles
            _DB.Vehicles.Remove(vehicle);

            // Attempt to Save the Changes
            await _DB.SaveChangesAsync();

            // Return an OkResult yielding the referenced Vehicle
            return Ok(vehicle);
        }

        #endregion

        #region Overrides

        protected override void Dispose(bool Disposing) {
            if (Disposing) {
                _DB.Dispose();
            }
            base.Dispose(Disposing);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Private helper method checking whether a specific  Vehicle (identified by ID) exists
        /// </summary>
        private bool VehicleExists(int ID){
            return _DB.Vehicles.Count(e => e.ID == ID) > 0;
        }

        #endregion

    }
}