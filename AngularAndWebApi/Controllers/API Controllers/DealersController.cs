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
        private AngularAndWebApiContext db = new AngularAndWebApiContext();

        // GET: api/Dealers
        public IQueryable<Dealer> GetDealers()
        {
            return db.Dealers;
        }

        // GET: api/Dealers/5
        [ResponseType(typeof(Dealer))]
        public async Task<IHttpActionResult> GetDealer(int id)
        {
            Dealer dealer = await db.Dealers.FindAsync(id);
            if (dealer == null)
            {
                return NotFound();
            }

            return Ok(dealer);
        }

        // PUT: api/Dealers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDealer(int id, Dealer dealer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dealer.ID)
            {
                return BadRequest();
            }

            db.Entry(dealer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DealerExists(id))
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

        // POST: api/Dealers
        [ResponseType(typeof(Dealer))]
        public async Task<IHttpActionResult> PostDealer(Dealer dealer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Dealers.Add(dealer);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = dealer.ID }, dealer);
        }

        // DELETE: api/Dealers/5
        [ResponseType(typeof(Dealer))]
        public async Task<IHttpActionResult> DeleteDealer(int id)
        {
            Dealer dealer = await db.Dealers.FindAsync(id);
            if (dealer == null)
            {
                return NotFound();
            }

            db.Dealers.Remove(dealer);
            await db.SaveChangesAsync();

            return Ok(dealer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DealerExists(int id)
        {
            return db.Dealers.Count(e => e.ID == id) > 0;
        }
    }
}