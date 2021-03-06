﻿using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AngularAndWebApi.Models;
using AngularAndWebApi.Models.DTOs;
using System;
using System.Diagnostics;

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
                                        id = x.ID,
                                        model = x.Model,
                                        makeYear = x.MakeYear,
                                        chassisNumber = x.ChassisNumber,
                                        engineCapacity = x.EngineCapacity
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
                                            id = x.ID,
                                            model = x.Model,
                                            makeYear = x.MakeYear,
                                            chassisNumber = x.ChassisNumber,
                                            engineCapacity = x.EngineCapacity
                                        }).FirstOrDefaultAsync(x => x.id == ID);

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
        [ResponseType(typeof(VehicleDTO))]
        public async Task<IHttpActionResult> DeleteVehicle(int ID) {

            // Fetching the referenced Vehicle
            Vehicle vehicle = await _DB.Vehicles.FindAsync(ID);

            // In case this Vehicle could not be found, return a NotFoundResult
            if (vehicle == null) {
                return NotFound();
            }

            // At this point perform the necessary pre-delete validations
            bool canBeDeleted = PerformBeforeDeleteValidations(vehicle);
            if (!canBeDeleted) {
                Exception cannotBeDeletedException = new Exception(Resources.sVehicleCannotBeDeletedReferencedElsewhereInSystem);
                Debug.WriteLine(cannotBeDeletedException.Message);

                // Return an InternalServerError
                return InternalServerError(cannotBeDeletedException);
            }

            // Remove the referenced Vehicle from the DBContext's Vehicles
            _DB.Vehicles.Remove(vehicle);

            // Attempt to Save the Changes.
            // In case of an Exception, return an appropriate result
            try {
                await _DB.SaveChangesAsync();
            } catch (Exception Ex) {

                // Getting the Inner Exception, to display the proper exception message
                Exception exception = Ex.InnerException != null
                                        ? Ex.InnerException.InnerException != null
                                            ? Ex.InnerException.InnerException
                                            : Ex.InnerException
                                        : Ex;

                // Also append the Exception message to the Debug.Listeners collection
                // ==> that is currently the Console / "Output"
                Debug.WriteLine(exception.Message);

                // Lastly, return an InternalServerError
                return InternalServerError(exception);
            }

            // Getting a VehicleDTO to send to the client
            VehicleDTO vehicleDTO = new VehicleDTO() {
                id              = vehicle.ID,
                model           = vehicle.Model,
                makeYear        = vehicle.MakeYear,
                chassisNumber   = vehicle.ChassisNumber,
                engineCapacity  = vehicle.EngineCapacity
            };

            // Return an OkResult yielding the referenced Vehicle
            return Ok(vehicleDTO);
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

        /// <summary>
        /// Private helper method checking whether the provided as input Vehicle can be deleted
        /// </summary>
        private bool PerformBeforeDeleteValidations(Vehicle Vehicle) {
            if (Vehicle == null) {
                return false;
            }

            #region Check for Sales referencing the to-be-deleted Vehicle

            // Getting an IQueryable on the Sales referencing this vehicle
            IQueryable<Sale> relatedSalesQueryable = _DB.Sales.Where(x => x.VehicleID == Vehicle.ID);
            if (relatedSalesQueryable == null) {
                return false;
            }
            if (relatedSalesQueryable.Any()) {
                return false;
            }

            #endregion

            // Lastly, if all before-delete checks have passed, return true
            return true;
        }

        #endregion

    }
}