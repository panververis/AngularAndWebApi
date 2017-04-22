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

        // GET: api/Regions
        public IQueryable<Region> GetRegions()
        {
            return _DB.Regions;
        }

        // GET: api/Regions/5
        [ResponseType(typeof(Region))]
        public async Task<IHttpActionResult> GetRegion(int id)
        {
            Region region = await _DB.Regions.FindAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            return Ok(region);
        }

        // PUT: api/Regions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRegion(int id, Region region)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != region.ID)
            {
                return BadRequest();
            }

            _DB.Entry(region).State = EntityState.Modified;

            try
            {
                await _DB.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Regions
        [ResponseType(typeof(Region))]
        public async Task<IHttpActionResult> PostRegion(Region region)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _DB.Regions.Add(region);
            await _DB.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = region.ID }, region);
        }

        // DELETE: api/Regions/5
        [ResponseType(typeof(Region))]
        public async Task<IHttpActionResult> DeleteRegion(int id)
        {
            Region region = await _DB.Regions.FindAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            _DB.Regions.Remove(region);
            await _DB.SaveChangesAsync();

            return Ok(region);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _DB.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RegionExists(int id)
        {
            return _DB.Regions.Count(e => e.ID == id) > 0;
        }
    }
}