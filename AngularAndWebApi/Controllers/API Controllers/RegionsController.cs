using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AngularAndWebApi.Models;
using AngularAndWebApi.Models.DTOs;

namespace AngularAndWebApi.Controllers.API_Controllers
{
    public class RegionsController : ApiController
    {

        // Initializing the DBContext
        private AngularAndWebApiContext _DB = new AngularAndWebApiContext();

        // GET: api/Regions
        #region  Get (All) Regions

        /// <summary>
        /// API method getting all the Regions (in DTOs) in an IQueryable
        /// </summary>
        public IQueryable<RegionDTO> GetRegions() {

            // Getting all of the DbContext's Regions in a IQueryable of RegionDTOs
            IQueryable<RegionDTO> regions =
                                _DB.Regions
                                    .Select(x => new RegionDTO() {
                                        id      = x.ID,
                                        name    = x.Name
                                    });

            // Returning the RegionDTOs' IQueryable
            return regions;
        }

        #endregion

        // GET: api/RegionsAndDetails
        #region Get (All) Regions and their related info

        /// <summary>
        /// API method getting all the Regions and their "Parent" Area's info (in DTOs) in an IQueryable,
        /// utilizing Web.Api2 Attributes Routing
        /// </summary>
        [Route("api/regionsanddetails")]
        public IQueryable<RegionDTO> GetRegionsAndDetails() {

            // Getting all of the DbContext's Regions in a IQueryable of RegionDTOs
            IQueryable<RegionDTO> regions = _DB
                                        .Regions
                                            .Select(x => new RegionDTO() {
                                                id = x.ID,
                                                name = x.Name,
                                                areaID = x.Area.ID,
                                                areaName = x.Area.Name
                                            });

            // Returning the RegionDTOs' IQueryable
            return regions;
        }

        #endregion

        // GET: api/Regions/5
        #region Get (Specific) Region (by ID)

        /// <summary>
        /// API Method getting a specific Region (in DTO) by the provided as input ID
        /// </summary>
        [ResponseType(typeof(RegionDTO))]
        public async Task<IHttpActionResult> GetRegion(int ID) {

            // Attempting to fetch the Region
            RegionDTO regionDTO = await _DB
                                       .Regions
                                        .Select(x => new RegionDTO() {
                                            id = x.ID,
                                            name = x.Name
                                        }).FirstOrDefaultAsync(x => x.id == ID);

            // If not fetched, return a NotFoundResult
            if (regionDTO == null) {
                return NotFound();
            }

            // Return an OkResult, with the Region
            return Ok(regionDTO);
        }

        #endregion

        // GET: api/RegionsAndDetails/5
        #region Get (Specific) Area and its Details (Regions) (by ID)

        /// <summary>
        /// API Method getting a specific Region (in DTO) along with its related info (Area), by the provided as input ID
        /// </summary>
        [Route("api/regionsanddetails/{ID}")]
        [ResponseType(typeof(RegionDTO))]
        public async Task<IHttpActionResult> GetRegionAndDetails(int ID) {

            // Attempting to fetch the Region
            RegionDTO regionDTO = await _DB
                                       .Regions
                                        .Select(x => new RegionDTO() {
                                            id = x.ID,
                                            name = x.Name,
                                            areaID = x.Area.ID,
                                            areaName = x.Area.Name
                                        }).FirstOrDefaultAsync(x => x.id == ID);

            // If not fetched, return a NotFoundResult
            if (regionDTO == null) {
                return NotFound();
            }

            // Return an OkResult, with the Region
            return Ok(regionDTO);
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