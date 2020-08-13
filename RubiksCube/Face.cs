namespace RubiksCube
{
    public readonly struct Face
    {
        public Colour TopLeft { get; }
        public Colour TopMiddle { get; }
        public Colour TopRight { get; }
        public Colour MiddleLeft { get; }
        public Colour MiddleMiddle { get; }
        public Colour MiddleRight { get; }
        public Colour BottomLeft { get; }
        public Colour BottomMiddle { get; }
        public Colour BottomRight { get; }

        public Face(Colour colour) : this(colour, colour, colour, colour, colour, colour, colour, colour, colour)
        {
        }

        public Face(Colour topLeft,
                    Colour topMiddle,
                    Colour topRight,
                    Colour middleLeft,
                    Colour middleMiddle,
                    Colour middleRight,
                    Colour bottomLeft,
                    Colour bottomMiddle,
                    Colour bottomRight)
        {
            TopLeft = topLeft;
            TopMiddle = topMiddle;
            TopRight = topRight;
            MiddleLeft = middleLeft;
            MiddleMiddle = middleMiddle;
            MiddleRight = middleRight;
            BottomLeft = bottomLeft;
            BottomMiddle = bottomMiddle;
            BottomRight = bottomRight;
        }

        public Face WithTopRow(Colour left, Colour middle, Colour right)
        {
            return new Face(left, middle, right,
                            MiddleLeft, MiddleMiddle, MiddleRight,
                            BottomLeft, BottomMiddle, BottomRight);
        }
        public Face WithBottomRow(Colour left, Colour middle, Colour right)
        {
            return new Face(TopLeft, TopMiddle, TopRight,
                            MiddleLeft, MiddleMiddle, MiddleRight,
                            left, middle, right);
        }

        public Face WithRightColumn(Colour top, Colour middle, Colour bottom)
        {
            return new Face(TopLeft, TopMiddle, top,
                            MiddleLeft, MiddleMiddle, middle,
                            BottomLeft, BottomMiddle, bottom);
        }

        public Face WithLeftColumn(Colour top, Colour middle, Colour bottom)
        {
            return new Face(top, TopMiddle, TopRight,
                            middle, MiddleMiddle, MiddleRight,
                            bottom, BottomMiddle, BottomRight);
        }
    }
}
