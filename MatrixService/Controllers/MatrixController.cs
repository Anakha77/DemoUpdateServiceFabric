using System;
using System.Web.Http;
using Matrix.Service.Service;

namespace Matrix.Service.Controllers
{
    [ServiceRequestActionFilter]
    public class MatrixController : ApiController
    {
        private readonly IMatrixGenerator _generator;

        public MatrixController()
        {
            //_generator = new EmptyGenerator();
            _generator = new MatrixGenerator();
            //_generator = new BaseGenerator();
        }

        // GET api/matrix
        public string Get()
        {
            var r = new Random();
            return Get(r.Next(128));
        }

        // GET api/matrix/5 
        public string Get(int id)
        {
            return _generator.Generate(id);
        }
    }
}
