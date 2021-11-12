namespace porductionLine_kai0ym.Entities
{
    internal class BallFactoryBase
    {
        public Abstractions.Toy CreateNew()
        {
            return new Ball();
        }

        public Toy CreateNew()
        {
            return new Ball(BallColor);
        }
    }
}