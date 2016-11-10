using System;

namespace Matrix.Service.Service
{
    class MatrixGenerator : IMatrixGenerator
    {
        protected const string Possible = " ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        public string Generate(int nbChars)
        {
            var text = "";
            var r = new Random();
            for (var i = 0; i < r.Next(nbChars); i++)
            {
                text += Possible[r.Next(Possible.Length)];
            }
            return text;
        }
    }
}
