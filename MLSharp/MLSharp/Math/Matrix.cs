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
        /// <summary>
        /// Sum of the matrix elements
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static double Sum(Matrix data)
        {
            double sum = 0;
            for (int i = 0; i < data.Rows; i++)
                for (int j = 0; j < data.Columns; j++)
                    sum += data[i, j];
            return sum;
        }
        public static Matrix Multiply(Matrix mat1,  Matrix mat2)
        {
            if (mat1.Columns != mat2.Rows)
            {
                throw new ArgumentException("Invalid dimensions for matrix multiplication.");
            }

            double[,] result = new double[mat1.Rows, mat2.Columns];

            for (int i = 0; i < mat1.Rows; i++)
                for (int j = 0; j < mat2.Columns; j++)
                    for (int k = 0; k < mat1.Columns; k++)
                        result[i, j] += mat1[i, k] * mat2[k, j];

            return new Matrix(result);
        }
        /// <summary>
        /// Returns a new matrix where each element is squared.
        /// </summary>
        /// <param name="matrix">The input matrix.</param>
        /// <returns>A new matrix with squared elements.</returns>
        public static Matrix SquareElements(Matrix matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix), "Input matrix cannot be null.");

            double[,] result = new double[matrix.Rows, matrix.Columns];

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    result[i, j] = matrix[i, j] * matrix[i, j];
                }
            }

            return new Matrix(result);
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
        /// <summary>
        /// Addition operator for matrices.
        /// </summary>
        /// <param name="a">First matrix.</param>
        /// <param name="b">Second matrix.</param>
        /// <returns>New matrix representing the sum of two matrices.</returns>
        /// <exception cref="ArgumentException">Thrown when matrix dimensions do not match.</exception>
        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Columns != b.Columns)
                throw new ArgumentException("Matrix dimensions must match for addition.");

            double[,] result = new double[a.Rows, a.Columns];

            for (int i = 0; i < a.Rows; i++)
            {
                for (int j = 0; j < a.Columns; j++)
                {
                    result[i, j] = a[i, j] + b[i, j];
                }
            }

            return new Matrix(result);
        }

        /// <summary>
        /// Subtraction operator for matrices.
        /// </summary>
        /// <param name="a">First matrix.</param>
        /// <param name="b">Second matrix.</param>
        /// <returns>New matrix representing the difference of two matrices.</returns>
        /// <exception cref="ArgumentException">Thrown when matrix dimensions do not match.</exception>
        public static Matrix operator -(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Columns != b.Columns)
                throw new ArgumentException("Matrix dimensions must match for subtraction.");

            double[,] result = new double[a.Rows, a.Columns];

            for (int i = 0; i < a.Rows; i++)
            {
                for (int j = 0; j < a.Columns; j++)
                {
                    result[i, j] = a[i, j] - b[i, j];
                }
            }

            return new Matrix(result);
        }
        #endregion
    }
}
