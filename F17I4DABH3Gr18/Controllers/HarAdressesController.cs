using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using F17I4DABH3Gr18.Models;

namespace F17I4DABH3Gr18.Controllers
{
    public class HarAdressesController : ApiController
    {
        private readonly PersonKartoket db = new PersonKartoket();

        // GET: api/HarAdresses
        public IQueryable<HarAdresseDTO> GetHarAdresses()
        {
            var harAdresses = from ad in db.HarAdresses
                select new HarAdresseDTO
                {
                    AdresseId = ad.AdresseId,
                    PersonId = ad.PersonId
                };

            return harAdresses;
        }

        // GET: api/HarAdresses/5
        /// <summary>
        /// This operation is used to retrieve all extra adresses for a person. Note: does not include the primary/home adresse
        /// </summary>
        /// <param name="id">id of the person</param>
        /// <returns>List of extra adresses</returns>
        [ResponseType(typeof(List<HarAdresseDetailDTO>))]
        public async Task<IHttpActionResult> GetHarAdresse(long id)
        {
            var harAdresse = await db.HarAdresses.Select(ad =>
                new HarAdresseDetailDTO()
                {
                    AdresseId = ad.AdresseId,
                    PersonId = ad.PersonId,
                    Type = ad.Type
                }).Where(ad => ad.PersonId == id).ToListAsync();


            if (harAdresse == null)
                return NotFound();

            return Ok(harAdresse);
        }

        // PUT: api/HarAdresses/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutHarAdresse(long id, HarAdresse harAdresse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != harAdresse.AdresseId)
                return BadRequest();

            db.Entry(harAdresse).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HarAdresseExists(id))
                    return NotFound();
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/HarAdresses
        [ResponseType(typeof(HarAdresse))]
        public async Task<IHttpActionResult> PostHarAdresse(HarAdresse harAdresse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.HarAdresses.Add(harAdresse);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HarAdresseExists(harAdresse.AdresseId))
                    return Conflict();
                throw;
            }

            return CreatedAtRoute("DefaultApi", new {id = harAdresse.AdresseId}, harAdresse);
        }

        // DELETE: api/HarAdresses/5
        [ResponseType(typeof(HarAdresse))]
        public async Task<IHttpActionResult> DeleteHarAdresse(long id)
        {
            var harAdresse = await db.HarAdresses.FindAsync(id);
            if (harAdresse == null)
                return NotFound();

            db.HarAdresses.Remove(harAdresse);
            await db.SaveChangesAsync();

            return Ok(harAdresse);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }

        private bool HarAdresseExists(long id)
        {
            return db.HarAdresses.Count(e => e.AdresseId == id) > 0;
        }
    }
}