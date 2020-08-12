using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace RubiksCube
{

    //   T
    // L F R
    //   B
    //   b

    public enum Rotation
    {
        Clockwise,
        Anticlockwise
    }
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

                return new Cube(front, Back, Left, Right, Top, Bottom, Rotations + 1u);
            }
        }
    }

    public readonly struct Face
    {
        public Face(Colour colour)
        : this(colour, colour, colour, colour, colour, colour, colour, colour, colour)
        {

        }

        public Face(Colour topLeft, Colour topMiddle, Colour topRight, Colour middleLeft, Colour middleMiddle, Colour middleRight, Colour bottomLeft, Colour bottomMiddle, Colour bottomRight)
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

        public Colour TopLeft { get; }
        public Colour TopMiddle { get; }
        public Colour TopRight { get; }
        public Colour MiddleLeft { get; }
        public Colour MiddleMiddle { get; }
        public Colour MiddleRight { get; }
        public Colour BottomLeft { get; }
        public Colour BottomMiddle { get; }
        public Colour BottomRight { get; }
    }


    public enum Colour : byte
    {
        Red,
        Orange,
        Green,
        Blue,
        White,
        Yellow
    }

    public class UnitTest1
    {
        [Fact]
        public void CompletedCube()
        {
            var cube = new Cube();

            using var _ = new AssertionScope();
            AssertSideIsComplete(cube.Front, Colour.Red);
            AssertSideIsComplete(cube.Back, Colour.Orange);
            AssertSideIsComplete(cube.Left, Colour.Green);
            AssertSideIsComplete(cube.Right, Colour.Blue);
            AssertSideIsComplete(cube.Top, Colour.White);
            AssertSideIsComplete(cube.Bottom, Colour.Yellow);

        }

        private void AssertSideIsComplete(Face face, Colour colour)
        {
            face.TopLeft.Should().Be(colour);
            face.TopMiddle.Should().Be(colour);
            face.TopRight.Should().Be(colour);
            face.MiddleLeft.Should().Be(colour);
            face.MiddleMiddle.Should().Be(colour);
            face.MiddleRight.Should().Be(colour);
            face.BottomLeft.Should().Be(colour);
            face.BottomMiddle.Should().Be(colour);
            face.BottomRight.Should().Be(colour);
        }

        private void AssertSideIs(Face face,
            Colour topLeft, Colour topMiddle, Colour topRight,
            Colour middleLeft, Colour middle, Colour middleRight,
            Colour bottomLeft, Colour bottomMiddle, Colour bottomRight)
        {
            face.TopLeft.Should().Be(topLeft);
            face.TopMiddle.Should().Be(topMiddle);
            face.TopRight.Should().Be(topRight);
            face.MiddleLeft.Should().Be(middleLeft);
            face.MiddleMiddle.Should().Be(middle);
            face.MiddleRight.Should().Be(middleRight);
            face.BottomLeft.Should().Be(bottomLeft);
            face.BottomMiddle.Should().Be(bottomMiddle);
            face.BottomRight.Should().Be(bottomRight);

        }

        [Fact]
        public void RotateFrontClockwise()
        {
            /*
                R G B     R O R 
                O G Y  => W G G 
                R W B     B Y B
            */

            var cube = new Cube(new Face(Colour.Red, Colour.Green, Colour.Blue,
                Colour.Orange, Colour.Green, Colour.Yellow,
                Colour.Red, Colour.White, Colour.Blue), new Face(Colour.Red), new Face(Colour.Red), new Face(Colour.Red), new Face(Colour.Red), new Face(Colour.Red));

            var rotatedCube = cube.RotateFront(Rotation.Clockwise);

            using var _ = new AssertionScope();
            AssertSideIs(rotatedCube.Front,
                Colour.Red, Colour.Orange, Colour.Red,
                Colour.White, Colour.Green, Colour.Green,
                Colour.Blue, Colour.Yellow, Colour.Blue);

            rotatedCube.Rotations.Should().Be(1);
        }

        [Fact]
        public void RotateFrontAntiClockwise()
        {
            /*
                 R O R    R G B 
                 W G G => O G Y 
                 B Y B    R W B 
            */

            var cube = new Cube(new Face(Colour.Red, Colour.Orange, Colour.Red,
                Colour.White, Colour.Green, Colour.Green,
                Colour.Blue, Colour.Yellow, Colour.Blue), new Face(Colour.Red), new Face(Colour.Red), new Face(Colour.Red), new Face(Colour.Red), new Face(Colour.Red));

            var rotatedCube = cube.RotateFront(Rotation.Anticlockwise);

            using var _ = new AssertionScope();
            AssertSideIs(rotatedCube.Front, Colour.Red, Colour.Green, Colour.Blue,
                Colour.Orange, Colour.Green, Colour.Yellow,
                Colour.Red, Colour.White, Colour.Blue
                );
            rotatedCube.Rotations.Should().Be(1);
        }


        [Fact]
        public void RotateFrontClockwiseFourTimesReturnsToTheStart()
        {
            var cube = new Cube(new Face(Colour.Red, Colour.Green, Colour.Blue,
                                         Colour.Orange, Colour.Green, Colour.Yellow,
                                         Colour.Red, Colour.White, Colour.Blue),
                                new Face(Colour.Red), new Face(Colour.Red), new Face(Colour.Red), new Face(Colour.Red), new Face(Colour.Red));

            var rotatedCube = cube
                                .RotateFront(Rotation.Clockwise)
                                .RotateFront(Rotation.Clockwise)
                                .RotateFront(Rotation.Clockwise)
                                .RotateFront(Rotation.Clockwise);

            using var _ = new AssertionScope();
            AssertSideIs(rotatedCube.Front,
                        Colour.Red, Colour.Green, Colour.Blue,
                        Colour.Orange, Colour.Green, Colour.Yellow,
                        Colour.Red, Colour.White, Colour.Blue);
            rotatedCube.Rotations.Should().Be(4);
        }
    }
}
