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
    public class MoviesController : ApiController
    {
        private mmdbContainer db = new mmdbContainer();

        // GET: api/Movies
        public IQueryable<object> GetMovies()
        {
            return db.Movies.Select(m => new { m.MovieId, m.Title,m.Genre,m.Poster,m.ReleaseDate,
                m.Description,
                m.Rating,
                Actors = db.Actors.Join(db.MovieActors, a => a.ActorId, ma => ma.ActorId, (a, ma) => new { Actor = a, MovieActor = ma })
                .Where(mova => mova.MovieActor.MovieId == m.MovieId)
                .Select(act => act.Actor) });
           
        }

        // GET: api/Movies/5
        [ResponseType(typeof(object))]
        public IHttpActionResult GetMovie(int id)
        {
            object movie = db.Movies.Select(m => new
            {
                m.MovieId,
                m.Title,
                m.Genre,
                m.Poster,
                m.ReleaseDate,
                m.Description,
                m.Rating,
                Actors = db.Actors.Join(db.MovieActors, a => a.ActorId, ma => ma.ActorId, (a, ma) => new { Actor = a, MovieActor = ma })
                 .Where(mova => mova.MovieActor.MovieId == m.MovieId)
                 .Select(act => act.Actor)
            }).Where(mov => mov.MovieId == id).FirstOrDefault();

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        // PUT: api/Movies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMovie(int id, Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movie.MovieId)
            {
                return BadRequest();
            }

            db.Entry(movie).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
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

        // POST: api/Movies
        [ResponseType(typeof(Movie))]
        public IHttpActionResult PostMovie(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Movies.Add(movie);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = movie.MovieId }, movie);
        }

        // DELETE: api/Movies/5
        [ResponseType(typeof(Movie))]
        public IHttpActionResult DeleteMovie(int id)
        {
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }

            db.Movies.Remove(movie);
            db.SaveChanges();

            return Ok(movie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MovieExists(int id)
        {
            return db.Movies.Count(e => e.MovieId == id) > 0;
        }
    }
}