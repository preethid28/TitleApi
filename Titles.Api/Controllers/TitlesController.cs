using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Titles.Api.EF;

namespace Titles.Api.Controllers
{
    [RoutePrefix("api")]
    public class TitlesController : ApiController
    {
        private TitlesEntities db = new TitlesEntities();

        // GET: api/Titles
        [Route("titles")]
        public IQueryable<Title> GetTitles()
        {   
            return db.Titles;
        }

        // GET: api/Titles/5
        //[HttpGet]
        [Route("details/{id:int}")]
        [ResponseType(typeof(Title))]
        public IHttpActionResult GetTitle(int id)
        {
            Title title = db.Titles.Find(id);

            if (title == null)
            {
                return NotFound();
            }
            db.Entry(title).Collection(x => x.Awards).Load();
            db.Entry(title).Collection(x => x.OtherNames).Load();
            db.Entry(title).Collection(x => x.StoryLines).Load();
            db.Entry(title).Collection(x => x.TitleGenres).Query().Include(x=>x.Genre).Load();
            db.Entry(title).Collection(x => x.TitleParticipants).Query().Include(x => x.Participant).Load();
            return Ok(title);
        }

        // PUT: api/Titles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTitle(int id, Title title)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != title.TitleId)
            {
                return BadRequest();
            }

            db.Entry(title).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TitleExists(id))
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

        // POST: api/Titles
        [ResponseType(typeof(Title))]
        public IHttpActionResult PostTitle(Title title)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Titles.Add(title);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TitleExists(title.TitleId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = title.TitleId }, title);
        }

        // DELETE: api/Titles/5
        [ResponseType(typeof(Title))]
        public IHttpActionResult DeleteTitle(int id)
        {
            Title title = db.Titles.Find(id);
            if (title == null)
            {
                return NotFound();
            }

            db.Titles.Remove(title);
            db.SaveChanges();

            return Ok(title);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TitleExists(int id)
        {
            return db.Titles.Count(e => e.TitleId == id) > 0;
        }
    }
}