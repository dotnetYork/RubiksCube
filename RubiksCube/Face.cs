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

        public Face(Colour colour)
            : this(colour, colour, colour, colour, colour, colour, colour, colour, colour)
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

        internal Face WithTopRow(Row row)
        {
            return new Face(row.Left, row.Middle, row.Right,
                            MiddleLeft, MiddleMiddle, MiddleRight,
                            BottomLeft, BottomMiddle, BottomRight);
        }
        internal Face WithBottomRow(Row row)
        {
            return new Face(TopLeft, TopMiddle, TopRight,
                            MiddleLeft, MiddleMiddle, MiddleRight,
                            row.Left, row.Middle, row.Right);
        }

        internal Face WithRightColumn(Column column)
        {
            return new Face(TopLeft, TopMiddle, column.Top,
                            MiddleLeft, MiddleMiddle, column.Middle,
                            BottomLeft, BottomMiddle, column.Bottom);
        }

        internal Face WithLeftColumn(Column column)
        {
            return new Face(column.Top, TopMiddle, TopRight,
                column.Middle, MiddleMiddle, MiddleRight,
                column.Bottom, BottomMiddle, BottomRight);
        }

        internal Row GetTopRow()
        {
            return new Row(TopLeft, TopMiddle, TopRight);
        }


        internal Row GetBottomRow()
        {
            return new Row(BottomLeft, BottomMiddle, BottomRight);
        }
        
        internal Column GetLeftColumn()
        {
            return new Column(TopLeft, MiddleLeft, BottomLeft);
        }

        internal Column GetRightColumn()
        {
            return new Column(TopRight, MiddleRight, BottomRight);
        }
    }
}
