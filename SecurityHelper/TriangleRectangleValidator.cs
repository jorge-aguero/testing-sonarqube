namespace SecurityHelper
{
    internal class TriangleRectangleValidator
    {
        private readonly ILogger _logger;

        public TriangleRectangleValidator(ILogger logger)
        {
            _logger = logger;
        }

        public bool IsTriangleRectangle(double sideA, double sideB, double diagonnal)
        {
            _logger.LogInformation("Request made to IsTriangleRectangle: {SideA}, {SideB} and {Diagonnal}", sideA, sideB, diagonnal);
            return Math.Pow(sideA, 2) + Math.Pow(sideB, 2) == Math.Pow(diagonnal, 2);
        }
    }
}
