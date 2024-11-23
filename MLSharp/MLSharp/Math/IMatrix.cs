namespace MLSharp.Math
{
    public interface IMatrix
    {
        public IMatrix Add(Matrix other);
        public IMatrix Multiply(Matrix other);
        public IMatrix Transpose();
    }
}
