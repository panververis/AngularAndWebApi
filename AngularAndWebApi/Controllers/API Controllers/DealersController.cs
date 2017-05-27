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
    public class DealersController : ApiController
    {

        // Initializing the DBContext
        private AngularAndWebApiContext _DB = new AngularAndWebApiContext();

        // GET: api/Dealers
        #region Get (All) Dealers

        /// <summary>
        /// API method getting all the Dealers (in DTOs) in an IQueryable
        /// </summary>
        public IQueryable<DealerDTO> GetDealers() {

            // Getting all of the DbContext's Dealers in a IQueryable of DealerDTOs
            IQueryable<DealerDTO> dealers = _DB.Dealers
                                            .Select(x => new DealerDTO() {
                                                id      = x.ID,
                                                name    = x.Name
                                            });

            // Returning the DBContext's Dealers
            return dealers;
        }

        #endregion

        // GET: api/DealersAndDetails
        #region Get (All) Sales and their Details

        /// <summary>
        /// API method getting all the Dealers and their Details (Staff) (in DTOs) in an IQueryable,
        /// utilizing Web.Api2 Attributes Routing
        /// </summary>
        [Route("api/dealersanddetails")]
        public IQueryable<DealerDTO> GetDealersAndDetails() {

            // Getting all of the DbContext's Dealers in a IQueryable of DealerDTOs
            IQueryable<DealerDTO> dealers = _DB
                                        .Dealers
                                            .Select(x => new DealerDTO() {
                                                id = x.ID,
                                                name = x.Name,
                                                staffMembers = x.StaffMembers
                                                                   .Select(y => new StaffDTO() {
                                                                       id = y.ID,
                                                                       firstName = y.FirstName,
                                                                       lastName = y.LastName,
                                                                       jobType = y.JobType
                                                                   }).ToList()
                                            });

            // Returning the BooksDTOs' IQueryable
            return dealers;
        }

        #endregion

        // GET: api/Dealers/5
        #region Get (Specific) Dealer (by ID)

        /// <summary>
        /// API Method getting a specific Dealer (in DTO) by the provided as input ID
        /// </summary>
        [ResponseType(typeof(DealerDTO))]
        public async Task<IHttpActionResult> GetDealer(int ID) {

            // Attempting to fetch the Area
            DealerDTO dealerDTO = await _DB
                                       .Dealers
                                        .Select(x => new DealerDTO() {
                                            id = x.ID,
                                            name = x.Name
                                        }).FirstOrDefaultAsync(x => x.id == ID);

            // If not fetched, return a NotFoundResult
            if (dealerDTO == null)
            {
                return NotFound();
            }

            // Return an OkResult, with the Dealer
            return Ok(dealerDTO);
        }

        #endregion

        // GET: api/DealersAndDetails/5
        #region Get (Specific) Dealer and its Details (Staff Member) (by ID)

        /// <summary>
        /// API Method getting a specific Dealer (in DTO) along with its Details (Staff Members), by the provided as input ID
        /// </summary>
        [Route("api/dealersanddetails/{ID}")]
        [ResponseType(typeof(DealerDTO))]
        public async Task<IHttpActionResult> GetDealersAndDetails(int ID) {

            // Attempting to fetch the Dealer
            DealerDTO dealerDTO = await _DB
                                       .Dealers
                                            .Select(x => new DealerDTO() {
                                                id = x.ID,
                                                name = x.Name,
                                                staffMembers = x.StaffMembers
                                                                       .Select(y => new StaffDTO() {
                                                                           id = y.ID,
                                                                           firstName = y.FirstName,
                                                                           lastName = y.LastName,
                                                                           jobType = y.JobType
                                                                       }).ToList()
                                            }).FirstOrDefaultAsync(x => x.id == ID);

            // If not fetched, return a NotFoundResult
            if (dealerDTO == null) {
                return NotFound();
            }

            // Return an OkResult, with the Dealer
            return Ok(dealerDTO);
        }

        #endregion

        // PUT: api/Dealers/5
        #region Put a Dealer

        /// <summary>
        /// API Method Putting a Dealer (identified by ID)
        /// </summary>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDealer(int ID, Dealer Dealer) {

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
        public async Task<IHttpActionResult> PostDealer(Dealer Dealer) {

            // In case the ModelState is not valid, return a BadRequestResult
            if (!ModelState.IsValid) {
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
        /// API Method Deleting a Dealer
        /// </summary>
        [ResponseType(typeof(Dealer))]
        public async Task<IHttpActionResult> DeleteDealer(int ID) {

            // Fetching the referenced Dealer
            Dealer dealer = await _DB.Dealers.FindAsync(ID);

            // In case this Dealer could not be found, return a NotFoundResult
            if (dealer == null) {
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
        /// Private helper method checking whether a specific Dealer (identified by ID) exists
        /// </summary>
        private bool DealerExists(int ID) {
            return _DB.Dealers.Count(e => e.ID == ID) > 0;
        }

        #endregion

    }
}