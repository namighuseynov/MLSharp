namespace MLSharp.Math
{
    public class Matrix : IMatrix
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="matrix"></param>
        public Matrix(double[,] matrix)
        {
            _matrix = matrix;
        }
        #endregion

        #region Fields
        /// <summary>
        /// The souce matrix
        /// </summary>
        private double[,] _matrix;
        #endregion

        #region Methods
        /// <summary>
        /// Matrix addition
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public IMatrix Add(Matrix other)
        {
            if (Rows != other.Rows || Columns != other.Columns)
                throw new ArgumentException("Matrix dimensions must match.");

            double[,] newMatrix = new double[Rows, Columns];
            
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    double newValue = _matrix[i, j] + other[i, j];
                    newMatrix[i, j] = newValue;
                }
            }

            return new Matrix(newMatrix);
        }
        /// <summary>
        /// Multiply matrix
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IMatrix Multiply(Matrix other)
        {
            if (this.Columns != other.Rows)
            {
                throw new ArgumentException("Invalid dimensions for matrix multiplication.");
            }

            double[,] result = new double[Rows, other.Columns]; 

            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < other.Columns; j++)
                    for (int k = 0; k < Columns; k++)
                        result[i, j] += this[i, k] * other[k, j];

            return new Matrix(result);

        }
        /// <summary>
        /// Scalar multiply
        /// </summary>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public IMatrix Multiply(double scalar)
        {
            double[,] result = new double[Rows, Columns];
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    result[i, j] = _matrix[i, j] * scalar;

            return new Matrix(result);
        }
        /// <summary>
        /// Transpose the matrix
        /// </summary>
        /// <returns></returns>
        public IMatrix Transpose()
        {
            double[,] result = new double[this.Columns, this.Rows];
            for (int i = 0; i < this.Rows; i++)
                for (int j = 0; j < this.Columns; j++)
                    result[j, i] = this[i, j];
            return new Matrix(result);
        }
        /// <summary>
        /// Print the matrix to console
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string matrixString = string.Empty;
            int m = Rows;
            int n = Columns;
            matrixString += "[";
            for (int i = 0; i < m; i++)
            {
                matrixString += "[";
                for (int j = 0; j < n; j++)
                {
                    matrixString += _matrix[i, j].ToString();
                    if (j != n - 1)
                    {
                        matrixString += ",";
                    }
                }
                matrixString += "]";
                if (i < m - 1)
                {
                    matrixString += ",\n";
                }
            }
            matrixString += "]";
            return matrixString;
        }
        /// <summary>
        /// Determinant of the matrix
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public double Determinant()
        {
            if (Rows != Columns)
                throw new InvalidOperationException("Determinant can only be calculated for square matrices.");

            return CalculateDeterminant(_matrix);
        }
        /// <summary>
        /// Determinant calculation
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        private double CalculateDeterminant(double[,] matrix)
        {
            int size = matrix.GetLength(0);
            if (size == 1)
                return matrix[0, 0];
            if (size == 2)
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

            double det = 0;
            for (int p = 0; p < size; p++)
            {
                double[,] subMatrix = CreateSubMatrix(matrix, 0, p);
                det += (p % 2 == 0 ? 1 : -1) * matrix[0, p] * CalculateDeterminant(subMatrix);
            }
            return det;
        }
        /// <summary>
        /// Creating of sub matrix
        /// </summary>
        /// <param name="matrix">source matrix</param>
        /// <param name="excludingRow">Excluding row</param>
        /// <param name="excludingCol">Excluding column</param>
        /// <returns></returns>
        private double[,] CreateSubMatrix(double[,] matrix, int excludingRow, int excludingCol)
        {
            int size = matrix.GetLength(0);
            double[,] subMatrix = new double[size - 1, size - 1];
            int r = -1;
            for (int i = 0; i < size; i++)
            {
                if (i == excludingRow)
                    continue;
                r++;
                int c = -1;
                for (int j = 0; j < size; j++)
                {
                    if (j == excludingCol)
                        continue;
                    subMatrix[r, ++c] = matrix[i, j];
                }
            }
            return subMatrix;
        }
        public static Matrix Identity(int size)
        {
            double[,] identity = new double[size, size];
            for (int i = 0; i < size; i++)
                identity[i, i] = 1;
            return new Matrix(identity);
        }
        /// <summary>
        /// zero matrix with size n
        /// </summary>
        /// <param name="rows">Rows</param>
        /// <param name="columns">Columns</param>
        /// <returns></returns>
        public static Matrix Zeros(int rows, int columns)
        {
            return new Matrix(new double[rows, columns]);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Rows
        /// </summary>
        public int Rows { get { return _matrix.GetLength(0); } }
        /// <summary>
        /// Columns
        /// </summary>
        public int Columns { get { return _matrix.GetLength(1); } }
        /// <summary>
        /// Size of matrix
        /// </summary>
        public string Size { get { return "[" + this.Rows + ", " + this.Columns + "]"; } }
        #endregion

        #region Operators
        /// <summary>
        /// Index operation
        /// </summary>
        /// <param name="row">Row</param>
        /// <param name="col">Column</param>
        /// <returns></returns>
        public double this[int row, int col]
        {
            get => _matrix[row, col];
            set => _matrix[row, col] = value;
        }
        #endregion
    }
}
