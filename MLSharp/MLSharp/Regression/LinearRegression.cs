using MLSharp.Math;

namespace MLSharp.Regression
{
    /// <summary>
    /// Linear Regression model
    /// </summary>
    public class LinearRegression
    {
        #region Fields
        //Parameters
        private double _bias = 0.0;
        private Matrix _weights;
        //Datas
        private Matrix _X;
        private Matrix _Y;
        #endregion

        #region Methods
        /// <summary>
        /// Fit the train data
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        public void Fit(Matrix X, Matrix Y)
        {
            if (X.Rows != Y.Rows)
                throw new ArgumentException("Number of rows in X and Y must be the same.");
            if (Y.Columns != 1)
                throw new ArgumentException("Y must be a vector (matrix with one column).");

            _X = X;
            _Y = Y;
            _weights = new Matrix(new double[X.Columns, 1]);
            Random random = new Random();
            for (int i = 0; i < X.Columns; i++)
            {
                _weights[i, 0] = random.NextDouble() * 0.01;
            }

        }
        /// <summary>
        /// Train the model
        /// </summary>
        /// <param name="learningRate">Learning rate</param>
        public void Train(double learningRate, double epsilon)
        {
            if (_X == null || _Y == null)
                throw new InvalidOperationException("Model should be initialized by calling Fit.");
            if (learningRate <= 0)
                throw new ArgumentException("Learning rate must be positive.");
            if (epsilon <= 0)
                throw new ArgumentException("Epsilon must be positive.");

            while (true)
            {
                double loss = Evaluate();
                if (loss <= epsilon)
                    break;

                Update(learningRate);
                Console.WriteLine(loss);
            }
        }
        public Matrix Predict(double x)
        {
            Matrix data = new Matrix(new double[1, 1]);
            data[0, 0] = x;
            return F(data);
        }
        /// <summary>
        /// Evaluate the model
        /// </summary>
        /// <returns></returns>
        private double Evaluate()
        {
            double loss = Matrix.Sum(Matrix.SquareElements(F(_X) - _Y))/_X.Rows;
            return loss;
        }

        private Matrix F(Matrix X)
        {
            Matrix f = Matrix.Multiply(X, _weights);
            for (int i = 0; i < f.Rows; i++)
                f[i, 0] += _bias;
            return f;
        }

        private void Update(double learningRate)
        {
            Matrix dW = Matrix.Multiply((Matrix)_X.Transpose(), (F(_X) - _Y));
            dW = (Matrix) dW.Multiply(2.0/_X.Rows);
            double db = 2.0 * Matrix.Sum(F(_X) - _Y)/_X.Rows;

            for (int i = 0; i < _weights.Rows; i++)
            {
                _weights[i, 0] -= learningRate * dW[i, 0];
            }

            _bias -= learningRate * db;
        }
        #endregion
    }
}
