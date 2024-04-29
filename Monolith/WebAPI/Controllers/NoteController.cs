using System.Web.Http;

namespace WebAPI.Controllers
{
    public class NoteController : ApiController
    {
        public class NoteRequest
        {
            public string Title { get; set; }
        }

        [HttpPost]
        public IHttpActionResult Create(NoteRequest note)
        {
            return this.Ok();
        }
        
    }
}