using mmdb.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace mmdb.Controllers
{
    [RoutePrefix("api/stats")]
    public class StatsController : ApiController
    {
        private mmdbContainer db = new mmdbContainer();

        // GET api/<controller>
        [HttpGet]
        [Route("TopGenres")]
        public IEnumerable<object> TopGenres()
        {

            return db.Movies.GroupBy(mg => mg.Genre).Select(m => new { Genre = m.Key, Count = m.Count() }).OrderByDescending(x => x.Count).Take(3);
        }
        [HttpGet]
        [Route("Genres")]
        public IEnumerable<object> Genres()
        {
            return db.Movies.GroupBy(m => m.Genre).Select(mov => new { Genre = mov.Key }).OrderBy(o => o.Genre) ;
        }
        [HttpGet]
        [Route("TopActors")]
        public IEnumerable<ActorStats> TopActors()
        {
            return db.Database.SqlQuery<ActorStats>("[Get_Top_Actors]");
        }

        [HttpPost]
        [Route("ActorsByGenre/{genre}")]
        public IEnumerable<ActorStats> ActorsByGenre(string genre)
        {
            return db.Database.SqlQuery<ActorStats>("[Get_Actors_By_Genre] @genre", new SqlParameter("genre", genre));
        }
    }
}