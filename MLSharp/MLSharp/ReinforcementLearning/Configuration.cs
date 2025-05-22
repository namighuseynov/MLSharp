namespace MLSharp.ReinforcementLearning
{
    /// <summary>
    /// Learning config
    /// </summary>
    public class Configuration
    {
        #region Constructors
        public Configuration() {
            _learningRate = 0.1;
            _maxIterations = 1000;
            _discountFactor = 0.9;
            _explorationRate = 0.2;
            _brainPath = string.Empty;
            _continueLearning = false;
        }
        public Configuration(
            double learningRate,
            double maxInterations,
            double explorationRate,
            double discountFactor,
            bool continueLearning,
            string brainPath
            ) { 
            _learningRate = learningRate;
            _maxIterations = maxInterations;
            _discountFactor = discountFactor;
            _explorationRate = explorationRate;
            _brainPath = brainPath;
            _continueLearning= continueLearning;
        }
        #endregion

        #region Fields
        private double _learningRate;
        private double _maxIterations;
        private double _explorationRate;
        private double _discountFactor;
        private bool _continueLearning;

        private string _brainPath;
        #endregion

        #region Properties
        public double LearningRate { get { return _learningRate; } }
        public double MaxIterations { get { return _maxIterations; } }
        public double ExplorationRate { get {return _discountFactor;} }
        public double DiscountFactor { get { return _discountFactor;} }
        public string BrainPath { get { return _brainPath; } }
        public bool ContinueLearning { get { return _continueLearning; } }
        #endregion
    }
}
