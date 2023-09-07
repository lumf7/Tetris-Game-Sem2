namespace Tetris
{
    public class ZBlock : Block
    {
        public override int Id => 7;

        protected override Coordinate StartOffset => new(0, 3);

        public override Coordinate[][] Tiles => new Coordinate[][] {
            new Coordinate[] {new(0,0), new(0,1), new(1,1), new(1,2)},
            new Coordinate[] {new(0,2), new(1,1), new(1,2), new(2,1)},
            new Coordinate[] {new(1,0), new(1,1), new(2,1), new(2,2)},
            new Coordinate[] {new(0,1), new(1,0), new(1,1), new(2,0)}
        };
    }
}
