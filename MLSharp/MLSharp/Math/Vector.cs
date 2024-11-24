namespace MLSharp.Math
{
    /// <summary>
    /// Vector class
    /// </summary>
    public class Vector
    {
        #region Constructors
        public Vector(double[] data)
        {
            _vector = (double[])data.Clone();
        }
        #endregion

        #region Fields
        /// <summary>
        /// Vector
        /// </summary>
        private double[] _vector;
        #endregion

        #region Properties
        public int Length { get { return _vector.Length; } }
        #endregion

        #region Methods
        /// <summary>
        /// Vector Addition
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Vector Add(Vector other)
        {
            if (this.Length != other.Length)
                throw new ArgumentException("Vectors must have the same length.");
            double[] result = new double[Length];
            for (int i = 0; i < Length; i++)
                result[i] = this[i] + other[i];

            return new Vector(result);
        }
        /// <summary>
        /// Dot operation
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public double Dot(Vector other)
        {
            if (Length != other.Length)
                throw new ArgumentException("Vectors must have the same dimension for dot product.");

            double result = 0;
            for (int i = 0; i < Length; i++)
                result += this[i] * other[i];

            return result;
        }
        /// <summary>
        /// Cross operation
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Vector Cross(Vector other)
        {
            if (Length != 3 || other.Length != 3)
                throw new ArgumentException("Cross product is only defined for 3D vectors.");

            return new Vector(new double[]
            {
                this[1] * other[2] - this[2] * other[1],
                this[2] * other[0] - this[0] * other[2],
                this[0] * other[1] - this[1] * other[0]
            });

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double Magnitude()
        {
            double result = 0;
            for (int i = 0; i < Length; i++)
                result += System.Math.Pow(this[i], 2);
            return System.Math.Sqrt(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Vector Normalize()
        {
            double magnitude = Magnitude();
            if (magnitude < 1e-10)
                throw new InvalidOperationException("Cannot normalize a zero vector.");

            for (int i = 0; i < Length; i++) 
                this[i] = this[i] / magnitude;

            return new Vector(_vector);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"[{string.Join(", ", _vector)}]";
        }
        #endregion

        #region Operators
        public double this[int index]
        {
            get =>  _vector[index];
            set => _vector[index] = value;
        }
        #endregion
    }
}
