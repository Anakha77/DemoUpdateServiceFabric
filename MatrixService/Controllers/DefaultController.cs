using System.Net.Http;
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
            //_generator = new RandomGenerator();
            //_generator = new AlphaNumericGenerator();
            _generator = new BoolGenerator();
        }

        [HttpGet]
        [Route("Get")]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage
            {
                Content = new StringContent(_generator.Generate())
            };
        }
    }
}
