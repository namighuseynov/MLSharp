namespace MLSharp.ReinforcementLearning
{
    /// <summary>
    /// 
    /// </summary>
    public class Configuration
    {
        #region Constructors
        public Configuration(
            double learningRate,
            double maxInterations,
            double explorationRate,
            double discountFactor
            ) { 
            _learningRate = learningRate;
            _maxIterations = maxInterations;
            _discountFactor = discountFactor;
            _explorationRate = explorationRate;
        }
        #endregion

        #region Fields
        private double _learningRate;
        private double _maxIterations;
        private double _explorationRate;
        private double _discountFactor;
        #endregion

        #region Properties
        public double LearningRate { get { return _learningRate; } }
        public double MaxIterations { get { return _maxIterations; } }
        public double ExplorationRate { get {return _discountFactor;} }
        public double DiscountFactor { get { return _discountFactor;} }
        #endregion
    }
}
