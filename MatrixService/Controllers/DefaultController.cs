using System;
using System.Web.Http;
using Matrix.Service.Generator;

namespace Matrix.Service.Controllers
{
    public class DefaultController : ApiController
    {
        private readonly IMatrixGenerator _generator;

        public DefaultController()
        {
            //_generator = new EmptyGenerator();
            _generator = new MatrixGenerator();
            //_generator = new BaseGenerator();
        }

        [HttpGet]
        [Route("Get")]
        public string Get()
        {
            var r = new Random();
            return Get(r.Next(128));
        }

        [HttpGet]
        [Route("Get/{id}")]
        public string Get(int id)
        {
            return _generator.Generate(id);
        }
    }
}
