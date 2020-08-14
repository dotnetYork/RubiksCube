using System;

namespace RubiksCube
{
    internal class Row
    {
        public Colour Left { get; }
        public Colour Middle { get; }
        public Colour Right { get; }

        internal Row(Colour left, Colour middle, Colour right)
        {
            Left = left;
            Middle = middle;
            Right = right;
        }

        public Column Rotate(Rotation rotation)
        {
            return rotation switch
            {
                Rotation.Clockwise => new Column(Left, Middle, Right),
                Rotation.Anticlockwise => new Column(Right, Middle, Left),
                _ => throw new NotImplementedException()
            };
        }
    }
}