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
    public class MovieActorsController : ApiController
    {
        private mmdbContainer db = new mmdbContainer();

        // POST: api/MovieActors
        [ResponseType(typeof(MovieActor))]
        public IHttpActionResult PostMovieActor(MovieActor movieActor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MovieActors.Add(movieActor);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MovieActorExists(movieActor.MovieId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = movieActor.MovieId }, movieActor);
        }

        // DELETE: api/MovieActors/5
        [HttpDelete]
        [Route("api/movieactors/{MovieId}/{ActorId}")]
        [ResponseType(typeof(MovieActor))]
        public IHttpActionResult DeleteMovieActor(int MovieId, int ActorId)
        {
            MovieActor movieActorCheck = db.MovieActors.Where(m => m.ActorId == ActorId && m.MovieId == MovieId).FirstOrDefault();
            if (movieActorCheck == null)
            {
                return NotFound();
            }

            db.MovieActors.Remove(movieActorCheck);
            db.SaveChanges();

            return Ok(movieActorCheck);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MovieActorExists(int id)
        {
            return db.MovieActors.Count(e => e.MovieId == id) > 0;
        }
    }
}