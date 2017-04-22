using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AngularAndWebApi.Models;

namespace AngularAndWebApi.Controllers.API_Controllers
{
    public class RegionsController : ApiController
    {

        // Initializing the DBContext
        private AngularAndWebApiContext _DB = new AngularAndWebApiContext();

        /////////////////////////// API Exposed Methods
        // GET: api/Regions
        #region  Get (All) Regions

        /// <summary>
        /// API method getting all the Regions in an IQueryable
        /// </summary>
        public IQueryable<Region> GetRegions() {

            // Returning the DBContext's Regions
            return _DB.Regions;
        }

        #endregion

        // GET: api/Regions/5
        #region Get (Specific) Region (by ID)

        /// <summary>
        /// API Method getting a specific Region by the provided as input ID
        /// </summary>
        [ResponseType(typeof(Region))]
        public async Task<IHttpActionResult> GetRegion(int ID) {

            // Attempting to fetch the Region
            Region region = await _DB.Regions.FindAsync(ID);

            // If not fetched, return a NotFoundResult
            if (region == null) {
                return NotFound();
            }

            // Return an OkResult, with the Region
            return Ok(region);
        }

        #endregion

        // PUT: api/Regions/5
        #region Put a Region

        /// <summary>
        /// API Method Putting a Region (identified by ID)
        /// </summary>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRegion(int ID, Region Region) {

            // If the ModelState is not valid, return a BadRequestResult
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            // Validation: If the provided as input ID and the Region's to be put are not matching,
            // return a BadRequestResult
            if (ID != Region.ID) {
                return BadRequest();
            }

            // Mark the Region's State as "Modified"
            _DB.Entry(Region).State = EntityState.Modified;

            // Attempt to Save any changes made
            try {
                await _DB.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {

                // If the referenced Region does not exist, return a NotFoundResult
                if (!RegionExists(ID)) {
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

        // POST: api/Regions
        #region Post a Region

        /// <summary>
        /// API Method posting a Region
        /// </summary>
        [ResponseType(typeof(Region))]
        public async Task<IHttpActionResult> PostRegion(Region Region) {

            // In case the ModelState is not valid, return a BadRequestResult
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            // Add the new Region to the DBContext's Regions
            _DB.Regions.Add(Region);

            // Attempt to Save the Change
            await _DB.SaveChangesAsync();

            // Return a CreatedAtRouteResult
            return CreatedAtRoute("DefaultApi", new { id = Region.ID }, Region);
        }

        #endregion

        // DELETE: api/Regions/5
        #region Delete a Region

        /// <summary>
        /// API Method Deleting a Region
        /// /// </summary>
        [ResponseType(typeof(Region))]
        public async Task<IHttpActionResult> DeleteRegion(int ID) {

            // Fetching the referenced Region
            Region region = await _DB.Regions.FindAsync(ID);

            // In case this Region could not be found, return a NotFoundResult
            if (region == null) {
                return NotFound();
            }

            // Remove the referenced Region from the DBContext's Regions
            _DB.Regions.Remove(region);

            // Attempt to Save the Changes
            await _DB.SaveChangesAsync();

            // Return an OkResult yielding the referenced Region
            return Ok(region);
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
        /// Private helper method checking whether a specific Region (identified by ID) exists
        /// </summary>
        private bool RegionExists(int ID) {
            return _DB.Regions.Count(e => e.ID == ID) > 0;
        }

        #endregion

    }
}