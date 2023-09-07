namespace Tetris
{
    public class BoardPhase
    {
        private Block currentBlock; //identifies which block is falling

        public Block CurrentBlock  //gets or sets the currently block
        {
            get => currentBlock;
            private set
            {
                currentBlock = value;
                currentBlock.Reset();

                for (int i = 0; i < 2; i++)
                {
                    currentBlock.Move(1, 0);

                    if (!BlockFits())  //if the block doesn't fit it moves
                    {
                        currentBlock.Move(-1, 0);
                    }
                }
            }
        }

        public GridLayout GridLayout { get; }  //gets the game grid
        public BlockQueue BlockQueue { get; }  //gets the block queue responsible for generating new blocks
        public bool RoundOver { get; private set; }  //gets a value indicating whether the round is over
        public int Score { get; private set; }  //gets the current player's score
        public Block HeldBlock { get; private set; }  //gets or sets the block that is currently being held by the player
        public bool CanHold { get; private set; }  //gets a value indicating whether the player can hold the current block

        public BoardPhase()  //initializes a new instance of the BoardPhase class, creating a game grid and block queue
        {
            GridLayout = new GridLayout(22, 10);
            BlockQueue = new BlockQueue();
            CurrentBlock = BlockQueue.StoreAndGenerateNextBlock();
            CanHold = true;
        }

        private bool BlockFits()  //this method checks whether the block fits 
        {
            foreach (Coordinate p in CurrentBlock.TilePositions())
            {
                if (!GridLayout.IsEmpty(p.Row, p.Column))
                {
                    return false;
                }
            }

            return true;
        }

        public void HoldBlock()
        {
            if (!CanHold)
            {
                return;
            }

            if (HeldBlock == null)
            {
                HeldBlock = CurrentBlock;
                CurrentBlock = BlockQueue.StoreAndGenerateNextBlock();
            }
            else
            {
                Block tmp = CurrentBlock;
                CurrentBlock = HeldBlock;
                HeldBlock = tmp;
            }

            CanHold = false;
        }

        public void RotateBlockCW()  //rotate the block clockwise
        {

            CurrentBlock.rotationState = (CurrentBlock.rotationState + 1) % CurrentBlock.Tiles.Length;

            if (!BlockFits())
            {
                if (CurrentBlock.rotationState == 0)
                {
                    CurrentBlock.rotationState = CurrentBlock.Tiles.Length - 1;
                }
                else
                {
                    CurrentBlock.rotationState--;
                }
            }
        }

        public void RotateBlockCCW()  //rotate the block counter clockwise
        {
            if (CurrentBlock.rotationState == 0)
            {
                CurrentBlock.rotationState = CurrentBlock.Tiles.Length - 1;
            }
            else
            {
                CurrentBlock.rotationState--;
            }

            if (!BlockFits())
            {
                CurrentBlock.rotationState = (CurrentBlock.rotationState + 1) % CurrentBlock.Tiles.Length;
            }
        }

        public void MoveBlockLeft()  //we first check if the block fits and then move to the left
        {
            CurrentBlock.Move(0, -1);

            if (!BlockFits())
            {
                CurrentBlock.Move(0, 1);
            }
        }

        public void MoveBlockRight()  //we first check if the block fits and then move it to the right
        {
            CurrentBlock.Move(0, 1);

            if (!BlockFits())
            {
                CurrentBlock.Move(0, -1);
            }
        }

        private bool EndGame()  //we check if the game has ended, that means the blocks have reached the top
        {
            return !(GridLayout.IsRowEmpty(0) && GridLayout.IsRowEmpty(1));
        }

        private void PlaceBlock()  //checks and makes sure the block can be placed
        {
            foreach (Coordinate p in CurrentBlock.TilePositions())
            {
                GridLayout[p.Row, p.Column] = CurrentBlock.Id;
            }

            Score += GridLayout.RemoveCompleteRow();  //checks if any rows need to be cleared

            if (EndGame())
            {
                RoundOver = true;  //if the blocks reach the top and the two top rows are not clear, the game ends
            }
            else
            {
                CurrentBlock = BlockQueue.StoreAndGenerateNextBlock();  //else we continue with other blocks
                CanHold = true;
            }
        }

        public void MoveBlockDown()  //checks i the block fits and moves it down
        {
            CurrentBlock.Move(1, 0);

            if (!BlockFits())
            {
                CurrentBlock.Move(-1, 0);
                PlaceBlock();
            }
        }

        private int TileDropDistance(Coordinate p)  //calculates the vertical distance a tile in the current block can drop before hitting another block or the bottom
        {
            int drop = 0;

            while (GridLayout.IsEmpty(p.Row + drop + 1, p.Column))
            {
                drop++;
            }

            return drop;
        }

        public int BlockDropDistance()  //calculates the maximum distance the current block can drop within the grid before hitting another block or the bottom
        {
            int drop = GridLayout.Rows;

            foreach (Coordinate p in CurrentBlock.TilePositions())
            {
                drop = System.Math.Min(drop, TileDropDistance(p));
            }

            return drop;
        }

        public void DropBlock()  //instantly drops the block to the maximum possible position within the grid and places it (the player can choose to instantly drop the block)
        {
            CurrentBlock.Move(BlockDropDistance(), 0);
            PlaceBlock();
        }
    }
}
