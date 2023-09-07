namespace Tetris
{
    public class Coordinate   //the class stores a row and a column
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Coordinate(int row, int column)  //constructor that initializes a new instance of the Coordinator class with the specified row and column coordinates
        {
            Row = row;
            Column = column;
        }
    }
}
