namespace Tetris
{
    public class OBlock : Block
    {
        private readonly Coordinate[][] tiles = new Coordinate[][]
        {
            new Coordinate[] { new(0,0), new(0,1), new(1,0), new(1,1) }  //we can provide one rotation state since it rotates to the same position
        };

        public override int Id => 4;
        protected override Coordinate StartOffset => new Coordinate(0, 4);
        public override Coordinate[][] Tiles => tiles;
    }
}
