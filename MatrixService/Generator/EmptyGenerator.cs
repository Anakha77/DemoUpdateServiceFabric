namespace Matrix.Service.Generator
{
    class EmptyGenerator : IMatrixGenerator
    {
        public string Generate(int nbChars)
        {
            return string.Empty;
        }
    }
}
