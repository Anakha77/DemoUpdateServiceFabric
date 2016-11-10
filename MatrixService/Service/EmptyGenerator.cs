namespace Matrix.Service.Service
{
    class EmptyGenerator : IMatrixGenerator
    {
        public string Generate(int nbChars)
        {
            return string.Empty;
        }
    }
}
