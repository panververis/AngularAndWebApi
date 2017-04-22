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
    public class DealersController : ApiController
    {

        // Initializing the DBContext
        private AngularAndWebApiContext _DB = new AngularAndWebApiContext();

        /////////////////////////// API Exposed Methods
        // GET: api/Dealers
        #region Get (All) Dealers

        /// <summary>
        /// API method getting all the Dealers in an IQueryable
        /// </summary>
        public IQueryable<Dealer> GetDealers() {

            // Returning the DBContext's Areas
            return _DB.Dealers;
        }

        #endregion

        // GET: api/Dealers/5
        #region Get (Specific) Dealer (by ID)

        /// <summary>
        /// API Method getting a specific Dealer by the provided as input ID
        /// </summary>
        [ResponseType(typeof(Dealer))]
        public async Task<IHttpActionResult> GetDealer(int ID)
        {
            // Attempting to fetch the Dealer
            Dealer dealer = await _DB.Dealers.FindAsync(ID);

            // If not fetched, return a NotFoundResult
            if (dealer == null)
            {
                return NotFound();
            }

            // Return an OkResult, with the Dealer
            return Ok(dealer);
        }

        #endregion

        // PUT: api/Dealers/5
        #region Put a Dealer

        /// <summary>
        /// API Method Putting a Dealer (identified by ID)
        /// </summary>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDealer(int ID, Dealer Dealer)
        {

            // If the ModelState is not valid, return a BadRequestResult
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validation: If the provided as input ID and the Dealer to be put are not matching,
            // return a BadRequestResult
            if (ID != Dealer.ID)
            {
                return BadRequest();
            }

            // Mark the Dealer's State as modified
            _DB.Entry(Dealer).State = EntityState.Modified;

            // Attempt to Save any changes made
            try {
                await _DB.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {

                // If the referenced Dealer does not exist, return a NotFoundResult
                if (!DealerExists(ID)) {
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

        // POST: api/Dealers
        #region Post a Dealer

        /// <summary>
        /// API Method posting a Dealer
        /// </summary>
        [ResponseType(typeof(Dealer))]
        public async Task<IHttpActionResult> PostDealer(Dealer Dealer)
        {
            // In case the ModelState is not valid, return a BadRequestResult
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Add the new Dealer to the DBContext's Dealers
            _DB.Dealers.Add(Dealer);

            // Attempt to Save the Change
            await _DB.SaveChangesAsync();

            // Return a CreatedAtRouteResult
            return CreatedAtRoute("DefaultApi", new { id = Dealer.ID }, Dealer);
        }

        #endregion

        // DELETE: api/Dealers/5
        #region Delete a Dealer

        /// <summary>
        /// API Method Deleting an Area
        /// </summary>
        [ResponseType(typeof(Dealer))]
        public async Task<IHttpActionResult> DeleteDealer(int ID)
        {

            // Fetching the referenced Dealer
            Dealer dealer = await _DB.Dealers.FindAsync(ID);

            // In case this Dealer could not be found, return a NotFoundResult
            if (dealer == null)
            {
                return NotFound();
            }

            // Remove the referenced Dealer from the DBContext's Dealers
            _DB.Dealers.Remove(dealer);

            // Attempt to Save the Changes
            await _DB.SaveChangesAsync();

            // Return an OkResult yielding the referenced Dealer
            return Ok(dealer);
        }

        #endregion
        ///////////////////////////

        #region Overrides

        protected override void Dispose(bool Disposing)
        {
            if (Disposing)
            {
                _DB.Dispose();
            }
            base.Dispose(Disposing);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Private helper method checking whether a specific Dealer (identified by ID) exists
        /// </summary>
        private bool DealerExists(int ID)
        {
            return _DB.Dealers.Count(e => e.ID == ID) > 0;
        }

        #endregion

    }
}