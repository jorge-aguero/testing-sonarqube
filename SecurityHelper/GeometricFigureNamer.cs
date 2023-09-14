namespace SecurityHelper
{
    internal class GeometricFigureNamer
    {
        public NamedGeometricFigure NameGeometricFigure(NameGeometricFigureRequest request)
        {
            return new NamedGeometricFigure
            {
                Sides = request.Sides,
                Description = MapSideQuantityToFigureName(request.Sides)
            };
        }

        private static string MapSideQuantityToFigureName(int sides) => ((GeometricFigures)sides).ToString();
    }
}
