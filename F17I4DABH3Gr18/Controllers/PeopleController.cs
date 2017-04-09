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
    public class PeopleController : ApiController
    {
        private PersonKartoket db = new PersonKartoket();

        // GET: api/People
        /// <summary>
        /// This operation retrives a list of all persons
        /// </summary>
        /// <returns>List of all persons</returns>
        public IQueryable<PersonDTO> GetPeople()
        {
            var persons = from ad in db.People
                             select new PersonDTO()
                             {
                                 Id = ad.PersonId,
                                 Fornavn = ad.Fornavn,
                                 AdresseId = ad.AdresseId,
                                 Efternavn = ad.Efternavn
                             };

            return persons;
        }

        // GET: api/People/5
        /// <summary>
        /// This operation is used to retrive detail informations about a single person
        /// </summary>
        /// <param name="id">id of person</param>
        /// <returns>Detail person object</returns>
        [ResponseType(typeof(PersonDetailDTO))]
        public async Task<IHttpActionResult> GetPerson(long id)
        {
            var person = await db.People
                .Select(ad =>
            new PersonDetailDTO()
            {
                Id = ad.PersonId,
                Fornavn = ad.Fornavn,
                Efternavn = ad.Efternavn,
                PrimaryAdressId = ad.AdresseId,
                Type = ad.Type,
                Mellemnavn = ad.Mellemnavn
            }).SingleOrDefaultAsync(ad => ad.Id == id);

            person.TelefonNrs = await db.TelefonNrs.Select(ad =>
                new TelefonNrDTO()
                {
                    Id = ad.TelefonId,
                    PersonId = ad.PersonId
                }).Where(ad => ad.PersonId == id).ToListAsync();

            person.HarAdresse = await db.HarAdresses.Select(ad =>
               new HarAdresseDTO()
               {
                   AdresseId = ad.AdresseId,
                   PersonId = ad.PersonId
               }).Where(ad => ad.PersonId == id).ToListAsync();

            return Ok(person);
        }

        // PUT: api/People/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPerson(long id, Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.PersonId)
            {
                return BadRequest();
            }

            db.Entry(person).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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

        // POST: api/People
        [ResponseType(typeof(Person))]
        public async Task<IHttpActionResult> PostPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.People.Add(person);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PersonExists(person.PersonId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = person.PersonId }, person);
        }

        // DELETE: api/People/5
        [ResponseType(typeof(Person))]
        public async Task<IHttpActionResult> DeletePerson(long id)
        {
            Person person = await db.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            db.People.Remove(person);
            await db.SaveChangesAsync();

            return Ok(person);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonExists(long id)
        {
            return db.People.Count(e => e.PersonId == id) > 0;
        }
    }
}