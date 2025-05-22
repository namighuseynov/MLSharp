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
            _epsilonDecay = 0.995;
            _minEpsilon = 0.01;
            _brainPath = string.Empty;
        }
        public Configuration(
            double learningRate,
            double maxIterations,
            double explorationRate,
            double discountFactor,
            double epsilonDecay,
            double minEpsilon,
            string brainPath
            ) { 
            _learningRate = learningRate;
            _maxIterations = maxIterations;
            _discountFactor = discountFactor;
            _explorationRate = explorationRate;
            _brainPath = brainPath;
            _epsilonDecay = epsilonDecay;
            _minEpsilon = minEpsilon;
        }
        #endregion

        #region Fields
        private double _learningRate;
        private double _maxIterations;
        private double _discountFactor;
        private double _explorationRate;
        private double _epsilonDecay;
        private double _minEpsilon;
        

        private string _brainPath;
        #endregion

        #region Properties
        public double LearningRate { get { return _learningRate; } }
        public double MaxIterations { get { return _maxIterations; } }
        public double ExplorationRate { get {return _explorationRate;} set { _explorationRate = value; } }
        public double DiscountFactor { get { return _discountFactor;} }
        public double EpsilonDecay { get { return _epsilonDecay; } }
        public double MinEpsilon { get { return _minEpsilon;} }
        public string BrainPath { get { return _brainPath; } }
        #endregion
    }
}
