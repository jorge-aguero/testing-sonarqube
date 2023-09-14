namespace SecurityHelper
{
    internal class NameGeometricFigureRequest
    {
        private int _sides;
        public int Sides
        {
            get => _sides;
            set
            {
                if (value < 3 || value > 6)
                    throw new ArgumentOutOfRangeException(nameof(Sides));

                _sides = value;
            }
        }
    }
}
