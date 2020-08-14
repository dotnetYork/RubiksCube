using System;

namespace RubiksCube
{
    internal class Column
    {
        public Colour Top { get; }
        public Colour Middle { get; }
        public Colour Bottom { get; }

        internal Column(Colour top, Colour middle, Colour bottom)
        {
            Top = top;
            Middle = middle;
            Bottom = bottom;
        }

        public Row Rotate(Rotation rotation)
        {
            return rotation switch
            {
                Rotation.Clockwise => new Row(Bottom, Middle, Top),
                Rotation.Anticlockwise => new Row(Top, Middle, Bottom),
                _ => throw new NotImplementedException()
            };
        }
    }
}