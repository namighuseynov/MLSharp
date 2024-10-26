namespace MLSharp.ReinforcementLearning
{
    /// <summary>
    /// 
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
        }
        public Configuration(
            double learningRate,
            double maxInterations,
            double explorationRate,
            double discountFactor,
            string brainPath
            ) { 
            _learningRate = learningRate;
            _maxIterations = maxInterations;
            _discountFactor = discountFactor;
            _explorationRate = explorationRate;
            _brainPath = brainPath;
        }
        #endregion

        #region Fields
        private double _learningRate;
        private double _maxIterations;
        private double _explorationRate;
        private double _discountFactor;

        private string _brainPath;
        #endregion

        #region Properties
        public double LearningRate { get { return _learningRate; } }
        public double MaxIterations { get { return _maxIterations; } }
        public double ExplorationRate { get {return _discountFactor;} }
        public double DiscountFactor { get { return _discountFactor;} }
        public string BrainPath { get { return _brainPath; } }
        #endregion
    }
}
