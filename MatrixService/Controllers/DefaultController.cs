using System.Web.Http;
using Matrix.Service.Generator;

namespace Matrix.Service.Controllers
{
    public class DefaultController : ApiController
    {
        private readonly IGenerator _generator;

        public DefaultController()
        {
            //_generator = new EmptyGenerator();
            _generator = new RandomGenerator();
            //_generator = new AlphaNumericGenerator();
        }

        [HttpGet]
        [Route("Get")]
        public string Get()
        {
            return _generator.Generate();
        }
    }
}
