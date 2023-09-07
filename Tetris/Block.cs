using System;
using System.Collections.Generic;

namespace Tetris
{
    public abstract class Block
    {
        public abstract Coordinate[][] Tiles { get; }  //the array includes the tile locations in the 4 rotational states of every block
        protected abstract Coordinate StartOffset { get; }  //determines where the block starts off on the grid
        public abstract int Id { get; }  //integer ID to help us distinguish the block

        public int rotationState;  //stores the current rotation state of the block
        private Coordinate offset; //represents the offset position of the block on the grid

        public Block()
        {
            offset = new Coordinate(StartOffset.Row, StartOffset.Column);
        }

        public IEnumerable<Coordinate> TilePositions()  //the method loops over the tile positions in the current rotaation state and adds the row offset and column offset
        {
            foreach (Coordinate p in Tiles[rotationState])
            {
                yield return new Coordinate(p.Row + offset.Row, p.Column + offset.Column);
            }
        }


        public void Move(int rows, int columns)  //this method lets us to move the block in any direction/it changes the offset of the block on the grid
        {
            offset.Row += rows;
            offset.Column += columns;
        }

        public void Reset()  //this method resets the state of the block, when the new block begins to come down
        {
            rotationState = 0;
            offset.Row = StartOffset.Row;
            offset.Column = StartOffset.Column;
        }
    }

        public class BlockQueue
        {
            private readonly Block[] blocks = new Block[] //the array of blocks
            {
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new OBlock(),
            new SBlock(),
            new TBlock(),
            new ZBlock()
            };

            private readonly Random random = new Random();

            public Block NextBlock { get; private set; }

            public BlockQueue()
            {
                NextBlock = RandomBlock();
            }

            private Block RandomBlock()  //this method picks a random block out of the block array above
            {
                return blocks[random.Next(blocks.Length)];
            }

            public Block StoreAndGenerateNextBlock() //this method generates the next block using the RandomBLock method defined above
            {
                Block block = NextBlock;

                do
                {
                    NextBlock = RandomBlock();
                }
                while (block.Id == NextBlock.Id);

                return block;
            }
        }
}
