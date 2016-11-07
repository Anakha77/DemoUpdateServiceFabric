namespace MatrixService.Service
{
    class EmptyGenerator : IMatrixGenerator
    {
        public string Generate(int nbChars)
        {
            return string.Empty;
        }
    }
}
