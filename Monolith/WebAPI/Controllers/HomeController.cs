using System.Web.Http;

namespace WebAPI.Controllers
{
    public class HomeController : ApiController
    {
        
        [HttpGet]
        public IHttpActionResult Index()
        {
            return this.Json("Hello from WebAPI!");
        }
        
       
    }
}