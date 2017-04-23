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
    public class AreasController : ApiController
    {

        // Initializing the DBContext
        private AngularAndWebApiContext _DB = new AngularAndWebApiContext();

        #region API Exposed Methods

        // GET: api/Areas
        #region Get (All) Areas

        /// <summary>
        /// API method getting all the Areas (in DTOs) in an IQueryable
        /// </summary>
        public IQueryable<AreaDTO> GetAreas() {

            // Getting all of the DbContext's Books in a IQueryable of BooksDTOs
            IQueryable<AreaDTO> books = _DB.Areas.Select(x => new AreaDTO() { ID = x.ID, Name = x.Name });

            // Returning the BooksDTOs' IQueryable
            return books;
        }

        #endregion

        // GET: api/Areas/5
        #region Get (Specific) Area (by ID)

        /// <summary>
        /// API Method getting a specific Area by the provided as input ID
        /// </summary>
        [ResponseType(typeof(Area))]
        public async Task<IHttpActionResult> GetArea(int ID) {

            // Attempting to fetch the Area
            Area area = await _DB.Areas.FindAsync(ID);

            // If not fetched, return a NotFoundResult
            if (area == null) {
                return NotFound();
            }

            // Return an OkResult, with the Area
            return Ok(area);
        }

        #endregion

        // PUT: api/Areas/5
        #region Put an Area

        /// <summary>
        /// API Method Putting an Area (identified by ID)
        /// </summary>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutArea(int ID, Area Area) {

            // If the ModelState is not valid, return a BadRequestResult
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            // Validation: If the provided as input ID and the Area's to be put are not matching,
            // return a BadRequestResult
            if (ID != Area.ID) {
                return BadRequest();
            }

            // Mark the Area's State as modified
            _DB.Entry(Area).State = EntityState.Modified;

            // Attempt to Save any changes made
            try {
                await _DB.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {

                // If the referenced Area does not exist, return a NotFoundResult
                if (!AreaExists(ID)) {
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

        // POST: api/Areas
        #region Post an Area

        /// <summary>
        /// API Method posting an Area
        /// </summary>
        [ResponseType(typeof(Area))]
        public async Task<IHttpActionResult> PostArea(Area Area)
        {
            // In case the ModelState is not valid, return a BadRequestResult
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            // Add the new Area to the DBContext's Areas
            _DB.Areas.Add(Area);

            // Attempt to Save the Change
            await _DB.SaveChangesAsync();

            // Return a CreatedAtRouteResult
            return CreatedAtRoute("DefaultApi", new { id = Area.ID }, Area);
        }

        #endregion

        // DELETE: api/Areas/5
        #region Delete an Area

        /// <summary>
        /// API Method Deleting an Area
        /// </summary>
        [ResponseType(typeof(Area))]
        public async Task<IHttpActionResult> DeleteArea(int ID)
        {
            // Fetching the referenced Area
            Area area = await _DB.Areas.FindAsync(ID);

            // In case this Area could not be found, return a NotFoundResult
            if (area == null) {
                return NotFound();
            }

            // Remove the referenced Area from the DBContext's Areas
            _DB.Areas.Remove(area);

            // Attempt to Save the Changes
            await _DB.SaveChangesAsync();

            // Return an OkResult yielding the referenced Area
            return Ok(area);
        }

        #endregion

        #endregion

        #region Overrides

        protected override void Dispose(bool Disposing)
        {
            if (Disposing) {
                _DB.Dispose();
            }
            base.Dispose(Disposing);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Private helper method checking whether a specific Area (identified by ID) exists
        /// </summary>
        private bool AreaExists(int ID)  {
            return _DB.Areas.Count(e => e.ID == ID) > 0;
        }

        #endregion

    }
}