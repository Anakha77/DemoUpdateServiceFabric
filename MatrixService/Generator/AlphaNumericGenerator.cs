namespace Matrix.Service.Generator
{
    class AlphaNumericGenerator : IGenerator
    {
        protected const string Possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        public string Generate()
        {
            return Possible;
        }
    }
}
