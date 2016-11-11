using System;

namespace Matrix.Service.Generator
{
    class RandomGenerator : IGenerator
    {
        protected const string Possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        public string Generate()
        {
            var text = "";
            const int nbChars = 128;

            var r = new Random();
            for (var i = 0; i < nbChars; i++)
            {
                text += Possible[r.Next(Possible.Length)];
            }
            return text;
        }
    }
}
