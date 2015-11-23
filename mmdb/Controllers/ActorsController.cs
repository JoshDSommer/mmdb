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
using mmdb.Models;

namespace mmdb.Controllers
{
    [RoutePrefix("api/actors")]
    public class ActorsController : ApiController
    {
        private mmdbContainer db = new mmdbContainer();

        // GET: api/Actors
        public IQueryable<object> GetActors()
        {
            return db.Actors.Select(actor => new
            {
                actor.ActorId,
                actor.Name,
                Movies = db.Movies.Join(db.MovieActors, m => m.MovieId, ma => ma.MovieId, (m, ma) => new { Movie = m, MovieActor = ma })
                 .Where(mova => mova.MovieActor.ActorId == actor.ActorId)
                 .Select(act => act.Movie)
            });
        }

        // GET: api/Actors/5
        [ResponseType(typeof(object))]
        public IHttpActionResult GetActor(string id)
        {
            object actor = db.Actors.Select(act => new
            {
                act.ActorId,
                act.Name,
                Movies = db.Movies.Join(db.MovieActors, m => m.MovieId, ma => ma.MovieId, (m, ma) => new { Movie = m, MovieActor = ma })
                 .Where(mova => mova.MovieActor.ActorId == act.ActorId)
                 .Select(a => a.Movie)
            }).Where(a => a.Name.ToLower() == id.ToLower()).FirstOrDefault();

    
            return Ok(actor);
        }

        // PUT: api/Actors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutActor(int id, Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != actor.ActorId)
            {
                return BadRequest();
            }

            db.Entry(actor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(id))
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

        // POST: api/Actors
        [ResponseType(typeof(Actor))]
        public IHttpActionResult PostActor(Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Actors.Add(actor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = actor.ActorId }, actor);
        }

        // DELETE: api/Actors/5
        [ResponseType(typeof(Actor))]
        public IHttpActionResult DeleteActor(int id)
        {
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return NotFound();
            }

            db.Actors.Remove(actor);
            db.SaveChanges();

            return Ok(actor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActorExists(int id)
        {
            return db.Actors.Count(e => e.ActorId == id) > 0;
        }
    }
}