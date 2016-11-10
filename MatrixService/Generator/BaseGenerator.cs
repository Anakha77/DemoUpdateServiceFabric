namespace Matrix.Service.Generator
{
    class BaseGenerator : IMatrixGenerator
    {
        protected const string Possible = " ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        public string Generate(int nbChars)
        {
            return Possible;
        }
    }
}
