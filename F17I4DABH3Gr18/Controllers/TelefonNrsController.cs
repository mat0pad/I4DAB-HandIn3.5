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
using F17I4DABH3Gr18.Models;

namespace F17I4DABH3Gr18.Controllers
{
    public class TelefonNrsController : ApiController
    {
        private PersonKartoket db = new PersonKartoket();

        // GET: api/TelefonNrs
        /// <summary>
        /// This operation is used to retrieve all telefons
        /// </summary>
        /// <returns>List of telefons</returns>
        public IQueryable<TelefonNrDTO> GetTelefonNrs()
        {
            var TelefonNrs = from ad in db.TelefonNrs
                             select new TelefonNrDTO()
                           {
                               Id = ad.TelefonId,
                               PersonId = ad.PersonId
                           };
            return TelefonNrs;
        }

        // GET: api/TelefonNrs/5
        /// <summary>
        /// This operation is used to retrieve all telefons details for person
        /// </summary>
        /// <param name="id">The id of the person</param>
        /// <returns>List of telefon details</returns>
        [ResponseType(typeof(List<TelefonNrDetailDTO>))]
        public async Task<IHttpActionResult> GetTelefonNr(long id)
        {

            var telefonNr = await db.TelefonNrs.Select(ad =>
                new TelefonNrDetailDTO()
                {
                    Id = ad.TelefonId,
                    PersonId = ad.PersonId,
                    Type = ad.Type,
                    Nummer = ad.Nummer
                }).Where(ad => ad.PersonId == id).ToListAsync();

            if (telefonNr == null)
                return NotFound();
            
            return Ok(telefonNr);
        }

        // PUT: api/TelefonNrs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTelefonNr(long id, TelefonNr telefonNr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != telefonNr.TelefonId)
            {
                return BadRequest();
            }

            db.Entry(telefonNr).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelefonNrExists(id))
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

        // POST: api/TelefonNrs
        [ResponseType(typeof(TelefonNr))]
        public async Task<IHttpActionResult> PostTelefonNr(TelefonNr telefonNr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TelefonNrs.Add(telefonNr);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TelefonNrExists(telefonNr.TelefonId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = telefonNr.TelefonId }, telefonNr);
        }

        // DELETE: api/TelefonNrs/5
        [ResponseType(typeof(TelefonNr))]
        public async Task<IHttpActionResult> DeleteTelefonNr(long id)
        {
            TelefonNr telefonNr = await db.TelefonNrs.FindAsync(id);
            if (telefonNr == null)
            {
                return NotFound();
            }

            db.TelefonNrs.Remove(telefonNr);
            await db.SaveChangesAsync();

            return Ok(telefonNr);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TelefonNrExists(long id)
        {
            return db.TelefonNrs.Count(e => e.TelefonId == id) > 0;
        }
    }
}