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
    public class AdressesController : ApiController
    {
        private PersonKartoket db = new PersonKartoket();

        // GET: api/Adresses
        public IQueryable<AdresseDTO> GetAdresses()
        {
            var adresses = from ad in db.Adresses
                           select new AdresseDTO()
                           {
                               Id = ad.AdresseId,
                               PostNr = ad.PostNr,
                               Vejnavn = ad.Vejnavn
                           };

            return adresses;
        }

        // GET: api/Adresses/5
        [ResponseType(typeof(AdresseDetailDTO))]
        public async Task<IHttpActionResult> GetAdresse(long id)
        {
            var adresse = await db.Adresses.Include(ad => ad.HarAdresses).Select(ad => 
            new AdresseDetailDTO()
            {
                Id = ad.AdresseId,
                Bynavn = ad.Bynavn,
                HusNr = ad.HusNr,
                PostNr = ad.PostNr,
                Type = ad.Type,
                Vejnavn = ad.Vejnavn
            }).SingleOrDefaultAsync(ad => ad.Id == id);


            if (adresse == null)
                return NotFound();

            return Ok(adresse);
        }

        // PUT: api/Adresses/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAdresse(long id, Adresse adresse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != adresse.AdresseId)
            {
                return BadRequest();
            }

            db.Entry(adresse).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdresseExists(id))
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

        // POST: api/Adresses
        [ResponseType(typeof(Adresse))]
        public async Task<IHttpActionResult> PostAdresse(Adresse adresse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Adresses.Add(adresse);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AdresseExists(adresse.AdresseId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = adresse.AdresseId }, adresse);
        }

        // DELETE: api/Adresses/5
        [ResponseType(typeof(Adresse))]
        public async Task<IHttpActionResult> DeleteAdresse(long id)
        {
            Adresse adresse = await db.Adresses.FindAsync(id);
            if (adresse == null)
            {
                return NotFound();
            }

            db.Adresses.Remove(adresse);
            await db.SaveChangesAsync();

            return Ok(adresse);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdresseExists(long id)
        {
            return db.Adresses.Count(e => e.AdresseId == id) > 0;
        }
    }
}