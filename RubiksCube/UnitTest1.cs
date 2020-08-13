using System;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace RubiksCube
{
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
        public void RotateFrontClockwiseFrontShouldBe()
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

        [Theory]
        [InlineData(Rotation.Clockwise)]
        [InlineData(Rotation.Anticlockwise)]
        public void RotateFrontClockwiseBackShouldBeSame(Rotation direction)
        {
            /*
                R G B     
                O G Y  => Same 
                R W B     
            */

            var cube = new Cube(new Face(Colour.Red),
                new Face(Colour.Red, Colour.Green, Colour.Blue,
                Colour.Orange, Colour.Green, Colour.Yellow,
                Colour.Red, Colour.White, Colour.Blue), new Face(Colour.Red), new Face(Colour.Red), new Face(Colour.Red), new Face(Colour.Red));

            var rotatedCube = cube.RotateFront(direction);

            using var _ = new AssertionScope();
            AssertSideIs(rotatedCube.Back,
                Colour.Red, Colour.Green, Colour.Blue,
                Colour.Orange, Colour.Green, Colour.Yellow,
                Colour.Red, Colour.White, Colour.Blue);

            rotatedCube.Rotations.Should().Be(1);
        }

        [Fact]
        public void RotateFrontClockwiseAdjacentFacesShouldBeRotated()
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

            var front = new Face(Colour.Red);
            var left = CreateFace("GGGGGBGGO");
            var right = CreateFace("RBBGBBBBB");
            var top = CreateFace("WWWWWWBOG");
            var bottom = CreateFace("ORGYYYYYY");
            var back = new Face(Colour.Yellow);

            var cube = new Cube(front, back, left, right, top, bottom);

            var rotatedCube = cube.RotateFront(Rotation.Clockwise);

            AssertSideIsComplete(rotatedCube.Front, Colour.Red);
            AssertSideIsComplete(rotatedCube.Back, Colour.Yellow);
            rotatedCube.Should().BeEquivalentTo(new
            {
                Left = CreateFace("GGOGGRGGG"),
                Right= CreateFace("BBBOBBGBB"),
                Top = CreateFace("WWWWWWOBG"),
                Bottom = CreateFace("BGRYYYYYY")
            });

            rotatedCube.Rotations.Should().Be(1);
        }

        public static Face CreateFace(string colours)
        {
            if (colours.Length != 9) throw new Exception($"Have {colours.Length} colours for this face ({colours}) instead of nine.");

            Colour ColourFromChar(char c)
                => c switch
                {
                    'R' => Colour.Red,
                    'G' => Colour.Green,
                    'B' => Colour.Blue,
                    'Y' => Colour.Yellow,
                    'O' => Colour.Orange,
                    'W' => Colour.White,
                    _ => throw new NotImplementedException()
                };

            return new Face(ColourFromChar(colours[0]), ColourFromChar(colours[1]), ColourFromChar(colours[2]),
                             ColourFromChar(colours[3]), ColourFromChar(colours[4]), ColourFromChar(colours[5]),
                             ColourFromChar(colours[6]), ColourFromChar(colours[7]), ColourFromChar(colours[8])
                );
        }

        [Fact]
        public void RotateFrontAntiClockwiseThenFrontFaceShouldBeRotated()
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
