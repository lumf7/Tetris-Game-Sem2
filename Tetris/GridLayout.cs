namespace Tetris
{
    public class GridLayout     
    {
        private readonly int[,] grid;  //holds a two dimensional array rows and columns
        public int Rows { get; }
        public int Columns { get; }

        public int this[int r, int c]    //indexer that provides access to the array
        {
            get => grid[r, c];
            set => grid[r, c] = value;
        }

        public GridLayout(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            grid = new int[rows, columns];   //initialize the array
        }

        public bool IsInside(int r, int c)  //checks if a cell inside the grid
        {
            return r >= 0 && r < Rows && c >= 0 && c < Columns;
        }

        public bool IsEmpty(int r, int c)   //checks if a cell is empty or not
        {
            return IsInside(r, c) && grid[r, c] == 0;
        }

        public bool IsRowFull(int r)    //checks if an entire row is full
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsRowEmpty(int r)    //checks if an entire row is empty
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        private void ClearRow(int r)  //clears a row
        {
            for (int c = 0; c < Columns; c++)
            {
                grid[r, c] = 0;
            }
        }

        public int RemoveCompleteRow()   //combines the earlier methods to actually clear full rows depending on the variable cleared
        {
            int cleared = 0;

            for (int r = Rows-1; r >= 0; r--) //from bottom to top
            {
                if (IsRowFull(r)) //if it's full we clear the row and increment clear
                {
                    ClearRow(r);  //clearing a row
                    cleared++;
                }
                else if (cleared > 0) //else if the row is not full and one row has been cleared previosuly, we move the row down in the value of the cleared variable
                {
                    for (int c = 0; c < Columns; c++)   //moves the row down by a certain number of rows
                    {
                        grid[r + cleared, c] = grid[r, c];
                        grid[r, c] = 0;
                    }
                }
            }

            return cleared;
        }
    }
}