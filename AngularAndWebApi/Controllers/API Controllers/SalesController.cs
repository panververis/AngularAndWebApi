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

namespace AngularAndWebApi.Controllers.API_Controllers
{
    public class SalesController : ApiController
    {

        // Initializing the DBContext
        private AngularAndWebApiContext _DB = new AngularAndWebApiContext();

        /////////////////////////// API Exposed Methods
        // GET: api/Sales
        #region  Get (All) Sales

        /// <summary>
        /// API method getting all the Sales in an IQueryable
        /// </summary>
        public IQueryable<Sale> GetSales() {

            // Returning the DBContext's Sales
            return _DB.Sales;
        }

        #endregion

        // GET: api/Sales/5
        #region Get (Specific) Sale (by ID)

        /// <summary>
        /// API Method getting a specific Sale by the provided as input ID
        /// </summary>
        [ResponseType(typeof(Sale))]
        public async Task<IHttpActionResult> GetSale(int ID) {

            // Attempting to fetch the Sale
            Sale sale = await _DB.Sales.FindAsync(ID);

            // If not fetched, return a NotFoundResult
            if (sale == null) {
                return NotFound();
            }

            // Return an OkResult, with the Sale
            return Ok(sale);
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
        ///////////////////////////

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