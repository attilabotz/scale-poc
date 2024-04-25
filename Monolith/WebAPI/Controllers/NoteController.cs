using System.Web.Http;

namespace WebAPI.Controllers
{
    public class NoteController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Create([FromBody] string title)
        {
            return this.Ok();
        }
        
    }
}