

using System.Web.Http;
using System.Web.Http.Results;

namespace WebAPI.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public string Get()
        {
            return "Hello from WebAPI!";
        }
        
       
    }
}