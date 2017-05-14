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

    public class StaffController : ApiController {

        // Initializing the DBContext
        private AngularAndWebApiContext _DB = new AngularAndWebApiContext();

        // GET: api/Staff
        #region  Get (All) Staff

        /// <summary>
        /// API method getting all the Staff (in DTOs) in an IQueryable
        /// </summary>
        public IQueryable<StaffDTO> GetAllStaff() {

            // Getting all of the DbContext's Staff in a IQueryable of StaffDTOs
            IQueryable<StaffDTO> staff = _DB.Staffs
                                                .Select(x => new StaffDTO() {
                                                                ID = x.ID,
                                                                FirstName = x.FirstName,
                                                                LastName = x.LastName,
                                                                JobType = x.JobType
                                                });

            // Returning the StaffDTOs' IQueryable
            return staff;
        }

        #endregion

        // GET: api/StaffAndDetails
        #region  Get (All) Staff and their Details

        /// <summary>
        /// API method getting all the Staff and their Details (Sales) and their "Parent" Dealer's info (in DTOs) in an IQueryable,
        /// utilizing Web.Api2 Attributes Routing
        /// </summary>
        [Route("api/staffanddetails")]
        public IQueryable<StaffDTO> GetStaffAndDetails() {

            // Getting all of the DbContext's Staff in a IQueryable of StaffDTOs
            IQueryable<StaffDTO> staff = _DB
                                        .Staffs
                                            .Include(x => x.Sales)
                                                .Select(x => new StaffDTO() {
                                                    ID              = x.ID,
                                                    FirstName       = x.FirstName,
                                                    LastName        = x.LastName,
                                                    JobType         = x.JobType,
                                                    DealerID        = x.Dealer.ID,
                                                    DealerName      = x.Dealer.Name,
                                                    Sales           = x.Sales.Select(y => new SaleDTO() {
                                                                                            ID = y.ID,
                                                                                            SaleDate = y.SaleDate,
                                                                                            SaleValue = y.SaleValue})
                                                                                                .ToList()
                                                });

            // Returning the StaffDTOs' IQueryable
            return staff;
        }

        #endregion

        // GET: api/Staff/5
        #region Get (Specific) Staff member (by ID)

        /// <summary>
        /// API Method getting a specific Staff member (in DTO) by the provided as input ID
        /// </summary>
        [ResponseType(typeof(StaffDTO))]
        public async Task<IHttpActionResult> GetStaff(int ID) {

            // Attempting to fetch the Staff member
            StaffDTO staffDTO = await _DB
                                       .Staffs
                                        .Select(x => new StaffDTO() {
                                            ID          = x.ID,
                                            FirstName   = x.FirstName,
                                            LastName    = x.LastName
                                        }).FirstOrDefaultAsync(x => x.ID == ID);

            // If not fetched, return a NotFoundResult
            if (staffDTO == null) {
                return NotFound();
            }

            // Return an OkResult, with the Staff member
            return Ok(staffDTO);
        }

        #endregion

        // GET: api/StaffAndDetails/5
        #region Get (Specific) Staff member and its Details (Sales) (by ID)

        /// <summary>
        /// API Method getting a specific Staff member (in DTO) along with its Details (Sales), by the provided as input ID
        /// </summary>
        [Route("api/staffanddetails/{ID}")]
        [ResponseType(typeof(StaffDTO))]
        public async Task<IHttpActionResult> GetStaffAndDetails(int ID) {

            // Attempting to fetch the Staff member
            StaffDTO staffDTO = await _DB
                                       .Staffs
                                        .Select(x => new StaffDTO() {
                                            ID = x.ID,
                                            FirstName = x.FirstName,
                                            LastName = x.LastName,
                                            JobType = x.JobType,
                                            Sales = x.Sales
                                                         .Select(y => new SaleDTO() {
                                                             ID = y.ID,
                                                             SaleDate = y.SaleDate,
                                                             SaleValue = y.SaleValue
                                                         }).ToList()
                                        }).FirstOrDefaultAsync(x => x.ID == ID);

            // If not fetched, return a NotFoundResult
            if (staffDTO == null) {
                return NotFound();
            }

            // Return an OkResult, with the StaffDTO
            return Ok(staffDTO);
        }

        #endregion

        // PUT: api/Staff/5
        #region Put a Staff member

        /// <summary>
        /// API Method Putting a Staff member (identified by ID)
        /// </summary>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStaff(int ID, Staff Staff) {

            // If the ModelState is not valid, return a BadRequestResult
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            // Validation: If the provided as input ID and the Staff member's to be put are not matching,
            // return a BadRequestResult
            if (ID != Staff.ID) {
                return BadRequest();
            }

            // Mark the Staff member's State as "Modified"
            _DB.Entry(Staff).State = EntityState.Modified;

            // Attempt to Save any changes made
            try  {
                await _DB.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                // If the referenced Staff member does not exist, return a NotFoundResult
                if (!StaffExists(ID)) {
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

        // POST: api/Staff
        #region Post a Staff member

        /// <summary>
        /// API Method posting a Staff member
        /// </summary>
        [ResponseType(typeof(Staff))]
        public async Task<IHttpActionResult> PostStaff(Staff Staff) {

            // In case the ModelState is not valid, return a BadRequestResult
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            // Add the new Staff member to the DBContext's Staff
            _DB.Staffs.Add(Staff);

            // Attempt to Save the Change
            await _DB.SaveChangesAsync();

            // Return a CreatedAtRouteResult
            return CreatedAtRoute("DefaultApi", new { id = Staff.ID }, Staff);
        }

        #endregion

        // DELETE: api/Staff/5
        #region Delete a Staff member

        /// <summary>
        /// API Method Deleting a Staff member
        /// </summary>
        [ResponseType(typeof(Staff))]
        public async Task<IHttpActionResult> DeleteStaff(int ID) {

            // Fetching the referenced Staff member
            Staff staff = await _DB.Staffs.FindAsync(ID);

            // In case this Staff member could not be found, return a NotFoundResult
            if (staff == null) {
                return NotFound();
            }

            // Remove the referenced Staff member from the DBContext's Staff
            _DB.Staffs.Remove(staff);

            // Attempt to Save the Changes
            await _DB.SaveChangesAsync();

            // Return an OkResult yielding the referenced Staff member
            return Ok(staff);
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
        /// Private helper method checking whether a specific Staff member (identified by ID) exists
        /// </summary>
        private bool StaffExists(int ID) {
            return _DB.Staffs.Count(e => e.ID == ID) > 0;
        }

        #endregion

    }
}