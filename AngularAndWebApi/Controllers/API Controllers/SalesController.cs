using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AngularAndWebApi.Models;
using AngularAndWebApi.Models.DTOs;

namespace AngularAndWebApi.Controllers.API_Controllers
{
    public class SalesController : ApiController
    {

        // Initializing the DBContext
        private AngularAndWebApiContext _DB = new AngularAndWebApiContext();

        // GET: api/Sales
        #region  Get (All) Sales

        /// <summary>
        /// API method getting all the Sales (in DTOs) in an IQueryable
        /// </summary>
        public IQueryable<SaleDTO> GetSales() {

            // Getting all of the DbContext's Sales in a IQueryable of SalesDTOs
            IQueryable<SaleDTO> sales = _DB.Sales
                                            .Select(x => new SaleDTO() {
                                                ID = x.ID,
                                                SaleDate = x.SaleDate,
                                                SaleValue = x.SaleValue});

            // Returning the DBContext's Sales
            return sales;
        }

        #endregion

        // GET: api/SalesAndDetails
        #region Get (All) Sales and their Details

        /// <summary>
        /// API method getting all the Sales and their Details (Vehicles / Staff / Dealers) (in DTOs) in an IQueryable,
        /// utilizing Web.Api2 Attributes Routing
        /// </summary>
        [Route("api/salesanddetails")]
        public IQueryable<SaleDTO> GetSalesAndDetails() {

            // Getting all of the DbContext's Sales in a IQueryable of BooksDTOs
            IQueryable<SaleDTO> sales = _DB
                                        .Sales
                                            .Select(x => new SaleDTO() {
                                                ID                      = x.ID,
                                                SaleDate                = x.SaleDate,
                                                SaleValue               = x.SaleValue,
                                                VehicleID               = x.Vehicle.ID,
                                                VehicleModel            = x.Vehicle.Model,
                                                VehicleMakeYear         = x.Vehicle.MakeYear,
                                                VehicleChassisNumber    = x.Vehicle.ChassisNumber,
                                                VehicleEngineCapacity   = x.Vehicle.EngineCapacity,
                                                StaffID                 = x.Staff.ID,
                                                StaffFirstName          = x.Staff.FirstName,
                                                StaffLastName           = x.Staff.LastName,
                                                DealerID                = x.Dealer.ID,
                                                DealerName              = x.Dealer.Name
                                            });

            // Returning the BooksDTOs' IQueryable
            return sales;
        }

        #endregion

        // GET: api/Sales/5
        #region Get (Specific) Sale (by ID)

        /// <summary>
        /// API Method getting a specific Sale (in DTO) by the provided as input ID
        /// </summary>
        [ResponseType(typeof(SaleDTO))]
        public async Task<IHttpActionResult> GetSale(int ID) {

            // Attempting to fetch the Area
            SaleDTO saleDTO = await _DB
                                       .Sales
                                        .Select(x => new SaleDTO() {
                                            ID          = x.ID,
                                            SaleDate    = x.SaleDate,
                                            SaleValue   = x.SaleValue
                                        }).FirstOrDefaultAsync(x => x.ID == ID);

            // If not fetched, return a NotFoundResult
            if (saleDTO == null) {
                return NotFound();
            }

            // Return an OkResult, with the Sale
            return Ok(saleDTO);
        }

        #endregion

        // GET: api/SalesAndDetails/5
        #region Get (Specific) Sales and its Details (Vehicles / Staff / Dealer) (by ID)

        /// <summary>
        /// API Method getting a specific Sale (in DTO) along with its Details (Vehicles / Staff / Dealer), by the provided as input ID
        /// </summary>
        [Route("api/salesanddetails/{ID}")]
        [ResponseType(typeof(SaleDTO))]
        public async Task<IHttpActionResult> GetSalesAndDetails(int ID) {

            // Attempting to fetch the Sale
            SaleDTO saleDTO = await _DB
                                       .Sales
                                        .Select(x => new SaleDTO() {
                                            ID                      = x.ID,
                                            SaleDate                = x.SaleDate,
                                            SaleValue               = x.SaleValue,
                                            VehicleID               = x.Vehicle.ID,
                                            VehicleModel            = x.Vehicle.Model,
                                            VehicleMakeYear         = x.Vehicle.MakeYear,
                                            VehicleChassisNumber    = x.Vehicle.ChassisNumber,
                                            VehicleEngineCapacity   = x.Vehicle.EngineCapacity,
                                            StaffID                 = x.Staff.ID,
                                            StaffFirstName          = x.Staff.FirstName,
                                            StaffLastName           = x.Staff.LastName,
                                            DealerID                = x.Dealer.ID,
                                            DealerName              = x.Dealer.Name

                                        }).FirstOrDefaultAsync(x => x.ID == ID);

            // If not fetched, return a NotFoundResult
            if (saleDTO == null) {
                return NotFound();
            }

            // Return an OkResult, with the Sale
            return Ok(saleDTO);
        }

        #endregion

        // PUT: api/Sales/5
        #region Put a Sale

        /// <summary>
        /// API Method Putting a Sale (identified by ID)
        /// </summary>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSale(int ID, Sale Sale) {

            // If the ModelState is not valid, return a BadRequestResult
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            // Validation: If the provided as input ID and the Sale's to be put are not matching,
            // return a BadRequestResult
            if (ID != Sale.ID) {
                return BadRequest();
            }

            // Mark the Sale's State as "Modified"
            _DB.Entry(Sale).State = EntityState.Modified;

            // Attempt to Save any changes made
            try {
                await _DB.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                // If the referenced Sale does not exist, return a NotFoundResult
                if (!SaleExists(ID)) {
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

        // POST: api/Sales
        #region Post a Sale

        /// <summary>
        /// API Method posting a Sale
        /// </summary>
        [ResponseType(typeof(Sale))]
        public async Task<IHttpActionResult> PostSale(Sale Sale) {

            // In case the ModelState is not valid, return a BadRequestResult
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            // Add the new Sale to the DBContext's Sales
            _DB.Sales.Add(Sale);

            // Attempt to Save the Change
            await _DB.SaveChangesAsync();

            // Return a CreatedAtRouteResult
            return CreatedAtRoute("DefaultApi", new { id = Sale.ID }, Sale);
        }

        #endregion

        // DELETE: api/Sales/5
        #region Delete a Sale

        /// <summary>
        /// API Method Deleting a Sale
        /// </summary>
        [ResponseType(typeof(Sale))]
        public async Task<IHttpActionResult> DeleteSale(int ID) {

            // Fetching the referenced Sale
            Sale sale = await _DB.Sales.FindAsync(ID);

            // In case this Sale could not be found, return a NotFoundResult
            if (sale == null)  {
                return NotFound();
            }

            // Remove the referenced Sale from the DBContext's Sales
            _DB.Sales.Remove(sale);

            // Attempt to Save the Changes
            await _DB.SaveChangesAsync();

            // Return an OkResult yielding the referenced Region
            return Ok(sale);
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
        /// Private helper method checking whether a specific Sale (identified by ID) exists
        /// </summary>
        private bool SaleExists(int ID) {
            return _DB.Sales.Count(e => e.ID == ID) > 0;
        }

        #endregion

    }
}