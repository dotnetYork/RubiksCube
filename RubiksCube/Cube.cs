namespace RubiksCube
{
    //   T
    // L F R
    //   B
    //   b

    public class Cube
    {
        public Cube(Face front, Face back, Face left, Face right, Face top, Face bottom)
            : this(front, back, left, right, top, bottom, 0)
        {

        }
        private Cube(Face front, Face back, Face left, Face right, Face top, Face bottom, uint rotations)
        {
            Bottom = bottom;
            Top = top;
            Right = right;
            Left = left;
            Back = back;
            Front = front;
            Rotations = rotations;
        }

        public Cube()
        : this(new Face(Colour.Red), new Face(Colour.Orange), new Face(Colour.Green), new Face(Colour.Blue), new Face(Colour.White), new Face(Colour.Yellow))
        { }

        public Face Bottom { get; }
        public Face Top { get; }
        public Face Right { get; }
        public Face Left { get; }
        public Face Back { get; }
        public Face Front { get; }
        public uint Rotations { get; }

        public Cube RotateFront(Rotation direction)
        {
            //         W W W                           W W W         
            //         W W W                           W W W         
            //         B O G                           O B G         
            //         - - -                           - - -         
            // G G G | R R R | R B B           G G O | R R R | B B B 
            // G G B | R R R | G B B     =>>   G G R | R R R | O B B 
            // G G O | R R R | B B B           G G G | R R R | G B B 
            //         - - -                           - - -         
            //         O R G                           B G R         
            //         Y Y Y                           Y Y Y         
            //         Y Y Y                           Y Y Y         
            //         - - -                           - - -        

            if (direction == Rotation.Anticlockwise)
            {
                var front = new Face(Front.TopRight, Front.MiddleRight, Front.BottomRight,
                    Front.TopMiddle, Front.MiddleMiddle, Front.BottomMiddle,
                    Front.TopLeft, Front.MiddleLeft, Front.BottomLeft);

                return new Cube(front, Back, Left, Right, Top, Bottom, Rotations + 1u);

            }
            else
            {
                var front = new Face(Front.BottomLeft, Front.MiddleLeft, Front.TopLeft,
                                     Front.BottomMiddle, Front.MiddleMiddle, Front.TopMiddle,
                                     Front.BottomRight, Front.MiddleRight, Front.TopRight);

                var left = Left.WithRightColumn(Bottom.GetTopRow().Rotate(Rotation.Clockwise));

                var right = Right.WithLeftColumn(Top.GetBottomRow().Rotate(Rotation.Clockwise));

                var top = Top.WithBottomRow(Left.GetRightColumn().Rotate(Rotation.Clockwise));

                var bottom = Bottom.WithTopRow(Right.GetLeftColumn().Rotate(Rotation.Clockwise));

                return new Cube(front, Back, left, right, top, bottom, Rotations + 1u);
            }
        }
    }
}
