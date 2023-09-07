namespace Tetris
{
    public class IBlock : Block
    {
        private readonly Coordinate[][] tiles = new Coordinate[][]
        {
            new Coordinate[] { new(1,0), new(1,1), new(1,2), new(1,3) },
            new Coordinate[] { new(0,2), new(1,2), new(2,2), new(3,2) },
            new Coordinate[] { new(2,0), new(2,1), new(2,2), new(2,3) },
            new Coordinate[] { new(0,1), new(1,1), new(2,1), new(3,1) }
        };

        public override int Id => 1;  //the ID of the block that allows us to identify it
        protected override Coordinate StartOffset => new Coordinate(-1, 3); //the starting position of the block
        public override Coordinate[][] Tiles => tiles;
    }
}
